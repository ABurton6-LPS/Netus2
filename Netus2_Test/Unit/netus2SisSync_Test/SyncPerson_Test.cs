﻿using NUnit.Framework;
using Netus2_DatabaseConnection.dbAccess;
using System.Data;
using Moq;
using System.Collections.Generic;
using System;
using Netus2SisSync.SyncProcesses.SyncJobs;
using Netus2_Test.MockDaoImpl;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2SisSync.SyncProcesses.SyncTasks.PersonTasks;
using Netus2SisSync.UtilityTools;

namespace Netus2_Test.Unit.netus2SisSync_Test
{
    class SyncPerson_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _sisConnection;
        MockDatabaseConnection _netus2Connection;
        CountDownLatch_Mock _latch;
        MockPersonDaoImpl mockPersonDaoImpl;
        MockUniqueIdentifierDaoImpl mockUniqueIdentifierDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;
            _sisConnection = (MockDatabaseConnection)DbConnectionFactory.GetSisConnection();
            _netus2Connection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();
            _latch = new CountDownLatch_Mock(0);

            tdBuilder = new TestDataBuilder();
            DaoImplFactory.MockAll = true;
            mockPersonDaoImpl = new MockPersonDaoImpl();
            DaoImplFactory.MockPersonDaoImpl = mockPersonDaoImpl;
            mockUniqueIdentifierDaoImpl = new MockUniqueIdentifierDaoImpl();
            DaoImplFactory.MockUniqueIdentifierDaoImpl = mockUniqueIdentifierDaoImpl;
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

            SyncJob_Person syncJob_Person = new SyncJob_Person("TestJob", DateTime.Now, _sisConnection, _netus2Connection);
            syncJob_Person.ReadFromSis();
            DataTable results = syncJob_Person._dtPerson;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(emptyString, results.Rows[0]["person_type"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["SIS_ID"].ToString());
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

        [TestCase]
        public void SisRead_Person_TestData()
        {
            SisPersonTestData tstData = new SisPersonTestData();
            tstData.PersonType = tdBuilder.teacher.Roles[0].Netus2Code;
            tstData.SisId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;
            tstData.FirstName = tdBuilder.teacher.FirstName;
            tstData.MiddleName = tdBuilder.teacher.MiddleName;
            tstData.LastName = tdBuilder.teacher.LastName;
            tstData.BirthDate = tdBuilder.teacher.BirthDate;
            tstData.Gender = tdBuilder.teacher.Gender.Netus2Code;
            tstData.Ethnic = tdBuilder.teacher.Ethnic.Netus2Code;
            tstData.ResStatus = null;
            tstData.LoginName = tdBuilder.teacher.LoginName;
            tstData.LoginPw = tdBuilder.teacher.LoginPw;

            SisPersonTestData tstData2 = new SisPersonTestData();
            tstData2.PersonType = tdBuilder.student.Roles[0].Netus2Code;
            tstData2.SisId = tdBuilder.student.UniqueIdentifiers[0].Identifier;
            tstData2.FirstName = tdBuilder.student.FirstName;
            tstData2.MiddleName = tdBuilder.student.MiddleName;
            tstData2.LastName = tdBuilder.student.LastName;
            tstData2.BirthDate = tdBuilder.student.BirthDate;
            tstData2.Gender = tdBuilder.student.Gender.Netus2Code;
            tstData2.Ethnic = tdBuilder.student.Ethnic.Netus2Code;
            tstData2.ResStatus = tdBuilder.student.ResidenceStatus.Netus2Code;
            tstData2.LoginName = tdBuilder.student.LoginName;
            tstData2.LoginPw = tdBuilder.student.LoginPw;

            List<SisPersonTestData> tstDataSet = new List<SisPersonTestData>();
            tstDataSet.Add(tstData);
            tstDataSet.Add(tstData2);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Person syncJob_Person = new SyncJob_Person("TestJob", DateTime.Now, _sisConnection, _netus2Connection);
            syncJob_Person.ReadFromSis();
            DataTable results = syncJob_Person._dtPerson;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstDataSet[0].PersonType, results.Rows[0]["person_type"]);
            Assert.AreEqual(tstDataSet[0].SisId, results.Rows[0]["SIS_ID"]);
            Assert.AreEqual(tstDataSet[0].FirstName, results.Rows[0]["first_name"]);
            Assert.AreEqual(tstDataSet[0].MiddleName, results.Rows[0]["middle_name"]);
            Assert.AreEqual(tstDataSet[0].LastName, results.Rows[0]["last_name"]);
            Assert.AreEqual(tstDataSet[0].BirthDate, results.Rows[0]["birth_date"]);
            Assert.AreEqual(tstDataSet[0].Gender, results.Rows[0]["enum_gender_id"]);
            Assert.AreEqual(tstDataSet[0].Ethnic, results.Rows[0]["enum_ethnic_id"]);
            Assert.AreEqual(emptyString, results.Rows[0]["enum_residence_status_id"].ToString());
            Assert.AreEqual(tstDataSet[0].LoginName, results.Rows[0]["login_name"]);
            Assert.AreEqual(tstDataSet[0].LoginPw, results.Rows[0]["login_pw"]);

            Assert.AreEqual(tstDataSet[1].PersonType, results.Rows[1]["person_type"]);
            Assert.AreEqual(tstDataSet[1].SisId, results.Rows[1]["SIS_ID"]);
            Assert.AreEqual(tstDataSet[1].FirstName, results.Rows[1]["first_name"]);
            Assert.AreEqual(tstDataSet[1].MiddleName, results.Rows[1]["middle_name"]);
            Assert.AreEqual(tstDataSet[1].LastName, results.Rows[1]["last_name"]);
            Assert.AreEqual(tstDataSet[1].BirthDate, results.Rows[1]["birth_date"]);
            Assert.AreEqual(tstDataSet[1].Gender, results.Rows[1]["enum_gender_id"]);
            Assert.AreEqual(tstDataSet[1].Ethnic, results.Rows[1]["enum_ethnic_id"]);
            Assert.AreEqual(tstDataSet[1].ResStatus, results.Rows[1]["enum_residence_status_id"]);
            Assert.AreEqual(tstDataSet[1].LoginName, results.Rows[1]["login_name"]);
            Assert.AreEqual(tstDataSet[1].LoginPw, results.Rows[1]["login_pw"]);
        }

