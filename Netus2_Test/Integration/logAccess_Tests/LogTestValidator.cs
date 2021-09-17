using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.logObjects;
using NUnit.Framework;
using System.Data;

namespace Netus2_Test.Integration
{
    public class LogTestValidator
    {
        public static void Assert_LogPerson(Person expected, LogPerson actual)
        {
            Assert.AreEqual(expected.Id, actual.person_id);
            Assert.AreEqual(expected.FirstName, actual.first_name);
            Assert.AreEqual(expected.MiddleName, actual.middle_name);
            Assert.AreEqual(expected.LastName, actual.last_name);
            Assert.AreEqual(expected.BirthDate, actual.birth_date);
            Assert.AreEqual(expected.Gender, actual.Gender);
            Assert.AreEqual(expected.Ethnic, actual.Ethnic);
            Assert.AreEqual(expected.ResidenceStatus, actual.ResidenceStatus);
            Assert.AreEqual(expected.LoginName, actual.login_name);
            Assert.AreEqual(expected.LoginPw, actual.login_pw);
        }

        public static void Assert_LogUniqueIdentifier(UniqueIdentifier expected, LogUniqueIdentifier actual)
        {
            Assert.AreEqual(expected.Id, actual.unique_identifier_id);
            Assert.AreEqual(expected.Identifier, actual.unique_identifier);
            Assert.AreEqual(expected.IdentifierType, actual.IdentifierType);
            Assert.AreEqual(expected.IsActive, actual.IsActive);
        }

        public static void Assert_LogProvider(Provider expected, LogProvider actual)
        {
            Assert.AreEqual(expected.Id, actual.provider_id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.UrlStandardAccess, actual.url_standard_access);
            Assert.AreEqual(expected.UrlAdminAccess, actual.url_admin_access);
        }

        public static void Assert_LogApp(Application expected, LogApp actual)
        {
            Assert.AreEqual(expected.Id, actual.app_id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.Provider.Id, actual.provider_id);
        }

        public static void Assert_LogPhoneNumber(PhoneNumber expected, LogPhoneNumber actual)
        {
            Assert.AreEqual(expected.Id, actual.phone_number_id);
            Assert.AreEqual(expected.PhoneNumberValue, actual.phone_number);
            Assert.AreEqual(expected.IsPrimary, actual.IsPrimary);
            Assert.AreEqual(expected.PhoneType, actual.PhoneType);
        }

        public static void Assert_LogAddress(Address expected, LogAddress actual)
        {
            Assert.AreEqual(expected.Id, actual.address_id);
            Assert.AreEqual(expected.Line1, actual.address_line_1);
            Assert.AreEqual(expected.Line2, actual.address_line_2);
            Assert.AreEqual(expected.Line3, actual.address_line_3);
            Assert.AreEqual(expected.Line4, actual.address_line_4);
            Assert.AreEqual(expected.Apartment, actual.apartment);
            Assert.AreEqual(expected.City, actual.city);
            Assert.AreEqual(expected.StateProvince, actual.StateProvince);
            Assert.AreEqual(expected.PostalCode, actual.postal_code);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.IsCurrent, actual.IsCurrent);
            Assert.AreEqual(expected.AddressType, actual.AddressType);
        }

        public static void Assert_LogEmploymentSession(EmploymentSession expected, LogEmploymentSession actual)
        {
            Assert.AreEqual(expected.Id, actual.employment_session_id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.StartDate, actual.start_date);
            Assert.AreEqual(expected.EndDate, actual.end_date);
            Assert.AreEqual(expected.IsPrimary, actual.IsPrimary);
            Assert.AreEqual(expected.SessionType, actual.SessionType);
            Assert.AreEqual(expected.Organization.Id, actual.organization_id);
        }

        public static void Assert_LogAcademicSession(AcademicSession expected, LogAcademicSession actual)
        {
            Assert.AreEqual(expected.Id, actual.academic_session_id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.StartDate, actual.start_date);
            Assert.AreEqual(expected.EndDate, actual.end_date);
            Assert.AreEqual(expected.SessionType, actual.SessionType);
            Assert.AreEqual(expected.Organization.Id, actual.organization_id);
        }

