using Netus2.daoInterfaces;
using Netus2.daoObjects;
using Netus2.dbAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Netus2.daoImplementations
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

        public JctEnrollmentAcademicSessionDao Read(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_enrollment_academic_session WHERE 1=1 ");
            sql.Append("AND enrollment_id = " + enrollmentId + " ");
            sql.Append("AND academic_session_id = " + academicSessionId);

            List<JctEnrollmentAcademicSessionDao> results = Read(sql.ToString(), connection);
            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception("The jct_enrollment_academic_session table contains a duplicate record.\n" +
                    "enrollment_id = " + enrollmentId + ", address_Id = " + academicSessionId);
        }

        public List<JctEnrollmentAcademicSessionDao> Read_WithEnrollmentId(int enrollmentId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_enrollment_academic_session where enrollment_id = " + enrollmentId;

            return Read(sql, connection);
        }
        public List<JctEnrollmentAcademicSessionDao> Read_WithAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_enrollment_academic_session where academic_session_id = " + academicSessionId;

            return Read(sql, connection);
        }

        private List<JctEnrollmentAcademicSessionDao> Read(string sql, IConnectable connection)
        {
            List<JctEnrollmentAcademicSessionDao> jctEnrollmentAcademicSessionDaos = new List<JctEnrollmentAcademicSessionDao>();

            SqlDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    JctEnrollmentAcademicSessionDao foundJctEnrollmentAcademicSessionDao = new JctEnrollmentAcademicSessionDao();
                    foundJctEnrollmentAcademicSessionDao.enrollment_id = reader.GetInt32(0);
                    foundJctEnrollmentAcademicSessionDao.academic_session_id = reader.GetInt32(1);
                    jctEnrollmentAcademicSessionDaos.Add(foundJctEnrollmentAcademicSessionDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return jctEnrollmentAcademicSessionDaos;
        }

        public JctEnrollmentAcademicSessionDao Write(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_enrollment_academic_session (enrollment_id, academic_session_id) VALUES (");
            sql.Append(enrollmentId + ", ");
            sql.Append(academicSessionId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            JctEnrollmentAcademicSessionDao jctEnrollmentAcademicSessionDao = new JctEnrollmentAcademicSessionDao();
            jctEnrollmentAcademicSessionDao.enrollment_id = enrollmentId;
            jctEnrollmentAcademicSessionDao.academic_session_id = academicSessionId;

            return jctEnrollmentAcademicSessionDao;
        }
    }
}
