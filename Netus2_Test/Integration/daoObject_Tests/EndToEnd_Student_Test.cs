using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using static Netus2_Test.Integration.TestDataValidator;

namespace Netus2_Test.Integration
{
    class EndToEnd_StudentTest
    {
        IConnectable connection;

        TestDataBuilder testDataBuilder;

        IPersonDao personDaoImpl;
        IAcademicSessionDao academicSessionDaoImpl;
        IEmploymentSessionDao employmentSessionDaoImpl;
        IClassEnrolledDao classEnrolledDaoImpl;
        IMarkDao markDaoImpl;
        IOrganizationDao organizationDaoImpl;
        ICourseDao courseDaoImpl;
        IResourceDao resourceDaoImpl;
        IProviderDao providerDaoImpl;

        [SetUp]
        public void Setup()
        {
            connection = DbConnectionFactory.GetNetus2Connection();
            connection.BeginTransaction();

            testDataBuilder = new TestDataBuilder(connection);

            organizationDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
            academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
            employmentSessionDaoImpl = DaoImplFactory.GetEmploymentSessionDaoImpl();
            personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
            courseDaoImpl = DaoImplFactory.GetCourseDaoImpl();
            classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
            resourceDaoImpl = DaoImplFactory.GetResourceDaoImpl();
            markDaoImpl = DaoImplFactory.GetMarkDaoImpl();
            providerDaoImpl = DaoImplFactory.GetProviderDaoImpl();
        }

