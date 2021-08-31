using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.SyncProcesses.SyncTasks.AddressTasks;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Netus2SisSync.SyncProcesses.SyncJobs
{
    public class SyncJob_Address : SyncJob
    {
        IConnectable _sisConnection;
        IConnectable _netus2Connection;
        public DataTable _dtAddress;

        public SyncJob_Address(string name, IConnectable sisConnection, IConnectable netus2Connection)
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
            _dtAddress = new DataTableFactory().Dt_Sis_Address;
            IDataReader reader = null;
            try
            {
                reader = _sisConnection.GetReader(SyncScripts.ReadSis_Address_SQL);
                while (reader.Read())
                {
                    DataRow myDataRow = _dtAddress.NewRow();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "":
                                if (value != DBNull.Value && value != null)
                                    myDataRow[""] = (string)value;
                                else
                                    myDataRow[""] = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in SIS lookup for Unique Identifier: " + columnName);
                        }
                    }
                    _dtAddress.Rows.Add(myDataRow);
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
                _sisConnection.CloseConnection();
            }
        }

        private void RunJobTasks()
        {
            foreach(DataRow row in _dtAddress.Rows)
            {
                new SyncTask_Address(
                "SyncTask_Address", this)
                    .Execute(row);
            }
        }
    }
}
