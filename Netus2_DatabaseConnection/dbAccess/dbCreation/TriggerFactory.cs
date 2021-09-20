namespace Netus2_DatabaseConnection.dbAccess.dbCreation
{
    public class TriggerFactory
    {
        public static void BuildTriggers(IConnectable connection)
        {
            BuildEnumTriggers(connection);
            BuildTableTriggers(connection);
        }

        private static void BuildEnumTriggers(IConnectable connection)
        {
            BuildUpdateEnumTriggers(connection);
            BuildDeleteEnumTriggers(connection);
        }

        private static void BuildTableTriggers(IConnectable connection)
        {
            BuildUpdateTableTriggers(connection);
            BuildInsertTableTriggers(connection);
            BuildDeleteTableTriggers(connection);
        }

        private static void BuildUpdateEnumTriggers(IConnectable connection)
        {
            BuildEnumLogAction_UpdateTrigger(connection);
            BuildEnumTrueFalse_UpdateTrigger(connection);
            BuildEnumPhone_UpdateTrigger(connection);
            BuildEnumAddress_UpdateTrigger(connection);
            BuildEnumClass_UpdateTrigger(connection);
            BuildEnumGender_UpdateTrigger(connection);
            BuildEnumImportance_UpdateTrigger(connection);
            BuildEnumOrganization_UpdateTrigger(connection);
            BuildEnumRole_UpdateTrigger(connection);
            BuildEnumScoreStatus_UpdateTrigger(connection);
            BuildEnumSession_UpdateTrigger(connection);
            BuildEnumResidenceStatus_UpdateTrigger(connection);
            BuildEnumCountry_UpdateTrigger(connection);
            BuildEnumStateProvince_UpdateTrigger(connection);
            BuildEnumGrade_UpdateTrigger(connection);
            BuildEnumSubject_UpdateTrigger(connection);
            BuildEnumPeriod_UpdateTrigger(connection);
            BuildEnumCategory_UpdateTrigger(connection);
            BuildEnumEthnic_UpdateTrigger(connection);
            BuildEnumIdentifier_UpdateTrigger(connection);
            BuildEnumSyncStatus_UpdateTrigger(connection);
        }

        private static void BuildDeleteEnumTriggers(IConnectable connection)
        {
            BuildEnumLogAction_DeleteTrigger(connection);
            BuildEnumTrueFalse_DeleteTrigger(connection);
            BuildEnumPhone_DeleteTrigger(connection);
            BuildEnumAddress_DeleteTrigger(connection);
            BuildEnumClass_DeleteTrigger(connection);
            BuildEnumGender_DeleteTrigger(connection);
            BuildEnumImportance_DeleteTrigger(connection);
            BuildEnumOrganization_DeleteTrigger(connection);
            BuildEnumRole_DeleteTrigger(connection);
            BuildEnumScoreStatus_DeleteTrigger(connection);
            BuildEnumSession_DeleteTrigger(connection);
            BuildEnumResidenceStatus_DeleteTrigger(connection);
            BuildEnumCountry_DeleteTrigger(connection);
            BuildEnumStateProvince_DeleteTrigger(connection);
            BuildEnumGrade_DeleteTrigger(connection);
            BuildEnumSubject_DeleteTrigger(connection);
            BuildEnumPeriod_DeleteTrigger(connection);
            BuildEnumCategory_DeleteTrigger(connection);
            BuildEnumEthnic_DeleteTrigger(connection);
            BuildEnumIdentifier_DeleteTrigger(connection);
            BuildEnumSyncStatus_DeleteTrigger(connection);
        }

        private static void BuildInsertTableTriggers(IConnectable connection)
        {
            BuildJctPersonPerson_InsertTrigger(connection);
        }

        private static void BuildUpdateTableTriggers(IConnectable connection)
        {
            BuildPerson_UpdateTrigger(connection);
            BuildJctPersonRole_UpdateTrigger(connection);
            BuildJctPersonPerson_UpdateTrigger(connection);
            BuildUniqueIdentifier_UpdateTrigger(connection);
            BuildProvider_UpdateTrigger(connection);
            BuildApp_UpdateTrigger(connection);
            BuildJctPersonApp_UpdateTrigger(connection);
            BuildPhoneNumber_UpdateTrigger(connection);
            BuildAddress_UpdateTrigger(connection);
            BuildJctPersonAddress_UpdateTrigger(connection);
            BuildEmploymentSession_UpdateTrigger(connection);
            BuilldAcademicSession_UpdateTrigger(connection);
            BuildOrganization_UpdateTrigger(connection);
            BuildResource_UpdateTrigger(connection);
            BuildCourse_UpdateTrigger(connection);
            BuildJctCourseSubject_UpdateTrigger(connection);
            BuildJctCourseGrade_UpdateTrigger(connection);
            BuildClass_UpdateTrigger(connection);
            BuildJctClassPerson_UpdateTrigger(connection);
            BuildJctClassPeriod_UpdateTrigger(connection);
            BuildJctClassResource_UpdateTrigger(connection);
            BuildLineItem_UpdateTrigger(connection);
            BuildEnrollment_UpdateTrigger(connection);
            BuildJctEnrollmentAcademicSession_UpdateTrigger(connection);
            BuildMark_UpdateTrigger(connection);
        }

        private static void BuildDeleteTableTriggers(IConnectable connection)
        {
            BuildPerson_DeleteTrigger(connection);
            BuildJctPersonRole_DeleteTrigger(connection);
            BuildJctPersonPerson_DeleteTrigger(connection);
            BuildUniqueIdentifier_DeleteTrigger(connection);
            BuildProvider_DeleteTrigger(connection);
            BuildApp_DeleteTrigger(connection);
            BuildJctPersonApp_DeleteTrigger(connection);
            BuildPhoneNumber_DeleteTrigger(connection);
            BuildAddress_DeleteTrigger(connection);
            BuildJctPersonAddress_DeleteTrigger(connection);
            BuildEmploymentSession_DeleteTrigger(connection);
            BuilldAcademicSession_DeleteTrigger(connection);
            BuildOrganization_DeleteTrigger(connection);
            BuildResource_DeleteTrigger(connection);
            BuildCourse_DeleteTrigger(connection);
            BuildJctCourseSubject_DeleteTrigger(connection);
            BuildJctCourseGrade_DeleteTrigger(connection);
            BuildClass_DeleteTrigger(connection);
            BuildJctClassPerson_DeleteTrigger(connection);
            BuildJctClassPeriod_DeleteTrigger(connection);
            BuildJctClassResource_DeleteTrigger(connection);
            BuildLineItem_DeleteTrigger(connection);
            BuildEnrollment_DeleteTrigger(connection);
            BuildJctEnrollmentAcademicSession_DeleteTrigger(connection);
            BuildMark_DeleteTrigger(connection);
        }

        private static void BuildEnumLogAction_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_log_action_update ON enum_log_action "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_log_action "
                + "(enum_log_action_id, netus2_code, sis_code, hr_code, descript, log_date, log_user) "
                + "SELECT d.enum_log_action_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME() "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumTrueFalse_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_true_false_update ON enum_true_false "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_true_false "
                + "(enum_true_false_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_true_false_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumPhone_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_phone_update ON enum_phone "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_phone "
                + "(enum_phone_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_phone_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumAddress_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_address_update ON enum_address "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_address "
                + "(enum_address_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_address_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumClass_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_class_update ON enum_class "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_class "
                + "(enum_class_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_class_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumGender_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_gender_update ON enum_gender "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_gender "
                + "(enum_gender_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_gender_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumImportance_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_importance_update ON enum_importance "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_importance "
                + "(enum_importance_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_importance_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }
        private static void BuildEnumOrganization_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_organization_update ON enum_organization "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_organization "
                + "(enum_organization_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_organization_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumRole_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_role_update ON enum_role "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_role "
                + "(enum_role_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id)"
                + "SELECT d.enum_role_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumScoreStatus_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_score_status_update ON enum_score_status "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_score_status "
                + "(enum_score_status_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_score_status_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumSession_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_session_update ON enum_session "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_session "
                + "(enum_session_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_session_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumResidenceStatus_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_residence_status_update ON enum_residence_status "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_residence_status "
                + "(enum_residence_status_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_residence_status_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumCountry_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_country_update ON enum_country "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_country "
                + "(enum_country_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_country_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumStateProvince_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_state_province_update ON enum_state_province "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_state_province "
                + "(enum_state_province_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_state_province_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumGrade_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_grade_update ON enum_grade "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_grade "
                + "(enum_grade_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_grade_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumSubject_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_subject_update ON enum_subject "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_subject "
                + "(enum_subject_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_subject_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumPeriod_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_period_update ON enum_period "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_period "
                + "(enum_period_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_period_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumCategory_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_category_update ON enum_category "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_category "
                + "(enum_category_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_category_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumEthnic_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_ethnic_update ON enum_ethnic "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_ethnic "
                + "(enum_ethnic_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_ethnic_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumIdentifier_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_identifier_update ON enum_identifier "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_identifier "
                + "(enum_identifier_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_identifier_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumSyncStatus_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_sync_status_update ON enum_sync_status "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_sync_status "
                + "(enum_sync_status_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_sync_status_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumLogAction_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_log_action_delete ON enum_log_action "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_log_action "
                + "(enum_log_action_id, netus2_code, sis_code, hr_code, descript, log_date, log_user) "
                + "SELECT d.enum_log_action_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME() "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumTrueFalse_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_true_false_delete ON enum_true_false "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_true_false "
                + "(enum_true_false_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_true_false_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumPhone_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_phone_delete ON enum_phone "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_phone "
                + "(enum_phone_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_phone_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumAddress_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_address_delete ON enum_address "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_address "
                + "(enum_address_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_address_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumClass_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_class_delete ON enum_class "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_class "
                + "(enum_class_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_class_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumGender_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_gender_delete ON enum_gender "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_gender "
                + "(enum_gender_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_gender_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumImportance_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_importance_delete ON enum_importance "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_importance "
                + "(enum_importance_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_importance_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumOrganization_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_organization_delete ON enum_organization "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_organization "
                + "(enum_organization_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_organization_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumRole_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_role_delete ON enum_role "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_role "
                + "(enum_role_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_role_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumScoreStatus_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_score_status_delete ON enum_score_status "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_score_status "
                + "(enum_score_status_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_score_status_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumSession_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_session_delete ON enum_session "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_session "
                + "(enum_session_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_session_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumResidenceStatus_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_residence_status_delete ON enum_residence_status "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_residence_status "
                + "(enum_residence_status_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_residence_status_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumCountry_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_country_delete ON enum_country "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_country "
                + "(enum_country_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_country_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumStateProvince_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_state_province_delete ON enum_state_province "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_state_province "
                + "(enum_state_province_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_state_province_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumGrade_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_grade_delete ON enum_grade "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_grade "
                + "(enum_grade_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_grade_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }
        private static void BuildEnumSubject_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_subject_delete ON enum_subject "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_subject "
                + "(enum_subject_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_subject_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumPeriod_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_period_delete ON enum_period "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_period "
                + "(enum_period_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_period_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumCategory_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_category_delete ON enum_category "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_category "
                + "(enum_category_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_category_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumEthnic_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_ethnic_delete ON enum_ethnic "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_ethnic "
                + "(enum_ethnic_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_ethnic_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumIdentifier_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_identifier_delete ON enum_identifier "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_identifier "
                + "(enum_identifier_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_identifier_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnumSyncStatus_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_enum_sync_status_delete ON enum_sync_status "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_enum_sync_status "
                + "(enum_sync_status_id, netus2_code, sis_code, hr_code, descript, log_date, log_user, enum_log_action_id) "
                + "SELECT d.enum_sync_status_id, d.netus2_code, d.sis_code, d.hr_code, d.descript, GETDATE(), SUSER_SNAME(), "
                + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildPerson_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_person_update ON person "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_person "
            + "(person_id, first_name, middle_name, last_name, birth_date, enum_gender_id, enum_ethnic_id, "
            + "enum_residence_status_id, login_name, login_pw, created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.person_id, d.first_name, d.middle_name, d.last_name, d.birth_date, d.enum_gender_id, "
            + "d.enum_ethnic_id, d.enum_residence_status_id, d.login_name, d.login_pw, d.created, "
            + "d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonRole_UpdateTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_jct_person_role_update ON jct_person_role "
                + "AFTER update "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_jct_person_role "
                + "(person_id, enum_role_id, log_date, log_user, enum_log_action_id)"
                + "SELECT d.person_id, d.enum_role_id, "
                + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
                + "FROM inserted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonPerson_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_person_person_update ON jct_person_person "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_person_person "
            + "(person_one_id, person_two_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.person_one_id, d.person_two_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonPerson_InsertTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_person_person_insert ON jct_person_person "
            + "AFTER insert "
            + "AS "
            + "BEGIN "
            + "INSERT INTO jct_person_person "
            + "(person_one_id, person_two_id) "
            + "SELECT d.person_two_id, d.person_one_id "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildUniqueIdentifier_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_unique_identifier_update ON unique_identifier "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_unique_identifier "
            + "(unique_identifier_id, person_id, unique_identifier, enum_identifier_id, is_active_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.unique_identifier_id, d.person_id, d.unique_identifier, d.enum_identifier_id, d.is_active_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildProvider_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_provider_update ON provider "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_provider "
            + "(provider_id, name, url_standard_access, url_admin_access, parent_provider_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.provider_id, d.name, d.url_standard_access, d.url_admin_access, "
            + "d.parent_provider_id, d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildApp_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_app_update ON app "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_app "
            + "(app_id, name, provider_id, created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.app_id, d.name, d.provider_id, d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonApp_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_person_app_update ON jct_person_app "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_person_app "
            + "(person_id, app_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.person_id, d.app_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildPhoneNumber_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_phone_number_update ON phone_number "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_phone_number "
            + "(phone_number_id, person_id, phone_number, is_primary_id, enum_phone_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.phone_number_id, d.person_id, d.phone_number, d.is_primary_id, d.enum_phone_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildAddress_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_address_update ON address "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_address "
            + "(address_id, address_line_1, address_line_2, address_line_3, address_line_4, apartment, "
            + "city, enum_state_province_id, postal_code, enum_country_id, is_current_id, enum_address_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.address_id, d.address_line_1, d.address_line_2, d.address_line_3, d.address_line_4, "
            + "d.apartment, d.city, d.enum_state_province_id, d.postal_code, d.enum_country_id, d.is_current_id, d.enum_address_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonAddress_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_person_address_update ON jct_person_address "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_person_address "
            + "(person_id, address_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.person_id, d.address_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEmploymentSession_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_employment_session_update ON employment_session "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_employment_session ("
            + "employment_session_id, name, person_id, start_date, end_date, is_primary_id, enum_session_id, organization_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.employment_session_id, d.name, d.person_id, d.start_date, d.end_date, d.is_primary_id, d.enum_session_id, d.organization_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuilldAcademicSession_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_academic_session_update ON academic_session "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_academic_session "
            + "(academic_session_id, term_code, school_year, name, start_date, end_date, enum_session_id, parent_session_id, organization_id, "
            + "created, created_by, changed, changed_by, log_date, log_user, enum_log_action_id) "
            + "SELECT d.academic_session_id,d.term_code,d.school_year,d.name,d.start_date,d.end_date,d.enum_session_id,d.parent_session_id,d.organization_id, "
            + "d.created, d.created_by, d.changed, d.changed_by,"
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildOrganization_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_organization_update ON organization "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_organization "
            + "(organization_id, name, enum_organization_id, identifier, sis_building_code, hr_building_code, organization_parent_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.organization_id, d.name, d.enum_organization_id, d.identifier, d.sis_building_code, d.hr_building_code, d.organization_parent_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildResource_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_resource_update ON resource "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_resource "
            + "(resource_id, name, enum_importance_id, vendor_resource_identification, "
            + "vendor_identification, application_identification, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.resource_id, d.name, d.enum_importance_id, d.vendor_resource_identification, "
            + "d.vendor_identification, d.application_identification, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildCourse_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_course_update ON course "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_course "
            + "(course_id, name, course_code, created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.course_id, d.name, d.course_code, d.created, d.created_by, "
            + "d.changed, d.changed_by, GETDATE(), SUSER_SNAME(), "
            + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctCourseSubject_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_course_subject_update ON jct_course_subject "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_course_subject "
            + "(course_id, enum_subject_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.course_id, d.enum_subject_id, GETDATE(), SUSER_SNAME(), "
            + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctCourseGrade_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_course_grade_update ON jct_course_grade "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_course_grade "
            + "(course_id, enum_grade_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.course_id, d.enum_grade_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildClass_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_class_update ON class "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_class "
            + "(class_id, name, class_code, enum_class_id, room, course_id, academic_session_id, "
            + "created, created_by, changed, changed_by, log_date, log_user, enum_log_action_id) "
            + "SELECT d.class_id, d.name, d.class_code, d.enum_class_id, d.room, d.course_id, "
            + "d.academic_session_id, d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctClassPerson_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_class_person_update ON jct_class_person "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_class_person "
            + "(class_id, person_id, enum_role_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.class_id, d.person_id, d.enum_role_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctClassPeriod_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_class_period_update ON jct_class_period "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_class_period "
            + "(class_id, enum_period_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.class_id, d.enum_period_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctClassResource_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_class_resource_update ON jct_class_resource "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_class_resource "
            + "(class_id, resource_id, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.class_id, d.resource_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLineItem_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_lineitem_update ON lineitem "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_lineitem "
            + "(lineitem_id, name, descript, assign_date, due_date, class_id, enum_category_id, "
            + "markValueMin, markValueMax, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.lineitem_id, d.name, d.descript, d.assign_date, d.due_date, d.class_id, d.enum_category_id, "
            + "d.markValueMin, d.markValueMax, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnrollment_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_enrollment_update ON enrollment "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_enrollment "
            + "(enrollment_id, person_id, class_id, enum_grade_id, is_primary_id, "
            + "start_date, end_date, created, created_by, changed, changed_by, log_date, log_user, enum_log_action_id) "
            + "SELECT d.enrollment_id, d.person_id, d.class_id, d.enum_grade_id, "
            + "d.is_primary_id, d.start_date, d.end_date, d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctEnrollmentAcademicSession_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_enrollment_academic_session_update ON jct_enrollment_academic_session "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_enrollment_academic_session "
            + "(enrollment_id, academic_session_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.enrollment_id, d.academic_session_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildMark_UpdateTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_mark_update ON mark "
            + "AFTER update "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_mark "
            + "(mark_id, lineitem_id, person_id, enum_score_status_id, score, score_date, comment, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.mark_id, d.lineitem_id, d.person_id, d.enum_score_status_id, d.score, d.score_date, d.comment, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'update') "
            + "FROM inserted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildPerson_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_person_delete ON person "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_person "
            + "(person_id, first_name, middle_name, last_name, birth_date, enum_gender_id, enum_ethnic_id, "
            + "enum_residence_status_id, login_name, login_pw, created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.person_id, d.first_name, d.middle_name, d.last_name, d.birth_date, d.enum_gender_id, "
            + "d.enum_ethnic_id, d.enum_residence_status_id, d.login_name, d.login_pw, d.created, "
            + "d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonRole_DeleteTrigger(IConnectable connection)
        {
            string sql =
                "CREATE TRIGGER trigger_jct_person_role_delete ON jct_person_role "
                + "AFTER delete "
                + "AS "
                + "BEGIN "
                + "INSERT INTO log_jct_person_role "
                + "(person_id, enum_role_id, log_date, log_user, enum_log_action_id)"
                + "SELECT d.person_id, d.enum_role_id, "
                + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
                + "FROM deleted d "
                + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonPerson_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_person_person_delete ON jct_person_person "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_person_person "
            + "(person_one_id, person_two_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.person_one_id, d.person_two_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "\n"
            + "DELETE FROM jct_person_person WHERE 1=1 "
            + "AND person_one_id IN (SELECT d.person_two_id FROM deleted d) "
            + "AND person_two_id IN (SELECT d.person_one_id FROM deleted d) "
            + "\n"
            + "INSERT INTO log_jct_person_person "
            + "(person_one_id, person_two_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.person_two_id, d.person_one_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildUniqueIdentifier_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_unique_identifier_delete ON unique_identifier "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_unique_identifier "
            + "(unique_identifier_id, person_id, unique_identifier, enum_identifier_id, is_active_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.unique_identifier_id, d.person_id, d.unique_identifier, d.enum_identifier_id, d.is_active_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildProvider_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_provider_delete ON provider "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_provider "
            + "(provider_id, name, url_standard_access, url_admin_access, parent_provider_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.provider_id, d.name, d.url_standard_access, d.url_admin_access, "
            + "d.parent_provider_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildApp_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_app_delete ON app "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_app "
            + "(app_id, name, provider_id, created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.app_id, d.name, d.provider_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonApp_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_person_app_delete ON jct_person_app "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_person_app "
            + "(person_id, app_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.person_id, d.app_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildPhoneNumber_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_phone_number_delete ON phone_number "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_phone_number "
            + "(phone_number_id, person_id, phone_number, is_primary_id, enum_phone_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.phone_number_id, d.person_id, d.phone_number, d.is_primary_id, d.enum_phone_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildAddress_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_address_delete ON address "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_address "
            + "(address_id, address_line_1, address_line_2, address_line_3, address_line_4, apartment, "
            + "city, enum_state_province_id, postal_code, enum_country_id, is_current_id, enum_address_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.address_id, d.address_line_1, d.address_line_2, d.address_line_3, d.address_line_4, "
            + "d.apartment, d.city, d.enum_state_province_id, d.postal_code, d.enum_country_id, d.is_current_id, d.enum_address_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctPersonAddress_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_person_address_delete ON jct_person_address "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_person_address "
            + "(person_id, address_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.person_id, d.address_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEmploymentSession_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_employment_session_delete ON employment_session "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_employment_session ("
            + "employment_session_id, name, person_id, start_date, end_date, is_primary_id, enum_session_id, organization_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.employment_session_id, d.name, d.person_id, d.start_date, d.end_date, d.is_primary_id, d.enum_session_id, d.organization_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuilldAcademicSession_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_academic_session_delete ON academic_session "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_academic_session "
            + "(academic_session_id, term_code, school_year, name, start_date, end_date, enum_session_id, parent_session_id, organization_id, "
            + "created, created_by, changed, changed_by, log_date, log_user, enum_log_action_id) "
            + "SELECT d.academic_session_id,d.term_code,d.school_year,d.name,d.start_date,d.end_date,d.enum_session_id,d.parent_session_id,d.organization_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildOrganization_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_organization_delete ON organization "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_organization "
            + "(organization_id, name, enum_organization_id, identifier, sis_building_code, hr_building_code, organization_parent_id, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.organization_id, d.name, d.enum_organization_id, d.identifier, d.sis_building_code, d.hr_building_code, d.organization_parent_id, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildResource_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_resource_delete ON resource "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_resource "
            + "(resource_id, name, enum_importance_id, vendor_resource_identification, "
            + "vendor_identification, application_identification, created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.resource_id, d.name, d.enum_importance_id, d.vendor_resource_identification, "
            + "d.vendor_identification, d.application_identification, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildCourse_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_course_delete ON course "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_course "
            + "(course_id, name, course_code, created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.course_id, d.name, d.course_code, d.created, d.created_by, "
            + "d.changed, d.changed_by, GETDATE(), SUSER_SNAME(), "
            + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctCourseSubject_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_course_subject_delete ON jct_course_subject "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_course_subject "
            + "(course_id, enum_subject_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.course_id, d.enum_subject_id, GETDATE(), SUSER_SNAME(), "
            + "(SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctCourseGrade_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_course_grade_delete ON jct_course_grade "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_course_grade "
            + "(course_id, enum_grade_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.course_id, d.enum_grade_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildClass_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_class_delete ON class "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_class "
            + "(class_id, name, class_code, enum_class_id, room, course_id, academic_session_id, "
            + "created, created_by, changed, changed_by, log_date, log_user, enum_log_action_id) "
            + "SELECT d.class_id, d.name, d.class_code, d.enum_class_id, d.room, d.course_id, "
            + "d.academic_session_id, d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctClassPerson_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_class_person_delete ON jct_class_person "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_class_person "
            + "(class_id, person_id, enum_role_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.class_id, d.person_id, d.enum_role_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctClassPeriod_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_class_period_delete ON jct_class_period "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_class_period "
            + "(class_id, enum_period_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.class_id, d.enum_period_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctClassResource_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_class_resource_delete ON jct_class_resource "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_class_resource "
            + "(class_id, resource_id, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.class_id, d.resource_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildLineItem_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_lineitem_delete ON lineitem "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_lineitem "
            + "(lineitem_id, name, descript, assign_date, due_date, class_id, enum_category_id, "
            + "markValueMin, markValueMax, created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.lineitem_id, d.name, d.descript, d.assign_date, d.due_date, d.class_id, d.enum_category_id, "
            + "d.markValueMin, d.markValueMax, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildEnrollment_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_enrollment_delete ON enrollment "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_enrollment "
            + "(enrollment_id, person_id, class_id, enum_grade_id, is_primary_id, "
            + "start_date, end_date, created, created_by, changed, changed_by, log_date, log_user, enum_log_action_id) "
            + "SELECT d.enrollment_id, d.person_id, d.class_id, d.enum_grade_id, "
            + "d.is_primary_id, d.start_date, d.end_date, d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildJctEnrollmentAcademicSession_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_jct_enrollment_academic_session_delete ON jct_enrollment_academic_session "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_jct_enrollment_academic_session "
            + "(enrollment_id, academic_session_id, log_date, log_user, enum_log_action_id) "
            + "SELECT d.enrollment_id, d.academic_session_id, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildMark_DeleteTrigger(IConnectable connection)
        {
            string sql =
            "CREATE TRIGGER trigger_mark_delete ON mark "
            + "AFTER delete "
            + "AS "
            + "BEGIN "
            + "INSERT INTO log_mark "
            + "(mark_id, lineitem_id, person_id, enum_score_status_id, score, score_date, comment, "
            + "created, created_by, changed, changed_by, "
            + "log_date, log_user, enum_log_action_id) "
            + "SELECT d.mark_id, d.lineitem_id, d.person_id, d.enum_score_status_id, d.score, d.score_date, d.comment, "
            + "d.created, d.created_by, d.changed, d.changed_by, "
            + "GETDATE(), SUSER_SNAME(), (SELECT enum_log_action_id FROM enum_log_action WHERE netus2_code LIKE 'delete') "
            + "FROM deleted d "
            + "END";

            connection.ExecuteNonQuery(sql);
        }
    }
}