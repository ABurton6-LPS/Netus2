using Moq;
using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using Netus2_Test.MockDaoImpl;
using Netus2SisSync.SyncProcesses.SyncJobs;
using Netus2SisSync.SyncProcesses.SyncTasks.AddressTasks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.Unit.SyncProcess
{
    public class SyncAddress_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _sisConnection;
        MockPersonDaoImpl mockPersonDaoImpl;
        MockAddressDaoImpl mockAddressDaoImpl;
        MockJctPersonAddressDaoImpl mockJctPersonAddressDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;
            _sisConnection = (MockDatabaseConnection)DbConnectionFactory.GetSisConnection();

            tdBuilder = new TestDataBuilder();
            DaoImplFactory.MockAll = true;
            mockPersonDaoImpl = new MockPersonDaoImpl(tdBuilder);
            DaoImplFactory.MockPersonDaoImpl = mockPersonDaoImpl;
            mockAddressDaoImpl = new MockAddressDaoImpl(tdBuilder);
            DaoImplFactory.MockAddressDaoImpl = mockAddressDaoImpl;
            mockJctPersonAddressDaoImpl = new MockJctPersonAddressDaoImpl(tdBuilder);
            DaoImplFactory.MockJctPersonAddressDaoImpl = mockJctPersonAddressDaoImpl;
        }

        [TestCase]
        public void SisRead_Address_NullData()
        {
            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = null;
            tstData.Line2 = null;
            tstData.Line3 = null;
            tstData.Line4 = null;
            tstData.Apartment = null;
            tstData.City = null;
            tstData.State = null;
            tstData.Zip = null;
            tstData.Country = null;
            tstData.IsPrimary = null;
            tstData.AddressType = null;
            tstData.PersonId = null;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Address syncJob_Address = new SyncJob_Address();
            syncJob_Address.ReadFromSis();
            DataTable results = syncJob_Address._dtAddress;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(emptyString, results.Rows[0]["address_line_1"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["address_line_2"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["address_line_3"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["address_line_4"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["apartment"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["city"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_state_province_id"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["postal_code"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_country_id"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["is_primary_id"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_address_id"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["suniq"].ToString());
        }

        [TestCase]
        public void SisRead_Address_TestData()
        {
            Address addressTeacher = tdBuilder.address_Teacher;
            Address addressStudent = tdBuilder.address_Student;

            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = addressTeacher.Line1;
            tstData.Line2 = addressTeacher.Line2;
            tstData.Line3 = addressTeacher.Line3;
            tstData.Line4 = addressTeacher.Line4;
            tstData.Apartment = addressTeacher.Apartment;
            tstData.City = addressTeacher.City;
            tstData.State = addressTeacher.StateProvince.Netus2Code;
            tstData.Zip = addressTeacher.PostalCode;
            tstData.Country = addressTeacher.Country.Netus2Code;
            tstData.IsPrimary = addressTeacher.IsPrimary.Netus2Code;
            tstData.AddressType = addressTeacher.AddressType.Netus2Code;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;

            SisAddressTestData tstData2 = new SisAddressTestData();
            tstData2.Line1 = addressStudent.Line1;
            tstData2.Line2 = addressStudent.Line2;
            tstData2.Line3 = addressStudent.Line3;
            tstData2.Line4 = addressStudent.Line4;
            tstData2.Apartment = addressStudent.Apartment;
            tstData2.City = addressStudent.City;
            tstData2.State = addressStudent.StateProvince.Netus2Code;
            tstData2.Zip = addressStudent.PostalCode;
            tstData2.Country = addressStudent.Country.Netus2Code;
            tstData2.IsPrimary = addressStudent.IsPrimary.Netus2Code;
            tstData2.AddressType = addressStudent.AddressType.Netus2Code;
            tstData2.PersonId = tdBuilder.student.UniqueIdentifiers[0].Identifier;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);
            tstDataSet.Add(tstData2);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Address syncJob_Address = new SyncJob_Address();
            syncJob_Address.ReadFromSis();
            DataTable results = syncJob_Address._dtAddress;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstData.Line1, results.Rows[0]["address_line_1"].ToString());
            Assert.AreEqual(tstData.Line2, results.Rows[0]["address_line_2"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["address_line_3"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["address_line_4"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["apartment"].ToString());
            Assert.AreEqual(tstData.City, results.Rows[0]["city"].ToString());
            Assert.AreEqual(tstData.State, results.Rows[0]["enum_state_province_id"].ToString());
            Assert.AreEqual(tstData.Zip, results.Rows[0]["postal_code"].ToString());
            Assert.AreEqual(tstData.Country, results.Rows[0]["enum_country_id"].ToString());
            Assert.AreEqual(tstData.IsPrimary, results.Rows[0]["is_primary_id"].ToString());
            Assert.AreEqual(tstData.AddressType, results.Rows[0]["enum_address_id"].ToString());
            Assert.AreEqual(tstData.PersonId, results.Rows[0]["suniq"]);

            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstData2.Line1, results.Rows[1]["address_line_1"].ToString());
            Assert.AreEqual(tstData2.Line2, results.Rows[1]["address_line_2"].ToString());
            Assert.AreEqual(emptyString, results.Rows[1]["address_line_3"].ToString());
            Assert.AreEqual(emptyString, results.Rows[1]["address_line_4"].ToString());
            Assert.AreEqual(emptyString, results.Rows[1]["apartment"].ToString());
            Assert.AreEqual(tstData2.City, results.Rows[1]["city"].ToString());
            Assert.AreEqual(tstData2.State, results.Rows[1]["enum_state_province_id"].ToString());
            Assert.AreEqual(tstData2.Zip, results.Rows[1]["postal_code"].ToString());
            Assert.AreEqual(tstData2.Country, results.Rows[1]["enum_country_id"].ToString());
            Assert.AreEqual(tstData2.IsPrimary, results.Rows[1]["is_primary_id"].ToString());
            Assert.AreEqual(tstData2.AddressType, results.Rows[1]["enum_address_id"].ToString());
            Assert.AreEqual(tstData2.PersonId, results.Rows[1]["suniq"]);
        }

        [TestCase]
        public void SisRead_JctPersonAddress_NullData()
        {
            SisJctPersonAddressTestData tstData = new SisJctPersonAddressTestData();
            tstData.Line1 = null;
            tstData.Line2 = null;
            tstData.City = null;
            tstData.State = null;
            tstData.Zip = null;
            tstData.PersonId = null;
            tstData.AddressTypeId = null;

            List<SisJctPersonAddressTestData> tstDataSet = new List<SisJctPersonAddressTestData>();
            tstDataSet.Add(tstData);

            SetMockJctReaderWithTestData(tstDataSet);

            SyncJob_Address syncJobAddress = new SyncJob_Address();
            syncJobAddress.ReadFromSisJctPersonAddress(_sisConnection);
            DataTable results = syncJobAddress._dtJctPersonAddress;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(emptyString, results.Rows[0]["address_line_1"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["address_line_2"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["city"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_state_province_id"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["postal_code"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["suniq"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_address_id"].ToString());
        }

        [TestCase]
        public void SisRead_JctPersonAddress_TestData()
        {
            SisJctPersonAddressTestData tstData = new SisJctPersonAddressTestData();
            tstData.Line1 = tdBuilder.address_Teacher.Line1;
            tstData.Line2 = tdBuilder.address_Teacher.Line2;
            tstData.City = tdBuilder.address_Teacher.City;
            tstData.State = tdBuilder.address_Teacher.StateProvince.Netus2Code;
            tstData.Zip = tdBuilder.address_Teacher.PostalCode;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;
            tstData.AddressTypeId = tdBuilder.address_Teacher.AddressType.Netus2Code;

            List<SisJctPersonAddressTestData> tstDataSet = new List<SisJctPersonAddressTestData>();
            tstDataSet.Add(tstData);

            SetMockJctReaderWithTestData(tstDataSet);

            SyncJob_Address syncJobAddress = new SyncJob_Address();
            syncJobAddress.ReadFromSisJctPersonAddress(_sisConnection);
            DataTable results = syncJobAddress._dtJctPersonAddress;

            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstData.Line1, results.Rows[0]["address_line_1"].ToString());
            Assert.AreEqual(tstData.Line2, results.Rows[0]["address_line_2"].ToString());
            Assert.AreEqual(tstData.City, results.Rows[0]["city"].ToString());
            Assert.AreEqual(tstData.State, results.Rows[0]["enum_state_province_id"].ToString());
            Assert.AreEqual(tstData.Zip, results.Rows[0]["postal_code"].ToString());
            Assert.AreEqual(tstData.PersonId, results.Rows[0]["suniq"].ToString());
            Assert.AreEqual(tstData.AddressTypeId, results.Rows[0]["enum_address_id"].ToString());
        }

        [TestCase]
        public void SyncAddress_ShouldWriteNewRecord()
        {
            Address addressTeacher = tdBuilder.address_Teacher;

            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = addressTeacher.Line1;
            tstData.Line2 = addressTeacher.Line2;
            tstData.Line3 = addressTeacher.Line3;
            tstData.Line4 = addressTeacher.Line4;
            tstData.Apartment = addressTeacher.Apartment;
            tstData.City = addressTeacher.City;
            tstData.State = addressTeacher.StateProvince.Netus2Code;
            tstData.Zip = addressTeacher.PostalCode;
            tstData.Country = addressTeacher.Country.Netus2Code;
            tstData.IsPrimary = addressTeacher.IsPrimary.Netus2Code;
            tstData.AddressType = addressTeacher.AddressType.Netus2Code;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Address("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Write);
            Assert.IsFalse(mockAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncAddress_ShouldNeitherUpdateNorWriteRecord()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;

            Address addressTeacher = tdBuilder.address_Teacher;

            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = addressTeacher.Line1;
            tstData.Line2 = addressTeacher.Line2;
            tstData.Line3 = addressTeacher.Line3;
            tstData.Line4 = addressTeacher.Line4;
            tstData.Apartment = addressTeacher.Apartment;
            tstData.City = addressTeacher.City;
            tstData.State = addressTeacher.StateProvince.Netus2Code;
            tstData.Zip = addressTeacher.PostalCode;
            tstData.Country = addressTeacher.Country.Netus2Code;
            tstData.IsPrimary = addressTeacher.IsPrimary.Netus2Code;
            tstData.AddressType = addressTeacher.AddressType.Netus2Code;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Address("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockAddressDaoImpl.WasCalled_Write);
            Assert.IsFalse(mockAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncAddress_ShouldUpdateRecord_DifferentLine1()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;

            Address addressTeacher = tdBuilder.address_Teacher;

            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = "16a New Street";
            tstData.Line2 = addressTeacher.Line2;
            tstData.Line3 = addressTeacher.Line3;
            tstData.Line4 = addressTeacher.Line4;
            tstData.Apartment = addressTeacher.Apartment;
            tstData.City = addressTeacher.City;
            tstData.State = addressTeacher.StateProvince.Netus2Code;
            tstData.Zip = addressTeacher.PostalCode;
            tstData.Country = addressTeacher.Country.Netus2Code;
            tstData.IsPrimary = addressTeacher.IsPrimary.Netus2Code;
            tstData.AddressType = addressTeacher.AddressType.Netus2Code;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Address("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockAddressDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncAddress_ShouldUpdateRecord_DifferentLine2()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;

            Address addressTeacher = tdBuilder.address_Teacher;

            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = addressTeacher.Line1;
            tstData.Line2 = "something new";
            tstData.Line3 = addressTeacher.Line3;
            tstData.Line4 = addressTeacher.Line4;
            tstData.Apartment = addressTeacher.Apartment;
            tstData.City = addressTeacher.City;
            tstData.State = addressTeacher.StateProvince.Netus2Code;
            tstData.Zip = addressTeacher.PostalCode;
            tstData.Country = addressTeacher.Country.Netus2Code;
            tstData.IsPrimary = addressTeacher.IsPrimary.Netus2Code;
            tstData.AddressType = addressTeacher.AddressType.Netus2Code;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Address("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockAddressDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncAddress_ShouldUpdateRecord_DifferentLine3()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;

            Address addressTeacher = tdBuilder.address_Teacher;

            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = addressTeacher.Line1;
            tstData.Line2 = addressTeacher.Line2;
            tstData.Line3 = "something different";
            tstData.Line4 = addressTeacher.Line4;
            tstData.Apartment = addressTeacher.Apartment;
            tstData.City = addressTeacher.City;
            tstData.State = addressTeacher.StateProvince.Netus2Code;
            tstData.Zip = addressTeacher.PostalCode;
            tstData.Country = addressTeacher.Country.Netus2Code;
            tstData.IsPrimary = addressTeacher.IsPrimary.Netus2Code;
            tstData.AddressType = addressTeacher.AddressType.Netus2Code;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Address("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockAddressDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncAddress_ShouldUpdateRecord_DifferentLine4()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;

            Address addressTeacher = tdBuilder.address_Teacher;

            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = addressTeacher.Line1;
            tstData.Line2 = addressTeacher.Line2;
            tstData.Line3 = addressTeacher.Line3;
            tstData.Line4 = "something strange";
            tstData.Apartment = addressTeacher.Apartment;
            tstData.City = addressTeacher.City;
            tstData.State = addressTeacher.StateProvince.Netus2Code;
            tstData.Zip = addressTeacher.PostalCode;
            tstData.Country = addressTeacher.Country.Netus2Code;
            tstData.IsPrimary = addressTeacher.IsPrimary.Netus2Code;
            tstData.AddressType = addressTeacher.AddressType.Netus2Code;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Address("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockAddressDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncAddress_ShouldUpdateRecord_DifferentApartment()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;

            Address addressTeacher = tdBuilder.address_Teacher;

            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = addressTeacher.Line1;
            tstData.Line2 = addressTeacher.Line2;
            tstData.Line3 = addressTeacher.Line3;
            tstData.Line4 = addressTeacher.Line4;
            tstData.Apartment = "togetherment?";
            tstData.City = addressTeacher.City;
            tstData.State = addressTeacher.StateProvince.Netus2Code;
            tstData.Zip = addressTeacher.PostalCode;
            tstData.Country = addressTeacher.Country.Netus2Code;
            tstData.IsPrimary = addressTeacher.IsPrimary.Netus2Code;
            tstData.AddressType = addressTeacher.AddressType.Netus2Code;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Address("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockAddressDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncAddress_ShouldUpdateRecord_DifferentCity()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;

            Address addressTeacher = tdBuilder.address_Teacher;

            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = addressTeacher.Line1;
            tstData.Line2 = addressTeacher.Line2;
            tstData.Line3 = addressTeacher.Line3;
            tstData.Line4 = addressTeacher.Line4;
            tstData.Apartment = addressTeacher.Apartment;
            tstData.City = "This one isn't too windy";
            tstData.State = addressTeacher.StateProvince.Netus2Code;
            tstData.Zip = addressTeacher.PostalCode;
            tstData.Country = addressTeacher.Country.Netus2Code;
            tstData.IsPrimary = addressTeacher.IsPrimary.Netus2Code;
            tstData.AddressType = addressTeacher.AddressType.Netus2Code;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Address("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockAddressDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncAddress_ShouldUpdateRecord_DifferentState()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;

            Address addressTeacher = tdBuilder.address_Teacher;

            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = addressTeacher.Line1;
            tstData.Line2 = addressTeacher.Line2;
            tstData.Line3 = addressTeacher.Line3;
            tstData.Line4 = addressTeacher.Line4;
            tstData.Apartment = addressTeacher.Apartment;
            tstData.City = addressTeacher.City;
            tstData.State = "unset";
            tstData.Zip = addressTeacher.PostalCode;
            tstData.Country = addressTeacher.Country.Netus2Code;
            tstData.IsPrimary = addressTeacher.IsPrimary.Netus2Code;
            tstData.AddressType = addressTeacher.AddressType.Netus2Code;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Address("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockAddressDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncAddress_ShouldUpdateRecord_DifferentCountry()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;

            Address addressTeacher = tdBuilder.address_Teacher;

            SisAddressTestData tstData = new SisAddressTestData();
            tstData.Line1 = addressTeacher.Line1;
            tstData.Line2 = addressTeacher.Line2;
            tstData.Line3 = addressTeacher.Line3;
            tstData.Line4 = addressTeacher.Line4;
            tstData.Apartment = addressTeacher.Apartment;
            tstData.City = addressTeacher.City;
            tstData.State = addressTeacher.StateProvince.Netus2Code;
            tstData.Zip = addressTeacher.PostalCode;
            tstData.Country = "unset";
            tstData.IsPrimary = addressTeacher.IsPrimary.Netus2Code;
            tstData.AddressType = addressTeacher.AddressType.Netus2Code;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;

            List<SisAddressTestData> tstDataSet = new List<SisAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Address("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockAddressDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncJctPersonAddress_ShouldWriteNewRecord()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;

            SisJctPersonAddressTestData tstData = new SisJctPersonAddressTestData();
            tstData.Line1 = tdBuilder.address_Teacher.Line1;
            tstData.Line2 = tdBuilder.address_Teacher.Line2;
            tstData.City = tdBuilder.address_Teacher.City;
            tstData.State = tdBuilder.address_Teacher.StateProvince.Netus2Code;
            tstData.Zip = tdBuilder.address_Teacher.PostalCode;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;
            tstData.AddressTypeId = tdBuilder.address_Teacher.AddressType.Netus2Code;

            List<SisJctPersonAddressTestData> tstDataSet = new List<SisJctPersonAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildJctTestDataTable(tstDataSet).Rows[0];

            new SyncTask_JctPersonAddress("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockPersonDaoImpl.WasCalled_ReadUsingUniqueIdentifier);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockJctPersonAddressDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockJctPersonAddressDaoImpl.WasCalled_Write);
            Assert.IsFalse(mockJctPersonAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SncJctPersonAddress_ShouldNeitherUpdateNorWriteNewRecord()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;
            mockJctPersonAddressDaoImpl._shouldReadReturnData = true;

            SisJctPersonAddressTestData tstData = new SisJctPersonAddressTestData();
            tstData.Line1 = tdBuilder.address_Teacher.Line1;
            tstData.Line2 = tdBuilder.address_Teacher.Line2;
            tstData.City = tdBuilder.address_Teacher.City;
            tstData.State = tdBuilder.address_Teacher.StateProvince.Netus2Code;
            tstData.Zip = tdBuilder.address_Teacher.PostalCode;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;
            tstData.AddressTypeId = tdBuilder.address_Teacher.AddressType.Netus2Code;

            List<SisJctPersonAddressTestData> tstDataSet = new List<SisJctPersonAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildJctTestDataTable(tstDataSet).Rows[0];

            new SyncTask_JctPersonAddress("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockPersonDaoImpl.WasCalled_ReadUsingUniqueIdentifier);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockJctPersonAddressDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockJctPersonAddressDaoImpl.WasCalled_Write);
            Assert.IsFalse(mockJctPersonAddressDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncJctPersonAddress_ShouldUpdateRecord_DifferentAddressType()
        {
            mockPersonDaoImpl._shouldReadReturnData = true;
            mockAddressDaoImpl._shouldReadReturnData = true;
            mockJctPersonAddressDaoImpl._shouldReadReturnData = true;

            SisJctPersonAddressTestData tstData = new SisJctPersonAddressTestData();
            tstData.Line1 = tdBuilder.address_Teacher.Line1;
            tstData.Line2 = tdBuilder.address_Teacher.Line2;
            tstData.City = tdBuilder.address_Teacher.City;
            tstData.State = tdBuilder.address_Teacher.StateProvince.Netus2Code;
            tstData.Zip = tdBuilder.address_Teacher.PostalCode;
            tstData.PersonId = tdBuilder.teacher.UniqueIdentifiers[0].Identifier;
            tstData.AddressTypeId = "home";

            List<SisJctPersonAddressTestData> tstDataSet = new List<SisJctPersonAddressTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildJctTestDataTable(tstDataSet).Rows[0];

            new SyncTask_JctPersonAddress("TestTask",
                new SyncJob_Address())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockPersonDaoImpl.WasCalled_ReadUsingUniqueIdentifier);
            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockJctPersonAddressDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockJctPersonAddressDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockJctPersonAddressDaoImpl.WasCalled_Update);
        }

        [TearDown]
        public void TearDown()
        {
            DaoImplFactory.MockPersonDaoImpl = null;
            DaoImplFactory.MockAddressDaoImpl = null;
            DaoImplFactory.MockJctPersonAddressDaoImpl = null;
        }

        private DataTable BuildTestDataTable(List<SisAddressTestData> tstDataSet)
        {
            DataTable tdAddress = DataTableFactory.CreateDataTable_Sis_Address();
            foreach(SisAddressTestData tstData in tstDataSet)
            {
                DataRow row = tdAddress.NewRow();
                row["address_line_1"] = tstData.Line1;
                row["address_line_2"] = tstData.Line2;
                row["address_line_3"] = tstData.Line3;
                row["address_line_4"] = tstData.Line4;
                row["apartment"] = tstData.Apartment;
                row["city"] = tstData.City;
                row["enum_state_province_id"] = tstData.State;
                row["postal_code"] = tstData.Zip;
                row["enum_country_id"] = tstData.Country;
                row["is_primary_id"] = tstData.IsPrimary;
                row["enum_address_id"] = tstData.AddressType;
                row["suniq"] = tstData.PersonId;
                tdAddress.Rows.Add(row);
            }

            return tdAddress;
        }

        private DataTable BuildJctTestDataTable(List<SisJctPersonAddressTestData> tstDataSet)
        {
            DataTable tdJctPersonAddress = DataTableFactory.CreateDataTable_Sis_JctPersonAddress();
            foreach (SisJctPersonAddressTestData tstData in tstDataSet)
            {
                DataRow row = tdJctPersonAddress.NewRow();
                row["address_line_1"] = tstData.Line1;
                row["address_line_2"] = tstData.Line2;
                row["city"] = tstData.City;
                row["enum_state_province_id"] = tstData.State;
                row["postal_code"] = tstData.Zip;
                row["suniq"] = tstData.PersonId;
                row["enum_address_id"] = tstData.AddressTypeId;
                tdJctPersonAddress.Rows.Add(row);
            }

            return tdJctPersonAddress;
        }

        private void SetMockReaderWithTestData(List<SisAddressTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 12);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count].Line1;
                        values[1] = tstDataSet[count].Line2;
                        values[2] = tstDataSet[count].Line3;
                        values[3] = tstDataSet[count].Line4;
                        values[4] = tstDataSet[count].Apartment;
                        values[5] = tstDataSet[count].City;
                        values[6] = tstDataSet[count].State;
                        values[7] = tstDataSet[count].Zip;
                        values[8] = tstDataSet[count].Country;
                        values[9] = tstDataSet[count].IsPrimary;
                        values[10] = tstDataSet[count].AddressType;
                        values[11] = tstDataSet[count].PersonId;
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "address_line_1");
            reader.Setup(x => x.GetOrdinal("address_line_1"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "address_line_2");
            reader.Setup(x => x.GetOrdinal("address_line_2"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "address_line_3");
            reader.Setup(x => x.GetOrdinal("address_line_3"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "address_line_4");
            reader.Setup(x => x.GetOrdinal("address_line_4"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "apartment");
            reader.Setup(x => x.GetOrdinal("apartment"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "city");
            reader.Setup(x => x.GetOrdinal("city"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "enum_state_province_id");
            reader.Setup(x => x.GetOrdinal("enum_state_province_id"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(7))
                .Returns(() => "postal_code");
            reader.Setup(x => x.GetOrdinal("postal_code"))
                .Returns(() => 7);
            reader.Setup(x => x.GetFieldType(7))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(8))
                .Returns(() => "enum_country_id");
            reader.Setup(x => x.GetOrdinal("enum_counry_id"))
                .Returns(() => 8);
            reader.Setup(x => x.GetFieldType(8))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(9))
                .Returns(() => "is_primary_id");
            reader.Setup(x => x.GetOrdinal("is_primary_id"))
                .Returns(() => 9);
            reader.Setup(x => x.GetFieldType(9))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(10))
                .Returns(() => "enum_address_id");
            reader.Setup(x => x.GetOrdinal("enum_address_id"))
                .Returns(() => 10);
            reader.Setup(x => x.GetFieldType(10))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(11))
                .Returns(() => "suniq");
            reader.Setup(x => x.GetOrdinal("suniq"))
                .Returns(() => 11);
            reader.Setup(x => x.GetFieldType(11))
                .Returns(() => typeof(string));

            _sisConnection.mockReader = reader;
        }

        private void SetMockJctReaderWithTestData(List<SisJctPersonAddressTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 7);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count].Line1;
                        values[1] = tstDataSet[count].Line2;
                        values[2] = tstDataSet[count].City;
                        values[3] = tstDataSet[count].State;
                        values[4] = tstDataSet[count].Zip;
                        values[5] = tstDataSet[count].PersonId;
                        values[6] = tstDataSet[count].AddressTypeId;
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "address_line_1");
            reader.Setup(x => x.GetOrdinal("address_line_1"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "address_line_2");
            reader.Setup(x => x.GetOrdinal("address_line_2"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "city");
            reader.Setup(x => x.GetOrdinal("city"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "enum_state_province_id");
            reader.Setup(x => x.GetOrdinal("enum_state_province_id"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "postal_code");
            reader.Setup(x => x.GetOrdinal("postal_code"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "suniq");
            reader.Setup(x => x.GetOrdinal("suniq"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "enum_address_id");
            reader.Setup(x => x.GetOrdinal("enum_address_id"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(string));

            _sisConnection.mockReader = reader;
        }
    }

    class SisAddressTestData
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
        public string Apartment { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string IsPrimary { get; set; }
        public string AddressType { get; set; }
        public string PersonId { get; set; }
    }

    class SisJctPersonAddressTestData
    {
        public string Line1 { get; set; } 
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PersonId { get; set; }
        public string AddressTypeId { get; set; }
    }
}