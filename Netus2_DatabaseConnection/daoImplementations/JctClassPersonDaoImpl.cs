using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctClassPersonDaoImpl : IJctClassPersonDao
    {
        public void Delete(int classId, int personId, IConnectable connection)
        {
            string sql = "DELETE FROM jct_class_person WHERE class_id = " + classId +
                "AND person_id = " + personId;

            connection.ExecuteNonQuery(sql);
        }

        public JctClassPersonDao Read(int classId, int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_person WHERE class_id = " + classId +
                " AND personId = " + personId;

            List<JctClassPersonDao> result = Read(sql, connection);
            if (result.Count > 0)
                return result[0];
            else
                return null;
        }

        public List<JctClassPersonDao> Read_WithClassId(int classId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_person WHERE class_id = " + classId;

            return Read(sql, connection);
        }

        public List<JctClassPersonDao> Read_WithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_person WHERE person_id = " + personId;

            return Read(sql, connection);
        }

        private List<JctClassPersonDao> Read(string sql, IConnectable connection)
        {
            List<JctClassPersonDao> jctClassPersonDaos = new List<JctClassPersonDao>();

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    JctClassPersonDao foundJctClassPersonDao = new JctClassPersonDao();
                    foundJctClassPersonDao.class_id = reader.GetInt32(0);
                    foundJctClassPersonDao.person_id = reader.GetInt32(1);
                    foundJctClassPersonDao.enum_role_id = reader.GetInt32(2);
                    jctClassPersonDaos.Add(foundJctClassPersonDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return jctClassPersonDaos;
        }

        public JctClassPersonDao Write(int classId, int personId, int roleId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_class_person (class_id, person_id, enum_role_id) VALUES (");
            sql.Append(classId + ", ");
            sql.Append(personId + ", ");
            sql.Append(roleId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            JctClassPersonDao jctClassPersonDao = new JctClassPersonDao();
            jctClassPersonDao.class_id = classId;
            jctClassPersonDao.person_id = personId;
            jctClassPersonDao.enum_role_id = roleId;

            return jctClassPersonDao;
        }
    }
}
