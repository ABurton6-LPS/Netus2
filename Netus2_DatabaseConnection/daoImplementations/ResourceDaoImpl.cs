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
    public class ResourceDaoImpl : IResourceDao
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

        public void Delete(Resource resource, IConnectable connection)
        {
            Delete_JctClassResource(resource, connection);

            DataRow row = daoObjectMapper.MapResource(resource);

            StringBuilder sql = new StringBuilder("DELETE FROM resource WHERE 1=1 ");
            sql.Append("AND resource_id = " + row["resource_id"] + " ");
            sql.Append("AND name " + (row["name"] != DBNull.Value ? "= '" + row["name"] + "' " : "IS NULL "));
            sql.Append("AND enum_importance_id " + (row["enum_importance_id"] != DBNull.Value ? "= " + row["enum_importance_id"] + " " : "IS NULL "));
            sql.Append("AND vendor_resource_identification " + (row["vendor_resource_identification"] != DBNull.Value ? "= '" + row["vendor_resource_identification"] + "' " : "IS NULL "));
            sql.Append("AND vendor_identification " + (row["vendor_identification"] != DBNull.Value ? "= '" + row["vendor_identification"] + "' " : "IS NULL "));
            sql.Append("AND application_identification " + (row["application_identification"] != DBNull.Value ? "= '" + row["application_identification"] + "' " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_JctClassResource(Resource resource, IConnectable connection)
        {
            IJctClassResourceDao jctClassResourceDaoImpl = DaoImplFactory.GetJctClassResourceDaoImpl();
            List<DataRow> foundJctClassResourceDaos =
                jctClassResourceDaoImpl.Read_WithResourceId(resource.Id, connection);

            foreach (DataRow foundJctClassResourceDao in foundJctClassResourceDaos)
            {
                jctClassResourceDaoImpl.Delete((int)foundJctClassResourceDao["class_id"],
                    (int)foundJctClassResourceDao["resource_id"], connection);
            }
        }

        public Resource Read_UsingResourceId(int resourceId, IConnectable connection)
        {
            string sql = "SELECT * FROM resource WHERE resource_id = " + resourceId;

            List<Resource> result = Read(sql, connection);
            if (result.Count > 0)
                return result[0];
            else
                return null;
        }

        public List<Resource> Read(Resource resource, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapResource(resource);

            StringBuilder sql = new StringBuilder("SELECT * FROM resource WHERE 1=1 ");
            if (row["resource_id"] != DBNull.Value)
                sql.Append("AND resource_id = " + row["resource_id"] + " ");
            else
            {
                if (row["name"] != DBNull.Value)
                    sql.Append("AND name LIKE '" + row["name"] + "' ");
                if (row["enum_importance_id"] != DBNull.Value)
                    sql.Append("AND enum_importance_id = " + row["enum_importance_id"] + " ");
                if (row["vendor_resource_identification"] != DBNull.Value)
                    sql.Append("AND vendor_resource_identification LIKE '" + row["vendor_resource_identification"] + "' ");
                if (row["vendor_identification"] != DBNull.Value)
                    sql.Append("AND vendor_identification LIKE '" + row["vendor_identification"] + "' ");
                if (row["application_identification"] != DBNull.Value)
                    sql.Append("AND application_identification LIKE '" + row["application_identification"] + "' ");
            }

            return Read(sql.ToString(), connection);
        }

        public List<Resource> Read(string sql, IConnectable connection)
        {
            DataTable dtResource = DataTableFactory.Dt_Netus2_Resource;
            dtResource = connection.ReadIntoDataTable(sql, dtResource);

            List<Resource> results = new List<Resource>();
            foreach (DataRow row in dtResource.Rows)
            {
                results.Add(daoObjectMapper.MapResource(row));
            }

            return results;
        }

        public void Update(Resource resource, IConnectable connection)
        {
            List<Resource> foundResources =
                Read(resource, connection);
            if (foundResources.Count == 0)
                Write(resource, connection);
            else if (foundResources.Count == 1)
            {
                resource.Id = foundResources[0].Id;
                UpdateInternals(resource, connection);
            }
            else if (foundResources.Count > 1)
                throw new Exception(foundResources.Count + " Resources found matching the description of:\n" +
                    resource.ToString());
        }

        private void UpdateInternals(Resource resource, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapResource(resource);

            if (row["resource_id"] != DBNull.Value)
            {
                StringBuilder sql = new StringBuilder("UPDATE resource SET ");
                sql.Append("name = " + (row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, "));
                sql.Append("enum_importance_id = " + (row["enum_importance_id"] != DBNull.Value ? row["enum_importance_id"] + ", " : "NULL, "));
                sql.Append("vendor_resource_identification = " + (row["vendor_resource_identification"] != DBNull.Value ? "'" + row["vendor_resource_identification"] + "', " : "NULL, "));
                sql.Append("vendor_identification = " + (row["vendor_identification"] != DBNull.Value ? "'" + row["vendor_identification"] + "', " : "NULL, ")); ;
                sql.Append("application_identification = " + (row["application_identification"] != DBNull.Value ? "'" + row["application_identification"] + "', " : "NULL, "));
                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE resource_id = " + row["resource_id"]);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
                throw new Exception("The following Resource needs to be inserted into the database, before it can be updated.\n" + resource.ToString());
        }

        public Resource Write(Resource resource, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapResource(resource);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, ");
            sqlValues.Append(row["enum_importance_id"] != DBNull.Value ? row["enum_importance_id"] + ", " : "NULL, ");
            sqlValues.Append(row["vendor_resource_identification"] != DBNull.Value ? "'" + row["vendor_resource_identification"] + "', " : "NULL, ");
            sqlValues.Append(row["vendor_identification"] != DBNull.Value ? "'" + row["vendor_identification"] + "', " : "NULL, ");
            sqlValues.Append(row["application_identification"] != DBNull.Value ? "'" + row["application_identification"] + "', " : "NULL, ");
            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql =
                "INSERT INTO resource " +
                "(name, enum_importance_id, vendor_resource_identification, vendor_identification, " +
                "application_identification, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["resource_id"] = connection.InsertNewRecord(sql);

            Resource result = daoObjectMapper.MapResource(row);

            return result;
        }
    }
}
