using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class JctPersonAddressDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        JctPersonAddressDaoImpl jctPersonAddressDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            jctPersonAddressDaoImpl = new JctPersonAddressDaoImpl();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM jct_person_address " +
                "WHERE 1=1 " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND address_id = " + tdBuilder.teacher.Addresses[0].Id;

            jctPersonAddressDaoImpl.Delete(tdBuilder.teacher.Id, tdBuilder.teacher.Addresses[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileGivenPersonIdAndAddressId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_person_address " +
                "WHERE 1=1 " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND address_id = " + tdBuilder.teacher.Addresses[0].Id;

            jctPersonAddressDaoImpl.Read(tdBuilder.teacher.Id, tdBuilder.teacher.Addresses[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_person_address " +
                "WHERE person_id = " + tdBuilder.teacher.Id;

            jctPersonAddressDaoImpl.Read_WithPersonId(tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithAddressId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_person_address " +
                "WHERE address_id = " + tdBuilder.teacher.Addresses[0].Id;

            jctPersonAddressDaoImpl.Read_WithAddressId(tdBuilder.teacher.Addresses[0].Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "INSERT INTO jct_person_address (" +
                "person_id, " +
                "address_id" +
                ") VALUES (" +
                tdBuilder.teacher.Id + ", " +
                tdBuilder.teacher.Addresses[0].Id + ")";

            jctPersonAddressDaoImpl.Write(tdBuilder.teacher.Id, tdBuilder.teacher.Addresses[0].Id, _netus2DbConnection);
        }
    }
}
