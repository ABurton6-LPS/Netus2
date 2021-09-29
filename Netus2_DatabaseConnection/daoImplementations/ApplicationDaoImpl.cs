using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class ApplicationDaoImpl : IApplicationDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();
        public int? _taskId = null;

        public void SetTaskId(int taskId)
        {
            _taskId = taskId;
        }

        public int? GetTaskId()
        {
            return _taskId;
        }

        public void Delete(Application appliction, IConnectable connection)
        {
            DeleteJctPersonApp(appliction, connection);

            DataRow row = daoObjectMapper.MapApp(appliction);

            StringBuilder sql = new StringBuilder("DELETE FROM app WHERE 1=1 ");
            sql.Append("AND app_id " + (row["app_id"] != DBNull.Value ? "= " + row["app_id"] + " " : "IS NULL "));
            sql.Append("AND name " + (row["name"] != DBNull.Value ? "LIKE '" + row["name"] + "' " : "IS NULL "));
            sql.Append("AND provider_id " + (row["provider_id"] != DBNull.Value ? "= " + row["provider_id"] + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void DeleteJctPersonApp(Application application, IConnectable connection)
        {
            IJctPersonAppDao jctPersonAppDaoImpl = DaoImplFactory.GetJctPersonAppDaoImpl();
            List<DataRow> foundDataRows = jctPersonAppDaoImpl.Read_WithAppId(application.Id, connection);

            foreach (DataRow foundDataRow in foundDataRows)
            {
                jctPersonAppDaoImpl.Delete((int)foundDataRow["person_id"], (int)foundDataRow["app_id"], connection);
            }
        }

        public Application Read_UsingAppId(int appId, IConnectable connection)
        {
            string sql = "SELECT * FROM app WHERE app_id = " + appId;

            List<Application> results = Read(sql, connection);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public List<Application> Read_UsingProviderId(int providerId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM app WHERE provider_id = " + providerId);

            return Read(sql.ToString(), connection);
        }

        public List<Application> Read(Application application, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapApp(application);

            StringBuilder sql = new StringBuilder("SELECT * FROM app WHERE 1=1 ");
            if (row["app_id"] != DBNull.Value)
                sql.Append("AND app_id = " + row["app_id"] + " ");
            else
            {
                if (row["name"] != DBNull.Value)
                    sql.Append("AND name = '" + row["name"] + "' ");
                if (row["provider_id"] != DBNull.Value)
                    sql.Append("AND provider_id = " + row["provider_id"] + " ");
            }

            return Read(sql.ToString(), connection);
        }

        private List<Application> Read(string sql, IConnectable connection)
        {
            DataTable dtApplication = DataTableFactory.CreateDataTable_Netus2_Application();
            dtApplication = connection.ReadIntoDataTable(sql, dtApplication);

            List<Application> results = new List<Application>();
            foreach (DataRow row in dtApplication.Rows)
            {
                Provider foundProvider = Read_Provider((int)row["provider_id"], connection);
                results.Add(daoObjectMapper.MapApp(row, foundProvider));
            }

            return results;
        }

        private Provider Read_Provider(int providerId, IConnectable connection)
        {
            IProviderDao providerDaoImpl = DaoImplFactory.GetProviderDaoImpl();
            return providerDaoImpl.Read_WithProviderId(providerId, connection);
        }

        public void Update(Application application, IConnectable connection)
        {
            List<Application> foundApplications = Read(application, connection);
            if (foundApplications.Count == 0)
                Write(application, connection);
            else if (foundApplications.Count == 1)
            {
                application.Id = foundApplications[0].Id;
                UpdateInternals(application, connection);
            }
            else
                throw new Exception(foundApplications.Count + " Applications found matching the description of:\n" +
                    application.ToString());
        }

        private void UpdateInternals(Application application, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapApp(application);

            StringBuilder sql = new StringBuilder("UPDATE app SET ");
            sql.Append("name = " + (row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, "));
            sql.Append("provider_id = " + (row["provider_id"] != DBNull.Value ? row["provider_id"] + ", " : "NULL, "));
            sql.Append("changed = dbo.CURRENT_DATETIME(), ");
            sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
            sql.Append("WHERE app_id = " + row["app_id"]);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public Application Write(Application application, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapApp(application);

            StringBuilder sql = new StringBuilder("INSERT INTO app (name, provider_id, created, created_by) VALUES (");
            sql.Append(row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, ");
            sql.Append(row["provider_id"] != DBNull.Value ? row["provider_id"] + ", " : "NULL, ");
            sql.Append("dbo.CURRENT_DATETIME(), ");
            sql.Append((_taskId != null ? _taskId.ToString() : "'Netus2'") + ")");

            row["app_id"] = connection.InsertNewRecord(sql.ToString());

            Provider foundProvider = Read_Provider((int)row["provider_id"], connection);
            return daoObjectMapper.MapApp(row, foundProvider);
        }
    }
}
