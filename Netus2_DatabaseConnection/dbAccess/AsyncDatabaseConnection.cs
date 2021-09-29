using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Netus2_DatabaseConnection.dbAccess
{
    public class AsyncDatabaseConnection : IConnectable
    {
        private SqlConnection connection;
        private SqlTransaction transaction = null;

        public AsyncDatabaseConnection(string connectionString)
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

        public DataTable ReadIntoDataTable(string sql, DataTable dt)
        {
            DataTable emptyDataTable = dt.Clone();

            if ((sql == null) || (sql == "") || !(sql.ToUpper().Substring(0, 6).Contains("SELECT")))
            {
                throw new Exception("SQL must begin with 'SELECT'.");
            }

            try
            {
                if (transaction != null)
                {
                    using (var command = new SqlCommand(sql, connection, transaction))
                    {
                        using (var reader = command.ExecuteReaderAsync().Result)
                        {
                            emptyDataTable.Load(reader);
                        }
                    }
                }
                else
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReaderAsync().Result)
                        {
                            emptyDataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\nSQL when error occured:\n" + sql, e);
            }

            return emptyDataTable;
        }

        public int ExecuteNonQuery(string sql)
        {
            
            if ((sql == null) || (sql == "") || (sql.ToUpper().Substring(0, 6).Contains("SELECT")))
            {
                throw new Exception("SQL Cannot be empty, null, or a query.");
            }

            int returnValue = 0;

            try
            {
                if (transaction != null)
                {
                    using (var command = new SqlCommand(sql, connection, transaction))
                    {
                        returnValue = command.ExecuteNonQueryAsync().Result;
                    }
                }
                else
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        returnValue = command.ExecuteNonQueryAsync().Result;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\nSQL when error occured:\n" + sql, e);
            }

            if (returnValue == 0)
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
                if (transaction != null)
                    reader = new SqlCommand(sql, connection, transaction).ExecuteReader();
                else
                    reader = new SqlCommand(sql, connection).ExecuteReader();

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