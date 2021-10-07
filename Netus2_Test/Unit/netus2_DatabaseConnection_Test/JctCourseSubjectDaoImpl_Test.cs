using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class JctCourseSubjectDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        JctCourseSubjectDaoImpl jctCourseSubjectDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            jctCourseSubjectDaoImpl = new JctCourseSubjectDaoImpl();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM jct_course_subject WHERE 1=1 " +
                "AND course_id = " + tdBuilder.spanishCourse.Id + " " +
                "AND enum_subject_id = " + tdBuilder.spanishCourse.Subjects[0].Id;

            jctCourseSubjectDaoImpl.Delete(tdBuilder.spanishCourse.Id, tdBuilder.spanishCourse.Subjects[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileGivenCourseIdAndSubjectId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_course_subject " +
                "WHERE 1=1 " +
                "AND course_id = " + tdBuilder.spanishCourse.Id + " " +
                "AND enum_subject_id = " + tdBuilder.spanishCourse.Subjects[0].Id;

            jctCourseSubjectDaoImpl.Read(tdBuilder.spanishCourse.Id, tdBuilder.spanishCourse.Subjects[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileGivenCourseId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_course_subject " +
                "WHERE 1=1 " +
                "AND course_id = " + tdBuilder.spanishCourse.Id;

            jctCourseSubjectDaoImpl.Read(tdBuilder.spanishCourse.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "INSERT INTO jct_course_subject (course_id, enum_subject_id) VALUES (" +
                tdBuilder.spanishCourse.Id + ", " +
                tdBuilder.spanishCourse.Subjects[0].Id + ")";

            jctCourseSubjectDaoImpl.Write(tdBuilder.spanishCourse.Id, tdBuilder.spanishCourse.Subjects[0].Id, _netus2DbConnection);
        }

        [TearDown]
        public void TearDown()
        {
            _netus2DbConnection.expectedNewRecordSql = null;
            _netus2DbConnection.expectedNonQuerySql = null;
            _netus2DbConnection.expectedReaderSql = null;
        }
    }
}