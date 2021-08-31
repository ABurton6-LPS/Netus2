using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.SyncProcesses.SyncTasks.PersonTasks;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_Person : SyncJob
    {
        IConnectable _sisConnection;
        IConnectable _netus2Connection;
        public DataTable _dtPerson;

        public SyncJob_Person(string name, IConnectable sisConnection, IConnectable netus2Connection)
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
            _dtPerson = new DataTableFactory().Dt_Sis_Person;
            IDataReader reader = null;
            try
            {
                reader = _sisConnection.GetReader(SyncScripts.ReadSis_Person_SQL);
                _dtPerson.Load(reader);
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
            foreach(DataRow row in _dtPerson.Rows)
            {
                new SyncTask_Person(
                "SyncTask_Person", this)
                    .Execute(row);
            }
        }
    }
}
