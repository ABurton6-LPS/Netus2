using Microsoft.Azure.WebJobs;
using Netus2;
using Netus2.dbAccess;
using Netus2SisSync;

namespace FunctionApp1
{
    public static class Netus2SisSync
    {
        [FunctionName("Netus2SisSync")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer)
        {
            IConnectable netus2Connection = new Netus2DatabaseConnection();
            IConnectable miStarConnection = new MiStarDatabaseConnection();
            try
            {
                netus2Connection.OpenConnection();
                miStarConnection.OpenConnection();

                SyncOrganization.Start(miStarConnection, netus2Connection);
                SyncAcademicSession.Start(miStarConnection, netus2Connection);
                SyncPerson.Start(miStarConnection, netus2Connection);
            }
            finally
            {
                miStarConnection.CloseConnection();
                netus2Connection.CloseConnection();
            }
        }        
    }
}