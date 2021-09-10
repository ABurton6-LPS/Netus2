using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class JctPersonPersonDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        JctPersonPersonDaoImpl jctPersonPersonDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            jctPersonPersonDaoImpl = new JctPersonPersonDaoImpl();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM jct_person_person " +
                "WHERE 1=1 " +
                "AND person_one_id = " + tdBuilder.teacher.Id + " " +
                "AND person_two_id = " + tdBuilder.student.Id;

            jctPersonPersonDaoImpl.Delete(tdBuilder.teacher.Id, tdBuilder.student.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileGivenPersonIdAndPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_person_person " +
                "WHERE 1=1 " +
                "AND person_one_id = " + tdBuilder.teacher.Id + " " +
                "AND person_two_id = " + tdBuilder.student.Id;

            jctPersonPersonDaoImpl.Read(tdBuilder.teacher.Id, tdBuilder.student.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileGivenOnlyOnePersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_person_person " +
                "WHERE 1=1 " +
                "AND person_one_id = " + tdBuilder.teacher.Id;

            jctPersonPersonDaoImpl.Read(tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "INSERT INTO jct_person_person (" +
                "person_one_id, " +
                "person_two_id" +
                ") VALUES (" +
                tdBuilder.teacher.Id + ", " +
                tdBuilder.student.Id + ")";

            jctPersonPersonDaoImpl.Write(tdBuilder.teacher.Id, tdBuilder.student.Id, _netus2DbConnection);
        }

        [TearDown]
        public void TearDown()
        {
            DbConnectionFactory.MockDatabaseConnection = null;
        }
    }
}
