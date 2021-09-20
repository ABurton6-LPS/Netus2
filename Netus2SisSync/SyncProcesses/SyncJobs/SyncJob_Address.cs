using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.UtilityTools;
using System.Data;
using System.Threading;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_Address : SyncJob
    {
        IConnectable _sisConnection;
        public DataTable _dtAddress;

        public SyncJob_Address(string name, IConnectable sisConnection)
            : base(name)
        {
            _sisConnection = sisConnection;
            SyncLogger.LogNewJob(this);
        }

        public void Start()
        {
            try
            {
                ReadFromSis();
                RunJobTasks();
            }
            finally
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"]);
            }
        }

        public void ReadFromSis()
        {
            _dtAddress = new DataTableFactory().Dt_Sis_Address;
            _dtAddress = _sisConnection.ReadIntoDataTable(SyncScripts.ReadSis_Address_SQL, _dtAddress);
        }

        private void RunJobTasks()
        {
            CountDownLatch latch = new CountDownLatch(_dtAddress.Rows.Count);
            foreach (DataRow row in _dtAddress.Rows)
            {
                new Thread(syncThread => SyncTask.Execute_Address_RecordSync(this, row, latch))
                    .Start();
            }
            latch.Wait();
        }
    }
}