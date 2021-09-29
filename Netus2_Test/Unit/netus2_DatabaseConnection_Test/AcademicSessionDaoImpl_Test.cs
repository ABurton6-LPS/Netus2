using Moq;
using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_Test.MockDaoImpl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class AcademicSessionDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        AcademicSessionDaoImpl academicSessionDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            academicSessionDaoImpl = new AcademicSessionDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            DaoImplFactory.MockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            DaoImplFactory.MockOrganizationDaoImpl = new MockOrganizationDaoImpl(tdBuilder);
            DaoImplFactory.MockJctEnrollmentAcademicSessionDaoImpl = new MockJctEnrollmentAcademicSessionDaoImpl(tdBuilder);

            tdBuilder.schoolYear.Children.Clear();

            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM academic_session " +
                "WHERE 1=1 " +
                "AND academic_session_id = " + tdBuilder.schoolYear.Id + " " +
                "AND term_code LIKE '" + tdBuilder.schoolYear.TermCode + "' " +
                "AND school_year = " + tdBuilder.schoolYear.SchoolYear + " " +
                "AND name LIKE '" + tdBuilder.schoolYear.Name + "' " +
                "AND start_date = '" + tdBuilder.schoolYear.StartDate + "' " +
                "AND end_date = '" + tdBuilder.schoolYear.EndDate + "' " +
                "AND enum_session_id = " + tdBuilder.schoolYear.SessionType.Id + " " +
                "AND organization_id = " + tdBuilder.schoolYear.Organization.Id;

            academicSessionDaoImpl.Delete(tdBuilder.schoolYear, _netus2DbConnection);
        }

        [TestCase]
        public void DeleteClassEnrolled_ShouldCallExpectedMethod()
        {
            MockClassEnrolledDaoImpl mockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            mockClassEnrolledDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockClassEnrolledDaoImpl = mockClassEnrolledDaoImpl;
            DaoImplFactory.MockOrganizationDaoImpl = new MockOrganizationDaoImpl(tdBuilder);
            DaoImplFactory.MockJctEnrollmentAcademicSessionDaoImpl = new MockJctEnrollmentAcademicSessionDaoImpl(tdBuilder);

            academicSessionDaoImpl.Delete(tdBuilder.schoolYear, _netus2DbConnection);

            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void UnlinkEnrollment_ShouldCallExpectedMethod()
        {
            DaoImplFactory.MockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder); ;
            DaoImplFactory.MockOrganizationDaoImpl = new MockOrganizationDaoImpl(tdBuilder);
            MockJctEnrollmentAcademicSessionDaoImpl mockJctEnrollmentAcademicSessionDaoImpl = new MockJctEnrollmentAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockJctEnrollmentAcademicSessionDaoImpl = mockJctEnrollmentAcademicSessionDaoImpl;

            academicSessionDaoImpl.Delete(tdBuilder.schoolYear, _netus2DbConnection);

            Assert.IsTrue(mockJctEnrollmentAcademicSessionDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void UnlinkChildren_ShouldUseExpectedSql()
        {
            DaoImplFactory.MockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            DaoImplFactory.MockOrganizationDaoImpl = new MockOrganizationDaoImpl(tdBuilder);
            DaoImplFactory.MockJctEnrollmentAcademicSessionDaoImpl = new MockJctEnrollmentAcademicSessionDaoImpl(tdBuilder);

            tdBuilder.schoolYear = RemoveAllButFirstChild(tdBuilder.schoolYear);

            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapAcademicSession(tdBuilder.semester1, tdBuilder.schoolYear.Id));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE academic_session " +
                "SET term_code = '" + tdBuilder.semester1.TermCode + "', " +
                "school_year = " + tdBuilder.semester1.SchoolYear + ", " +
                "name = '" + tdBuilder.semester1.Name + "', " +
                "start_date = '" + tdBuilder.semester1.StartDate + "', " +
                "end_date = '" + tdBuilder.semester1.EndDate + "', " +
                "enum_session_id = " + tdBuilder.semester1.SessionType.Id + ", " +
                "parent_session_id = NULL, " +
                "organization_id = " + tdBuilder.school.Id + ", " +
                "changed = dbo.CURRENT_DATETIME(), " +
                "changed_by = 'Netus2' " +
                "WHERE academic_session_id = " + tdBuilder.semester1.Id;

            academicSessionDaoImpl.Delete(tdBuilder.schoolYear, _netus2DbConnection);
        }

        [TestCase]
        public void ReadUsingOrganizationId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM academic_session WHERE organization_id = " + tdBuilder.school.Id;

            academicSessionDaoImpl.Read_UsingOrganizationId(tdBuilder.school.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadUsingClassEnrolledId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql = 
                "SELECT * FROM academic_session WHERE academic_session_id IN (" +
                "SELECT academic_session_id FROM class WHERE class_id = " + tdBuilder.classEnrolled.Id + ")";

            academicSessionDaoImpl.Read_UsingClassEnrolledId(tdBuilder.classEnrolled.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadUsingAcademicSessionId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM academic_session WHERE academic_session_id = " + tdBuilder.schoolYear.Id;

            academicSessionDaoImpl.Read_UsingAcademicSessionId(tdBuilder.schoolYear.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadUsingBuildingCodeTermCodeSchoolyear_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM academic_session WHERE 1=1 " +
                "AND term_code = '" + tdBuilder.schoolYear.TermCode + "' " +
                "AND school_year = " + tdBuilder.schoolYear.SchoolYear + " " +
                "AND organization_id in (" +
                "SELECT organization_id FROM organization WHERE sis_building_code LIKE '" +
                tdBuilder.school.SisBuildingCode + "')";

            academicSessionDaoImpl.Read_UsingSisBuildingCode_TermCode_Schoolyear(
                tdBuilder.school.SisBuildingCode, 
                tdBuilder.schoolYear.TermCode, 
                tdBuilder.schoolYear.SchoolYear,
                _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithIdWithoutParentId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM academic_session WHERE 1=1 AND academic_session_id = " + tdBuilder.schoolYear.Id + " ";

            academicSessionDaoImpl.Read(tdBuilder.schoolYear, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithoutIdWithoutParentId_ShouldUseExpectedSql()
        {
            tdBuilder.schoolYear.Id = -1;

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM academic_session WHERE 1=1 " +
                "AND name = '" + tdBuilder.schoolYear.Name + "' " +
                "AND term_code = '" + tdBuilder.schoolYear.TermCode + "' " +
                "AND school_year = " + tdBuilder.schoolYear.SchoolYear + " " +
                "AND start_date = '" + tdBuilder.schoolYear.StartDate + "' " +
                "AND end_date = '" + tdBuilder.schoolYear.EndDate + "' " +
                "AND enum_session_id = " + tdBuilder.schoolYear.SessionType.Id + " ";

            academicSessionDaoImpl.Read(tdBuilder.schoolYear, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithIdWithParentId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM academic_session WHERE 1=1 AND academic_session_id = " + tdBuilder.semester1.Id + " ";

            academicSessionDaoImpl.Read(tdBuilder.semester1, tdBuilder.schoolYear.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithoutIdWithParentId_ShouldUseExpectedSql()
        {
            tdBuilder.semester1.Id = -1;

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM academic_session WHERE 1=1 " +
                "AND name = '" + tdBuilder.semester1.Name + "' " +
                "AND term_code = '" + tdBuilder.semester1.TermCode + "' " +
                "AND school_year = " + tdBuilder.semester1.SchoolYear + " " +
                "AND start_date = '" + tdBuilder.semester1.StartDate + "' " +
                "AND end_date = '" + tdBuilder.semester1.EndDate + "' " +
                "AND enum_session_id = " + tdBuilder.semester1.SessionType.Id + " " +
                "AND parent_session_id = " + tdBuilder.schoolYear.Id + " ";

            academicSessionDaoImpl.Read(tdBuilder.semester1, tdBuilder.schoolYear.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadOrganization_ShouldCallExpectedMethods()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapAcademicSession(tdBuilder.schoolYear, -1));
            SetMockReaderWithTestData(tstDataSet);

            MockOrganizationDaoImpl mockOrganizationDaoImpl = new MockOrganizationDaoImpl(tdBuilder);
            DaoImplFactory.MockOrganizationDaoImpl = mockOrganizationDaoImpl;

            academicSessionDaoImpl.Read(tdBuilder.schoolYear, _netus2DbConnection);

            Assert.IsTrue(mockOrganizationDaoImpl.WasCalled_ReadWithOrganizationId);
        }

        [TestCase]
        public void ReadChildren_ShouldUseExpectedSql()
        {
            RemoveAllButFirstChild(tdBuilder.schoolYear);

            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapAcademicSession(tdBuilder.schoolYear, -1));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM academic_session WHERE parent_session_id = " + tdBuilder.schoolYear.Id;

            academicSessionDaoImpl.Read_Children(tdBuilder.schoolYear, _netus2DbConnection);
        }

        [TestCase]
        public void UpdateWithoutParentId_WhenRecordFound_ShouldUseExpectedSql()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapAcademicSession(tdBuilder.schoolYear, -1));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE academic_session SET " +
                "term_code = '" + tdBuilder.schoolYear.TermCode + "', " +
                "school_year = " + tdBuilder.schoolYear.SchoolYear + ", " +
                "name = '" + tdBuilder.schoolYear.Name + "', " +
                "start_date = '" + tdBuilder.schoolYear.StartDate + "', " +
                "end_date = '" + tdBuilder.schoolYear.EndDate + "', " +
                "enum_session_id = " + tdBuilder.schoolYear.SessionType.Id + ", " +
                "parent_session_id = NULL, " +
                "organization_id = " + tdBuilder.school.Id + ", " +
                "changed = dbo.CURRENT_DATETIME(), " +
                "changed_by = 'Netus2' " +
                "WHERE academic_session_id = " + tdBuilder.schoolYear.Id + "";

            academicSessionDaoImpl.Update(tdBuilder.schoolYear, _netus2DbConnection);
        }

        [TestCase]
        public void UpdateWithParentId_WhenRecordFound_ShouldUseExpectedSql()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapAcademicSession(tdBuilder.semester1, tdBuilder.schoolYear.Id));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE academic_session SET " +
                "term_code = '" + tdBuilder.semester1.TermCode + "', " +
                "school_year = " + tdBuilder.semester1.SchoolYear + ", " +
                "name = '" + tdBuilder.semester1.Name + "', " +
                "start_date = '" + tdBuilder.semester1.StartDate + "', " +
                "end_date = '" + tdBuilder.semester1.EndDate + "', " +
                "enum_session_id = " + tdBuilder.semester1.SessionType.Id + ", " +
                "parent_session_id = " + tdBuilder.schoolYear.Id + ", " +
                "organization_id = " + tdBuilder.school.Id + ", " +
                "changed = dbo.CURRENT_DATETIME(), " +
                "changed_by = 'Netus2' " +
                "WHERE academic_session_id = " + tdBuilder.semester1.Id + "";

            academicSessionDaoImpl.Update(tdBuilder.semester1, tdBuilder.schoolYear.Id, _netus2DbConnection);
        }

        [TestCase]
        public void UpdateWithoutParentId_WhenRecordNotFound_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO academic_session " +
                "(term_code, school_year, name, start_date, end_date, enum_session_id, " +
                "parent_session_id, organization_id, created, created_by" +
                ") VALUES (" +
                "'" + tdBuilder.schoolYear.TermCode + "', " +
                tdBuilder.schoolYear.SchoolYear + ", " +
                "'" + tdBuilder.schoolYear.Name + "', " +
                "'" + tdBuilder.schoolYear.StartDate + "', " +
                "'" + tdBuilder.schoolYear.EndDate + "', " +
                tdBuilder.schoolYear.SessionType.Id + ", " +
                "NULL, " +
                tdBuilder.school.Id + ", " +
                "dbo.CURRENT_DATETIME(), 'Netus2')";

            academicSessionDaoImpl.Update(tdBuilder.schoolYear, _netus2DbConnection);
        }

        [TestCase]
        public void UpdateWithParentId_WhenRecordNotFound_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO academic_session " +
                "(term_code, school_year, name, start_date, end_date, enum_session_id, " +
                "parent_session_id, organization_id, created, created_by" +
                ") VALUES (" +
                "'" + tdBuilder.semester1.TermCode + "', " +
                tdBuilder.semester1.SchoolYear + ", " +
                "'" + tdBuilder.semester1.Name + "', " +
                "'" + tdBuilder.semester1.StartDate + "', " +
                "'" + tdBuilder.semester1.EndDate + "', " +
                tdBuilder.semester1.SessionType.Id + ", " +
                tdBuilder.schoolYear.Id + ", " +
                tdBuilder.school.Id + ", " +
                "dbo.CURRENT_DATETIME(), 'Netus2')";

            academicSessionDaoImpl.Update(tdBuilder.semester1, tdBuilder.schoolYear.Id, _netus2DbConnection);
        }

        [TestCase]
        public void WriteWithoutParentId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO academic_session " +
                "(term_code, school_year, name, start_date, end_date, enum_session_id, " +
                "parent_session_id, organization_id, created, created_by" +
                ") VALUES (" +
                "'" + tdBuilder.schoolYear.TermCode + "', " +
                tdBuilder.schoolYear.SchoolYear + ", " +
                "'" + tdBuilder.schoolYear.Name + "', " +
                "'" + tdBuilder.schoolYear.StartDate + "', " +
                "'" + tdBuilder.schoolYear.EndDate + "', " +
                tdBuilder.schoolYear.SessionType.Id + ", " +
                "NULL, " +
                tdBuilder.school.Id + ", " +
                "dbo.CURRENT_DATETIME(), 'Netus2')";

            academicSessionDaoImpl.Write(tdBuilder.schoolYear, _netus2DbConnection);
        }

        [TestCase]
        public void WriteWithParentId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO academic_session " +
                "(term_code, school_year, name, start_date, end_date, enum_session_id, " +
                "parent_session_id, organization_id, created, created_by" +
                ") VALUES (" +
                "'" + tdBuilder.semester1.TermCode + "', " +
                tdBuilder.semester1.SchoolYear + ", " +
                "'" + tdBuilder.semester1.Name + "', " +
                "'" + tdBuilder.semester1.StartDate + "', " +
                "'" + tdBuilder.semester1.EndDate + "', " +
                tdBuilder.semester1.SessionType.Id + ", " +
                tdBuilder.schoolYear.Id + ", " +
                tdBuilder.school.Id + ", " +
                "dbo.CURRENT_DATETIME(), 'Netus2')";

            academicSessionDaoImpl.Write(tdBuilder.semester1, tdBuilder.schoolYear.Id, _netus2DbConnection);
        }

        [TearDown]
        public void TearDown()
        {
            _netus2DbConnection.mockReader = new Mock<IDataReader>();
        }

        private AcademicSession RemoveAllButFirstChild(AcademicSession parent)
        {
            for (int i = 1; i < parent.Children.Count; i++)
            {
                parent.Children.RemoveAt(i);
            }

            return parent;
        }

        private void SetMockReaderWithTestData(List<DataRow> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 13);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count]["academic_session_id"];
                        values[1] = tstDataSet[count]["term_code"];
                        values[2] = tstDataSet[count]["school_year"];
                        values[3] = tstDataSet[count]["name"];
                        values[4] = tstDataSet[count]["start_date"];
                        values[5] = tstDataSet[count]["end_date"];
                        values[6] = tstDataSet[count]["enum_session_id"];
                        values[7] = tstDataSet[count]["parent_session_id"];
                        values[8] = tstDataSet[count]["organization_id"];
                        values[9] = tstDataSet[count]["created"];
                        values[10] = tstDataSet[count]["created_by"];
                        values[11] = tstDataSet[count]["changed"];
                        values[12] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "academic_session_id");
            reader.Setup(x => x.GetOrdinal("academic_session_id"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "term_code");
            reader.Setup(x => x.GetOrdinal("term_code"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "school_year");
            reader.Setup(x => x.GetOrdinal("school_year"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "start_date");
            reader.Setup(x => x.GetOrdinal("start_date"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "end_date");
            reader.Setup(x => x.GetOrdinal("end_date"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "enum_session_id");
            reader.Setup(x => x.GetOrdinal("enum_session_id"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(7))
                .Returns(() => "parent_session_id");
            reader.Setup(x => x.GetOrdinal("parent_session_id"))
                .Returns(() => 7);
            reader.Setup(x => x.GetFieldType(7))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(8))
                .Returns(() => "organization_id");
            reader.Setup(x => x.GetOrdinal("organization_id"))
                .Returns(() => 8);
            reader.Setup(x => x.GetFieldType(8))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(9))
                .Returns(() => "created");
            reader.Setup(x => x.GetOrdinal("created"))
                .Returns(() => 9);
            reader.Setup(x => x.GetFieldType(9))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(10))
                .Returns(() => "created_by");
            reader.Setup(x => x.GetOrdinal("created_by"))
                .Returns(() => 10);
            reader.Setup(x => x.GetFieldType(10))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(11))
                .Returns(() => "changed");
            reader.Setup(x => x.GetOrdinal("changed"))
                .Returns(() => 11);
            reader.Setup(x => x.GetFieldType(11))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(12))
                .Returns(() => "changed_by");
            reader.Setup(x => x.GetOrdinal("changed_by"))
                .Returns(() => 12);
            reader.Setup(x => x.GetFieldType(12))
                .Returns(() => typeof(string));

            _netus2DbConnection.mockReader = reader;
        }
    }
}