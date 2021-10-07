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
    public class CourseDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        CourseDaoImpl courseDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            courseDaoImpl = new CourseDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            DaoImplFactory.MockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseSubjectDaoImpl = new MockJctCourseSubjectDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseGradeDaoImpl = new MockJctCourseGradeDaoImpl(tdBuilder);

            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM course " +
                "WHERE 1=1 " +
                "AND course_id = " + tdBuilder.spanishCourse.Id + " " +
                "AND name = '" + tdBuilder.spanishCourse.Name + "' " +
                "AND course_code = '" + tdBuilder.spanishCourse.CourseCode + "' ";

            courseDaoImpl.Delete(tdBuilder.spanishCourse, _netus2DbConnection);
        }

        [TestCase]
        public void DeleteClassEnrolled_ShouldCallExpectedMethod()
        {
            MockClassEnrolledDaoImpl mockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            DaoImplFactory.MockClassEnrolledDaoImpl = mockClassEnrolledDaoImpl;
            DaoImplFactory.MockJctCourseSubjectDaoImpl = new MockJctCourseSubjectDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseGradeDaoImpl = new MockJctCourseGradeDaoImpl(tdBuilder);

            courseDaoImpl.Delete(tdBuilder.spanishCourse, _netus2DbConnection);

            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_UsingCourseId);
        }

        [TestCase]
        public void DeleteJctCourseSubject_ShouldCallExpectedMethod()
        {
            DaoImplFactory.MockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            MockJctCourseSubjectDaoImpl mockJctCourseSubjectDaoImpl = new MockJctCourseSubjectDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseSubjectDaoImpl = mockJctCourseSubjectDaoImpl;
            DaoImplFactory.MockJctCourseGradeDaoImpl = new MockJctCourseGradeDaoImpl(tdBuilder);

            courseDaoImpl.Delete(tdBuilder.spanishCourse, _netus2DbConnection);

            Assert.IsTrue(mockJctCourseSubjectDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void DeleteJctCourseGrade_ShouldCallExpectedMethod()
        {
            DaoImplFactory.MockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseSubjectDaoImpl = new MockJctCourseSubjectDaoImpl(tdBuilder);
            MockJctCourseGradeDaoImpl mockJctCourseGradeDaoImpl = new MockJctCourseGradeDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseGradeDaoImpl = mockJctCourseGradeDaoImpl;

            courseDaoImpl.Delete(tdBuilder.spanishCourse, _netus2DbConnection);

            Assert.IsTrue(mockJctCourseGradeDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void Read_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM course WHERE course_id = " + tdBuilder.spanishCourse.Id;

            courseDaoImpl.Read(tdBuilder.spanishCourse.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WithIdPopulated_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM course WHERE 1=1 AND course_id = " + tdBuilder.spanishCourse.Id + " ";

            courseDaoImpl.Read(tdBuilder.spanishCourse, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WithoutIdPopulated_ShouldUseExpectedSql()
        {
            tdBuilder.spanishCourse.Id = -1;

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM course " +
                "WHERE 1=1 " +
                "AND name = '" + tdBuilder.spanishCourse.Name + "' " +
                "AND course_code = '" + tdBuilder.spanishCourse.CourseCode + "' ";

            courseDaoImpl.Read(tdBuilder.spanishCourse, _netus2DbConnection);
        }

        [TestCase]
        public void ReadJctCourseSubject_ShouldCallExpectedMethod()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapCourse(tdBuilder.spanishCourse));
            SetMockReaderWithTestData(tstDataSet);

            MockJctCourseSubjectDaoImpl mockJctCourseSubjectDaoImpl = new MockJctCourseSubjectDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseSubjectDaoImpl = mockJctCourseSubjectDaoImpl;

            courseDaoImpl.Read(tdBuilder.spanishCourse, _netus2DbConnection);

            Assert.IsTrue(mockJctCourseSubjectDaoImpl.WasCalled_ReadWithCourseId);
        }

        [TestCase]
        public void ReadJctCourseGrade_ShouldCallExpectedMethod()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapCourse(tdBuilder.spanishCourse));
            SetMockReaderWithTestData(tstDataSet);

            MockJctCourseGradeDaoImpl mockJctCourseGradeDaoImpl = new MockJctCourseGradeDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseGradeDaoImpl = mockJctCourseGradeDaoImpl;

            courseDaoImpl.Read(tdBuilder.spanishCourse, _netus2DbConnection);

            Assert.IsTrue(mockJctCourseGradeDaoImpl.WasCalled_ReadWithCourseId);
        }

        [TestCase]
        public void Update_WhenRecordIsNotFound_ShouldUseExpectedSql()
        {
            DaoImplFactory.MockJctCourseSubjectDaoImpl = new MockJctCourseSubjectDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseGradeDaoImpl = new MockJctCourseGradeDaoImpl(tdBuilder);

            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO course (" +
                "name, " +
                "course_code, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "'" + tdBuilder.spanishCourse.Name + "', " +
                "'" + tdBuilder.spanishCourse.CourseCode + "', " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            courseDaoImpl.Update(tdBuilder.spanishCourse, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhenRecordIsFound_ShouldUseExpectedSql()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapCourse(tdBuilder.spanishCourse));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE course SET " +
                "name = '" + tdBuilder.spanishCourse.Name + "', " +
                "course_code = '" + tdBuilder.spanishCourse.CourseCode + "', " +
                "changed = dbo.CURRENT_DATETIME(), " +
                "changed_by = 'Netus2' " +
                "WHERE course_id = " + tdBuilder.spanishCourse.Id;

            courseDaoImpl.Update(tdBuilder.spanishCourse, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhenRecordIsFound_ShouldCallExpectedMethods()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapCourse(tdBuilder.spanishCourse));
            SetMockReaderWithTestData(tstDataSet);

            MockJctCourseSubjectDaoImpl mockJctCourseSubjectDaoImpl = new MockJctCourseSubjectDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseSubjectDaoImpl = mockJctCourseSubjectDaoImpl;
            MockJctCourseGradeDaoImpl mockJctCourseGradeDaoImpl = new MockJctCourseGradeDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseGradeDaoImpl = mockJctCourseGradeDaoImpl;

            courseDaoImpl.Update(tdBuilder.spanishCourse, _netus2DbConnection);

            Assert.IsTrue(mockJctCourseSubjectDaoImpl.WasCalled_ReadWithCourseId);
            Assert.IsTrue(mockJctCourseGradeDaoImpl.WasCalled_ReadWithCourseId);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO course (" +
                "name, " +
                "course_code, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "'" + tdBuilder.spanishCourse.Name + "', " +
                "'" + tdBuilder.spanishCourse.CourseCode + "', " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            courseDaoImpl.Write(tdBuilder.spanishCourse, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldCallExpectedMethods()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapCourse(tdBuilder.spanishCourse));
            SetMockReaderWithTestData(tstDataSet);

            MockJctCourseSubjectDaoImpl mockJctCourseSubjectDaoImpl = new MockJctCourseSubjectDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseSubjectDaoImpl = mockJctCourseSubjectDaoImpl;
            MockJctCourseGradeDaoImpl mockJctCourseGradeDaoImpl = new MockJctCourseGradeDaoImpl(tdBuilder);
            DaoImplFactory.MockJctCourseGradeDaoImpl = mockJctCourseGradeDaoImpl;

            courseDaoImpl.Write(tdBuilder.spanishCourse, _netus2DbConnection);

            Assert.IsTrue(mockJctCourseSubjectDaoImpl.WasCalled_ReadWithCourseId);
            Assert.IsTrue(mockJctCourseGradeDaoImpl.WasCalled_ReadWithCourseId);
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
                        values[0] = tstDataSet[count]["course_id"];
                        values[1] = tstDataSet[count]["name"];
                        values[2] = tstDataSet[count]["course_code"];
                        values[3] = tstDataSet[count]["created"];
                        values[4] = tstDataSet[count]["created_by"];
                        values[5] = tstDataSet[count]["changed"];
                        values[6] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "course_id");
            reader.Setup(x => x.GetOrdinal("course_id"))
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
                .Returns(() => "course_code");
            reader.Setup(x => x.GetOrdinal("course_code"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

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
