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
            if(resource.Id <= 0)
                throw new Exception("Cannot delete a resource which doesn't have a database-assigned ID.\n" + resource.ToString());

            Delete_JctClassResource(resource, connection);

            DataRow row = daoObjectMapper.MapResource(resource);

            string sql = "DELETE FROM resource WHERE " +
            "resource_id = @resource_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@resource_id", resource.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void Delete_JctClassResource(Resource resource, IConnectable connection)
        {
            IJctClassResourceDao jctClassResourceDaoImpl = DaoImplFactory.GetJctClassResourceDaoImpl();
            List<DataRow> foundJctClassResourceDaos =
                jctClassResourceDaoImpl.Read_AllWithResourceId(resource.Id, connection);

            foreach (DataRow foundJctClassResourceDao in foundJctClassResourceDaos)
            {
                jctClassResourceDaoImpl.Delete((int)foundJctClassResourceDao["class_enrolled_id"],
                    (int)foundJctClassResourceDao["resource_id"], connection);
            }
        }

        public Resource Read_UsingResourceId(int resourceId, IConnectable connection)
        {
            string sql = "SELECT * FROM resource WHERE resource_id = @resource_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@resource_id", resourceId));

            List<Resource> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching resourceId: " + resourceId);
        }

        public List<Resource> Read(Resource resource, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapResource(resource);
            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM resource WHERE 1=1 ");
            if (row["resource_id"] != DBNull.Value)
            {
                sql.Append("AND resource_id = @resource_id");
                parameters.Add(new SqlParameter("@resource_id", row["resource_id"]));
            }
            else
            {
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("AND name = @name ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }

                if (row["enum_importance_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_importance_id = @enum_importance_id ");
                    parameters.Add(new SqlParameter("@enum_importance_id", row["enum_importance_id"]));
                }

                if (row["vendor_resource_identification"] != DBNull.Value)
                {
                    sql.Append("AND vendor_resource_identification = @vendor_resource_identification ");
                    parameters.Add(new SqlParameter("@vendor_resource_identification", row["vendor_resource_identification"]));
                }

                if (row["vendor_identification"] != DBNull.Value)
                {
                    sql.Append("AND vendor_identification = @vendor_identification ");
                    parameters.Add(new SqlParameter("@vendor_identification", row["vendor_identification"]));
                }

                if (row["application_identification"] != DBNull.Value)
                {
                    sql.Append("AND application_identification = @application_identification ");
                    parameters.Add(new SqlParameter("@application_identification", row["application_identification"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<Resource> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtResource = DataTableFactory.CreateDataTable_Netus2_Resource();
            dtResource = connection.ReadIntoDataTable(sql, dtResource, parameters);

            List<Resource> results = new List<Resource>();
            foreach (DataRow row in dtResource.Rows)
                results.Add(daoObjectMapper.MapResource(row));

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
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE resource SET ");
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("name = @name, ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }
                else
                    sql.Append("name = NULL, ");

                if (row["enum_importance_id"] != DBNull.Value)
                {
                    sql.Append("enum_importance_id = @enum_importance_id, ");
                    parameters.Add(new SqlParameter("@enum_importance_id", row["enum_importance_id"]));
                }
                else
                    sql.Append("enum_importance_id = NULL, ");

                if (row["vendor_resource_identification"] != DBNull.Value)
                {
                    sql.Append("vendor_resource_identification = @vendor_resource_identification, ");
                    parameters.Add(new SqlParameter("@vendor_resource_identification", row["vendor_resource_identification"]));
                }
                else
                    sql.Append("vendor_resource_identification = NULL, ");

                if (row["vendor_identification"] != DBNull.Value)
                {
                    sql.Append("vendor_identification = @vendor_identification, ");
                    parameters.Add(new SqlParameter("@vendor_identification", row["vendor_identification"]));
                }
                else
                    sql.Append("vendor_identification = NULL, ");

                if (row["application_identification"] != DBNull.Value)
                {
                    sql.Append("application_identification = @application_identification, ");
                    parameters.Add(new SqlParameter("@application_identification", row["application_identification"]));
                }
                else
                    sql.Append("application_identification = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE resource_id = @resource_id");
                parameters.Add(new SqlParameter("@resource_id", row["resource_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);
            }
            else
                throw new Exception("The following Resource needs to be inserted into the database, before it can be updated.\n" + resource.ToString());
        }

        public Resource Write(Resource resource, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapResource(resource);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["name"] != DBNull.Value)
            {
                sqlValues.Append("@name, ");
                parameters.Add(new SqlParameter("@name", row["name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_importance_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_importance_id, ");
                parameters.Add(new SqlParameter("@enum_importance_id", row["enum_importance_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["vendor_resource_identification"] != DBNull.Value)
            {
                sqlValues.Append("@vendor_resource_identification, ");
                parameters.Add(new SqlParameter("@vendor_resource_identification", row["vendor_resource_identification"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["vendor_identification"] != DBNull.Value)
            {
                sqlValues.Append("@vendor_identification, ");
                parameters.Add(new SqlParameter("@vendor_identification", row["vendor_identification"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["application_identification"] != DBNull.Value)
            {
                sqlValues.Append("@application_identification, ");
                parameters.Add(new SqlParameter("@application_identification", row["application_identification"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql =
                "INSERT INTO resource " +
                "(name, enum_importance_id, vendor_resource_identification, vendor_identification, " +
                "application_identification, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["resource_id"] = connection.InsertNewRecord(sql, parameters);

            Resource result = daoObjectMapper.MapResource(row);

            return result;
        }
    }
}
