using Netus2_DatabaseConnection.dbAccess;
using Netus2SisSync.SyncProcesses.SyncJobs;
using NUnit.Framework;

namespace Netus2_Test.Integration
{
    public class Netus2Sync_Test
    {
        [TestCase]
        public void TestRun()
        {
            new SyncJob_Organization("SyncJob_Organization").Start();

            //new SyncJob_AcademicSession("SyncJob_AcademicSession", sisConnection, netus2Connection)
            //    .Start();

            //new SyncJob_Person("SyncJob_Person", sisConnection, netus2Connection)
            //    .Start();
        }
    }
}
