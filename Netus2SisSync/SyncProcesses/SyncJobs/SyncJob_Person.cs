using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.SyncProcesses.SyncTasks.PersonTasks;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_Person : SyncJob
    {
        IConnectable _sisConnection;
        public DataTable _dtPerson;

        public SyncJob_Person(string name, IConnectable sisConnection)
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
            _dtPerson = new DataTableFactory().Dt_Sis_Person;
            _dtPerson = _sisConnection.ReadIntoDataTable(SyncScripts.ReadSis_Person_SQL, _dtPerson);
        }

        private void RunJobTasks()
        {
            CountDownLatch latch = new CountDownLatch(_dtPerson.Rows.Count);
            foreach (DataRow row in _dtPerson.Rows)
            {
                new Thread(syncThread => SyncTask.Execute_Person_RecordSync(this, row, latch))
                    .Start();
            }
            latch.Wait();
        }
    }
}