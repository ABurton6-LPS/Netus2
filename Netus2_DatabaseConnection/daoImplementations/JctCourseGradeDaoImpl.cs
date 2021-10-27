using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctCourseGradeDaoImpl : IJctCourseGradeDao
    {
        public void Delete(int courseId, int gradeId, IConnectable connection)
        {
            if (courseId <= 0 || gradeId < 0)
                throw new Exception("Cannot delete a record from jct_course_grade " +
                    "without a database-assigned ID for both courseId and gradeId." +
                    "\ncourseId: " + courseId +
                    "\ngradeId: " + gradeId);

            StringBuilder sql = new StringBuilder("DELETE FROM jct_course_grade WHERE 1=1 ");
            sql.Append("AND course_id = @course_id ");
            sql.Append("AND enum_grade_id = @enum_grade_id");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@course_id", courseId));
            parameters.Add(new SqlParameter("@enum_grade_id", gradeId));

            connection.ExecuteNonQuery(sql.ToString(), parameters);
        }

        public DataRow Read(int courseId, int gradeId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_course_grade WHERE 1=1 " +
            "AND course_id = @course_id " +
            "AND enum_grade_id = @enum_grade_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@course_id", courseId));
            parameters.Add(new SqlParameter("@enum_grade_id", gradeId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching courseId: " + courseId + " and gradeId: " + gradeId);
        }

        public List<DataRow> Read_AllWithCourseId(int courseId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_course_grade WHERE " +
            "course_id = @course_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@course_id", courseId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllWithGradeId(int gradeId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_course_grade WHERE " +
            "enum_grade_id = @enum_grade_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enum_grade_id", gradeId));

            return Read(sql, connection, parameters);
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctCourseGrade = DataTableFactory.CreateDataTable_Netus2_JctCourseGrade();
            dtJctCourseGrade = connection.ReadIntoDataTable(sql, dtJctCourseGrade, parameters);

            List<DataRow> jctCourseGradeDaos = new List<DataRow>();
            foreach(DataRow row in dtJctCourseGrade.Rows)
                jctCourseGradeDaos.Add(row);

            return jctCourseGradeDaos;
        }

        public DataRow Write(int courseId, int gradeId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_course_grade (" +
                "course_id, enum_grade_id) VALUES (" +
                "@course_id, @enum_grade_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@course_id", courseId));
            parameters.Add(new SqlParameter("@enum_grade_id", gradeId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctCourseGradeDao = DataTableFactory.CreateDataTable_Netus2_JctCourseGrade().NewRow();
            jctCourseGradeDao["course_id"] = courseId;
            jctCourseGradeDao["enum_grade_id"] = gradeId;

            return jctCourseGradeDao;
        }
    }
}
