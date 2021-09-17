﻿using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
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

            DataRow row = daoObjectMapper.MapProvider(provider, -1);

            StringBuilder sql = new StringBuilder("DELETE FROM provider WHERE 1=1 ");
            sql.Append("AND provider_id = " + row["provider_id"] + " ");
            sql.Append("AND name " + (row["name"] != DBNull.Value ? "LIKE '" + row["name"] + "' " : "IS NULL "));
            sql.Append("AND url_standard_access " + (row["url_standard_access"] != DBNull.Value ? "LIKE '" + row["url_standard_access"] + "' " : "IS NULL "));
            sql.Append("AND url_admin_access " + (row["url_admin_access"] != DBNull.Value ? "LIKE '" + row["url_admin_access"] + "' " : "IS NULL "));
            sql.Append("AND populated_by " + (row["populated_by"] != DBNull.Value ? "LIKE '" + row["populated_by"] + "' " : "IS NULL"));

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
                DataRow row = daoObjectMapper.MapProvider(provider, parentId);

                sql.Append("SELECT * FROM provider WHERE 1=1 ");
                if (row["provider_id"] != DBNull.Value)
                    sql.Append("AND provider_id = " + row["provider_id"] + " ");
                else
                {
                    if (row["name"] != DBNull.Value)
                        sql.Append("AND name LIKE '" + row["name"] + "' ");
                    if (row["url_standard_access"] != DBNull.Value)
                        sql.Append("AND url_standard_access LIKE '" + row["url_standard_access"] + "' ");
                    if (row["url_admin_access"] != DBNull.Value)
                        sql.Append("AND url_admin_access LIKE '" + row["url_admin_access"] + "' ");
                    if (row["populated_by"] != DBNull.Value)
                        sql.Append("AND populated_by LIKE '" + row["populated_by"] + "' ");
                    if (row["parent_provider_id"] != DBNull.Value)
                        sql.Append("AND parent_provider_id = " + row["parent_provider_id"]);
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<Provider> Read(string sql, IConnectable connection)
        {
            DataTable dtProvider = new DataTableFactory().Dt_Netus2_Provider;
            dtProvider = connection.ReadIntoDataTable(sql, dtProvider).Result;

            List<Provider> results = new List<Provider>();
            foreach (DataRow row in dtProvider.Rows)
            {
                results.Add(daoObjectMapper.MapProvider(row));
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
            DataRow row = daoObjectMapper.MapProvider(provider, parentProviderId);

            if (row["provider_id"] != DBNull.Value)
            {
                StringBuilder sql = new StringBuilder("UPDATE provider SET ");
                sql.Append("name = " + (row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, "));
                sql.Append("url_standard_access = " + (row["url_standard_access"] != DBNull.Value ? "'" + row["url_standard_access"] + "', " : "NULL, "));
                sql.Append("url_admin_access = " + (row["url_admin_access"] != DBNull.Value ? "'" + row["url_admin_access"] + "', " : "NULL, "));
                sql.Append("populated_by = " + (row["populated_by"] != DBNull.Value ? "'" + row["populated_by"] + "', " : "NULL, "));
                sql.Append("parent_provider_id = " + (row["parent_provider_id"] != DBNull.Value ? row["parent_provider_id"] + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE provider_id = " + row["provider_id"]);

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
            DataRow row = daoObjectMapper.MapProvider(provider, parentProviderId);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, ");
            sqlValues.Append(row["url_standard_access"] != DBNull.Value ? "'" + row["url_standard_access"] + "', " : "NULL, ");
            sqlValues.Append(row["url_admin_access"] != DBNull.Value ? "'" + row["url_admin_access"] + "', " : "NULL, ");
            sqlValues.Append(row["populated_by"] != DBNull.Value ? "'" + row["populated_by"] + "', " : "NULL, ");
            sqlValues.Append(row["parent_provider_id"] != DBNull.Value ? row["parent_provider_id"] + ", " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            StringBuilder sql = new StringBuilder("INSERT INTO provider (");
            sql.Append("name, url_standard_access, url_admin_access, populated_by, parent_provider_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(sqlValues.ToString());
            sql.Append(")");

            row["provider_id"] = connection.InsertNewRecord(sql.ToString());

            Provider result = daoObjectMapper.MapProvider(row);

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
