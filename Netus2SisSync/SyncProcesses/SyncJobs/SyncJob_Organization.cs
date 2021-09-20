using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.UtilityTools;
using System;
using System.Data;
using System.Threading;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_Organization : SyncJob
    {
        public DataTable _dtOrganization;

        public SyncJob_Organization(string name) : base(name)
        {
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
            IConnectable sisConnection = DbConnectionFactory.GetSisConnection();
            try
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["sisread_start"]);
                _dtOrganization = new DataTableFactory().Dt_Sis_Organization;
                _dtOrganization = sisConnection.ReadIntoDataTable(SyncScripts.ReadSis_Organization_SQL, _dtOrganization);
            }
            catch (Exception e)
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["sisread_error"]);
                SyncLogger.LogError(e, this);
            }
            finally
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["sisread_end"]);
                sisConnection.CloseConnection();
            }
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
                new Thread(syncThread => SyncTask.Execute_Organization_ParentRecordSync(this, row, parentLatch))
                    .Start();
            }
            parentLatch.Wait();
        }
    }
}