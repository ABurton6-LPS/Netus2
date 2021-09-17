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
            //IConnectable netus2Connection = DbConnectionFactory.GetNetus2Connection();
            //IConnectable sisConnection = DbConnectionFactory.GetSisConnection();
            //try
            //{
            //    new SyncJob_Organization("SyncJob_Organization", sisConnection, netus2Connection)
            //        .Start();

            //    new SyncJob_AcademicSession("SyncJob_AcademicSession", sisConnection, netus2Connection)
            //        .Start();

            //    new SyncJob_Person("SyncJob_Person", sisConnection, netus2Connection)
            //        .Start();
            //}
            //finally
            //{
            //    sisConnection.CloseConnection();
            //    netus2Connection.CloseConnection();
            //}
        }
    }
}
