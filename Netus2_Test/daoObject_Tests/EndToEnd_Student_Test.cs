using Netus2;
using Netus2.daoImplementations;
using Netus2.daoInterfaces;
using Netus2.enumerations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using static Netus2_Test.daoObject_Tests.TestDataValidator;

namespace Netus2_Test.daoObject_Tests
{
    class EndToEnd_StudentTest
    {
        Netus2DatabaseConnection connection;

        TestDataBuilder testDataBuilder;

        IPersonDao personDaoImpl;
        IAcademicSessionDao academicSessionDaoImpl;
        IClassEnrolledDao classEnrolledDaoImpl;
        IMarkDao markDaoImpl;
        ILineItemDao lineitemDaoImpl;
        IOrganizationDao organizationDaoImpl;
        ICourseDao courseDaoImpl;
        IResourceDao resourceDaoImpl;
        IProviderDao providerDaoImpl;

        [SetUp]
        public void Setup()
        {
            connection = new Netus2DatabaseConnection();
            connection.OpenConnection();
            connection.BeginTransaction();

            testDataBuilder = new TestDataBuilder(connection);

            personDaoImpl = new PersonDaoImpl();
            academicSessionDaoImpl = new AcademicSessionDaoImpl();
            classEnrolledDaoImpl = new ClassEnrolledDaoImpl();
            markDaoImpl = new MarkDaoImpl();
            lineitemDaoImpl = new LineItemDaoImpl();
            organizationDaoImpl = new OrganizationDaoImpl();
            courseDaoImpl = new CourseDaoImpl();
            resourceDaoImpl = new ResourceDaoImpl();
            providerDaoImpl = new ProviderDaoImpl();
        }

