using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctEnrollmentAcademicSessionDaoImpl : IJctEnrollmentAcademicSessionDao
    {
        public void Delete(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM jct_enrollment_academic_session WHERE 1=1 ");
            sql.Append("AND enrollment_id = " + enrollmentId + " ");
            sql.Append("AND academic_session_id = " + academicSessionId);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public DataRow Read(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_enrollment_academic_session WHERE 1=1 ");
            sql.Append("AND enrollment_id = " + enrollmentId + " ");
            sql.Append("AND academic_session_id = " + academicSessionId);

            List<DataRow> results = Read(sql.ToString(), connection);
            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception("The jct_enrollment_academic_session table contains a duplicate record.\n" +
                    "enrollment_id = " + enrollmentId + ", address_Id = " + academicSessionId);
        }

        public List<DataRow> Read_WithEnrollmentId(int enrollmentId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_enrollment_academic_session where enrollment_id = " + enrollmentId;

            return Read(sql, connection);
        }
        public List<DataRow> Read_WithAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_enrollment_academic_session where academic_session_id = " + academicSessionId;

            return Read(sql, connection);
        }

        private List<DataRow> Read(string sql, IConnectable connection)
        {
            List<DataRow> jctEnrollmentAcademicSessionDaos = new List<DataRow>();
            DataTable dtJctEnrollmentAcademicSession = new DataTableFactory().Dt_Netus2_JctEnrollmentAcademicSession;

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtJctEnrollmentAcademicSession.Load(reader);

                foreach(DataRow row in dtJctEnrollmentAcademicSession.Rows)
                {
                    jctEnrollmentAcademicSessionDaos.Add(row);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return jctEnrollmentAcademicSessionDaos;
        }

        public DataRow Write(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_enrollment_academic_session (enrollment_id, academic_session_id) VALUES (");
            sql.Append(enrollmentId + ", ");
            sql.Append(academicSessionId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            DataRow jctEnrollmentAcademicSessionDao = new DataTableFactory().Dt_Netus2_JctEnrollmentAcademicSession.NewRow();
            jctEnrollmentAcademicSessionDao["enrollment_id"] = enrollmentId;
            jctEnrollmentAcademicSessionDao["academic_session_id"] = academicSessionId;

            return jctEnrollmentAcademicSessionDao;
        }
    }
}
