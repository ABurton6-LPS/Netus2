using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class ProviderDaoImpl : IProviderDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(Provider provider, IConnectable connection)
        {
            provider = UnlinkChildren(provider, connection);
            DeleteApplications(provider, connection);

            ProviderDao providerDao = daoObjectMapper.MapProvider(provider, -1);

            StringBuilder sql = new StringBuilder("DELETE FROM provider WHERE 1=1 ");
            sql.Append("AND provider_id = " + providerDao.provider_id + " ");
            sql.Append("AND name " + (providerDao.name != null ? "LIKE '" + providerDao.name + "' " : "IS NULL "));
            sql.Append("AND url_standard_access " + (providerDao.url_standard_access != null ? "LIKE '" + providerDao.url_standard_access + "' " : "IS NULL "));
            sql.Append("AND url_admin_access " + (providerDao.url_admin_access != null ? "LIKE '" + providerDao.url_admin_access + "' " : "IS NULL "));
            sql.Append("AND populated_by " + (providerDao.populated_by != null ? "LIKE '" + providerDao.populated_by + "' " : "IS NULL"));

            connection.ExecuteNonQuery(sql.ToString());
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

        public Provider Read_WithProviderId(int providerId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM provider WHERE provider_id = " + providerId);

            List<Provider> results = Read(sql.ToString(), connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
        }

        public Provider Read_WithAppId(int appId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM provider WHERE provider_id IN (");
            sql.Append("SELECT provider_id FROM app WHERE app_id = " + appId);
            sql.Append(")");

            List<Provider> results = Read(sql.ToString(), connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
        }

        public List<Provider> Read(Provider provider, IConnectable connection)
        {
            return Read(provider, -1, connection);
        }

        public List<Provider> Read(Provider provider, int parentId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            if (provider == null)
            {
                sql.Append("SELECT * FROM provider WHERE parent_provider_id = " + parentId);
            }
            else
            {
                ProviderDao providerDao = daoObjectMapper.MapProvider(provider, parentId);

                sql.Append("SELECT * FROM provider WHERE 1=1 ");
                if (providerDao.provider_id != null)
                    sql.Append("AND provider_id = " + providerDao.provider_id + " ");
                else
                {
                    if (providerDao.name != null)
                        sql.Append("AND name LIKE '" + providerDao.name + "' ");
                    if (providerDao.url_standard_access != null)
                        sql.Append("AND url_standard_access LIKE '" + providerDao.url_standard_access + "' ");
                    if (providerDao.url_admin_access != null)
                        sql.Append("AND url_admin_access LIKE '" + providerDao.url_admin_access + "' ");
                    if (providerDao.populated_by != null)
                        sql.Append("AND populated_by LIKE '" + providerDao.populated_by + "' ");
                    if (providerDao.parent_provider_id != null)
                        sql.Append("AND parent_provider_id = " + providerDao.parent_provider_id);
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<Provider> Read(string sql, IConnectable connection)
        {
            List<ProviderDao> foundProvidersDao = new List<ProviderDao>();

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    ProviderDao foundProviderDao = new ProviderDao();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "provider_id":
                                if (value != DBNull.Value && value != null)
                                    foundProviderDao.provider_id = (int)value;
                                else
                                    foundProviderDao.provider_id = null;
                                break;
                            case "name":
                                foundProviderDao.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case "url_standard_access":
                                foundProviderDao.url_standard_access = value != DBNull.Value ? (string)value : null;
                                break;
                            case "url_admin_access":
                                foundProviderDao.url_admin_access = value != DBNull.Value ? (string)value : null;
                                break;
                            case "populated_by":
                                foundProviderDao.populated_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case "parent_provider_id":
                                foundProviderDao.parent_provider_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case "created":
                                if (value != DBNull.Value && value != null)
                                    foundProviderDao.created = (DateTime)value;
                                else
                                    foundProviderDao.created = null;
                                break;
                            case "created_by":
                                foundProviderDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case "changed":
                                if (value != DBNull.Value && value != null)
                                    foundProviderDao.changed = (DateTime)value;
                                else
                                    foundProviderDao.changed = null;
                                break;
                            case "changed_by":
                                foundProviderDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in provider table: " + columnName);
                        }
                    }
                    foundProvidersDao.Add(foundProviderDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<Provider> results = new List<Provider>();
            foreach (ProviderDao foundProviderDao in foundProvidersDao)
            {
                results.Add(daoObjectMapper.MapProvider(foundProviderDao));
            }

            foreach (Provider result in results)
            {
                result.Children.AddRange(Read(null, result.Id, connection));
            }

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
            ProviderDao providerDao = daoObjectMapper.MapProvider(provider, parentProviderId);

            if (providerDao.provider_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE provider SET ");
                sql.Append("name = " + (providerDao.name != null ? "'" + providerDao.name + "', " : "NULL, "));
                sql.Append("url_standard_access = " + (providerDao.url_standard_access != null ? "'" + providerDao.url_standard_access + "', " : "NULL, "));
                sql.Append("url_admin_access = " + (providerDao.url_admin_access != null ? "'" + providerDao.url_admin_access + "', " : "NULL, "));
                sql.Append("populated_by = " + (providerDao.populated_by != null ? "'" + providerDao.populated_by + "', " : "NULL, "));
                sql.Append("parent_provider_id = " + (providerDao.parent_provider_id != null ? providerDao.parent_provider_id + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE provider_id = " + providerDao.provider_id);

                connection.ExecuteNonQuery(sql.ToString());

                UpdateChildren(provider.Children, provider.Id, connection);
            }
            else
            {
                throw new Exception("The following Provider needs to be inserted into the database, before it can be updated.\n" + provider.ToString());
            }
        }

        public Provider Write(Provider provider, IConnectable connection)
        {
            return Write(provider, -1, connection);
        }

        public Provider Write(Provider provider, int parentProviderId, IConnectable connection)
        {
            ProviderDao providerDao = daoObjectMapper.MapProvider(provider, parentProviderId);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(providerDao.name != null ? "'" + providerDao.name + "', " : "NULL, ");
            sqlValues.Append(providerDao.url_standard_access != null ? "'" + providerDao.url_standard_access + "', " : "NULL, ");
            sqlValues.Append(providerDao.url_admin_access != null ? "'" + providerDao.url_admin_access + "', " : "NULL, ");
            sqlValues.Append(providerDao.populated_by != null ? "'" + providerDao.populated_by + "', " : "NULL, ");
            sqlValues.Append(providerDao.parent_provider_id != null ? providerDao.parent_provider_id + ", " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            StringBuilder sql = new StringBuilder("INSERT INTO provider (");
            sql.Append("name, url_standard_access, url_admin_access, populated_by, parent_provider_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(sqlValues.ToString());
            sql.Append(")");

            providerDao.provider_id = connection.InsertNewRecord(sql.ToString());

            Provider result = daoObjectMapper.MapProvider(providerDao);

            result.Children = UpdateChildren(provider.Children, result.Id, connection);

            return result;
        }

        private List<Provider> UpdateChildren(List<Provider> children, int parentId, IConnectable connection)
        {
            List<Provider> updatedChildren = new List<Provider>();
            List<Provider> foundChildren = Read(null, parentId, connection);

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
