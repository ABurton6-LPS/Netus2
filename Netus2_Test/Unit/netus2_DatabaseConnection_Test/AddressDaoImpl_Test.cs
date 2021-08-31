using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class AddressDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        AddressDaoImpl addressDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            addressDaoImpl = new AddressDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            Assert.Fail("Not yet implemented");
        }

        [TearDown]
        public void TearDown()
        {
            DbConnectionFactory.MockDatabaseConnection = null;
        }
    }
}