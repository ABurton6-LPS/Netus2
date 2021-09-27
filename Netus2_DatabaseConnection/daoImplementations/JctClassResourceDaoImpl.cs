using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctClassResourceDaoImpl : IJctClassResourceDao
    {
        public void Delete(int classId, int resourceId, IConnectable connection)
        {
            string sql = "DELETE FROM jct_class_resource WHERE class_id = " + classId +
                " AND resource_id = " + resourceId;

            connection.ExecuteNonQuery(sql);
        }

        public DataRow Read(int classId, int resourceId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_resource WHERE class_id = " + classId +
                " AND resource_id = " + resourceId;

            List<DataRow> results = Read(sql, connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
        }

        public List<DataRow> Read_WithClassId(int classId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_resource WHERE class_id = " + classId;

            return Read(sql, connection);
        }

        public List<DataRow> Read_WithResourceId(int resourceId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_resource WHERE resource_id = " + resourceId;

            return Read(sql, connection);
        }

        private List<DataRow> Read(string sql, IConnectable connection)
        {
            DataTable dtJctClassResource = DataTableFactory.Dt_Netus2_JctClassResource;
            dtJctClassResource = connection.ReadIntoDataTable(sql, dtJctClassResource);

            List<DataRow> jctClassResourceDaos = new List<DataRow>();
            foreach (DataRow row in dtJctClassResource.Rows)
            {
                jctClassResourceDaos.Add(row);
            }

            return jctClassResourceDaos;
        }

        public DataRow Write(int classId, int resourceId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_class_resource (class_id, resource_id) VALUES (");
            sql.Append(classId + ", ");
            sql.Append(resourceId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            DataRow jctClassResourceDao = DataTableFactory.Dt_Netus2_JctClassResource.NewRow();
            jctClassResourceDao["class_id"] = classId;
            jctClassResourceDao["resource_id"] = resourceId;

            return jctClassResourceDao;
        }
    }
}
