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
    public class ProviderDaoImpl : IProviderDao
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

        public void Delete(Provider provider, IConnectable connection)
        {
            if(provider.Id <= 0)
                throw new Exception("Cannot delete a provider which doesn't have a database-assigned ID.\n" + provider.ToString());

            provider = UnlinkChildren(provider, connection);
            DeleteApplications(provider, connection);

            string sql = "DELETE FROM provider WHERE " +
                "provider_id = @provider_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@provider_id", provider.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void DeleteApplications(Provider provider, IConnectable connection)
        {
            IApplicationDao appDaoImpl = DaoImplFactory.GetApplicationDaoImpl();
            List<Application> foundApps = appDaoImpl.Read_UsingProviderId(provider.Id, connection);

            foreach (Application foundApp in foundApps)
            {
                appDaoImpl.Delete(foundApp, connection);
            }
        }

        private Provider UnlinkChildren(Provider provider, IConnectable connection)
        {
            List<Provider> childrenToRemove = new List<Provider>();
            foreach (Provider child in provider.Children)
            {
                Update(child, connection);
                childrenToRemove.Add(child);
            }
            foreach (Provider child in childrenToRemove)
            {
                provider.Children.Remove(child);
            }

            return provider;
        }

        public Provider Read_UsingProviderId(int providerId, IConnectable connection)
        {
            string sql = "SELECT * FROM provider WHERE provider_id = @provider_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@provider_id", providerId));

            List<Provider> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public Provider Read_AllWithAppId(int appId, IConnectable connection)
        {
            string sql = "SELECT * FROM provider WHERE provider_id IN (" +
            "SELECT provider_id FROM app WHERE app_id = @app_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@app_id", appId));

            List<Provider> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public List<Provider> Read_AllChildrenWithParentId(int parentId, IConnectable connection)
        {
            string sql = "SELECT * FROM provider WHERE parent_provider_id = @parent_provider_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@parent_provider_id", parentId));

            return Read(sql, connection, parameters);
        }

        public List<Provider> Read(Provider provider, IConnectable connection)
        {
            return Read(provider, -1, connection);
        }

        public List<Provider> Read(Provider provider, int parentId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapProvider(provider, parentId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM provider WHERE 1=1 ");
            if (row["provider_id"] != DBNull.Value)
            {
                sql.Append("AND provider_id = @provider_id");
                parameters.Add(new SqlParameter("@provider_id", row["provider_id"]));
            }
            else
            {
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("AND name = @name ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }

                if (row["url_standard_access"] != DBNull.Value)
                {
                    sql.Append("AND url_standard_access = @url_standard_access ");
                    parameters.Add(new SqlParameter("@url_standard_access", row["url_standard_access"]));
                }

                if (row["url_admin_access"] != DBNull.Value)
                {
                    sql.Append("AND url_admin_access = @url_admin_access ");
                    parameters.Add(new SqlParameter("@url_admin_access", row["url_admin_access"]));
                }

                if (row["populated_by"] != DBNull.Value)
                {
                    sql.Append("AND populated_by = @populated_by ");
                    parameters.Add(new SqlParameter("@populated_by", row["populated_by"]));
                }

                if (row["parent_provider_id"] != DBNull.Value)
                {
                    sql.Append("AND parent_provider_id = @parent_provider_id ");
                    parameters.Add(new SqlParameter("@parent_provider_id", row["parent_provider_id"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<Provider> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtProvider = DataTableFactory.CreateDataTable_Netus2_Provider();
            dtProvider = connection.ReadIntoDataTable(sql, dtProvider, parameters);

            List<Provider> results = new List<Provider>();
            foreach (DataRow row in dtProvider.Rows)
                results.Add(daoObjectMapper.MapProvider(row));

            foreach (Provider result in results)
                result.Children.AddRange(Read_AllChildrenWithParentId(result.Id, connection));

            return results;
        }

        public void Update(Provider provider, IConnectable connection)
        {
            Update(provider, -1, connection);
        }

        private void Update(Provider provider, int parentProviderId, IConnectable connection)
        {
            List<Provider> foundProviders = Read(provider, connection);
            if (foundProviders.Count == 0)
                Write(provider, parentProviderId, connection);
            else if (foundProviders.Count == 1)
            {
                provider.Id = foundProviders[0].Id;
                UpdateInternals(provider, parentProviderId, connection);
            }
            else if (foundProviders.Count > 1)
                throw new Exception(foundProviders.Count + " Providers found matching the description of:\n" +
                    provider.ToString());
        }

        private void UpdateInternals(Provider provider, int parentProviderId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapProvider(provider, parentProviderId);

            if (row["provider_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE provider SET ");
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("name = @name, ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }
                else
                    sql.Append("name = NULL, ");

                if (row["url_standard_access"] != DBNull.Value)
                {
                    sql.Append("url_standard_access = @url_standard_access, ");
                    parameters.Add(new SqlParameter("@url_standard_access", row["url_standard_access"]));
                }
                else
                    sql.Append("url_standard_access = NULL, ");

                if (row["url_admin_access"] != DBNull.Value)
                {
                    sql.Append("url_admin_access = @url_admin_access, ");
                    parameters.Add(new SqlParameter("@url_admin_access", row["url_admin_access"]));
                }
                else
                    sql.Append("url_admin_access = NULL, ");

                if (row["populated_by"] != DBNull.Value)
                {
                    sql.Append("populated_by = @populated_by, ");
                    parameters.Add(new SqlParameter("@populated_by", row["populated_by"]));
                }
                else
                    sql.Append("populated_by = NULL, ");

                if (row["parent_provider_id"] != DBNull.Value)
                {
                    sql.Append("parent_provider_id = @parent_provider_id, ");
                    parameters.Add(new SqlParameter("@parent_provider_id", row["parent_provider_id"]));
                }
                else
                    sql.Append("parent_provider_id = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE provider_id = @provider_id");
                parameters.Add(new SqlParameter("@provider_id", row["provider_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);

                UpdateChildren(provider.Children, provider.Id, connection);
            }
            else
                throw new Exception("The following Provider needs to be inserted into the database, before it can be updated.\n" + provider.ToString());
        }

        public Provider Write(Provider provider, IConnectable connection)
        {
            return Write(provider, -1, connection);
        }

        public Provider Write(Provider provider, int parentProviderId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapProvider(provider, parentProviderId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["name"] != DBNull.Value)
            {
                sqlValues.Append("@name, ");
                parameters.Add(new SqlParameter("@name", row["name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["url_standard_access"] != DBNull.Value)
            {
                sqlValues.Append("@url_standard_access, ");
                parameters.Add(new SqlParameter("@url_standard_access", row["url_standard_access"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["url_admin_access"] != DBNull.Value)
            {
                sqlValues.Append("@url_admin_access, ");
                parameters.Add(new SqlParameter("@url_admin_access", row["url_admin_access"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["populated_by"] != DBNull.Value)
            {
                sqlValues.Append("@populated_by, ");
                parameters.Add(new SqlParameter("@populated_by", row["populated_by"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["parent_provider_id"] != DBNull.Value)
            {
                sqlValues.Append("@parent_provider_id, ");
                parameters.Add(new SqlParameter("@parent_provider_id", row["parent_provider_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql = 
                "INSERT INTO provider " +
                "(name, url_standard_access, url_admin_access, populated_by, " +
                "parent_provider_id, created, created_by)" +
                "VALUES (" + sqlValues.ToString() + ")";

            row["provider_id"] = connection.InsertNewRecord(sql, parameters);

            Provider result = daoObjectMapper.MapProvider(row);

            result.Children = UpdateChildren(provider.Children, result.Id, connection);

            return result;
        }

        private List<Provider> UpdateChildren(List<Provider> children, int parentId, IConnectable connection)
        {
            List<Provider> updatedChildren = new List<Provider>();
            List<Provider> foundChildren = Read_AllChildrenWithParentId(parentId, connection);

            foreach (Provider child in children)
            {
                Update(child, parentId, connection);
                updatedChildren.AddRange(Read(child, parentId, connection));
            }

            foreach (Provider foundChild in foundChildren)
            {
                if (children.Find(x => (x.Id == foundChild.Id)) == null)
                    Update(foundChild, connection);
            }

            return updatedChildren;
        }
    }
}
