using NUnit.Framework;
using Netus2_DatabaseConnection.dbAccess;
using Netus2SisSync.UtilityTools;
using System.Data;
using Moq;
using System.Collections.Generic;
using System;
using Netus2SisSync.SyncProcesses.SyncJobs;
using Netus2SisSync.SyncProcesses.SyncTasks.AcademicSessionTasks;

namespace Netus2_Test.Unit.netus2SisSync_Test
{
    public class SyncAccademicSession_Test
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
        public void SisRead_AcademicSession_NullData()
        {
            SisAcademicSessionTestData tstData = new SisAcademicSessionTestData();
            tstData.SessionCode = null;
            tstData.Name = null;
            tstData.SessionId = null;
            tstData.StartDate = null;
            tstData.EndDate = null;
            tstData.ParSessionCode = null;
            tstData.OrgId = null;

            List<SisAcademicSessionTestData> tstDataSet = new List<SisAcademicSessionTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_AcademicSession syncJob_AcademicSession = new SyncJob_AcademicSession("TestJob", DateTime.Now, _sisConnection, _netus2DbConnection);
            syncJob_AcademicSession.ReadFromSis();
            DataTable results = syncJob_AcademicSession._dtAcademicSession;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(emptyString, results.Rows[0]["session_code"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["name"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_session_id"].ToString());
            Assert.AreEqual(new DateTime().ToString(), results.Rows[0]["start_date"].ToString());
            Assert.AreEqual(new DateTime().ToString(), results.Rows[0]["end_date"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["parent_session_code"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["organization_id"].ToString());
        }

        private void SetMockReaderWithTestData(List<SisAcademicSessionTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 7);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "session_code");
            reader.Setup(x => x.GetOrdinal("session_code"))
                .Returns(() => 0);
            reader.Setup(x => x.GetValue(0))
                .Returns(() => tstDataSet[count].SessionCode);

            reader.Setup(x => x.GetName(1))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 1);
            reader.Setup(x => x.GetValue(1))
                .Returns(() => tstDataSet[count].Name);

            reader.Setup(x => x.GetName(2))
                .Returns(() => "enum_session_id");
            reader.Setup(x => x.GetOrdinal("enum_session_id"))
                .Returns(() => 2);
            reader.Setup(x => x.GetValue(2))
                .Returns(() => tstDataSet[count].SessionId);

            reader.Setup(x => x.GetName(3))
                .Returns(() => "start_date");
            reader.Setup(x => x.GetOrdinal("start_date"))
                .Returns(() => 3);
            reader.Setup(x => x.GetValue(3))
                .Returns(() => tstDataSet[count].StartDate);

            reader.Setup(x => x.GetName(4))
                .Returns(() => "end_date");
            reader.Setup(x => x.GetOrdinal("end_date"))
                .Returns(() => 4);
            reader.Setup(x => x.GetValue(4))
                .Returns(() => tstDataSet[count].EndDate);

            reader.Setup(x => x.GetName(5))
                .Returns(() => "parent_session_code");
            reader.Setup(x => x.GetOrdinal("parent_session_code"))
                .Returns(() => 5);
            reader.Setup(x => x.GetValue(5))
                .Returns(() => tstDataSet[count].ParSessionCode);

            reader.Setup(x => x.GetName(6))
                .Returns(() => "organization_id");
            reader.Setup(x => x.GetOrdinal("organization_id"))
                .Returns(() => 6);
            reader.Setup(x => x.GetValue(6))
                .Returns(() => tstDataSet[count].OrgId);

            _sisConnection.mockReader = reader;
        }
    }

    class SisAcademicSessionTestData
    {
        public string SessionCode { get; set; }
        public string Name { get; set; }
        public string SessionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ParSessionCode { get; set; }
        public string OrgId { get; set; }
    }
}
