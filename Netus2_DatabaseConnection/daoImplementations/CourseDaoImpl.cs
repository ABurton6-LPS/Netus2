using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
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
            Delete_ClassEnrolled(course, connection);
            Delete_JctCourseSubject(course, connection);
            Delete_JctCourseGrade(course, connection);

            DataRow row = daoObjectMapper.MapCourse(course);

            StringBuilder sql = new StringBuilder("DELETE FROM course WHERE 1=1 ");
            sql.Append("AND course_id = " + row["course_id"] + " ");
            sql.Append("AND name " + (row["name"] != DBNull.Value ? "= '" + row["name"] + "' " : "IS NULL "));
            sql.Append("AND course_code " + (row["course_code"] != DBNull.Value ? "= '" + row["course_code"] + "' " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
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

        public Course Read(int courseId, IConnectable connection)
        {
            string sql = "SELECT * FROM course WHERE course_id = " + courseId;
            List<Course> results = Read(sql, connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
        }

        public List<Course> Read(Course course, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            DataRow row = daoObjectMapper.MapCourse(course);

            sql.Append("SELECT * FROM course WHERE 1=1 ");
            if (row["course_id"] != DBNull.Value)
                sql.Append("AND course_id = " + row["course_id"] + " ");
            else
            {
                if (row["name"] != DBNull.Value)
                    sql.Append("AND name = '" + row["name"] + "' ");
                if (row["course_code"] != DBNull.Value)
                    sql.Append("AND course_code = '" + row["course_code"] + "' ");
            }

            return Read(sql.ToString(), connection);
        }

        private List<Course> Read(string sql, IConnectable connection)
        {
            DataTable dtCourse = new DataTableFactory().Dt_Netus2_Course;
            dtCourse = connection.ReadIntoDataTable(sql, dtCourse);

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
            List<DataRow> foundJctCourseSubjectDaos = jctCourseSubjectDaoImpl.Read(courseId, connection);
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
            List<DataRow> foundJctCourseGradeDaos = jctCourseGradeDaoImpl.Read(courseId, connection);
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
                StringBuilder sql = new StringBuilder("UPDATE course SET ");
                sql.Append("name = " + (row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, "));
                sql.Append("course_code = " + (row["course_code"] != DBNull.Value ? "'" + row["course_code"] + "', " : "NULL, "));
                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE course_id = " + row["course_id"]);

                connection.ExecuteNonQuery(sql.ToString());

                UpdateJctCourseSubject(course.Subjects, course.Id, connection);
                UpdateJctCourseGrade(course.Grades, course.Id, connection);
            }
            else
            {
                throw new Exception("The following Course needs to be inserted into the database, before it can be updated.\n" + course.ToString());
            }
        }

        public Course Write(Course course, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapCourse(course);

            StringBuilder sql = new StringBuilder("INSERT INTO course (");
            sql.Append("name, course_code, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, ");
            sql.Append(row["course_code"] != DBNull.Value ? "'" + row["course_code"] + "', " : "NULL, ");
            sql.Append("dbo.CURRENT_DATETIME(), ");
            sql.Append((_taskId != null ? _taskId.ToString() : "'Netus2'") + ")");

            row["course_id"] = connection.InsertNewRecord(sql.ToString());

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
                jctCourseSubjectDaoImpl.Read(courseId, connection);
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
                jctCourseGradeDaoImpl.Read(courseId, connection);
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
