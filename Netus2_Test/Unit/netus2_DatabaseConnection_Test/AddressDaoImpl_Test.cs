using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_Test.MockDaoImpl;
using NUnit.Framework;

namespace Netus2_Test.Unit
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
            DaoImplFactory.MockJctPersonAddressDaoImpl = new MockJctPersonAddressDaoImpl(tdBuilder);

            _netus2DbConnection.expectedReaderSql =
                "DELETE FROM address " +
                "WHERE 1 = 1 " +
                "AND address_id = " + tdBuilder.address_Student.Id + " " +
                "AND address_line_1 LIKE '" + tdBuilder.address_Student.Line1 + "' " +
                "AND address_line_2 IS NULL " +
                "AND address_line_3 IS NULL " +
                "AND address_line_4 IS NULL " +
                "AND apartment IS NULL " +
                "AND city LIKE '" + tdBuilder.address_Student.City + "'" +
                "AND enum_state_province_id = " + tdBuilder.address_Student.StateProvince.Id + " " +
                "AND postal_code IS NULL " +
                "AND enum_country_id = " + tdBuilder.address_Student.Country.Id + " " +
                "AND is_current_id = " + tdBuilder.address_Student.IsCurrent.Id + " " +
                "AND enum_address_id = " + tdBuilder.address_Student.AddressType.Id;

            addressDaoImpl.Delete(tdBuilder.address_Student, _netus2DbConnection);
        }

        [TearDown]
        public void TearDown()
        {
            DaoImplFactory.MockJctPersonAddressDaoImpl = null;
            DbConnectionFactory.MockDatabaseConnection = null;
        }
    }
}