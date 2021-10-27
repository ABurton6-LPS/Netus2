using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Netus2_DatabaseConnection.dbAccess
{
    public interface IConnectable
    {
        ConnectionState GetState();

        void CloseConnection();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        DataTable ReadIntoDataTable(string sql, DataTable dt);

        DataTable ReadIntoDataTable(string sql, DataTable dt, List<SqlParameter> parameters);

        int ExecuteNonQuery(string sql);

        int ExecuteNonQuery(string sql, List<SqlParameter> parameters);

        int InsertNewRecord(string sql, List<SqlParameter> parameters);
    }
}
