using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
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

        public DataRow Read(int courseId, int gradeId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_course_grade WHERE 1=1 ");
            sql.Append("AND course_id = " + courseId + " ");
            sql.Append("AND enum_grade_id = " + gradeId);

            List<DataRow> results = Read(sql.ToString(), connection);
            if (results.Count == 1)
                return results[0];
            else if (results.Count == 0)
                return null;
            else
                throw new Exception("The jct_course_grade table contains a duplicate record.\n" +
                    "course_id = " + courseId + ", grade_id = " + gradeId);
        }

        public List<DataRow> Read(int courseId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_course_grade WHERE 1=1 ");
            sql.Append("AND course_id = " + courseId);

            return Read(sql.ToString(), connection);
        }

        private List<DataRow> Read(string sql, IConnectable connection)
        {
            DataTable dtJctCourseGrade = new DataTableFactory().Dt_Netus2_JctCourseGrade;
            dtJctCourseGrade = connection.ReadIntoDataTable(sql, dtJctCourseGrade).Result;

            List<DataRow> jctCourseGradeDaos = new List<DataRow>();
            foreach(DataRow row in dtJctCourseGrade.Rows)
            {
                jctCourseGradeDaos.Add(row);
            }

            return jctCourseGradeDaos;
        }

        public DataRow Write(int courseId, int gradeId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_course_grade (course_id, enum_grade_id) VALUES (");
            sql.Append(courseId + ", ");
            sql.Append(gradeId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            DataRow jctCourseGradeDao = new DataTableFactory().Dt_Netus2_JctCourseGrade.NewRow();
            jctCourseGradeDao["course_id"] = courseId;
            jctCourseGradeDao["enum_grade_id"] = gradeId;

            return jctCourseGradeDao;
        }
    }
}
