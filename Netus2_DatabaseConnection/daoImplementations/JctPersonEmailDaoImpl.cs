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
    public class JctPersonEmailDaoImpl : IJctPersonEmailDao
    {
        public void Delete(int personId, int emailId, IConnectable connection)
        {
            if (personId <= 0 || emailId <= 0)
                throw new Exception("Cannot delete a record from jct_person_email " +
                    "without a database-assigned ID for both personId and emailId." +
                    "\npersonId: " + personId +
                    "\nemailId: " + emailId);

            string sql = "DELETE FROM jct_person_email WHERE 1=1 " +
            "AND person_id = @person_id " +
            "AND email_id = @email_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@email_id", emailId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int personId, int emailId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_email WHERE 1=1 " +
            "AND person_id = @person_id " +
            "AND email_id = @email_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@email_id", emailId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching personId: " + personId + " and emailId: " + emailId);
        }

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_email WHERE " +
                "person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_WithAllEmailId(int emailId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_email WHERE " +
                "email_id = @email_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@email_id", emailId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllEmailIsNotInTempTable(IConnectable connection)
        {
            string sql =
                "SELECT jpe.person_id, jpe.email_id " +
                "FROM jct_person_email jpe " +
                "WHERE jpe.email_id NOT IN ( " +
                "SELECT tjpe.email_id " +
                "FROM temp_jct_person_email tjpe " +
                "WHERE tjpe.person_id = jpe.person_id )";

            return Read(sql, connection, new List<SqlParameter>());
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctPersonEmail = DataTableFactory.CreateDataTable_Netus2_JctPersonEmail();
            dtJctPersonEmail = connection.ReadIntoDataTable(sql, dtJctPersonEmail, parameters);

            List<DataRow> jctPersonEmailDaos = new List<DataRow>();
            foreach (DataRow row in dtJctPersonEmail.Rows)
                jctPersonEmailDaos.Add(row);

            return jctPersonEmailDaos;
        }

        public DataRow Write(int personId, int emailId, int isPrimaryId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_person_email (" +
                "person_id, email_id, is_primary_id) VALUES (" +
                "@person_id, @email_id, @is_primary_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@email_id", emailId));
            parameters.Add(new SqlParameter("@is_primary_id", isPrimaryId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctPersonEmailDao = DataTableFactory.CreateDataTable_Netus2_JctPersonEmail().NewRow();
            jctPersonEmailDao["person_id"] = personId;
            jctPersonEmailDao["email_id"] = emailId;
            jctPersonEmailDao["is_primary_id"] = isPrimaryId;

            return jctPersonEmailDao;
        }

        public void Write_ToTempTable(int personId, int emailId, IConnectable connection)
        {
            string sql = "INSERT INTO temp_jct_person_email (" +
                "person_id, email_id) VALUES (" +
                "@person_id, @email_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@email_id", emailId));

            connection.ExecuteNonQuery(sql, parameters);
        }
    }
}
