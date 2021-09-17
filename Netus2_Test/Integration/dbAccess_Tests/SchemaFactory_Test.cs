using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.dbAccess.dbCreation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.Integration
{
    public class SchemaFactory_Test
    {
        IConnectable connection;

        [SetUp]
        public void Setup()
        {
            connection = DbConnectionFactory.GetLocalConnection();
            connection.BeginTransaction();
        }

        [TestCase("enum_log_action")]
        [TestCase("enum_true_false")]
        [TestCase("enum_phone")]
        [TestCase("enum_address")]
        [TestCase("enum_class")]
        [TestCase("enum_gender")]
        [TestCase("enum_importance")]
        [TestCase("enum_organization")]
        [TestCase("enum_role")]
        [TestCase("enum_score_status")]
        [TestCase("enum_session")]
        [TestCase("enum_residence_status")]
        [TestCase("enum_country")]
        [TestCase("enum_state_province")]
        [TestCase("enum_grade")]
        [TestCase("enum_subject")]
        [TestCase("enum_period")]
        [TestCase("enum_category")]
        [TestCase("enum_ethnic")]
        [TestCase("enum_identifier")]
        [TestCase("enum_sync_status")]
        [TestCase("person")]
        [TestCase("jct_person_role")]
        [TestCase("jct_person_person")]
        [TestCase("unique_identifier")]
        [TestCase("provider")]
        [TestCase("app")]
        [TestCase("jct_person_app")]
        [TestCase("phone_number")]
        [TestCase("address")]
        [TestCase("organization")]
        [TestCase("employment_session")]
        [TestCase("academic_session")]
        [TestCase("resource")]
        [TestCase("course")]
        [TestCase("jct_course_subject")]
        [TestCase("jct_course_grade")]
        [TestCase("class")]
        [TestCase("jct_class_period")]
        [TestCase("jct_class_resource")]
        [TestCase("lineitem")]
        [TestCase("enrollment")]
        [TestCase("mark")]
        [TestCase("sync_job")]
        [TestCase("sync_task")]
        [TestCase("sync_job_status")]
        [TestCase("sync_task_status")]
        [TestCase("sync_error")]
        public void Test_BuildSchema_CreatesExpectedTableWithProperColumns(String tableName)
        {
            List<String> expectedColumns = GetExpectedColumns(tableName);

            if (!CheckIfTableExists(tableName))
                SchemaFactory.BuildSchema(connection);

            Assert.True(CheckIfTableExists(tableName));
            List<string> columnsInTable = GetColumnsInTable(tableName);

            Assert.AreEqual(expectedColumns.Count, columnsInTable.Count);
            foreach (string expectedColumName in expectedColumns)
            {
                if (columnsInTable.Contains(expectedColumName))
                    Assert.Pass();
                else
                    Assert.Fail(tableName + " does not contain the expected column " + expectedColumName);
            }
        }

        public bool CheckIfTableExists(string tableName)
        {
            string sql =
                "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'" + tableName + "'";

            int numberOfRecordsReturned = 0;
            IDataReader reader = null;
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
            string sql = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + tableName + "'";

            IDataReader reader = null;
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

            if ((tableName.Length >= 4) && (tableName.Substring(0, 4).Equals("enum")))
            {
                expectedColumns.Add(tableName + "_id");
                expectedColumns.Add("netus2_code");
                expectedColumns.Add("sis_code");
                expectedColumns.Add("hr_code");
                expectedColumns.Add("descript");
            }
            else
            {
                switch (tableName)
                {
                    case "person":
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
                        break;
                    case "jct_person_role":
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("enum_role_id");
                        break;
                    case "jct_person_person":
                        expectedColumns.Add("person_one_id");
                        expectedColumns.Add("person_two_id");
                        break;
                    case "unique_identifier":
                        expectedColumns.Add("unique_identifier_id");
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("unique_identifier");
                        expectedColumns.Add("enum_identifier_id");
                        expectedColumns.Add("is_active_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        break;
                    case "provider":
                        expectedColumns.Add("provider_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("url_standard_access");
                        expectedColumns.Add("url_admin_access");
                        expectedColumns.Add("populated_by");
                        expectedColumns.Add("parent_provider_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        break;
                    case "app":
                        expectedColumns.Add("app_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("provider_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        break;
                    case "jct_person_app":
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("app_id");
                        break;
                    case "phone_number":
                        expectedColumns.Add("phone_number_id");
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("phone_number");
                        expectedColumns.Add("is_primary_id");
                        expectedColumns.Add("enum_phone_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        break;
                    case "address":
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
                        break;
                    case "jct_person_address":
                        expectedColumns.Add("person_id");
                        expectedColumns.Add("address_id");
                        break;
                    case "employment_session":
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
                        break;
                    case "academic_session":
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
                        expectedColumns.Add("term_code");
                        expectedColumns.Add("school_year");
                        break;
                    case "organization":
                        expectedColumns.Add("organization_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("enum_organization_id");
                        expectedColumns.Add("identifier");
                        expectedColumns.Add("sis_building_code");
                        expectedColumns.Add("hr_building_code");
                        expectedColumns.Add("organization_parent_id");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        break;
                    case "resource":
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
                        break;
                    case "course":
                        expectedColumns.Add("course_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("course_code");
                        expectedColumns.Add("created");
                        expectedColumns.Add("created_by");
                        expectedColumns.Add("changed");
                        expectedColumns.Add("changed_by");
                        break;
                    case "jct_course_subject":
                        expectedColumns.Add("course_id");
                        expectedColumns.Add("enum_subject_id");
                        break;
                    case "jct_course_grade":
                        expectedColumns.Add("course_id");
                        expectedColumns.Add("enum_grade_id");
                        break;
                    case "class":
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
                        break;
                    case "jct_class_period":
                        expectedColumns.Add("class_id");
                        expectedColumns.Add("enum_period_id");
                        break;
                    case "jct_class_resource":
                        expectedColumns.Add("class_id");
                        expectedColumns.Add("resource_id");
                        break;
                    case "lineitem":
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
                        break;
                    case "enrollment":
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
                        break;
                    case "mark":
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
                        break;
                    case "sync_job":
                        expectedColumns.Add("sync_job_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("timestamp");
                        break;
                    case "sync_task":
                        expectedColumns.Add("sync_task_id");
                        expectedColumns.Add("sync_job_id");
                        expectedColumns.Add("name");
                        expectedColumns.Add("timestamp");
                        break;
                    case "sync_job_status":
                        expectedColumns.Add("sync_job_status_id");
                        expectedColumns.Add("sync_job_id");
                        expectedColumns.Add("enum_sync_status_id");
                        expectedColumns.Add("timestamp");
                        break;
                    case "sync_task_status":
                        expectedColumns.Add("sync_task_status_id");
                        expectedColumns.Add("sync_task_id");
                        expectedColumns.Add("enum_sync_status_id");
                        expectedColumns.Add("timestamp");
                        break;
                    case "sync_error":
                        expectedColumns.Add("sync_error_id");
                        expectedColumns.Add("sync_job_id");
                        expectedColumns.Add("sync_task_id");
                        expectedColumns.Add("message");
                        expectedColumns.Add("stack_trace");
                        expectedColumns.Add("timestamp");
                        break;
                    default:
                        Assert.Fail("I need to know what columns to expect for table " + tableName);
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