using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.SyncProcesses.SyncTasks.AcademicSessionTasks;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

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
            IDataReader reader = null;
            try
            {
                reader = _sisConnection.GetReader(SyncScripts.ReadSiS_AcademicSession_SQL);
                _dtAcademicSession.Load(reader);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                SyncLogger.LogError(e, this, _netus2Connection);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void RunJobTasks()
        {
            foreach (DataRow row in _dtAcademicSession.Rows)
            {
                new SyncTask_AcademicSessionChildRecords(
                "SyncTask_AcademicSessionChildRecords", this)
                    .Execute(row);

                new SyncTask_AcademicSessionParentRecords(
                "SyncTask_AcademicSessionParentRecords", this)
                    .Execute(row);
            }
        }
    }
}
