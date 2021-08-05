using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.SyncProcesses;
using System.Data;
using System.Threading;

namespace Netus2SisSync.UtilityTools
{
    public static class TaskExecutor
    {
        public static void ExecuteTask(SyncTask task, DataTable dataTable)
        {
            CountDownLatch latch = new CountDownLatch(dataTable.Rows.Count);
            foreach (DataRow row in dataTable.Rows)
            {
                new Thread(syncthread => task.Execute(row, latch)).Start();
                Thread.Sleep(100);
            }
            latch.Wait();
        }
    }
}
