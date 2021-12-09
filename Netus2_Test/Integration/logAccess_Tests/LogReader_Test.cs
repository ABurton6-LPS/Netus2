using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.logObjects;
using Netus2_DatabaseConnection.utilityTools;
using Netus2_Test.utiltiyTools;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Netus2_Test.Integration
{
    class LogReader_Test
    {
        IConnectable connection;
        LogReader logReader;
        IPersonDao personDaoImpl;
        IProviderDao providerDaoImpl;
        IApplicationDao applicationDaoImpl;
        IResourceDao resourceDaoImpl;
        ICourseDao courseDaoImpl;
        IClassEnrolledDao classEnrolledDaoImpl;
        ILineItemDao lineItemDaoImpl;
        IMarkDao markDaoImpl;
        IEnrollmentDao enrollmentDaoImpl;
        IOrganizationDao organizationDaoImpl;
        IAcademicSessionDao academicSessionDaoImpl;
        IAddressDao addressDaoImpl;
        IEmploymentSessionDao employmentSessionDaoImpl;
        IUniqueIdentifierDao uniqueIdentifierDaoImpl;
        IPhoneNumberDao phoneNumberDaoImpl;

        [SetUp]
        public void Setup()
        {
            DbConnectionFactory.environment = new MockEnvironment();

            connection = DbConnectionFactory.GetNetus2Connection();
            connection.BeginTransaction();
            logReader = new LogReader();
            organizationDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
            academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
            personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
            courseDaoImpl = DaoImplFactory.GetCourseDaoImpl();
            classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
            resourceDaoImpl = DaoImplFactory.GetResourceDaoImpl();
            lineItemDaoImpl = DaoImplFactory.GetLineItemDaoImpl();
            enrollmentDaoImpl = DaoImplFactory.GetEnrollmentDaoImpl();
            markDaoImpl = DaoImplFactory.GetMarkDaoImpl();
            addressDaoImpl = DaoImplFactory.GetAddressDaoImpl();
            providerDaoImpl = DaoImplFactory.GetProviderDaoImpl();
            applicationDaoImpl = DaoImplFactory.GetApplicationDaoImpl();
            employmentSessionDaoImpl = DaoImplFactory.GetEmploymentSessionDaoImpl();
            uniqueIdentifierDaoImpl = DaoImplFactory.GetUniqueIdentifierDaoImpl();
            phoneNumberDaoImpl = DaoImplFactory.GetPhoneNumberDaoImpl();
        }

        [Test]
        public void LogPerson_ShouldLog_WriteUpdateDelete()
        {
            //Write
            Person person = personDaoImpl.Write(new Person("ftest", "ltest", new DateTime(1985, 9, 6), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]), connection);

            //Read logs after write
            List<LogPerson> logs = new List<LogPerson>();
            foreach (LogPerson log in logReader.Read_LogPerson(connection))
                if (log.person_id == person.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            person.FirstName = "new";
            personDaoImpl.Update(person, connection);
            person = personDaoImpl.Read_UsingPersonId(person.Id, connection);

            //Read logs after update
            logs.Clear();
            foreach (LogPerson log in logReader.Read_LogPerson(connection))
                if (log.person_id == person.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogPerson updateLog = null;
            foreach (LogPerson log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            personDaoImpl.Delete(person, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogPerson log in logReader.Read_LogPerson(connection))
                if (log.person_id == person.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogPerson deleteLog = null;
            foreach (LogPerson log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogJctPersonRole_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Person person = new Person("ftest", "ltest", new DateTime(1985, 9, 6), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]);
            Enumeration role = Enum_Role.values["unset"];

            //Write
            person.Roles.Add(role);
            person = personDaoImpl.Write(person, connection);

            //Read logs after write
            List<LogJctPersonRole> logs = new List<LogJctPersonRole>();
            foreach (LogJctPersonRole log in logReader.Read_LogJctPersonRole(connection))
                if (log.person_id == person.Id && log.Role.Id == role.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Delete
            person.Roles[0] = Enum_Role.values["student"];
            personDaoImpl.Update(person, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogJctPersonRole log in logReader.Read_LogJctPersonRole(connection))
                if (log.person_id == person.Id && log.Role.Id == role.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(2, logs.Count);

            //Get delete log from logs
            LogJctPersonRole deleteLog = null;
            foreach (LogJctPersonRole log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogJctPersonPerson_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Person person1 = personDaoImpl.Write(new Person("ftest", "ltest", new DateTime(1985, 9, 6), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]), connection);
            Person person2 = personDaoImpl.Write(new Person("ftest", "ltest", new DateTime(1985, 9, 6), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]), connection);

            //Write
            person1.Relations.Add(person2.Id);
            personDaoImpl.Update(person1, connection);

            //Read logs after write
            List<LogJctPersonPerson> logs = new List<LogJctPersonPerson>();
            foreach (LogJctPersonPerson log in logReader.Read_LogJctPersonPerson(connection))
                if ((log.person_one_id == person1.Id && log.person_two_id == person2.Id) || (log.person_one_id == person2.Id && log.person_two_id == person1.Id))
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(2, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[1].LogAction);

            //Delete
            person1.Relations.Clear();
            personDaoImpl.Update(person1, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogJctPersonPerson log in logReader.Read_LogJctPersonPerson(connection))
                if ((log.person_one_id == person1.Id && log.person_two_id == person2.Id) || (log.person_one_id == person2.Id && log.person_two_id == person1.Id))
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(4, logs.Count);

            //Get delete logs from logs
            List<LogJctPersonPerson> deleteLogs = new List<LogJctPersonPerson>();
            foreach (LogJctPersonPerson log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLogs.Add(log);

            //Assert on delete logs
            Assert.AreEqual(2, deleteLogs.Count);
        }

        [Test]
        public void LogUniqueIdentifier_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Person person = personDaoImpl.Write(new Person("ftest", "ltest", new DateTime(1985, 9, 6), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]), connection);

            //Write
            UniqueIdentifier uniqueIdentifier = uniqueIdentifierDaoImpl.Write(new UniqueIdentifier("test", Enum_Identifier.values["unset"]), person.Id, connection);

            //Read logs after write
            List<LogUniqueIdentifier> logs = new List<LogUniqueIdentifier>();
            foreach (LogUniqueIdentifier log in logReader.Read_LogUniqueIdentifier(connection))
                if (log.unique_identifier_id == uniqueIdentifier.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            uniqueIdentifier.Identifier = "new";
            uniqueIdentifierDaoImpl.Update(uniqueIdentifier, person.Id, connection);
            uniqueIdentifier = uniqueIdentifierDaoImpl.Read(uniqueIdentifier, person.Id, connection)[0];

            //Read logs after update
            logs.Clear();
            foreach (LogUniqueIdentifier log in logReader.Read_LogUniqueIdentifier(connection))
                if (log.unique_identifier_id == uniqueIdentifier.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogUniqueIdentifier updateLog = null;
            foreach (LogUniqueIdentifier log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            uniqueIdentifierDaoImpl.Delete(uniqueIdentifier, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogUniqueIdentifier log in logReader.Read_LogUniqueIdentifier(connection))
                if (log.unique_identifier_id == uniqueIdentifier.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogUniqueIdentifier deleteLog = null;
            foreach (LogUniqueIdentifier log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogProvider_ShouldLog_WriteUpdateDelete()
        {
            //Write
            Provider provider = providerDaoImpl.Write(new Provider("testName"), connection);

            //Read logs after write
            List<LogProvider> logs = new List<LogProvider>();
            foreach (LogProvider log in logReader.Read_LogProvider(connection))
                if (log.provider_id == provider.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            provider.Name = "new";
            providerDaoImpl.Update(provider, connection);
            provider = providerDaoImpl.Read_UsingProviderId(provider.Id, connection);

            //Read logs after update
            logs.Clear();
            foreach (LogProvider log in logReader.Read_LogProvider(connection))
                if (log.provider_id == provider.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogProvider updateLog = null;
            foreach (LogProvider log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            providerDaoImpl.Delete(provider, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogProvider log in logReader.Read_LogProvider(connection))
                if (log.provider_id == provider.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogProvider deleteLog = null;
            foreach (LogProvider log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogApplicationlication_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Provider provider = providerDaoImpl.Write(new Provider("testName"), connection);

            //Write
            Application application = applicationDaoImpl.Write(new Application("testName", provider), connection);

            //Read logs after write
            List<LogApplication> logs = new List<LogApplication>();
            foreach (LogApplication log in logReader.Read_LogApplication(connection))
                if (log.application_id == application.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            application.Name = "newTestName";
            applicationDaoImpl.Update(application, connection);
            application = applicationDaoImpl.Read_UsingAppId(application.Id, connection);

            //Read logs after update
            logs.Clear();
            foreach (LogApplication log in logReader.Read_LogApplication(connection))
                if (log.application_id == application.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogApplication updateLog = null;
            foreach (LogApplication log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            applicationDaoImpl.Delete(application, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogApplication log in logReader.Read_LogApplication(connection))
                if (log.application_id == application.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogApplication deleteLog = null;
            foreach (LogApplication log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogJctPersonApplication_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Provider provider = providerDaoImpl.Write(new Provider("testName"), connection);
            Application application = applicationDaoImpl.Write(new Application("testName", provider), connection);
            Person person = new Person("fname", "lname", new DateTime(), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]);            

            //Write
            person.Applications.Add(application);
            person = personDaoImpl.Write(person, connection);

            //Read logs after write
            List<LogJctPersonApplication> logs = new List<LogJctPersonApplication>();
            foreach (LogJctPersonApplication log in logReader.Read_LogJctPersonApplication(connection))
                if (log.person_id == person.Id && log.application_id == application.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Delete
            person.Applications.Clear();
            personDaoImpl.Update(person, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogJctPersonApplication log in logReader.Read_LogJctPersonApplication(connection))
                if (log.person_id == person.Id && log.application_id == application.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(2, logs.Count);

            //Get delete log from logs
            LogJctPersonApplication deleteLog = null;
            foreach (LogJctPersonApplication log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogJctPersonPhoneNumber_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Person person = new Person("fname", "lname", new DateTime(), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]);
            PhoneNumber phoneNumber = new PhoneNumber("1234567890", Enum_Phone.values["cell"]);

            //Write
            phoneNumber = phoneNumberDaoImpl.Write(phoneNumber, connection);
            person.PhoneNumbers.Add(phoneNumber);
            person = personDaoImpl.Write(person, connection);

            //Read logs after write
            List<LogJctPersonPhoneNumber> logs = new List<LogJctPersonPhoneNumber>();
            foreach (LogJctPersonPhoneNumber log in logReader.Read_LogJctPersonPhoneNumber(connection))
                if (log.person_id == person.Id && log.phone_number_id == phoneNumber.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Delete
            person.PhoneNumbers.Clear();
            personDaoImpl.Update(person, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogJctPersonPhoneNumber log in logReader.Read_LogJctPersonPhoneNumber(connection))
                if (log.person_id == person.Id && log.phone_number_id == phoneNumber.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(2, logs.Count);

            //Get delete log from logs
            LogJctPersonPhoneNumber deleteLog = null;
            foreach (LogJctPersonPhoneNumber log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogPhoneNumber_ShouldLog_WriteUpdateDelete()
        {
            //Write
            PhoneNumber phoneNumber = phoneNumberDaoImpl.Write(new PhoneNumber("2134567890", Enum_Phone.values["unset"]), connection);

            //Read logs after write
            List<LogPhoneNumber> logs = new List<LogPhoneNumber>();
            foreach (LogPhoneNumber log in logReader.Read_LogPhoneNumber(connection))
                if (log.phone_number_id == phoneNumber.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            phoneNumber.PhoneNumberValue = "0123456789";
            phoneNumberDaoImpl.Update(phoneNumber, connection);
            phoneNumber = phoneNumberDaoImpl.Read(phoneNumber, connection)[0];

            //Read logs after update
            logs.Clear();
            foreach (LogPhoneNumber log in logReader.Read_LogPhoneNumber(connection))
                if (log.phone_number_id == phoneNumber.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogPhoneNumber updateLog = null;
            foreach (LogPhoneNumber log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            phoneNumberDaoImpl.Delete(phoneNumber, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogPhoneNumber log in logReader.Read_LogPhoneNumber(connection))
                if (log.phone_number_id == phoneNumber.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogPhoneNumber deleteLog = null;
            foreach (LogPhoneNumber log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogAddress_ShouldLog_WriteUpdateDelete()
        {
            //Write
            Address address = addressDaoImpl.Write(new Address("tst1", "apt. 1", "tstCity", "12345"), connection);

            //Read logs after write
            List<LogAddress> logs = new List<LogAddress>();
            foreach (LogAddress log in logReader.Read_LogAddress(connection))
                if (log.address_id == address.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            address.Line2 = "tst2";
            addressDaoImpl.Update(address, connection);
            address = addressDaoImpl.Read_UsingAddressId(address.Id, connection);

            //Read logs after update
            logs.Clear();
            foreach (LogAddress log in logReader.Read_LogAddress(connection))
                if (log.address_id == address.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogAddress updateLog = null;
            foreach (LogAddress log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            addressDaoImpl.Delete(address, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogAddress log in logReader.Read_LogAddress(connection))
                if (log.address_id == address.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogAddress deleteLog = null;
            foreach (LogAddress log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogJctPersonAddress_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Address address = addressDaoImpl.Write(new Address("tst1", "apt. 1", "tstCity", "12345"), connection);
            address.IsPrimary = true;
            Person person = new Person("fname", "lname", new DateTime(), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]);

            //Write
            person.Addresses.Add(address);
            person = personDaoImpl.Write(person, connection);

            List<LogJctPersonAddress> logs = new List<LogJctPersonAddress>();
            foreach (LogJctPersonAddress log in logReader.Read_LogJctPersonAddress(connection))
                if (log.person_id == person.Id && log.address_id == address.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Delete
            person.Addresses.Clear();
            personDaoImpl.Update(person, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogJctPersonAddress log in logReader.Read_LogJctPersonAddress(connection))
                if (log.person_id == person.Id && log.address_id == address.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(2, logs.Count);

            //Get delete log from logs
            LogJctPersonAddress deleteLog = null;
            foreach (LogJctPersonAddress log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogEmploymentSession_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Organization org = organizationDaoImpl.Write(new Organization("testName", Enum_Organization.values["unset"], "testIdentifier", "testSisBuildingCode"), connection);
            Person person = personDaoImpl.Write(new Person("ftest", "ltest", new DateTime(1985, 9, 6), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]), connection);

            //Write
            EmploymentSession employmentSession = employmentSessionDaoImpl.Write(new EmploymentSession(true, org), person.Id, connection);

            //Read logs after write
            List<LogEmploymentSession> logs = new List<LogEmploymentSession>();
            foreach (LogEmploymentSession log in logReader.Read_LogEmploymentSession(connection))
                if (log.employment_session_id == employmentSession.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            employmentSession.Name = "new";
            employmentSessionDaoImpl.Update(employmentSession, person.Id, connection);
            employmentSession = employmentSessionDaoImpl.Read_AllWithPersonId(person.Id, connection)[0];

            //Read logs after update
            logs.Clear();
            foreach (LogEmploymentSession log in logReader.Read_LogEmploymentSession(connection))
                if (log.employment_session_id == employmentSession.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogEmploymentSession updateLog = null;
            foreach (LogEmploymentSession log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            employmentSessionDaoImpl.Delete(employmentSession, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogEmploymentSession log in logReader.Read_LogEmploymentSession(connection))
                if (log.employment_session_id == employmentSession.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogEmploymentSession deleteLog = null;
            foreach (LogEmploymentSession log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogAcademicSession_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Organization org = organizationDaoImpl.Write(new Organization("testName", Enum_Organization.values["unset"], "testIdentifier", "testSisBuildingCode"), connection);

            //Write
            AcademicSession academicSession = academicSessionDaoImpl.Write(new AcademicSession(Enum_Session.values["unset"], org, "tst"), connection);

            //Read logs after write
            List<LogAcademicSession> logs = new List<LogAcademicSession>();
            foreach (LogAcademicSession log in logReader.Read_LogAcademicSession(connection))
                if (log.academic_session_id == academicSession.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            academicSession.TermCode = "new";
            academicSessionDaoImpl.Update(academicSession, connection);
            academicSession = academicSessionDaoImpl.Read_UsingAcademicSessionId(academicSession.Id, connection);

            //Read logs after update
            logs.Clear();
            foreach (LogAcademicSession log in logReader.Read_LogAcademicSession(connection))
                if (log.academic_session_id == academicSession.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogAcademicSession updateLog = null;
            foreach (LogAcademicSession log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            academicSessionDaoImpl.Delete(academicSession, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogAcademicSession log in logReader.Read_LogAcademicSession(connection))
                if (log.academic_session_id == academicSession.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogAcademicSession deleteLog = null;
            foreach (LogAcademicSession log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogOrganization_ShouldLog_WriteUpdateDelete()
        {
            //Write
            Organization org = organizationDaoImpl.Write(new Organization("testName", Enum_Organization.values["unset"], "test", "tstBldg"), connection);

            //Read logs after write
            List<LogOrganization> logs = new List<LogOrganization>();
            foreach (LogOrganization log in logReader.Read_LogOrganization(connection))
                if (log.organization_id == org.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            org.Name = "new";
            organizationDaoImpl.Update(org, connection);
            org = organizationDaoImpl.Read_UsingOrganizationId(org.Id, connection);

            //Read logs after update
            logs.Clear();
            foreach (LogOrganization log in logReader.Read_LogOrganization(connection))
                if (log.organization_id == org.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogOrganization updateLog = null;
            foreach (LogOrganization log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            organizationDaoImpl.Delete(org, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogOrganization log in logReader.Read_LogOrganization(connection))
                if (log.organization_id == org.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogOrganization deleteLog = null;
            foreach (LogOrganization log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogResource_ShouldLog_WriteUpdateDelete()
        {
            //Write
            Resource resource = resourceDaoImpl.Write(new Resource("testName", "test"), connection);

            //Read logs after write
            List<LogResource> logs = new List<LogResource>();
            foreach (LogResource log in logReader.Read_LogResource(connection))
                if (log.resource_id == resource.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            resource.Name = "new";
            resourceDaoImpl.Update(resource, connection);
            resource = resourceDaoImpl.Read_UsingResourceId(resource.Id, connection);

            //Read logs after update
            logs.Clear();
            foreach (LogResource log in logReader.Read_LogResource(connection))
                if (log.resource_id == resource.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogResource updateLog = null;
            foreach (LogResource log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            resourceDaoImpl.Delete(resource, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogResource log in logReader.Read_LogResource(connection))
                if (log.resource_id == resource.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogResource deleteLog = null;
            foreach (LogResource log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogCourse_ShouldLog_WriteUpdateDelete()
        {
            //Write
            Course course = courseDaoImpl.Write(new Course("testName", "testCourseCode"), connection);

            //Read logs after write
            List<LogCourse> logs = new List<LogCourse>();
            foreach (LogCourse log in logReader.Read_LogCourse(connection))
                if (log.course_id == course.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            course.Name = "new";
            courseDaoImpl.Update(course, connection);
            course = courseDaoImpl.Read(course, connection)[0];

            //Read logs after update
            logs.Clear();
            foreach (LogCourse log in logReader.Read_LogCourse(connection))
                if (log.course_id == course.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogCourse updateLog = null;
            foreach (LogCourse log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            courseDaoImpl.Delete(course, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogCourse log in logReader.Read_LogCourse(connection))
                if (log.course_id == course.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogCourse deleteLog = null;
            foreach (LogCourse log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogJctCourseSubject_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Course course = new Course("testName", "tstCode");
            Enumeration subject = Enum_Subject.values["unset"];

            //Write
            course.Subjects.Add(subject);
            course = courseDaoImpl.Write(course, connection);

            //Read logs after write
            List<LogJctCourseSubject> logs = new List<LogJctCourseSubject>();
            foreach (LogJctCourseSubject log in logReader.Read_LogJctCourseSubject(connection))
                if (log.course_id == course.Id && log.Subject.Id == subject.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Delete
            course.Subjects.Clear();
            courseDaoImpl.Update(course, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogJctCourseSubject log in logReader.Read_LogJctCourseSubject(connection))
                if (log.course_id == course.Id && log.Subject.Id == subject.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(2, logs.Count);

            LogJctCourseSubject deleteLog = null;
            foreach (LogJctCourseSubject log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogJctCourseGrade_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Course course = new Course("testName", "testCode");
            Enumeration grade = Enum_Grade.values["unset"];

            //Write
            course.Grades.Add(grade);
            course = courseDaoImpl.Write(course, connection);

            //Read logs after write
            List<LogJctCourseGrade> logs = new List<LogJctCourseGrade>();
            foreach (LogJctCourseGrade log in logReader.Read_LogJctCourseGrade(connection))
                if (log.course_id == course.Id && log.Grade.Id == grade.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Delete
            course.Grades.Clear();
            courseDaoImpl.Update(course, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogJctCourseGrade log in logReader.Read_LogJctCourseGrade(connection))
                if (log.course_id == course.Id && log.Grade.Id == grade.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(2, logs.Count);

            //Get delete log from logs
            LogJctCourseGrade deleteLog = null;
            foreach (LogJctCourseGrade log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogClass_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Course course = courseDaoImpl.Write(new Course("testName", "testCourseCode"), connection);
            Organization org = organizationDaoImpl.Write(new Organization("testName", Enum_Organization.values["unset"], "testIdentifier", "testSisBuildingCode"), connection);
            AcademicSession academicSession = academicSessionDaoImpl.Write(new AcademicSession(Enum_Session.values["unset"], org, "tst"), connection);

            //Write
            ClassEnrolled classEnrolled = classEnrolledDaoImpl.Write(new ClassEnrolled("testName", "tstClassCode", "testRoom", course, academicSession), connection);

            //Read logs after write
            List<LogClassEnrolled> logs = new List<LogClassEnrolled>();
            foreach (LogClassEnrolled log in logReader.Read_LogClassEnrolled(connection))
                if (log.class_enrolled_id == classEnrolled.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            classEnrolled.Name = "new";
            classEnrolledDaoImpl.Update(classEnrolled, connection);
            classEnrolled = classEnrolledDaoImpl.Read(classEnrolled, connection)[0];

            //Read logs after update
            logs.Clear();
            foreach (LogClassEnrolled log in logReader.Read_LogClassEnrolled(connection))
                if (log.class_enrolled_id == classEnrolled.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogClassEnrolled updateLog = null;
            foreach (LogClassEnrolled log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            classEnrolledDaoImpl.Delete(classEnrolled, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogClassEnrolled log in logReader.Read_LogClassEnrolled(connection))
                if (log.class_enrolled_id == classEnrolled.Id)
                    logs.Add(log);


            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogClassEnrolled deleteLog = null;
            foreach (LogClassEnrolled log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogJctClassPeriod_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Course course = courseDaoImpl.Write(new Course("testName", "tstCourseCode"), connection);
            Organization org = organizationDaoImpl.Write(new Organization("testName", Enum_Organization.values["unset"], "test", "tstBldg"), connection);
            AcademicSession academicSession = academicSessionDaoImpl.Write(new AcademicSession(Enum_Session.values["unset"], org, "tst"), connection);
            ClassEnrolled classEnrolled = new ClassEnrolled("testName", "testClassCode", "testRoom", course, academicSession);
            Enumeration period = Enum_Period.values["unset"];

            //Write
            classEnrolled.Periods.Add(period);
            classEnrolled = classEnrolledDaoImpl.Write(classEnrolled, connection);

            //Read logs after write
            List<LogJctClassPeriod> logs = new List<LogJctClassPeriod>();
            foreach (LogJctClassPeriod log in logReader.Read_LogJctClassPeriod(connection))
                if (log.class_id == classEnrolled.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Delete
            classEnrolled.Periods.Clear();
            classEnrolledDaoImpl.Update(classEnrolled, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogJctClassPeriod log in logReader.Read_LogJctClassPeriod(connection))
                if (log.class_id == classEnrolled.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(2, logs.Count);

            //Get delete log from logs
            LogJctClassPeriod deleteLog = null;
            foreach (LogJctClassPeriod log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogJctClassResource_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Resource resource = resourceDaoImpl.Write(new Resource("tstName", "tstVenName"), connection);
            Course course = courseDaoImpl.Write(new Course("testName", "tstCourseCode"), connection);
            Organization org = organizationDaoImpl.Write(new Organization("testName", Enum_Organization.values["unset"], "test", "tstBldg"), connection);
            AcademicSession academicSession = academicSessionDaoImpl.Write(new AcademicSession(Enum_Session.values["unset"], org, "tst"), connection);
            ClassEnrolled classEnrolled = new ClassEnrolled("testName", "testClassCode", "testRoom", course, academicSession);

            //Write
            classEnrolled.Resources.Add(resource);
            classEnrolled = classEnrolledDaoImpl.Write(classEnrolled, connection);

            //Read logs after write
            List<LogJctClassResource> logs = new List<LogJctClassResource>();
            foreach (LogJctClassResource log in logReader.Read_LogJctClassResource(connection))
                if (log.class_id == classEnrolled.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Delete
            classEnrolled.Resources.Clear();
            classEnrolledDaoImpl.Update(classEnrolled, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogJctClassResource log in logReader.Read_LogJctClassResource(connection))
                if (log.class_id == classEnrolled.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(2, logs.Count);

            //Get delete log from logs
            LogJctClassResource deleteLog = null;
            foreach (LogJctClassResource log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogLineitem_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Course course = courseDaoImpl.Write(new Course("testName", "tstCourseCode"), connection);
            Organization org = organizationDaoImpl.Write(new Organization("testName", Enum_Organization.values["unset"], "test", "tstBldg"), connection);
            AcademicSession academicSession = academicSessionDaoImpl.Write(new AcademicSession(Enum_Session.values["unset"], org, "tst"), connection);
            ClassEnrolled classEnrolled = classEnrolledDaoImpl.Write(new ClassEnrolled("testName", "testClassCode", "testRoom", course, academicSession), connection);

            //Write
            LineItem lineItem = lineItemDaoImpl.Write(new LineItem("testName", new DateTime(), new DateTime(), classEnrolled, Enum_Category.values["unset"], 0, 100), connection);

            //Read logs after write
            List<LogLineItem> logs = new List<LogLineItem>();
            foreach (LogLineItem log in logReader.Read_LogLineItem(connection))
                if (log.lineitem_id == lineItem.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            lineItem.Descript = "newDescript";
            lineItemDaoImpl.Update(lineItem, connection);
            lineItem = lineItemDaoImpl.Read_UsingLineItemId(lineItem.Id, connection);

            //Read logs after update
            logs.Clear();
            foreach (LogLineItem log in logReader.Read_LogLineItem(connection))
                if (log.lineitem_id == lineItem.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogLineItem updateLog = null;
            foreach (LogLineItem log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            lineItemDaoImpl.Delete(lineItem, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogLineItem log in logReader.Read_LogLineItem(connection))
                if (log.lineitem_id == lineItem.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogLineItem deleteLog = null;
            foreach (LogLineItem log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogEnrollment_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Person person = personDaoImpl.Write(new Person("fname", "lname", new DateTime(1985, 9, 6), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]), connection);
            Organization organization = organizationDaoImpl.Write(new Organization("School", Enum_Organization.values["school"], "S", "12345"), connection);
            AcademicSession academicSession = academicSessionDaoImpl.Write(new AcademicSession(Enum_Session.values["school year"], organization, "HY"), connection);

            //Write
            Enrollment enrollment = new Enrollment(academicSession);
            enrollment.GradeLevel = Enum_Grade.values["1"];
            enrollment = enrollmentDaoImpl.Write(enrollment, person.Id, connection);

            //Read logs after write
            List<LogEnrollment> logs = new List<LogEnrollment>();
            foreach (LogEnrollment log in logReader.Read_LogEnrollment(connection))
                if (log.enrollment_id == enrollment.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            enrollment.EndDate = new DateTime(2021, 1, 1);
            enrollmentDaoImpl.Update(enrollment, person.Id, connection);
            enrollment = enrollmentDaoImpl.Read(enrollment, person.Id, connection)[0];

            //Read logs after update
            logs.Clear();
            foreach (LogEnrollment log in logReader.Read_LogEnrollment(connection))
                if (log.enrollment_id == enrollment.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogEnrollment updateLog = null;
            foreach (LogEnrollment log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            enrollmentDaoImpl.Delete(enrollment, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogEnrollment log in logReader.Read_LogEnrollment(connection))
                if (log.enrollment_id == enrollment.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogEnrollment deleteLog = null;
            foreach (LogEnrollment log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogMark_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Person person = personDaoImpl.Write(new Person("ftest", "ltest", new DateTime(1985, 9, 6), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]), connection);
            Course course = courseDaoImpl.Write(new Course("testName", "tstCourseCode"), connection);
            Organization org = organizationDaoImpl.Write(new Organization("testName", Enum_Organization.values["unset"], "test", "tstBldg"), connection);
            AcademicSession academicSession = academicSessionDaoImpl.Write(new AcademicSession(Enum_Session.values["unset"], org, "tst"), connection);
            ClassEnrolled classEnrolled = classEnrolledDaoImpl.Write(new ClassEnrolled("testName", "testClassCode", "testRoom", course, academicSession), connection);
            LineItem lineItem = lineItemDaoImpl.Write(new LineItem("testName", new DateTime(), new DateTime(), classEnrolled, Enum_Category.values["unset"], 0, 100), connection);

            //Write
            Mark mark = markDaoImpl.Write(new Mark(lineItem, Enum_Score_Status.values["unset"], 95, new DateTime()), person.Id, connection);

            //Read logs after write
            List<LogMark> logs = new List<LogMark>();
            foreach (LogMark log in logReader.Read_LogMark(connection))
                if (log.mark_id == mark.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Update
            mark.Score = 97;
            markDaoImpl.Update(mark, person.Id, connection);
            mark = markDaoImpl.Read(mark, person.Id, connection)[0];

            //Read logs after update
            logs.Clear();
            foreach (LogMark log in logReader.Read_LogMark(connection))
                if (log.mark_id == mark.Id)
                    logs.Add(log);

            //Assert on number of logs after update
            Assert.AreEqual(2, logs.Count);

            //Get update log from logs
            LogMark updateLog = null;
            foreach (LogMark log in logs)
                if (log.LogAction == Enum_Log_Action.values["update"])
                    updateLog = log;

            //Assert on update log
            Assert.IsNotNull(updateLog);

            //Delete
            markDaoImpl.Delete(mark, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogMark log in logReader.Read_LogMark(connection))
                if (log.mark_id == mark.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(3, logs.Count);

            //Get delete log from logs
            LogMark deleteLog = null;
            foreach (LogMark log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [Test]
        public void LogJctEnrollmentClassEnrolled_ShouldLog_WriteUpdateDelete()
        {
            //Prerequisite
            Person person = personDaoImpl.Write(new Person("fname", "lname", new DateTime(1985, 9, 6), Enum_Gender.values["unset"], Enum_Ethnic.values["unset"]), connection);
            Course course = courseDaoImpl.Write(new Course("testName", "tstCourseCode"), connection);
            Organization organization = organizationDaoImpl.Write(new Organization("testName", Enum_Organization.values["unset"], "tstId", "tstBldg"), connection);
            AcademicSession academicSession = academicSessionDaoImpl.Write(new AcademicSession(Enum_Session.values["unset"], organization, "tst"), connection);
            ClassEnrolled classEnrolled = classEnrolledDaoImpl.Write(new ClassEnrolled("testName", "testClassCode", "testRoom", course, academicSession), connection);

            //Write
            Enrollment enrollment = new Enrollment(academicSession);
            enrollment.ClassesEnrolled.Add(classEnrolled);
            enrollment.GradeLevel = Enum_Grade.values["1"];
            enrollment = enrollmentDaoImpl.Write(enrollment, person.Id, connection);

            //Read logs after write
            List <LogJctEnrollmentClassEnrolled> logs = new List<LogJctEnrollmentClassEnrolled>();
            foreach (LogJctEnrollmentClassEnrolled log in logReader.Read_JctEnrollmentClassEnrolled(connection))
                if (log.enrollment_id == enrollment.Id && log.class_enrolled_id == classEnrolled.Id)
                    logs.Add(log);

            //Assert on log after write
            Assert.AreEqual(1, logs.Count);
            Assert.AreEqual(Enum_Log_Action.values["insert"], logs[0].LogAction);

            //Delete
            enrollment.ClassesEnrolled.Clear();
            enrollmentDaoImpl.Update(enrollment, person.Id, connection);

            //Read logs after delete
            logs.Clear();
            foreach (LogJctEnrollmentClassEnrolled log in logReader.Read_JctEnrollmentClassEnrolled(connection))
                if (log.enrollment_id == enrollment.Id && log.class_enrolled_id == classEnrolled.Id)
                    logs.Add(log);

            //Assert on number of logs after delete
            Assert.AreEqual(2, logs.Count);

            //Get delete log from logs
            LogJctEnrollmentClassEnrolled deleteLog = null;
            foreach (LogJctEnrollmentClassEnrolled log in logs)
                if (log.LogAction == Enum_Log_Action.values["delete"])
                    deleteLog = log;

            //Assert on delete log
            Assert.IsNotNull(deleteLog);
        }

        [TearDown]
        public void TearDown()
        {
            connection.RollbackTransaction();
            connection.CloseConnection();
        }
    }
}