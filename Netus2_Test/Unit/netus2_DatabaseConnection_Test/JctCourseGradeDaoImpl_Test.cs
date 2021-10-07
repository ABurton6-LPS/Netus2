using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class JctCourseGradeDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        JctCourseGradeDaoImpl jctCourseGradeDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            jctCourseGradeDaoImpl = new JctCourseGradeDaoImpl();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM jct_course_grade WHERE 1=1 " +
                "AND course_id = " + tdBuilder.spanishCourse.Id + " " +
                "AND enum_grade_id = " + tdBuilder.spanishCourse.Grades[0].Id;

            jctCourseGradeDaoImpl.Delete(tdBuilder.spanishCourse.Id, tdBuilder.spanishCourse.Grades[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileGivenCourseIdAndGradeId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_course_grade " +
                "WHERE 1=1 " +
                "AND course_id = " + tdBuilder.spanishCourse.Id + " " +
                "AND enum_grade_id = " + tdBuilder.spanishCourse.Grades[0].Id;

            jctCourseGradeDaoImpl.Read(tdBuilder.spanishCourse.Id, tdBuilder.spanishCourse.Grades[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileGivenCourseId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_course_grade " +
                "WHERE 1=1 " +
                "AND course_id = " + tdBuilder.spanishCourse.Id;

            jctCourseGradeDaoImpl.Read(tdBuilder.spanishCourse.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "INSERT INTO jct_course_grade (course_id, enum_grade_id) VALUES (" +
                tdBuilder.spanishCourse.Id + ", " +
                tdBuilder.spanishCourse.Grades[0].Id + ")";

            jctCourseGradeDaoImpl.Write(tdBuilder.spanishCourse.Id, tdBuilder.spanishCourse.Grades[0].Id, _netus2DbConnection);
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