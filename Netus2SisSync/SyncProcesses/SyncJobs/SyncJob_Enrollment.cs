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
    public class SyncJob_Enrollment : SyncJob
    {
        public DataTable _dtEnrollment;
        public DataTable _dtJctEnrollmentClassEnrolled;

        public SyncJob_Enrollment() : base("SyncJob_Enrollment")
        {
            SyncLogger.LogNewJob(this);
        }

        public void Start()
        {
            try
            {
                ReadFromSis();
                TempTableFactory.Create_JctEnrollmentClassEnrolled();
                RunJobTasks();
                UnlinkDiscardedJctEnrollmentClassEnrolledRecords();
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

                ReadFromSisEnrollment(sisConnection);

                ReadFromSisJctEnrollmentClassEnrolled(sisConnection);

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

        public void ReadFromSisEnrollment(IConnectable sisConnection)
        {
            _dtEnrollment = sisConnection.ReadIntoDataTable(
                    SyncScripts.ReadSis_Enrollment_SQL,
                    DataTableFactory.CreateDataTable_Sis_Enrollment());
        }

        public void ReadFromSisJctEnrollmentClassEnrolled(IConnectable sisConnection)
        {
            _dtJctEnrollmentClassEnrolled = sisConnection.ReadIntoDataTable(
                SyncScripts.ReadSis_JctEnrollmentClassEnrolled_SQL,
                DataTableFactory.CreateDataTable_Sis_JctEnrollmentClassEnrolled());
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

        private void UnlinkDiscardedJctEnrollmentClassEnrolledRecords()
        {
            IConnectable netus2Connection = DbConnectionFactory.GetNetus2Connection();
            IJctEnrollmentClassEnrolledDao jctEnrollmentClassEnrolledDaoImpl = DaoImplFactory.GetJctEnrollmentClassEnrolledDaoImpl();

            List<DataRow> recordsToBeDeleted = jctEnrollmentClassEnrolledDaoImpl.Read_AllClassEnrolledNotInTempTable(netus2Connection);
            foreach (DataRow row in recordsToBeDeleted)
                jctEnrollmentClassEnrolledDaoImpl.Delete((int)row["enrollment_id"], (int)row["class_enrolled_id"], netus2Connection);
        }
    }
}