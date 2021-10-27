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
using System.Text;
using System.Threading;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_Address : SyncJob
    {
        public DataTable _dtAddress;

        public SyncJob_Address() : base("SyncJob_Address")
        {
            SyncLogger.LogNewJob(this);
        }

        public void Start()
        {
            try
            {
                ReadFromSis();
                TempTableFactory.Create_JctPersonAddress();
                RunJobTasks();
                UnlinkDiscardedJctPersonAddressRecords();
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"]);
            }
            catch (Exception e)
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["error"]);
                SyncLogger.LogError(e, this);
            }
            finally
            {
                TempTableFactory.Drop_JctPersonAddress();
            }
        }

        public void ReadFromSis()
        {
            IConnectable sisConnection = DbConnectionFactory.GetSisConnection();
            try
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["sisread_start"]);
                
                _dtAddress = DataTableFactory.CreateDataTable_Sis_Address();
                _dtAddress = sisConnection.ReadIntoDataTable(SyncScripts.ReadSis_Address_SQL, _dtAddress);

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
            while (numberOfThreadsProcessed < _dtAddress.Rows.Count)
            {
                CountDownLatch latch = new CountDownLatch(_maxThreadsPerBatch);
                for (int i = 0; i < _maxThreadsPerBatch; i++)
                {
                    if (numberOfThreadsProcessed < _dtAddress.Rows.Count)
                    {
                        DataRow row = _dtAddress.Rows[numberOfThreadsProcessed];

                        new Thread(syncThread => SyncTask.Execute_Address_RecordSync(
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

        private void UnlinkDiscardedJctPersonAddressRecords()
        {
            IConnectable netus2Connection = DbConnectionFactory.GetNetus2Connection();
            IJctPersonAddressDao jctPersonAddressDaoImpl = new JctPersonAddressDaoImpl();

            List<DataRow> recordsToBeDeleted = jctPersonAddressDaoImpl.Read_AllAddressIsNotInTempTable(netus2Connection);
            foreach (DataRow row in recordsToBeDeleted)
                jctPersonAddressDaoImpl.Delete((int)row["person_id"], (int)row["address_id"], netus2Connection);
        }
    }
}