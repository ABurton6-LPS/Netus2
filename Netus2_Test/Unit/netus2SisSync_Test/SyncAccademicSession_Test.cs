using NUnit.Framework;
using Netus2_DatabaseConnection.dbAccess;
using Netus2SisSync.UtilityTools;
using System.Data;
using Moq;
using System.Collections.Generic;
using System;
using Netus2SisSync.SyncProcesses.SyncJobs;
using Netus2SisSync.SyncProcesses.SyncTasks.AcademicSessionTasks;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_Test.MockDaoImpl;

namespace Netus2_Test.Unit.netus2SisSync_Test
{
    public class SyncAccademicSession_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _sisConnection;
        MockDatabaseConnection _netus2DbConnection;
        MockAcademicSessionDaoImpl mockAcademicSessionDaoImpl;
        MockOrganizationDaoImpl mockOrganizationDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;
            _sisConnection = (MockDatabaseConnection)DbConnectionFactory.GetSisConnection();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

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
            tstData.SchoolYear = null;
            tstData.Name = null;
            tstData.SessionId = null;
            tstData.StartDate = null;
            tstData.EndDate = null;
            tstData.ParSessionCode = null;

            List<SisAcademicSessionTestData> tstDataSet = new List<SisAcademicSessionTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_AcademicSession syncJob_AcademicSession = new SyncJob_AcademicSession("TestJob", _sisConnection, _netus2DbConnection);
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
            tstData.SchoolCode = tdBuilder.district.BuildingCode;
            tstData.TermCode = tdBuilder.semester1.TermCode;
            tstData.SchoolYear = tdBuilder.semester1.SchoolYear;
            tstData.Name = tdBuilder.semester1.Name;
            tstData.SessionId = tdBuilder.semester1.SessionType.Netus2Code;
            tstData.StartDate = tdBuilder.semester1.StartDate;
            tstData.EndDate = tdBuilder.semester1.EndDate;
            tstData.ParSessionCode = tdBuilder.school.BuildingCode + "-" + tdBuilder.schoolYear.TermCode + "-" + tdBuilder.schoolYear.SchoolYear;

            SisAcademicSessionTestData tstData2 = new SisAcademicSessionTestData();
            tstData2.SchoolCode = tdBuilder.district.BuildingCode;
            tstData2.TermCode = tdBuilder.semester2.TermCode;
            tstData2.SchoolYear = tdBuilder.semester2.SchoolYear;
            tstData2.Name = tdBuilder.semester2.Name;
            tstData2.SessionId = tdBuilder.semester2.SessionType.Netus2Code;
            tstData2.StartDate = tdBuilder.semester2.StartDate;
            tstData2.EndDate = tdBuilder.semester2.EndDate;
            tstData2.ParSessionCode = tdBuilder.school.BuildingCode + "-" + tdBuilder.schoolYear.TermCode + "-" + tdBuilder.schoolYear.SchoolYear;

