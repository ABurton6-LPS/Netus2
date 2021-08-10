using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2SisSync.SyncProcesses.SyncTasks.PersonTasks;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_Person : SyncJob
    {
        IConnectable _sisConnection;
        IConnectable _netus2Connection;
        public DataTable _dtPerson;

        public SyncJob_Person(string name, DateTime timestamp, IConnectable sisConnection, IConnectable netus2Connection)
            : base(name, timestamp)
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
            _dtPerson = DataTableFactory.CreateDataTable("Person");
            IDataReader reader = null;
            try
            {
                reader = _sisConnection.GetReader(SyncScripts.ReadSis_Person_SQL);
                while (reader.Read())
                {
                    DataRow myDataRow = _dtPerson.NewRow();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "person_type":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["person_type"] = (string)value;
                                else
                                    myDataRow["person_type"] = null;
                                break;
                            case "SIS_ID":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["SIS_ID"] = (string)value;
                                else
                                    myDataRow["SIS_ID"] = null;
                                break;
                            case "first_name":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["first_name"] = (string)value;
                                else
                                    myDataRow["first_name"] = null;
                                break;
                            case "middle_name":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["middle_name"] = (string)value;
                                else
                                    myDataRow["middle_name"] = null;
                                break;
                            case "last_name":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["last_name"] = (string)value;
                                else
                                    myDataRow["last_name"] = null;
                                break;
                            case "birth_date":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["birth_date"] = (DateTime)value;
                                else
                                    myDataRow["birth_date"] = new DateTime();
                                break;
                            case "enum_gender_id":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["enum_gender_id"] = (string)value;
                                else
                                    myDataRow["enum_gender_id"] = null;
                                break;
                            case "enum_ethnic_id":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["enum_ethnic_id"] = (string)value;
                                else
                                    myDataRow["enum_ethnic_id"] = null;
                                break;
                            case "enum_residence_status_id":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["enum_residence_status_id"] = (string)value;
                                else
                                    myDataRow["enum_residence_status_id"] = null;
                                break;
                            case "login_name":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["login_name"] = (string)value;
                                else
                                    myDataRow["login_name"] = null;
                                break;
                            case "login_pw":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["login_pw"] = (string)value;
                                else
                                    myDataRow["login_pw"] = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in SIS lookup for Unique Identifier: " + columnName);
                        }
                    }
                    _dtPerson.Rows.Add(myDataRow);
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
            TaskExecutor.ExecuteTask(new SyncTask_Person(
                "SyncTask_Person", DateTime.Now, this),
                _dtPerson);
        }
    }
}
