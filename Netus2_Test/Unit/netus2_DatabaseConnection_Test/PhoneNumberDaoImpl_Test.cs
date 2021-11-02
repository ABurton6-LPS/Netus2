using Moq;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_Test.MockDaoImpl;
using NUnit.Framework;
using System.Data;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class PhoneNumberDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        PhoneNumberDaoImpl phoneNumberDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            phoneNumberDaoImpl = new PhoneNumberDaoImpl();
        }

        [TestCase]
        public void DeletePhoneNumber_ShouldCallExpectedMethod()
        {
            MockJctPersonPhoneNumberDaoImpl mockJctPersonPhoneNumberDaoImpl = new MockJctPersonPhoneNumberDaoImpl(tdBuilder);
            mockJctPersonPhoneNumberDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockJctPersonPhoneNumberDaoImpl = mockJctPersonPhoneNumberDaoImpl;

            phoneNumberDaoImpl.Delete(tdBuilder.student.PhoneNumbers[0], _netus2DbConnection);

            Assert.IsTrue(mockJctPersonPhoneNumberDaoImpl.WasCalled_ReadWithPhoneNumberId);
            Assert.IsTrue(mockJctPersonPhoneNumberDaoImpl.WasCalled_Delete);
        }

        [TearDown]
        public void TearDown()
        {
            _netus2DbConnection.mockReader = new Mock<IDataReader>();
            _netus2DbConnection.expectedNewRecordSql = null;
            _netus2DbConnection.expectedNonQuerySql = null;
            _netus2DbConnection.expectedReaderSql = null;
        }
    }
}