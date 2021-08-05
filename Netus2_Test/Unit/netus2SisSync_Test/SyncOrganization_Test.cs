using NUnit.Framework;
using Netus2_DatabaseConnection.dbAccess;
using Netus2SisSync.UtilityTools;
using System.Data;
using Moq;
using System.Collections.Generic;
using System;
using Netus2SisSync.SyncProcesses.SyncJobs;
using Netus2SisSync.SyncProcesses.SyncTasks.OrganizationTasks;

namespace Netus2_Test.Unit.netus2SisSync_Test
{
    class SyncOrganization_Test
    {
        MockDatabaseConnection _sisConnection;
        MockDatabaseConnection _netus2DbConnection;
        CountDownLatch_Mock _latch;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;
            _sisConnection = (MockDatabaseConnection)DbConnectionFactory.GetSisConnection();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();
            _latch = new CountDownLatch_Mock(0);
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

            SyncJob_Organization syncJob_Organization = new SyncJob_Organization("TestJob", DateTime.Now, _sisConnection, _netus2DbConnection);
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
            tstData.Name = "TestName";
            tstData.OrgId = "school";
            tstData.Ident = "tst";
            tstData.BldgCode = "tstBldg";
            tstData.ParOrgId = "tstParentId";

            SisOrganizationTestData tstData2 = new SisOrganizationTestData();
            tstData2.Name = "TestName2";
            tstData2.OrgId = "school2";
            tstData2.Ident = "tst2";
            tstData2.BldgCode = "tstBldg2";
            tstData2.ParOrgId = "tstParentId2";

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            tstDataSet.Add(tstData2);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Organization syncJob_Organization = new SyncJob_Organization("TestJob", DateTime.Now, _sisConnection, _netus2DbConnection);
            syncJob_Organization.ReadFromSis();
            DataTable results = syncJob_Organization._dtOrganization;

            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstDataSet[0].Name, results.Rows[0]["name"]);
            Assert.AreEqual(tstDataSet[0].OrgId, results.Rows[0]["enum_organization_id"]);
            Assert.AreEqual(tstDataSet[0].Ident, results.Rows[0]["identifier"]);
            Assert.AreEqual(tstDataSet[0].BldgCode, results.Rows[0]["building_code"]);
            Assert.AreEqual(tstDataSet[0].ParOrgId, results.Rows[0]["organization_parent_id"]);

            Assert.AreEqual(tstDataSet[1].Name, results.Rows[1]["name"]);
            Assert.AreEqual(tstDataSet[1].OrgId, results.Rows[1]["enum_organization_id"]);
            Assert.AreEqual(tstDataSet[1].Ident, results.Rows[1]["identifier"]);
            Assert.AreEqual(tstDataSet[1].BldgCode, results.Rows[1]["building_code"]);
            Assert.AreEqual(tstDataSet[1].ParOrgId, results.Rows[1]["organization_parent_id"]);
        }

        [TestCase]
        public void SyncChild_Organization()
        {
            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = "TestName";
            tstData.OrgId = "tstOrgId";
            tstData.Ident = "tstId";
            tstData.BldgCode = "tstBldg";
            tstData.ParOrgId = "tstParOrgId";

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            SyncJob_Organization syncJob_Organization = 
                new SyncJob_Organization("TestJob", DateTime.Now, _sisConnection, _netus2DbConnection);
            SyncTask_OrganizationChildRecords syncTask_OrganizationChildRecords = 
                new SyncTask_OrganizationChildRecords("TestTask", DateTime.Now, syncJob_Organization);

            syncTask_OrganizationChildRecords.Execute(row, _latch);
        }

        [TestCase]
        public void SyncParent_Organization()
        {
            SisOrganizationTestData tstData = new SisOrganizationTestData();
            tstData.Name = "TestName";
            tstData.OrgId = "tstOrgId";
            tstData.Ident = "tstId";
            tstData.BldgCode = "tstBldg";
            tstData.ParOrgId = "tstParOrgId";

            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            SyncJob_Organization syncJob_Organization =
                new SyncJob_Organization("TestJob", DateTime.Now, _sisConnection, _netus2DbConnection);
            SyncTask_OrganizationParentRecords syncTask_OrganizationParentRecords =
                new SyncTask_OrganizationParentRecords("TestTask", DateTime.Now, syncJob_Organization);

            syncTask_OrganizationParentRecords.Execute(row, _latch);
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