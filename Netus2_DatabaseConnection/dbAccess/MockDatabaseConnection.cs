using Moq;
using System;
using System.Data;

namespace Netus2_DatabaseConnection.dbAccess
{
    public class MockDatabaseConnection : IConnectable
    {
        private ConnectionState state = ConnectionState.Closed;
        public Mock<IDataReader> mockReader = new Mock<IDataReader>();

        public MockDatabaseConnection(string connectionString)
        {
            //Do Nothing
        }

        public ConnectionState GetState()
        {
            return state;
        }

        public void OpenConnection()
        {
            state = ConnectionState.Open;
        }

        public void CloseConnection()
        {
            state = ConnectionState.Closed;
        }

        public void BeginTransaction()
        {
            //Do Nothing
        }

        public void CommitTransaction()
        {
            //Do Nothing
        }

        public void RollbackTransaction()
        {
            //Do Nothing
        }

        public IDataReader GetReader(string sql)
        {
            return mockReader.Object;
        }

        public int ExecuteNonQuery(string sql)
        {
            return 1;
        }

        public int InsertNewRecord(string sql)
        {
            return 1;
        }
    }
}