            List<SisAcademicSessionTestData> tstDataSet = new List<SisAcademicSessionTestData>();
            tstDataSet.Add(tstData);
            tstDataSet.Add(tstData2);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_AcademicSession syncJob_AcademicSession = new SyncJob_AcademicSession("TestJob", _sisConnection, _netus2DbConnection);
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
            tstData.SchoolCode = tdBuilder.district.BuildingCode;
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
                new SyncJob_AcademicSession("TestJob", _sisConnection, _netus2DbConnection))
                .Execute(row);

            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadWithoutParentId);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_WriteWithoutParentId);
        }

        [TestCase]
        public void SyncChild_AcademicSession_ShouldNeitherUpdateNorWriteRecord()
        {
            mockAcademicSessionDaoImpl._shouldReadReturnData = true;
            mockOrganizationDaoImpl._shouldReadReturnData = true;

            SisAcademicSessionTestData tstData = new SisAcademicSessionTestData();
            tstData.SchoolCode = tdBuilder.district.BuildingCode;
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
                new SyncJob_AcademicSession("TestJob", _sisConnection, _netus2DbConnection))
                .Execute(row);

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
            tstData.SchoolCode = tdBuilder.district.BuildingCode;
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
                new SyncJob_AcademicSession("TestJob", _sisConnection, _netus2DbConnection))
                .Execute(row);

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
            tstData.SchoolCode = tdBuilder.district.BuildingCode;
            tstData.TermCode = tdBuilder.schoolYear.TermCode;
            tstData.SchoolYear = tdBuilder.schoolYear.SchoolYear;
            tstData.Name = tdBuilder.semester1.Name;
            tstData.SessionId = tdBuilder.semester1.SessionType.Netus2Code;
            tstData.StartDate = tdBuilder.semester1.StartDate;
            tstData.EndDate = tdBuilder.semester1.EndDate;
            tstData.ParSessionCode = tdBuilder.school.BuildingCode + "-" + tdBuilder.schoolYear.TermCode + "-" + tdBuilder.schoolYear.SchoolYear;

            List<SisAcademicSessionTestData> tstDataSet = new List<SisAcademicSessionTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            SyncJob_AcademicSession syncJob_AcademicSession =
                new SyncJob_AcademicSession("TestJob", _sisConnection, _netus2DbConnection);
            SyncTask_AcademicSessionParentRecords syncTask_AcademicSessionParentRecords =
                new SyncTask_AcademicSessionParentRecords("TestTask", syncJob_AcademicSession);

            syncTask_AcademicSessionParentRecords.Execute(row);

            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadWithoutParentId);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadUsingSchoolCodeTermCodeSchoolYear);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_UpdateWithParentId);
        }

        private DataTable BuildTestDataTable(List<SisAcademicSessionTestData> tstDataSet)
        {
            DataTable dtAcademicSession = DataTableFactory.CreateDataTable("AcademicSession");
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

            reader.Setup(x => x.GetName(0))
                .Returns(() => "school_code");
            reader.Setup(x => x.GetOrdinal("school_code"))
                .Returns(() => 0);
            reader.Setup(x => x.GetValue(0))
                .Returns(() => tstDataSet[count].SchoolCode);

            reader.Setup(x => x.GetName(1))
                .Returns(() => "term_code");
            reader.Setup(x => x.GetOrdinal("term_code"))
                .Returns(() => 1);
            reader.Setup(x => x.GetValue(1))
                .Returns(() => tstDataSet[count].TermCode);

            reader.Setup(x => x.GetName(2))
                .Returns(() => "school_year");
            reader.Setup(x => x.GetOrdinal("school_year"))
                .Returns(() => 2);
            reader.Setup(x => x.GetValue(2))
                .Returns(() => tstDataSet[count].SchoolYear);

            reader.Setup(x => x.GetName(3))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 3);
            reader.Setup(x => x.GetValue(3))
                .Returns(() => tstDataSet[count].Name);

            reader.Setup(x => x.GetName(4))
                .Returns(() => "enum_session_id");
            reader.Setup(x => x.GetOrdinal("enum_session_id"))
                .Returns(() => 4);
            reader.Setup(x => x.GetValue(4))
                .Returns(() => tstDataSet[count].SessionId);

            reader.Setup(x => x.GetName(5))
                .Returns(() => "start_date");
            reader.Setup(x => x.GetOrdinal("start_date"))
                .Returns(() => 5);
            reader.Setup(x => x.GetValue(5))
                .Returns(() => tstDataSet[count].StartDate);

            reader.Setup(x => x.GetName(6))
                .Returns(() => "end_date");
            reader.Setup(x => x.GetOrdinal("end_date"))
                .Returns(() => 6);
            reader.Setup(x => x.GetValue(6))
                .Returns(() => tstDataSet[count].EndDate);

            reader.Setup(x => x.GetName(7))
                .Returns(() => "parent_session_code");
            reader.Setup(x => x.GetOrdinal("parent_session_code"))
                .Returns(() => 7);
            reader.Setup(x => x.GetValue(7))
                .Returns(() => tstDataSet[count].ParSessionCode);

            _sisConnection.mockReader = reader;
        }
    }

    class SisAcademicSessionTestData
    {
        public string SchoolCode { get; set; }
        public string TermCode { get; set; }
        public int? SchoolYear { get; set; }
        public string Name { get; set; }
        public string SessionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ParSessionCode { get; set; }
    }
}
