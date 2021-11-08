using Moq;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Netus2_DatabaseConnection.dbAccess
{
    public class MockDatabaseConnection : IConnectable
    {
        private ConnectionState state = ConnectionState.Open;
        public Mock<IDataReader> mockReader = new Mock<IDataReader>();
        public string expectedReaderSql = null;
        public string expectedNonQuerySql = null;
        public string expectedNewRecordSql = null;

        private List<DataRow> testEnums = new List<DataRow>();               

        public MockDatabaseConnection()
        {
            List<string> testEnumKeys = new List<string>();

            testEnumKeys.Add("start");
            testEnumKeys.Add("end");
            testEnumKeys.Add("error");
            testEnumKeys.Add("tstorgid");
            testEnumKeys.Add("district");
            testEnumKeys.Add("school");
            testEnumKeys.Add("school year");
            testEnumKeys.Add("semester");
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
            testEnumKeys.Add("fl");
            testEnumKeys.Add("scheduled");
            testEnumKeys.Add("test");
            testEnumKeys.Add("student");
            testEnumKeys.Add("6");
            testEnumKeys.Add("submitted");
            testEnumKeys.Add("student id");
            testEnumKeys.Add("tstsession");
            testEnumKeys.Add("01652");
            testEnumKeys.Add("staff");
            testEnumKeys.Add("01656");
            testEnumKeys.Add("primary");
            testEnumKeys.Add("enum_id");
            testEnumKeys.Add("sisread_end");
            testEnumKeys.Add("sisread_error");
            testEnumKeys.Add("sisread_start");
            testEnumKeys.Add("max_threads");
            testEnumKeys.Add("false");
            testEnumKeys.Add("unset");
            testEnumKeys.Add("be");
            testEnumKeys.Add("14");
            testEnumKeys.Add("aide");
            testEnumKeys.Add("sis_organization_query");
            testEnumKeys.Add("sis_academic_session_query");
            testEnumKeys.Add("sis_person_query");
            testEnumKeys.Add("sis_address_query");
            testEnumKeys.Add("sis_course_query");
            testEnumKeys.Add("sis_jct_person_address_query");
            testEnumKeys.Add("me");
            testEnumKeys.Add("am");
            testEnumKeys.Add("office");

            DataTable mockEnumDataTable = DataTableFactory.CreateDataTable_Netus2_Enumeration();

            foreach(string testEnumKey in testEnumKeys)
            {
                DataRow newRow = mockEnumDataTable.NewRow();
                newRow["netus2_code"] = testEnumKey;
                newRow["sis_code"] = testEnumKey;
                newRow["hr_code"] = null;
                newRow["descript"] = "Test Enum";
                testEnums.Add(newRow);
            }            
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

        public DataTable ReadIntoDataTable(string sql, DataTable dt, List<SqlParameter> parameters)
        {
            if (sql.Contains("SELECT * FROM enum_"))
            {
                int count = -1;
                var enumReader = new Mock<IDataReader>();

                enumReader.Setup(x => x.Read())
                    .Returns(() => count < testEnums.Count - 1)
                    .Callback(() => count++);

                enumReader.Setup(x => x.FieldCount)
                    .Returns(() => 5);

                enumReader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = count;
                        values[1] = testEnums[count]["netus2_code"];
                        values[2] = testEnums[count]["sis_code"];
                        values[3] = testEnums[count]["hr_code"];
                        values[4] = testEnums[count]["descript"];
                    }
                ).Returns(count);

                enumReader.Setup(x => x.GetName(0))
                .Returns(() => "enum_id");
                enumReader.Setup(x => x.GetOrdinal("enum_id"))
                    .Returns(() => 0);
                enumReader.Setup(x => x.GetFieldType(0))
                    .Returns(() => typeof(int));

                enumReader.Setup(x => x.GetName(1))
                .Returns(() => "netus2_code");
                enumReader.Setup(x => x.GetOrdinal("netus2_code"))
                    .Returns(() => 1);
                enumReader.Setup(x => x.GetFieldType(1))
                    .Returns(() => typeof(string));

                enumReader.Setup(x => x.GetName(2))
                .Returns(() => "sis_code");
                enumReader.Setup(x => x.GetOrdinal("sis_code"))
                    .Returns(() => 2);
                enumReader.Setup(x => x.GetFieldType(2))
                    .Returns(() => typeof(string));

                enumReader.Setup(x => x.GetName(3))
                .Returns(() => "hr_code");
                enumReader.Setup(x => x.GetOrdinal("hr_code"))
                    .Returns(() => 3);
                enumReader.Setup(x => x.GetFieldType(3))
                    .Returns(() => typeof(string));

                enumReader.Setup(x => x.GetName(4))
                .Returns(() => "descript");
                enumReader.Setup(x => x.GetOrdinal("descript"))
                    .Returns(() => 4);
                enumReader.Setup(x => x.GetFieldType(4))
                    .Returns(() => typeof(string));

                if(dt.Rows.Count == 0)
                    dt = GetTaskDataTableFromReader(enumReader.Object, dt);

                return dt;
            }
            if (sql.Contains("config"))
            {
                DataTable dtConfig = DataTableFactory.CreateDataTable_Netus2_Config();
                if(dtConfig.Rows.Count == 0)
                {
                    DataRow row = dtConfig.NewRow();
                    row["config_id"] = -1;
                    row["enum_config_id"] = -1;
                    row["config_value"] = "-1";
                    row["is_for_student_id"] = -1;
                    row["is_for_staff_id"] = -1;
                    dtConfig.Rows.Add(row);
                }

                return dtConfig;
            }
            else if (expectedReaderSql != null)
            {
                Assert.AreEqual(expectedReaderSql, sql);
                Assert.IsNull(expectedNewRecordSql);
                Assert.IsNull(expectedNonQuerySql);

                expectedReaderSql = null;
            }

            return GetTaskDataTableFromReader(mockReader.Object, dt);
        }

        private DataTable GetTaskDataTableFromReader(IDataReader reader, DataTable dt)
        {
            DataTable cloanedDataTable = dt.Clone();
            cloanedDataTable.Clear();
            if(reader != null)
                cloanedDataTable.Load(reader);
            return cloanedDataTable;
        }

        public int ExecuteNonQuery(string sql, List<SqlParameter> parameters)
        {
            if (expectedNonQuerySql != null)
            {
                Assert.AreEqual(expectedNonQuerySql, sql);
                Assert.IsNull(expectedNewRecordSql);
                Assert.IsNull(expectedReaderSql);
                expectedNonQuerySql = null;
            }

            return 1;
        }

        public int InsertNewRecord(string sql, List<SqlParameter> parameters)
        {
            if (expectedNewRecordSql != null)
            {
                Assert.AreEqual(expectedNewRecordSql, sql);
                Assert.IsNull(expectedNonQuerySql);
                Assert.IsNull(expectedReaderSql);

                expectedNewRecordSql = null;
            }
            else if (sql.Contains("INSERT INTO sync_error"))
            {
                string values = sql.Substring(82);
                string[] separatedValues = values.Split(", '");

                string errorMessage = separatedValues[1].Substring(0, separatedValues[1].Length - 1).Trim();

                string fullStackTrace = separatedValues[2].Trim();
                string stackTraceWithoutFrill = fullStackTrace.Substring(fullStackTrace.IndexOf("at ") + 3, fullStackTrace.Length - 4);
                string[] stackTraceParts = stackTraceWithoutFrill.Split(" in ");
                string stackTrace = "";
                foreach(string stackTracePart in stackTraceParts)
                {
                    stackTrace += stackTracePart.Trim() + "\n";
                }

                Assert.Fail("An error was logged:\n" + errorMessage + "\n" + stackTrace);
            }

            return 1;
        }

        public DataTable ReadIntoDataTable(string sql, DataTable dt)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            return ReadIntoDataTable(sql, dt, parameters);
        }

        public int ExecuteNonQuery(string sql)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            return ExecuteNonQuery(sql, parameters);
        }

        public int InsertNewRecord(string sql)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            return InsertNewRecord(sql, parameters);
        }
    }
}