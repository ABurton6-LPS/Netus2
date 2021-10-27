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
    public class JctEnrollmentAcademicSessionDaoImpl : IJctEnrollmentAcademicSessionDao
    {
        public void Delete(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            if (enrollmentId <= 0 || academicSessionId <= 0)
                throw new Exception("Cannot delete a record from jct_enrollment_academic_session " +
                    "without a database-assigned ID for both enrollmentId and academicSessionId." +
                    "\nenrollmentId: " + enrollmentId +
                    "\nacademicSessionId: " + academicSessionId);

            string sql = "DELETE FROM jct_enrollment_academic_session WHERE 1=1 " +
            "AND enrollment_id = @enrollment_id " +
            "AND academic_session_id = @academic_session_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enrollment_id", enrollmentId));
            parameters.Add(new SqlParameter("@academic_session_id", academicSessionId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_enrollment_academic_session WHERE 1=1 " +
            "AND enrollment_id = @enrollment_id " +
            "AND academic_session_id = @academic_session_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enrollment_id", enrollmentId));
            parameters.Add(new SqlParameter("@academic_session_id", academicSessionId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public List<DataRow> Read_AllWithEnrollmentId(int enrollmentId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_enrollment_academic_session WHERE " +
                "enrollment_id = @enrollment_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enrollment_id", enrollmentId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllWithAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_enrollment_academic_session WHERE " +
                "academic_session_id = @academic_session_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@academic_session_id", academicSessionId));

            return Read(sql, connection, parameters);
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctEnrollmentAcademicSession = DataTableFactory.CreateDataTable_Netus2_JctEnrollmentAcademicSession();
            dtJctEnrollmentAcademicSession = connection.ReadIntoDataTable(sql, dtJctEnrollmentAcademicSession, parameters);

            List<DataRow> jctEnrollmentAcademicSessionDaos = new List<DataRow>();
            foreach (DataRow row in dtJctEnrollmentAcademicSession.Rows)
                jctEnrollmentAcademicSessionDaos.Add(row);

            return jctEnrollmentAcademicSessionDaos;
        }

        public DataRow Write(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_enrollment_academic_session (" +
                "enrollment_id, academic_session_id) VALUES (" +
                "@enrollment_id, @academic_session_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enrollment_id", enrollmentId));
            parameters.Add(new SqlParameter("@academic_session_id", academicSessionId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctEnrollmentAcademicSessionDao = DataTableFactory.CreateDataTable_Netus2_JctEnrollmentAcademicSession().NewRow();
            jctEnrollmentAcademicSessionDao["enrollment_id"] = enrollmentId;
            jctEnrollmentAcademicSessionDao["academic_session_id"] = academicSessionId;

            return jctEnrollmentAcademicSessionDao;
        }
    }
}
