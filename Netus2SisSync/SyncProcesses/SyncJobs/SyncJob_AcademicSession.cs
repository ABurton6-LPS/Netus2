using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.SyncProcesses.SyncTasks.AcademicSessionTasks;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_AcademicSession : SyncJob
    {
        IConnectable _sisConnection;
        IConnectable _netus2Connection;
        public DataTable _dtAcademicSession;

        public SyncJob_AcademicSession(string name, IConnectable sisConnection, IConnectable netus2Connection) 
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
            _dtAcademicSession = new DataTableFactory().Dt_Sis_AcademicSession;
            _dtAcademicSession = _sisConnection.ReadIntoDataTable(SyncScripts.ReadSiS_AcademicSession_SQL, _dtAcademicSession).Result;
        }

        private void RunJobTasks()
        {
            CountDownLatch childLatch = new CountDownLatch(_dtAcademicSession.Rows.Count);
            foreach (DataRow row in _dtAcademicSession.Rows)
            {
                new Thread(syncThread => SyncTask.Execute_AcademicSession_ChildRecordSync(this, row, childLatch))
                    .Start();
            }
            childLatch.Wait();


            CountDownLatch parentLatch = new CountDownLatch(_dtAcademicSession.Rows.Count);
            foreach (DataRow row in _dtAcademicSession.Rows)
            {
                new Thread(syncThread => SyncTask.Execute_AcademicSession_ParentRecordSync(this, row, parentLatch))
                    .Start();
            }
            parentLatch.Wait();
        }
    }
}