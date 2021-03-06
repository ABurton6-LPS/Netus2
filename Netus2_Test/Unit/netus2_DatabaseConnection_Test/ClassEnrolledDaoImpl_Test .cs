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
    public class ClassEnrolledImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        ClassEnrolledDaoImpl classEnrolledDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            classEnrolledDaoImpl = new ClassEnrolledDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void DeleteLineItem_ShouldCallExpectedMethods()
        {
            DaoImplFactory.MockJctClassPeriodDaoImpl = new MockJctClassPeriodDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassResourceDaoImpl = new MockJctClassResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockEnrollmentDaoImpl = new MockEnrollmentDaoImpl(tdBuilder);

            MockLineItemDaoImpl mockLineItemDaoImpl = new MockLineItemDaoImpl(tdBuilder);
            mockLineItemDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockLineItemDaoImpl = mockLineItemDaoImpl;

            classEnrolledDaoImpl.Delete(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockLineItemDaoImpl.WasCalled_ReadWithClassEnrolledId);
            Assert.IsTrue(mockLineItemDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void DeleteJctClassPeriod_ShouldCallExpectedMethod()
        {
            DaoImplFactory.MockLineItemDaoImpl = new MockLineItemDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassResourceDaoImpl = new MockJctClassResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockEnrollmentDaoImpl = new MockEnrollmentDaoImpl(tdBuilder);

            MockJctClassPeriodDaoImpl mockJctClassPeriodDaoImpl = new MockJctClassPeriodDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassPeriodDaoImpl = mockJctClassPeriodDaoImpl;

            classEnrolledDaoImpl.Delete(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockJctClassPeriodDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void DeleteJctClassResource_ShouldCallExpectedMethod()
        {
            DaoImplFactory.MockLineItemDaoImpl = new MockLineItemDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassPeriodDaoImpl = new MockJctClassPeriodDaoImpl(tdBuilder);
            DaoImplFactory.MockEnrollmentDaoImpl = new MockEnrollmentDaoImpl(tdBuilder);

            MockJctClassResourceDaoImpl mockJctClassResourceDaoImpl = new MockJctClassResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassResourceDaoImpl = mockJctClassResourceDaoImpl;

            classEnrolledDaoImpl.Delete(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockJctClassResourceDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void DeleteJctEnrollmentClassEnrolled_ShouldCallExpecteddMethods()
        {
            DaoImplFactory.MockLineItemDaoImpl = new MockLineItemDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassPeriodDaoImpl = new MockJctClassPeriodDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassResourceDaoImpl = new MockJctClassResourceDaoImpl(tdBuilder);

            MockJctEnrollmentClassEnrolledDaoImpl mockJctEnrollmentClassEnrolledDaoImpl = new MockJctEnrollmentClassEnrolledDaoImpl(tdBuilder);
            mockJctEnrollmentClassEnrolledDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockJctEnrollmentClassEnrolledDaoImpl = mockJctEnrollmentClassEnrolledDaoImpl;

            classEnrolledDaoImpl.Delete(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockJctEnrollmentClassEnrolledDaoImpl.WasCalled_ReadAllWithClassEnrolledId);
            Assert.IsTrue(mockJctEnrollmentClassEnrolledDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void Read_ShouldCallExpectedMethodFromAcademicSessionDaoImpl()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapClassEnrolled(tdBuilder.classEnrolled));
            SetMockReaderWithTestData(tstDataSet);

            MockAcademicSessionDaoImpl mockAcademicSessionDaoImpl = new MockAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockAcademicSessionDaoImpl = mockAcademicSessionDaoImpl;

            classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadUsingAcademicSessionId);
        }

        [TestCase]
        public void Read_ShouldCallExpectedMethodFromCourseDaoImpl()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapClassEnrolled(tdBuilder.classEnrolled));
            SetMockReaderWithTestData(tstDataSet);

            MockCourseDaoImpl mockCourseDaoImpl = new MockCourseDaoImpl(tdBuilder);
            DaoImplFactory.MockCourseDaoImpl = mockCourseDaoImpl;

            classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_ReadUsingCourseId);
        }

        [TestCase]
        public void Read_ShouldCallExpectedMethodsFrom_JctClassResourceDaoImpl_And_ResourceDaoImpl()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapClassEnrolled(tdBuilder.classEnrolled));
            SetMockReaderWithTestData(tstDataSet);

            MockResourceDaoImpl mockResourceDaoImpl = new MockResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockResourceDaoImpl = mockResourceDaoImpl;

            MockJctClassResourceDaoImpl mockJctClassResourceDaoImpl = new MockJctClassResourceDaoImpl(tdBuilder);
            mockJctClassResourceDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockJctClassResourceDaoImpl = mockJctClassResourceDaoImpl;

            classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockJctClassResourceDaoImpl.WasCalled_ReadWithClassId);
            Assert.IsTrue(mockResourceDaoImpl.WasCalled_ReadUsingResourceId);
        }

        [TestCase]
        public void Read_ShouldCallExpectedMethodsFromJctClassPeriodDaoImpl()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapClassEnrolled(tdBuilder.classEnrolled));
            SetMockReaderWithTestData(tstDataSet);

            MockJctClassPeriodDaoImpl mockJctClassPeriodDaoImpl = new MockJctClassPeriodDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassPeriodDaoImpl = mockJctClassPeriodDaoImpl;

            classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockJctClassPeriodDaoImpl.WasCalled_ReadWithClassId);
        }

        [TestCase]
        public void Write_ShouldCallExpectedMethodFrom_AcademicSessionDaoImpl()
        {
            MockAcademicSessionDaoImpl mockAcademicSessionDaoImpl = new MockAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockAcademicSessionDaoImpl = mockAcademicSessionDaoImpl;
            DaoImplFactory.MockCourseDaoImpl = new MockCourseDaoImpl(tdBuilder);
            DaoImplFactory.MockResourceDaoImpl = new MockResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassResourceDaoImpl = new MockJctClassResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassPeriodDaoImpl = new MockJctClassPeriodDaoImpl(tdBuilder); ;
            DaoImplFactory.MockPersonDaoImpl = new MockPersonDaoImpl(tdBuilder);

            classEnrolledDaoImpl.Write(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadUsingAcademicSessionId);
        }

        [TestCase]
        public void Write_ShouldCallExpectedMethodFrom_CourseDaoImpl()
        {
            DaoImplFactory.MockAcademicSessionDaoImpl = new MockAcademicSessionDaoImpl(tdBuilder);
            MockCourseDaoImpl mockCourseDaoImpl = new MockCourseDaoImpl(tdBuilder);
            DaoImplFactory.MockCourseDaoImpl = mockCourseDaoImpl;
            DaoImplFactory.MockResourceDaoImpl = new MockResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassResourceDaoImpl = new MockJctClassResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassPeriodDaoImpl = new MockJctClassPeriodDaoImpl(tdBuilder); ;
            DaoImplFactory.MockPersonDaoImpl = new MockPersonDaoImpl(tdBuilder);

            classEnrolledDaoImpl.Write(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_ReadUsingCourseId);
        }

        [TestCase]
        public void Write_ShouldCallExpectedMethodFrom_ResourceDaoImpl()
        {
            DaoImplFactory.MockAcademicSessionDaoImpl = new MockAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockCourseDaoImpl = new MockCourseDaoImpl(tdBuilder);
            MockResourceDaoImpl mockResourceDaoImpl = new MockResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockResourceDaoImpl = mockResourceDaoImpl;
            DaoImplFactory.MockJctClassResourceDaoImpl = new MockJctClassResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassPeriodDaoImpl = new MockJctClassPeriodDaoImpl(tdBuilder); ;
            DaoImplFactory.MockPersonDaoImpl = new MockPersonDaoImpl(tdBuilder);

            classEnrolledDaoImpl.Write(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockResourceDaoImpl.WasCalled_Read);
        }

        [TestCase]
        public void Write_ShouldCallExpectedMethodFrom_JctClassPeriod()
        {
            DaoImplFactory.MockAcademicSessionDaoImpl = new MockAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockCourseDaoImpl = new MockCourseDaoImpl(tdBuilder);
            DaoImplFactory.MockResourceDaoImpl = new MockResourceDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassResourceDaoImpl = new MockJctClassResourceDaoImpl(tdBuilder);
            MockJctClassPeriodDaoImpl mockJctClassPeriodDaoImpl = new MockJctClassPeriodDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassPeriodDaoImpl = mockJctClassPeriodDaoImpl;
            DaoImplFactory.MockPersonDaoImpl = new MockPersonDaoImpl(tdBuilder);

            classEnrolledDaoImpl.Write(tdBuilder.classEnrolled, _netus2DbConnection);

            Assert.IsTrue(mockJctClassPeriodDaoImpl.WasCalled_ReadWithClassId);
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
                        values[0] = tstDataSet[count]["class_enrolled_id"];
                        values[1] = tstDataSet[count]["name"];
                        values[2] = tstDataSet[count]["class_enrolled_code"];
                        values[3] = tstDataSet[count]["enum_class_enrolled_id"];
                        values[4] = tstDataSet[count]["room"];
                        values[5] = tstDataSet[count]["course_id"];
                        values[6] = tstDataSet[count]["academic_session_id"];
                        values[7] = tstDataSet[count]["created"];
                        values[8] = tstDataSet[count]["created_by"];
                        values[9] = tstDataSet[count]["changed"];
                        values[10] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "class_enrolled_id");
            reader.Setup(x => x.GetOrdinal("class_enrolled_id"))
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
                .Returns(() => "class_enrolled_code");
            reader.Setup(x => x.GetOrdinal("class_enrolled_code"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "enum_class_enrolled_id");
            reader.Setup(x => x.GetOrdinal("enum_class_enrolled_id"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "room");
            reader.Setup(x => x.GetOrdinal("room"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "course_id");
            reader.Setup(x => x.GetOrdinal("course_id"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "academic_session_id");
            reader.Setup(x => x.GetOrdinal("academic_session_id"))
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