using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctClassResourceDaoImpl : IJctClassResourceDao
    {
        public void Delete(int classId, int resourceId, IConnectable connection)
        {
            if (classId <= 0 || resourceId <= 0)
                throw new Exception("Cannot delete a record from jct_class_period " +
                    "without a database-assigned ID for both classId and resourceId." +
                    "\nclassId: " + classId +
                    "\resourceId: " + resourceId);

            string sql = "DELETE FROM jct_class_resource WHERE 1=1 " +
                "AND class_id = @class_id " +
                "AND resource_id = @resource_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classId));
            parameters.Add(new SqlParameter("@resource_id", resourceId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int classId, int resourceId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_resource WHERE 1=1 " +
                "AND class_id = @class_id " +
                "AND resource_id = @resource_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classId));
            parameters.Add(new SqlParameter("@resource_id", resourceId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public List<DataRow> Read_AllWithClassId(int classId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_resource WHERE " +
                "class_id = @class_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllWithResourceId(int resourceId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_resource WHERE " +
                "resource_id = @resource_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@resource_id", resourceId));

            return Read(sql, connection, parameters);
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctClassResource = DataTableFactory.CreateDataTable_Netus2_JctClassResource();
            dtJctClassResource = connection.ReadIntoDataTable(sql, dtJctClassResource, parameters);

            List<DataRow> jctClassResourceDaos = new List<DataRow>();
            foreach (DataRow row in dtJctClassResource.Rows)
                jctClassResourceDaos.Add(row);

            return jctClassResourceDaos;
        }

        public DataRow Write(int classId, int resourceId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_class_resource (" +
                "class_id, resource_id) VALUES (" +
                "@class_id, @resource_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classId));
            parameters.Add(new SqlParameter("@resource_id", resourceId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctClassResourceDao = DataTableFactory.CreateDataTable_Netus2_JctClassResource().NewRow();
            jctClassResourceDao["class_id"] = classId;
            jctClassResourceDao["resource_id"] = resourceId;

            return jctClassResourceDao;
        }
    }
}
