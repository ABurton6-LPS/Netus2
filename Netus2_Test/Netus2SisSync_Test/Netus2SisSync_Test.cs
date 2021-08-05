using NUnit.Framework;
using Netus2_DatabaseConnection.dbAccess;
using Netus2SisSync.UtilityTools;
using Netus2SisSync.SyncProcesses;
using Netus2SisSync.SyncProcesses.Tasks;
using System.Data;
using Moq;
using System.Collections.Generic;
using System;
using Netus2_DatabaseConnection.enumerations;

namespace Netus2_Test.netus2SisSync_Test
{
    class Netus2SisSync_Test
    {
        MockDatabaseConnection mockMiStarDbConnection;
        MockDatabaseConnection mockNetus2DbConnection;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;
            mockMiStarDbConnection = (MockDatabaseConnection)DbConnectionFactory.GetMiStarConnection();
            mockNetus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();
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

            var reader = GetTestData_SisRead_Organization(tstDataSet);

            mockMiStarDbConnection.mockReader = reader;
            SyncJob syncOrganizationJobTest = new SyncJob("TestJob", DateTime.Now);
            DataTable results = SyncOrganization.ReadFromSis(syncOrganizationJobTest, mockMiStarDbConnection, mockNetus2DbConnection);

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

            var reader = GetTestData_SisRead_Organization(tstDataSet);

            mockMiStarDbConnection.mockReader = reader;
            SyncJob syncOrganizationJobTest = new SyncJob("TestJob", DateTime.Now);
            DataTable results = SyncOrganization.ReadFromSis(syncOrganizationJobTest, mockMiStarDbConnection, mockNetus2DbConnection);

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

            CountDownLatch_Mock latch = new CountDownLatch_Mock(0);
            SyncJob job = new SyncJob("TestSyncJob", DateTime.Now);
            List<SisOrganizationTestData> tstDataSet = new List<SisOrganizationTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            SyncOrganization.SyncForChildRecords(job, row, latch);
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

        private Mock<IDataReader> GetTestData_SisRead_Organization(List<SisOrganizationTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 5);

            reader.Setup(x => x.GetValue(0))
                .Returns(() => tstDataSet[count].Name);

            reader.Setup(x => x.GetValue(1))
                .Returns(() => tstDataSet[count].OrgId);

            reader.Setup(x => x.GetValue(2))
                .Returns(() => tstDataSet[count].Ident);

            reader.Setup(x => x.GetValue(3))
                .Returns(() => tstDataSet[count].BldgCode);

            reader.Setup(x => x.GetValue(4))
                .Returns(() => tstDataSet[count].ParOrgId);

            return reader;
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