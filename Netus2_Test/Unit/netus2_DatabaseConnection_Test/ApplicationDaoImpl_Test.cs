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
    public class ApplicationDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        ApplicationDaoImpl applicationDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            applicationDaoImpl = new ApplicationDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void DeleteJctPersonApp_ShouldCallExpectedMethods()
        {
            MockJctPersonAppDaoImpl mockJctPersonAppDaoImpl = new MockJctPersonAppDaoImpl(tdBuilder);
            mockJctPersonAppDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockJctPersonAppDaoImpl = mockJctPersonAppDaoImpl;

            applicationDaoImpl.Delete(tdBuilder.application, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonAppDaoImpl.WasCalled_ReadWithAppId);
            Assert.IsTrue(mockJctPersonAppDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void ReadProvider_ShouldCallExpectedMethod()
        {
            MockProviderDaoImpl mockProviderDaoImpl = new MockProviderDaoImpl(tdBuilder);
            DaoImplFactory.MockProviderDaoImpl = mockProviderDaoImpl;

            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapApp(tdBuilder.application));
            SetMockReaderWithTestData(tstDataSet);

            applicationDaoImpl.Read(tdBuilder.application, _netus2DbConnection);

            Assert.IsTrue(mockProviderDaoImpl.WasCalled_ReadWithProviderId);
        }

        [TestCase]
        public void Write_ShouldCallExpectedMethod()
        {
            MockProviderDaoImpl mockProviderDaoImpl = new MockProviderDaoImpl(tdBuilder);
            DaoImplFactory.MockProviderDaoImpl = mockProviderDaoImpl;

            applicationDaoImpl.Write(tdBuilder.application, _netus2DbConnection);

            Assert.IsTrue(mockProviderDaoImpl.WasCalled_ReadWithProviderId);
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
                .Returns(() => 7);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count]["app_id"];
                        values[1] = tstDataSet[count]["name"];
                        values[2] = tstDataSet[count]["provider_id"];
                        values[3] = tstDataSet[count]["created"];
                        values[4] = tstDataSet[count]["created_by"];
                        values[5] = tstDataSet[count]["changed"];
                        values[6] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "app_id");
            reader.Setup(x => x.GetOrdinal("app_id"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "provider_id");
            reader.Setup(x => x.GetOrdinal("provider_id"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "created");
            reader.Setup(x => x.GetOrdinal("created"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "created_by");
            reader.Setup(x => x.GetOrdinal("created_by"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "changed");
            reader.Setup(x => x.GetOrdinal("changed"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "changed_by");
            reader.Setup(x => x.GetOrdinal("changed_by"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(string));

            _netus2DbConnection.mockReader = reader;
        }
    }
}