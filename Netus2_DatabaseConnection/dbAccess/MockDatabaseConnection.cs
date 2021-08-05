using Moq;
using Netus2_DatabaseConnection.enumerations;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.dbAccess
{
    public class MockDatabaseConnection : IConnectable
    {
        private ConnectionState state = ConnectionState.Open;
        public Mock<IDataReader> mockReader = new Mock<IDataReader>();

        private List<string> testEnumKeys = new List<string>();               

        public MockDatabaseConnection(string connectionString)
        {
            testEnumKeys.Add("start");
            testEnumKeys.Add("end");
            testEnumKeys.Add("error");
            testEnumKeys.Add("tstorgid");
            testEnumKeys.Add("district");
            testEnumKeys.Add("school");
            testEnumKeys.Add("school year");
            testEnumKeys.Add("male");
            testEnumKeys.Add("cau");
            testEnumKeys.Add("primary teacher");
            testEnumKeys.Add("true");
            testEnumKeys.Add("cell");
            testEnumKeys.Add("mi");
            testEnumKeys.Add("us");
            testEnumKeys.Add("home");
            testEnumKeys.Add("state id");
            testEnumKeys.Add("employment");
            testEnumKeys.Add("1");
            testEnumKeys.Add("spn");
            testEnumKeys.Add("scheduled");
            testEnumKeys.Add("test");
            testEnumKeys.Add("student");
            testEnumKeys.Add("6");
            testEnumKeys.Add("submitted");
            testEnumKeys.Add("student id");
        }

        public ConnectionState GetState()
        {
            return state;
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
            if (sql.Contains("SELECT * FROM enum_"))
            {
                var enumReader = new Mock<IDataReader>();

                int count = -1;

                enumReader.Setup(x => x.Read())
                    .Returns(() => count < testEnumKeys.Count - 1)
                    .Callback(() => count++);

                enumReader.Setup(x => x.FieldCount)
                    .Returns(() => 6);

                enumReader.Setup(x => x.GetValue(0))
                    .Returns(() => 1);

                enumReader.Setup(x => x.GetValue(1))
                    .Returns(() => testEnumKeys[count]);

                enumReader.Setup(x => x.GetValue(2))
                    .Returns(() => null);

                enumReader.Setup(x => x.GetValue(3))
                    .Returns(() => null);

                enumReader.Setup(x => x.GetValue(4))
                    .Returns(() => null);

                enumReader.Setup(x => x.GetValue(5))
                    .Returns(() => "TestEnum");

                return enumReader.Object;
            }

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