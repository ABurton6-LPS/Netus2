using Netus2.dbAccess;
using System;

namespace Netus2
{
    public class LogFactory
    {
        public static void BuildLogs(IConnectable connection)
        {
            BuildLogEnums(connection);
            BuildLogDataTables(connection);
        }

        private static void BuildLogEnums(IConnectable connection)
        {
            BuildLogEnumLogAction(connection);
            BuildLogEnumTrueFalse(connection);
            BuildLogEnumPhone(connection);
            BuildLogEnumAddress(connection);
            BuildLogEnumClass(connection);
            BuildLogEnumGender(connection);
            BuildLogEnumImportance(connection);
            BuildLogEnumOrganization(connection);
            BuildLogEnumRole(connection);
            BuildLogEnumScoreStatus(connection);
            BuildLogEnumSession(connection);
            BuildLogEnumResidenceStatus(connection);
            BuildLogEnumCountry(connection);
            BuildLogEnumStateProvince(connection);
            BuildLogEnumGrade(connection);
            BuildLogEnumSubject(connection);
            BuildLogEnumPeriod(connection);
            BuildLogEnumCategory(connection);
            BuildLogEnumEthnic(connection);
            BuildLogEnumIdentifier(connection);
        }

        private static void BuildLogDataTables(IConnectable connection)
        {
            BuildLogPerson(connection);
            BuildLogJctPersonRole(connection);
            BuildLogJctPersonPerson(connection);
            BuildLogUniqueIdentifier(connection);
            BuildLogProvider(connection);
            BuildLogApp(connection);
            BuildLogJctPersonApp(connection);
            BuildLogPhoneNumber(connection);
            BuildLogAddress(connection);
            BuildLogJctPersonAddress(connection);
            BuildLogEmploymentSession(connection);
            BuildLogAcademicSession(connection);
            BuildLogOrganization(connection);
            BuildLogResource(connection);
            BuildLogCourse(connection);
            BuildLogJctCourseSubject(connection);
            BuildLogJctCourseGrade(connection);
            BuildLogClass(connection);
            BuildLogJctClassPerson(connection);
            BuildLogJctClassPeriod(connection);
            BuildLogJctClassResource(connection);
            BuildLogLineItem(connection);
            BuildLogEnrollment(connection);
            BuildLogJctEnrollmentAcademicSession(connection);
            BuildLogMark(connection);
        }

