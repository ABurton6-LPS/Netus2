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
    public class SyncJob_AcademicSession : SyncJob
    {
        public DataTable _dtAcademicSession;

        public SyncJob_AcademicSession() : base("SyncJob_AcademicSession")
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
                _dtAcademicSession = new DataTableFactory().Dt_Sis_AcademicSession;
                _dtAcademicSession = sisConnection.ReadIntoDataTable(SyncScripts.ReadSiS_AcademicSession_SQL, _dtAcademicSession);
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
            while (numberOfThreadsProcessed < _dtAcademicSession.Rows.Count)
            {
                CountDownLatch latch = new CountDownLatch(_maxThreadsPerBatch);
                for (int i = 0; i < _maxThreadsPerBatch; i++)
                {
                    if (numberOfThreadsProcessed < _dtAcademicSession.Rows.Count)
                    {
                        DataRow row = _dtAcademicSession.Rows[numberOfThreadsProcessed];

                        new Thread(syncThread => SyncTask.Execute_AcademicSession_ChildRecordSync(
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


            numberOfThreadsProcessed = 0;
            while (numberOfThreadsProcessed < _dtAcademicSession.Rows.Count)
            {
                CountDownLatch latch = new CountDownLatch(_maxThreadsPerBatch);
                for (int i = 0; i < _maxThreadsPerBatch; i++)
                {
                    if (numberOfThreadsProcessed < _dtAcademicSession.Rows.Count)
                    {
                        DataRow row = _dtAcademicSession.Rows[numberOfThreadsProcessed];

                        new Thread(syncThread => SyncTask.Execute_AcademicSession_ParentRecordSync(
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