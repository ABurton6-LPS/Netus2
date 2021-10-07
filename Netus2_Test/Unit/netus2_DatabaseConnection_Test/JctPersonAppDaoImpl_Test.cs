using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class JctPersonAppDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        JctPersonAppDaoImpl jctPersonAppDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            jctPersonAppDaoImpl = new JctPersonAppDaoImpl();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM jct_person_app " +
                "WHERE 1=1 " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND app_id = " + tdBuilder.teacher.Applications[0].Id;

            jctPersonAppDaoImpl.Delete(tdBuilder.teacher.Id, tdBuilder.teacher.Applications[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileGivenPersonIdAndAppId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_person_app " +
                "WHERE 1=1 " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND app_id = " + tdBuilder.teacher.Applications[0].Id;

            jctPersonAppDaoImpl.Read(tdBuilder.teacher.Id, tdBuilder.teacher.Applications[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_person_app " +
                "WHERE person_id = " + tdBuilder.teacher.Id;

            jctPersonAppDaoImpl.Read_WithPersonId(tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithAppId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_person_app " +
                "WHERE app_id = " + tdBuilder.teacher.Applications[0].Id;

            jctPersonAppDaoImpl.Read_WithAppId(tdBuilder.teacher.Applications[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "INSERT INTO jct_person_app (" +
                "person_id, " +
                "app_id" +
                ") VALUES (" +
                tdBuilder.teacher.Id + ", " +
                tdBuilder.teacher.Applications[0].Id + ")";

            jctPersonAppDaoImpl.Write(tdBuilder.teacher.Id, tdBuilder.teacher.Applications[0].Id, _netus2DbConnection);
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