        [Test]
        public void GivenStudent_WithRelevantData_ShouldReadAndWriteToAllAppropriateTables()
        {
            Person person = testDataBuilder.student;

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", connection);
            Assert_Table(person.Applications[0].Id, 1, "app", connection);
            Assert_Table(person.Id, 1, "jct_person_app", connection);
            Assert_Table(person.Id, 1, "person", connection);
            Assert_Table(person.Id, 1, "jct_person_role", connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", connection);
            Assert_Table(person.Marks[0].Id, 1, "mark", connection);
            Assert_Table(person.Marks[0].LineItem.Id, 1, "lineitem", connection);
            Assert_Table(person.Enrollments[0].Id, 1, "enrollment", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "class", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_period", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", connection);

            List<Person> results = personDaoImpl.Read(person, connection);

            AssertPerson(person, results[0], connection);
        }

        [Test]
        public void GivenStudent_WhenStudentIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            personDaoImpl.Delete(person, connection);

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", connection);
            Assert_Table(person.Applications[0].Id, 1, "app", connection);
            Assert_Table(person.Id, 0, "jct_person_app", connection);
            Assert_Table(person.Id, 0, "person", connection);
            Assert_Table(person.Id, 0, "jct_person_role", connection);
            Assert_Table(person.PhoneNumbers[0].Id, 0, "phone_number", connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 0, "unique_identifier", connection);
            Assert_Table(person.Marks[0].Id, 0, "mark", connection);
            Assert_Table(person.Marks[0].LineItem.Id, 1, "lineitem", connection);
            Assert_Table(person.Enrollments[0].Id, 0, "enrollment", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "class", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_person", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_period", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", connection);
        }

        [Test]
        public void GivenStudent_WhenClassIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            classEnrolledDaoImpl.Delete(person.Enrollments[0].ClassEnrolled, connection);

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", connection);
            Assert_Table(person.Applications[0].Id, 1, "app", connection);
            Assert_Table(person.Id, 1, "jct_person_app", connection);
            Assert_Table(person.Id, 1, "person", connection);
            Assert_Table(person.Id, 1, "jct_person_role", connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", connection);
            Assert_Table(person.Marks[0].Id, 0, "mark", connection);
            Assert_Table(person.Marks[0].LineItem.Id, 0, "lineitem", connection);
            Assert_Table(person.Enrollments[0].Id, 0, "enrollment", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "class", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_person", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_period", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", connection);
        }

        [Test]
        public void GivenStudent_WhenResourceIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            resourceDaoImpl.Delete(person.Enrollments[0].ClassEnrolled.Resources[0], connection);

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", connection);
            Assert_Table(person.Applications[0].Id, 1, "app", connection);
            Assert_Table(person.Id, 1, "jct_person_app", connection);
            Assert_Table(person.Id, 1, "person", connection);
            Assert_Table(person.Id, 1, "jct_person_role", connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", connection);
            Assert_Table(person.Marks[0].Id, 1, "mark", connection);
            Assert_Table(person.Marks[0].LineItem.Id, 1, "lineitem", connection);
            Assert_Table(person.Enrollments[0].Id, 1, "enrollment", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "class", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_period", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 0, "resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", connection);
        }

        [Test]
        public void GivenStudent_WhenOrganizationIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            organizationDaoImpl.Delete(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization, connection);

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", connection);
            Assert_Table(person.Applications[0].Id, 1, "app", connection);
            Assert_Table(person.Id, 1, "jct_person_app", connection);
            Assert_Table(person.Id, 1, "person", connection);
            Assert_Table(person.Id, 1, "jct_person_role", connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", connection);
            Assert_Table(person.Marks[0].Id, 0, "mark", connection);
            Assert_Table(person.Marks[0].LineItem.Id, 0, "lineitem", connection);
            Assert_Table(person.Enrollments[0].Id, 0, "enrollment", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "class", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_period", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 0, "academic_session", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 0, "organization", connection);
        }

        [Test]
        public void GivenStudent_WhenCourseIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            courseDaoImpl.Delete(person.Enrollments[0].ClassEnrolled.Course, connection);

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", connection);
            Assert_Table(person.Applications[0].Id, 1, "app", connection);
            Assert_Table(person.Id, 1, "jct_person_app", connection);
            Assert_Table(person.Id, 1, "person", connection);
            Assert_Table(person.Id, 1, "jct_person_role", connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", connection);
            Assert_Table(person.Marks[0].Id, 0, "mark", connection);
            Assert_Table(person.Marks[0].LineItem.Id, 0, "lineitem", connection);
            Assert_Table(person.Enrollments[0].Id, 0, "enrollment", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "class", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_period", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 0, "course", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 0, "jct_course_subject", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 0, "jct_course_grade", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", connection);
        }

        [Test]
        public void GivenStudent_WhenProviderIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            providerDaoImpl.Delete(person.Applications[0].Provider, connection);

            Assert_Table(person.Applications[0].Provider.Id, 0, "provider", connection);
            Assert_Table(person.Applications[0].Id, 0, "app", connection);
            Assert_Table(person.Id, 0, "jct_person_app", connection);
            Assert_Table(person.Id, 1, "person", connection);
            Assert_Table(person.Id, 1, "jct_person_role", connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", connection);
            Assert_Table(person.Marks[0].Id, 1, "mark", connection);
            Assert_Table(person.Marks[0].LineItem.Id, 1, "lineitem", connection);
            Assert_Table(person.Enrollments[0].Id, 1, "enrollment", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "class", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_period", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", connection);
        }

        [Test]
        public void GivenAcademicSession_WhenChangeOrganization_ShouldUpdateRelevantTables()
        {
            AcademicSession academicSession = testDataBuilder.schoolYear;
            Organization oldOrg = academicSession.Organization;

            string name = "New Organization";
            Enumeration orgType = Enum_Organization.values["school"];
            string identifier = "New Org";
            Organization newOrg = new Organization(name, orgType, identifier);
            newOrg = organizationDaoImpl.Write(newOrg, connection);

            academicSession.Organization = newOrg;
            academicSessionDaoImpl.Update(academicSession, connection);

            organizationDaoImpl.Delete(oldOrg, connection);

            AcademicSession result = academicSessionDaoImpl.Read(academicSession, connection)[0];

            AssertAcademicSession(academicSession, result);
            Assert_Table(result.Id, 1, "academic_session", connection);
            Assert.AreNotEqual(oldOrg.Id, result.Organization.Id);
            Assert_Table(oldOrg.Id, 0, "organization", connection);
            Assert_Table(result.Organization.Id, 1, "organization", connection);
        }

        [Test]
        public void GivenClassEnrolled_WhenChangeAcademicSession_ShouldUpdateRelevantTables()
        {
            ClassEnrolled classEnrolled = testDataBuilder.classEnrolled;
            AcademicSession oldAcademicSession = classEnrolled.AcademicSession;

            string name = "New Academic Session";
            Enumeration sessionType = Enum_Session.values["school year"];
            AcademicSession newAcademicSession = new AcademicSession(name, sessionType, testDataBuilder.school);
            newAcademicSession = academicSessionDaoImpl.Write(newAcademicSession, connection);
            classEnrolled.AcademicSession = newAcademicSession;
            classEnrolledDaoImpl.Update(classEnrolled, connection);
            academicSessionDaoImpl.Delete(oldAcademicSession, connection);

            ClassEnrolled result = classEnrolledDaoImpl.Read(classEnrolled, connection)[0];
            AssertClassEnrolled(classEnrolled, result);
            Assert_Table(result.Id, 1, "class", connection);
            Assert.AreNotEqual(oldAcademicSession.Id, result.AcademicSession.Id);
            Assert_Table(oldAcademicSession.Id, 0, "academic_session", connection);
            Assert_Table(result.AcademicSession.Id, 1, "academic_session", connection);
            Assert_Table(result.AcademicSession.Organization.Id, 1, "organization", connection);
        }

        [Test]
        public void GivenStudentWithMarks_WhenMarkIsDeleted_ShouldUpdateRelevantTables()
        {
            Person person = testDataBuilder.student;
            Mark mark = testDataBuilder.mark;

            Assert_Table(mark.Id, 1, "mark", connection);

            markDaoImpl.Delete(mark, person.Id, connection);
            person.Marks.Clear();

            Assert_Table(mark.Id, 0, "mark", connection);
            AssertPerson(person, personDaoImpl.Read(person, connection)[0], connection);
        }

        [Test]
        public void GivenStudentWithMarks_WhenMarkIsChanged_ShouldUpdateRelevantTables()
        {
            Person person = testDataBuilder.student;
            LineItem lineItem = testDataBuilder.lineItem;
            Mark mark = testDataBuilder.mark;

            Assert_Table(mark.Id, 1, "mark", connection);

            Mark newMark = new Mark(testDataBuilder.lineItem, Enum_Score_Status.values["exempt"], 20.00, new DateTime());
            newMark.Score = 2.0;
            newMark = markDaoImpl.Write(newMark, person.Id, connection);
            person.Marks[0] = newMark;
            personDaoImpl.Update(person, connection);

            Person personAfterRead = personDaoImpl.Read(person, connection)[0];

            Assert.AreNotEqual(mark.Id, newMark.Id);
            Assert.AreNotEqual(mark.Id, personAfterRead.Marks[0].Id);
            Assert.AreEqual(newMark.Id, personAfterRead.Marks[0].Id);
            Assert_Table(mark.Id, 0, "mark", connection);
            Assert_Table(personAfterRead.Marks[0].Id, 1, "mark", connection);
        }

        [Test]
        public void GivenStudentWithEnrollment_WhenEnrollmentChanges_ShuldReflectInDatabase()
        {
            Person student = testDataBuilder.student;
            int oldEnrollmentId = student.Enrollments[0].Id;

            Enrollment newEnrollment = new Enrollment(Enum_Grade.values["7"], new DateTime(2022, 1, 1), new DateTime(2023, 1, 1), Enum_True_False.values["true"], testDataBuilder.classEnrolled, null);
            student.Enrollments[0] = newEnrollment;

            personDaoImpl.Update(student, connection);
            student = personDaoImpl.Read(student, connection)[0];
            Assert.IsTrue(student.Enrollments.Count == 1);
            int newEnrollmentId = student.Enrollments[0].Id;

            Assert.AreNotEqual(oldEnrollmentId, newEnrollmentId);
            Assert_Table(oldEnrollmentId, 0, "enrollment", connection);
            Assert_Table(newEnrollmentId, 1, "enrollment", connection);
        }

        [TearDown]
        public void TearDown()
        {
            connection.RollbackTransaction();
            connection.CloseConnection();
        }
    }
}
