using Moq;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using Netus2_Test.MockDaoImpl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class AddressDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        AddressDaoImpl addressDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            addressDaoImpl = new AddressDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void DeleteJctpersonAddress_ShouldCallExpectedMethods()
        {
            MockJctPersonAddressDaoImpl mockJctPersonAddressDaoImpl = new MockJctPersonAddressDaoImpl(tdBuilder);
            mockJctPersonAddressDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockJctPersonAddressDaoImpl = mockJctPersonAddressDaoImpl;

            addressDaoImpl.Delete(tdBuilder.address_Teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonAddressDaoImpl.WasCalled_ReadAllWithAddressId);
            Assert.IsTrue(mockJctPersonAddressDaoImpl.WasCalled_Delete);
        }

        [TearDown]
        public void TearDown()
        {
            _netus2DbConnection.mockReader = new Mock<IDataReader>();
            _netus2DbConnection.expectedNewRecordSql = null;
            _netus2DbConnection.expectedNonQuerySql = null;
            _netus2DbConnection.expectedReaderSql = null;
        }

        private void SetMockReaderWithTestData(List<DataRow> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 16);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count]["address_id"];
                        values[1] = tstDataSet[count]["address_line_1"];
                        values[2] = tstDataSet[count]["address_line_2"];
                        values[3] = tstDataSet[count]["address_line_3"];
                        values[4] = tstDataSet[count]["address_line_4"];
                        values[5] = tstDataSet[count]["apartment"];
                        values[6] = tstDataSet[count]["city"];
                        values[7] = tstDataSet[count]["enum_state_province_id"];
                        values[8] = tstDataSet[count]["postal_code"];
                        values[9] = tstDataSet[count]["enum_country_id"];
                        values[10] = tstDataSet[count]["is_current_id"];
                        values[11] = tstDataSet[count]["enum_address_id"];
                        values[12] = tstDataSet[count]["created"];
                        values[13] = tstDataSet[count]["created_by"];
                        values[14] = tstDataSet[count]["changed"];
                        values[15] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "address_id");
            reader.Setup(x => x.GetOrdinal("address_id"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "address_line_1");
            reader.Setup(x => x.GetOrdinal("address_line_1"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "address_line_2");
            reader.Setup(x => x.GetOrdinal("address_line_2"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "address_line_3");
            reader.Setup(x => x.GetOrdinal("address_line_3"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "address_line_4");
            reader.Setup(x => x.GetOrdinal("address_line_4"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "apartment");
            reader.Setup(x => x.GetOrdinal("apartment"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "city");
            reader.Setup(x => x.GetOrdinal("city"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(7))
                .Returns(() => "enum_state_province_id");
            reader.Setup(x => x.GetOrdinal("enum_state_province_id"))
                .Returns(() => 7);
            reader.Setup(x => x.GetFieldType(7))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(8))
                .Returns(() => "postal_code");
            reader.Setup(x => x.GetOrdinal("postal_code"))
                .Returns(() => 8);
            reader.Setup(x => x.GetFieldType(8))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(9))
                .Returns(() => "enum_country_id");
            reader.Setup(x => x.GetOrdinal("enum_country_id"))
                .Returns(() => 9);
            reader.Setup(x => x.GetFieldType(9))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(10))
                .Returns(() => "is_current_id");
            reader.Setup(x => x.GetOrdinal("is_current_id"))
                .Returns(() => 10);
            reader.Setup(x => x.GetFieldType(10))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(11))
                .Returns(() => "enum_address_id");
            reader.Setup(x => x.GetOrdinal("enum_address_id"))
                .Returns(() => 11);
            reader.Setup(x => x.GetFieldType(11))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(12))
                .Returns(() => "created");
            reader.Setup(x => x.GetOrdinal("created"))
                .Returns(() => 12);
            reader.Setup(x => x.GetFieldType(12))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(13))
                .Returns(() => "created_by");
            reader.Setup(x => x.GetOrdinal("created_by"))
                .Returns(() => 13);
            reader.Setup(x => x.GetFieldType(13))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(14))
                .Returns(() => "changed");
            reader.Setup(x => x.GetOrdinal("changed"))
                .Returns(() => 14);
            reader.Setup(x => x.GetFieldType(14))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(15))
                .Returns(() => "changed_by");
            reader.Setup(x => x.GetOrdinal("changed_by"))
                .Returns(() => 15);
            reader.Setup(x => x.GetFieldType(15))
                .Returns(() => typeof(string));

            _netus2DbConnection.mockReader = reader;
        }
    }
}