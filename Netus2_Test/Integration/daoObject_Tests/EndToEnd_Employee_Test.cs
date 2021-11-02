using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2_Test.utiltiyTools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using static Netus2_Test.Integration.TestDataValidator;

namespace Netus2_Test.Integration
{
    class EndToEnd_EmployeeTest
    {
        TestDataBuilder testDataBuilder;

        IConnectable connection;
        IOrganizationDao orgDaoImpl;
        IPersonDao personDaoImpl;
        IPhoneNumberDao phoneNumberDaoImpl;
        IProviderDao providerDaoImpl;
        IApplicationDao appDaoImpl;

        [SetUp]
        public void Setup()
        {
            DbConnectionFactory.environment = new MockEnvironment();

            connection = DbConnectionFactory.GetNetus2Connection();
            connection.BeginTransaction();

            testDataBuilder = new TestDataBuilder(connection);

            orgDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
            personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
            phoneNumberDaoImpl = DaoImplFactory.GetPhoneNumberDaoImpl();
            providerDaoImpl = DaoImplFactory.GetProviderDaoImpl();
            appDaoImpl = DaoImplFactory.GetApplicationDaoImpl();
        }

        [Test]
        public void GivenPerson_ShouldWriteAndRead()
        {
            Person student = testDataBuilder.student;
            Person teacher = testDataBuilder.teacher;

            List<Person> studentMarks = personDaoImpl.Read(student, connection);
            List<Person> teacherMarks = personDaoImpl.Read(teacher, connection);

            AssertPerson(student, studentMarks[0], connection);
            AssertPerson(teacher, teacherMarks[0], connection);
        }

        [Test]
        public void GivenPerson_WhenRoleIsChanged_ShouldChangeJctPersonRole()
        {
            Person student = testDataBuilder.student;
            Person teacher = testDataBuilder.teacher;

            teacher.Roles[0] = Enum_Role.values["administrator"];
            personDaoImpl.Update(teacher, connection);

            List<Person> teacherMarks = personDaoImpl.Read(teacher, connection);

            Assert_Table(teacherMarks[0].Id, 1, "jct_person_role", DataTableFactory.CreateDataTable_Netus2_JctPersonRole(), connection);
        }

        [Test]
        public void GivenPerson_WhenRoleIsAdded_ShouldBeAddedToJctPersonRole()
        {
            Person student = testDataBuilder.student;
            Person teacher = testDataBuilder.teacher;

            teacher.Roles.Add(Enum_Role.values["administrator"]);
            personDaoImpl.Update(teacher, connection);

            List<Person> teacherMarks = personDaoImpl.Read(teacher, connection);

            Assert_Table(teacherMarks[0].Id, 2, "jct_person_role", DataTableFactory.CreateDataTable_Netus2_JctPersonRole(), connection);
        }


        [Test]
        public void GivenPerson_WhenRoleIsRemoved_ShouldBeRemovedFromJctPersonRole()
        {
            Person teacher = testDataBuilder.teacher;

            Assert_Table(teacher.Id, 1, "jct_person_role", DataTableFactory.CreateDataTable_Netus2_JctPersonRole(), connection);

            teacher.Roles.RemoveAt(0);
            personDaoImpl.Update(teacher, connection);

            Assert_Table(teacher.Id, 0, "jct_person_role", DataTableFactory.CreateDataTable_Netus2_JctPersonRole(), connection);
        }


