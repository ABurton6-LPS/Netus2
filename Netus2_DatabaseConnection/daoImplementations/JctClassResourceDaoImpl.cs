using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dbAccess;
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
                "AND resource_id = " + resourceId;

            connection.ExecuteNonQuery(sql);
        }

        public JctClassResourceDao Read(int classId, int resourceId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_resource WHERE class_id = " + classId +
                " AND resourceId = " + resourceId;

            List<JctClassResourceDao> results = Read(sql, connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
        }

        public List<JctClassResourceDao> Read_WithClassId(int classId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_resource WHERE class_id = " + classId;

            return Read(sql, connection);
        }

        public List<JctClassResourceDao> Read_WithResourceId(int resourceId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_resource WHERE resource_id = " + resourceId;

            return Read(sql, connection);
        }

        private List<JctClassResourceDao> Read(string sql, IConnectable connection)
        {
            List<JctClassResourceDao> jctClassResourceDaos = new List<JctClassResourceDao>();

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    JctClassResourceDao foundJctClassResourceDao = new JctClassResourceDao();
                    foundJctClassResourceDao.class_id = reader.GetInt32(0);
                    foundJctClassResourceDao.resource_id = reader.GetInt32(1);
                    jctClassResourceDaos.Add(foundJctClassResourceDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return jctClassResourceDaos;
        }

        public JctClassResourceDao Write(int classId, int resourceId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_class_resource (class_id, resource_id) VALUES (");
            sql.Append(classId + ", ");
            sql.Append(resourceId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            JctClassResourceDao jctClassResourceDao = new JctClassResourceDao();
            jctClassResourceDao.class_id = classId;
            jctClassResourceDao.resource_id = resourceId;

            return jctClassResourceDao;
        }
    }
}
