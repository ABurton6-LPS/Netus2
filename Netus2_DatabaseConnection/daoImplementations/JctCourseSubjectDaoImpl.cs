using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctCourseSubjectDaoImpl : IJctCourseSubjectDao
    {
        public void Delete(int courseId, int subjectId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM jct_course_subject WHERE 1=1 ");
            sql.Append("AND course_id = " + courseId + " ");
            sql.Append("AND enum_subject_id = " + subjectId);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public JctCourseSubjectDao Read(int courseId, int subjectId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_course_subject WHERE 1=1 ");
            sql.Append("AND course_id = " + courseId + " ");
            sql.Append("AND enum_subject_id = " + subjectId);

            List<JctCourseSubjectDao> results = Read(sql.ToString(), connection);
            if (results.Count == 1)
                return results[0];
            else if (results.Count == 0)
                return null;
            else
                throw new Exception("The jct_course_subject table contains a duplicate record.\n" +
                    "course_id = " + courseId + ", subject_id = " + subjectId);
        }

        public List<JctCourseSubjectDao> Read(int courseId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_course_subject WHERE 1=1 ");
            sql.Append("AND course_id = " + courseId);

            return Read(sql.ToString(), connection);
        }

        private List<JctCourseSubjectDao> Read(string sql, IConnectable connection)
        {
            List<JctCourseSubjectDao> jctCourseSubjectDaos = new List<JctCourseSubjectDao>();
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql.ToString());
                while (reader.Read())
                {
                    JctCourseSubjectDao foundJctCourseSubjectDao = new JctCourseSubjectDao();
                    foundJctCourseSubjectDao.course_id = reader.GetInt32(0);
                    foundJctCourseSubjectDao.enum_subject_id = reader.GetInt32(1);
                    jctCourseSubjectDaos.Add(foundJctCourseSubjectDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return jctCourseSubjectDaos;
        }

        public JctCourseSubjectDao Write(int courseId, int subjectId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_course_subject (course_id, enum_subject_id) VALUES (");
            sql.Append(courseId + ", ");
            sql.Append(subjectId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            JctCourseSubjectDao jctCourseSubjectDao = new JctCourseSubjectDao();
            jctCourseSubjectDao.course_id = courseId;
            jctCourseSubjectDao.enum_subject_id = subjectId;

            return jctCourseSubjectDao;
        }
    }
}
