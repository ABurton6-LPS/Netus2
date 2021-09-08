using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class JctEnrollmentAcademicSessionDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        JctEnrollmentAcademicSessionDaoImpl jctEnrollmentAcademicSessionDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            jctEnrollmentAcademicSessionDaoImpl = new JctEnrollmentAcademicSessionDaoImpl();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM jct_enrollment_academic_session " +
                "WHERE 1=1 " +
                "AND enrollment_id = " + tdBuilder.enrollment.Id + " " +
                "AND academic_session_id = " + tdBuilder.semester1.Id;

            jctEnrollmentAcademicSessionDaoImpl.Delete(tdBuilder.enrollment.Id, tdBuilder.semester1.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileGivenEnrollmentIdAndAcademicSessionId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_enrollment_academic_session " +
                "WHERE 1=1 " +
                "AND enrollment_id = " + tdBuilder.enrollment.Id + " " +
                "AND academic_session_id = " + tdBuilder.semester1.Id;

            jctEnrollmentAcademicSessionDaoImpl.Read(tdBuilder.enrollment.Id, tdBuilder.semester1.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithEnrollmentId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_enrollment_academic_session " +
                "WHERE enrollment_id = " + tdBuilder.enrollment.Id;

            jctEnrollmentAcademicSessionDaoImpl.Read_WithEnrollmentId(tdBuilder.enrollment.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithAcademicSessionId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_enrollment_academic_session " +
                "WHERE academic_session_id = " + tdBuilder.semester1.Id;

            jctEnrollmentAcademicSessionDaoImpl.Read_WithAcademicSessionId(tdBuilder.semester1.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "INSERT INTO jct_enrollment_academic_session (" +
                "enrollment_id, " +
                "academic_session_id" +
                ") VALUES (" +
                tdBuilder.enrollment.Id + ", " +
                tdBuilder.semester1.Id + ")";

            jctEnrollmentAcademicSessionDaoImpl.Write(tdBuilder.enrollment.Id, tdBuilder.semester1.Id, _netus2DbConnection);
        }

        [TearDown]
        public void TearDown()
        {
            DbConnectionFactory.MockDatabaseConnection = null;
        }
    }
}
