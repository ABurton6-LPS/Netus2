using Moq;
using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_Test.MockDaoImpl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class ProviderDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        ProviderDaoImpl providerDaoImpl;
        DaoObjectMapper daoObjectMapper;
        MockApplicationDaoImpl mockApplicationDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            providerDaoImpl = new ProviderDaoImpl();

            daoObjectMapper = new DaoObjectMapper();

            mockApplicationDaoImpl = new MockApplicationDaoImpl(tdBuilder);
            DaoImplFactory.MockApplicationDaoImpl = mockApplicationDaoImpl;
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM provider " +
                "WHERE 1=1 " +
                "AND provider_id = " + tdBuilder.provider.Id + " " +
                "AND name LIKE '" + tdBuilder.provider.Name + "' " +
                "AND url_standard_access LIKE '" + tdBuilder.provider.UrlStandardAccess + "' " +
                "AND url_admin_access LIKE '" + tdBuilder.provider.UrlAdminAccess + "' " +
                "AND populated_by LIKE '" + tdBuilder.provider.PopulatedBy + "' ";

            providerDaoImpl.Delete(tdBuilder.provider, _netus2DbConnection);
        }

        [TestCase]
        public void DeleteApplication_ShouldCallExpectedMethods()
        {
            mockApplicationDaoImpl._shouldReadReturnData = true;

            providerDaoImpl.Delete(tdBuilder.provider, _netus2DbConnection);

            Assert.IsTrue(mockApplicationDaoImpl.WasCalled_ReadUsingProviderId);
            Assert.IsTrue(mockApplicationDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void UnlinkChildren_ShouldUseExpectedSql()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapProvider(tdBuilder.provider, tdBuilder.provider_parent.Id));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE provider SET " +
                "name = '" + tdBuilder.provider.Name + "', " +
                "url_standard_access = '" + tdBuilder.provider.UrlStandardAccess + "', " +
                "url_admin_access = '" + tdBuilder.provider.UrlAdminAccess + "', " +
                "populated_by = '" + tdBuilder.provider.PopulatedBy + "', " +
                "parent_provider_id = NULL, " +
                "changed = dbo.CURRENT_DATETIME(), " +
                "changed_by = 'Netus2' " +
                "WHERE provider_id = " + tdBuilder.provider.Id;

            providerDaoImpl.Delete(tdBuilder.provider_parent, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithProviderId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM provider " +
                "WHERE provider_id = " + tdBuilder.provider.Id;

            providerDaoImpl.Read_WithProviderId(tdBuilder.provider.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithAppId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM provider " +
                "WHERE provider_id IN (" +
                "SELECT provider_id FROM app " +
                "WHERE app_id = " + tdBuilder.application.Id + ")";

            providerDaoImpl.Read_WithAppId(tdBuilder.application.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileNotUsingParentId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM provider " +
                "WHERE 1=1 " +
                "AND provider_id = " + tdBuilder.provider.Id + " ";

            providerDaoImpl.Read(tdBuilder.provider, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileProviderIsNull_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM provider " +
                "WHERE parent_provider_id = " + tdBuilder.provider_parent.Id;

            providerDaoImpl.Read(null, tdBuilder.provider_parent.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileProviderIdIsUsed_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM provider WHERE 1=1 AND provider_id = " + tdBuilder.provider.Id + " ";

            providerDaoImpl.Read(tdBuilder.provider, tdBuilder.provider_parent.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileProvidderIdIsNotUsed_ShouldUseExpectedSql()
        {
            tdBuilder.provider.Id = -1;

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM provider " +
                "WHERE 1=1 " +
                "AND name LIKE '" + tdBuilder.provider.Name + "' " +
                "AND url_standard_access LIKE '" + tdBuilder.provider.UrlStandardAccess + "' " +
                "AND url_admin_access LIKE '" + tdBuilder.provider.UrlAdminAccess + "' " +
                "AND populated_by LIKE '" + tdBuilder.provider.PopulatedBy + "' " +
                "AND parent_provider_id = " + tdBuilder.provider_parent.Id;

            providerDaoImpl.Read(tdBuilder.provider, tdBuilder.provider_parent.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhileNotFindingRecord_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO provider (" +
                "name, " +
                "url_standard_access, " +
                "url_admin_access, " +
                "populated_by, " +
                "parent_provider_id, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "'" + tdBuilder.provider.Name + "', " +
                "'" + tdBuilder.provider.UrlStandardAccess + "', " +
                "'" + tdBuilder.provider.UrlAdminAccess + "', " +
                "'" + tdBuilder.provider.PopulatedBy + "', " +
                "NULL, " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            providerDaoImpl.Update(tdBuilder.provider, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhileFindingRecord_ShouldUseExpectedSql()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapProvider(tdBuilder.provider, tdBuilder.provider_parent.Id));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE provider SET " +
                "name = '" + tdBuilder.provider.Name + "', " +
                "url_standard_access = '" + tdBuilder.provider.UrlStandardAccess + "', " +
                "url_admin_access = '" + tdBuilder.provider.UrlAdminAccess + "', " +
                "populated_by = '" + tdBuilder.provider.PopulatedBy + "', " +
                "parent_provider_id = NULL, " +
                "changed = dbo.CURRENT_DATETIME(), " +
                "changed_by = 'Netus2' " +
                "WHERE provider_id = " + tdBuilder.provider.Id;

            providerDaoImpl.Update(tdBuilder.provider, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO provider (" +
                "name, " +
                "url_standard_access, " +
                "url_admin_access, " +
                "populated_by, " +
                "parent_provider_id, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "'" + tdBuilder.provider.Name + "', " +
                "'" + tdBuilder.provider.UrlStandardAccess + "', " +
                "'" + tdBuilder.provider.UrlAdminAccess + "', " +
                "'" + tdBuilder.provider.PopulatedBy + "', " +
                "NULL, " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            providerDaoImpl.Write(tdBuilder.provider, _netus2DbConnection);
        }

        [TearDown]
        public void TearDown()
        {
            DbConnectionFactory.MockDatabaseConnection = null;
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
                        values[0] = tstDataSet[count]["provider_id"];
                        values[1] = tstDataSet[count]["name"];
                        values[2] = tstDataSet[count]["url_standard_access"];
                        values[3] = tstDataSet[count]["url_admin_access"];
                        values[4] = tstDataSet[count]["populated_by"];
                        values[5] = tstDataSet[count]["parent_provider_id"];
                        values[6] = tstDataSet[count]["created"];
                        values[7] = tstDataSet[count]["created_by"];
                        values[8] = tstDataSet[count]["changed"];
                        values[9] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "provider_id");
            reader.Setup(x => x.GetOrdinal("provider_id"))
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
                .Returns(() => "url_standard_access");
            reader.Setup(x => x.GetOrdinal("url_standard_access"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "url_admin_access");
            reader.Setup(x => x.GetOrdinal("url_admin_access"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "populated_by");
            reader.Setup(x => x.GetOrdinal("populated_by"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "parent_provider_id");
            reader.Setup(x => x.GetOrdinal("parent_provider_id"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(int));

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