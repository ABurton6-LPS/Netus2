﻿using Moq;
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
    public class OrganizationDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        OrganizationDaoImpl organizationDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            organizationDaoImpl = new OrganizationDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM organization " +
                "WHERE 1=1 " +
                "AND organization_id = " + tdBuilder.school.Id + " " +
                "AND name LIKE '" + tdBuilder.school.Name + "' " +
                "AND enum_organization_id = " + tdBuilder.school.OrganizationType.Id + " " +
                "AND identifier LIKE '" + tdBuilder.school.Identifier + "' " +
                "AND building_code LIKE '" + tdBuilder.school.BuildingCode + "' ";

            organizationDaoImpl.Delete(tdBuilder.school, _netus2DbConnection);
        }

        [TestCase]
        public void UnlinkChildren_ShouldUseExpectedSql()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapOrganization(tdBuilder.school, tdBuilder.schoolYear.Id));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "";

            organizationDaoImpl.Delete(tdBuilder.district, _netus2DbConnection);
        }

        [TestCase]
        public void Delete_ShouldCallExpectedMethods()
        {
            MockEmploymentSessionDaoImpl mockEmploymentSessionDaoImpl = new MockEmploymentSessionDaoImpl(tdBuilder);
            mockEmploymentSessionDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockEmploymentSessionDaoImpl = mockEmploymentSessionDaoImpl;
            MockAcademicSessionDaoImpl mockAcademicSessionDaoImpl = new MockAcademicSessionDaoImpl(tdBuilder);
            mockAcademicSessionDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockAcademicSessionDaoImpl = mockAcademicSessionDaoImpl;

            organizationDaoImpl.Delete(tdBuilder.district, _netus2DbConnection);

            Assert.IsTrue(mockEmploymentSessionDaoImpl.WasCalled_ReadWithOrganizationId);
            Assert.IsTrue(mockEmploymentSessionDaoImpl.WasCalled_DeleteWithOrganizationId);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadUsingOrganizationId);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_Delete);
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
                        values[0] = tstDataSet[count]["organization_id"];
                        values[1] = tstDataSet[count]["name"];
                        values[2] = tstDataSet[count]["enum_organization_id"];
                        values[3] = tstDataSet[count]["identifier"];
                        values[4] = tstDataSet[count]["building_code"];
                        values[5] = tstDataSet[count]["organization_parent_id"];
                        values[6] = tstDataSet[count]["created"];
                        values[7] = tstDataSet[count]["created_by"];
                        values[8] = tstDataSet[count]["changed"];
                        values[9] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "organization_id");
            reader.Setup(x => x.GetOrdinal("organization_id"))
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
                .Returns(() => "enum_organization_id");
            reader.Setup(x => x.GetOrdinal("enum_organization_id"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "identifier");
            reader.Setup(x => x.GetOrdinal("identifier"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "building_code");
            reader.Setup(x => x.GetOrdinal("building_code"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "organization_parent_id");
            reader.Setup(x => x.GetOrdinal("organization_parent_id"))
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
