using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2SisSync.SyncProcesses.SyncTasks.OrganizationTasks;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_Organization : SyncJob
    {
        IConnectable _sisConnection;
        IConnectable _netus2Connection;
        public DataTable _dtOrganization;

        public SyncJob_Organization(string name, DateTime timestamp, IConnectable sisConnection, IConnectable netus2Connection)
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
            _dtOrganization = DataTableFactory.CreateDataTable("Organization");
            IDataReader reader = null;
            try
            {
                reader = _sisConnection.GetReader(SyncScripts.ReadSis_Organization_SQL);
                while (reader.Read())
                {
                    DataRow myDataRow = _dtOrganization.NewRow();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "name":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["name"] = (string)value;
                                else
                                    myDataRow["name"] = null;
                                break;
                            case "enum_organization_id":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["enum_organization_id"] = ((string)value).ToLower();
                                else
                                    myDataRow["enum_organization_id"] = null;
                                break;
                            case "identifier":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["identifier"] = (string)value;
                                else
                                    myDataRow["identifier"] = null;
                                break;
                            case "building_code":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["building_code"] = (string)value;
                                else
                                    myDataRow["building_code"] = null;
                                break;
                            case "organization_parent_id":
                                if (value != DBNull.Value && value != null)
                                    myDataRow["organization_parent_id"] = (string)value;
                                else
                                    myDataRow["organization_parent_id"] = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in SIS lookup for Organization: " + columnName);
                        }
                    }
                    _dtOrganization.Rows.Add(myDataRow);
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
            TaskExecutor.ExecuteTask(new SyncTask_OrganizationChildRecords(
                "SyncTask_OrganizationChildRecords", DateTime.Now, this),
                _dtOrganization);

            TaskExecutor.ExecuteTask(new SyncTask_OrganizationParentRecords(
                "SyncTask_OrganizationParentRecords", DateTime.Now, this),
                _dtOrganization);
        }
    }
}