        public static void Assert_LogOrganization(Organization expected, LogOrganization actual)
        {
            Assert.AreEqual(expected.Id, actual.organization_id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.OrganizationType, actual.OrganizationType);
            Assert.AreEqual(expected.Identifier, actual.identifier);
        }

        public static void Assert_LogResource(Resource expeted, LogResource actual)
        {
            Assert.AreEqual(expeted.Id, actual.resource_id);
            Assert.AreEqual(expeted.Name, actual.name);
            Assert.AreEqual(expeted.Importance, actual.Importance);
            Assert.AreEqual(expeted.VendorResourceId, actual.vendor_resource_identification);
            Assert.AreEqual(expeted.VendorId, actual.vendor_identification);
            Assert.AreEqual(expeted.ApplicationId, actual.application_identification);
        }

        public static void Assert_LogCourse(Course expected, LogCourse actual)
        {
            Assert.AreEqual(expected.Id, actual.course_id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.CourseCode, actual.course_code);
        }

        public static void Assert_LogClass(ClassEnrolled expected, LogClass actual)
        {
            Assert.AreEqual(expected.Id, actual.class_id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.ClassCode, actual.class_code);
            Assert.AreEqual(expected.ClassType, actual.ClassType);
            Assert.AreEqual(expected.Room, actual.room);
            Assert.AreEqual(expected.Course.Id, actual.course_id);
            Assert.AreEqual(expected.AcademicSession.Id, actual.academic_session_id);
        }

        public static void Assert_LogLineitem(LineItem expected, LogLineItem actual)
        {
            Assert.AreEqual(expected.Id, actual.lineitem_id);
            Assert.AreEqual(expected.Name, actual.name);
            Assert.AreEqual(expected.Descript, actual.descript);
            Assert.AreEqual(expected.AssignDate, actual.assign_date);
            Assert.AreEqual(expected.DueDate, actual.due_date);
            Assert.AreEqual(expected.ClassAssigned.Id, actual.class_id);
            Assert.AreEqual(expected.Category, actual.Category);
            Assert.AreEqual(expected.MarkValueMin, actual.markValueMin);
            Assert.AreEqual(expected.MarkValueMax, actual.markValueMax);
        }

        public static void Assert_LogEnrollment(Enrollment expected, LogEnrollment actual)
        {
            Assert.AreEqual(expected.Id, actual.enrollment_id);
            Assert.AreEqual(expected.ClassEnrolled.Id, actual.class_id);
            Assert.AreEqual(expected.GradeLevel, actual.GradeLevel);
            Assert.AreEqual(expected.StartDate, actual.start_date);
            Assert.AreEqual(expected.EndDate, actual.end_date);
            Assert.AreEqual(expected.IsPrimary, actual.IsPrimary);
        }

        public static void Assert_LogMark(Mark expected, LogMark actual)
        {
            Assert.AreEqual(expected.Id, actual.mark_id);
            Assert.AreEqual(expected.LineItem.Id, actual.lineitem_id);
            Assert.AreEqual(expected.ScoreStatus, actual.ScoreStatus);
            Assert.AreEqual(expected.Score, actual.score);
            Assert.AreEqual(expected.ScoreDate, actual.score_date);
            Assert.AreEqual(expected.Comment, actual.comment);
        }

        public static void Assert_LogTable(int id, int expectedNumberOfRecords, string tableName, DataTable dt, IConnectable connection)
        {
            string sql = "";
            if (tableName.Substring(0, 7) == "log_jct")
                sql = "SELECT * FROM " + tableName + " WHERE " + tableName + "_id = " + id;
            else
                sql = "SELECT * FROM " + tableName + " WHERE " + tableName.Substring(4) + "_id = " + id;

            Assert.AreEqual(expectedNumberOfRecords, RunSql(sql, dt, connection));
        }

        private static int RunSql(string sql, DataTable dt, IConnectable connection)
        {
            dt = connection.ReadIntoDataTable(sql, dt).Result;

            int recordsFound = 0;
            foreach (DataRow row in dt.Rows)
            {
                recordsFound++;
            }

            return recordsFound;
        }
    }
}
