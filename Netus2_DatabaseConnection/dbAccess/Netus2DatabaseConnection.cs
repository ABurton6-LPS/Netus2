using Netus2.dbAccess;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Netus2
{
    public class Netus2DatabaseConnection : IConnectable
    {
        //Local database connection
        string connectionString = @"Data Source=ITDSL0995104653;Initial Catalog=Netus2;Integrated Security=SSPI";

        //Cloud database connection
        //string connectionString = @"Data Source=tcp:janusdb.database.windows.net,1433;Initial Catalog=Netus2;Uid=janus;Pwd=AqIiA59@$J0K";

        private SqlConnection connection;
        private SqlTransaction transaction = null;

        public Netus2DatabaseConnection()
        {
            connection = new SqlConnection(connectionString);
        }

        public ConnectionState GetState()
        {
            return connection.State;
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (transaction != null)
            {
                transaction.Commit();
            }
        }

        public void RollbackTransaction()
        {
            if (transaction != null)
            {
                transaction.Rollback();
            }
        }

        public SqlDataReader GetReader(string sql)
        {
            if ((sql == null) || (sql == "") || !(sql.ToUpper().Substring(0, 6).Contains("SELECT")))
            {
                throw new Exception("SQL must begin with 'SELECT'.");
            }

            SqlCommand command = null;

            if (transaction != null)
            {
                command = new SqlCommand(sql, connection, transaction);
            }
            else
            {
                command = new SqlCommand(sql, connection);
            }

            return command.ExecuteReader();
        }

        public int ExecuteNonQuery(string sql)
        {
            if ((sql == null) || (sql == "") || (sql.ToUpper().Substring(0, 6).Contains("SELECT")))
            {
                throw new Exception("SQL Cannot be empty, null, or a query.");
            }

            SqlCommand command = null;

            if (transaction != null)
            {
                command = new SqlCommand(sql, connection, transaction);
            }
            else
            {
                command = new SqlCommand(sql, connection);
            }

            return command.ExecuteNonQuery();
        }

        public int InsertNewRecord(string sql)
        {
            if ((sql == null) || (sql == "") || !(sql.ToUpper().Substring(0, 6).Contains("INSERT")))
            {
                throw new Exception("SQL must be an INSERT statement");
            }

            int idOfInsertedRecord = -1;
            int numberOfInsertedRecords = ExecuteNonQuery(sql);

            if (numberOfInsertedRecords != 1)
            {
                throw new Exception("You can only insert one record at a time. You tried to insert " +
                    numberOfInsertedRecords + ".\n" + sql);
            }

            sql = "SELECT SCOPE_IDENTITY()";
            SqlDataReader reader = null;
            try
            {
                reader = GetReader(sql);
                while (reader.Read())
                {
                    idOfInsertedRecord = (int)reader.GetDecimal(0);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return idOfInsertedRecord;
        }
    }
}