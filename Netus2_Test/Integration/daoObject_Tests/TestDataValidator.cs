using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;
using System.Data;

namespace Netus2_Test.Integration.daoObject_Tests
{
    class TestDataValidator
    {
        public static void AssertPerson(Person expected, Person actual, IConnectable connection)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.MiddleName, actual.MiddleName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.BirthDate, actual.BirthDate);
            Assert.AreEqual(expected.Gender, actual.Gender);
            Assert.AreEqual(expected.Ethnic, actual.Ethnic);
            Assert.AreEqual(expected.Roles.Count, actual.Roles.Count);
            Assert_Table(actual.Id, expected.Roles.Count, "jct_person_role", connection);
            for (int i = 0; i < expected.Roles.Count; i++)
            {
                Assert.AreEqual(expected.Roles[i], actual.Roles[i]);
            }
            Assert.AreEqual(expected.ResidenceStatus, actual.ResidenceStatus);
            Assert.AreEqual(expected.LoginName, actual.LoginName);
            Assert.AreEqual(expected.LoginPw, actual.LoginPw);
            Assert.AreEqual(expected.Relations.Count, actual.Relations.Count);
            Assert_Table(actual.Id, expected.Relations.Count, "jct_person_person", connection);
            for (int i = 0; i < expected.Relations.Count; i++)
            {
                Assert.AreEqual(expected.Relations[i], actual.Relations[i]);
            }
            Assert.AreEqual(expected.PhoneNumbers.Count, actual.PhoneNumbers.Count);
            for (int i = 0; i < expected.PhoneNumbers.Count; i++)
            {
                Assert_Table(actual.PhoneNumbers[i].Id, expected.PhoneNumbers.Count, "phone_number", connection);
                AssertPhoneNumber(expected.PhoneNumbers[i], actual.PhoneNumbers[i]);
            }
            Assert.AreEqual(expected.Applications.Count, actual.Applications.Count);
            for (int i = 0; i < expected.Applications.Count; i++)
            {
                Assert_Table(actual.Applications[i].Id, expected.Applications.Count, "app", connection);
                Assert_Table(actual.Id, expected.Applications.Count, "jct_person_app", connection);
                AssertApplication(expected.Applications[i], actual.Applications[i]);
            }
            Assert.AreEqual(expected.Addresses.Count, actual.Addresses.Count);
            for (int i = 0; i < expected.Addresses.Count; i++)
            {
                Assert_Table(actual.Addresses[i].Id, expected.Addresses.Count, "address", connection);
                AssertAddress(expected.Addresses[i], actual.Addresses[i]);
            }
            Assert.AreEqual(expected.UniqueIdentifiers.Count, actual.UniqueIdentifiers.Count);
            for (int i = 0; i < expected.UniqueIdentifiers.Count; i++)
            {
                Assert_Table(actual.UniqueIdentifiers[i].Id, expected.UniqueIdentifiers.Count, "unique_identifier", connection);
                AssertUniqueIdentifier(expected.UniqueIdentifiers[i], actual.UniqueIdentifiers[i]);
            }
            Assert.AreEqual(expected.Marks.Count, actual.Marks.Count);
            for (int i = 0; i < expected.Marks.Count; i++)
            {
                Assert_Table(actual.Marks[i].Id, expected.Marks.Count, "mark", connection);
                AssertMark(expected.Marks[i], actual.Marks[i]);
            }
            Assert.AreEqual(expected.Enrollments.Count, actual.Enrollments.Count);
            for (int i = 0; i < expected.Enrollments.Count; i++)
            {
                Assert_Table(actual.Enrollments[i].Id, expected.Enrollments.Count, "enrollment", connection);
                AssertEnrollment(expected.Enrollments[i], actual.Enrollments[i]);
            }
            Assert.AreEqual(expected.EmploymentSessions.Count, actual.EmploymentSessions.Count);
            for (int i = 0; i < expected.EmploymentSessions.Count; i++)
            {
                Assert_Table(actual.EmploymentSessions[i].Id, expected.EmploymentSessions.Count, "employment_session", connection);
                AssertEmploymentSession(expected.EmploymentSessions[i], actual.EmploymentSessions[i]);
            }
        }

        private static void AssertLineItem(LineItem expected, LineItem actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Descript, actual.Descript);
            Assert.AreEqual(expected.AssignDate, actual.AssignDate);
            Assert.AreEqual(expected.DueDate, actual.DueDate);
            if (expected.ClassAssigned == null)
            {
                Assert.IsNull(actual.ClassAssigned);
            }
            else
            {
                Assert.NotNull(actual.ClassAssigned);
                AssertClassEnrolled(expected.ClassAssigned, actual.ClassAssigned);
            }
            Assert.AreEqual(expected.Category, actual.Category);
            Assert.AreEqual(expected.MarkValueMin, actual.MarkValueMin);
            Assert.AreEqual(expected.MarkValueMax, actual.MarkValueMax);
        }

        public static void AssertMark(Mark expected, Mark actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            if (expected.LineItem == null)
                Assert.IsNull(actual.LineItem);
            if (expected.LineItem != null)
            {
                Assert.IsNotNull(actual.LineItem);
                AssertLineItem(expected.LineItem, actual.LineItem);
            }
            Assert.AreEqual(expected.ScoreStatus, actual.ScoreStatus);
            Assert.AreEqual(expected.Score, actual.Score);
            Assert.AreEqual(expected.ScoreDate, actual.ScoreDate);
            Assert.AreEqual(expected.Comment, actual.Comment);
        }

        public static void AssertApplication(Application expected, Application actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            if (expected.Provider == null)
                Assert.IsNull(actual.Provider);
            if (expected.Provider != null)
            {
                Assert.IsNotNull(actual.Provider);
                AssertProvider(expected.Provider, actual.Provider);
            }
        }

        public static void AssertProvider(Provider expected, Provider actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.UrlStandardAccess, actual.UrlStandardAccess);
            Assert.AreEqual(expected.UrlAdminAccess, actual.UrlAdminAccess);
            Assert.AreEqual(expected.PopulatedBy, actual.PopulatedBy);
            Assert.AreEqual(expected.Children.Count, actual.Children.Count);
            for (int i = 0; i < expected.Children.Count; i++)
            {
                AssertProvider(expected.Children[i], actual.Children[i]);
            }
        }

        public static void AssertAddress(Address expected, Address actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Line1, actual.Line1);
            Assert.AreEqual(expected.Line2, actual.Line2);
            Assert.AreEqual(expected.Line3, actual.Line3);
            Assert.AreEqual(expected.Line4, actual.Line4);
            Assert.AreEqual(expected.Apartment, actual.Apartment);
            Assert.AreEqual(expected.City, actual.City);
            Assert.AreEqual(expected.StateProvince, actual.StateProvince);
            Assert.AreEqual(expected.PostalCode, actual.PostalCode);
            Assert.AreEqual(expected.Country, actual.Country);
            Assert.AreEqual(expected.IsCurrent, actual.IsCurrent);
            Assert.AreEqual(expected.AddressType, actual.AddressType);
        }

        public static void AssertPhoneNumber(PhoneNumber expected, PhoneNumber actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.PhoneNumberValue, actual.PhoneNumberValue);
            Assert.AreEqual(expected.IsPrimary, actual.IsPrimary);
            Assert.AreEqual(expected.PhoneType, actual.PhoneType);
        }

        public static void AssertUniqueIdentifier(UniqueIdentifier expected, UniqueIdentifier actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Identifier, actual.Identifier);
            Assert.AreEqual(expected.IdentifierType, actual.IdentifierType);
            Assert.AreEqual(expected.IsActive, actual.IsActive);
        }

        public static void AssertOrganization(Organization expected, Organization actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.OrganizationType, actual.OrganizationType);
            Assert.AreEqual(expected.Identifier, actual.Identifier);
            Assert.AreEqual(expected.BuildingCode, actual.BuildingCode);
            Assert.AreEqual(expected.Children.Count, actual.Children.Count);
            for (int i = 0; i < expected.Children.Count; i++)
            {
                AssertOrganization(expected.Children[i], actual.Children[i]);
            }
        }

        public static void AssertEmploymentSession(EmploymentSession expected, EmploymentSession actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
            if (expected.Organization == null)
                Assert.IsNull(actual.Organization);
            if (expected.Organization != null)
            {
                Assert.IsNotNull(actual.Organization);
                AssertOrganization(expected.Organization, actual.Organization);
            }
            Assert.AreEqual(expected.IsPrimary, expected.IsPrimary);
            Assert.AreEqual(expected.GetSessionType(), expected.GetSessionType());
        }

        public static void AssertEnrollment(Enrollment expected, Enrollment actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            if (expected.ClassEnrolled == null)
                Assert.IsNull(actual.ClassEnrolled);
            if (expected.ClassEnrolled != null)
            {
                Assert.IsNotNull(actual.ClassEnrolled);
                AssertClassEnrolled(expected.ClassEnrolled, actual.ClassEnrolled);
            }
            Assert.AreEqual(expected.GradeLevel, actual.GradeLevel);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
            Assert.AreEqual(expected.IsPrimary, actual.IsPrimary);
        }

        public static void AssertClassEnrolled(ClassEnrolled expected, ClassEnrolled actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.ClassCode, actual.ClassCode);
            Assert.AreEqual(expected.ClassType, actual.ClassType);
            Assert.AreEqual(expected.Room, actual.Room);
            if (expected.Course == null)
                Assert.IsNull(actual.Course);
            if (expected.Course != null)
            {
                Assert.IsNotNull(actual.Course);
                AssertCourse(expected.Course, actual.Course);
            }
            if (expected.AcademicSession == null)
                Assert.IsNull(expected.AcademicSession);
            if (expected.AcademicSession != null)
            {
                Assert.IsNotNull(actual.AcademicSession);
                AssertAcademicSession(expected.AcademicSession, actual.AcademicSession);
            }
            Assert.AreEqual(expected.Periods.Count, actual.Periods.Count);
            for (int i = 0; i < expected.Periods.Count; i++)
            {
                Assert.AreEqual(expected.Periods[i], actual.Periods[i]);
            }
            Assert.AreEqual(expected.Resources.Count, actual.Resources.Count);
            for (int i = 0; i < expected.Resources.Count; i++)
            {
                AssertResource(expected.Resources[i], actual.Resources[i]);
            }
        }

        public static void AssertResource(Resource expected, Resource actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Importance, actual.Importance);
            Assert.AreEqual(expected.VendorResourceId, actual.VendorResourceId);
            Assert.AreEqual(expected.VendorId, actual.VendorId);
            Assert.AreEqual(expected.ApplicationId, actual.ApplicationId);
        }

        public static void AssertCourse(Course expected, Course actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.CourseCode, actual.CourseCode);
            Assert.AreEqual(expected.Grades.Count, actual.Grades.Count);
            for (int i = 0; i < expected.Grades.Count; i++)
            {
                Assert.AreEqual(expected.Grades[i], actual.Grades[i]);
            }
            Assert.AreEqual(expected.Subjects.Count, actual.Subjects.Count);
            for (int i = 0; i < expected.Subjects.Count; i++)
            {
                Assert.AreEqual(expected.Subjects[i], actual.Subjects[i]);
            }
        }

        public static void AssertAcademicSession(AcademicSession expected, AcademicSession actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.StartDate, actual.StartDate);
            Assert.AreEqual(expected.EndDate, actual.EndDate);
            if (expected.Organization == null)
                Assert.IsNull(actual.Organization);
            if (expected.Organization != null)
            {
                Assert.IsNotNull(actual.Organization);
                AssertOrganization(expected.Organization, actual.Organization);
            }
            Assert.AreEqual(expected.SessionType, actual.SessionType);
            for (int i = 0; i < expected.Children.Count; i++)
            {
                AssertAcademicSession(expected.Children[i], actual.Children[i]);
            }
        }

        public static void Assert_Table(int id, int expectedNumberOfRecords, string tableName, IConnectable connection)
        {
            string sql = "";

            if (tableName == "jct_person_person")
            {
                sql = "SELECT * FROM " + tableName + " WHERE (person_one_id = " + id + "OR person_two_id = " + id + ")";
                Assert.AreEqual(expectedNumberOfRecords * 2, RunSql(sql, connection));
            }
            else if (tableName.Length > 9 && tableName.Substring(0, 9) == "jct_class")
            {
                sql = "SELECT * FROM " + tableName + " WHERE class_id = " + id;
                Assert.AreEqual(expectedNumberOfRecords, RunSql(sql, connection));
            }
            else if (tableName.Length > 10 && tableName.Substring(0, 10) == "jct_course")
            {
                sql = "SELECT * FROM " + tableName + " WHERE course_id = " + id;
                Assert.AreEqual(expectedNumberOfRecords, RunSql(sql, connection));
            }
            else if (tableName.Substring(0, 3) == "jct")
            {
                sql = "SELECT * FROM " + tableName + " WHERE person_id = " + id;
                Assert.AreEqual(expectedNumberOfRecords, RunSql(sql, connection));
            }
            else
            {
                sql = "SELECT * FROM " + tableName + " WHERE " + tableName + "_id = " + id;
                Assert.AreEqual(expectedNumberOfRecords, RunSql(sql, connection));
            }
        }

        private static int RunSql(string sql, IConnectable connection)
        {
            IDataReader reader = null;
            int recordsFound = 0;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    recordsFound++;
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return recordsFound;
        }
    }
}
