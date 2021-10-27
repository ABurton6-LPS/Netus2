using System;
using System.Collections.Generic;
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
            return ReadIntoDataTable(sql, dt, new List<SqlParameter>());
        }

        public DataTable ReadIntoDataTable(string sql, DataTable dt, List<SqlParameter> parameters)
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
                        if (parameters.Count > 0)
                            command.Parameters.AddRange(parameters.ToArray());
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
                        if (parameters.Count > 0)
                            command.Parameters.AddRange(parameters.ToArray());
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
            return ExecuteNonQuery(sql, new List<SqlParameter>());
        }

        public int ExecuteNonQuery(string sql, List<SqlParameter> parameters)
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
                        if (parameters.Count > 0)
                            command.Parameters.AddRange(parameters.ToArray());

                        returnValue = command.ExecuteNonQueryAsync().Result;
                    }
                }
                else
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        if (parameters.Count > 0)
                            command.Parameters.AddRange(parameters.ToArray());

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
                foreach(SqlParameter parameter in parameters)
                {
                    sql = sql.Replace(parameter.ParameterName, "'" + parameter.Value.ToString() + "'");
                }

                throw new Exception("SQL Non-Query did not affect any rows:\n" + sql);
            }

            return returnValue;
        }

        public int InsertNewRecord(string sql, List<SqlParameter> parameters)
        {
            if ((sql == null) || (sql == "") || !(sql.ToUpper().Substring(0, 6).Contains("INSERT")))
            {
                throw new Exception("SQL must be an INSERT statement");
            }

            int idOfInsertedRecord = -1;
            sql += "; SELECT SCOPE_IDENTITY()";

            if (transaction != null)
            {
                using (var command = new SqlCommand(sql, connection, transaction))
                {
                    if (parameters.Count > 0)
                        command.Parameters.AddRange(parameters.ToArray());

                    idOfInsertedRecord = Convert.ToInt32(command.ExecuteScalarAsync().Result);
                }
            }
            else
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    if (parameters.Count > 0)
                        command.Parameters.AddRange(parameters.ToArray());

                    idOfInsertedRecord = Convert.ToInt32(command.ExecuteScalarAsync().Result);
                }
            }

            return idOfInsertedRecord;
        }
    }
}