using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
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
            _dtAcademicSession = DataTableFactory.CreateDataTable("AcademicSession");
            IDataReader reader = null;
            try
            {
                reader = _sisConnection.GetReader(SyncScripts.ReadSiS_AcademicSession_SQL);
                while (reader.Read())
                {
                    DataRow myDataRow = _dtAcademicSession.NewRow();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "school_code":
                                myDataRow["school_code"] = (string)value;
                                break;
                            case "term_code":
                                myDataRow["term_code"] = (string)value;
                                break;
                            case "school_year":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["school_year"] = Convert.ToInt32(value);
                                else
                                    myDataRow["school_year"] = -1;
                                break;
                            case "name":
                                myDataRow["name"] = (string)value;
                                break;
                            case "enum_session_id":
                                myDataRow["enum_session_id"] = (string)value;
                                break;
                            case "start_date":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["start_date"] = (DateTime)value;
                                else
                                    myDataRow["start_date"] = new DateTime();
                                break;
                            case "end_date":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["end_date"] = (DateTime)value;
                                else
                                    myDataRow["end_date"] = new DateTime();
                                break;
                            case "parent_session_code":
                                if (value != DBNull.Value)
                                    myDataRow["parent_session_code"] = (string)value;
                                break;
                            default:
                                throw new Exception("Unexpected column found in SIS lookup for Academic Session: " + columnName);
                        }
                    }
                    _dtAcademicSession.Rows.Add(myDataRow);
                }
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
