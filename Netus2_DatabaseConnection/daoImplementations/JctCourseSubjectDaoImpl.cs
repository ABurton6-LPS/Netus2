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
    public class JctCourseSubjectDaoImpl : IJctCourseSubjectDao
    {
        public void Delete(int courseId, int subjectId, IConnectable connection)
        {
            if (courseId <= 0 || subjectId < 0)
                throw new Exception("Cannot delete a record from jct_course_subject " +
                    "without a database-assigned ID for both courseId and subjectId." +
                    "\ncourseId: " + courseId +
                    "\nsubjectId: " + subjectId);

            string sql = "DELETE FROM jct_course_subject WHERE 1=1 " +
            "AND course_id = @course_id " +
            "AND enum_subject_id = @enum_subject_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@course_id", courseId));
            parameters.Add(new SqlParameter("@enum_subject_id", subjectId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int courseId, int subjectId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_course_subject WHERE 1=1 " +
            "AND course_id = @course_id " +
            "AND enum_subject_id = @enum_subject_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@course_id", courseId));
            parameters.Add(new SqlParameter("@enum_subject_id", subjectId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public List<DataRow> Read_AllWithCourseId(int courseId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_course_subject WHERE " +
                "course_id = @course_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@course_id", courseId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllWithSubjectId(int subjectId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_course_subject WHERE " +
                "enum_subject_id = @enum_subject_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enum_subject_id", subjectId));

            return Read(sql, connection, parameters);
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctCourseSubject = DataTableFactory.CreateDataTable_Netus2_JctCourseSubject();
            dtJctCourseSubject = connection.ReadIntoDataTable(sql, dtJctCourseSubject, parameters);

            List<DataRow> jctCourseSubjectDaos = new List<DataRow>();
            foreach (DataRow row in dtJctCourseSubject.Rows)
                jctCourseSubjectDaos.Add(row);

            return jctCourseSubjectDaos;
        }

        public DataRow Write(int courseId, int subjectId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_course_subject (" +
                "course_id, enum_subject_id) VALUES (" +
                "@course_id, @enum_subject_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@course_id", courseId));
            parameters.Add(new SqlParameter("@enum_subject_id", subjectId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctCourseSubjectDao = DataTableFactory.CreateDataTable_Netus2_JctCourseSubject().NewRow();
            jctCourseSubjectDao["course_id"] = courseId;
            jctCourseSubjectDao["enum_subject_id"] = subjectId;

            return jctCourseSubjectDao;
        }
    }
}
