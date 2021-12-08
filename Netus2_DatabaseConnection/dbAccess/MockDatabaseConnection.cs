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

        private List<Enumeration> testEnums = new List<Enumeration>();

        public MockDatabaseConnection()
        {
            PopulateTestEnums();
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
                        values[1] = testEnums[count].Netus2Code;
                        values[2] = testEnums[count].SisCode;
                        values[3] = testEnums[count].HrCode;
                        values[4] = testEnums[count].Descript;
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

                if (dt.Rows.Count == 0)
                    dt = GetTaskDataTableFromReader(enumReader.Object, dt);

                return dt;
            }
            if (sql.Contains("config"))
            {
                DataTable dtConfig = DataTableFactory.CreateDataTable_Netus2_Config();
                if (dtConfig.Rows.Count == 0)
                {
                    DataRow row = dtConfig.NewRow();
                    row["config_id"] = -1;
                    row["type"] = "SQL";
                    row["name"] = "Test";
                    row["value"] = "1";
                    row["is_for_student"] = false;
                    row["is_for_staff"] = false;
                    row["descript"] = "Only used for unit tests";
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
            if (reader != null)
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
                foreach (string stackTracePart in stackTraceParts)
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

        private void PopulateTestEnums()
        {
            Enumeration testEnum = new Enumeration();
            testEnum.Netus2Code = "sis_Db_String";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "max_threads";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sis_organization_query";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sis_academic_session_query";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sis_person_query";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sis_address_query";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sis_jct_person_address_query";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sis_phone_number_query";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sis_jct_person_phone_number_query";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sis_course_query";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sis_class_query";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sis_enrollment_query";
            testEnum.Descript = "Enum_Config";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "insert";
            testEnum.Descript = "Enum_Log_Action";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "update";
            testEnum.Descript = "Enum_Log_Action";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "delete";
            testEnum.Descript = "Enum_Log_Action";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "start";
            testEnum.Descript = "Enum_Sync_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "error";
            testEnum.Descript = "Enum_Sync_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "end";
            testEnum.Descript = "Enum_Sync_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sisRead_start";
            testEnum.Descript = "Enum_Sync_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sisRead_end";
            testEnum.Descript = "Enum_Sync_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sisRead_error";
            testEnum.Descript = "Enum_Sync_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "tasks_started";
            testEnum.Descript = "Enum_Sync_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "unset";
            testEnum.SisCode = "_";
            testEnum.Descript = "Enum_True_False";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "false";
            testEnum.SisCode = "0";
            testEnum.Descript = "Enum_True_False";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "true";
            testEnum.SisCode = "1";
            testEnum.Descript = "Enum_True_False";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "cell";
            testEnum.SisCode = "c";
            testEnum.Descript = "Enum_Phone";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "fax";
            testEnum.SisCode = "f";
            testEnum.Descript = "Enum_Phone";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "pager";
            testEnum.SisCode = "p";
            testEnum.Descript = "Enum_Phone";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "residence";
            testEnum.SisCode = "r";
            testEnum.Descript = "Enum_Phone";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sms";
            testEnum.SisCode = "s";
            testEnum.Descript = "Enum_Phone";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "tdd";
            testEnum.SisCode = "t";
            testEnum.Descript = "Enum_Phone";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "work";
            testEnum.SisCode = "w";
            testEnum.Descript = "Enum_Phone";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "multiple types";
            testEnum.SisCode = "mx";
            testEnum.Descript = "Enum_Phone";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "home";
            testEnum.SisCode = "home";
            testEnum.Descript = "Enum_Address";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "office";
            testEnum.SisCode = "office";
            testEnum.Descript = "Enum_Address";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "vacation";
            testEnum.SisCode = "vacation";
            testEnum.Descript = "Enum_Address";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "birth";
            testEnum.SisCode = "birth";
            testEnum.Descript = "Enum_Address";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "mailing";
            testEnum.SisCode = "mailing";
            testEnum.Descript = "Enum_Address";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "homeroom";
            testEnum.SisCode = "homeroom";
            testEnum.Descript = "Enum_Class";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "scheduled";
            testEnum.SisCode = "scheduled";
            testEnum.Descript = "Enum_Class";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "male";
            testEnum.SisCode = "m";
            testEnum.Descript = "Enum_Gender";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "female";
            testEnum.SisCode = "f";
            testEnum.Descript = "Enum_Gender";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "primary";
            testEnum.SisCode = "primary";
            testEnum.Descript = "Enum_Importance";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "secondary";
            testEnum.SisCode = "secondary";
            testEnum.Descript = "Enum_Importance";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "department";
            testEnum.SisCode = "department";
            testEnum.Descript = "Enum_Organization";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "school";
            testEnum.SisCode = "school";
            testEnum.Descript = "Enum_Organization";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "district";
            testEnum.SisCode = "district";
            testEnum.Descript = "Enum_Organization";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "state";
            testEnum.SisCode = "state";
            testEnum.Descript = "Enum_Organization";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "national";
            testEnum.SisCode = "national";
            testEnum.Descript = "Enum_Organization";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "administrator";
            testEnum.SisCode = "administrator";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "aide";
            testEnum.SisCode = "aide";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "guardian";
            testEnum.SisCode = "guardian";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "parent";
            testEnum.SisCode = "parent";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "proctor";
            testEnum.SisCode = "proctor";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "relative";
            testEnum.SisCode = "relative";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "student";
            testEnum.SisCode = "student";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "primary teacher";
            testEnum.SisCode = "primary teacher";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "secondary teacher";
            testEnum.SisCode = "secondary teacher";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "tirtiary teacher";
            testEnum.SisCode = "tirtiary teacher";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "counselor";
            testEnum.SisCode = "counselor";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "staff";
            testEnum.SisCode = "staff";
            testEnum.Descript = "Enum_Role";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "exempt";
            testEnum.SisCode = "exempt";
            testEnum.Descript = "Enum_Score_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "fully graded";
            testEnum.SisCode = "fully graded";
            testEnum.Descript = "Enum_Score_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "not submitted";
            testEnum.SisCode = "not submitted";
            testEnum.Descript = "Enum_Score_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "partially graded";
            testEnum.SisCode = "partially graded";
            testEnum.Descript = "Enum_Score_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "submitted";
            testEnum.SisCode = "submitted";
            testEnum.Descript = "Enum_Score_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "grading period";
            testEnum.SisCode = "grading period";
            testEnum.Descript = "Enum_Session";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "semester";
            testEnum.SisCode = "semester";
            testEnum.Descript = "Enum_Session";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "school year";
            testEnum.SisCode = "school year";
            testEnum.Descript = "Enum_Session";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "term";
            testEnum.SisCode = "term";
            testEnum.Descript = "Enum_Session";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "employment";
            testEnum.SisCode = "employment";
            testEnum.Descript = "Enum_Session";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "01652";
            testEnum.SisCode = "01652";
            testEnum.Descript = "Enum_Residence_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "01653";
            testEnum.SisCode = "01653";
            testEnum.Descript = "Enum_Residence_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "01654";
            testEnum.SisCode = "01654";
            testEnum.Descript = "Enum_Residence_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "01655";
            testEnum.SisCode = "01655";
            testEnum.Descript = "Enum_Residence_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "01656";
            testEnum.SisCode = "01656";
            testEnum.Descript = "Enum_Residence_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "us";
            testEnum.SisCode = "us";
            testEnum.Descript = "Enum_Counry";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "mi";
            testEnum.SisCode = "mi";
            testEnum.Descript = "Enum_State_Province";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "-6";
            testEnum.SisCode = "-6";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "-5";
            testEnum.SisCode = "-5";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "-4";
            testEnum.SisCode = "-4";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "-3";
            testEnum.SisCode = "-3";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "-2";
            testEnum.SisCode = "-2";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "-1";
            testEnum.SisCode = "-1";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "0";
            testEnum.SisCode = "0";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "1";
            testEnum.SisCode = "1";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "2";
            testEnum.SisCode = "2";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "3";
            testEnum.SisCode = "3";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "4";
            testEnum.SisCode = "4";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "5";
            testEnum.SisCode = "5";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "6";
            testEnum.SisCode = "6";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "7";
            testEnum.SisCode = "7";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "8";
            testEnum.SisCode = "8";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "9";
            testEnum.SisCode = "9";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "10";
            testEnum.SisCode = "10";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "11";
            testEnum.SisCode = "11";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "12";
            testEnum.SisCode = "12";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "14";
            testEnum.SisCode = "14";
            testEnum.Descript = "Enum_Grade";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "agri";
            testEnum.SisCode = "agri";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "art";
            testEnum.SisCode = "art";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "be";
            testEnum.SisCode = "be";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "bl";
            testEnum.SisCode = "bl";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "bus";
            testEnum.SisCode = "bus";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "cj";
            testEnum.SisCode = "cj";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "co";
            testEnum.SisCode = "co";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "comp";
            testEnum.SisCode = "comp";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "cwp";
            testEnum.SisCode = "cwp";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "e9";
            testEnum.SisCode = "e9";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "ec";
            testEnum.SisCode = "ec";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "elec";
            testEnum.SisCode = "elec";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "eng";
            testEnum.SisCode = "eng";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "fl";
            testEnum.SisCode = "fl";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "he";
            testEnum.SisCode = "he";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "hist";
            testEnum.SisCode = "hist";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "ia";
            testEnum.SisCode = "ia";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "is";
            testEnum.SisCode = "is";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "la";
            testEnum.SisCode = "la";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "lf";
            testEnum.SisCode = "lf";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "lit";
            testEnum.SisCode = "lit";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "math";
            testEnum.SisCode = "math";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "misc";
            testEnum.SisCode = "misc";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "nc";
            testEnum.SisCode = "nc";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "on";
            testEnum.SisCode = "on";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "pe";
            testEnum.SisCode = "pe";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "peh";
            testEnum.SisCode = "peh";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "phil";
            testEnum.SisCode = "phil";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sci";
            testEnum.SisCode = "sci";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "soc";
            testEnum.SisCode = "soc";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "sped";
            testEnum.SisCode = "sped";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "ug";
            testEnum.SisCode = "ug";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "voc";
            testEnum.SisCode = "voc";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "xa";
            testEnum.SisCode = "xa";
            testEnum.Descript = "Enum_Subject";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "h";
            testEnum.SisCode = "h";
            testEnum.Descript = "Enum_Period";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "test";
            testEnum.SisCode = "test";
            testEnum.Descript = "Enum_Category";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "homework";
            testEnum.SisCode = "homework";
            testEnum.Descript = "Enum_Category";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "public classwork";
            testEnum.SisCode = "public classwork";
            testEnum.Descript = "Enum_Category";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "nat";
            testEnum.SisCode = "1";
            testEnum.Descript = "Enum_Ethnic";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "cau";
            testEnum.SisCode = "2";
            testEnum.Descript = "Enum_Ethnic";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "his";
            testEnum.SisCode = "3";
            testEnum.Descript = "Enum_Ethnic";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "baa";
            testEnum.SisCode = "4";
            testEnum.Descript = "Enum_Ethnic";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "asi";
            testEnum.SisCode = "5";
            testEnum.Descript = "Enum_Ethnic";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "nhi";
            testEnum.SisCode = "6";
            testEnum.Descript = "Enum_Ethnic";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "mul";
            testEnum.SisCode = "7";
            testEnum.Descript = "Enum_Ethnic";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "student id";
            testEnum.SisCode = "unique_id";
            testEnum.Descript = "Enum_Identifier";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "state id";
            testEnum.SisCode = "state id";
            testEnum.Descript = "Enum_Identifier";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "employee id";
            testEnum.SisCode = "funiq";
            testEnum.Descript = "Enum_Identifier";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "badge id";
            testEnum.SisCode = "badge id";
            testEnum.Descript = "Enum_Identifier";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "active";
            testEnum.SisCode = "active";
            testEnum.Descript = "Enum_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "inactive";
            testEnum.SisCode = "inactive";
            testEnum.Descript = "Enum_Status";
            testEnums.Add(testEnum);

            testEnum = new Enumeration();
            testEnum.Netus2Code = "on leave";
            testEnum.SisCode = "on leave";
            testEnum.Descript = "Enum_Status";
            testEnums.Add(testEnum);
        }
    }
}