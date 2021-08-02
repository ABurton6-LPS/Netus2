using Microsoft.Azure.WebJobs;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2SisSync.SyncProcesses.Tasks;
using Netus2SisSync.UtilityTools;

namespace Netus2SisSync.SyncProcesses
{
    public static class Netus2SisSync
    {
        [FunctionName("Netus2SisSync")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer)
        {
            IConnectable netus2Connection = DbConnectionFactory.GetConnection("Netus2");
            IConnectable miStarConnection = DbConnectionFactory.GetConnection("MiStar");
            try
            {
                netus2Connection.OpenConnection();
                miStarConnection.OpenConnection();

                SyncJob syncOrganizationJob = SyncLogger.LogNewJob("Organization_Start", netus2Connection);
                SyncOrganization.Start(syncOrganizationJob, miStarConnection, netus2Connection);
                SyncLogger.LogStatus(syncOrganizationJob, Enum_Sync_Status.values["end"], netus2Connection);

                SyncJob syncAcademicSessionJob = SyncLogger.LogNewJob("AcademicSession_Start", netus2Connection);
                SyncAcademicSession.Start(syncAcademicSessionJob, miStarConnection, netus2Connection);
                SyncLogger.LogStatus(syncAcademicSessionJob, Enum_Sync_Status.values["end"], netus2Connection);

                SyncJob syncPersonJob = SyncLogger.LogNewJob("Person_Start", netus2Connection);
                SyncPerson.Start(syncPersonJob, miStarConnection, netus2Connection);
                SyncLogger.LogStatus(syncPersonJob, Enum_Sync_Status.values["end"], netus2Connection);
            }
            finally
            {
                miStarConnection.CloseConnection();
                netus2Connection.CloseConnection();
            }
        }        
    }
}