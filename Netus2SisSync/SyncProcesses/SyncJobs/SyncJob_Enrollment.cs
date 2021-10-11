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
    public class SyncJob_Enrollment : SyncJob
    {
        public DataTable _dtEnrollment;

        public SyncJob_Enrollment() : base("SyncJob_Enrollment")
        {
            SyncLogger.LogNewJob(this);
        }

        public void Start()
        {
            try
            {
                ReadFromSis();
                RunJobTasks();
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"]);
            }
            catch (Exception e)
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["error"]);
                SyncLogger.LogError(e, this);
            }
        }

        public void ReadFromSis()
        {
            IConnectable sisConnection = DbConnectionFactory.GetSisConnection();
            try
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["sisread_start"]);
                
                _dtEnrollment = DataTableFactory.CreateDataTable_Sis_Enrollment();
                _dtEnrollment = sisConnection.ReadIntoDataTable(SyncScripts.ReadSis_Enrollment_SQL, _dtEnrollment);

                SyncLogger.LogStatus(this, Enum_Sync_Status.values["sisread_end"]);
            }
            catch (Exception e)
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["sisread_error"]);
                SyncLogger.LogError(e, this);
            }
            finally
            {
                sisConnection.CloseConnection();
            }
        }

        private void RunJobTasks()
        {
            SyncLogger.LogStatus(this, Enum_Sync_Status.values["tasks_started"]);

            int numberOfThreadsProcessed = 0;
            while (numberOfThreadsProcessed < _dtEnrollment.Rows.Count)
            {
                CountDownLatch latch = new CountDownLatch(_maxThreadsPerBatch);
                for (int i = 0; i < _maxThreadsPerBatch; i++)
                {
                    if (numberOfThreadsProcessed < _dtEnrollment.Rows.Count)
                    {
                        DataRow row = _dtEnrollment.Rows[numberOfThreadsProcessed];

                        new Thread(syncThread => SyncTask.Execute_Enrollment_RecordSync(
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