        [TestCase]
        public void Sync_Person_ShouldWriteNewRecord()
        {
            SisPersonTestData tstData = new SisPersonTestData();
            tstData.PersonType = tdBuilder.student.Roles[0].Netus2Code;
            tstData.SisId = tdBuilder.student.UniqueIdentifiers[0].Identifier;
            tstData.FirstName = tdBuilder.student.FirstName;
            tstData.MiddleName = tdBuilder.student.MiddleName;
            tstData.LastName = tdBuilder.student.LastName;
            tstData.BirthDate = tdBuilder.student.BirthDate;
            tstData.Gender = tdBuilder.student.Gender.Netus2Code;
            tstData.Ethnic = tdBuilder.student.Ethnic.Netus2Code;
            tstData.ResStatus = tdBuilder.student.ResidenceStatus.Netus2Code;
            tstData.LoginName = tdBuilder.student.LoginName;
            tstData.LoginPw = tdBuilder.student.LoginPw;

            List<SisPersonTestData> tstDataSet = new List<SisPersonTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Person("TestTask", DateTime.Now,
                new SyncJob_Person("TestJob", DateTime.Now, _sisConnection, _netus2Connection))
                .Execute(row, _latch);

            Assert.IsTrue(mockUniqueIdentifierDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockPersonDaoImpl.WasCalled_Write);
        }

        [TestCase]
        public void Sync_Person_ShouldUpdateRecord()
        {
            mockUniqueIdentifierDaoImpl.ReadReturnData.Add(tdBuilder.uniqueId_Student);
            mockPersonDaoImpl.ReadReturnData.Add(tdBuilder.student);

            SisPersonTestData tstData = new SisPersonTestData();
            tstData.PersonType = tdBuilder.student.Roles[0].Netus2Code;
            tstData.SisId = tdBuilder.uniqueId_Student.Identifier;
            tstData.FirstName = tdBuilder.student.FirstName;
            tstData.MiddleName = tdBuilder.student.MiddleName;
            tstData.LastName = tdBuilder.student.LastName;
            tstData.BirthDate = tdBuilder.student.BirthDate;
            tstData.Gender = tdBuilder.student.Gender.Netus2Code;
            tstData.Ethnic = tdBuilder.student.Ethnic.Netus2Code;
            tstData.ResStatus = "newtststatus";
            tstData.LoginName = tdBuilder.student.LoginName;
            tstData.LoginPw = tdBuilder.student.LoginPw;

            List<SisPersonTestData> tstDataSet = new List<SisPersonTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Person("TestTask", DateTime.Now,
                new SyncJob_Person("TestJob", DateTime.Now, _sisConnection, _netus2Connection))
                .Execute(row, _latch);

            Assert.IsTrue(mockUniqueIdentifierDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockPersonDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockPersonDaoImpl.WasCalled_Update);
        }

        private DataTable BuildTestDataTable(List<SisPersonTestData> tstDataSet)
        {
            DataTable dtPerson = DataTableFactory.CreateDataTable("Person");
            foreach(SisPersonTestData tstData in tstDataSet)
            {
                DataRow row = dtPerson.NewRow();
                row["person_type"] = tstData.PersonType;
                row["SIS_ID"] = tstData.SisId;
                row["first_name"] = tstData.FirstName;
                row["middle_name"] = tstData.MiddleName;
                row["last_name"] = tstData.LastName;
                row["birth_date"] = tstData.BirthDate;
                row["enum_gender_id"] = tstData.Gender;
                row["enum_ethnic_id"] = tstData.Ethnic;
                row["enum_residence_status_id"] = tstData.ResStatus;
                row["login_name"] = tstData.LoginName;
                row["login_pw"] = tstData.LoginPw;
                dtPerson.Rows.Add(row);
            }

            return dtPerson;
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
        public string SisId { get; set; }
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
