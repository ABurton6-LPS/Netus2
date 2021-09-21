using System.Data;
using System.Threading.Tasks;

namespace Netus2_DatabaseConnection.dbAccess
{
    public interface IConnectable
    {

        /// <summary>
        /// Indicates the state of the SqlConnection during the most recent network operation performed on the connection.
        /// </summary>
        ConnectionState GetState();

        /// <summary>
        /// Closes the connection to the database. You need to have an already open conection for this to be used.
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// Begin a transaction using the current connection.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commit an active transaction. You need to have already began a transaction for this to be used.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rollback an active transaction. You need to have already began a transaction for this to be used.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Provide a SQL statement as a string, and the DataTable object that your query should fill, and
        /// it will return a filled DataTable object containing the data that resulted from your SQL query.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dt"></param>
        DataTable ReadIntoDataTable(string sql, DataTable dt);

        /// <summary>
        /// Use this to pass in a SQL statement which is neither a Query or an Insert statement.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>The number of recoreds affected.</returns>
        int ExecuteNonQuery(string sql);

        /// <summary>
        /// Use this to pass in a SQL statement for the insertion of a single record into a database table.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tableBeingInsertedInto"></param>
        /// <returns>The largest primary key in the table after the insertion, which should be the
        /// primary key of the record just inserted.</returns>
        int InsertNewRecord(string sql);
    }
}
