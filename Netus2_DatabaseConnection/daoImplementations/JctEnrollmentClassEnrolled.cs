using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctEnrollmentClassEnrolledDaoImpl : IJctEnrollmentClassEnrolledDao
    {
        public void Delete(int enrollmentId, int classEnrolledId, IConnectable connection)
        {
            if (enrollmentId <= 0 || classEnrolledId <= 0)
                throw new Exception("Cannot delete a record from jct_enrollment_class_enrolled " +
                    "without a database-assigned ID for both enrollmentId and classEnrolledId." +
                    "\nenrollmentId: " + enrollmentId +
                    "\nclassEnrolledId: " + classEnrolledId);

            string sql = "DELETE FROM jct_enrollment_class_enrolled WHERE 1=1 " +
            "AND enrollment_id = @enrollment_id " +
            "AND class_enrolled_id = @class_enrolled_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enrollment_id", enrollmentId));
            parameters.Add(new SqlParameter("@class_enrolled_id", classEnrolledId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int enrollmentId, int classEnrolledId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_enrollment_class_enrolled WHERE 1=1 " +
            "AND enrollment_id = @enrollment_id " +
            "AND class_enrolled_id = @class_enrolled_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enrollment_id", enrollmentId));
            parameters.Add(new SqlParameter("@class_enrolled_id", classEnrolledId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching enrollmentId: " + enrollmentId + " and classEnrolledId: " + classEnrolledId);
        }

        public List<DataRow> Read_AllWithEnrollmentId(int enrollmentId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_enrollment_class_enrolled WHERE " +
                "enrollment_id = @enrollment_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enrollment_id", enrollmentId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllWithClassEnrolledId(int classEnrolledId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_enrollment_class_enrolled WHERE " +
                "class_enrolled_id = @class_enrolled_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_enrolled_id", classEnrolledId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllClassEnrolledIsNotInTempTable(IConnectable connection)
        {
            string sql =
                "SELECT jece.enrollment_id, jece.class_enrolled_id " +
                "FROM jct_enrollemnt_class_enrolled jece " +
                "WHERE jece.class_enrolled_id NOT IN ( " +
                "SELECT tjece.class_enrolled_id " +
                "FROM temp_jct_enrollment_class_enrolled tjece " +
                "WHERE tjece.enrollment_id = jece.enrollment_id)";

            return Read(sql, connection, new List<SqlParameter>());
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctEnrollmentClassEnrolled = DataTableFactory.CreateDataTable_Netus2_JctEnrollmentClassEnrolled();
            dtJctEnrollmentClassEnrolled = connection.ReadIntoDataTable(sql, dtJctEnrollmentClassEnrolled, parameters);
            
            List<DataRow> jctEnrollmentClassEnrolledDaos = new List<DataRow>();
            foreach (DataRow row in dtJctEnrollmentClassEnrolled.Rows)
                jctEnrollmentClassEnrolledDaos.Add(row);

            return jctEnrollmentClassEnrolledDaos;
        }

        public void Update(int enrollmentId, int classEnrolledId, DateTime? startDate, DateTime? endDate, IConnectable connection)
        {
            DataRow foundJctEnrollmentClassEnrolledDao = Read(enrollmentId, classEnrolledId, connection);

            if (foundJctEnrollmentClassEnrolledDao == null)
                Write(enrollmentId, classEnrolledId, startDate, endDate, connection);
            else
                UpdateInternals(enrollmentId, classEnrolledId, startDate, endDate, connection);
        }

        private void UpdateInternals(int enrollmentId, int classEnrolledId, DateTime? startDate, DateTime? endDate, IConnectable connection)
        {
            string sql = "UPDATE jct_enrollment_class_enrolled SET " +
                "enrollment_start_date = @enrollment_start_date, " +
                "enrollment_end_date = @enrollment_end_date " +
                "WHERE enrollment_id = @enrollment_id " +
                "AND class_enrolled_id = @class_enrolled_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if(startDate != null)
                parameters.Add(new SqlParameter("@enrollment_start_date", startDate));
            else
                parameters.Add(new SqlParameter("@enrollment_start_date", DBNull.Value));
            if(endDate != null)
                parameters.Add(new SqlParameter("@enrollment_end_date", endDate));
            else
                parameters.Add(new SqlParameter("@enrollment_end_date", DBNull.Value));
            parameters.Add(new SqlParameter("@enrollment_id", enrollmentId));
            parameters.Add(new SqlParameter("@class_enrolled_id", classEnrolledId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Write(int enrollmentId, int classEnrolledId, DateTime? startDate, DateTime? endDate, IConnectable connection)
        {
            string sql = "INSERT INTO jct_enrollment_class_enrolled (" +
                "enrollment_id, class_enrolled_id, enrollment_start_date, enrollment_end_date) VALUES (" +
                "@enrollment_id, @class_enrolled_id, @enrollment_start_date, @enrollment_end_date)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enrollment_id", enrollmentId));
            parameters.Add(new SqlParameter("@class_enrolled_id", classEnrolledId));
            if (startDate != null)
                parameters.Add(new SqlParameter("@enrollment_start_date", startDate));
            else
                parameters.Add(new SqlParameter("@enrollment_start_date", DBNull.Value));
            if (endDate != null)
                parameters.Add(new SqlParameter("@enrollment_end_date", endDate));
            else
                parameters.Add(new SqlParameter("@enrollment_end_date", DBNull.Value));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctEnrollmentClassEnrolledDao = DataTableFactory.CreateDataTable_Netus2_JctEnrollmentClassEnrolled().NewRow();
            jctEnrollmentClassEnrolledDao["enrollment_id"] = enrollmentId;
            jctEnrollmentClassEnrolledDao["class_enrolled_id"] = classEnrolledId;
            if(startDate != null)
                jctEnrollmentClassEnrolledDao["enrollment_start_date"] = startDate;
            else
                jctEnrollmentClassEnrolledDao["enrollment_start_date"] = DBNull.Value;
            if(endDate != null)
                jctEnrollmentClassEnrolledDao["enrollment_end_date"] = endDate;
            else
                jctEnrollmentClassEnrolledDao["enrollment_end_date"] = DBNull.Value;

            return jctEnrollmentClassEnrolledDao;
        }

        public void Write_ToTempTable(int enrollmentId, int class_enrolled_id, IConnectable connection)
        {
            string sql = "INSERT INTO temp_jct_enrollment_class_enrolled (" +
                "enrollment_id, class_enrolled_id) VALUES (" +
                "@enrollment_id, @class_enrolled_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enrollment_id", enrollmentId));
            parameters.Add(new SqlParameter("@class_enrolled_id", class_enrolled_id));

            connection.ExecuteNonQuery(sql, parameters);
        }
    }
}
