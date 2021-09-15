namespace Netus2_DatabaseConnection.dbAccess.dbCreation
{
    public class SchemaFactory
    {
        public static void BuildSchema(IConnectable connection)
        {
            BuildEnums(connection);
            BuildTables(connection);
        }

        private static void BuildEnums(IConnectable connection)
        {
            BuildEnumLogAction(connection);
            BuildEnumSyncStatus(connection);
            BuildEnumTrueFalse(connection);
            BuildEnumPhone(connection);
            BuildEnumAddress(connection);
            BuildEnumClass(connection);
            BuildEnumGender(connection);
            BuildEnumImportance(connection);
            BuildEnumOrganization(connection);
            BuildEnumRole(connection);
            BuildEnumScoreStatus(connection);
            BuildEnumSession(connection);
            BuildEnumResidenceStatus(connection);
            BuildEnumCountry(connection);
            BuildEnumStateProvince(connection);
            BuildEnumGrade(connection);
            BuildEnumSubject(connection);
            BuildEnumPeriod(connection);
            BuildEnumCategory(connection);
            BuildEnumEthnic(connection);
            BuildEnumIdentifier(connection);
        }

        private static void BuildTables(IConnectable connection)
        {
            BuildPerson(connection);
            BuildJctPersonRole(connection);
            BuildJctPersonPerson(connection);
            BuildUniqueIdentifier(connection);
            BuildProvider(connection);
            BuildApp(connection);
            BuildJctPersonApp(connection);
            BuildPhoneNumber(connection);
            BuildAddress(connection);
            BuildJctPersonAddress(connection);
            BuildOrganization(connection);
            BuildEmploymentSession(connection);
            BuildAcademicSession(connection);
            BuildResource(connection);
            BuildCourse(connection);
            BuildJctCourseSubject(connection);
            BuildJctCourseGrade(connection);
            BuildClass(connection);
            BuildJctClassPeriod(connection);
            BuildJctClassPerson(connection);
            BuildJctClassResource(connection);
            BuildLineItem(connection);
            BuildEnrollment(connection);
            BuildJctEnrollmentAcademicSession(connection);
            BuildMark(connection);
            BuildSyncJob(connection);
            BuildSyncTask(connection);
            BuildSyncJobStatus(connection);
            BuildSyncTaskStatus(connection);
            BuildSyncError(connection);
        }

        private static void BuildEnumLogAction(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_log_action ("
                    + "enum_log_action_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumSyncStatus(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_sync_status( "
                + "enum_sync_status_id int IDENTITY(1,1) PRIMARY KEY, "
                + "netus2_code varchar(20) NOT NULL, "
                + "sis_code varchar(20), "
                + "hr_code varchar(20), "
                + "pip_code varchar(20), "
                + "descript varchar(1150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumTrueFalse(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_true_false("
                    + "enum_true_false_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumPhone(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_phone ("
                    + "enum_phone_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumAddress(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_address ("
                    + "enum_address_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumClass(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_class("
                    + "enum_class_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumGender(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_gender("
                    + "enum_gender_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumImportance(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_importance("
                    + "enum_importance_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumOrganization(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_organization("
                    + "enum_organization_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumRole(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_role("
                    + "enum_role_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumScoreStatus(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_score_status("
                    + "enum_score_status_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumSession(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_session("
                    + "enum_session_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }
        private static void BuildEnumResidenceStatus(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_residence_status("
                    + "enum_residence_status_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumCountry(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_country("
                    + "enum_country_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumStateProvince(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_state_province("
                    + "enum_state_province_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumGrade(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_grade("
                    + "enum_grade_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumSubject(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_subject("
                    + "enum_subject_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumPeriod(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_period("
                    + "enum_period_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumCategory(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_category("
                    + "enum_category_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumEthnic(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_ethnic("
                    + "enum_ethnic_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumIdentifier(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enum_identifier("
                    + "enum_identifier_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "netus2_code varchar(20) NOT NULL UNIQUE,"
                    + "sis_code varchar(20),"
                    + "hr_code varchar(20),"
                    + "pip_code varchar(20),"
                    + "descript varchar(150) NOT NULL)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildPerson(IConnectable connection)
        {
            string sql =
                "CREATE TABLE person ("
                    + "person_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "first_name varchar(30) NOT NULL,"
                    + "middle_name varchar(30),"
                    + "last_name varchar(30) NOT NULL,"
                    + "birth_date date NOT NULL,"
                    + "enum_gender_id int,"
                    + "enum_ethnic_id int,"
                    + "enum_residence_status_id int,"
                    + "login_name varchar(150),"
                    + "login_pw varchar(150),"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (enum_gender_id) REFERENCES enum_gender(enum_gender_id),"
                    + "FOREIGN KEY (enum_ethnic_id) REFERENCES enum_ethnic(enum_ethnic_id),"
                    + "FOREIGN KEY (enum_residence_status_id) REFERENCES enum_residence_status(enum_residence_status_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonRole(IConnectable connection)
        {
            string sql =
                "CREATE TABLE jct_person_role (" +
                "person_id int NOT NULL," +
                "enum_role_id int NOT NULL," +
                "PRIMARY KEY (person_id, enum_role_id)," +
                "FOREIGN KEY (person_id) REFERENCES person(person_id)," +
                "FOREIGN KEY (enum_role_id) REFERENCES enum_role(enum_role_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonPerson(IConnectable connection)
        {
            string sql =
                "CREATE TABLE jct_person_person ("
                    + "person_one_id int NOT NULL,"
                    + "person_two_id int NOT NULL,"
                    + "PRIMARY KEY (person_one_id, person_two_id),"
                    + "FOREIGN KEY (person_one_id) REFERENCES person(person_id),"
                    + "FOREIGN KEY (person_two_id) REFERENCES person(person_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildUniqueIdentifier(IConnectable connection)
        {
            string sql =
                "CREATE TABLE unique_identifier ("
                    + "unique_identifier_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "person_id int NOT NULL,"
                    + "unique_identifier varchar(150) UNIQUE NOT NULL,"
                    + "enum_identifier_id int NOT NULL,"
                    + "is_active_id int NOT NULL,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (person_id) REFERENCES person(person_id),"
                    + "FOREIGN KEY (enum_identifier_id) REFERENCES enum_identifier(enum_identifier_id),"
                    + "FOREIGN KEY (is_active_id) REFERENCES enum_true_false(enum_true_false_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildProvider(IConnectable connection)
        {
            string sql =
                "CREATE TABLE provider ("
                    + "provider_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "name varchar(150) NOT NULL,"
                    + "url_standard_access varchar(150),"
                    + "url_admin_access varchar(150),"
                    + "populated_by text,"
                    + "parent_provider_id int,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (parent_provider_id) REFERENCES provider(provider_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildApp(IConnectable connection)
        {
            string sql =
                "CREATE TABLE app ("
                    + "app_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "name varchar(150) NOT NULL,"
                    + "provider_id int NOT NULL,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (provider_id) REFERENCES provider(provider_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonApp(IConnectable connection)
        {
            string sql =
                "CREATE TABLE jct_person_app ("
                    + "person_id int NOT NULL,"
                    + "app_id int NOT NULL,"
                    + "PRIMARY KEY (person_id, app_id),"
                    + "FOREIGN KEY (person_id) REFERENCES person(person_id),"
                    + "FOREIGN KEY (app_id) REFERENCES app(app_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildPhoneNumber(IConnectable connection)
        {
            string sql =
                "CREATE TABLE phone_number ("
                    + "phone_number_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "person_id int,"
                    + "phone_number varchar(20) NOT NULL,"
                    + "is_primary_id int NOT NULL,"
                    + "enum_phone_id int NOT NULL,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (person_id) REFERENCES person(person_id),"
                    + "FOREIGN KEY (is_primary_id) REFERENCES enum_true_false(enum_true_false_id),"
                    + "FOREIGN KEY (enum_phone_id) REFERENCES enum_phone(enum_phone_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildAddress(IConnectable connection)
        {
            string sql =
                "CREATE TABLE address ("
                    + "address_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "address_line_1 varchar(150) NOT NULL,"
                    + "address_line_2 varchar(150),"
                    + "address_line_3 varchar(150),"
                    + "address_line_4 varchar(150),"
                    + "apartment varchar(15),"
                    + "city varchar(150) NOT NULL,"
                    + "enum_state_province_id int NOT NULL,"
                    + "postal_code varchar(15),"
                    + "enum_country_id int NOT NULL,"
                    + "is_current_id int NOT NULL,"
                    + "enum_address_id int NOT NULL,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (enum_state_province_id) REFERENCES enum_state_province(enum_state_province_id),"
                    + "FOREIGN KEY (enum_country_id) REFERENCES enum_country(enum_country_id),"
                    + "FOREIGN KEY (is_current_id) REFERENCES enum_true_false(enum_true_false_id),"
                    + "FOREIGN KEY (enum_address_id) REFERENCES enum_address(enum_address_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonAddress(IConnectable connection)
        {
            string sql =
                "CREATE TABLE jct_person_address ("
                + "person_id int NOT NULL,"
                + "address_id int NOT NULL,"
                + "PRIMARY KEY (person_id, address_id),"
                + "FOREIGN KEY (person_id) REFERENCES person(person_id),"
                + "FOREIGN KEY (address_id) REFERENCES address(address_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEmploymentSession(IConnectable connection)
        {
            string sql =
                "CREATE TABLE employment_session ("
                    + "employment_session_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "name varchar(150) NOT NULL,"
                    + "person_id int NOT NULL,"
                    + "start_date date NOT NULL,"
                    + "end_date date NOT NULL,"
                    + "is_primary_id int NOT NULL,"
                    + "enum_session_id int NOT NULL,"
                    + "organization_id int NOT NULL,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (person_id) REFERENCES person(person_id),"
                    + "FOREIGN KEY (is_primary_id) REFERENCES enum_true_false(enum_true_false_id),"
                    + "FOREIGN KEY (enum_session_id) REFERENCES enum_session(enum_session_id),"
                    + "FOREIGN KEY (organization_id) REFERENCES organization(organization_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildAcademicSession(IConnectable connection)
        {
            string sql =
                "CREATE TABLE academic_session ("
                    + "academic_session_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "term_code varchar(5) NOT NULL, "
                    + "school_year int, "
                    + "name varchar(150) NOT NULL,"
                    + "start_date date NOT NULL,"
                    + "end_date date NOT NULL,"
                    + "enum_session_id int NOT NULL,"
                    + "parent_session_id int,"
                    + "organization_id int NOT NULL,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (enum_session_id) REFERENCES enum_session(enum_session_id),"
                    + "FOREIGN KEY (parent_session_id) REFERENCES academic_session(academic_session_id),"
                    + "FOREIGN KEY (organization_id) REFERENCES organization(organization_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildOrganization(IConnectable connection)
        {
            string sql =
                "CREATE TABLE organization ("
                    + "organization_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "name varchar(150) NOT NULL,"
                    + "enum_organization_id int NOT NULL,"
                    + "identifier varchar(150),"
                    + "building_code varchar(150) NOT NULL UNIQUE,"
                    + "organization_parent_id int,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (enum_organization_id) REFERENCES enum_organization(enum_organization_id),"
                    + "FOREIGN KEY (organization_parent_id) REFERENCES organization(organization_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildResource(IConnectable connection)
        {
            string sql =
                "CREATE TABLE resource ("
                    + "resource_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "name varchar(150) NOT NULL,"
                    + "enum_importance_id int,"
                    + "vendor_resource_identification varchar(150) NOT NULL,"
                    + "vendor_identification varchar(150),"
                    + "application_identification varchar(150),"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (enum_importance_id) REFERENCES enum_importance(enum_importance_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildCourse(IConnectable connection)
        {
            string sql =
                "CREATE TABLE course ("
                    + "course_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "name varchar(150) NOT NULL,"
                    + "course_code varchar(150) NOT NULL,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctCourseSubject(IConnectable connection)
        {
            string sql =
                "CREATE TABLE jct_course_subject ("
                    + "course_id int NOT NULL,"
                    + "enum_subject_id int NOT NULL,"
                    + "PRIMARY KEY (course_id, enum_subject_id),"
                    + "FOREIGN KEY (course_id) REFERENCES course(course_id),"
                    + "FOREIGN KEY (enum_subject_id) REFERENCES enum_subject(enum_subject_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctCourseGrade(IConnectable connection)
        {
            string sql =
                "CREATE TABLE jct_course_grade ("
                    + "course_id int NOT NULL,"
                    + "enum_grade_id int NOT NULL,"
                    + "PRIMARY KEY (course_id, enum_grade_id),"
                    + "FOREIGN KEY (course_id) REFERENCES course(course_id),"
                    + "FOREIGN KEY (enum_grade_id) REFERENCES enum_grade(enum_grade_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildClass(IConnectable connection)
        {
            string sql =
                "CREATE TABLE class ("
                    + "class_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "name varchar(150) NOT NULL,"
                    + "class_code varchar(150) NOT NULL,"
                    + "enum_class_id int NOT NULL,"
                    + "room varchar(150) NOT NULL,"
                    + "course_id int NOT NULL,"
                    + "academic_session_id int NOT NULL,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (enum_class_id) REFERENCES enum_class(enum_class_id),"
                    + "FOREIGN KEY (course_id) REFERENCES course(course_id),"
                    + "FOREIGN KEY (academic_session_id) REFERENCES academic_session(academic_session_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctClassPerson(IConnectable connection)
        {
            string sql =
                "CREATE TABLE jct_class_person ("
                + "class_id int NOT NULL,"
                + "person_id int NOT NULL,"
                + "enum_role_id int NOT NULL,"
                + "PRIMARY KEY (class_id, person_id),"
                + "FOREIGN KEY (class_id) REFERENCES class(class_id),"
                + "FOREIGN KEY (person_id) REFERENCES person(person_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctClassPeriod(IConnectable connection)
        {
            string sql =
                "CREATE TABLE jct_class_period ("
                    + "class_id int NOT NULL,"
                    + "enum_period_id int NOT NULL,"
                    + "PRIMARY KEY (class_id, enum_period_id),"
                    + "FOREIGN KEY (class_id) REFERENCES class(class_id),"
                    + "FOREIGN KEY (enum_period_id) REFERENCES enum_period(enum_period_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctClassResource(IConnectable connection)
        {
            string sql =
                "CREATE TABLE jct_class_resource("
                    + "class_id int NOT NULL,"
                    + "resource_id int NOT NULL,"
                    + "PRIMARY KEY (class_id, resource_id),"
                    + "FOREIGN KEY (class_id) REFERENCES class(class_id),"
                    + "FOREIGN KEY (resource_id) REFERENCES resource(resource_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLineItem(IConnectable connection)
        {
            string sql =
                "CREATE TABLE lineitem ("
                    + "lineitem_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "name varchar(150) NOT NULL,"
                    + "descript varchar(150),"
                    + "assign_date date NOT NULL,"
                    + "due_date date NOT NULL,"
                    + "class_id int NOT NULL,"
                    + "enum_category_id int NOT NULL,"
                    + "markValueMin float NOT NULL,"
                    + "markValueMax float NOT NULL,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (class_id) REFERENCES class(class_id),"
                    + "FOREIGN KEY (enum_category_id) REFERENCES enum_category(enum_category_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnrollment(IConnectable connection)
        {
            string sql =
                "CREATE TABLE enrollment ("
                    + "enrollment_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "person_id int NOT NULL,"
                    + "class_id int,"
                    + "enum_grade_id int NOT NULL,"
                    + "start_date date NOT NULL,"
                    + "end_date date,"
                    + "is_primary_id int NOT NULL,"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (person_id) REFERENCES person(person_id),"
                    + "FOREIGN KEY (class_id) REFERENCES class(class_id),"
                    + "FOREIGN KEY (enum_grade_id) REFERENCES enum_grade(enum_grade_id),"
                    + "FOREIGN KEY (is_primary_id) REFERENCES enum_true_false(enum_true_false_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctEnrollmentAcademicSession(IConnectable connection)
        {
            string sql =
                "CREATE TABLE jct_enrollment_academic_session ("
                + "enrollment_id int NOT NULL, "
                + "academic_session_id int NOT NULL, "
                + "PRIMARY KEY (enrollment_id, academic_session_id), "
                + "FOREIGN KEY (enrollment_id) REFERENCES enrollment(enrollment_id), "
                + "FOREIGN KEY (academic_session_id) REFERENCES academic_session(academic_session_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildMark(IConnectable connection)
        {
            string sql =
                "CREATE TABLE mark ("
                    + "mark_id int IDENTITY(1,1) PRIMARY KEY,"
                    + "lineitem_id int NOT NULL,"
                    + "person_id int NOT NULL,"
                    + "enum_score_status_id int NOT NULL,"
                    + "score float NOT NULL,"
                    + "score_date date NOT NULL,"
                    + "comment varchar(150),"
                    + "created datetime NOT NULL,"
                    + "created_by varchar(150) NOT NULL,"
                    + "changed datetime,"
                    + "changed_by varchar (150),"
                    + "FOREIGN KEY (lineitem_id) REFERENCES lineitem(lineitem_id),"
                    + "FOREIGN KEY (person_id) REFERENCES person(person_id),"
                    + "FOREIGN KEY (enum_score_status_id) REFERENCES enum_score_status(enum_score_status_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildSyncJob(IConnectable connection)
        {
            string sql =
                "CREATE TABLE sync_job( "
                + "sync_job_id int IDENTITY(1,1) PRIMARY KEY, "
                + "[name] text NOT NULL, "
                + "[timestamp] datetime)";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildSyncTask(IConnectable connection)
        {
            string sql =
                "CREATE TABLE sync_task( "
                + "sync_task_id int IDENTITY(1,1) PRIMARY KEY, "
                + "sync_job_id int, "
                + "[name] text NOT NULL, "
                + "[timestamp] datetime, "
                + "FOREIGN KEY(sync_job_id) REFERENCES sync_job(sync_job_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildSyncJobStatus(IConnectable connection)
        {
            string sql =
                "CREATE TABLE sync_job_status( "
                + "sync_job_status_id int IDENTITY(1,1) PRIMARY KEY, "
                + "sync_job_id int, "
                + "enum_sync_status_id int, "
                + "[timestamp] datetime, "
                + "FOREIGN KEY(sync_job_id) REFERENCES sync_job(sync_job_id), "
                + "FOREIGN KEY(enum_sync_status_id) REFERENCES enum_sync_status(enum_sync_status_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildSyncTaskStatus(IConnectable connection)
        {
            string sql =
                "CREATE TABLE sync_task_status( "
                + "sync_task_status_id int IDENTITY(1,1) PRIMARY KEY, "
                + "sync_task_id int, "
                + "enum_sync_status_id int, "
                + "[timestamp] datetime, "
                + "FOREIGN KEY(sync_task_id) REFERENCES sync_task(sync_task_id), "
                + "FOREIGN KEY(enum_sync_status_id) REFERENCES enum_sync_status(enum_sync_status_id))";
            connection.ExecuteNonQuery(sql);
        }

        private static void BuildSyncError(IConnectable connection)
        {
            string sql =
                "CREATE TABLE sync_error( "
                + "sync_error_id int IDENTITY(1,1) PRIMARY KEY, "
                + "sync_job_id int, "
                + "sync_task_id int, "
                + "[message] text, "
                + "stack_trace text NOT NULL, "
                + "[timestamp] datetime, "
                + "FOREIGN KEY(sync_job_id) REFERENCES sync_job(sync_job_id), "
                + "FOREIGN KEY(sync_task_id) REFERENCES sync_task(sync_task_id))";
            connection.ExecuteNonQuery(sql);
        }
    }
}
