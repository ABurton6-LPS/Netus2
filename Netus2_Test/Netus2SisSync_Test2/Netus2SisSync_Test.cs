using NUnit.Framework;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using Netus2;
using Netus2.dbAccess;
using Netus2.enumerations;
using Netus2.daoImplementations;
using System.Collections.Generic;
using Netus2.daoInterfaces;
using Netus2_DatabaseConnection;
using Netus2SisSync;

namespace Netus2_Test.Netus2SisSync_Test2
{
    class Netus2SisSync_Test
    {
        LoggerFactory loggerFactory;
        ILogger log;
        CountDownLatch_Mock latch;
        IConnectable miStarConnection;
        IConnectable netus2Connection;

        [SetUp]
        public void SetUp()
        {
            loggerFactory = new LoggerFactory();
            latch = new CountDownLatch_Mock(0);
            log = loggerFactory.CreateLogger("Test Logger");

            //miStarConnection = new MiStarDatabaseConnection();
            //miStarConnection.OpenConnection();

            //netus2Connection = new Netus2DatabaseConnection();
            //netus2Connection.OpenConnection();
            //netus2Connection.BeginTransaction();
        }

        [TestCase]
        public void Netus2SisSync_LiveRun()
        {
            try
            {
                FunctionApp1.Netus2SisSync.Run(null, log);
                Assert.Pass();
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestCase]
        public void Netus2SisSyncTest_Organization_Children()
        {
            DataTable dtOrganization = SyncOrganization.ReadFromSis(miStarConnection);

            for(int i = 0; (i < 10 && i < dtOrganization.Rows.Count); i++)
            {
                DataRow row = dtOrganization.Rows[i];
                SyncOrganization.SyncForChildRecords(row, latch, netus2Connection, log);
                ConfirmNetus2WasSynced_Organization_Children(row, netus2Connection);
            }
        }

        [TestCase]
        public void Netus2SisSyncTest_Organization_Parents()
        {
            DataTable dtOrganization = SyncOrganization.ReadFromSis(miStarConnection);

            for (int i = 0; (i < 10 && i < dtOrganization.Rows.Count); i++)
            {
                DataRow row = dtOrganization.Rows[i];
                SyncOrganization.SyncForParentRecords(row, latch, netus2Connection, log);
                ConfirmNetus2WasSynced_Organization_Parents(row, netus2Connection);
            }
        }

        [TestCase]
        public void Netus2SisSyncTest_AcademicSession_Children()
        {
            DataTable dtAcademicSession = SyncAcademicSession.ReadFromSis(miStarConnection);

            for (int i = 0; (i < 10 && i < dtAcademicSession.Rows.Count); i++)
            {
                DataRow row = dtAcademicSession.Rows[i];
                SyncAcademicSession.SyncForChildRecords(row, latch, netus2Connection, log);
                ConfirmNetus2WasSynched_AcademicSession_Children(row, netus2Connection);
            }
        }

        [TestCase]
        public void Netus2SisSyncTest_AcademicSession_Parents()
        {
            DataTable dtAcademicSession = SyncAcademicSession.ReadFromSis(miStarConnection);

            for (int i = 0; (i < 10 && i < dtAcademicSession.Rows.Count); i++)
            {
                DataRow row = dtAcademicSession.Rows[i];
                SyncAcademicSession.SyncForParentRecords(row, latch, netus2Connection, log);
                ConfirmNetus2WasSynched_AcademicSession_Parents(row, netus2Connection);
            }
        }

        [TestCase]
        public void Netus2SisSyncTest_Person()
        {
            DataTable dtPerson = SyncPerson.ReadFromSis(miStarConnection);

            for (int i = 0; (i < 10 && i < dtPerson.Rows.Count); i++)
            {
                DataRow row = dtPerson.Rows[i];
                SyncPerson.SyncForAllRecords(row, latch, netus2Connection, log);
                ConfirmNetus2WasSynched_Person(row, netus2Connection);
            }
        }

        //[TearDown]
        //public void TearDown()
        //{
        //    if(miStarConnection.GetState() == ConnectionState.Open)
        //    {
        //        miStarConnection.CloseConnection();
        //    }

        //    if (netus2Connection.GetState() == ConnectionState.Open)
        //    {
        //        netus2Connection.RollbackTransaction();
        //        netus2Connection.CloseConnection();
        //    }
        //}

        private static void ConfirmNetus2WasSynced_Organization_Children(DataRow row, IConnectable connection)
        {
            string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
            Enumeration sisEnumOrganization = Enum_Organization.values[row["enum_organization_id"].ToString()];
            string sisIdentifier = row["identifier"].ToString() == "" ? null : row["identifier"].ToString();
            string sisBuildingCode = row["building_code"].ToString() == "" ? null : row["building_code"].ToString();

            Organization org = new Organization(sisName, sisEnumOrganization, sisIdentifier);
            org.BuildingCode = sisBuildingCode;

            IOrganizationDao orgDaoImpl = new OrganizationDaoImpl();
            List<Organization> foundOrgs = orgDaoImpl.Read(org, connection);

            if (foundOrgs.Count > 1)
            {
                Assert.Fail("Multiple Organization records found matching:\n" + org.ToString());
            }
            else if (foundOrgs.Count == 0)
            {
                Assert.Fail(org.Name + " was not written to the Netus2 database.");
            }
            else if (foundOrgs.Count == 1)
            {
                org.Id = foundOrgs[0].Id;

                if ((org.Name != foundOrgs[0].Name) ||
                    (org.OrganizationType != foundOrgs[0].OrganizationType) ||
                    (org.Identifier != foundOrgs[0].Identifier) ||
                    (org.BuildingCode != foundOrgs[0].BuildingCode))
                {
                    Assert.Fail(org.Name + " was not updated in the Netus2 databse.\n" +
                        "sisEnumOrganization: " + sisEnumOrganization + "\n OrganizationType: " + org.OrganizationType + "\n" +
                        "sisIdentifier: " + sisIdentifier + "\n Identifier: " + org.Identifier + "\n" +
                        "sisBuildingCode: " + sisBuildingCode + "\n BuildingCode: " + org.BuildingCode);
                }
            }
        }

        private static void ConfirmNetus2WasSynced_Organization_Parents(DataRow row, IConnectable connection)
        {
            string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
            Enumeration sisEnumOrganization = Enum_Organization.values[row["enum_organization_id"].ToString()];
            string sisIdentifier = row["identifier"].ToString() == "" ? null : row["identifier"].ToString();
            string sisBuildingCode = row["building_code"].ToString() == "" ? null : row["building_code"].ToString();

            Organization org = new Organization(sisName, sisEnumOrganization, sisIdentifier);
            org.BuildingCode = sisBuildingCode;

            IOrganizationDao orgDaoImpl = new OrganizationDaoImpl();
            List<Organization> foundOrgs = orgDaoImpl.Read(org, connection);

            if (foundOrgs.Count > 1)
            {
                Assert.Fail("Multiple Organization records found matching:\n" + org.ToString());
            }
            else if (foundOrgs.Count == 0)
            {
                Assert.Fail(org.Name + " was not written to the Netus2 database.");
            }
            else if (foundOrgs.Count == 1)
            {
                org.Id = foundOrgs[0].Id;
            }

            string sisParentBuildingcode = row["organization_parent_id"].ToString();
            if (sisParentBuildingcode != null && sisParentBuildingcode != "")
            {
                Organization parentOrg = orgDaoImpl.Read_WithBuildingCode(sisParentBuildingcode, connection);
                if (parentOrg != null)
                {
                    List<int> childIds = new List<int>();
                    foreach (Organization child in parentOrg.Children)
                    {
                        childIds.Add(child.Id);
                    }

                    if (childIds.Contains(org.Id) == false)
                    {
                        Assert.Fail(org.Name + " was not updated to be linked to the proper parent id of: " + parentOrg.Id);
                    }
                }
            }
        }

        private static void ConfirmNetus2WasSynched_AcademicSession_Children(DataRow row, IConnectable connection)
        {
            string sisSessionCode = row["session_code"].ToString() == "" ? null : row["session_code"].ToString();
            int numberOfDashesInSessionCode = 2;
            int skipThisCharacterAndStartOnNextOne = 1;
            string schoolCode = sisSessionCode.Substring(0, (sisSessionCode.IndexOf('-')));
            int schoolYear = Int32.Parse(sisSessionCode.Substring(sisSessionCode.LastIndexOf('-') + skipThisCharacterAndStartOnNextOne));
            string termCode = sisSessionCode.Substring(sisSessionCode.IndexOf('-') + skipThisCharacterAndStartOnNextOne,
                (sisSessionCode.Length - schoolCode.Length) - schoolYear.ToString().Length - numberOfDashesInSessionCode);
            string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
            Enumeration sisEnumSession = Enum_Session.values[row["enum_session_id"].ToString()];
            DateTime sisStartDate = DateTime.Parse(row["start_date"].ToString());
            DateTime sisEndDate = DateTime.Parse(row["end_date"].ToString());
            string sisOrganizationId = row["organization_id"].ToString() == "" ? null : row["organization_id"].ToString();

            OrganizationDaoImpl orgDaoImpl = new OrganizationDaoImpl();
            Organization org = orgDaoImpl.Read_WithBuildingCode(sisOrganizationId, connection);

            AcademicSession academicSession = new AcademicSession(sisName, sisEnumSession, org, termCode);
            academicSession.SchoolYear = schoolYear;
            academicSession.StartDate = sisStartDate;
            academicSession.EndDate = sisEndDate;

            IAcademicSessionDao academicSessionDaoImpl = new AcademicSessionDaoImpl();
            List<AcademicSession> foundAcademicSessions = academicSessionDaoImpl.Read(academicSession, connection);

            if (foundAcademicSessions.Count > 1)
            {
                Assert.Fail("Multiple Academic Session records found matching:\n" + academicSession.ToString());
            }
            else if (foundAcademicSessions.Count == 0)
            {
                Assert.Fail(academicSession.Name + " was not written to the Netus2 database.");
            }
            else if (foundAcademicSessions.Count == 1)
            {
                academicSession.Id = foundAcademicSessions[0].Id;

                if ((academicSession.TermCode != foundAcademicSessions[0].TermCode) ||
                    (academicSession.SchoolYear != foundAcademicSessions[0].SchoolYear) ||
                    (academicSession.Name != foundAcademicSessions[0].Name) ||
                    (academicSession.StartDate != foundAcademicSessions[0].StartDate) ||
                    (academicSession.EndDate != foundAcademicSessions[0].EndDate) ||
                    (academicSession.SessionType != foundAcademicSessions[0].SessionType) ||
                    (academicSession.Organization.Id != foundAcademicSessions[0].Organization.Id))
                {
                    Assert.Fail(academicSession.Name + " was not updated in the Netus2 database.\n" +
                       "sisSessionCode: " + sisSessionCode);
                }

            }
        }

        private static void ConfirmNetus2WasSynched_AcademicSession_Parents(DataRow row, IConnectable connection)
        {
            string sisSessionCode = row["session_code"].ToString() == "" ? null : row["session_code"].ToString();
            int numberOfDashesInSessionCode = 2;
            int skipThisCharacterAndStartOnNextOne = 1;
            string schoolCode = sisSessionCode.Substring(0, (sisSessionCode.IndexOf('-')));
            int schoolYear = Int32.Parse(sisSessionCode.Substring(sisSessionCode.LastIndexOf('-') + skipThisCharacterAndStartOnNextOne));
            string termCode = sisSessionCode.Substring(sisSessionCode.IndexOf('-') + skipThisCharacterAndStartOnNextOne,
                (sisSessionCode.Length - schoolCode.Length) - schoolYear.ToString().Length - numberOfDashesInSessionCode);
            string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
            Enumeration sisEnumSession = Enum_Session.values[row["enum_session_id"].ToString()];
            DateTime sisStartDate = DateTime.Parse(row["start_date"].ToString());
            DateTime sisEndDate = DateTime.Parse(row["end_date"].ToString());
            string sisOrganizationId = row["organization_id"].ToString() == "" ? null : row["organization_id"].ToString();

            OrganizationDaoImpl orgDaoImpl = new OrganizationDaoImpl();
            Organization org = orgDaoImpl.Read_WithBuildingCode(sisOrganizationId, connection);

            AcademicSession academicSession = new AcademicSession(sisName, sisEnumSession, org, termCode);
            academicSession.SchoolYear = schoolYear;
            academicSession.StartDate = sisStartDate;
            academicSession.EndDate = sisEndDate;

            IAcademicSessionDao academicSessionDaoImpl = new AcademicSessionDaoImpl();
            List<AcademicSession> foundAcademicSessions = academicSessionDaoImpl.Read(academicSession, connection);

            if (foundAcademicSessions.Count == 1)
            {
                academicSession.Id = foundAcademicSessions[0].Id;
            }
            else
            {
                Assert.Fail(foundAcademicSessions.Count + " record(s) found matching Academic Session " + academicSession.ToString());
            }

            AcademicSession parentAcademicSession = null;
            string sisParentSessionCode = row["parent_session_code"].ToString() == "" ? null : row["parent_session_code"].ToString();

            if (sisParentSessionCode != null && sisParentSessionCode != "")
            {
                string sisParentSchoolCode = sisParentSessionCode.Substring(0, (sisParentSessionCode.IndexOf('-')));
                int sisParentSchoolYear = Int32.Parse(sisParentSessionCode.Substring(sisParentSessionCode.LastIndexOf('-') + skipThisCharacterAndStartOnNextOne));
                string sisParentTermCode = sisParentSessionCode.Substring(sisParentSessionCode.IndexOf('-') + skipThisCharacterAndStartOnNextOne,
                    (sisParentSessionCode.Length - sisParentSchoolCode.Length) - sisParentSchoolYear.ToString().Length - numberOfDashesInSessionCode);

                parentAcademicSession = academicSessionDaoImpl.Read_UsingSchoolCode_TermCode_Schoolyear(sisParentSchoolCode, sisParentTermCode, sisParentSchoolYear, connection);
                if (parentAcademicSession != null)
                {
                    List<int> childIds = new List<int>();
                    foreach (AcademicSession child in parentAcademicSession.Children)
                    {
                        childIds.Add(child.Id);
                    }

                    if (childIds.Contains(academicSession.Id) == false)
                    {
                        Assert.Fail(academicSession.Name + " was not updated to be linked to the proper parent id of: " + parentAcademicSession.Id);
                    }
                }
            }
        }

        private static void ConfirmNetus2WasSynched_Person(DataRow row, IConnectable connection)
        {
            Enumeration sisPersonType = Enum_Role.values[row["person_type"].ToString()];
            string sisId = row["SIS_ID"].ToString() == "" ? null : row["SIS_ID"].ToString();
            string sisFirstName = row["first_name"].ToString() == "" ? null : row["first_name"].ToString();
            string sisMiddleName = row["middle_name"].ToString() == "" ? null : row["middle_name"].ToString();
            string sisLastName = row["last_name"].ToString() == "" ? null : row["last_name"].ToString();
            DateTime sisBirthDate = DateTime.Parse(row["birth_date"].ToString());
            Enumeration sisGender = Enum_Gender.values[row["enum_gender_id"].ToString()];
            Enumeration sisEthnic = row["enum_ethnic_id"].ToString() == "unset" ? Enum_Ethnic.values["unset"] : Enum_Ethnic.GetEnumFromSisCode(row["enum_ethnic_id"].ToString());
            Enumeration sisResidenceStatus = Enum_Residence_Status.values[row["enum_residence_status_id"].ToString()];
            string sisLoginName = row["login_name"].ToString() == "" ? null : row["login_name"].ToString();
            string sisLoginPw = row["login_pw"].ToString() == "" ? null : row["login_pw"].ToString();

            Enumeration typeOfSisId = null;
            if (sisPersonType == Enum_Role.values["staff"])
            {
                typeOfSisId = Enum_Identifier.values["funiq"];
            }
            else
            {
                typeOfSisId = Enum_Identifier.values["student id"];
            }
            UniqueIdentifier uniqueId = new UniqueIdentifier(sisId, typeOfSisId, Enum_True_False.values["true"]);
            IUniqueIdentifierDao uniqueIdentifierDaoImpl = new UniqueIdentifierDaoImpl();
            List<UniqueIdentifier> foundUniqueIdentifiers = uniqueIdentifierDaoImpl.Read(uniqueId, -1, connection);

            IPersonDao personDaoImpl = new PersonDaoImpl();
            Person person = person = new Person(sisFirstName, sisLastName, sisBirthDate, sisGender, sisEthnic);

            if (foundUniqueIdentifiers.Count > 1)
            {
                Assert.Fail("Multiple UniqueIdentifier records found matching:\n" + uniqueId.ToString());
            }
            else if (foundUniqueIdentifiers.Count == 0)
            {
                person.Roles.Add(sisPersonType);
                person.MiddleName = sisMiddleName;
                person.ResidenceStatus = sisResidenceStatus;
                person.LoginName = sisLoginName;
                person.LoginPw = sisLoginPw;
                person.UniqueIdentifiers.Add(uniqueId);
                Assert.Fail("Person was not written to the database:\n" + person.ToString());
            }
            else if (foundUniqueIdentifiers.Count == 1)
            {
                person.UniqueIdentifiers.Add(foundUniqueIdentifiers[0]);
                List<Person> foundPersons = personDaoImpl.Read(person, connection);

                if (foundPersons.Count == 0)
                {
                    Assert.Fail("Person record was never written to the Netus2 database.\n" + person.ToString());
                }
                else if (foundPersons.Count > 1)
                {
                    Assert.Fail("Multiple records found matching Person record:\n" + person.ToString());
                }
                else if (foundPersons.Count == 1)
                {
                    person = foundPersons[0];

                    bool needsToBeUpdated = false;
                    if (person.Roles.Contains(sisPersonType) == false)
                    {
                        person.Roles.Add(sisPersonType);
                        needsToBeUpdated = true;
                    }
                    if ((person.MiddleName != sisMiddleName) ||
                        (person.ResidenceStatus != sisResidenceStatus) ||
                        (person.LoginName != sisLoginName) ||
                        (person.LoginPw != sisLoginPw))
                    {
                        person.MiddleName = sisMiddleName;
                        person.ResidenceStatus = sisResidenceStatus;
                        person.LoginName = sisLoginName;
                        person.LoginPw = sisLoginPw;
                        needsToBeUpdated = true;
                    }
                    if (needsToBeUpdated)
                        Assert.Fail("Person was not updated:\n" + person.ToString());
                }
            }
        }
    }

    public class CountDownLatch_Mock : CountDownLatch
    {
        public CountDownLatch_Mock(int count) : base(count){}
        public void Reset(int count){}
        public void Signal(){}
        public void Wait(){}
    }
}