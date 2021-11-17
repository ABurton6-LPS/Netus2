using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            if (appliction.Id <= 0)
                throw new Exception("Cannot delete an application which doesn't have a database-assigned ID.\n" + appliction.ToString());

            DeleteJctPersonApp(appliction, connection);

            DataRow row = daoObjectMapper.MapApp(appliction);

            string sql = "DELETE FROM application WHERE " +
                "application_id = @application_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@application_id", appliction.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void DeleteJctPersonApp(Application application, IConnectable connection)
        {
            IJctPersonAppDao jctPersonAppDaoImpl = DaoImplFactory.GetJctPersonAppDaoImpl();
            List<DataRow> foundDataRows = jctPersonAppDaoImpl.Read_AllWithAppId(application.Id, connection);

            foreach (DataRow foundDataRow in foundDataRows)
            {
                jctPersonAppDaoImpl.Delete((int)foundDataRow["person_id"], (int)foundDataRow["application_id"], connection);
            }
        }

        public Application Read_UsingAppId(int appId, IConnectable connection)
        {
            string sql = "SELECT * FROM application WHERE application_id = @application_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@application_id", appId));

            List<Application> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching appId: " + appId);
        }

        public List<Application> Read_UsingProviderId(int providerId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM application WHERE provider_id = @provider_id");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@provider_id", providerId));

            return Read(sql.ToString(), connection, parameters);
        }

        public List<Application> Read(Application application, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapApp(application);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM application WHERE 1=1 ");
            if (row["application_id"] != DBNull.Value)
            {
                sql.Append("AND application_id = @application_id");
                parameters.Add(new SqlParameter("@application_id", row["application_id"]));
            }                
            else
            {
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("AND name = @name ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }

                if (row["provider_id"] != DBNull.Value)
                {
                    sql.Append("AND provider_id = @provider_id");
                    parameters.Add(new SqlParameter("@provider_id", row["provider_id"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<Application> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtApplication = DataTableFactory.CreateDataTable_Netus2_Application();
            dtApplication = connection.ReadIntoDataTable(sql, dtApplication, parameters);

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
            return providerDaoImpl.Read_UsingProviderId(providerId, connection);
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
            
            if(row["application_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE application SET ");
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("name = @name, ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }
                else
                    sql.Append("name = NULL, ");

                if (row["provider_id"] != DBNull.Value)
                {
                    sql.Append("provider_id = @provider_id, ");
                    parameters.Add(new SqlParameter("@provider_id", row["provider_id"]));
                }
                else
                    sql.Append("provider_id = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE application_id = @application_id");
                parameters.Add(new SqlParameter("@application_id", row["application_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);
            }
            else
                throw new Exception("The following Application needs to be inserted into the database, before it can be updated.\n" + application.ToString());
        }

        public Application Write(Application application, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapApp(application);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["name"] != DBNull.Value)
            {
                sqlValues.Append("@name, ");
                parameters.Add(new SqlParameter("@name", row["name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["provider_id"] != DBNull.Value)
            {
                sqlValues.Append("@provider_id, ");
                parameters.Add(new SqlParameter("@provider_id", row["provider_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql = "INSERT INTO application " +
                "(name, provider_id, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["application_id"] = connection.InsertNewRecord(sql, parameters);

            Provider foundProvider = Read_Provider((int)row["provider_id"], connection);
            Application result = daoObjectMapper.MapApp(row, foundProvider);

            return result;
        }
    }
}