        private static void BuildLogEnumLogAction(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_log_action ("
                + "log_enum_log_action_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_log_action_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumTrueFalse(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_true_false ("
                + "log_enum_true_false_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_true_false_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumPhone(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_phone ("
                + "log_enum_phone_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_phone_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumAddress(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_address ("
                + "log_enum_address_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_address_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumClass(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_class ("
                + "log_enum_class_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_class_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumGender(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_gender ("
                + "log_enum_gender_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_gender_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumImportance(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_importance ("
                + "log_enum_importance_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_importance_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumOrganization(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_organization ("
                + "log_enum_organization_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_organization_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumRole(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_role ("
                + "log_enum_role_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_role_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumScoreStatus(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_score_status ("
                + "log_enum_score_status_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_score_status_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumSession(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_session ("
                + "log_enum_session_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_session_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumResidenceStatus(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_residence_status ("
                + "log_enum_residence_status_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_residence_status_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumCountry(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_country ("
                + "log_enum_country_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_country_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumStateProvince(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_state_province ("
                + "log_enum_state_province_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_state_province_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumGrade(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_grade ("
                + "log_enum_grade_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_grade_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumSubject(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_subject ("
                + "log_enum_subject_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_subject_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumPeriod(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_period ("
                + "log_enum_period_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_period_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumCategory(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_category ("
                + "log_enum_category_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_category_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumEthnic(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_ethnic ("
                + "log_enum_ethnic_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_ethnic_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnumIdentifier(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enum_identifier ("
                + "log_enum_identifier_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enum_identifier_id int,"
                + "netus2_code varchar(20),"
                + "sis_code varchar(20),"
                + "hr_code varchar(20),"
                + "pip_code varchar(20),"
                + "descript varchar(150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogPerson(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_person ("
                + "log_person_id int IDENTITY(1,1) PRIMARY KEY,"
                + "person_id int,"
                + "first_name varchar(30),"
                + "middle_name varchar(30),"
                + "last_name varchar(30),"
                + "birth_date date,"
                + "enum_gender_id int,"
                + "enum_ethnic_id int,"
                + "enum_residence_status_id int,"
                + "login_name varchar(150),"
                + "login_pw varchar(150),"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogJctPersonRole(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_jct_person_role ("
                + "log_jct_person_role_id int IDENTITY(1,1) PRIMARY KEY,"
                + "person_id int,"
                + "enum_role_id int,"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogJctPersonPerson(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_jct_person_person ("
                + "log_jct_person_person_id int IDENTITY(1,1) PRIMARY KEY,"
                + "person_one_id int,"
                + "person_two_id int,"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogUniqueIdentifier(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_unique_identifier ("
                + "log_unique_identifier_id int IDENTITY(1,1) PRIMARY KEY,"
                + "unique_identifier_id int,"
                + "person_id int,"
                + "unique_identifier varchar(150),"
                + "enum_identifier_id int,"
                + "is_active_id int,"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogProvider(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_provider ("
                + "log_provider_id int IDENTITY(1,1) PRIMARY KEY,"
                + "provider_id int,"
                + "name varchar(150),"
                + "url_standard_access varchar(150),"
                + "url_admin_access varchar(150),"
                + "parent_provider_id int,"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogApp(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_app ("
                + "log_app_id int IDENTITY(1,1) PRIMARY KEY,"
                + "app_id int,"
                + "name varchar(150),"
                + "provider_id int,"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogJctPersonApp(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_jct_person_app ("
                + "log_jct_person_app_id int IDENTITY(1,1) PRIMARY KEY,"
                + "person_id int,"
                + "app_id int,"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogPhoneNumber(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_phone_number ("
                + "log_phone_number_id int IDENTITY(1,1) PRIMARY KEY,"
                + "phone_number_id int,"
                + "person_id int,"
                + "phone_number varchar(20),"
                + "is_primary_id int,"
                + "enum_phone_id int,"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogAddress(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_address ("
                + "log_address_id int IDENTITY(1,1) PRIMARY KEY,"
                + "address_id int,"
                + "address_line_1 varchar(150),"
                + "address_line_2 varchar(150),"
                + "address_line_3 varchar(150),"
                + "address_line_4 varchar(150),"
                + "apartment varchar(15),"
                + "city varchar(150),"
                + "enum_state_province_id int,"
                + "postal_code varchar(15),"
                + "enum_country_id int,"
                + "is_current_id int,"
                + "enum_address_id int,"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogJctPersonAddress(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_jct_person_address ("
                + "log_jct_person_address_id int IDENTITY(1,1) PRIMARY KEY,"
                + "person_id int,"
                + "address_id int,"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEmploymentSession(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_employment_session ("
                + "log_employment_session_id int IDENTITY(1,1) PRIMARY KEY,"
                + "employment_session_id int,"
                + "name varchar(150),"
                + "person_id int,"
                + "start_date date,"
                + "end_date date,"
                + "is_primary_id int,"
                + "enum_session_id int,"
                + "organization_id int,"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogAcademicSession(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_academic_session ("
                + "log_academic_session_id int IDENTITY(1,1) PRIMARY KEY,"
                + "term_code varchar(5), "
                + "school_year int, "
                + "academic_session_id int,"
                + "name varchar(150),"
                + "start_date date,"
                + "end_date date,"
                + "enum_session_id int,"
                + "parent_session_id int,"
                + "organization_id int,"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogOrganization(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_organization ("
                + "log_organization_id int IDENTITY(1,1) PRIMARY KEY,"
                + "organization_id int,"
                + "name varchar(150),"
                + "enum_organization_id int,"
                + "identifier varchar(150),"
                + "building_code varchar(150),"
                + "organization_parent_id int,"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogResource(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_resource ("
                + "log_resource_id int IDENTITY(1,1) PRIMARY KEY,"
                + "resource_id int,"
                + "name varchar(150),"
                + "enum_importance_id int,"
                + "vendor_resource_identification varchar(150),"
                + "vendor_identification varchar(150),"
                + "application_identification varchar(150),"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogCourse(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_course ("
                + "log_course_id int IDENTITY(1,1) PRIMARY KEY,"
                + "course_id int,"
                + "name varchar(150),"
                + "course_code varchar(150),"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogJctCourseSubject(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_jct_course_subject ("
                + "log_jct_course_subject_id int IDENTITY(1,1) PRIMARY KEY,"
                + "course_id int,"
                + "enum_subject_id int,"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogJctCourseGrade(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_jct_course_grade ("
                + "log_jct_course_grade_id int IDENTITY(1,1) PRIMARY KEY,"
                + "course_id int,"
                + "enum_grade_id int,"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogClass(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_class ("
                + "log_class_id int IDENTITY(1,1) PRIMARY KEY,"
                + "class_id int,"
                + "name varchar(150),"
                + "class_code varchar(150),"
                + "enum_class_id int,"
                + "room varchar(150),"
                + "course_id int,"
                + "academic_session_id int,"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogJctClassPerson(IConnectable connection)
        {
            string sql =
            "CREATE TABLE log_jct_class_person ("
            + "log_jct_class_person_id int IDENTITY(1,1) PRIMARY KEY,"
            + "class_id int,"
            + "person_id int,"
            + "enum_role_id int,"
            + "log_date datetime,"
            + "log_user varchar(150),"
            + "enum_log_action_id int,"
            + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogJctClassPeriod(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_jct_class_period ("
                + "log_jct_class_period_id int IDENTITY(1,1) PRIMARY KEY,"
                + "class_id int,"
                + "enum_period_id int,"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogJctClassResource(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_jct_class_resource ("
                + "log_jct_class_resource_id int IDENTITY(1,1) PRIMARY KEY,"
                + "class_id int,"
                + "resource_id int,"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogLineItem(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_lineitem ("
                + "log_lineitem_id int IDENTITY(1,1) PRIMARY KEY,"
                + "lineitem_id int,"
                + "name varchar(150),"
                + "descript varchar(150),"
                + "assign_date date,"
                + "due_date date,"
                + "class_id int,"
                + "enum_category_id int,"
                + "markValueMin float,"
                + "markValueMax float,"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogEnrollment(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_enrollment ("
                + "log_enrollment_id int IDENTITY(1,1) PRIMARY KEY,"
                + "enrollment_id int,"
                + "person_id int,"
                + "class_id int,"
                + "enum_grade_id int,"
                + "start_date date,"
                + "end_date date,"
                + "is_primary_id int,"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogJctEnrollmentAcademicSession(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_jct_enrollment_academic_session ("
                + "log_jct_enrollment_academic_session_id int IDENTITY(1,1) PRIMARY KEY, "
                + "enrollment_id int, "
                + "academic_session_id int, "
                + "log_date datetime, "
                + "log_user varchar(150), "
                + "enum_log_action_id int, "
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLogMark(IConnectable connection)
        {
            string sql =
                "CREATE TABLE log_mark ("
                + "log_mark_id int IDENTITY(1,1) PRIMARY KEY,"
                + "mark_id int,"
                + "lineitem_id int,"
                + "person_id int,"
                + "enum_score_status_id int,"
                + "score float,"
                + "score_date date,"
                + "comment varchar(150),"
                + "created datetime,"
                + "created_by varchar(150),"
                + "changed datetime,"
                + "changed_by varchar (150),"
                + "log_date datetime,"
                + "log_user varchar(150),"
                + "enum_log_action_id int,"
                + "FOREIGN KEY (enum_log_action_id) REFERENCES enum_log_action(enum_log_action_id))";

            connection.ExecuteNonQuery(sql);
        }
    }
}