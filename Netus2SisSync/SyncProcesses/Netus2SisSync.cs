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
            new SyncJob_Organization().Start();

            new SyncJob_AcademicSession().Start();

            new SyncJob_Person().Start();
        }        
    }
}