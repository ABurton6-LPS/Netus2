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

namespace Netus2_Test.Unit
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
            DbConnectionFactory.TestMode = true;
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
            tstData.ParOrgId = null;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Organization syncJob_Organization = new SyncJob_Organization("TestJob", _sisConnection, _netus2Connection);
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
            tstData.BldgCode = tdBuilder.district.BuildingCode;
            tstData.ParOrgId = null;

            SisOrganizationTestData tstData2 = new SisOrganizationTestData();
            tstData2.Name = tdBuilder.school.Name;
            tstData2.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData2.Ident = tdBuilder.school.Identifier;
            tstData2.BldgCode = tdBuilder.school.BuildingCode;
            tstData2.ParOrgId = tdBuilder.district.BuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            tstDataSet.Add(tstData2);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Organization syncJob_Organization = new SyncJob_Organization("TestJob", _sisConnection, _netus2Connection);
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
            Assert.AreEqual(tstDataSet[1].ParOrgId, results.Rows[1]["organization_parent_id"]);
        }

        [TestCase]
        public void SyncChild_Organization_ShouldWriteNewRecord()
        {
            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = tdBuilder.school.Name;
            tstData.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData.Ident = tdBuilder.school.Identifier;
            tstData.BldgCode = tdBuilder.school.BuildingCode;
            tstData.ParOrgId = tdBuilder.district.BuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_OrganizationChildRecords("TestTask", 
                new SyncJob_Organization("TestJob", _sisConnection, _netus2Connection))
                .Execute(row);

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithoutParentId);
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
            tstData.BldgCode = tdBuilder.school.BuildingCode;
            tstData.ParOrgId = tdBuilder.district.BuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_OrganizationChildRecords("TestTask",
                new SyncJob_Organization("TestJob", _sisConnection, _netus2Connection))
                .Execute(row);

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithoutParentId);
            Assert.IsFalse(mockOrganizationDaoImpl.WasCalled_WriteWithoutParentId);
            Assert.IsFalse(mockOrganizationDaoImpl.WasCalled_UpdateWithoutParentId);
        }

        [TestCase]
        public void SyncChild_Organization_ShouldUpdateRecord()
        {
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = "NewTestName";
            tstData.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData.Ident = tdBuilder.school.Identifier;
            tstData.BldgCode = tdBuilder.school.BuildingCode;
            tstData.ParOrgId = tdBuilder.district.BuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_OrganizationChildRecords("TestTask",
                new SyncJob_Organization("TestJob", _sisConnection, _netus2Connection))
                .Execute(row);

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithoutParentId);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_UpdateWithoutParentId);
        }

        [TestCase]
        public void SyncParent_Organization()
        {
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = tdBuilder.school.Name;
            tstData.OrgId = tdBuilder.school.OrganizationType.Netus2Code;
            tstData.Ident = tdBuilder.school.Identifier;
            tstData.BldgCode = tdBuilder.school.BuildingCode;
            tstData.ParOrgId = tdBuilder.district.BuildingCode;

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            SetMockForNetus2Objects(tstDataSet);

            SyncJob_Organization syncJob_Organization =
                new SyncJob_Organization("TestJob", _sisConnection, _netus2Connection);
            SyncTask_OrganizationParentRecords syncTask_OrganizationParentRecords =
                new SyncTask_OrganizationParentRecords("TestTask", syncJob_Organization);

            syncTask_OrganizationParentRecords.Execute(row);

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithoutParentId);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithBuildingCode);
            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_UpdateWithParentId);
        }

        [TearDown]
        public void TearDown()
        {
            DbConnectionFactory.MockDatabaseConnection = null;
        }

        private DataTable BuildTestDataTable(List<SisOrganizationTestData> tstDataSet)
        {
            DataTable dtOrganization = DataTableFactory.CreateDataTable("Organization");
            foreach (SisOrganizationTestData tstData in tstDataSet)
            {
                DataRow row = dtOrganization.NewRow();
                row["name"] = tstData.Name;
                row["enum_organization_id"] = tstData.OrgId;
                row["identifier"] = tstData.Ident;
                row["building_code"] = tstData.BldgCode;
                row["organization_parent_id"] = tstData.ParOrgId;
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
                .Returns(() => 8);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "organization_id");
            reader.Setup(x => x.GetOrdinal("organization_id"))
                .Returns(() => 0);
            reader.Setup(x => x.GetValue(0))
                .Returns(() => 1);

            reader.Setup(x => x.GetName(1))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 1);
            reader.Setup(x => x.GetValue(1))
                .Returns(() => tstDataSet[count].Name);

            reader.Setup(x => x.GetName(2))
                .Returns(() => "enum_organization_id");
            reader.Setup(x => x.GetOrdinal("enum_organization_id"))
                .Returns(() => 2);
            reader.Setup(x => x.GetValue(2))
                .Returns(() => 1);

            reader.Setup(x => x.GetName(3))
                .Returns(() => "identifier");
            reader.Setup(x => x.GetOrdinal("identifier"))
                .Returns(() => 3);
            reader.Setup(x => x.GetValue(3))
                .Returns(() => tstDataSet[count].Ident);

            reader.Setup(x => x.GetName(4))
                .Returns(() => "building_code");
            reader.Setup(x => x.GetOrdinal("building_code"))
                .Returns(() => 4);
            reader.Setup(x => x.GetValue(4))
                .Returns(() => tstDataSet[count].BldgCode);

            reader.Setup(x => x.GetName(5))
                .Returns(() => "organization_parent_id");
            reader.Setup(x => x.GetOrdinal("organization_parent_id"))
                .Returns(() => 5);
            reader.Setup(x => x.GetValue(5))
                .Returns(() => 1);

            reader.Setup(x => x.GetName(6))
                .Returns(() => "created");
            reader.Setup(x => x.GetOrdinal("created"))
                .Returns(() => 6);
            reader.Setup(x => x.GetValue(6))
                .Returns(() => new DateTime());

            reader.Setup(x => x.GetName(7))
                .Returns(() => "created_by");
            reader.Setup(x => x.GetOrdinal("created_by"))
                .Returns(() => 7);
            reader.Setup(x => x.GetValue(7))
                .Returns(() => "Test");

            reader.Setup(x => x.GetName(8))
                .Returns(() => "changed");
            reader.Setup(x => x.GetOrdinal("changed"))
                .Returns(() => 8);
            reader.Setup(x => x.GetValue(8))
                .Returns(() => new DateTime());

            reader.Setup(x => x.GetName(9))
                .Returns(() => "changed_by");
            reader.Setup(x => x.GetOrdinal("changed_by"))
                .Returns(() => 9);
            reader.Setup(x => x.GetValue(9))
                .Returns(() => "Test");

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

            reader.Setup(x => x.GetName(0))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 0);
            reader.Setup(x => x.GetValue(0))
                .Returns(() => tstDataSet[count].Name);

            reader.Setup(x => x.GetName(1))
                .Returns(() => "enum_organization_id");
            reader.Setup(x => x.GetOrdinal("enum_organization_id"))
                .Returns(() => 1);
            reader.Setup(x => x.GetValue(1))
                .Returns(() => tstDataSet[count].OrgId);

            reader.Setup(x => x.GetName(2))
                .Returns(() => "identifier");
            reader.Setup(x => x.GetOrdinal("identifier"))
                .Returns(() => 2);
            reader.Setup(x => x.GetValue(2))
                .Returns(() => tstDataSet[count].Ident);

            reader.Setup(x => x.GetName(3))
                .Returns(() => "building_code");
            reader.Setup(x => x.GetOrdinal("building_code"))
                .Returns(() => 3);
            reader.Setup(x => x.GetValue(3))
                .Returns(() => tstDataSet[count].BldgCode);

            reader.Setup(x => x.GetName(4))
                .Returns(() => "organization_parent_id");
            reader.Setup(x => x.GetOrdinal("organization_parent_id"))
                .Returns(() => 4);
            reader.Setup(x => x.GetValue(4))
                .Returns(() => tstDataSet[count].ParOrgId);

            _sisConnection.mockReader = reader;
        }
    }

    class SisOrganizationTestData
    {
        public string Name { get; set; }
        public string OrgId { get; set; }
        public string Ident { get; set; }
        public string BldgCode { get; set; }
        public string ParOrgId { get; set; }
    }
}