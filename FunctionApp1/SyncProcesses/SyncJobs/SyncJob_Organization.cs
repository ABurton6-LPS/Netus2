using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.SyncProcesses.SyncTasks.OrganizationTasks;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

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
            IDataReader reader = null;
            try
            {
                reader = _sisConnection.GetReader(SyncScripts.ReadSis_Organization_SQL);
                _dtOrganization.Load(reader);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                SyncLogger.LogError(e, this, _netus2Connection);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void RunJobTasks()
        {
            foreach (DataRow row in _dtOrganization.Rows)
            {
                SyncTask_OrganizationChildRecords syncTask = new SyncTask_OrganizationChildRecords(
                    "SyncTask_OrganizationChildRecords", this);
                syncTask.Execute(row);

                SyncTask_OrganizationParentRecords syncTaskParent = new SyncTask_OrganizationParentRecords(
                    "SyncTask_OrganizationParentRecords", this);
                syncTaskParent.Execute(row);
            }
        }
    }
}
