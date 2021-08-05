using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.logObjects;
using NUnit.Framework;
using System.Collections.Generic;
using static Netus2_Test.logAccess_Tests.LogTestValidator;

namespace Netus2_Test.logAccess_Tests
{
    class LogReader_Test
    {
        IConnectable connection;
        LogReader logReader;
        TestDataBuilder testDataBuilder;
        IPersonDao personDaoImpl;
        IProviderDao providerDaoImpl;
        IApplicationDao applicationDaoImpl;
        IOrganizationDao orgDaoImpl;
        IResourceDao resourceDaoImpl;
        ICourseDao courseDaoImpl;
        IClassEnrolledDao classEnrolledDaoImpl;
        ILineItemDao lineItemDaoImpl;
        IMarkDao markDaoImpl;
        IEnrollmentDao enrollmentDaoImpl;
        IOrganizationDao organizationDaoImpl;
        IAcademicSessionDao academicSessionDaoImpl;
        IAddressDao addressDaoImpl;

        [SetUp]
        public void Setup()
        {
            connection = DbConnectionFactory.GetNetus2Connection();
            connection.BeginTransaction();
            testDataBuilder = new TestDataBuilder(connection);
            logReader = new LogReader();
            personDaoImpl = new PersonDaoImpl();
            providerDaoImpl = new ProviderDaoImpl();
            applicationDaoImpl = new ApplicationDaoImpl();
            orgDaoImpl = new OrganizationDaoImpl();
            resourceDaoImpl = new ResourceDaoImpl();
            courseDaoImpl = new CourseDaoImpl();
            classEnrolledDaoImpl = new ClassEnrolledDaoImpl();
            lineItemDaoImpl = new LineItemDaoImpl();
            markDaoImpl = new MarkDaoImpl();
            enrollmentDaoImpl = new EnrollmentDaoImpl();
            organizationDaoImpl = new OrganizationDaoImpl();
            academicSessionDaoImpl = new AcademicSessionDaoImpl();
            addressDaoImpl = new AddressDaoImpl();
        }

        [Test]
        public void LogPerson_Update()
        {
            int preTestLogCount = logReader.Read_LogPerson(connection).Count;

            testDataBuilder.teacher.FirstName = "John";
            personDaoImpl.Update(testDataBuilder.teacher, connection);

            List<LogPerson> logPeople = logReader.Read_LogPerson(connection);
            List<LogPerson> thisPersonsLogs = new List<LogPerson>();

            foreach (LogPerson logPerson in logPeople)
            {
                if (logPerson.person_id == testDataBuilder.teacher.Id)
                    thisPersonsLogs.Add(logPerson);
            }

            Assert.IsNotNull(logPeople);
            Assert.IsNotEmpty(logPeople);
            Assert.AreEqual(preTestLogCount + 1, logPeople.Count);

            Assert_LogTable(testDataBuilder.teacher.Id, thisPersonsLogs.Count, "log_person", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], thisPersonsLogs[thisPersonsLogs.Count - 1].LogAction);
        }

