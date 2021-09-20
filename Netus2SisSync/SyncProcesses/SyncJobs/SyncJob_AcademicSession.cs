using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.UtilityTools;
using System.Data;
using System.Threading;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_AcademicSession : SyncJob
    {
        IConnectable _sisConnection;
        public DataTable _dtAcademicSession;

        public SyncJob_AcademicSession(string name, IConnectable sisConnection) 
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
            _dtAcademicSession = new DataTableFactory().Dt_Sis_AcademicSession;
            _dtAcademicSession = _sisConnection.ReadIntoDataTable(SyncScripts.ReadSiS_AcademicSession_SQL, _dtAcademicSession);
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