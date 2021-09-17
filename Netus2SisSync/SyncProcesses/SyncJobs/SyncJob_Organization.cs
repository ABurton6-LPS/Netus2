using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.SyncProcesses.SyncTasks.OrganizationTasks;
using Netus2SisSync.UtilityTools;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_Organization : SyncJob
    {
        IConnectable _sisConnection;
        IConnectable _netus2Connection;
        public DataTable _dtOrganization;

        public SyncJob_Organization(string name, IConnectable sisConnection, IConnectable netus2Connection)
            : base(name)
        {
            _sisConnection = sisConnection;
            _netus2Connection = netus2Connection;
            SyncLogger.LogNewJob(this, _netus2Connection);
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
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"], _netus2Connection);
            }
        }

        public void ReadFromSis()
        {
            _dtOrganization = new DataTableFactory().Dt_Sis_Organization;
            _dtOrganization = _sisConnection.ReadIntoDataTable(SyncScripts.ReadSis_Organization_SQL, _dtOrganization).Result;
        }

        private void RunJobTasks()
        {
            CountDownLatch childLatch = new CountDownLatch(_dtOrganization.Rows.Count);
            foreach (DataRow row in _dtOrganization.Rows)
            {
                new Thread(syncThread => SyncTask.Execute_Organization_ChildRecordSync(this, row, childLatch))
                    .Start();
            }
            childLatch.Wait();

            CountDownLatch parentLatch = new CountDownLatch(_dtOrganization.Rows.Count);
            foreach (DataRow row in _dtOrganization.Rows)
            {
                new Thread(syncThread => SyncTask.Execute_Organization_ParentRecordSync(this, row, childLatch))
                    .Start();
            }
            parentLatch.Wait();
        }
    }
}