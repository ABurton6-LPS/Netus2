using NUnit.Framework;
using Netus2_DatabaseConnection.dbAccess;
using Netus2SisSync.UtilityTools;
using Netus2SisSync.SyncProcesses;
using Netus2SisSync.SyncProcesses.Tasks;
using System.Data;
using System.Data.SqlClient;
using Moq;
using Netus2_DatabaseConnection.enumerations;

namespace Netus2_Test.netus2SisSync_Test
{
    class Netus2SisSync_Test
    {
        MockDatabaseConnection mockMiStarDbConnection;
        MockDatabaseConnection mockNetus2DbConnection;

        [SetUp]
        public void SetUp()
        {
            mockMiStarDbConnection = (MockDatabaseConnection)DbConnectionFactory.GetConnection("Mock");
            mockMiStarDbConnection.OpenConnection();
            mockNetus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetConnection("Mock");
            mockNetus2DbConnection.OpenConnection();
        }

        [TestCase]
        public void SyncOrganizationTest()
        {
            SyncJob syncOrganizationJobTest = SyncLogger.LogNewJob("SyncOrganizationTest", mockMiStarDbConnection);
            DataTable results = SyncOrganization.ReadFromSis(syncOrganizationJobTest, mockMiStarDbConnection, mockNetus2DbConnection);
            
            Assert.NotNull(results);
        }
    }
}