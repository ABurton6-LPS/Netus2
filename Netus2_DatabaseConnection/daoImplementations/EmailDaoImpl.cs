using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class EmailDaoImpl : IEmailDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();
        public int? _taskId = null;

        public void SetTaskId(int taskId)
        {
            _taskId = taskId;
        }

        public int? GetTaskId()
        {
            return _taskId;
        }

        public void Delete(Email email, IConnectable connection)
        {
            if(email.Id <= 0)
                throw new Exception("Cannot delete an email which doesn't have a database-assigned ID.\n" + email.ToString());

            Delete_JctPersonEmail(email.Id, connection);

            string sql = "DELETE FROM email WHERE " +
                "email_id = @email_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@email_id", email.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void Delete_JctPersonEmail(int emailId, IConnectable connection)
        {
            IJctPersonEmailDao jctPersonEmailDaoImpl = DaoImplFactory.GetJctPersonEmailDaoImpl();
            List<DataRow> foundDataRows =
                jctPersonEmailDaoImpl.Read_WithAllEmailId(emailId, connection);

            foreach (DataRow foundDataRow in foundDataRows)
            {
                int personId = (int)foundDataRow["person_id"];
                jctPersonEmailDaoImpl.Delete(personId, emailId, connection);
            }
        }

        public Email Read_UsingEmailId(int emailId, IConnectable connection)
        {
            string sql = "SELECT * FROM email WHERE email_id = @email_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@email_id", emailId));

            List<Email> results = Read(sql, connection, parameters);
            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching emailId: " + emailId);
        }

        public List<Email> Read(Email email, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEmail(email);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM email WHERE 1=1 ");
            if (row["email_id"] != DBNull.Value)
            {
                sql.Append("AND email_id = @email_id");
                parameters.Add(new SqlParameter("@email_id", row["email_id"]));
            }                
            else
            {
                if (row["email_value"] != DBNull.Value)
                {
                    sql.Append("AND emaemail_valueil = @email_value ");
                    parameters.Add(new SqlParameter("@email_value", row["email_value"]));
                }

                if (row["enum_email_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_email_id = @enum_email_id ");
                    parameters.Add(new SqlParameter("@enum_email_id", row["enum_email_id"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<Email> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtEmail = DataTableFactory.CreateDataTable_Netus2_Email();
            dtEmail = connection.ReadIntoDataTable(sql, dtEmail, parameters);

            List<Email> results = new List<Email>();
            foreach (DataRow row in dtEmail.Rows)
                results.Add(daoObjectMapper.MapEmail(row));

            return results;
        }

        public void Update(Email email, IConnectable connection)
        {
            List<Email> foundEmails = Read(email, connection);
            if (foundEmails.Count == 0)
                Write(email, connection);
            else if (foundEmails.Count == 1)
            {
                email.Id = foundEmails[0].Id;
                UpdateInternals(email, connection);
            }
            else
                throw new Exception(foundEmails.Count + " Emails found matching the description of:\n" +
                    email.ToString());
        }

        private void UpdateInternals(Email email, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEmail(email);

            if(row["email_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE email SET ");
                if (row["email_value"] != DBNull.Value)
                {
                    sql.Append("email_value = @email_value, ");
                    parameters.Add(new SqlParameter("@email_value", row["email_value"]));
                }
                else
                    sql.Append("email_value = NULL, ");

                if (row["enum_email_id"] != DBNull.Value)
                {
                    sql.Append("enum_email_id = @enum_email_id, ");
                    parameters.Add(new SqlParameter("@enum_email_id", row["enum_email_id"]));
                }
                else
                    sql.Append("enum_email_id = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE email_id = @email_id");
                parameters.Add(new SqlParameter("@email_id", row["email_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);
            }
            else
                throw new Exception("The following Email needs to be inserted into the database, before it can be updated.\n" + email.ToString());
        }

        public Email Write(Email email, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEmail(email);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["email_value"] != DBNull.Value)
            {
                sqlValues.Append("@email_value, ");
                parameters.Add(new SqlParameter("@email_value", row["email_value"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_email_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_email_id, ");
                parameters.Add(new SqlParameter("@enum_email_id", row["enum_email_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql = "INSERT INTO email " +
                "(email_value, enum_email_id, created, created_by) VALUES (" + sqlValues.ToString() + ")";

            row["email_id"] = connection.InsertNewRecord(sql, parameters);

            Email result = daoObjectMapper.MapEmail(row);

            return result;
        }
    }
}
