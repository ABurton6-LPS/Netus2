using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Netus2_DatabaseConnection.dbAccess;
using Netus2SisSync.SyncProcesses.SyncJobs;
using System;

namespace Netus2SisSync.SyncProcesses
{
    public static class Netus2SisSync
    {
        [FunctionName("Netus2SisSync")]
        public static void Run([TimerTrigger("%CRON_EXPRESSION%")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Netus2SisSync Timer trigger function started at: {DateTime.Now}");

            if (ShouldRunSync("AllJobs"))
            {
                if (ShouldRunSync("Organization"))
                    new SyncJob_Organization().Start();

                if (ShouldRunSync("AcademicSession"))
                    new SyncJob_AcademicSession().Start();

                if (ShouldRunSync("Person"))
                    new SyncJob_Person().Start();

                if (ShouldRunSync("Address"))
                    new SyncJob_Address().Start();
            }

            log.LogInformation($"Netus2SisSync Timer trigger function finished at: {DateTime.Now}");
        }

        private static bool ShouldRunSync(string syncProcessName)
        {
            string shouldRunSync = System.Environment.GetEnvironmentVariable("ShouldRunSync_" + syncProcessName);
            if (shouldRunSync == "true" || shouldRunSync == null)
                return true;
            else
                return false;
        }
    }
}