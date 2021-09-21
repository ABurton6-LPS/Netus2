using NUnit.Framework;
using Netus2_DatabaseConnection.dbAccess;
using System.Data;
using Moq;
using System.Collections.Generic;
using System;
using Netus2SisSync.SyncProcesses.SyncJobs;
using Netus2SisSync.SyncProcesses.SyncTasks.AcademicSessionTasks;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_Test.MockDaoImpl;
using Netus2_DatabaseConnection.utilityTools;
using Netus2_DatabaseConnection;

namespace Netus2_Test.Unit.SyncProcess
{
    public class SyncAccademicSession_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _sisConnection;
        MockAcademicSessionDaoImpl mockAcademicSessionDaoImpl;
        MockOrganizationDaoImpl mockOrganizationDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;
            _sisConnection = (MockDatabaseConnection)DbConnectionFactory.GetSisConnection();

            tdBuilder = new TestDataBuilder();
            DaoImplFactory.MockAll = true;
            mockAcademicSessionDaoImpl = new MockAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockAcademicSessionDaoImpl = mockAcademicSessionDaoImpl;
            mockOrganizationDaoImpl = new MockOrganizationDaoImpl(tdBuilder);
            DaoImplFactory.MockOrganizationDaoImpl = mockOrganizationDaoImpl;
        }

        [TestCase]
        public void SisRead_AcademicSession_NullData()
        {
            SisAcademicSessionTestData tstData = new SisAcademicSessionTestData();
            tstData.SchoolCode = null;
            tstData.TermCode = null;
            tstData.SchoolYear = -1;
            tstData.Name = null;
            tstData.SessionId = null;
            tstData.StartDate = new DateTime();
            tstData.EndDate = new DateTime();
            tstData.ParSessionCode = null;

            List<SisAcademicSessionTestData> tstDataSet = new List<SisAcademicSessionTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_AcademicSession syncJob_AcademicSession = new SyncJob_AcademicSession("TestJob");
            syncJob_AcademicSession.ReadFromSis();
            DataTable results = syncJob_AcademicSession._dtAcademicSession;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(emptyString, results.Rows[0]["school_code"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["term_code"].ToString());
            Assert.AreEqual(-1, results.Rows[0]["school_year"]);
            Assert.AreEqual(emptyString, results.Rows[0]["name"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_session_id"].ToString());
            Assert.AreEqual(new DateTime().ToString(), results.Rows[0]["start_date"].ToString());
            Assert.AreEqual(new DateTime().ToString(), results.Rows[0]["end_date"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["parent_session_code"].ToString());
        }

        [TestCase]
        public void SisRead_AcademicSession_TestData()
        {
            SisAcademicSessionTestData tstData = new SisAcademicSessionTestData();
            tstData.SchoolCode = tdBuilder.district.SisBuildingCode;
            tstData.TermCode = tdBuilder.semester1.TermCode;
            tstData.SchoolYear = tdBuilder.semester1.SchoolYear;
            tstData.Name = tdBuilder.semester1.Name;
            tstData.SessionId = tdBuilder.semester1.SessionType.Netus2Code;
            tstData.StartDate = tdBuilder.semester1.StartDate;
            tstData.EndDate = tdBuilder.semester1.EndDate;
            tstData.ParSessionCode = tdBuilder.school.SisBuildingCode + "-" + tdBuilder.schoolYear.TermCode + "-" + tdBuilder.schoolYear.SchoolYear;

            SisAcademicSessionTestData tstData2 = new SisAcademicSessionTestData();
            tstData2.SchoolCode = tdBuilder.district.SisBuildingCode;
            tstData2.TermCode = tdBuilder.semester2.TermCode;
            tstData2.SchoolYear = tdBuilder.semester2.SchoolYear;
            tstData2.Name = tdBuilder.semester2.Name;
            tstData2.SessionId = tdBuilder.semester2.SessionType.Netus2Code;
            tstData2.StartDate = tdBuilder.semester2.StartDate;
            tstData2.EndDate = tdBuilder.semester2.EndDate;
            tstData2.ParSessionCode = tdBuilder.school.SisBuildingCode + "-" + tdBuilder.schoolYear.TermCode + "-" + tdBuilder.schoolYear.SchoolYear;

            List<SisAcademicSessionTestData> tstDataSet = new List<SisAcademicSessionTestData>();
            tstDataSet.Add(tstData);
            tstDataSet.Add(tstData2);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_AcademicSession syncJob_AcademicSession = new SyncJob_AcademicSession("TestJob");
            syncJob_AcademicSession.ReadFromSis();
            DataTable results = syncJob_AcademicSession._dtAcademicSession;

            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstDataSet[0].SchoolCode, results.Rows[0]["school_code"].ToString());
            Assert.AreEqual(tstDataSet[0].TermCode, results.Rows[0]["term_code"].ToString());
            Assert.AreEqual(tstDataSet[0].SchoolYear, results.Rows[0]["school_year"]);
            Assert.AreEqual(tstDataSet[0].Name, results.Rows[0]["name"].ToString());
            Assert.AreEqual(tstDataSet[0].SessionId, results.Rows[0]["enum_session_id"].ToString());
            Assert.AreEqual(tstDataSet[0].StartDate.ToString(), results.Rows[0]["start_date"].ToString());
            Assert.AreEqual(tstDataSet[0].EndDate.ToString(), results.Rows[0]["end_date"].ToString());
            Assert.AreEqual(tstDataSet[0].ParSessionCode, results.Rows[0]["parent_session_code"].ToString());

            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstDataSet[1].SchoolCode, results.Rows[1]["school_code"].ToString());
            Assert.AreEqual(tstDataSet[1].TermCode, results.Rows[1]["term_code"].ToString());
            Assert.AreEqual(tstDataSet[1].SchoolYear, results.Rows[1]["school_year"]);
            Assert.AreEqual(tstDataSet[1].Name, results.Rows[1]["name"].ToString());
            Assert.AreEqual(tstDataSet[1].SessionId, results.Rows[1]["enum_session_id"].ToString());
            Assert.AreEqual(tstDataSet[1].StartDate.ToString(), results.Rows[1]["start_date"].ToString());
            Assert.AreEqual(tstDataSet[1].EndDate.ToString(), results.Rows[1]["end_date"].ToString());
            Assert.AreEqual(tstDataSet[1].ParSessionCode, results.Rows[1]["parent_session_code"].ToString());
        }

        [TestCase]
        public void SyncChild_AcademicSession_ShouldWriteNewRecord()
        {
            SisAcademicSessionTestData tstData = new SisAcademicSessionTestData();
            tstData.SchoolCode = tdBuilder.district.SisBuildingCode;
            tstData.TermCode = tdBuilder.schoolYear.TermCode;
            tstData.SchoolYear = tdBuilder.schoolYear.SchoolYear;
            tstData.Name = tdBuilder.schoolYear.Name;
            tstData.SessionId = tdBuilder.schoolYear.SessionType.Netus2Code;
            tstData.StartDate = tdBuilder.schoolYear.StartDate;
            tstData.EndDate = tdBuilder.schoolYear.EndDate;
            tstData.ParSessionCode = null;

            List<SisAcademicSessionTestData> tstDataSet = new List<SisAcademicSessionTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_AcademicSessionChildRecords("TestTask", 
                new SyncJob_AcademicSession("TestJob"))
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadWithoutParentId);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_WriteWithoutParentId);
        }

        [TestCase]
        public void SyncChild_AcademicSession_ShouldNeitherUpdateNorWriteRecord()
        {
            mockAcademicSessionDaoImpl._shouldReadReturnData = true;
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisAcademicSessionTestData tstData = new SisAcademicSessionTestData();
            tstData.SchoolCode = tdBuilder.district.SisBuildingCode;
            tstData.TermCode = tdBuilder.schoolYear.TermCode;
            tstData.SchoolYear = tdBuilder.schoolYear.SchoolYear;
            tstData.Name = tdBuilder.schoolYear.Name;
            tstData.SessionId = tdBuilder.schoolYear.SessionType.Netus2Code;
            tstData.StartDate = tdBuilder.schoolYear.StartDate;
            tstData.EndDate = tdBuilder.schoolYear.EndDate;
            tstData.ParSessionCode = null;

            List<SisAcademicSessionTestData> tstDataSet = new List<SisAcademicSessionTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_AcademicSessionChildRecords("TestTask",
                new SyncJob_AcademicSession("TestJob"))
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadWithoutParentId);
            Assert.IsFalse(mockAcademicSessionDaoImpl.WasCalled_WriteWithoutParentId);
            Assert.IsFalse(mockAcademicSessionDaoImpl.WasCalled_UpdateWithoutParentId);
        }

        [TestCase]
        public void SyncChild_AcademicSession_ShouldUpdateRecord()
        {
            mockAcademicSessionDaoImpl._shouldReadReturnData = true;
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisAcademicSessionTestData tstData = new SisAcademicSessionTestData();
            tstData.SchoolCode = tdBuilder.district.SisBuildingCode;
            tstData.TermCode = tdBuilder.schoolYear.TermCode;
            tstData.SchoolYear = tdBuilder.schoolYear.SchoolYear;
            tstData.Name = "NewTestName";
            tstData.SessionId = tdBuilder.schoolYear.SessionType.Netus2Code;
            tstData.StartDate = tdBuilder.schoolYear.StartDate;
            tstData.EndDate = tdBuilder.schoolYear.EndDate;
            tstData.ParSessionCode = null;

            List<SisAcademicSessionTestData> tstDataSet = new List<SisAcademicSessionTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_AcademicSessionChildRecords("TestTask",
                new SyncJob_AcademicSession("TestJob"))
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadWithoutParentId);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_UpdateWithoutParentId);
        }

        [TestCase]
        public void SyncParent_AcademicSession()
        {
            mockAcademicSessionDaoImpl._shouldReadReturnData = true;
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            tdBuilder.district.Children.Clear();

            SisAcademicSessionTestData tstData = new SisAcademicSessionTestData();
            tstData.SchoolCode = tdBuilder.district.SisBuildingCode;
            tstData.TermCode = tdBuilder.schoolYear.TermCode;
            tstData.SchoolYear = tdBuilder.schoolYear.SchoolYear;
            tstData.Name = tdBuilder.semester1.Name;
            tstData.SessionId = tdBuilder.semester1.SessionType.Netus2Code;
            tstData.StartDate = tdBuilder.semester1.StartDate;
            tstData.EndDate = tdBuilder.semester1.EndDate;
            tstData.ParSessionCode = tdBuilder.school.SisBuildingCode + "-" + tdBuilder.schoolYear.TermCode + "-" + tdBuilder.schoolYear.SchoolYear;

            List<SisAcademicSessionTestData> tstDataSet = new List<SisAcademicSessionTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            SyncJob_AcademicSession syncJob_AcademicSession =
                new SyncJob_AcademicSession("TestJob");
            SyncTask_AcademicSessionParentRecords syncTask_AcademicSessionParentRecords =
                new SyncTask_AcademicSessionParentRecords("TestTask", syncJob_AcademicSession);

            syncTask_AcademicSessionParentRecords.Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadWithoutParentId);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadUsingSchoolCodeTermCodeSchoolYear);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_UpdateWithParentId);
        }

        [TearDown]
        public void TearDown()
        {
            DbConnectionFactory.MockDatabaseConnection = null;
        }

        private DataTable BuildTestDataTable(List<SisAcademicSessionTestData> tstDataSet)
        {
            DataTable dtAcademicSession = new DataTableFactory().Dt_Sis_AcademicSession;
            foreach(SisAcademicSessionTestData tstData in tstDataSet)
            {
                DataRow row = dtAcademicSession.NewRow();
                row["school_code"] = tstData.SchoolCode;
                row["term_code"] = tstData.TermCode;
                row["school_year"] = tstData.SchoolYear;
                row["name"] = tstData.Name;
                row["enum_session_id"] = tstData.SessionId;
                row["start_date"] = tstData.StartDate;
                row["end_date"] = tstData.EndDate;
                row["parent_session_code"] = tstData.ParSessionCode;
                dtAcademicSession.Rows.Add(row);
            }

            return dtAcademicSession;
        }

        private void SetMockReaderWithTestData(List<SisAcademicSessionTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 8);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count].SchoolCode;
                        values[1] = tstDataSet[count].TermCode;
                        values[2] = tstDataSet[count].SchoolYear;
                        values[3] = tstDataSet[count].Name;
                        values[4] = tstDataSet[count].SessionId;
                        values[5] = tstDataSet[count].StartDate;
                        values[6] = tstDataSet[count].EndDate;
                        values[7] = tstDataSet[count].ParSessionCode;
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "school_code");
            reader.Setup(x => x.GetOrdinal("school_code"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "term_code");
            reader.Setup(x => x.GetOrdinal("term_code"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "school_year");
            reader.Setup(x => x.GetOrdinal("school_year"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "enum_session_id");
            reader.Setup(x => x.GetOrdinal("enum_session_id"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "start_date");
            reader.Setup(x => x.GetOrdinal("start_date"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "end_date");
            reader.Setup(x => x.GetOrdinal("end_date"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(7))
                .Returns(() => "parent_session_code");
            reader.Setup(x => x.GetOrdinal("parent_session_code"))
                .Returns(() => 7);
            reader.Setup(x => x.GetFieldType(7))
                .Returns(() => typeof(string));

            _sisConnection.mockReader = reader;
        }
    }

    class SisAcademicSessionTestData
    {
        public string SchoolCode { get; set; }
        public string TermCode { get; set; }
        public int SchoolYear { get; set; }
        public string Name { get; set; }
        public string SessionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ParSessionCode { get; set; }
    }
}
