using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctCourseGradeDaoImpl : IJctCourseGradeDao
    {
        public void Delete(int courseId, int gradeId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM jct_course_grade WHERE 1=1 ");
            sql.Append("AND course_id = " + courseId + " ");
            sql.Append("AND enum_grade_id = " + gradeId);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public JctCourseGradeDao Read(int courseId, int gradeId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_course_grade WHERE 1=1 ");
            sql.Append("AND course_id = " + courseId + " ");
            sql.Append("AND enum_grade_id = " + gradeId);

            List<JctCourseGradeDao> results = Read(sql.ToString(), connection);
            if (results.Count == 1)
                return results[0];
            else if (results.Count == 0)
                return null;
            else
                throw new Exception("The jct_course_grade table contains a duplicate record.\n" +
                    "course_id = " + courseId + ", grade_id = " + gradeId);
        }

        public List<JctCourseGradeDao> Read(int courseId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_course_grade WHERE 1=1 ");
            sql.Append("AND course_id = " + courseId);

            return Read(sql.ToString(), connection);
        }

        private List<JctCourseGradeDao> Read(string sql, IConnectable connection)
        {
            List<JctCourseGradeDao> jctCourseGradeDaos = new List<JctCourseGradeDao>();
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql.ToString());
                while (reader.Read())
                {
                    JctCourseGradeDao foundJctCourseGradeDao = new JctCourseGradeDao();
                    foundJctCourseGradeDao.course_id = reader.GetInt32(0);
                    foundJctCourseGradeDao.enum_grade_id = reader.GetInt32(1);
                    jctCourseGradeDaos.Add(foundJctCourseGradeDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return jctCourseGradeDaos;
        }

        public JctCourseGradeDao Write(int courseId, int gradeId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_course_grade (course_id, enum_grade_id) VALUES (");
            sql.Append(courseId + ", ");
            sql.Append(gradeId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            JctCourseGradeDao jctCourseGradeDao = new JctCourseGradeDao();
            jctCourseGradeDao.course_id = courseId;
            jctCourseGradeDao.enum_grade_id = gradeId;

            return jctCourseGradeDao;
        }
    }
}
