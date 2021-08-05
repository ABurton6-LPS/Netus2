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
    class SyncPerson_Test
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
        public void SisRead_Person_NullData()
        {
            SisPersonTestData tstData = new SisPersonTestData();
            tstData.PersonType = null;
            tstData.SisId = null;
            tstData.FirstName = null;
            tstData.MiddleName = null;
            tstData.LastName = null;
            tstData.BirthDate = null;
            tstData.Gender = null;
            tstData.Ethnic = null;
            tstData.ResStatus = null;
            tstData.LoginName = null;
            tstData.LoginPw = null;

            List<SisPersonTestData> tstDataSet = new List<SisPersonTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Person syncJob_Person = new SyncJob_Person("TestJob", DateTime.Now, _sisConnection, _netus2DbConnection);
            syncJob_Person.ReadFromSis();
            DataTable results = syncJob_Person._dtPerson;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(emptyString, results.Rows[0]["person_type"].ToString());
            Assert.AreEqual(-1, results.Rows[0]["SIS_ID"]);
            Assert.AreEqual(emptyString, results.Rows[0]["first_name"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["middle_name"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["last_name"].ToString());
            Assert.AreEqual(new DateTime().ToString(), results.Rows[0]["birth_date"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_gender_id"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_ethnic_id"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_residence_status_id"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["login_name"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["login_pw"].ToString());
        }

        private void SetMockReaderWithTestData(List<SisPersonTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 11);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "person_type");
            reader.Setup(x => x.GetOrdinal("person_type"))
                .Returns(() => 0);
            reader.Setup(x => x.GetValue(0))
                .Returns(() => tstDataSet[count].PersonType);

            reader.Setup(x => x.GetName(1))
                .Returns(() => "SIS_ID");
            reader.Setup(x => x.GetOrdinal("SIS_ID"))
                .Returns(() => 1);
            reader.Setup(x => x.GetValue(1))
                .Returns(() => tstDataSet[count].SisId);

            reader.Setup(x => x.GetName(2))
                .Returns(() => "first_name");
            reader.Setup(x => x.GetOrdinal("first_name"))
                .Returns(() => 2);
            reader.Setup(x => x.GetValue(2))
                .Returns(() => tstDataSet[count].FirstName);

            reader.Setup(x => x.GetName(3))
                .Returns(() => "middle_name");
            reader.Setup(x => x.GetOrdinal("middle_name"))
                .Returns(() => 3);
            reader.Setup(x => x.GetValue(3))
                .Returns(() => tstDataSet[count].MiddleName);

            reader.Setup(x => x.GetName(4))
                .Returns(() => "last_name");
            reader.Setup(x => x.GetOrdinal("last_name"))
                .Returns(() => 4);
            reader.Setup(x => x.GetValue(4))
                .Returns(() => tstDataSet[count].LastName);

            reader.Setup(x => x.GetName(5))
                .Returns(() => "birth_date");
            reader.Setup(x => x.GetOrdinal("birth_date"))
                .Returns(() => 5);
            reader.Setup(x => x.GetValue(5))
                .Returns(() => tstDataSet[count].BirthDate);

            reader.Setup(x => x.GetName(6))
                .Returns(() => "enum_gender_id");
            reader.Setup(x => x.GetOrdinal("enum_gender_id"))
                .Returns(() => 6);
            reader.Setup(x => x.GetValue(6))
                .Returns(() => tstDataSet[count].Gender);

            reader.Setup(x => x.GetName(7))
                .Returns(() => "enum_ethnic_id");
            reader.Setup(x => x.GetOrdinal("enum_ethnic_id"))
                .Returns(() => 7);
            reader.Setup(x => x.GetValue(7))
                .Returns(() => tstDataSet[count].Ethnic);

            reader.Setup(x => x.GetName(8))
                .Returns(() => "enum_residence_status_id");
            reader.Setup(x => x.GetOrdinal("enum_residence_status_id"))
                .Returns(() => 8);
            reader.Setup(x => x.GetValue(8))
                .Returns(() => tstDataSet[count].ResStatus);

            reader.Setup(x => x.GetName(9))
                .Returns(() => "login_name");
            reader.Setup(x => x.GetOrdinal("login_name"))
                .Returns(() => 9);
            reader.Setup(x => x.GetValue(9))
                .Returns(() => tstDataSet[count].LoginName);

            reader.Setup(x => x.GetName(10))
                .Returns(() => "login_pw");
            reader.Setup(x => x.GetOrdinal("login_pw"))
                .Returns(() => 10);
            reader.Setup(x => x.GetValue(10))
                .Returns(() => tstDataSet[count].LoginPw);

            _sisConnection.mockReader = reader;
        }
    }

    class SisPersonTestData
    {
        public string PersonType { get; set; }
        public int? SisId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Ethnic { get; set; }
        public string ResStatus { get; set; }
        public string LoginName { get; set; }
        public string LoginPw { get; set; }
    }
}
