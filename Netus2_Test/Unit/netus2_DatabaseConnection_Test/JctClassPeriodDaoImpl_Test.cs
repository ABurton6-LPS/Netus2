using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class JctClassPeriodDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        JctClassPeriodDaoImpl jctClassPeriodDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            jctClassPeriodDaoImpl = new JctClassPeriodDaoImpl();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM jct_class_period " +
                "WHERE 1=1 " +
                "AND class_id = " + tdBuilder.classEnrolled.Id + " " +
                "AND enum_period_id = " + tdBuilder.classEnrolled.Periods[0].Id;

            jctClassPeriodDaoImpl.Delete(tdBuilder.classEnrolled.Id, tdBuilder.classEnrolled.Periods[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_class_period " +
                "WHERE 1=1 AND " +
                "class_id = " + tdBuilder.classEnrolled.Id + " " +
                "AND enum_period_id = " + tdBuilder.classEnrolled.Periods[0].Id;

            jctClassPeriodDaoImpl.Read(tdBuilder.classEnrolled.Id, tdBuilder.classEnrolled.Periods[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileUsingOnlyClassEnrolledId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_class_period " +
                "WHERE 1=1 " +
                "AND class_id = " + tdBuilder.classEnrolled.Id;

            jctClassPeriodDaoImpl.Read(tdBuilder.classEnrolled.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "INSERT INTO jct_class_period (class_id, enum_period_id) VALUES (" +
                tdBuilder.classEnrolled.Id + ", " +
                tdBuilder.classEnrolled.Periods[0].Id + ")";

            jctClassPeriodDaoImpl.Write(tdBuilder.classEnrolled.Id, tdBuilder.classEnrolled.Periods[0].Id, _netus2DbConnection);
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
