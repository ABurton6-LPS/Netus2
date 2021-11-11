using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.enumerations;

namespace Netus2SisSync.UtilityTools
{
    class SyncScripts
    {
        public static string ReadSis_Organization_SQL = BuildScript_Sis_Organization();
        public static string ReadSiS_AcademicSession_SQL = BuildScript_Sis_AcademicSession();
        public static string ReadSis_Person_SQL = BuildScript_Sis_Person();
        public static string ReadSis_Address_SQL = BuildScript_Sis_Address();
        public static string ReadSis_JctPersonAddress_SQL = BuildScript_Sis_JctPersonAddress();
        public static string ReadSis_PhoneNumber_SQL = BuildScript_Sis_PhoneNumber();
        public static string ReadSis_JctPersonPhoneNumber_SQL = BuildScript_Sis_JctPersonPhoneNumber();
        public static string ReadSis_Course_SQL = BuildScript_Sis_Course();
        public static string ReadSis_Class_SQL = BuildScript_Sis_Class();
        public static string ReadSis_Enrollment_SQL = BuildScript_Sis_Enrollment();
        public static string ReadSis_LineItem_SQL = BuildScript_Sis_LineItem();
        public static string ReadSis_Mark_SQL = BuildScript_Sis_Mark();

        private static string BuildScript_Sis_Organization()
        {
            Config config = Netus2_DatabaseConnection.utilityTools.UtilityTools.ReadConfig(
                Enum_Config.values["sis_organization_query"], Enum_True_False.values["true"], Enum_True_False.values["true"]);

            return config.ConfigValue;
        }

        private static string BuildScript_Sis_AcademicSession()
        {
            Config config = Netus2_DatabaseConnection.utilityTools.UtilityTools.ReadConfig(
                Enum_Config.values["sis_academic_session_query"], Enum_True_False.values["true"], Enum_True_False.values["true"]);

            return config.ConfigValue;
        }

        private static string BuildScript_Sis_Person()
        {
            Config config = Netus2_DatabaseConnection.utilityTools.UtilityTools.ReadConfig(
                Enum_Config.values["sis_person_query"], Enum_True_False.values["true"], Enum_True_False.values["true"]);

            return config.ConfigValue;
        }

        private static string BuildScript_Sis_Address()
        {
            Config config = Netus2_DatabaseConnection.utilityTools.UtilityTools.ReadConfig(
                Enum_Config.values["sis_address_query"], Enum_True_False.values["true"], Enum_True_False.values["true"]);

            return config.ConfigValue;
        }

        private static string BuildScript_Sis_JctPersonAddress()
        {
            Config config = Netus2_DatabaseConnection.utilityTools.UtilityTools.ReadConfig(
                Enum_Config.values["sis_jct_person_address_query"], Enum_True_False.values["true"], Enum_True_False.values["true"]);

            return config.ConfigValue;
        }

        private static string BuildScript_Sis_PhoneNumber()
        {
            Config config = Netus2_DatabaseConnection.utilityTools.UtilityTools.ReadConfig(
                Enum_Config.values["sis_phone_number_query"], Enum_True_False.values["true"], Enum_True_False.values["true"]);

            return config.ConfigValue;
        }

        private static string BuildScript_Sis_JctPersonPhoneNumber()
        {
            Config config = Netus2_DatabaseConnection.utilityTools.UtilityTools.ReadConfig(
                Enum_Config.values["sis_jct_person_phone_number_query"], Enum_True_False.values["true"], Enum_True_False.values["true"]);

            return config.ConfigValue;
        }

        private static string BuildScript_Sis_Course()
        {
            Config config = Netus2_DatabaseConnection.utilityTools.UtilityTools.ReadConfig(
                Enum_Config.values["sis_course_query"], Enum_True_False.values["true"], Enum_True_False.values["true"]);

            return config.ConfigValue;
        }

        private static string BuildScript_Sis_Class()
        {
            return "";
        }

        private static string BuildScript_Sis_Enrollment()
        {
            return "";
        }

        private static string BuildScript_Sis_LineItem()
        {
            return "";
        }

        private static string BuildScript_Sis_Mark()
        {
            return "";
        }
    }
}