        [Test]
        public void LogPerson_Delete()
        {
            int preTestLogCount = logReader.Read_LogPerson(connection).Count;

            personDaoImpl.Delete(testDataBuilder.teacher, connection);

            List<LogPerson> logPeople = logReader.Read_LogPerson(connection);
            List<LogPerson> thisPersonsLogs = new List<LogPerson>();

            foreach (LogPerson logPerson in logPeople)
            {
                if (logPerson.person_id == testDataBuilder.teacher.Id)
                    thisPersonsLogs.Add(logPerson);
            }

            Assert.IsNotNull(logPeople);
            Assert.IsNotEmpty(logPeople);
            Assert.AreEqual(preTestLogCount + 1, logPeople.Count);

            Assert_LogTable(testDataBuilder.teacher.Id, thisPersonsLogs.Count, "log_person", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], thisPersonsLogs[thisPersonsLogs.Count - 1].LogAction);
        }

        [Test]
        public void LogJctPersonRole_Delete()
        {
            int preTestLogCount = logReader.Read_LogJctPersonRole(connection).Count;

            testDataBuilder.teacher.Roles[0] = Enum_Role.values["administrator"];
            personDaoImpl.Update(testDataBuilder.teacher, connection);

            List<LogJctPersonRole> logJctPersonRoles = logReader.Read_LogJctPersonRole(connection);

            Assert.IsNotNull(logJctPersonRoles);
            Assert.IsNotEmpty(logJctPersonRoles);
            Assert.AreEqual(preTestLogCount + 1, logJctPersonRoles.Count);

            Assert_LogTable(logJctPersonRoles[preTestLogCount].log_jct_person_role_id, 1, "log_jct_person_role", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logJctPersonRoles[preTestLogCount].LogAction);

            Assert.AreEqual(testDataBuilder.teacher.Id, logJctPersonRoles[preTestLogCount].person_id);
            Assert.AreEqual(Enum_Role.values["primary teacher"], logJctPersonRoles[preTestLogCount].Role);
        }

        [Test]
        public void LogJctPersonPersonn_Delete()
        {
            int preTestLogCount = logReader.Read_LogJctPersonPerson(connection).Count;

            testDataBuilder.teacher.Relations.Clear();
            personDaoImpl.Update(testDataBuilder.teacher, connection);

            List<LogJctPersonPerson> logJctPersonPersons = logReader.Read_LogJctPersonPerson(connection);

            Assert.IsNotNull(logJctPersonPersons);
            Assert.IsNotEmpty(logJctPersonPersons);
            Assert.AreEqual(preTestLogCount + 2, logJctPersonPersons.Count);

            Assert_LogTable(logJctPersonPersons[preTestLogCount].log_jct_person_person_id, 1, "log_jct_person_person", connection);
            Assert_LogTable(logJctPersonPersons[preTestLogCount + 1].log_jct_person_person_id, 1, "log_jct_person_person", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logJctPersonPersons[preTestLogCount].LogAction);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logJctPersonPersons[preTestLogCount + 1].LogAction);

            Assert.AreEqual(testDataBuilder.teacher.Id, logJctPersonPersons[preTestLogCount].person_one_id);
            Assert.AreEqual(testDataBuilder.student.Id, logJctPersonPersons[preTestLogCount].person_two_id);
            Assert.AreEqual(testDataBuilder.teacher.Id, logJctPersonPersons[preTestLogCount + 1].person_two_id);
            Assert.AreEqual(testDataBuilder.student.Id, logJctPersonPersons[preTestLogCount + 1].person_one_id);
        }

        [Test]
        public void LogUniqueIdentifier_Update()
        {
            int preTestLogCount = logReader.Read_LogUniqueIdentifier(connection).Count;

            testDataBuilder.teacher.UniqueIdentifiers[0].Identifier = "NewIdentifier";
            personDaoImpl.Update(testDataBuilder.teacher, connection);

            List<LogUniqueIdentifier> logUniqueIdentifiers = logReader.Read_LogUniqueIdentifier(connection);
            List<LogUniqueIdentifier> thisUniqueIdsLogs = new List<LogUniqueIdentifier>();
            foreach (LogUniqueIdentifier logUniqueIdentifier in logUniqueIdentifiers)
            {
                if (logUniqueIdentifier.unique_identifier_id == testDataBuilder.uniqueId_Teacher.Id)
                    thisUniqueIdsLogs.Add(logUniqueIdentifier);
            }

            Assert.IsNotNull(logUniqueIdentifiers);
            Assert.IsNotEmpty(logUniqueIdentifiers);
            Assert.AreEqual(preTestLogCount + 1, logUniqueIdentifiers.Count);

            Assert_LogTable(testDataBuilder.uniqueId_Teacher.Id, thisUniqueIdsLogs.Count, "log_unique_identifier", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], thisUniqueIdsLogs[thisUniqueIdsLogs.Count - 1].LogAction);

            Assert.AreEqual(testDataBuilder.teacher.Id, thisUniqueIdsLogs[thisUniqueIdsLogs.Count - 1].person_id);
        }

        [Test]
        public void LogUniqueIdentifier_Delete()
        {
            int preTestLogCount = logReader.Read_LogUniqueIdentifier(connection).Count;

            testDataBuilder.teacher.UniqueIdentifiers.Clear();
            personDaoImpl.Update(testDataBuilder.teacher, connection);

            List<LogUniqueIdentifier> logUniqueIdentifiers = logReader.Read_LogUniqueIdentifier(connection);
            List<LogUniqueIdentifier> thisUniqueIdsLogs = new List<LogUniqueIdentifier>();
            foreach(LogUniqueIdentifier logUniqueIdentifier in logUniqueIdentifiers)
            {
                if (logUniqueIdentifier.unique_identifier_id == testDataBuilder.uniqueId_Teacher.Id)
                    thisUniqueIdsLogs.Add(logUniqueIdentifier);
            }

            Assert.IsNotNull(logUniqueIdentifiers);
            Assert.IsNotEmpty(logUniqueIdentifiers);
            Assert.AreEqual(preTestLogCount + 1, logUniqueIdentifiers.Count);

            Assert_LogTable(testDataBuilder.uniqueId_Teacher.Id, thisUniqueIdsLogs.Count, "log_unique_identifier", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], thisUniqueIdsLogs[thisUniqueIdsLogs.Count - 1].LogAction);

            Assert.AreEqual(testDataBuilder.teacher.Id, thisUniqueIdsLogs[thisUniqueIdsLogs.Count - 1].person_id);
        }

        [Test]
        public void LogProvider_Update()
        {
            int preTestLogCount = logReader.Read_LogProvider(connection).Count;

            testDataBuilder.provider.Name = "New Provider Name";
            providerDaoImpl.Update(testDataBuilder.provider, connection);

            List<LogProvider> logProviders = logReader.Read_LogProvider(connection);

            Assert.IsNotNull(logProviders);
            Assert.IsNotEmpty(logProviders);
            Assert.AreEqual(preTestLogCount + 1, logProviders.Count);

            Assert_LogTable((int)logProviders[preTestLogCount].provider_id, 1, "log_provider", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logProviders[preTestLogCount].LogAction);

            Assert_LogProvider(testDataBuilder.provider, logProviders[preTestLogCount]);
        }

        [Test]
        public void LogProvider_Delete()
        {
            int preTestLogCount = logReader.Read_LogProvider(connection).Count;

            Provider oldProvider = testDataBuilder.provider;
            providerDaoImpl.Delete(testDataBuilder.provider, connection);

            List<LogProvider> logProviders = logReader.Read_LogProvider(connection);

            Assert.IsNotNull(logProviders);
            Assert.IsNotEmpty(logProviders);
            Assert.AreEqual(preTestLogCount + 1, logProviders.Count);

            Assert_LogTable((int)logProviders[preTestLogCount].provider_id, 1, "log_provider", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logProviders[preTestLogCount].LogAction);

            Assert_LogProvider(oldProvider, logProviders[preTestLogCount]);
        }

        [Test]
        public void LogApp_Update()
        {
            int preTestLogCount = logReader.Read_LogApp(connection).Count;

            Application app = testDataBuilder.application;
            app.Name = "New Application";
            applicationDaoImpl.Update(app, connection);

            List<LogApp> logApps = logReader.Read_LogApp(connection);

            Assert.IsNotNull(logApps);
            Assert.IsNotEmpty(logApps);
            Assert.AreEqual(preTestLogCount + 1, logApps.Count);

            Assert_LogTable((int)logApps[preTestLogCount].app_id, logApps.Count, "log_app", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logApps[preTestLogCount].LogAction);

            Assert_LogApp(app, logApps[preTestLogCount]);
        }

        [Test]
        public void LogJctClassPerson_Delete()
        {
            int preTestLogCount = logReader.Read_LogJctClassPerson(connection).Count;

            testDataBuilder.classEnrolled.RemoveStaff(testDataBuilder.teacher, Enum_Role.values["primary teacher"]);
            classEnrolledDaoImpl.Update(testDataBuilder.classEnrolled, connection);

            List<LogJctClassPerson> logJctClassPersons = logReader.Read_LogJctClassPerson(connection);

            Assert.IsNotNull(logJctClassPersons);
            Assert.IsNotEmpty(logJctClassPersons);
            Assert.AreEqual(preTestLogCount + 1, logJctClassPersons.Count);

            Assert_LogTable(logJctClassPersons[preTestLogCount].log_jct_class_person_id, 1, "log_jct_class_person", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logJctClassPersons[preTestLogCount].LogAction);

            Assert.AreEqual(testDataBuilder.teacher.Id, logJctClassPersons[preTestLogCount].person_id);
            Assert.AreEqual(Enum_Role.values["primary teacher"], logJctClassPersons[preTestLogCount].Role);
        }

        [Test]
        public void LogApp_Delete()
        {
            int preTestLogcount = logReader.Read_LogApp(connection).Count;

            Application app = testDataBuilder.application;
            applicationDaoImpl.Delete(app, connection);

            List<LogApp> logApps = logReader.Read_LogApp(connection);

            Assert.IsNotNull(logApps);
            Assert.IsNotEmpty(logApps);
            Assert.AreEqual(preTestLogcount + 1, logApps.Count);

            Assert_LogTable((int)logApps[preTestLogcount].app_id, logApps.Count, "log_app", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logApps[preTestLogcount].LogAction);

            Assert_LogApp(app, logApps[preTestLogcount]);
        }

        [Test]
        public void LogJctPersonApp_Delete()
        {
            int preTestLogCount = logReader.Read_LogJctPersonApp(connection).Count;

            Application oldApplication = testDataBuilder.teacher.Applications[0];
            testDataBuilder.teacher.Applications[0] =
                new Application("old application", testDataBuilder.teacher.Applications[0].Provider);
            personDaoImpl.Update(testDataBuilder.teacher, connection);

            List<LogJctPersonApp> logJctPersonApps = logReader.Read_LogJctPersonApp(connection);

            Assert.IsNotNull(logJctPersonApps);
            Assert.IsNotEmpty(logJctPersonApps);
            Assert.AreEqual(preTestLogCount + 1, logJctPersonApps.Count);

            Assert_LogTable(logJctPersonApps[preTestLogCount].log_jct_person_app_id, 1, "log_jct_person_app", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logJctPersonApps[preTestLogCount].LogAction);

            Assert.AreEqual(testDataBuilder.teacher.Id, logJctPersonApps[preTestLogCount].person_id);
            Assert.AreEqual(oldApplication.Id, logJctPersonApps[preTestLogCount].app_id);
        }

        [Test]
        public void LogPhoneNumber_Update()
        {
            int preTestLogCount = logReader.Read_LogPhoneNumber(connection).Count;

            testDataBuilder.teacher.PhoneNumbers[0].PhoneNumberValue = "8006532816";
            personDaoImpl.Update(testDataBuilder.teacher, connection);

            List<LogPhoneNumber> logPhoneNumbers = logReader.Read_LogPhoneNumber(connection);
            List<LogPhoneNumber> thisPhoneNumbersLogs = new List<LogPhoneNumber>();

            foreach (LogPhoneNumber logPhoneNumber in logPhoneNumbers)
            {
                if (logPhoneNumber.phone_number_id == testDataBuilder.phoneNumber_Teacher.Id)
                    thisPhoneNumbersLogs.Add(logPhoneNumber);
            }

            Assert.IsNotNull(logPhoneNumbers);
            Assert.IsNotEmpty(logPhoneNumbers);
            Assert.AreEqual(preTestLogCount + 1, logPhoneNumbers.Count);

            Assert_LogTable(testDataBuilder.phoneNumber_Teacher.Id, thisPhoneNumbersLogs.Count, "log_phone_number", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], thisPhoneNumbersLogs[thisPhoneNumbersLogs.Count - 1].LogAction);

            Assert.AreEqual(testDataBuilder.teacher.Id, thisPhoneNumbersLogs[thisPhoneNumbersLogs.Count - 1].person_id);
        }

        [Test]
        public void LogPhoneNumber_Delete()
        {
            int preTestLogCount = logReader.Read_LogPhoneNumber(connection).Count;

            testDataBuilder.teacher.PhoneNumbers.Clear();
            personDaoImpl.Update(testDataBuilder.teacher, connection);

            List<LogPhoneNumber> logPhoneNumbers = logReader.Read_LogPhoneNumber(connection);
            List<LogPhoneNumber> thisPhoneNumbersLogs = new List<LogPhoneNumber>();

            foreach(LogPhoneNumber logPhoneNumber in logPhoneNumbers)
            {
                if (logPhoneNumber.phone_number_id == testDataBuilder.phoneNumber_Teacher.Id)
                    thisPhoneNumbersLogs.Add(logPhoneNumber);
            }

            Assert.IsNotNull(logPhoneNumbers);
            Assert.IsNotEmpty(logPhoneNumbers);
            Assert.AreEqual(preTestLogCount + 1, logPhoneNumbers.Count);

            Assert_LogTable(testDataBuilder.phoneNumber_Teacher.Id, thisPhoneNumbersLogs.Count, "log_phone_number", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], thisPhoneNumbersLogs[thisPhoneNumbersLogs.Count - 1].LogAction);

            Assert.AreEqual(testDataBuilder.teacher.Id, thisPhoneNumbersLogs[thisPhoneNumbersLogs.Count - 1].person_id);
        }

        [Test]
        public void LogAddress_Update()
        {
            int preTestLogCount = logReader.Read_LogAddress(connection).Count;

            Address oldAddress = testDataBuilder.address_Teacher;
            oldAddress.Line1 = "New Address Line 1";
            addressDaoImpl.Update(oldAddress, connection);

            List<LogAddress> logAddresses = logReader.Read_LogAddress(connection);

            Assert.IsNotNull(logAddresses);
            Assert.IsNotEmpty(logAddresses);
            Assert.AreEqual(preTestLogCount + 1, logAddresses.Count);

            Assert_LogTable((int)logAddresses[preTestLogCount].address_id, 1, "log_address", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logAddresses[preTestLogCount].LogAction);
            Assert_LogAddress(oldAddress, logAddresses[preTestLogCount]);
        }

        [Test]
        public void LogAddress_Delete()
        {
            int preTestLogCount = logReader.Read_LogAddress(connection).Count;

            Address oldAddress = testDataBuilder.address_Teacher;
            addressDaoImpl.Delete(oldAddress, connection);

            List<LogAddress> logAddresses = logReader.Read_LogAddress(connection);

            Assert.IsNotNull(logAddresses);
            Assert.IsNotEmpty(logAddresses);
            Assert.AreEqual(preTestLogCount + 1, logAddresses.Count);

            Assert_LogTable((int)logAddresses[preTestLogCount].address_id, 1, "log_address", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logAddresses[preTestLogCount].LogAction);
            Assert_LogAddress(oldAddress, logAddresses[preTestLogCount]);
        }

        [Test]
        public void LogJctPersonAddress_Delete()
        {
            int preTestLogCount = logReader.Read_LogJctPersonAddress(connection).Count;

            Person person = testDataBuilder.student;
            Address oldAddress = testDataBuilder.address_Student;
            person.Addresses.Clear();
            personDaoImpl.Update(person, connection);

            List<LogJctPersonAddress> logJctPersonAddresss = logReader.Read_LogJctPersonAddress(connection);

            Assert.IsNotNull(logJctPersonAddresss);
            Assert.IsNotEmpty(logJctPersonAddresss);
            Assert.AreEqual(preTestLogCount + 1, logJctPersonAddresss.Count);

            Assert_LogTable(logJctPersonAddresss[preTestLogCount].log_jct_person_address_id, 1, "log_jct_person_address", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logJctPersonAddresss[preTestLogCount].LogAction);

            Assert.AreEqual(person.Id, logJctPersonAddresss[preTestLogCount].person_id);
            Assert.AreEqual(oldAddress.Id, logJctPersonAddresss[preTestLogCount].address_id);
        }

        [Test]
        public void LogEmploymentSession_Update()
        {
            int preTestLogCount = logReader.Read_LogEmploymentSession(connection).Count;

            Person teacher = testDataBuilder.teacher;
            EmploymentSession oldEmploymentSession = teacher.EmploymentSessions[0];
            teacher.EmploymentSessions[0].Name = "New Employment Session Name";
            personDaoImpl.Update(teacher, connection);

            List<LogEmploymentSession> logEmploymentSessions = logReader.Read_LogEmploymentSession(connection);

            Assert.IsNotNull(logEmploymentSessions);
            Assert.IsNotEmpty(logEmploymentSessions);
            Assert.AreEqual(preTestLogCount + 1, logEmploymentSessions.Count);

            Assert_LogTable((int)logEmploymentSessions[preTestLogCount].employment_session_id, logEmploymentSessions.Count, "log_employment_session", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logEmploymentSessions[preTestLogCount].LogAction);

            Assert.AreEqual(teacher.Id, logEmploymentSessions[preTestLogCount].person_id);
            Assert_LogEmploymentSession(oldEmploymentSession, logEmploymentSessions[preTestLogCount]);
        }

        [Test]
        public void LogEmploymentSession_Delete()
        {
            int preTestLogCount = logReader.Read_LogEmploymentSession(connection).Count;

            Person teacher = testDataBuilder.teacher;
            EmploymentSession oldEmploymentSession = teacher.EmploymentSessions[0];
            teacher.EmploymentSessions.Clear();
            personDaoImpl.Update(teacher, connection);

            List<LogEmploymentSession> logEmploymentSessions = logReader.Read_LogEmploymentSession(connection);

            Assert.IsNotNull(logEmploymentSessions);
            Assert.IsNotEmpty(logEmploymentSessions);
            Assert.AreEqual(preTestLogCount + 1, logEmploymentSessions.Count);

            Assert_LogTable((int)logEmploymentSessions[preTestLogCount].employment_session_id, logEmploymentSessions.Count, "log_employment_session", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logEmploymentSessions[preTestLogCount].LogAction);

            Assert.AreEqual(teacher.Id, logEmploymentSessions[preTestLogCount].person_id);
            Assert_LogEmploymentSession(oldEmploymentSession, logEmploymentSessions[preTestLogCount]);
        }

        [Test]
        public void LogAcademicSession_Update()
        {
            int preTestLogCount = logReader.Read_LogAcademicSession(connection).Count;

            Person student = testDataBuilder.student;
            AcademicSession oldAcademicSession = student.Enrollments[0].ClassEnrolled.AcademicSession;
            student.Enrollments[0].ClassEnrolled.AcademicSession.Name = "New Academic Session Name";
            academicSessionDaoImpl.Update(student.Enrollments[0].ClassEnrolled.AcademicSession, connection);

            List<LogAcademicSession> logAcademicSessions = logReader.Read_LogAcademicSession(connection);

            Assert.IsNotNull(logAcademicSessions);
            Assert.IsNotEmpty(logAcademicSessions);
            Assert.AreEqual(preTestLogCount + 1, logAcademicSessions.Count);

            Assert_LogTable((int)logAcademicSessions[preTestLogCount].academic_session_id, logAcademicSessions.Count, "log_academic_session", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logAcademicSessions[preTestLogCount].LogAction);

            Assert_LogAcademicSession(oldAcademicSession, logAcademicSessions[preTestLogCount]);
        }

        [Test]
        public void LogAcademicSession_Delete()
        {
            int preTestLogCount = logReader.Read_LogAcademicSession(connection).Count;

            AcademicSession academicSession = testDataBuilder.schoolYear;
            academicSessionDaoImpl.Delete(academicSession, connection);

            List<LogAcademicSession> logAcademicSessions = logReader.Read_LogAcademicSession(connection);

            Assert.IsNotNull(logAcademicSessions);
            Assert.IsNotEmpty(logAcademicSessions);
            Assert.AreEqual(preTestLogCount + 1, logAcademicSessions.Count);

            Assert_LogTable((int)logAcademicSessions[preTestLogCount].academic_session_id, 1, "log_academic_session", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logAcademicSessions[preTestLogCount].LogAction);

            Assert_LogAcademicSession(academicSession, logAcademicSessions[preTestLogCount]);
        }

        [Test]
        public void LogOrganization_Update()
        {
            int preTestLogCount = logReader.Read_LogOrganization(connection).Count;

            Organization org = testDataBuilder.school;
            org.Name = "New Organization Name";
            orgDaoImpl.Update(org, connection);

            List<LogOrganization> logOrganizations = logReader.Read_LogOrganization(connection);

            Assert.IsNotNull(logOrganizations);
            Assert.IsNotEmpty(logOrganizations);
            Assert.AreEqual(preTestLogCount + 1, logOrganizations.Count);

            Assert_LogTable((int)logOrganizations[preTestLogCount].organization_id, logOrganizations.Count, "log_organization", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logOrganizations[preTestLogCount].LogAction);

            Assert_LogOrganization(org, logOrganizations[preTestLogCount]);
        }

        [Test]
        public void LogOrganization_Delete()
        {
            int preTestLogCount = logReader.Read_LogOrganization(connection).Count;

            Organization org = testDataBuilder.school;
            organizationDaoImpl.Delete(org, connection);

            List<LogOrganization> logOrganizations = logReader.Read_LogOrganization(connection);

            Assert.IsNotNull(logOrganizations);
            Assert.IsNotEmpty(logOrganizations);
            Assert.AreEqual(preTestLogCount + 1, logOrganizations.Count);

            Assert_LogTable((int)logOrganizations[preTestLogCount].organization_id, logOrganizations.Count, "log_organization", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logOrganizations[preTestLogCount].LogAction);

            Assert_LogOrganization(org, logOrganizations[preTestLogCount]);
        }

        [Test]
        public void LogResource_Update()
        {
            int preTestLogCount = logReader.Read_LogResource(connection).Count;

            Resource oldResource = testDataBuilder.resource;
            Resource newResource = oldResource;
            newResource.Name = "different name";
            resourceDaoImpl.Update(newResource, connection); ;

            List<LogResource> logResources = logReader.Read_LogResource(connection);

            Assert.IsNotNull(logResources);
            Assert.IsNotEmpty(logResources);
            Assert.AreEqual(1, logResources.Count);

            Assert_LogTable((int)logResources[0].resource_id, 1, "log_resource", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logResources[0].LogAction);

            Assert_LogResource(oldResource, logResources[0]);
        }

        [Test]
        public void LogResource_Delete()
        {
            int preTestLogCount = logReader.Read_LogResource(connection).Count;

            Resource resource = testDataBuilder.resource;
            resourceDaoImpl.Delete(resource, connection); ;

            List<LogResource> logResources = logReader.Read_LogResource(connection);

            Assert.IsNotNull(logResources);
            Assert.IsNotEmpty(logResources);
            Assert.AreEqual(preTestLogCount + 1, logResources.Count);

            Assert_LogTable((int)logResources[preTestLogCount].resource_id, 1, "log_resource", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logResources[preTestLogCount].LogAction);

            Assert_LogResource(resource, logResources[preTestLogCount]);
        }

        [Test]
        public void LogCourse_Update()
        {
            int preTestLogCount = logReader.Read_LogCourse(connection).Count;

            Course course = testDataBuilder.spanishCourse;
            course.Name = "New Course name";
            courseDaoImpl.Update(course, connection);

            List<LogCourse> logCourses = logReader.Read_LogCourse(connection);

            Assert.IsNotNull(logCourses);
            Assert.IsNotEmpty(logCourses);
            Assert.AreEqual(preTestLogCount + 1, logCourses.Count);

            Assert_LogTable((int)logCourses[preTestLogCount].course_id, logCourses.Count, "log_course", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logCourses[preTestLogCount].LogAction);

            Assert_LogCourse(course, logCourses[preTestLogCount]);
        }

        [Test]
        public void LogCourse_Delete()
        {
            int preTestLogCount = logReader.Read_LogCourse(connection).Count;

            Course course = testDataBuilder.spanishCourse;
            courseDaoImpl.Delete(course, connection);

            List<LogCourse> logCourses = logReader.Read_LogCourse(connection);

            Assert.IsNotNull(logCourses);
            Assert.IsNotEmpty(logCourses);
            Assert.AreEqual(preTestLogCount + 1, logCourses.Count);

            Assert_LogTable((int)logCourses[preTestLogCount].course_id, logCourses.Count, "log_course", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logCourses[preTestLogCount].LogAction);

            Assert_LogCourse(course, logCourses[preTestLogCount]);
        }

        [Test]
        public void LogJctCourseSubject_Delete()
        {
            int preTestLogCount = logReader.Read_LogJctCourseSubject(connection).Count;

            Person student = testDataBuilder.student;
            student.Enrollments[0].ClassEnrolled.Course.Subjects[0] = Enum_Subject.values["sci"];
            courseDaoImpl.Update(student.Enrollments[0].ClassEnrolled.Course, connection);

            List<LogJctCourseSubject> logJctCourseSubjects = logReader.Read_LogJctCourseSubject(connection);

            Assert.IsNotNull(logJctCourseSubjects);
            Assert.IsNotEmpty(logJctCourseSubjects);
            Assert.AreEqual(preTestLogCount + 1, logJctCourseSubjects.Count);

            Assert_LogTable(logJctCourseSubjects[preTestLogCount].log_jct_course_subject_id, 1, "log_jct_course_subject", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logJctCourseSubjects[preTestLogCount].LogAction);

            Assert.AreEqual(student.Enrollments[0].ClassEnrolled.Course.Id, logJctCourseSubjects[preTestLogCount].course_id);
            Assert.AreEqual(Enum_Subject.values["spn"], logJctCourseSubjects[preTestLogCount].Subject);
        }

        [Test]
        public void LogJctCourseGrade_Delete()
        {
            int preTestLogCount = logReader.Read_LogJctCourseGrade(connection).Count;

            Person student = testDataBuilder.student;
            student.Enrollments[0].ClassEnrolled.Course.Grades[0] = Enum_Grade.values["3"];
            courseDaoImpl.Update(student.Enrollments[0].ClassEnrolled.Course, connection);

            List<LogJctCourseGrade> logJctCourseGrades = logReader.Read_LogJctCourseGrade(connection);

            Assert.IsNotNull(logJctCourseGrades);
            Assert.IsNotEmpty(logJctCourseGrades);
            Assert.AreEqual(preTestLogCount + 1, logJctCourseGrades.Count);

            Assert_LogTable(logJctCourseGrades[preTestLogCount].log_jct_course_grade_id, 1, "log_jct_course_grade", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logJctCourseGrades[preTestLogCount].LogAction);

            Assert.AreEqual(student.Enrollments[0].ClassEnrolled.Course.Id, logJctCourseGrades[preTestLogCount].course_id);
            Assert.AreEqual(Enum_Grade.values["1"], logJctCourseGrades[preTestLogCount].Grade);
        }

        [Test]
        public void LogClass_Update()
        {
            int preTestLogCount = logReader.Read_LogClass(connection).Count;

            ClassEnrolled oldClass = testDataBuilder.classEnrolled;
            ClassEnrolled newClass = oldClass;
            newClass.Name = "New Class Name";
            classEnrolledDaoImpl.Update(newClass, connection);

            List<LogClass> logClasses = logReader.Read_LogClass(connection);

            Assert.IsNotNull(logClasses);
            Assert.IsNotEmpty(logClasses);
            Assert.AreEqual(preTestLogCount + 1, logClasses.Count);

            Assert_LogTable((int)logClasses[preTestLogCount].class_id, logClasses.Count, "log_class", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logClasses[preTestLogCount].LogAction);

            Assert_LogClass(oldClass, logClasses[preTestLogCount]);
        }

        [Test]
        public void LogClass_Delete()
        {
            int preTestLogCount = logReader.Read_LogClass(connection).Count;

            ClassEnrolled oldClass = testDataBuilder.classEnrolled;
            classEnrolledDaoImpl.Delete(oldClass, connection);

            List<LogClass> logClasses = logReader.Read_LogClass(connection);

            Assert.IsNotNull(logClasses);
            Assert.IsNotEmpty(logClasses);
            Assert.AreEqual(preTestLogCount + 1, logClasses.Count);

            Assert_LogTable((int)logClasses[preTestLogCount].class_id, logClasses.Count, "log_class", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logClasses[preTestLogCount].LogAction);

            Assert_LogClass(oldClass, logClasses[preTestLogCount]);
        }

        [Test]
        public void LogJctClassPeriod_Delete()
        {
            int preTestLogCount = logReader.Read_LogJctClassPeriod(connection).Count;

            Person student = testDataBuilder.student;
            student.Enrollments[0].ClassEnrolled.Periods[0] = Enum_Period.values["2"];
            classEnrolledDaoImpl.Update(student.Enrollments[0].ClassEnrolled, connection);

            List<LogJctClassPeriod> logJctClassPeriods = logReader.Read_LogJctClassPeriod(connection);

            Assert.IsNotNull(logJctClassPeriods);
            Assert.IsNotEmpty(logJctClassPeriods);
            Assert.AreEqual(preTestLogCount + 1, logJctClassPeriods.Count);

            Assert_LogTable(logJctClassPeriods[preTestLogCount].log_jct_class_period_id, 1, "log_jct_class_period", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logJctClassPeriods[preTestLogCount].LogAction);

            Assert.AreEqual(student.Enrollments[0].ClassEnrolled.Id, logJctClassPeriods[preTestLogCount].class_id);
            Assert.AreEqual(Enum_Period.values["1"], logJctClassPeriods[preTestLogCount].Period);
        }

        [Test]
        public void LogJctClassResource_Delete()
        {
            int preTestLogCount = logReader.Read_LogJctClassResource(connection).Count;

            Person student = testDataBuilder.student;
            Resource oldresource = student.Enrollments[0].ClassEnrolled.Resources[0];
            student.Enrollments[0].ClassEnrolled.Resources[0] = resourceDaoImpl.Write(new Resource("new resource", "2"), connection);
            classEnrolledDaoImpl.Update(student.Enrollments[0].ClassEnrolled, connection);

            List<LogJctClassResource> logJctClassResources = logReader.Read_LogJctClassResource(connection);

            Assert.IsNotNull(logJctClassResources);
            Assert.IsNotEmpty(logJctClassResources);
            Assert.AreEqual(preTestLogCount + 1, logJctClassResources.Count);

            Assert_LogTable(logJctClassResources[preTestLogCount].log_jct_class_resource_id, 1, "log_jct_class_resource", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logJctClassResources[preTestLogCount].LogAction);

            Assert.AreEqual(student.Enrollments[0].ClassEnrolled.Id, logJctClassResources[preTestLogCount].class_id);
            Assert.AreEqual(oldresource.Id, logJctClassResources[preTestLogCount].resource_id);
        }

        [Test]
        public void LogLineitem_Update()
        {
            int preTestLogCount = logReader.Read_LogLineItem(connection).Count;

            LineItem oldLineItem = testDataBuilder.lineItem;
            oldLineItem.Descript = "new Lineitem description";
            lineItemDaoImpl.Update(oldLineItem, connection);

            List<LogLineItem> logLineItems = logReader.Read_LogLineItem(connection);

            Assert.IsNotNull(logLineItems);
            Assert.IsNotEmpty(logLineItems);
            Assert.AreEqual(preTestLogCount + 1, logLineItems.Count);

            Assert_LogTable((int)logLineItems[preTestLogCount].lineitem_id, 1, "log_lineitem", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logLineItems[preTestLogCount].LogAction);

            Assert_LogLineitem(oldLineItem, logLineItems[preTestLogCount]);
        }

        [Test]
        public void LogLineitem_Delete()
        {
            int preTestLogCount = logReader.Read_LogLineItem(connection).Count;

            LineItem oldLineItem = testDataBuilder.lineItem;
            lineItemDaoImpl.Delete(oldLineItem, connection);

            List<LogLineItem> logLineItems = logReader.Read_LogLineItem(connection);

            Assert.IsNotNull(logLineItems);
            Assert.IsNotEmpty(logLineItems);
            Assert.AreEqual(preTestLogCount + 1, logLineItems.Count);

            Assert_LogTable((int)logLineItems[preTestLogCount].lineitem_id, 1, "log_lineitem", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logLineItems[preTestLogCount].LogAction);

            Assert_LogLineitem(oldLineItem, logLineItems[preTestLogCount]);
        }

        [Test]
        public void LogEnrollment_Update()
        {
            int preTestLogCount = logReader.Read_LogEnrollment(connection).Count;

            Person student = testDataBuilder.student;
            Enrollment oldEnrollment = student.Enrollments[0];
            student.Enrollments[0].GradeLevel = Enum_Grade.values["1"];
            personDaoImpl.Update(student, connection);

            List<LogEnrollment> logEnrollments = logReader.Read_LogEnrollment(connection);

            Assert.IsNotNull(logEnrollments);
            Assert.IsNotEmpty(logEnrollments);
            Assert.AreEqual(preTestLogCount + 1, logEnrollments.Count);

            Assert_LogTable((int)logEnrollments[preTestLogCount].enrollment_id, logEnrollments.Count, "log_enrollment", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logEnrollments[preTestLogCount].LogAction);

            Assert.AreEqual(student.Id, logEnrollments[preTestLogCount].person_id);
            Assert_LogEnrollment(oldEnrollment, logEnrollments[preTestLogCount]);
        }

        [Test]
        public void LogEnrollment_Delete()
        {
            int preTestLogCount = logReader.Read_LogEnrollment(connection).Count;

            Person student = testDataBuilder.student;
            Enrollment oldEnrollment = student.Enrollments[0];
            student.Enrollments.Clear();
            personDaoImpl.Update(student, connection);

            List<LogEnrollment> logEnrollments = logReader.Read_LogEnrollment(connection);

            Assert.IsNotNull(logEnrollments);
            Assert.IsNotEmpty(logEnrollments);
            Assert.AreEqual(preTestLogCount + 1, logEnrollments.Count);

            Assert_LogTable((int)logEnrollments[preTestLogCount].enrollment_id, logEnrollments.Count, "log_enrollment", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logEnrollments[preTestLogCount].LogAction);

            Assert.AreEqual(student.Id, logEnrollments[preTestLogCount].person_id);
            Assert_LogEnrollment(oldEnrollment, logEnrollments[preTestLogCount]);
        }

        [Test]
        public void LogMark_Update()
        {
            int preTestLogCount = logReader.Read_LogMark(connection).Count;

            Person student = testDataBuilder.student;
            Mark oldMark = student.Marks[0];
            oldMark.Comment = "New comment for old mark";
            markDaoImpl.Update(oldMark, student.Id, connection);

            List<LogMark> logMarks = logReader.Read_LogMark(connection);

            Assert.IsNotNull(logMarks);
            Assert.IsNotEmpty(logMarks);
            Assert.AreEqual(preTestLogCount + 1, logMarks.Count);

            Assert_LogTable((int)logMarks[preTestLogCount].mark_id, 2, "log_mark", connection);
            Assert.AreEqual(Enum_Log_Action.values["update"], logMarks[preTestLogCount].LogAction);

            Assert.AreEqual(student.Id, logMarks[preTestLogCount].person_id);
            Assert_LogMark(oldMark, logMarks[preTestLogCount]);
        }

        [Test]
        public void LogMark_Delete()
        {
            int preTestLogCount = logReader.Read_LogMark(connection).Count;

            Person student = testDataBuilder.student;
            Mark oldMark = student.Marks[0];
            markDaoImpl.Delete(oldMark, student.Id, connection);

            List<LogMark> logMarks = logReader.Read_LogMark(connection);

            Assert.IsNotNull(logMarks);
            Assert.IsNotEmpty(logMarks);
            Assert.AreEqual(preTestLogCount + 1, logMarks.Count);

            Assert_LogTable((int)logMarks[preTestLogCount].mark_id, 2, "log_mark", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logMarks[preTestLogCount].LogAction);

            Assert.AreEqual(student.Id, logMarks[preTestLogCount].person_id);
            Assert_LogMark(oldMark, logMarks[preTestLogCount]);
        }

        [Test]
        public void LogJctEnrollmentAcademicSession_Delete()
        {
            int preTestLogCount = logReader.Read_JctEnrollmentAcademicSession(connection).Count;

            Organization org = testDataBuilder.school;
            AcademicSession academicSession = testDataBuilder.schoolYear;
            Person student = testDataBuilder.student;
            Enrollment enrollment = testDataBuilder.enrollment;

            AcademicSession newAcademicSession = new AcademicSession("New Academic Session Name", Enum_Session.values["grading period"], org, "T1");
            newAcademicSession = academicSessionDaoImpl.Write(newAcademicSession, connection);
            enrollment.AcademicSessions[0] = newAcademicSession;
            enrollmentDaoImpl.Update(enrollment, student.Id, connection);

            List<LogJctEnrollmentAcademicSession> logJctEnrollmentAcademicSessions = logReader.Read_JctEnrollmentAcademicSession(connection);

            Assert.IsNotNull(logJctEnrollmentAcademicSessions);
            Assert.IsNotEmpty(logJctEnrollmentAcademicSessions);
            Assert.AreEqual(preTestLogCount + 1, logJctEnrollmentAcademicSessions.Count);

            Assert_LogTable((int)logJctEnrollmentAcademicSessions[preTestLogCount].log_jct_enrollment_academic_session_id, 1, "log_jct_enrollment_academic_session", connection);
            Assert.AreEqual(Enum_Log_Action.values["delete"], logJctEnrollmentAcademicSessions[preTestLogCount].LogAction);

            Assert.AreEqual(enrollment.Id, logJctEnrollmentAcademicSessions[preTestLogCount].enrollment_id);
            Assert.AreEqual(academicSession.Id, logJctEnrollmentAcademicSessions[preTestLogCount].academic_session_id);
        }

        [TearDown]
        public void TearDown()
        {
            connection.RollbackTransaction();
            connection.CloseConnection();
        }
    }
}
