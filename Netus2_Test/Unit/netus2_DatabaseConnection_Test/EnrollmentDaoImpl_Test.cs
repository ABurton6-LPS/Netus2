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
    public class EnrollmentDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        EnrollmentDaoImpl enrollmentDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            enrollmentDaoImpl = new EnrollmentDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void Delete_ShouldCallExpectedMethod()
        {
            MockJctEnrollmentAcademicSessionDaoImpl mockJctEnrollmentAcademicSessionDaoImpl = new MockJctEnrollmentAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockJctEnrollmentAcademicSessionDaoImpl = mockJctEnrollmentAcademicSessionDaoImpl;

            enrollmentDaoImpl.Delete(tdBuilder.enrollment, _netus2DbConnection);

            Assert.IsTrue(mockJctEnrollmentAcademicSessionDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void Read_ShouldCallExpectedMethods()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapEnrollment(tdBuilder.enrollment, tdBuilder.student.Id));
            SetMockReaderWithTestData(tstDataSet);

            MockClassEnrolledDaoImpl mockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            DaoImplFactory.MockClassEnrolledDaoImpl = mockClassEnrolledDaoImpl;
            MockJctEnrollmentAcademicSessionDaoImpl mockJctEnrollmentAcademicSessionId = new MockJctEnrollmentAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockJctEnrollmentAcademicSessionDaoImpl = mockJctEnrollmentAcademicSessionId;
            MockAcademicSessionDaoImpl mockAcademicSessionDaoImpl = new MockAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockAcademicSessionDaoImpl = mockAcademicSessionDaoImpl;

            enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, _netus2DbConnection);

            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_ReadUsingClassId);
            Assert.IsTrue(mockJctEnrollmentAcademicSessionId.WasCalled_ReadWithEnrollmentId);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadUsingAcademicSessionId);
        }

        [TestCase]
        public void Update_ShouldCallExpectedMethods()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapEnrollment(tdBuilder.enrollment, tdBuilder.student.Id));
            SetMockReaderWithTestData(tstDataSet);

            MockJctEnrollmentAcademicSessionDaoImpl mockJctEnrollmentAcademicSessionId = new MockJctEnrollmentAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockJctEnrollmentAcademicSessionDaoImpl = mockJctEnrollmentAcademicSessionId;
            MockAcademicSessionDaoImpl mockAcademicSessionDaoImpl = new MockAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockAcademicSessionDaoImpl = mockAcademicSessionDaoImpl;

            enrollmentDaoImpl.Update(tdBuilder.enrollment, tdBuilder.student.Id, _netus2DbConnection);

            Assert.IsTrue(mockJctEnrollmentAcademicSessionId.WasCalled_ReadWithEnrollmentId);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadUsingAcademicSessionId);
        }

        [TestCase]
        public void Write_ShouldCallExpectedMethods()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapEnrollment(tdBuilder.enrollment, tdBuilder.student.Id));
            SetMockReaderWithTestData(tstDataSet);

            MockClassEnrolledDaoImpl mockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            DaoImplFactory.MockClassEnrolledDaoImpl = mockClassEnrolledDaoImpl;
            MockJctEnrollmentAcademicSessionDaoImpl mockJctEnrollmentAcademicSessionId = new MockJctEnrollmentAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockJctEnrollmentAcademicSessionDaoImpl = mockJctEnrollmentAcademicSessionId;
            MockAcademicSessionDaoImpl mockAcademicSessionDaoImpl = new MockAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockAcademicSessionDaoImpl = mockAcademicSessionDaoImpl;

            enrollmentDaoImpl.Write(tdBuilder.enrollment, tdBuilder.student.Id, _netus2DbConnection);

            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_ReadUsingClassId);
            Assert.IsTrue(mockJctEnrollmentAcademicSessionId.WasCalled_ReadWithEnrollmentId);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadUsingAcademicSessionId);
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
                .Returns(() => 11);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count]["enrollment_id"];
                        values[1] = tstDataSet[count]["person_id"];
                        values[2] = tstDataSet[count]["class_id"];
                        values[3] = tstDataSet[count]["enum_grade_id"];
                        values[4] = tstDataSet[count]["start_date"];
                        values[5] = tstDataSet[count]["end_date"];
                        values[6] = tstDataSet[count]["is_primary_id"];
                        values[7] = tstDataSet[count]["created"];
                        values[8] = tstDataSet[count]["created_by"];
                        values[9] = tstDataSet[count]["changed"];
                        values[10] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "enrollment_id");
            reader.Setup(x => x.GetOrdinal("enrollment_id"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "person_id");
            reader.Setup(x => x.GetOrdinal("person_id"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "class_id");
            reader.Setup(x => x.GetOrdinal("class_id"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "enum_grade_id");
            reader.Setup(x => x.GetOrdinal("enum_grade_id"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(int));

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
                .Returns(() => "is_primary_id");
            reader.Setup(x => x.GetOrdinal("is_primary_id"))
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
