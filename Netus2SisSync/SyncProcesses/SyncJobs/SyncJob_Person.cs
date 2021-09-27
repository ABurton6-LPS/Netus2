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
        public DataTable _dtPerson;

        public SyncJob_Person() : base("SyncJob_Person")
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
                _dtPerson = DataTableFactory.Dt_Sis_Person;
                _dtPerson = sisConnection.ReadIntoDataTable(SyncScripts.ReadSis_Person_SQL, _dtPerson);
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
            SyncLogger.LogStatus(this, Enum_Sync_Status.values["tasks_started"]);

            int numberOfThreadsProcessed = 0;
            while (numberOfThreadsProcessed < _dtPerson.Rows.Count)
            {
                CountDownLatch latch = new CountDownLatch(_maxThreadsPerBatch);
                for (int i = 0; i < _maxThreadsPerBatch; i++)
                {
                    if (numberOfThreadsProcessed < _dtPerson.Rows.Count)
                    {
                        DataRow row = _dtPerson.Rows[numberOfThreadsProcessed];

                        new Thread(syncThread => SyncTask.Execute_Person_RecordSync(
                            this, row, latch)).Start();

                        numberOfThreadsProcessed++;
                    }
                    else
                    {
                        latch.Signal();
                    }
                }
                latch.Wait();
            }
        }
    }
}