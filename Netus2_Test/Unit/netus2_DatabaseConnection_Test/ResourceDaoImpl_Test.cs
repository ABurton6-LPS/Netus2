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
    class ResourceDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        ResourceDaoImpl resourceDaoImpl;
        DaoObjectMapper daoObjectMapper;
        MockJctClassResourceDaoImpl mockJctClassResource;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            resourceDaoImpl = new ResourceDaoImpl();

            daoObjectMapper = new DaoObjectMapper();

            mockJctClassResource = new MockJctClassResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassResourceDaoImpl = mockJctClassResource;
        }

        [TestCase]
        public void DeleteJctClassResource_ShouldCallExpectedMethods()
        {
            mockJctClassResource._shouldReadReturnData = true;

            resourceDaoImpl.Delete(tdBuilder.resource, _netus2DbConnection);

            Assert.IsTrue(mockJctClassResource.WasCalled_ReadWithResourceId);
            Assert.IsTrue(mockJctClassResource.WasCalled_Delete);
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
                .Returns(() => 10);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count]["resource_id"];
                        values[1] = tstDataSet[count]["name"];
                        values[2] = tstDataSet[count]["enum_importance_id"];
                        values[3] = tstDataSet[count]["vendor_resource_identification"];
                        values[4] = tstDataSet[count]["vendor_identification"];
                        values[5] = tstDataSet[count]["application_identification"];
                        values[6] = tstDataSet[count]["created"];
                        values[7] = tstDataSet[count]["created_by"];
                        values[8] = tstDataSet[count]["changed"];
                        values[9] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "resource_id");
            reader.Setup(x => x.GetOrdinal("resource_id"))
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
                .Returns(() => "enum_importance_id");
            reader.Setup(x => x.GetOrdinal("enum_importance_id"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "vendor_resource_identification");
            reader.Setup(x => x.GetOrdinal("vendor_resource_identification"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "vendor_identification");
            reader.Setup(x => x.GetOrdinal("vendor_identification"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "application_identification");
            reader.Setup(x => x.GetOrdinal("application_identification"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "created");
            reader.Setup(x => x.GetOrdinal("created"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(7))
                .Returns(() => "created_by");
            reader.Setup(x => x.GetOrdinal("created_by"))
                .Returns(() => 7);
            reader.Setup(x => x.GetFieldType(7))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(8))
                .Returns(() => "changed");
            reader.Setup(x => x.GetOrdinal("changed"))
                .Returns(() => 8);
            reader.Setup(x => x.GetFieldType(8))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(9))
                .Returns(() => "changed_by");
            reader.Setup(x => x.GetOrdinal("changed_by"))
                .Returns(() => 9);
            reader.Setup(x => x.GetFieldType(9))
                .Returns(() => typeof(string));

            _netus2DbConnection.mockReader = reader;
        }
    }
}
