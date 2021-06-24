using Netus2.daoInterfaces;
using Netus2.daoObjects;
using Netus2.dbAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Netus2.daoImplementations
{
    public class JctPersonRoleDaoImpl : IJctPersonRoleDao
    {
        public void Delete(int personId, int roleId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM jct_person_role WHERE 1=1 ");
            sql.Append("AND person_id = " + personId + " ");
            sql.Append("AND enum_role_id = " + roleId);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public JctPersonRoleDao Read(int personId, int roleId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_role WHERE 1=1 ");
            sql.Append("AND person_id = " + personId + " ");
            sql.Append("AND enum_role_id = " + roleId);

            List<JctPersonRoleDao> results = Read(sql.ToString(), connection);
            if (results.Count == 1)
                return results[0];
            else if (results.Count == 0)
                return null;
            else
                throw new Exception("The jct_person_role table contains a duplicate record...somehow.\n" +
                    "person_id = " + personId + ", role_id = " + roleId);
        }

        public List<JctPersonRoleDao> Read(int personId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_role WHERE 1=1 ");
            sql.Append("AND person_id = " + personId);

            return Read(sql.ToString(), connection);
        }

        private List<JctPersonRoleDao> Read(string sql, IConnectable connection)
        {
            List<JctPersonRoleDao> jctPersonRoleDaos = new List<JctPersonRoleDao>();
            SqlDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql.ToString());
                while (reader.Read())
                {
                    JctPersonRoleDao foundJctPersonRoleDao = new JctPersonRoleDao();
                    foundJctPersonRoleDao.person_id = reader.GetInt32(0);
                    foundJctPersonRoleDao.enum_role_id = reader.GetInt32(1);
                    jctPersonRoleDaos.Add(foundJctPersonRoleDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return jctPersonRoleDaos;
        }

        public JctPersonRoleDao Write(int personId, int roleId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_person_role (person_id, enum_role_id) VALUES (");
            sql.Append(personId + ", ");
            sql.Append(roleId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            JctPersonRoleDao jctPersonRoleDao = new JctPersonRoleDao();
            jctPersonRoleDao.person_id = personId;
            jctPersonRoleDao.enum_role_id = roleId;

            return jctPersonRoleDao;
        }
    }
}
