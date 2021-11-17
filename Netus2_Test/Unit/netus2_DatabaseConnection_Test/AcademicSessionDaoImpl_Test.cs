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
        public void DeleteClassEnrolled_ShouldCallExpectedMethod()
        {
            MockClassEnrolledDaoImpl mockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            mockClassEnrolledDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockClassEnrolledDaoImpl = mockClassEnrolledDaoImpl;
            DaoImplFactory.MockOrganizationDaoImpl = new MockOrganizationDaoImpl(tdBuilder);
            DaoImplFactory.MockJctEnrollmentClassEnrolledDaoImpl = new MockJctEnrollmentClassEnrolledDaoImpl(tdBuilder);

            academicSessionDaoImpl.Delete(tdBuilder.schoolYear, _netus2DbConnection);

            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void DeleteEnrollment_ShouldCallExpectedMethod()
        {
            DaoImplFactory.MockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            MockEnrollmentDaoImpl mockEnrollmentDaoImpl = new MockEnrollmentDaoImpl(tdBuilder);
            mockEnrollmentDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockEnrollmentDaoImpl = mockEnrollmentDaoImpl;

            academicSessionDaoImpl.Delete(tdBuilder.schoolYear, _netus2DbConnection);

            Assert.IsTrue(mockEnrollmentDaoImpl.WasCalled_ReadAllWithAcademicSessionId);
            Assert.IsTrue(mockEnrollmentDaoImpl.WasCalled_Delete);
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