        [Test]
        public void GivenStudent_WithRelevantData_ShouldReadAndWriteToAllAppropriateTables()
        {
            Person person = testDataBuilder.student;

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", new DataTableFactory().Dt_Netus2_Provider, connection);
            Assert_Table(person.Applications[0].Id, 1, "app", new DataTableFactory().Dt_Netus2_Application, connection);
            Assert_Table(person.Id, 1, "jct_person_app", new DataTableFactory().Dt_Netus2_JctPersonApp, connection);
            Assert_Table(person.Id, 1, "person", new DataTableFactory().Dt_Netus2_Person, connection);
            Assert_Table(person.Id, 1, "jct_person_role", new DataTableFactory().Dt_Netus2_JctPersonRole, connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", new DataTableFactory().Dt_Netus2_PhoneNumber, connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", new DataTableFactory().Dt_Netus2_Address, connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", new DataTableFactory().Dt_Netus2_UniqueIdentifier, connection);
            Assert_Table(person.Marks[0].Id, 1, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);
            Assert_Table(person.Marks[0].LineItem.Id, 1, "lineitem", new DataTableFactory().Dt_Netus2_LineItem, connection);
            Assert_Table(person.Enrollments[0].Id, 1, "enrollment", new DataTableFactory().Dt_Netus2_Enrollment, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "class", new DataTableFactory().Dt_Netus2_ClassEnrolled, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_period", new DataTableFactory().Dt_Netus2_JctClassPeriod, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_resource", new DataTableFactory().Dt_Netus2_JctClassResource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", new DataTableFactory().Dt_Netus2_Resource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", new DataTableFactory().Dt_Netus2_Course, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", new DataTableFactory().Dt_Netus2_JctCourseSubject, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", new DataTableFactory().Dt_Netus2_JctCourseGrade, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", new DataTableFactory().Dt_Netus2_AcademicSession, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", new DataTableFactory().Dt_Netus2_Organization, connection);

            List<Person> results = personDaoImpl.Read(person, connection);

            AssertPerson(person, results[0], connection);
        }

        [Test]
        public void GivenStudent_WhenStudentIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            personDaoImpl.Delete(person, connection);

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", new DataTableFactory().Dt_Netus2_Provider, connection);
            Assert_Table(person.Applications[0].Id, 1, "app", new DataTableFactory().Dt_Netus2_Application, connection);
            Assert_Table(person.Id, 0, "jct_person_app", new DataTableFactory().Dt_Netus2_JctPersonApp, connection);
            Assert_Table(person.Id, 0, "person", new DataTableFactory().Dt_Netus2_Person, connection);
            Assert_Table(person.Id, 0, "jct_person_role", new DataTableFactory().Dt_Netus2_JctPersonRole, connection);
            Assert_Table(person.PhoneNumbers[0].Id, 0, "phone_number", new DataTableFactory().Dt_Netus2_PhoneNumber, connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", new DataTableFactory().Dt_Netus2_Address, connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 0, "unique_identifier", new DataTableFactory().Dt_Netus2_UniqueIdentifier, connection);
            Assert_Table(person.Marks[0].Id, 0, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);
            Assert_Table(person.Marks[0].LineItem.Id, 1, "lineitem", new DataTableFactory().Dt_Netus2_LineItem, connection);
            Assert_Table(person.Enrollments[0].Id, 0, "enrollment", new DataTableFactory().Dt_Netus2_Enrollment, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "class", new DataTableFactory().Dt_Netus2_ClassEnrolled, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_person", new DataTableFactory().Dt_Netus2_JctClassPerson, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_period", new DataTableFactory().Dt_Netus2_JctClassPeriod, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_resource", new DataTableFactory().Dt_Netus2_JctClassResource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", new DataTableFactory().Dt_Netus2_Resource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", new DataTableFactory().Dt_Netus2_Course, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", new DataTableFactory().Dt_Netus2_JctCourseSubject, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", new DataTableFactory().Dt_Netus2_JctCourseGrade, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", new DataTableFactory().Dt_Netus2_AcademicSession, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", new DataTableFactory().Dt_Netus2_Organization, connection);
        }

        [Test]
        public void GivenStudent_WhenClassIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            classEnrolledDaoImpl.Delete(person.Enrollments[0].ClassEnrolled, connection);

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", new DataTableFactory().Dt_Netus2_Provider, connection);
            Assert_Table(person.Applications[0].Id, 1, "app", new DataTableFactory().Dt_Netus2_Application, connection);
            Assert_Table(person.Id, 1, "jct_person_app", new DataTableFactory().Dt_Netus2_JctPersonApp, connection);
            Assert_Table(person.Id, 1, "person", new DataTableFactory().Dt_Netus2_Person, connection);
            Assert_Table(person.Id, 1, "jct_person_role", new DataTableFactory().Dt_Netus2_JctPersonRole, connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", new DataTableFactory().Dt_Netus2_PhoneNumber, connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", new DataTableFactory().Dt_Netus2_Address, connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", new DataTableFactory().Dt_Netus2_UniqueIdentifier, connection);
            Assert_Table(person.Marks[0].Id, 0, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);
            Assert_Table(person.Marks[0].LineItem.Id, 0, "lineitem", new DataTableFactory().Dt_Netus2_LineItem, connection);
            Assert_Table(person.Enrollments[0].Id, 0, "enrollment", new DataTableFactory().Dt_Netus2_Enrollment, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "class", new DataTableFactory().Dt_Netus2_ClassEnrolled, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_person", new DataTableFactory().Dt_Netus2_JctClassPerson, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_period", new DataTableFactory().Dt_Netus2_JctClassPeriod, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_resource", new DataTableFactory().Dt_Netus2_JctClassResource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", new DataTableFactory().Dt_Netus2_Resource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", new DataTableFactory().Dt_Netus2_Course, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", new DataTableFactory().Dt_Netus2_JctCourseSubject, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", new DataTableFactory().Dt_Netus2_JctCourseGrade, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", new DataTableFactory().Dt_Netus2_AcademicSession, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", new DataTableFactory().Dt_Netus2_Organization, connection);
        }

        [Test]
        public void GivenStudent_WhenResourceIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            resourceDaoImpl.Delete(person.Enrollments[0].ClassEnrolled.Resources[0], connection);

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", new DataTableFactory().Dt_Netus2_Provider, connection);
            Assert_Table(person.Applications[0].Id, 1, "app", new DataTableFactory().Dt_Netus2_Application, connection);
            Assert_Table(person.Id, 1, "jct_person_app", new DataTableFactory().Dt_Netus2_JctPersonApp, connection);
            Assert_Table(person.Id, 1, "person", new DataTableFactory().Dt_Netus2_Person, connection);
            Assert_Table(person.Id, 1, "jct_person_role", new DataTableFactory().Dt_Netus2_JctPersonRole, connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", new DataTableFactory().Dt_Netus2_PhoneNumber, connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", new DataTableFactory().Dt_Netus2_Address, connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", new DataTableFactory().Dt_Netus2_UniqueIdentifier, connection);
            Assert_Table(person.Marks[0].Id, 1, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);
            Assert_Table(person.Marks[0].LineItem.Id, 1, "lineitem", new DataTableFactory().Dt_Netus2_LineItem, connection);
            Assert_Table(person.Enrollments[0].Id, 1, "enrollment", new DataTableFactory().Dt_Netus2_Enrollment, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "class", new DataTableFactory().Dt_Netus2_ClassEnrolled, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_period", new DataTableFactory().Dt_Netus2_JctClassPeriod, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_resource", new DataTableFactory().Dt_Netus2_JctClassResource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 0, "resource", new DataTableFactory().Dt_Netus2_Resource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", new DataTableFactory().Dt_Netus2_Course, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", new DataTableFactory().Dt_Netus2_JctCourseSubject, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", new DataTableFactory().Dt_Netus2_JctCourseGrade, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", new DataTableFactory().Dt_Netus2_AcademicSession, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", new DataTableFactory().Dt_Netus2_Organization, connection);
        }

        [Test]
        public void GivenStudent_WhenOrganizationIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            organizationDaoImpl.Delete(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization, connection);

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", new DataTableFactory().Dt_Netus2_Provider, connection);
            Assert_Table(person.Applications[0].Id, 1, "app", new DataTableFactory().Dt_Netus2_Application, connection);
            Assert_Table(person.Id, 1, "jct_person_app", new DataTableFactory().Dt_Netus2_JctPersonApp, connection);
            Assert_Table(person.Id, 1, "person", new DataTableFactory().Dt_Netus2_Person, connection);
            Assert_Table(person.Id, 1, "jct_person_role", new DataTableFactory().Dt_Netus2_JctPersonRole, connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", new DataTableFactory().Dt_Netus2_PhoneNumber, connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", new DataTableFactory().Dt_Netus2_Address, connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", new DataTableFactory().Dt_Netus2_UniqueIdentifier, connection);
            Assert_Table(person.Marks[0].Id, 0, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);
            Assert_Table(person.Marks[0].LineItem.Id, 0, "lineitem", new DataTableFactory().Dt_Netus2_LineItem, connection);
            Assert_Table(person.Enrollments[0].Id, 0, "enrollment", new DataTableFactory().Dt_Netus2_Enrollment, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "class", new DataTableFactory().Dt_Netus2_ClassEnrolled, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_period", new DataTableFactory().Dt_Netus2_JctClassPeriod, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_resource", new DataTableFactory().Dt_Netus2_JctClassResource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", new DataTableFactory().Dt_Netus2_Resource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", new DataTableFactory().Dt_Netus2_Course, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", new DataTableFactory().Dt_Netus2_JctCourseSubject, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", new DataTableFactory().Dt_Netus2_JctCourseGrade, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 0, "academic_session", new DataTableFactory().Dt_Netus2_AcademicSession, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 0, "organization", new DataTableFactory().Dt_Netus2_Organization, connection);
        }

        [Test]
        public void GivenStudent_WhenCourseIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            courseDaoImpl.Delete(person.Enrollments[0].ClassEnrolled.Course, connection);

            Assert_Table(person.Applications[0].Provider.Id, 1, "provider", new DataTableFactory().Dt_Netus2_Provider, connection);
            Assert_Table(person.Applications[0].Id, 1, "app", new DataTableFactory().Dt_Netus2_Application, connection);
            Assert_Table(person.Id, 1, "jct_person_app", new DataTableFactory().Dt_Netus2_JctPersonApp, connection);
            Assert_Table(person.Id, 1, "person", new DataTableFactory().Dt_Netus2_Person, connection);
            Assert_Table(person.Id, 1, "jct_person_role", new DataTableFactory().Dt_Netus2_JctPersonRole, connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", new DataTableFactory().Dt_Netus2_PhoneNumber, connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", new DataTableFactory().Dt_Netus2_Address, connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", new DataTableFactory().Dt_Netus2_UniqueIdentifier, connection);
            Assert_Table(person.Marks[0].Id, 0, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);
            Assert_Table(person.Marks[0].LineItem.Id, 0, "lineitem", new DataTableFactory().Dt_Netus2_LineItem, connection);
            Assert_Table(person.Enrollments[0].Id, 0, "enrollment", new DataTableFactory().Dt_Netus2_Enrollment, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "class", new DataTableFactory().Dt_Netus2_ClassEnrolled, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_period", new DataTableFactory().Dt_Netus2_JctClassPeriod, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 0, "jct_class_resource", new DataTableFactory().Dt_Netus2_JctClassResource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", new DataTableFactory().Dt_Netus2_Resource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 0, "course", new DataTableFactory().Dt_Netus2_Course, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 0, "jct_course_subject", new DataTableFactory().Dt_Netus2_JctCourseSubject, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 0, "jct_course_grade", new DataTableFactory().Dt_Netus2_JctCourseGrade, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", new DataTableFactory().Dt_Netus2_AcademicSession, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", new DataTableFactory().Dt_Netus2_Organization, connection);
        }

        [Test]
        public void GivenStudent_WhenProviderIsDeleted_ShouldDeleteAllRelatedRecords()
        {
            Person person = testDataBuilder.student;

            providerDaoImpl.Delete(person.Applications[0].Provider, connection);

            Assert_Table(person.Applications[0].Provider.Id, 0, "provider", new DataTableFactory().Dt_Netus2_Provider, connection);
            Assert_Table(person.Applications[0].Id, 0, "app", new DataTableFactory().Dt_Netus2_Application, connection);
            Assert_Table(person.Id, 0, "jct_person_app", new DataTableFactory().Dt_Netus2_JctPersonApp, connection);
            Assert_Table(person.Id, 1, "person", new DataTableFactory().Dt_Netus2_Person, connection);
            Assert_Table(person.Id, 1, "jct_person_role", new DataTableFactory().Dt_Netus2_JctPersonRole, connection);
            Assert_Table(person.PhoneNumbers[0].Id, 1, "phone_number", new DataTableFactory().Dt_Netus2_PhoneNumber, connection);
            Assert_Table(person.Addresses[0].Id, 1, "address", new DataTableFactory().Dt_Netus2_Address, connection);
            Assert_Table(person.UniqueIdentifiers[0].Id, 1, "unique_identifier", new DataTableFactory().Dt_Netus2_UniqueIdentifier, connection);
            Assert_Table(person.Marks[0].Id, 1, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);
            Assert_Table(person.Marks[0].LineItem.Id, 1, "lineitem", new DataTableFactory().Dt_Netus2_LineItem, connection);
            Assert_Table(person.Enrollments[0].Id, 1, "enrollment", new DataTableFactory().Dt_Netus2_Enrollment, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "class", new DataTableFactory().Dt_Netus2_ClassEnrolled, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_period", new DataTableFactory().Dt_Netus2_JctClassPeriod, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Id, 1, "jct_class_resource", new DataTableFactory().Dt_Netus2_JctClassResource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Resources[0].Id, 1, "resource", new DataTableFactory().Dt_Netus2_Resource, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "course", new DataTableFactory().Dt_Netus2_Course, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_subject", new DataTableFactory().Dt_Netus2_JctCourseSubject, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.Course.Id, 1, "jct_course_grade", new DataTableFactory().Dt_Netus2_JctCourseGrade, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Id, 1, "academic_session", new DataTableFactory().Dt_Netus2_AcademicSession, connection);
            Assert_Table(person.Enrollments[0].ClassEnrolled.AcademicSession.Organization.Id, 1, "organization", new DataTableFactory().Dt_Netus2_Organization, connection);
        }

        [Test]
        public void GivenAcademicSession_WhenChangeOrganization_ShouldUpdateRelevantTables()
        {
            AcademicSession academicSession = testDataBuilder.schoolYear;
            Organization oldOrg = academicSession.Organization;

            string name = "New Organization";
            Enumeration orgType = Enum_Organization.values["school"];
            string identifier = "New Org";
            string buildingCode = "Test Building Code";
            Organization newOrg = new Organization(name, orgType, identifier, buildingCode);
            newOrg = organizationDaoImpl.Write(newOrg, connection);

            academicSession.Organization = newOrg;
            academicSessionDaoImpl.Update(academicSession, connection);
            academicSession = academicSessionDaoImpl.Read(academicSession, connection)[0];

            AcademicSession result = academicSessionDaoImpl.Read(academicSession, connection)[0];

            AssertAcademicSession(academicSession, result);
            Assert_Table(result.Id, 1, "academic_session", new DataTableFactory().Dt_Netus2_AcademicSession, connection);
            Assert.AreNotEqual(oldOrg.Id, result.Organization.Id);
            Assert_Table(oldOrg.Id, 1, "organization", new DataTableFactory().Dt_Netus2_Organization, connection);
            Assert_Table(result.Organization.Id, 1, "organization", new DataTableFactory().Dt_Netus2_Organization, connection);
        }

        [Test]
        public void GivenClassEnrolled_WhenChangeAcademicSession_ShouldUpdateRelevantTables()
        {
            ClassEnrolled classEnrolled = testDataBuilder.classEnrolled;
            AcademicSession oldAcademicSession = classEnrolled.AcademicSession;

            string name = "New Academic Session";
            Enumeration sessionType = Enum_Session.values["school year"];
            string termCode = "T1";
            AcademicSession newAcademicSession = new AcademicSession(name, sessionType, testDataBuilder.school, termCode);
            newAcademicSession = academicSessionDaoImpl.Write(newAcademicSession, connection);
            classEnrolled.AcademicSession = newAcademicSession;
            classEnrolledDaoImpl.Update(classEnrolled, connection);
            academicSessionDaoImpl.Delete(oldAcademicSession, connection);

            ClassEnrolled result = classEnrolledDaoImpl.Read(classEnrolled, connection)[0];
            AssertClassEnrolled(classEnrolled, result);
            Assert_Table(result.Id, 1, "class", new DataTableFactory().Dt_Netus2_ClassEnrolled, connection);
            Assert.AreNotEqual(oldAcademicSession.Id, result.AcademicSession.Id);
            Assert_Table(oldAcademicSession.Id, 0, "academic_session", new DataTableFactory().Dt_Netus2_AcademicSession, connection);
            Assert_Table(result.AcademicSession.Id, 1, "academic_session", new DataTableFactory().Dt_Netus2_AcademicSession, connection);
            Assert_Table(result.AcademicSession.Organization.Id, 1, "organization", new DataTableFactory().Dt_Netus2_Organization, connection);
        }

        [Test]
        public void GivenStudentWithMarks_WhenMarkIsDeleted_ShouldUpdateRelevantTables()
        {
            Person person = testDataBuilder.student;
            Mark mark = testDataBuilder.mark;

            Assert_Table(mark.Id, 1, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);

            markDaoImpl.Delete(mark, person.Id, connection);
            person.Marks.Clear();

            Assert_Table(mark.Id, 0, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);
            AssertPerson(person, personDaoImpl.Read(person, connection)[0], connection);
        }

        [Test]
        public void GivenStudentWithMarks_WhenMarkIsChanged_ShouldUpdateRelevantTables()
        {
            Person person = testDataBuilder.student;
            LineItem lineItem = testDataBuilder.lineItem;
            Mark mark = testDataBuilder.mark;

            Assert_Table(mark.Id, 1, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);

            Mark newMark = new Mark(testDataBuilder.lineItem, Enum_Score_Status.values["exempt"], 20.00, new DateTime());
            newMark.Score = 2.0;
            newMark = markDaoImpl.Write(newMark, person.Id, connection);
            person.Marks[0] = newMark;
            personDaoImpl.Update(person, connection);

            Person personAfterRead = personDaoImpl.Read(person, connection)[0];

            Assert.AreNotEqual(mark.Id, newMark.Id);
            Assert.AreNotEqual(mark.Id, personAfterRead.Marks[0].Id);
            Assert.AreEqual(newMark.Id, personAfterRead.Marks[0].Id);
            Assert_Table(mark.Id, 0, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);
            Assert_Table(personAfterRead.Marks[0].Id, 1, "mark", new DataTableFactory().Dt_Netus2_Mark, connection);
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
            Assert_Table(oldEnrollmentId, 0, "enrollment", new DataTableFactory().Dt_Netus2_Enrollment, connection);
            Assert_Table(newEnrollmentId, 1, "enrollment", new DataTableFactory().Dt_Netus2_Enrollment, connection);
        }

        [TearDown]
        public void TearDown()
        {
            connection.RollbackTransaction();
            connection.CloseConnection();
        }
    }
}
