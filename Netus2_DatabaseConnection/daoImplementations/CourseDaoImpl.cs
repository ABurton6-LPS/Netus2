using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class CourseDaoImpl : ICourseDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(Course course, IConnectable connection)
        {
            Delete_ClassEnrolled(course, connection);
            Delete_JctCourseSubject(course, connection);
            Delete_JctCourseGrade(course, connection);

            CourseDao courseDao = daoObjectMapper.MapCourse(course);

            StringBuilder sql = new StringBuilder("DELETE FROM course WHERE 1=1 ");
            sql.Append("AND course_id = " + courseDao.course_id + " ");
            sql.Append("AND name " + (courseDao.name != null ? "= '" + courseDao.name + "' " : "IS NULL "));
            sql.Append("AND course_code " + (courseDao.course_code != null ? "= '" + courseDao.course_code + "' " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_ClassEnrolled(Course course, IConnectable connection)
        {
            IClassEnrolledDao classEnrolledDaoImpl = new ClassEnrolledDaoImpl();
            List<ClassEnrolled> foundClassEnrolleds = classEnrolledDaoImpl.Read_UsingCourseId(course.Id, connection);
            foreach (ClassEnrolled classEnrolled in foundClassEnrolleds)
            {
                classEnrolledDaoImpl.Delete(classEnrolled, connection);
            }
        }

        private void Delete_JctCourseSubject(Course course, IConnectable connection)
        {
            IJctCourseSubjectDao jctCourseSubjectDaoImpl = new JctCourseSubjectDaoImpl();
            foreach (Enumeration subject in course.Subjects)
            {
                jctCourseSubjectDaoImpl.Delete(course.Id, subject.Id, connection);
            }
        }

        private void Delete_JctCourseGrade(Course course, IConnectable connection)
        {
            IJctCourseGradeDao jctCourseGradeDaoImpl = new JctCourseGradeDaoImpl();
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

            CourseDao courseDao = daoObjectMapper.MapCourse(course);

            sql.Append("SELECT * FROM course WHERE 1=1");
            if (courseDao.course_id != null)
                sql.Append("AND course_id = " + courseDao.course_id + " ");
            else
            {
                if (courseDao.name != null)
                    sql.Append("AND name = '" + courseDao.name + "' ");
                if (courseDao.course_code != null)
                    sql.Append("AND course_code = '" + courseDao.course_code + "' ");
            }

            return Read(sql.ToString(), connection);
        }

        private List<Course> Read(string sql, IConnectable connection)
        {
            List<CourseDao> foundCourseDaos = new List<CourseDao>();
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    CourseDao foundCourseDao = new CourseDao();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "course_id":
                                if (value != DBNull.Value)
                                    foundCourseDao.course_id = (int)value;
                                else
                                    foundCourseDao.course_id = null;
                                break;
                            case "name":
                                foundCourseDao.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case "course_code":
                                foundCourseDao.course_code = value != DBNull.Value ? (string)value : null;
                                break;
                            case "created":
                                if (value != DBNull.Value)
                                    foundCourseDao.created = (DateTime)value;
                                else
                                    foundCourseDao.created = null;
                                break;
                            case "created_by":
                                foundCourseDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case "changed":
                                if (value != DBNull.Value)
                                    foundCourseDao.changed = (DateTime)value;
                                else
                                    foundCourseDao.changed = null;
                                break;
                            case "changed_by":
                                foundCourseDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in course table: " + columnName);
                        }
                    }
                    foundCourseDaos.Add(foundCourseDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<Course> results = new List<Course>();
            foreach (CourseDao foundCourseDao in foundCourseDaos)
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

            IJctCourseSubjectDao jctCourseSubjectDaoImpl = new JctCourseSubjectDaoImpl();
            List<JctCourseSubjectDao> foundJctCourseSubjectDaos = jctCourseSubjectDaoImpl.Read(courseId, connection);
            foreach (JctCourseSubjectDao foundJctCourseSubjectDao in foundJctCourseSubjectDaos)
            {
                idsFound.Add((int)foundJctCourseSubjectDao.enum_subject_id);
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

            IJctCourseGradeDao jctCourseGradeDaoImpl = new JctCourseGradeDaoImpl();
            List<JctCourseGradeDao> foundJctCourseGradeDaos = jctCourseGradeDaoImpl.Read(courseId, connection);
            foreach (JctCourseGradeDao foundJctCourseGradeDao in foundJctCourseGradeDaos)
            {
                idsFound.Add((int)foundJctCourseGradeDao.enum_grade_id);
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
            CourseDao courseDao = daoObjectMapper.MapCourse(course);

            if (courseDao.course_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE course SET ");
                sql.Append("name = " + (courseDao.name != null ? "'" + courseDao.name + "', " : "NULL, "));
                sql.Append("course_code = " + (courseDao.course_code != null ? "'" + courseDao.course_code + "', " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE course_id = " + courseDao.course_id);

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
            CourseDao courseDao = daoObjectMapper.MapCourse(course);

            StringBuilder sql = new StringBuilder("INSERT INTO course (");
            sql.Append("name, course_code, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(courseDao.name != null ? "'" + courseDao.name + "', " : "NULL, ");
            sql.Append(courseDao.course_code != null ? "'" + courseDao.course_code + "', " : "NULL, ");
            sql.Append("GETDATE(), ");
            sql.Append("'Netus2')");

            courseDao.course_id = connection.InsertNewRecord(sql.ToString());

            Course result = daoObjectMapper.MapCourse(courseDao);

            result.Subjects = UpdateJctCourseSubject(course.Subjects, result.Id, connection);
            result.Grades = UpdateJctCourseGrade(course.Grades, result.Id, connection);

            return result;
        }

        private List<Enumeration> UpdateJctCourseSubject(List<Enumeration> subjects, int courseId, IConnectable connection)
        {
            List<Enumeration> updatedSubjects = new List<Enumeration>();
            IJctCourseSubjectDao jctCourseSubjectDaoImpl = new JctCourseSubjectDaoImpl();
            List<JctCourseSubjectDao> foundJctCourseSubjectDaos =
                jctCourseSubjectDaoImpl.Read(courseId, connection);
            List<int> subjectIds = new List<int>();
            List<int> foundSubjectIds = new List<int>();

            foreach (Enumeration subject in subjects)
            {
                subjectIds.Add(subject.Id);
            }

            foreach (JctCourseSubjectDao jctCourseSubjectDao in foundJctCourseSubjectDaos)
            {
                foundSubjectIds.Add((int)jctCourseSubjectDao.enum_subject_id);
            }

            foreach (int subjectId in subjectIds)
            {
                if (foundSubjectIds.Contains(subjectId) == false)
                    jctCourseSubjectDaoImpl.Write(courseId, subjectId, connection);

                int enumSubjectId = (int)jctCourseSubjectDaoImpl.Read(courseId, subjectId, connection).enum_subject_id;

                updatedSubjects.Add(Enum_Subject.GetEnumFromId(enumSubjectId));
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
            IJctCourseGradeDao jctCourseGradeDaoImpl = new JctCourseGradeDaoImpl();
            List<JctCourseGradeDao> foundJctCourseGradeDaos =
                jctCourseGradeDaoImpl.Read(courseId, connection);
            List<int> gradeIds = new List<int>();
            List<int> foundGradeIds = new List<int>();

            foreach (Enumeration grade in grades)
            {
                gradeIds.Add(grade.Id);
            }

            foreach (JctCourseGradeDao jctCourseGradeDao in foundJctCourseGradeDaos)
            {
                foundGradeIds.Add((int)jctCourseGradeDao.enum_grade_id);
            }

            foreach (int gradeId in gradeIds)
            {
                if (foundGradeIds.Contains(gradeId) == false)
                    jctCourseGradeDaoImpl.Write(courseId, gradeId, connection);

                int enumGradeId = (int)jctCourseGradeDaoImpl.Read(courseId, gradeId, connection).enum_grade_id;

                updatedGrades.Add(Enum_Grade.GetEnumFromId(enumGradeId));
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
