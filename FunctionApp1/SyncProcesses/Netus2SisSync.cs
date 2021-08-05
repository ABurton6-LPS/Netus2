using Microsoft.Azure.WebJobs;
using Netus2_DatabaseConnection.dbAccess;
using Netus2SisSync.SyncProcesses.SyncJobs;
using System;

namespace Netus2SisSync.SyncProcesses
{
    public static class Netus2SisSync
    {
        [FunctionName("Netus2SisSync")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer)
        {
            IConnectable netus2Connection = DbConnectionFactory.GetNetus2Connection();
            IConnectable sisConnection = DbConnectionFactory.GetSisConnection();
            try
            {
                new SyncJob_Organization("SyncJob_Organization", DateTime.Now, sisConnection, netus2Connection)
                    .Start();

                new SyncJob_AcademicSession("SyncJob_AcademicSession", DateTime.Now, sisConnection, netus2Connection)
                    .Start();

                new SyncJob_Person("SyncJob_Person", DateTime.Now, sisConnection, netus2Connection)
                    .Start();
            }
            finally
            {
                sisConnection.CloseConnection();
                netus2Connection.CloseConnection();
            }
        }        
    }
}