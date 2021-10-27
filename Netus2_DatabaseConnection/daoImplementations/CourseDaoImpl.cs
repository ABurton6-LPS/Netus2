using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class CourseDaoImpl : ICourseDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();
        public int? _taskId = null;

        public void SetTaskId(int taskId)
        {
            _taskId = taskId;
        }

        public int? GetTaskId()
        {
            return _taskId;
        }

        public void Delete(Course course, IConnectable connection)
        {
            if (course.Id <= 0)
                throw new Exception("Cannot delete a course which doesn't have a database-assigned ID.\n" + course.ToString());

            Delete_ClassEnrolled(course, connection);
            Delete_JctCourseSubject(course, connection);
            Delete_JctCourseGrade(course, connection);

            DataRow row = daoObjectMapper.MapCourse(course);

            string sql = "DELETE FROM course WHERE " +
                "course_id = @course_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@course_id", course.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void Delete_ClassEnrolled(Course course, IConnectable connection)
        {
            IClassEnrolledDao classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
            List<ClassEnrolled> foundClassEnrolleds = classEnrolledDaoImpl.Read_UsingCourseId(course.Id, connection);
            foreach (ClassEnrolled classEnrolled in foundClassEnrolleds)
            {
                classEnrolledDaoImpl.Delete(classEnrolled, connection);
            }
        }

        private void Delete_JctCourseSubject(Course course, IConnectable connection)
        {
            IJctCourseSubjectDao jctCourseSubjectDaoImpl = DaoImplFactory.GetJctCourseSubjectDaoImpl();
            foreach (Enumeration subject in course.Subjects)
            {
                jctCourseSubjectDaoImpl.Delete(course.Id, subject.Id, connection);
            }
        }

        private void Delete_JctCourseGrade(Course course, IConnectable connection)
        {
            IJctCourseGradeDao jctCourseGradeDaoImpl = DaoImplFactory.GetJctCourseGradeDaoImpl();
            foreach (Enumeration grade in course.Grades)
            {
                jctCourseGradeDaoImpl.Delete(course.Id, grade.Id, connection);
            }
        }

        public Course Read_UsingCourseId(int courseId, IConnectable connection)
        {
            string sql = "SELECT * FROM course WHERE course_id = @course_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@course_id", courseId));

            List<Course> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching courseId: " + courseId);
        }

        public List<Course> Read(Course course, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapCourse(course);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM course WHERE 1=1 ");
            if (row["course_id"] != DBNull.Value)
            {
                sql.Append("AND course_id = @course_id");
                parameters.Add(new SqlParameter("@course_id", row["course_id"]));
            }
            else
            {
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("AND name = @name ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }

                if (row["course_code"] != DBNull.Value)
                {
                    sql.Append("AND course_code = @course_code");
                    parameters.Add(new SqlParameter("@course_code", row["course_code"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<Course> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtCourse = DataTableFactory.CreateDataTable_Netus2_Course();
            dtCourse = connection.ReadIntoDataTable(sql, dtCourse, parameters);

            List<Course> results = new List<Course>();
            foreach (DataRow foundCourseDao in dtCourse.Rows)
            {
                results.Add(daoObjectMapper.MapCourse(foundCourseDao));
            }

            foreach (Course result in results)
            {
                result.Subjects = Read_JctCourseSubject(result.Id, connection);
                result.Grades = Read_JctCourseGrade(result.Id, connection);
            }

            return results;
        }

        private List<Enumeration> Read_JctCourseSubject(int courseId, IConnectable connection)
        {
            List<Enumeration> foundSubjects = new List<Enumeration>();
            List<int> idsFound = new List<int>();

            IJctCourseSubjectDao jctCourseSubjectDaoImpl = DaoImplFactory.GetJctCourseSubjectDaoImpl();
            List<DataRow> foundJctCourseSubjectDaos = jctCourseSubjectDaoImpl.Read_AllWithCourseId(courseId, connection);
            foreach (DataRow foundJctCourseSubjectDao in foundJctCourseSubjectDaos)
            {
                idsFound.Add((int)foundJctCourseSubjectDao["enum_subject_id"]);
            }

            foreach (int idFound in idsFound)
            {
                foundSubjects.Add(Enum_Subject.GetEnumFromId(idFound));
            }

            return foundSubjects;
        }

        private List<Enumeration> Read_JctCourseGrade(int courseId, IConnectable connection)
        {
            List<Enumeration> foundGrades = new List<Enumeration>();
            List<int> idsFound = new List<int>();

            IJctCourseGradeDao jctCourseGradeDaoImpl = DaoImplFactory.GetJctCourseGradeDaoImpl();
            List<DataRow> foundJctCourseGradeDaos = jctCourseGradeDaoImpl.Read_AllWithCourseId(courseId, connection);
            foreach (DataRow foundJctCourseGradeDao in foundJctCourseGradeDaos)
            {
                idsFound.Add((int)foundJctCourseGradeDao["enum_grade_id"]);
            }

            foreach (int idFound in idsFound)
            {
                foundGrades.Add(Enum_Grade.GetEnumFromId(idFound));
            }

            return foundGrades;
        }

        public void Update(Course course, IConnectable connection)
        {
            List<Course> foundCourses = Read(course, connection);
            if (foundCourses.Count == 0)
                Write(course, connection);
            else if (foundCourses.Count == 1)
            {
                course.Id = foundCourses[0].Id;
                UpdateInternals(course, connection);
            }
            else
                throw new Exception(foundCourses.Count + " Courses found matching the description of:\n" +
                    course.ToString());
        }

        private void UpdateInternals(Course course, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapCourse(course);

            if (row["course_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE course SET ");
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("name = @name, ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }
                else
                    sql.Append("name = NULL, ");

                if (row["course_code"] != DBNull.Value)
                {
                    sql.Append("course_code = @course_code, ");
                    parameters.Add(new SqlParameter("@course_code", row["course_code"]));
                }
                else
                    sql.Append("course_code = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE course_id = @course_id");
                parameters.Add(new SqlParameter("@course_id", row["course_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);

                UpdateJctCourseSubject(course.Subjects, course.Id, connection);
                UpdateJctCourseGrade(course.Grades, course.Id, connection);
            }
            else
                throw new Exception("The following Course needs to be inserted into the database, before it can be updated.\n" + course.ToString());
        }

        public Course Write(Course course, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapCourse(course);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["name"] != DBNull.Value)
            {
                sqlValues.Append("@name, ");
                parameters.Add(new SqlParameter("@name", row["name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["course_code"] != DBNull.Value)
            {
                sqlValues.Append("@course_code, ");
                parameters.Add(new SqlParameter("@course_code", row["course_code"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql = "INSERT INTO course " +
                "(name, course_code, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["course_id"] = connection.InsertNewRecord(sql.ToString(), parameters);

            Course result = daoObjectMapper.MapCourse(row);

            result.Subjects = UpdateJctCourseSubject(course.Subjects, result.Id, connection);
            result.Grades = UpdateJctCourseGrade(course.Grades, result.Id, connection);

            return result;
        }

        private List<Enumeration> UpdateJctCourseSubject(List<Enumeration> subjects, int courseId, IConnectable connection)
        {
            List<Enumeration> updatedSubjects = new List<Enumeration>();
            IJctCourseSubjectDao jctCourseSubjectDaoImpl = DaoImplFactory.GetJctCourseSubjectDaoImpl();
            List<DataRow> foundJctCourseSubjectDaos =
                jctCourseSubjectDaoImpl.Read_AllWithCourseId(courseId, connection);
            List<int> subjectIds = new List<int>();
            List<int> foundSubjectIds = new List<int>();

            foreach (Enumeration subject in subjects)
            {
                subjectIds.Add(subject.Id);
            }

            foreach (DataRow jctCourseSubjectDao in foundJctCourseSubjectDaos)
            {
                foundSubjectIds.Add((int)jctCourseSubjectDao["enum_subject_id"]);
            }

            foreach (int subjectId in subjectIds)
            {
                if (foundSubjectIds.Contains(subjectId) == false)
                    jctCourseSubjectDaoImpl.Write(courseId, subjectId, connection);


                DataRow jctCourseSubject = jctCourseSubjectDaoImpl.Read(courseId, subjectId, connection);
                if(jctCourseSubject != null)
                {
                    int enumSubjectId = (int)jctCourseSubject["enum_subject_id"];
                    updatedSubjects.Add(Enum_Subject.GetEnumFromId(enumSubjectId));
                }
            }

            foreach (int foundSubjectId in foundSubjectIds)
            {
                if (subjectIds.Contains(foundSubjectId) == false)
                    jctCourseSubjectDaoImpl.Delete(courseId, foundSubjectId, connection);
            }

            return updatedSubjects;
        }

        private List<Enumeration> UpdateJctCourseGrade(List<Enumeration> grades, int courseId, IConnectable connection)
        {
            List<Enumeration> updatedGrades = new List<Enumeration>();
            IJctCourseGradeDao jctCourseGradeDaoImpl = DaoImplFactory.GetJctCourseGradeDaoImpl();
            List<DataRow> foundJctCourseGradeDaos =
                jctCourseGradeDaoImpl.Read_AllWithCourseId(courseId, connection);
            List<int> gradeIds = new List<int>();
            List<int> foundGradeIds = new List<int>();

            foreach (Enumeration grade in grades)
            {
                gradeIds.Add(grade.Id);
            }

            foreach (DataRow jctCourseGradeDao in foundJctCourseGradeDaos)
            {
                foundGradeIds.Add((int)jctCourseGradeDao["enum_grade_id"]);
            }

            foreach (int gradeId in gradeIds)
            {
                if (foundGradeIds.Contains(gradeId) == false)
                    jctCourseGradeDaoImpl.Write(courseId, gradeId, connection);

                DataRow jctCourseGrade = jctCourseGradeDaoImpl.Read(courseId, gradeId, connection);
                if(jctCourseGrade != null)
                {
                    int enumGradeId = (int)jctCourseGrade["enum_grade_id"];
                    updatedGrades.Add(Enum_Grade.GetEnumFromId(enumGradeId));
                }
            }

            foreach (int foundGradeId in foundGradeIds)
            {
                if (gradeIds.Contains(foundGradeId) == false)
                    jctCourseGradeDaoImpl.Delete(courseId, foundGradeId, connection);
            }

            return updatedGrades;
        }
    }
}
