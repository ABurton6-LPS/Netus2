using NUnit.Framework;
using Netus2SisSync.UtilityTools;
using System.Data;
using Moq;
using System.Collections.Generic;
using System;
using Netus2SisSync.SyncProcesses.SyncJobs;
using Netus2SisSync.SyncProcesses.SyncTasks.OrganizationTasks;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_Test.MockDaoImpl;
using Netus2_DatabaseConnection.utilityTools;
using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.enumerations;

namespace Netus2_Test.Unit.SyncProcess
{
    class SyncOrganization_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _sisConnection;
        MockDatabaseConnection _netus2Connection;
        MockOrganizationDaoImpl mockOrganizationDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;
            _sisConnection = (MockDatabaseConnection)DbConnectionFactory.GetSisConnection();
            _netus2Connection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            tdBuilder = new TestDataBuilder();
            DaoImplFactory.MockAll = true;
            mockOrganizationDaoImpl = new MockOrganizationDaoImpl(tdBuilder);
            DaoImplFactory.MockOrganizationDaoImpl = mockOrganizationDaoImpl;
        }

        [TestCase]
        public void SisRead_Organization_NullData()
        {
            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = null;
            tstData.OrgId = null;
            tstData.Ident = null;
            tstData.BldgCode = null;
            tstData.ParentOrgId = null;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Organization syncJob_Organization = new SyncJob_Organization();
            syncJob_Organization.ReadFromSis();
            DataTable results = syncJob_Organization._dtOrganization;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(emptyString, results.Rows[0]["name"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_organization_id"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["identifier"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["building_code"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["organization_parent_id"].ToString());
        }

        [TestCase]
        public void SisRead_Organization_TestData()
        {
            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = tdBuilder.district.Name;
            tstData.OrgId = tdBuilder.district.OrganizationType.Netus2Code;
            tstData.Ident = tdBuilder.district.Identifier;
            tstData.BldgCode = tdBuilder.district.SisBuildingCode;
            tstData.ParentOrgId = null;

            SisOrganizationTestData tstData2 = new SisOrganizationTestData();
            tstData2.Name = tdBuilder.school.Name;
            tstData2.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData2.Ident = tdBuilder.school.Identifier;
            tstData2.BldgCode = tdBuilder.school.SisBuildingCode;
            tstData2.ParentOrgId = tdBuilder.district.SisBuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            tstDataSet.Add(tstData2);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Organization syncJob_Organization = new SyncJob_Organization();
            syncJob_Organization.ReadFromSis();
            DataTable results = syncJob_Organization._dtOrganization;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstDataSet[0].Name, results.Rows[0]["name"]);
            Assert.AreEqual(tstDataSet[0].OrgId, results.Rows[0]["enum_organization_id"]);
            Assert.AreEqual(tstDataSet[0].Ident, results.Rows[0]["identifier"]);
            Assert.AreEqual(tstDataSet[0].BldgCode, results.Rows[0]["building_code"]);
            Assert.AreEqual(emptyString, results.Rows[0]["organization_parent_id"].ToString());

            Assert.AreEqual(tstDataSet[1].Name, results.Rows[1]["name"]);
            Assert.AreEqual(tstDataSet[1].OrgId, results.Rows[1]["enum_organization_id"]);
            Assert.AreEqual(tstDataSet[1].Ident, results.Rows[1]["identifier"]);
            Assert.AreEqual(tstDataSet[1].BldgCode, results.Rows[1]["building_code"]);
            Assert.AreEqual(tstDataSet[1].ParentOrgId, results.Rows[1]["organization_parent_id"]);
        }

        [TestCase]
        public void SyncChild_Organization_ShouldWriteNewRecord()
        {
            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = tdBuilder.school.Name;
            tstData.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData.Ident = tdBuilder.school.Identifier;
            tstData.BldgCode = tdBuilder.school.SisBuildingCode;
            tstData.ParentOrgId = tdBuilder.district.SisBuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_OrganizationChildRecords("TestTask", 
                new SyncJob_Organization())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithSisBuildingCode);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_WriteWithoutParentId);
        }

        [TestCase]
        public void SyncChild_Organization_ShouldNeitherUpdateNorWriteRecord()
        {
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = tdBuilder.school.Name;
            tstData.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData.Ident = tdBuilder.school.Identifier;
            tstData.BldgCode = tdBuilder.school.SisBuildingCode;
            tstData.ParentOrgId = tdBuilder.district.SisBuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_OrganizationChildRecords("TestTask",
                new SyncJob_Organization())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithSisBuildingCode);
            Assert.IsFalse(mockOrganizationDaoImpl.WasCalled_WriteWithoutParentId);
            Assert.IsFalse(mockOrganizationDaoImpl.WasCalled_UpdateWithoutParentId);
        }

        [TestCase]
        public void SyncChild_Organization_ShouldUpdateRecord_ChangeName()
        {
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = "NewTestName";
            tstData.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData.Ident = tdBuilder.school.Identifier;
            tstData.BldgCode = tdBuilder.school.SisBuildingCode;
            tstData.ParentOrgId = tdBuilder.district.SisBuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_OrganizationChildRecords("TestTask",
                new SyncJob_Organization())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithSisBuildingCode);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_UpdateWithoutParentId);
        }

        [TestCase]
        public void SyncChild_Organization_ShouldUpdateRecord_ChangeOrgType()
        {
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = tdBuilder.school.Name;
            tstData.OrgId = Enum_Organization.values["unset"].Netus2Code;
            tstData.Ident = tdBuilder.school.Identifier;
            tstData.BldgCode = tdBuilder.school.SisBuildingCode;
            tstData.ParentOrgId = tdBuilder.district.SisBuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_OrganizationChildRecords("TestTask",
                new SyncJob_Organization())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithSisBuildingCode);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_UpdateWithoutParentId);
        }

        [TestCase]
        public void SyncChild_Organization_ShouldUpdateRecord_ChangeIdentifier()
        {
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = tdBuilder.school.Name;
            tstData.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData.Ident = "NewIdentifier";
            tstData.BldgCode = tdBuilder.school.SisBuildingCode;
            tstData.ParentOrgId = tdBuilder.district.SisBuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_OrganizationChildRecords("TestTask",
                new SyncJob_Organization())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithSisBuildingCode);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_UpdateWithoutParentId);
        }

        [TestCase]
        public void SyncChild_Organization_ShouldUpdateRecord_ChangeBuildingCode()
        {
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = tdBuilder.school.Name;
            tstData.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData.Ident = tdBuilder.school.Identifier;
            tstData.BldgCode = "NewBuildingCode";
            tstData.ParentOrgId = tdBuilder.district.SisBuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_OrganizationChildRecords("TestTask",
                new SyncJob_Organization())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithSisBuildingCode);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_UpdateWithoutParentId);
        }

        [TestCase]
        public void SyncParent_Organization_ChangeParent_ShouldCallExpectedMethods()
        {
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = tdBuilder.school.Name;
            tstData.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData.Ident = tdBuilder.school.Identifier;
            tstData.BldgCode = tdBuilder.school.SisBuildingCode;
            tstData.ParentOrgId = "NewParentOrg";

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            SetMockForNetus2Objects(tstDataSet);

            SyncJob_Organization syncJob_Organization =
                new SyncJob_Organization();
            SyncTask_OrganizationParentRecords syncTask_OrganizationParentRecords =
                new SyncTask_OrganizationParentRecords("TestTask", syncJob_Organization);

            syncTask_OrganizationParentRecords.Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithoutParentId);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithSisBuildingCode);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_UpdateWithParentId);
        }

        [TestCase]
        public void SyncParent_Organization_RemoveParent_ShouldCallExpectedMethods()
        {
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = tdBuilder.school.Name;
            tstData.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData.Ident = tdBuilder.school.Identifier;
            tstData.BldgCode = tdBuilder.school.SisBuildingCode;
            tstData.ParentOrgId = null;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            SetMockForNetus2Objects(tstDataSet);

            SyncJob_Organization syncJob_Organization =
                new SyncJob_Organization();
            SyncTask_OrganizationParentRecords syncTask_OrganizationParentRecords =
                new SyncTask_OrganizationParentRecords("TestTask", syncJob_Organization);

            syncTask_OrganizationParentRecords.Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithoutParentId);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadParent);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_UpdateWithoutParentId);
        }

        private DataTable BuildTestDataTable(List<SisOrganizationTestData> tstDataSet)
        {
            DataTable dtOrganization = DataTableFactory.CreateDataTable_Sis_Organization();
            foreach (SisOrganizationTestData tstData in tstDataSet)
            {
                DataRow row = dtOrganization.NewRow();
                row["name"] = tstData.Name;
                row["enum_organization_id"] = tstData.OrgId;
                row["identifier"] = tstData.Ident;
                row["building_code"] = tstData.BldgCode;
                row["organization_parent_id"] = tstData.ParentOrgId;
                dtOrganization.Rows.Add(row);
            }

            return dtOrganization;
        }

        private void SetMockForNetus2Objects(List<SisOrganizationTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 11);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = 1;
                        values[1] = tstDataSet[count].Name;
                        values[2] = 1;
                        values[3] = tstDataSet[count].Ident;
                        values[4] = tstDataSet[count].BldgCode;
                        values[5] = "Test HR Building Code";
                        values[6] = 1;
                        values[7] = new DateTime();
                        values[8] = "Test";
                        values[9] = new DateTime();
                        values[10] = "Test";
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "organization_id");
            reader.Setup(x => x.GetOrdinal("organization_id"))
                .Returns(() => 0);

            reader.Setup(x => x.GetName(1))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 1);

            reader.Setup(x => x.GetName(2))
                .Returns(() => "enum_organization_id");
            reader.Setup(x => x.GetOrdinal("enum_organization_id"))
                .Returns(() => 2);

            reader.Setup(x => x.GetName(3))
                .Returns(() => "identifier");
            reader.Setup(x => x.GetOrdinal("identifier"))
                .Returns(() => 3);

            reader.Setup(x => x.GetName(4))
                .Returns(() => "sis_building_code");
            reader.Setup(x => x.GetOrdinal("sis_building_code"))
                .Returns(() => 4);

            reader.Setup(x => x.GetName(5))
                .Returns(() => "hr_building_code");
            reader.Setup(x => x.GetOrdinal("hr_building_code"))
                .Returns(() => 5);

            reader.Setup(x => x.GetName(6))
                .Returns(() => "organization_parent_id");
            reader.Setup(x => x.GetOrdinal("organization_parent_id"))
                .Returns(() => 6);

            reader.Setup(x => x.GetName(7))
                .Returns(() => "created");
            reader.Setup(x => x.GetOrdinal("created"))
                .Returns(() => 7);

            reader.Setup(x => x.GetName(8))
                .Returns(() => "created_by");
            reader.Setup(x => x.GetOrdinal("created_by"))
                .Returns(() => 8);

            reader.Setup(x => x.GetName(9))
                .Returns(() => "changed");
            reader.Setup(x => x.GetOrdinal("changed"))
                .Returns(() => 9);

            reader.Setup(x => x.GetName(10))
                .Returns(() => "changed_by");
            reader.Setup(x => x.GetOrdinal("changed_by"))
                .Returns(() => 10);

            _netus2Connection.mockReader = reader;
        }

        private void SetMockReaderWithTestData(List<SisOrganizationTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 5);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count].Name;
                        values[1] = tstDataSet[count].OrgId;
                        values[2] = tstDataSet[count].Ident;
                        values[3] = tstDataSet[count].BldgCode;
                        values[4] = tstDataSet[count].ParentOrgId;
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "enum_organization_id");
            reader.Setup(x => x.GetOrdinal("enum_organization_id"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "identifier");
            reader.Setup(x => x.GetOrdinal("identifier"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "building_code");
            reader.Setup(x => x.GetOrdinal("building_code"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "organization_parent_id");
            reader.Setup(x => x.GetOrdinal("organization_parent_id"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(string));

            _sisConnection.mockReader = reader;
        }
    }

    class SisOrganizationTestData
    {
        public string Name { get; set; }
        public string OrgId { get; set; }
        public string Ident { get; set; }
        public string BldgCode { get; set; }
        public string ParentOrgId { get; set; }
    }
}