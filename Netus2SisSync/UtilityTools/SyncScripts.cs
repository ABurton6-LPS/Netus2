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
        public static string ReadSis_Email_SQL = BuildScript_Sis_Email();
        public static string ReadSis_JctPersonEmail_SQL = BuildScript_Sis_JctPersonEmail();
        public static string ReadSis_PhoneNumber_SQL = BuildScript_Sis_PhoneNumber();
        public static string ReadSis_JctPersonPhoneNumber_SQL = BuildScript_Sis_JctPersonPhoneNumber();
        public static string ReadSis_Course_SQL = BuildScript_Sis_Course();
        public static string ReadSis_Class_SQL = BuildScript_Sis_Class();
        public static string ReadSis_Enrollment_SQL = BuildScript_Sis_Enrollment();
        public static string ReadSis_JctEnrollmentClassEnrolled_SQL = BuildScript_Sis_JctEnrollmentClassEnrolled();
        public static string ReadSis_LineItem_SQL = BuildScript_Sis_LineItem();
        public static string ReadSis_Mark_SQL = BuildScript_Sis_Mark();

        private static string BuildScript_Sis_Organization()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("sis_organization_query", true, true).Value;
        }

        private static string BuildScript_Sis_AcademicSession()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("sis_academic_session_query", true, true).Value;
        }

        private static string BuildScript_Sis_Person()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("person_query", true, true).Value;
        }

        private static string BuildScript_Sis_Address()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("address_query", true, true).Value;
        }

        private static string BuildScript_Sis_JctPersonAddress()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("jct_person_address_query", true, true).Value;
        }

        private static string BuildScript_Sis_Email()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("email_query", true, true).Value;
        }

        private static string BuildScript_Sis_JctPersonEmail()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("jct_person_email_query", true, true).Value;
        }

        private static string BuildScript_Sis_PhoneNumber()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("phone_number_query", true, true).Value;
        }

        private static string BuildScript_Sis_JctPersonPhoneNumber()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("jct_person_phone_number_query", true, true).Value;
        }

        private static string BuildScript_Sis_Course()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("sis_course_query", true, true).Value;
        }

        private static string BuildScript_Sis_Class()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("sis_class_enrolled_query", true, true).Value;
        }

        private static string BuildScript_Sis_Enrollment()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("sis_enrollment_query", true, true).Value;
        }

        private static string BuildScript_Sis_JctEnrollmentClassEnrolled()
        {
            return Netus2_DatabaseConnection.utilityTools.UtilityTools
                .ReadConfig("sis_jct_enrollment_class_enrolled_query", true, true).Value;
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