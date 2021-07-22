using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netus2.dbAccess
{
    public interface IConnectable
    {

        /// <summary>
        /// Indicates the state of the SqlConnection during the most recent network operation performed on the connection.
        /// </summary>
        ConnectionState GetState();

        /// <summary>
        /// Opens a connection to the database.
        /// </summary>
        void OpenConnection();

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
        /// Provide a SQL statement as a string, and it will provide the SqlDataReader object that can be used
        /// to retrieve the results of your SQL query.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>
        /// <para>
        /// SqlDataReader object that can be iterated through to retrive the results of your query.
        /// </para>
        /// Note: Make sure you close this reader when you are done, and before opening another.
        /// </returns>
        SqlDataReader GetReader(string sql);

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
