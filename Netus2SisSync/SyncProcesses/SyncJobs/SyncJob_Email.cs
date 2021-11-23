using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_Email : SyncJob
    {
        public DataTable _dtEmail;
        public DataTable _dtJctPersonEmail;

        public SyncJob_Email() : base("SyncJob_Email")
        {
            SyncLogger.LogNewJob(this);
        }

        public void Start()
        {
            try
            {
                ReadFromSis();
                TempTableFactory.Create_JctPersonEmail();
                RunJobTasks();
                UnlinkDiscardedJctPersonEmailRecords();
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"]);
            }
            catch (Exception e)
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["error"]);
                SyncLogger.LogError(e, this);
            }
            finally
            {
                TempTableFactory.Drop_JctPersonEmail();
            }
        }

        public void ReadFromSis()
        {
            IConnectable sisConnection = DbConnectionFactory.GetSisConnection();
            try
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["sisread_start"]);

                ReadFromSisEmail(sisConnection);

                ReadFromSisJctPersonEmail(sisConnection);

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

        public void ReadFromSisEmail(IConnectable sisConnection)
        {
            _dtEmail = sisConnection.ReadIntoDataTable(
                    SyncScripts.ReadSis_Email_SQL,
                    DataTableFactory.CreateDataTable_Sis_Email());
        }

        public void ReadFromSisJctPersonEmail(IConnectable sisConnection)
        {
            _dtJctPersonEmail = sisConnection.ReadIntoDataTable(
                    SyncScripts.ReadSis_JctPersonEmail_SQL,
                    DataTableFactory.CreateDataTable_Sis_JctPersonEmail());
        }

        private void RunJobTasks()
        {
            SyncLogger.LogStatus(this, Enum_Sync_Status.values["tasks_started"]);

            int numberOfThreadsProcessed = 0;
            while (numberOfThreadsProcessed < _dtEmail.Rows.Count)
            {
                CountDownLatch latch = new CountDownLatch(_maxThreadsPerBatch);
                for (int i = 0; i < _maxThreadsPerBatch; i++)
                {
                    if (numberOfThreadsProcessed < _dtEmail.Rows.Count)
                    {
                        DataRow row = _dtEmail.Rows[numberOfThreadsProcessed];

                        new Thread(syncThread => SyncTask.Execute_Email_RecordSync(
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
            while (numberOfThreadsProcessed < _dtJctPersonEmail.Rows.Count)
            {
                CountDownLatch latch = new CountDownLatch(_maxThreadsPerBatch);
                for (int i = 0; i < _maxThreadsPerBatch; i++)
                {
                    if (numberOfThreadsProcessed < _dtJctPersonEmail.Rows.Count)
                    {
                        DataRow row = _dtJctPersonEmail.Rows[numberOfThreadsProcessed];

                        new Thread(syncThread => SyncTask.Execute_JctPersonEmail_RecordSync(
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

        private void UnlinkDiscardedJctPersonEmailRecords()
        {
            IConnectable netus2Connection = DbConnectionFactory.GetNetus2Connection();
            IJctPersonEmailDao jctPersonEmailDaoImpl = DaoImplFactory.GetJctPersonEmailDaoImpl();

            List<DataRow> recordsToBeDeleted = jctPersonEmailDaoImpl.Read_AllEmailIsNotInTempTable(netus2Connection);
            foreach (DataRow row in recordsToBeDeleted)
                jctPersonEmailDaoImpl.Delete((int)row["person_id"], (int)row["email_id"], netus2Connection);
        }
    }
}