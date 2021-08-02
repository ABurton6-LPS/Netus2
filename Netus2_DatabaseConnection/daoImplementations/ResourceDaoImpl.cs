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
    public class ResourceDaoImpl : IResourceDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(Resource resource, IConnectable connection)
        {
            Delete_JctClassResource(resource, connection);

            ResourceDao resourceDao = daoObjectMapper.MapResource(resource);

            StringBuilder sql = new StringBuilder("DELETE FROM resource WHERE 1=1 ");
            sql.Append("AND resource_id = " + resourceDao.resource_id + " ");
            sql.Append("AND name " + (resourceDao.name != null ? "= '" + resourceDao.name + "' " : "IS NULL "));
            sql.Append("AND enum_importance_id " + (resourceDao.enum_importance_id != null ? "= " + resourceDao.enum_importance_id + " " : "IS NULL "));
            sql.Append("AND vendor_resource_identification " + (resourceDao.vendor_resource_identification != null ? "= '" + resourceDao.vendor_resource_identification + "' " : "IS NULL "));
            sql.Append("AND vendor_identification " + (resourceDao.vendor_identification != null ? "= '" + resourceDao.vendor_identification + "' " : "IS NULL "));
            sql.Append("AND application_identification " + (resourceDao.application_identification != null ? "= '" + resourceDao.application_identification + "' " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_JctClassResource(Resource resource, IConnectable connection)
        {
            IJctClassResourceDao jctClassResourceDaoImpl = new JctClassResourceDaoImpl();
            List<JctClassResourceDao> foundJctClassResourceDaos =
                jctClassResourceDaoImpl.Read_WithResourceId(resource.Id, connection);

            foreach (JctClassResourceDao foundJctClassResourceDao in foundJctClassResourceDaos)
            {
                jctClassResourceDaoImpl.Delete((int)foundJctClassResourceDao.class_id,
                    (int)foundJctClassResourceDao.resource_id, connection);
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
            ResourceDao resourceDao = daoObjectMapper.MapResource(resource);

            StringBuilder sql = new StringBuilder("SELECT * FROM resource WHERE 1=1 ");
            if (resourceDao.resource_id != null)
                sql.Append("AND resource_id = " + resourceDao.resource_id + " ");
            else
            {
                if (resourceDao.name != null)
                    sql.Append("AND name LIKE '" + resourceDao.name + "' ");
                if (resourceDao.enum_importance_id != null)
                    sql.Append("AND enum_importance_id = " + resourceDao.enum_importance_id + " ");
                if (resourceDao.vendor_resource_identification != null)
                    sql.Append("AND vendor_resource_identification LIKE '" + resourceDao.vendor_resource_identification + "' ");
                if (resourceDao.vendor_identification != null)
                    sql.Append("AND vendor_identification LIKE '" + resourceDao.vendor_identification + "' ");
                if (resourceDao.application_identification != null)
                    sql.Append("AND application_identification LIKE '" + resourceDao.application_identification + "' ");
            }

            return Read(sql.ToString(), connection);
        }

        public List<Resource> Read(string sql, IConnectable connection)
        {
            List<ResourceDao> foundResourceDaos = new List<ResourceDao>();

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    ResourceDao foundResourceDao = new ResourceDao();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                if (value != DBNull.Value)
                                    foundResourceDao.resource_id = (int)value;
                                else
                                    foundResourceDao.resource_id = null;
                                break;
                            case 1:
                                foundResourceDao.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    foundResourceDao.enum_importance_id = (int)value;
                                else
                                    foundResourceDao.enum_importance_id = null;
                                break;
                            case 3:
                                foundResourceDao.vendor_resource_identification = value != DBNull.Value ? (string)value : null;
                                break;
                            case 4:
                                foundResourceDao.vendor_identification = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                foundResourceDao.application_identification = value != DBNull.Value ? (string)value : null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    foundResourceDao.created = (DateTime)value;
                                else
                                    foundResourceDao.created = null;
                                break;
                            case 7:
                                foundResourceDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    foundResourceDao.changed = (DateTime)value;
                                else
                                    foundResourceDao.changed = null;
                                    break;
                            case 9:
                                foundResourceDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in resource table: " + reader.GetName(i));
                        }
                    }
                    foundResourceDaos.Add(foundResourceDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<Resource> results = new List<Resource>();
            foreach (ResourceDao foundResourceDao in foundResourceDaos)
            {
                results.Add(daoObjectMapper.MapResource(foundResourceDao));
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
            ResourceDao resourceDao = daoObjectMapper.MapResource(resource);

            if (resourceDao.resource_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE resource SET ");
                sql.Append("name = " + (resourceDao.name != null ? "'" + resourceDao.name + "', " : "NULL, "));
                sql.Append("enum_importance_id = " + (resourceDao.enum_importance_id != null ? +resourceDao.enum_importance_id + ", " : "NULL, "));
                sql.Append("vendor_resource_identification = " + (resourceDao.vendor_resource_identification != null ? "'" + resourceDao.vendor_resource_identification + "', " : "NULL, "));
                sql.Append("vendor_identification = " + (resourceDao.vendor_identification != null ? "'" + resourceDao.vendor_identification + "', " : "NULL, ")); ;
                sql.Append("application_identification = " + (resourceDao.application_identification != null ? "'" + resourceDao.application_identification + "', " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE resource_id = " + resourceDao.resource_id);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
                throw new Exception("The following Resource needs to be inserted into the database, before it can be updated.\n" + resource.ToString());
        }

        public Resource Write(Resource resource, IConnectable connection)
        {
            ResourceDao resourceDao = daoObjectMapper.MapResource(resource);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(resourceDao.name != null ? "'" + resourceDao.name + "', " : "NULL, ");
            sqlValues.Append(resourceDao.enum_importance_id != null ? resourceDao.enum_importance_id + ", " : "NULL, ");
            sqlValues.Append(resourceDao.vendor_resource_identification != null ? "'" + resourceDao.vendor_resource_identification + "', " : "NULL, ");
            sqlValues.Append(resourceDao.vendor_identification != null ? "'" + resourceDao.vendor_identification + "', " : "NULL, ");
            sqlValues.Append(resourceDao.application_identification != null ? "'" + resourceDao.application_identification + "', " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            string sql =
                "INSERT INTO resource " +
                "(name, enum_importance_id, vendor_resource_identification, vendor_identification, " +
                "application_identification, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            resourceDao.resource_id = connection.InsertNewRecord(sql);

            Resource result = daoObjectMapper.MapResource(resourceDao);

            return result;
        }
    }
}