        [Test]
        public void GivenPerson_WhenRelationIsAdded_ShouldBeAddedToJctPersonPersonInBothDirections()
        {
            Person student = testDataBuilder.student;
            Person teacher = testDataBuilder.teacher;

            Assert_Table(student.Id, 1, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(teacher.Id, 1, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
        }

        [Test]
        public void GivenPerson_WhenRelationIsRemoved_ShouldDeleteJctPersonPersonRecord()
        {
            Person student = testDataBuilder.student;
            Person teacher = testDataBuilder.teacher;

            Assert_Table(student.Id, 1, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(teacher.Id, 1, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);

            teacher.Relations.Clear();
            personDaoImpl.Update(teacher, connection);

            List<Person> studentMarks = personDaoImpl.Read(student, connection);
            List<Person> teacherMarks = personDaoImpl.Read(teacher, connection);

            Assert_Table(studentMarks[0].Id, 0, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(teacherMarks[0].Id, 0, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
        }

        [Test]
        public void GivenPerson_WhenRelationIsChanged_ShuldBeChangedInJctPersonPerson()
        {
            Person student1 = testDataBuilder.student;
            Person teacher = testDataBuilder.teacher;

            Assert_Table(student1.Id, 1, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(teacher.Id, 1, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);

            Person student2 = new Person("new", "person", new DateTime(), Enum_Gender.values["female"], Enum_Ethnic.values["cau"]);
            student2 = personDaoImpl.Write(student2, connection);
            teacher.Relations[0] = student2.Id;
            personDaoImpl.Update(teacher, connection);

            List<Person> student1Marks = personDaoImpl.Read(student1, connection);
            List<Person> student2Marks = personDaoImpl.Read(student2, connection);
            List<Person> teacherMarks = personDaoImpl.Read(teacher, connection);

            Assert_Table(student1Marks[0].Id, 0, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(student2Marks[0].Id, 1, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(teacherMarks[0].Id, 1, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
        }

        [Test]
        public void GivenPerson_WhenApplicationIsAdded_ShouldAddToJctPersonApp()
        {
            Person person = testDataBuilder.teacher;

            Assert_Table(person.Id, 1, "jct_person_app", DataTableFactory.CreateDataTable_Netus2_JctPersonApp(), connection);
            AssertApplication(person.Applications[0], appDaoImpl.Read(person.Applications[0], connection)[0]);
        }

        [Test]
        public void GivenPerson_WhenApplicationIsChanged_ShouldChangeInJctPersonApp()
        {
            Person person = testDataBuilder.teacher;
            Application app = person.Applications[0];

            Assert_Table(person.Id, 1, "jct_person_app", DataTableFactory.CreateDataTable_Netus2_JctPersonApp(), connection);
            AssertApplication(person.Applications[0], app);

            person.Applications[0] = DaoImplFactory.GetApplicationDaoImpl().Write(new Application("NewTestApp", person.Applications[0].Provider), connection);
            personDaoImpl.Update(person, connection);
            person = personDaoImpl.Read(person, connection)[0];

            Assert_Table(person.Id, 1, "jct_person_app", DataTableFactory.CreateDataTable_Netus2_JctPersonApp(), connection);
            AssertApplication(person.Applications[0], appDaoImpl.Read(person.Applications[0], connection)[0]);
            AssertApplication(app, appDaoImpl.Read(app, connection)[0]);
        }

        [Test]
        public void GivenPerson_WhenApplicationIsRemoved_ShouldDeleteFromJctPersonApp()
        {
            Person person = testDataBuilder.teacher;
            Application app = person.Applications[0];

            Assert_Table(person.Id, 1, "jct_person_app", DataTableFactory.CreateDataTable_Netus2_JctPersonApp(), connection);
            AssertApplication(person.Applications[0], app);

            person.Applications.Clear();
            personDaoImpl.Update(person, connection);
            person = personDaoImpl.Read(person, connection)[0];

            Assert_Table(person.Id, 0, "jct_person_app", DataTableFactory.CreateDataTable_Netus2_JctPersonApp(), connection);
            AssertApplication(app, appDaoImpl.Read(app, connection)[0]);
        }

        [Test]
        public void GivenStudentWithTeacher_WhenTeacherIsDeleted_ShouldAlsoDeleteAllRellatedTableRecords()
        {
            Person student = testDataBuilder.student;
            Person teacher = testDataBuilder.teacher;

            int expectedNumberOfRecords = 1;
            Assert_Table(teacher.Id, expectedNumberOfRecords, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(student.Id, expectedNumberOfRecords, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(teacher.Id, expectedNumberOfRecords, "jct_person_role", DataTableFactory.CreateDataTable_Netus2_JctPersonRole(), connection);
            Assert_Table(teacher.Id, expectedNumberOfRecords, "jct_person_app", DataTableFactory.CreateDataTable_Netus2_JctPersonApp(), connection);
            Assert_Table(teacher.PhoneNumbers[0].Id, expectedNumberOfRecords, "phone_number", DataTableFactory.CreateDataTable_Netus2_PhoneNumber(), connection);
            Assert_Table(teacher.GetAddresses()[0].Id, expectedNumberOfRecords, "address", DataTableFactory.CreateDataTable_Netus2_Address(), connection);
            Assert_Table(teacher.EmploymentSessions[0].Id, expectedNumberOfRecords, "employment_session", DataTableFactory.CreateDataTable_Netus2_EmploymentSession(), connection);
            Assert_Table(teacher.UniqueIdentifiers[0].Id, expectedNumberOfRecords, "unique_identifier", DataTableFactory.CreateDataTable_Netus2_UniqueIdentifier(), connection);
            Assert_Table(teacher.Id, expectedNumberOfRecords, "jct_class_person", DataTableFactory.CreateDataTable_Netus2_JctClassPerson(), connection);


            personDaoImpl.Delete(teacher, connection);

            Assert_Table(teacher.Id, 0, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(student.Id, 0, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(teacher.Id, 0, "jct_person_role", DataTableFactory.CreateDataTable_Netus2_JctPersonRole(), connection);
            Assert_Table(teacher.Id, 0, "jct_person_app", DataTableFactory.CreateDataTable_Netus2_JctPersonApp(), connection);
            Assert_Table(teacher.PhoneNumbers[0].Id, 0, "jct_person_phone_number", DataTableFactory.CreateDataTable_Netus2_JctPersonPhoneNumber(), connection);
            Assert_Table(teacher.Id, 0, "jct_person_address", DataTableFactory.CreateDataTable_Netus2_JctPersonAddress(), connection);
            Assert_Table(teacher.PhoneNumbers[0].Id, 1, "phone_number", DataTableFactory.CreateDataTable_Netus2_PhoneNumber(), connection);
            Assert_Table(teacher.EmploymentSessions[0].Id, 0, "employment_session", DataTableFactory.CreateDataTable_Netus2_EmploymentSession(), connection);
            Assert_Table(teacher.UniqueIdentifiers[0].Id, 0, "unique_identifier", DataTableFactory.CreateDataTable_Netus2_UniqueIdentifier(), connection);
            Assert_Table(student.Id, 0, "jct_class_person", DataTableFactory.CreateDataTable_Netus2_JctClassPerson(), connection);
        }

        [Test]
        public void GivenTeacherWithStudent_WhenStudentIsDeleted_ShouldAlsoDeleteAllRellatedTableRecords()
        {
            Person student = testDataBuilder.student;
            Person teacher = testDataBuilder.teacher;

            int expectedNumberOfRecords = 1;
            Assert_Table(student.Id, expectedNumberOfRecords, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(teacher.Id, expectedNumberOfRecords, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(student.Id, expectedNumberOfRecords, "jct_person_role", DataTableFactory.CreateDataTable_Netus2_JctPersonRole(), connection);
            Assert_Table(student.Id, expectedNumberOfRecords, "jct_person_app", DataTableFactory.CreateDataTable_Netus2_JctPersonApp(), connection);
            Assert_Table(student.PhoneNumbers[0].Id, expectedNumberOfRecords, "phone_number", DataTableFactory.CreateDataTable_Netus2_PhoneNumber(), connection);
            Assert_Table(student.GetAddresses()[0].Id, expectedNumberOfRecords, "address", DataTableFactory.CreateDataTable_Netus2_Address(), connection);
            Assert_Table(student.Enrollments[0].Id, expectedNumberOfRecords, "enrollment", DataTableFactory.CreateDataTable_Netus2_Enrollment(), connection);
            Assert_Table(student.UniqueIdentifiers[0].Id, expectedNumberOfRecords, "unique_identifier", DataTableFactory.CreateDataTable_Netus2_UniqueIdentifier(), connection);
            Assert_Table(student.Marks[0].Id, expectedNumberOfRecords, "mark", DataTableFactory.CreateDataTable_Netus2_Mark(), connection);


            personDaoImpl.Delete(student, connection);

            Assert_Table(student.Id, 0, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(teacher.Id, 0, "jct_person_person", DataTableFactory.CreateDataTable_Netus2_JctPersonPerson(), connection);
            Assert_Table(student.Id, 0, "jct_person_role", DataTableFactory.CreateDataTable_Netus2_JctPersonRole(), connection);
            Assert_Table(student.Id, 0, "jct_person_app", DataTableFactory.CreateDataTable_Netus2_JctPersonApp(), connection);
            Assert_Table(student.PhoneNumbers[0].Id, 0, "jct_person_phone_number", DataTableFactory.CreateDataTable_Netus2_JctPersonPhoneNumber(), connection);
            Assert_Table(student.Id, 0, "jct_person_address", DataTableFactory.CreateDataTable_Netus2_JctPersonAddress(), connection);
            Assert_Table(student.PhoneNumbers[0].Id, 1, "phone_number", DataTableFactory.CreateDataTable_Netus2_PhoneNumber(), connection);
            Assert_Table(student.Enrollments[0].Id, 0, "enrollment", DataTableFactory.CreateDataTable_Netus2_Enrollment(), connection);
            Assert_Table(student.UniqueIdentifiers[0].Id, 0, "unique_identifier", DataTableFactory.CreateDataTable_Netus2_UniqueIdentifier(), connection);
            Assert_Table(student.Marks[0].Id, 0, "mark", DataTableFactory.CreateDataTable_Netus2_Mark(), connection);
        }

        [Test]
        public void GivenOrganization_WhenOrganizationIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.teacher;

            Assert_Table(person.EmploymentSessions[0].Id, 1, "employment_session", DataTableFactory.CreateDataTable_Netus2_EmploymentSession(), connection);
            Assert_Table(person.EmploymentSessions[0].Organization.Id, 1, "organization", DataTableFactory.CreateDataTable_Netus2_Organization(), connection);

            orgDaoImpl.Delete(person.EmploymentSessions[0].Organization, connection);

            Assert_Table(person.EmploymentSessions[0].Id, 0, "employment_session", DataTableFactory.CreateDataTable_Netus2_EmploymentSession(), connection);
            Assert_Table(person.EmploymentSessions[0].Organization.Id, 0, "organization", DataTableFactory.CreateDataTable_Netus2_Organization(), connection);
        }

        [Test]
        public void GivenProvider_WhenApplicationIsAdded_ShouldInsertNewRecordWithLinkToProvider()
        {
            Application app = testDataBuilder.application;

            Assert_Table(app.Id, 1, "app", DataTableFactory.CreateDataTable_Netus2_Application(), connection);
            Assert_Table(app.Provider.Id, 1, "provider", DataTableFactory.CreateDataTable_Netus2_Provider(), connection);
            AssertApplication(app, appDaoImpl.Read(app, connection)[0]);
        }

        [Test]
        public void GivenProvider_WhenProviderIsDeleted_ShouldAlsoDeleteApplicationAndLinkWithPerson()
        {
            Person person = testDataBuilder.teacher;

            Assert_Table(person.Id, 1, "jct_person_app", DataTableFactory.CreateDataTable_Netus2_JctPersonApp(), connection);
            Assert_Table(person.Applications[0].Id, 1, "app", DataTableFactory.CreateDataTable_Netus2_Application(), connection);
            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", DataTableFactory.CreateDataTable_Netus2_Provider(), connection);

            providerDaoImpl.Delete(person.Applications[0].Provider, connection);

            Assert_Table(person.Applications[0].Id, 0, "jct_person_app", DataTableFactory.CreateDataTable_Netus2_JctPersonApp(), connection);
            Assert_Table(person.Applications[0].Id, 0, "app", DataTableFactory.CreateDataTable_Netus2_Application(), connection);
            Assert_Table(person.Applications[0].Provider.Id, 0, "provider", DataTableFactory.CreateDataTable_Netus2_Provider(), connection);
        }

        [Test]
        public void GivenUnassignedPhoneNumber_ShouldBeAbleToReadAndWrite()
        {
            PhoneNumber phoneNumber = new PhoneNumber("1234567890", Enum_Phone.values["office"]);
            PhoneNumber phoneNumberAfterWrite = phoneNumberDaoImpl.Write(phoneNumber, connection);
            PhoneNumber phoneNumberAfterRead = phoneNumberDaoImpl.Read(phoneNumber, connection)[0];

            AssertPhoneNumber(phoneNumberAfterWrite, phoneNumberAfterRead);
        }

        [Test]
        public void GivenUnassignedPhoneNumber_ShouldBeAbleToAssignIt()
        {
            PhoneNumber phoneNumber = phoneNumberDaoImpl.Write(
                new PhoneNumber("1234567890", Enum_Phone.values["office"]), connection);

            Person student = testDataBuilder.student;
            student.PhoneNumbers.Clear();
            student.PhoneNumbers.Add(phoneNumber);
            personDaoImpl.Update(student, connection);

            List<Person> results = personDaoImpl.Read(student, connection);

            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
            Assert.AreEqual(1, results.Count);
            Assert.IsNotNull(results[0].PhoneNumbers);
            Assert.IsNotEmpty(results[0].PhoneNumbers);
            Assert.AreEqual(1, results[0].PhoneNumbers.Count);
            Assert.AreEqual(phoneNumber.Id, results[0].PhoneNumbers[0].Id);
        }

        [TearDown]
        public void TearDown()
        {
            connection.RollbackTransaction();
            connection.CloseConnection();
        }
    }
}
