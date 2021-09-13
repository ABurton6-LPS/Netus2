using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Netus2_DatabaseConnection.dbAccess
{
    public class GenericDatabaseConnection : IConnectable
    {
        private IDbConnection connection;
        private IDbTransaction transaction = null;

        public GenericDatabaseConnection(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public ConnectionState GetState()
        {
            return connection.State;
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

        public IDataReader GetReader(string sql)
        {
            if ((sql == null) || (sql == "") || !(sql.ToUpper().Substring(0, 6).Contains("SELECT")))
            {
                throw new Exception("SQL must begin with 'SELECT'.");
            }

            SqlCommand command = null;

            if (transaction != null)
            {
                command = new SqlCommand(sql, (SqlConnection)connection, (SqlTransaction)transaction);
            }
            else
            {
                command = new SqlCommand(sql, (SqlConnection)connection);
            }

            return command.ExecuteReader();
        }

        public int ExecuteNonQuery(string sql)
        {
            if ((sql == null) || (sql == "") || (sql.ToUpper().Substring(0, 6).Contains("SELECT")))
            {
                throw new Exception("SQL Cannot be empty, null, or a query.");
            }

            IDbCommand command = null;

            if (transaction != null)
            {
                command = new SqlCommand(sql, (SqlConnection)connection, (SqlTransaction)transaction);
            }
            else
            {
                command = new SqlCommand(sql, (SqlConnection)connection);
            }

            int returnValue = command.ExecuteNonQuery();
            if(returnValue == 0)
            {
                throw new Exception("SQL Non-Query did not affect any rows:\n" + sql);
            }
            return returnValue;
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
            IDataReader reader = null;
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