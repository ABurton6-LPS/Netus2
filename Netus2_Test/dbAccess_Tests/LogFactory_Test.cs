using Netus2;
using Netus2.dbAccess;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Netus2_Test.dbAccess_Tests
{
    public class LogFactory_Test
    {
        IConnectable connection;

        [SetUp]
        public void Setup()
        {
            connection = new Netus2DatabaseConnection();
            connection.OpenConnection();
            connection.BeginTransaction();

            if (!CheckIfTableExists("enum_log_action"))
                SchemaFactory.BuildSchema(connection);
        }

        [TestCase("log_enum_log_action")]
        [TestCase("log_enum_true_false")]
        [TestCase("log_enum_phone")]
        [TestCase("log_enum_address")]
        [TestCase("log_jct_person_address")]
        [TestCase("log_enum_class")]
        [TestCase("log_enum_gender")]
        [TestCase("log_enum_importance")]
        [TestCase("log_enum_organization")]
        [TestCase("log_enum_role")]
        [TestCase("log_enum_score_status")]
        [TestCase("log_enum_session")]
        [TestCase("log_enum_residence_status")]
        [TestCase("log_enum_country")]
        [TestCase("log_enum_state_province")]
        [TestCase("log_enum_grade")]
        [TestCase("log_enum_subject")]
        [TestCase("log_enum_period")]
        [TestCase("log_enum_category")]
        [TestCase("log_enum_ethnic")]
        [TestCase("log_enum_identifier")]
        [TestCase("log_person")]
        [TestCase("log_jct_person_role")]
        [TestCase("log_jct_person_person")]
        [TestCase("log_unique_identifier")]
        [TestCase("log_provider")]
        [TestCase("log_app")]
        [TestCase("log_jct_person_app")]
        [TestCase("log_phone_number")]
        [TestCase("log_address")]
        [TestCase("log_organization")]
        [TestCase("log_employment_session")]
        [TestCase("log_academic_session")]
        [TestCase("log_resource")]
        [TestCase("log_course")]
        [TestCase("log_jct_course_subject")]
        [TestCase("log_jct_course_grade")]
        [TestCase("log_class")]
        [TestCase("log_jct_class_period")]
        [TestCase("log_jct_class_resource")]
        [TestCase("log_lineitem")]
        [TestCase("log_enrollment")]
        [TestCase("log_mark")]
        public void Test_BuildSchema_CreatesExpectedTableWithProperColumns(String tableName)
        {
            List<String> expectedColumns = GetExpectedColumns(tableName);

            if (!CheckIfTableExists(tableName))
                LogFactory.BuildLogs(connection);

            Assert.True(CheckIfTableExists(tableName));
            Assert.AreEqual(expectedColumns, GetColumnsInTable(tableName));
        }

        public bool CheckIfTableExists(string tableName)
        {
            string sql =
                "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" + tableName + "'";
            int numberOfRecordsReturned = 0;
            SqlDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    numberOfRecordsReturned++;
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return numberOfRecordsReturned > 0;
        }

        private List<String> GetColumnsInTable(string tableName)
        {
            List<String> foundColumns = new List<String>();
            string sql = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + tableName + "' AND COLUMN_NAME NOT LIKE 'populated_by'";
            SqlDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    foundColumns.Add(reader.GetString(3));
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return foundColumns;
        }

        private List<String> GetExpectedColumns(String tableName)
        {
            List<String> expectedColumns = new List<String>();

            if (tableName.Equals("log_enum_log_action"))
            {
                expectedColumns.Add(tableName + "_id");
                expectedColumns.Add(tableName.Substring(4) + "_id");
                expectedColumns.Add("netus2_code");
                expectedColumns.Add("sis_code");
                expectedColumns.Add("hr_code");
                expectedColumns.Add("pip_code");
                expectedColumns.Add("descript");
                expectedColumns.Add("log_date");
                expectedColumns.Add("log_user");
            }
            else if ((tableName.Length >= 8) && (tableName.Substring(0, 8).Equals("log_enum")))
            {
                expectedColumns.Add(tableName + "_id");
                expectedColumns.Add(tableName.Substring(4) + "_id");
                expectedColumns.Add("netus2_code");
                expectedColumns.Add("sis_code");
                expectedColumns.Add("hr_code");
                expectedColumns.Add("pip_code");
                expectedColumns.Add("descript");
                expectedColumns.Add("log_date");
                expectedColumns.Add("log_user");
                expectedColumns.Add("enum_log_action_id");
            }
            else
            {
                switch (tableName)
                {
                    case "log_person":
                        expectedColumns.Add("log_person_id");
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("first_name");
                        expectedColumns.Add("middle_name");
                        expectedColumns.Add("last_name");
                        expectedColumns.Add("birth_date");
                        expectedColumns.Add("enum_gender_id");
                        expectedColumns.Add("enum_ethnic_id");
                        expectedColumns.Add("enum_residence_status_id");
                        expectedColumns.Add("login_name");
                        expectedColumns.Add("login_pw");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_jct_person_role":
                        expectedColumns.Add("log_jct_person_role_id");
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("enum_role_id");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_jct_person_person":
                        expectedColumns.Add("log_jct_person_person_id");
                        expectedColumns.Add("person_one_id");
                        expectedColumns.Add("person_two_id");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_unique_identifier":
                        expectedColumns.Add("log_unique_identifier_id");
                        expectedColumns.Add("unique_identifier_id");
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("unique_identifier");
                        expectedColumns.Add("enum_identifier_id");
                        expectedColumns.Add("is_active_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_provider":
                        expectedColumns.Add("log_provider_id");
                        expectedColumns.Add("provider_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("url_standard_access");
                        expectedColumns.Add("url_admin_access");
                        expectedColumns.Add("parent_provider_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_app":
                        expectedColumns.Add("log_app_id");
                        expectedColumns.Add("app_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("provider_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_jct_person_app":
                        expectedColumns.Add("log_jct_person_app_id");
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("app_id");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_phone_number":
                        expectedColumns.Add("log_phone_number_id");
                        expectedColumns.Add("phone_number_id");
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("phone_number");
                        expectedColumns.Add("is_primary_id");
                        expectedColumns.Add("enum_phone_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_address":
                        expectedColumns.Add("log_address_id");
                        expectedColumns.Add("address_id");
                        expectedColumns.Add("address_line_1");
                        expectedColumns.Add("address_line_2");
                        expectedColumns.Add("address_line_3");
                        expectedColumns.Add("address_line_4");
                        expectedColumns.Add("apartment");
                        expectedColumns.Add("city");
                        expectedColumns.Add("enum_state_province_id");
                        expectedColumns.Add("postal_code");
                        expectedColumns.Add("enum_country_id");
                        expectedColumns.Add("is_current_id");
                        expectedColumns.Add("enum_address_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_jct_person_address":
                        expectedColumns.Add("log_jct_person_address_id");
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("address_id");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_employment_session":
                        expectedColumns.Add("log_employment_session_id");
                        expectedColumns.Add("employment_session_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("start_date");
                        expectedColumns.Add("end_date");
                        expectedColumns.Add("is_primary_id");
                        expectedColumns.Add("enum_session_id");
                        expectedColumns.Add("organization_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_academic_session":
                        expectedColumns.Add("log_academic_session_id");
                        expectedColumns.Add("academic_session_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("start_date");
                        expectedColumns.Add("end_date");
                        expectedColumns.Add("enum_session_id");
                        expectedColumns.Add("parent_session_id");
                        expectedColumns.Add("organization_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_organization":
                        expectedColumns.Add("log_organization_id");
                        expectedColumns.Add("organization_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("enum_organization_id");
                        expectedColumns.Add("identifier");
                        expectedColumns.Add("building_code");
                        expectedColumns.Add("organization_parent_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_resource":
                        expectedColumns.Add("log_resource_id");
                        expectedColumns.Add("resource_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("enum_importance_id");
                        expectedColumns.Add("vendor_resource_identification");
                        expectedColumns.Add("vendor_identification");
                        expectedColumns.Add("application_identification");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_course":
                        expectedColumns.Add("log_course_id");
                        expectedColumns.Add("course_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("course_code");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_jct_course_subject":
                        expectedColumns.Add("log_jct_course_subject_id");
                        expectedColumns.Add("course_id");
                        expectedColumns.Add("enum_subject_id");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_jct_course_grade":
                        expectedColumns.Add("log_jct_course_grade_id");
                        expectedColumns.Add("course_id");
                        expectedColumns.Add("enum_grade_id");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_class":
                        expectedColumns.Add("log_class_id");
                        expectedColumns.Add("class_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("class_code");
                        expectedColumns.Add("enum_class_id");
                        expectedColumns.Add("room");
                        expectedColumns.Add("course_id");
                        expectedColumns.Add("academic_session_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_jct_class_period":
                        expectedColumns.Add("log_jct_class_period_id");
                        expectedColumns.Add("class_id");
                        expectedColumns.Add("enum_period_id");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_jct_class_resource":
                        expectedColumns.Add("log_jct_class_resource_id");
                        expectedColumns.Add("class_id");
                        expectedColumns.Add("resource_id");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_lineitem":
                        expectedColumns.Add("log_lineitem_id");
                        expectedColumns.Add("lineitem_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("descript");
                        expectedColumns.Add("assign_date");
                        expectedColumns.Add("due_date");
                        expectedColumns.Add("class_id");
                        expectedColumns.Add("enum_category_id");
                        expectedColumns.Add("markValueMin");
                        expectedColumns.Add("markValueMax");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_enrollment":
                        expectedColumns.Add("log_enrollment_id");
                        expectedColumns.Add("enrollment_id");
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("class_id");
                        expectedColumns.Add("enum_grade_id");
                        expectedColumns.Add("start_date");
                        expectedColumns.Add("end_date");
                        expectedColumns.Add("is_primary_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    case "log_mark":
                        expectedColumns.Add("log_mark_id");
                        expectedColumns.Add("mark_id");
                        expectedColumns.Add("lineitem_id");
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("enum_score_status_id");
                        expectedColumns.Add("score");
                        expectedColumns.Add("score_date");
                        expectedColumns.Add("comment");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                    default:
                        Assert.Fail("I need to know what columns to expect for table " + tableName);
                        expectedColumns.Add("log_date");
                        expectedColumns.Add("log_user");
                        expectedColumns.Add("enum_log_action_id");
                        break;
                }
            }

            return expectedColumns;
        }

        [TearDown]
        public void TearDown()
        {
            connection.RollbackTransaction();
            connection.CloseConnection();
        }
    }
}