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
    public class SyncJob_PhoneNumber : SyncJob
    {
        public DataTable _dtPhoneNumber;
        public DataTable _dtJctPersonPhoneNumber;

        public SyncJob_PhoneNumber() : base("SyncJob_PhoneNumber")
        {
            SyncLogger.LogNewJob(this);
        }

        public void Start()
        {
            try
            {
                ReadFromSis();
                TempTableFactory.Create_JctPersonPhoneNumber();
                RunJobTasks();
                UnlinkDiscardedJctPersonPhoneNumberRecords();
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"]);
            }
            catch (Exception e)
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["error"]);
                SyncLogger.LogError(e, this);
            }
            finally
            {
                TempTableFactory.Drop_JctPersonPhoneNumber();
            }
        }

        public void ReadFromSis()
        {
            IConnectable sisConnection = DbConnectionFactory.GetSisConnection();
            try
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["sisread_start"]);

                ReadFromSisPhoneNumber(sisConnection);

                ReadFromSisJctPersonPhoneNumber(sisConnection);

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
                _totalRecordsToProcess = _dtPhoneNumber.Rows.Count + _dtJctPersonPhoneNumber.Rows.Count;
            }
        }

        public void ReadFromSisPhoneNumber(IConnectable sisConnection)
        {
            _dtPhoneNumber = sisConnection.ReadIntoDataTable(
                    SyncScripts.ReadSis_PhoneNumber_SQL,
                    DataTableFactory.CreateDataTable_Sis_PhoneNumber());
        }

        public void ReadFromSisJctPersonPhoneNumber(IConnectable sisConnection)
        {
            _dtJctPersonPhoneNumber = sisConnection.ReadIntoDataTable(
                    SyncScripts.ReadSis_JctPersonPhoneNumber_SQL,
                    DataTableFactory.CreateDataTable_Sis_JctPersonPhoneNumber());
        }

        private void RunJobTasks()
        {
            SyncLogger.LogStatus(this, Enum_Sync_Status.values["tasks_started"]);
            SyncLogger.LogTotalRecordsToProcess(this, _totalRecordsToProcess);

            int numberOfThreadsProcessed = 0;
            while (numberOfThreadsProcessed < _dtPhoneNumber.Rows.Count)
            {
                CountDownLatch latch = new CountDownLatch(_maxThreadsPerBatch);
                for (int i = 0; i < _maxThreadsPerBatch; i++)
                {
                    if (numberOfThreadsProcessed < _dtPhoneNumber.Rows.Count)
                    {
                        DataRow row = _dtPhoneNumber.Rows[numberOfThreadsProcessed];

                        new Thread(syncThread => SyncTask.Execute_PhoneNumber_RecordSync(
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
            while (numberOfThreadsProcessed < _dtJctPersonPhoneNumber.Rows.Count)
            {
                CountDownLatch latch = new CountDownLatch(_maxThreadsPerBatch);
                for (int i = 0; i < _maxThreadsPerBatch; i++)
                {
                    if (numberOfThreadsProcessed < _dtJctPersonPhoneNumber.Rows.Count)
                    {
                        DataRow row = _dtJctPersonPhoneNumber.Rows[numberOfThreadsProcessed];

                        new Thread(syncThread => SyncTask.Execute_JctPersonPhoneNumber_RecordSync(
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

        private void UnlinkDiscardedJctPersonPhoneNumberRecords()
        {
            IConnectable netus2Connection = DbConnectionFactory.GetNetus2Connection();
            IJctPersonPhoneNumberDao jctPersonPhoneNumberDaoImpl = DaoImplFactory.GetJctPersonPhoneNumberDaoImpl();

            List<DataRow> recordsToBeDeleted = jctPersonPhoneNumberDaoImpl.Read_AllPhoneNumberIsNotInTempTable(netus2Connection);
            foreach (DataRow row in recordsToBeDeleted)
                jctPersonPhoneNumberDaoImpl.Delete((int)row["person_id"], (int)row["phone_number_id"], netus2Connection);
        }
    }
}