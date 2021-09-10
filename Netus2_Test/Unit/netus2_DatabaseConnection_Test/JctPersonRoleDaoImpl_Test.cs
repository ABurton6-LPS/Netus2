using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class JctPersonRoleDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        JctPersonRoleDaoImpl jctPersonRoleDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            jctPersonRoleDaoImpl = new JctPersonRoleDaoImpl();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM jct_person_role " +
                "WHERE 1=1 " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND enum_role_id = " + tdBuilder.teacher.Roles[0].Id;

            jctPersonRoleDaoImpl.Delete(tdBuilder.teacher.Id, tdBuilder.teacher.Roles[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileGivenPersonIdAndRoleId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_person_role " +
                "WHERE 1=1 " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND enum_role_id = " + tdBuilder.teacher.Roles[0].Id;

            jctPersonRoleDaoImpl.Read(tdBuilder.teacher.Id, tdBuilder.teacher.Roles[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileGivenOnlyPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_person_role " +
                "WHERE 1=1 " +
                "AND person_id = " + tdBuilder.teacher.Id;

            jctPersonRoleDaoImpl.Read(tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "INSERT INTO jct_person_role (" +
                "person_id, " +
                "enum_role_id" +
                ") VALUES (" +
                tdBuilder.teacher.Id + ", " +
                tdBuilder.teacher.Roles[0].Id + ")";

            jctPersonRoleDaoImpl.Write(tdBuilder.teacher.Id, tdBuilder.teacher.Roles[0].Id, _netus2DbConnection);
        }

        [TearDown]
        public void TearDown()
        {
            DbConnectionFactory.MockDatabaseConnection = null;
        }
    }
}
