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
    public class EmploymentSessionDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        EmploymentSessionDaoImpl employmentSessionDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            employmentSessionDaoImpl = new EmploymentSessionDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void DeleteWithPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM employment_session " +
                "WHERE 1=1 " +
                "AND employment_session_id = " + tdBuilder.employmentSession.Id + " " +
                "AND name = '" + tdBuilder.employmentSession.Name + "' " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND start_date = '" + tdBuilder.employmentSession.StartDate + "' " +
                "AND end_date = '" + tdBuilder.employmentSession.EndDate + "' " +
                "AND is_primary_id = " + tdBuilder.employmentSession.IsPrimary.Id + " " +
                "AND enum_session_id = " + tdBuilder.employmentSession.SessionType.Id + " " +
                "AND organization_id = " + tdBuilder.employmentSession.Organization.Id + " ";

            employmentSessionDaoImpl.Delete_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void DeleteWithOrganizationId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM employment_session " +
                "WHERE 1=1 " +
                "AND employment_session_id = " + tdBuilder.employmentSession.Id + " " +
                "AND name = '" + tdBuilder.employmentSession.Name + "' " +
                "AND start_date = '" + tdBuilder.employmentSession.StartDate + "' " +
                "AND end_date = '" + tdBuilder.employmentSession.EndDate + "' " +
                "AND is_primary_id = " + tdBuilder.employmentSession.IsPrimary.Id + " " +
                "AND enum_session_id = " + tdBuilder.employmentSession.SessionType.Id + " " +
                "AND organization_id = " + tdBuilder.employmentSession.Organization.Id + " ";

            employmentSessionDaoImpl.Delete_WithOrganizationId(tdBuilder.employmentSession, tdBuilder.employmentSession.Organization.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithPersonId_WhileUsingNullEmploymentSession_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM employment_session WHERE person_id = " + tdBuilder.teacher.Id;

            employmentSessionDaoImpl.Read_WithPersonId(null, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithPersonId_WhileUsingEmploymentSessionId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM employment_session WHERE 1=1 AND employment_session_id = " + tdBuilder.employmentSession.Id + " ";

            employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithPersonId_WhileNotUsingEmploymentSessionId_ShouldUseExpectedSql()
        {
            tdBuilder.employmentSession.Id = -1;

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM employment_session " +
                "WHERE 1=1 " +
                "AND name = '" + tdBuilder.employmentSession.Name + "' " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND datediff(day, start_date, '" + tdBuilder.employmentSession.StartDate + "') = 0 " +
                "AND datediff(day, end_date, '" + tdBuilder.employmentSession.EndDate + "') = 0 " +
                "AND is_primary_id = " + tdBuilder.employmentSession.IsPrimary.Id + " " +
                "AND enum_session_id = " + tdBuilder.employmentSession.SessionType.Id + " " +
                "AND organization_id = " + tdBuilder.employmentSession.Organization.Id + " ";

            employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithOrganizationId_WhileUsingNullEmploymentSession_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM employment_session WHERE organization_id = " + tdBuilder.employmentSession.Organization.Id;

            employmentSessionDaoImpl.Read_WithOrganizationId(null, tdBuilder.employmentSession.Organization.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithOrganizationId_WhileUsingEmploymentSessionId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM employment_session WHERE 1=1 AND employment_session_id = " + tdBuilder.employmentSession.Id + " ";

            employmentSessionDaoImpl.Read_WithOrganizationId(tdBuilder.employmentSession, tdBuilder.employmentSession.Organization.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithOrganizationId_WhileNotUsingEmploymentSessionId_ShouldUseExpectedSql()
        {
            tdBuilder.employmentSession.Id = -1;

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM employment_session " +
                "WHERE 1=1 " +
                "AND name = '" + tdBuilder.employmentSession.Name + "' " +
                "AND datediff(day, start_date, '" + tdBuilder.employmentSession.StartDate + "') = 0 " +
                "AND datediff(day, end_date, '" + tdBuilder.employmentSession.EndDate + "') = 0 " +
                "AND is_primary_id = " + tdBuilder.employmentSession.IsPrimary.Id + " " +
                "AND enum_session_id = " + tdBuilder.employmentSession.SessionType.Id + " " +
                "AND organization_id = " + tdBuilder.employmentSession.Organization.Id + " ";

            employmentSessionDaoImpl.Read_WithOrganizationId(tdBuilder.employmentSession, tdBuilder.employmentSession.Organization.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_ShouldCallExpectedMethod()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapEmploymentSession_WithOrganizationId(tdBuilder.employmentSession, tdBuilder.employmentSession.Organization.Id));
            SetMockReaderWithTestData(tstDataSet);

            MockOrganizationDaoImpl mockOrganizationDaoImpl = new MockOrganizationDaoImpl(tdBuilder);
            DaoImplFactory.MockOrganizationDaoImpl = mockOrganizationDaoImpl;

            employmentSessionDaoImpl.Read_WithOrganizationId(tdBuilder.employmentSession, tdBuilder.employmentSession.Organization.Id, _netus2DbConnection);

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithOrganizationId);
        }

        [TestCase]
        public void Update_WhileRecordIsNotFound_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO employment_session (" +
                "name, " +
                "person_id, " +
                "start_date, " +
                "end_date, " +
                "is_primary_id, " +
                "enum_session_id, " +
                "organization_id, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "'" + tdBuilder.employmentSession.Name + "', " +
                tdBuilder.teacher.Id + ", " +
                "'" + tdBuilder.employmentSession.StartDate + "', " +
                "'" + tdBuilder.employmentSession.EndDate + "', " +
                tdBuilder.employmentSession.IsPrimary.Id + ", " +
                tdBuilder.employmentSession.SessionType.Id + ", " +
                tdBuilder.employmentSession.Organization.Id + ", " +
                "GETDATE(), " +
                "'Netus2')";

            employmentSessionDaoImpl.Update(tdBuilder.employmentSession, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhileRecordIsFound_ShouldUseExpectedSql()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapEmploymentSession_WithOrganizationId(tdBuilder.employmentSession, tdBuilder.employmentSession.Organization.Id));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE employment_session SET " +
                "name = '" + tdBuilder.employmentSession.Name + "', " +
                "person_id = " + tdBuilder.teacher.Id + ", " +
                "start_date = '" + tdBuilder.employmentSession.StartDate + "', " +
                "end_date = '" + tdBuilder.employmentSession.EndDate + "', " +
                "is_primary_id = " + tdBuilder.employmentSession.IsPrimary.Id + ", " +
                "enum_session_id = " + tdBuilder.employmentSession.SessionType.Id + ", " +
                "organization_id = " + tdBuilder.employmentSession.Organization.Id + ", " +
                "changed = GETDATE(), " +
                "changed_by = 'Netus2' " +
                "WHERE employment_session_id = " + tdBuilder.employmentSession.Id;

            employmentSessionDaoImpl.Update(tdBuilder.employmentSession, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO employment_session (" +
                "name, " +
                "person_id, " +
                "start_date, " +
                "end_date, " +
                "is_primary_id, " +
                "enum_session_id, " +
                "organization_id, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "'" + tdBuilder.employmentSession.Name + "', " +
                tdBuilder.teacher.Id + ", " +
                "'" + tdBuilder.employmentSession.StartDate + "', " +
                "'" + tdBuilder.employmentSession.EndDate + "', " +
                tdBuilder.employmentSession.IsPrimary.Id + ", " +
                tdBuilder.employmentSession.SessionType.Id + ", " +
                tdBuilder.employmentSession.Organization.Id + ", " +
                "GETDATE(), " +
                "'Netus2')";

            employmentSessionDaoImpl.Write(tdBuilder.employmentSession, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldCallExpectedMethod()
        {
            MockOrganizationDaoImpl mockOrganizationDaoImpl = new MockOrganizationDaoImpl(tdBuilder);
            DaoImplFactory.MockOrganizationDaoImpl = mockOrganizationDaoImpl;

            employmentSessionDaoImpl.Write(tdBuilder.employmentSession, tdBuilder.teacher.Id, _netus2DbConnection);

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithOrganizationId);
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
                .Returns(() => 11);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count]["employment_session_id"];
                        values[1] = tstDataSet[count]["name"];
                        values[2] = tstDataSet[count]["person_id"];
                        values[3] = tstDataSet[count]["start_date"];
                        values[4] = tstDataSet[count]["is_primary_id"];
                        values[5] = tstDataSet[count]["enum_session_id"];
                        values[6] = tstDataSet[count]["organization_id"];
                        values[7] = tstDataSet[count]["created"];
                        values[8] = tstDataSet[count]["created_by"];
                        values[9] = tstDataSet[count]["changed"];
                        values[10] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "employment_session_id");
            reader.Setup(x => x.GetOrdinal("employment_session_id"))
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
                .Returns(() => "person_id");
            reader.Setup(x => x.GetOrdinal("person_id"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "start_date");
            reader.Setup(x => x.GetOrdinal("start_date"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "is_primary_id");
            reader.Setup(x => x.GetOrdinal("is_primary_id"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "enum_session_id");
            reader.Setup(x => x.GetOrdinal("enum_session_id"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "organization_id");
            reader.Setup(x => x.GetOrdinal("organization_id"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(7))
                .Returns(() => "created");
            reader.Setup(x => x.GetOrdinal("created"))
                .Returns(() => 7);
            reader.Setup(x => x.GetFieldType(7))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(8))
                .Returns(() => "created_by");
            reader.Setup(x => x.GetOrdinal("created_by"))
                .Returns(() => 8);
            reader.Setup(x => x.GetFieldType(8))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(9))
                .Returns(() => "changed");
            reader.Setup(x => x.GetOrdinal("changed"))
                .Returns(() => 9);
            reader.Setup(x => x.GetFieldType(9))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(10))
                .Returns(() => "changed_by");
            reader.Setup(x => x.GetOrdinal("changed_by"))
                .Returns(() => 10);
            reader.Setup(x => x.GetFieldType(10))
                .Returns(() => typeof(string));

            _netus2DbConnection.mockReader = reader;
        }
    }
}
