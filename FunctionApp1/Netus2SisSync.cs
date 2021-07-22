using System;
using System.Data;
using System.Threading;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Netus2;
using Netus2.dbAccess;
using Netus2_DatabaseConnection;
using Netus2SisSync;

namespace FunctionApp1
{
    public static class Netus2SisSync
    {
        [FunctionName("Netus2SisSync")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function Netus2SisSync executed at: {DateTime.Now}");

            IConnectable miStarConnection = new MiStarDatabaseConnection();
            miStarConnection.OpenConnection();

            DataTable dtOrganization = SyncOrganization.ReadFromSis(miStarConnection);
            DataTable dtAcademicSession = SyncAcademicSession.ReadFromSis(miStarConnection);
            DataTable dtPerson = SyncPerson.ReadFromSis(miStarConnection);

            miStarConnection.CloseConnection();

            CountDownLatch dtOrganizationChildLatch = new CountDownLatch(dtOrganization.Rows.Count);
            foreach (DataRow row in dtOrganization.Rows)
            {
                new Thread(syncThread => SyncOrganization.SyncForChildRecords(row, dtOrganizationChildLatch, log)).Start();
                Thread.Sleep(100);
            }
            dtOrganizationChildLatch.Wait();

            CountDownLatch dtOrganizationParentLatch = new CountDownLatch(dtOrganization.Rows.Count);
            foreach (DataRow row in dtOrganization.Rows)
            {
                new Thread(syncThread => SyncOrganization.SyncForParentRecords(row, dtOrganizationParentLatch, log)).Start();
                Thread.Sleep(100);
            }
            dtOrganizationParentLatch.Wait();

            CountDownLatch dtAcademicSessionChildLatch = new CountDownLatch(dtAcademicSession.Rows.Count);
            foreach (DataRow row in dtAcademicSession.Rows)
            {
                new Thread(syncthread => SyncAcademicSession.SyncForChildRecords(row, dtAcademicSessionChildLatch, log)).Start();
                Thread.Sleep(100);
            }
            dtAcademicSessionChildLatch.Wait();

            CountDownLatch dtAcademicSessionParentLatch = new CountDownLatch(dtAcademicSession.Rows.Count);
            foreach (DataRow row in dtAcademicSession.Rows)
            {
                new Thread(syncthread => SyncAcademicSession.SyncForParentRecords(row, dtAcademicSessionParentLatch, log)).Start();
                Thread.Sleep(100);
            }
            dtAcademicSessionParentLatch.Wait();

            CountDownLatch dtPersonLatch = new CountDownLatch(dtPerson.Rows.Count);
            foreach (DataRow row in dtPerson.Rows)
            {
                new Thread(syncthread => SyncPerson.SyncForAllRecords(row, dtPersonLatch, log)).Start();
                Thread.Sleep(100);
            }
            dtPersonLatch.Wait();
        }
    }
}