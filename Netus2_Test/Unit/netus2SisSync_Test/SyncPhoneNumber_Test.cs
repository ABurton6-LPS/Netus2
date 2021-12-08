using Moq;
using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2_Test.MockDaoImpl;
using Netus2SisSync.SyncProcesses.SyncJobs;
using Netus2SisSync.SyncProcesses.SyncTasks.PhoneNumberTasks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.Unit.SyncProcess
{
    public class SyncPhoneNumber_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _sisConnection;
        MockPhoneNumberDaoImpl mockPhoneNumberDaoImpl;
        MockJctPersonPhoneNumberDaoImpl mockJctPersonPhoneNumberDaoImpl;
        MockPersonDaoImpl mockPersonDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;
            _sisConnection = (MockDatabaseConnection)DbConnectionFactory.GetSisConnection();

            tdBuilder = new TestDataBuilder();
            DaoImplFactory.MockAll = true;
            mockPhoneNumberDaoImpl = new MockPhoneNumberDaoImpl(tdBuilder);
            DaoImplFactory.MockPhoneNumberDaoImpl = mockPhoneNumberDaoImpl;
            mockJctPersonPhoneNumberDaoImpl = new MockJctPersonPhoneNumberDaoImpl(tdBuilder);
            DaoImplFactory.MockJctPersonPhoneNumberDaoImpl = mockJctPersonPhoneNumberDaoImpl;
            mockPersonDaoImpl = new MockPersonDaoImpl(tdBuilder);
            DaoImplFactory.MockPersonDaoImpl = mockPersonDaoImpl;
        }

        [TestCase]
        public void SisRead_PhoneNumber_NullData()
        {
            SisPhoneNumberTestData tstData = new SisPhoneNumberTestData();
            tstData.PhoneNumber = null;
            tstData.PhoneType = null;

            List<SisPhoneNumberTestData> tstDataSet = new List<SisPhoneNumberTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_PhoneNumber syncJob_PhoneNumber = new SyncJob_PhoneNumber();
            syncJob_PhoneNumber.ReadFromSis();
            DataTable results = syncJob_PhoneNumber._dtPhoneNumber;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(emptyString, results.Rows[0]["phone_number"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["phone_type_code"].ToString());
        }

        [TestCase]
        public void SisRead_PhoneNumber_TestData()
        {
            SisPhoneNumberTestData tstData = new SisPhoneNumberTestData();
            tstData.PhoneNumber = tdBuilder.teacher.PhoneNumbers[0].PhoneNumberValue;
            tstData.PhoneType = tdBuilder.teacher.PhoneNumbers[0].PhoneType.SisCode;

            SisPhoneNumberTestData tstData2 = new SisPhoneNumberTestData();
            tstData2.PhoneNumber = tdBuilder.student.PhoneNumbers[0].PhoneNumberValue;
            tstData2.PhoneType = tdBuilder.student.PhoneNumbers[0].PhoneType.SisCode;

            List<SisPhoneNumberTestData> tstDataSet = new List<SisPhoneNumberTestData>();
            tstDataSet.Add(tstData);
            tstDataSet.Add(tstData2);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_PhoneNumber syncJob_PhoneNumber = new SyncJob_PhoneNumber();
            syncJob_PhoneNumber.ReadFromSis();
            DataTable results = syncJob_PhoneNumber._dtPhoneNumber;

            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstData.PhoneNumber, results.Rows[0]["phone_number"].ToString());
            Assert.AreEqual(tstData.PhoneType, results.Rows[0]["phone_type_code"].ToString());

            Assert.AreEqual(tstData2.PhoneNumber, results.Rows[1]["phone_number"].ToString());
            Assert.AreEqual(tstData2.PhoneType, results.Rows[1]["phone_type_code"].ToString());
        }

        [TestCase]
        public void SisRead_JctPersonPhoneNumber_NullData()
        {
            SisJctPersonPhoneNumberTestData tstData = new SisJctPersonPhoneNumberTestData();
            tstData.PhoneNumber = null;
            tstData.PersonId = null;
            tstData.IsPrimary = false;

            List<SisJctPersonPhoneNumberTestData> tstDataSet = new List<SisJctPersonPhoneNumberTestData>();
            tstDataSet.Add(tstData);

            SetMockJctReaderWithTestData(tstDataSet);

            SyncJob_PhoneNumber syncJobPhoneNumber = new SyncJob_PhoneNumber();
            syncJobPhoneNumber.ReadFromSisJctPersonPhoneNumber(_sisConnection);
            DataTable results = syncJobPhoneNumber._dtJctPersonPhoneNumber;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(emptyString, results.Rows[0]["phone_number"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["unique_id"].ToString());            
            Assert.AreEqual(tstData.IsPrimary.ToString(), results.Rows[0]["is_primary"].ToString());
        }

        [TestCase]
        public void SisRead_JctPersonPhoneNumber_TestData()
        {
            SisJctPersonPhoneNumberTestData tstData = new SisJctPersonPhoneNumberTestData();
            tstData.PhoneNumber = tdBuilder.teacher.PhoneNumbers[0].PhoneNumberValue;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;
            tstData.IsPrimary = tdBuilder.teacher.PhoneNumbers[0].IsPrimary;

            List<SisJctPersonPhoneNumberTestData> tstDataSet = new List<SisJctPersonPhoneNumberTestData>();
            tstDataSet.Add(tstData);

            SetMockJctReaderWithTestData(tstDataSet);

            SyncJob_PhoneNumber syncJobPhoneNumber = new SyncJob_PhoneNumber();
            syncJobPhoneNumber.ReadFromSisJctPersonPhoneNumber(_sisConnection);
            DataTable results = syncJobPhoneNumber._dtJctPersonPhoneNumber;

            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstData.PhoneNumber, results.Rows[0]["phone_number"].ToString());
            Assert.AreEqual(tstData.PersonId, results.Rows[0]["unique_id"].ToString());
            Assert.AreEqual(tstData.IsPrimary.ToString(), results.Rows[0]["is_primary"].ToString());
        }

        [TestCase]
        public void SyncPhoneNumber_ShouldWriteNewRecord()
        {
            SisPhoneNumberTestData tstData = new SisPhoneNumberTestData();
            tstData.PhoneNumber = tdBuilder.teacher.PhoneNumbers[0].PhoneNumberValue;
            tstData.PhoneType = tdBuilder.teacher.PhoneNumbers[0].PhoneType.SisCode;

            List<SisPhoneNumberTestData> tstDataSet = new List<SisPhoneNumberTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_PhoneNumber("TestTask",
                new SyncJob_PhoneNumber())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockPhoneNumberDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockPhoneNumberDaoImpl.WasCalled_Write);
            Assert.IsFalse(mockPhoneNumberDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncPhoneNumber_ShouldNeitherUpdateNorWriteNewRecord()
        {
            mockPhoneNumberDaoImpl._shouldReadReturnData = true;

            SisPhoneNumberTestData tstData = new SisPhoneNumberTestData();
            tstData.PhoneNumber = tdBuilder.teacher.PhoneNumbers[0].PhoneNumberValue;
            tstData.PhoneType = tdBuilder.teacher.PhoneNumbers[0].PhoneType.SisCode;

            List<SisPhoneNumberTestData> tstDataSet = new List<SisPhoneNumberTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_PhoneNumber("TestTask",
                new SyncJob_PhoneNumber())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockPhoneNumberDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockPhoneNumberDaoImpl.WasCalled_Write);
            Assert.IsFalse(mockPhoneNumberDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncPhoneNumber_ShouldUpdateRecord_DifferentPhoneType()
        {
            mockPhoneNumberDaoImpl._shouldReadReturnData = true;

            SisPhoneNumberTestData tstData = new SisPhoneNumberTestData();
            tstData.PhoneNumber = tdBuilder.teacher.PhoneNumbers[0].PhoneNumberValue;
            tstData.PhoneType = Enum_Phone.values["cell"].SisCode;

            List<SisPhoneNumberTestData> tstDataSet = new List<SisPhoneNumberTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_PhoneNumber("TestTask",
                new SyncJob_PhoneNumber())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockPhoneNumberDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockPhoneNumberDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockPhoneNumberDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncJctPersonPhoneNumber_ShouldWriteNewRecord()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockPhoneNumberDaoImpl._shouldReadReturnData = true;

            SisJctPersonPhoneNumberTestData tstData = new SisJctPersonPhoneNumberTestData();
            tstData.PhoneNumber = tdBuilder.teacher.PhoneNumbers[0].PhoneNumberValue;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;
            tstData.IsPrimary = tdBuilder.teacher.PhoneNumbers[0].IsPrimary;

            List<SisJctPersonPhoneNumberTestData> tstDataSet = new List<SisJctPersonPhoneNumberTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestJctDataTable(tstDataSet).Rows[0];

            new SyncTask_JctPersonPhoneNumber("TestTask",
                new SyncJob_PhoneNumber())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockJctPersonPhoneNumberDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockJctPersonPhoneNumberDaoImpl.WasCalled_Write);
            Assert.IsFalse(mockJctPersonPhoneNumberDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncJctPersonPhoneNumber_ShouldNeitherUpdateNorWriteNewRecord()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockPhoneNumberDaoImpl._shouldReadReturnData = true;
            mockJctPersonPhoneNumberDaoImpl._shouldReadReturnData = true;

            SisJctPersonPhoneNumberTestData tstData = new SisJctPersonPhoneNumberTestData();
            tstData.PhoneNumber = tdBuilder.teacher.PhoneNumbers[0].PhoneNumberValue;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;
            tstData.IsPrimary = tdBuilder.teacher.PhoneNumbers[0].IsPrimary;

            List<SisJctPersonPhoneNumberTestData> tstDataSet = new List<SisJctPersonPhoneNumberTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestJctDataTable(tstDataSet).Rows[0];

            new SyncTask_JctPersonPhoneNumber("TestTask",
                new SyncJob_PhoneNumber())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockJctPersonPhoneNumberDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockJctPersonPhoneNumberDaoImpl.WasCalled_Write);
            Assert.IsFalse(mockJctPersonPhoneNumberDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncJctPersonPhoneNumber_ShouldUpdateRecord_DifferentIsPrimary()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockPhoneNumberDaoImpl._shouldReadReturnData = true;
            mockJctPersonPhoneNumberDaoImpl._shouldReadReturnData = true;

            SisJctPersonPhoneNumberTestData tstData = new SisJctPersonPhoneNumberTestData();
            tstData.PhoneNumber = tdBuilder.teacher.PhoneNumbers[0].PhoneNumberValue;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;
            tstData.IsPrimary = true;

            List<SisJctPersonPhoneNumberTestData> tstDataSet = new List<SisJctPersonPhoneNumberTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestJctDataTable(tstDataSet).Rows[0];

            new SyncTask_JctPersonPhoneNumber("TestTask",
                new SyncJob_PhoneNumber())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockJctPersonPhoneNumberDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockJctPersonPhoneNumberDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockJctPersonPhoneNumberDaoImpl.WasCalled_Update);
        }

        private DataTable BuildTestDataTable(List<SisPhoneNumberTestData> tstDataSet)
        {
            DataTable tdPhoneNumer = DataTableFactory.CreateDataTable_Sis_PhoneNumber();
            foreach (SisPhoneNumberTestData tstData in tstDataSet)
            {
                DataRow row = tdPhoneNumer.NewRow();
                row["phone_number"] = tstData.PhoneNumber;
                row["phone_type_code"] = tstData.PhoneType;
                tdPhoneNumer.Rows.Add(row);
            }

            return tdPhoneNumer;
        }

        private DataTable BuildTestJctDataTable(List<SisJctPersonPhoneNumberTestData> tstDataSet)
        {
            DataTable tdPhoneNumer = DataTableFactory.CreateDataTable_Sis_JctPersonPhoneNumber();
            foreach (SisJctPersonPhoneNumberTestData tstData in tstDataSet)
            {
                DataRow row = tdPhoneNumer.NewRow();
                row["phone_number"] = tstData.PhoneNumber;
                row["unique_id"] = tstData.PersonId;
                row["is_primary"] = tstData.IsPrimary;
                tdPhoneNumer.Rows.Add(row);
            }

            return tdPhoneNumer;
        }

        private void SetMockReaderWithTestData(List<SisPhoneNumberTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 2);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count].PhoneNumber;
                        values[1] = tstDataSet[count].PhoneType;
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "phone_number");
            reader.Setup(x => x.GetOrdinal("phone_number"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "phone_type_code");
            reader.Setup(x => x.GetOrdinal("phone_type_code"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            _sisConnection.mockReader = reader;
        }

        private void SetMockJctReaderWithTestData(List<SisJctPersonPhoneNumberTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 3);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count].PhoneNumber;
                        values[1] = tstDataSet[count].PersonId;
                        values[2] = tstDataSet[count].IsPrimary;
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "phone_number");
            reader.Setup(x => x.GetOrdinal("phone_number"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "unique_id");
            reader.Setup(x => x.GetOrdinal("unique_id"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "is_primary");
            reader.Setup(x => x.GetOrdinal("is_primary"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            _sisConnection.mockReader = reader;
        }
    }

    class SisPhoneNumberTestData
    {
        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }
    }

    class SisJctPersonPhoneNumberTestData
    {
        public string PhoneNumber { get; set; }
        public string PersonId { get; set; }
        public bool IsPrimary { get; set; }
    }
}
