using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
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

            //The order these jobs run in is important. Some depend on others to have been ran first to ensure that database relationships are properly maintained.

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

                if (ShouldRunSync("PhoneNumber"))
                    new SyncJob_PhoneNumber().Start();

                if (ShouldRunSync("Email"))
                    new SyncJob_Email().Start();

                if (ShouldRunSync("Course"))
                    new SyncJob_Course().Start();

                if (ShouldRunSync("ClassEnrolled"))
                    new SyncJob_ClassEnrolled().Start();

                if (ShouldRunSync("Enrollment"))
                    new SyncJob_Enrollment().Start();

                //if (ShouldRunSync("LineItem"))
                //    new SyncJob_LineItem().Start();

                //if (ShouldRunSync("Mark"))
                //    new SyncJob_Mark().Start();
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