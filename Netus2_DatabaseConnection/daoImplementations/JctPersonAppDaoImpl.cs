using Netus2.daoInterfaces;
using Netus2.daoObjects;
using Netus2.dbAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Netus2.daoImplementations
{
    public class JctPersonAppDaoImpl : IJctPersonAppDao
    {
        public void Delete(int personId, int appId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM jct_person_app WHERE 1=1 ");
            sql.Append("AND person_id = " + personId + " ");
            sql.Append("AND app_id = " + appId);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public JctPersonAppDao Read(int personId, int appId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_app WHERE 1=1 ");
            sql.Append("AND person_id = " + personId + " ");
            sql.Append("AND app_id = " + appId);

            List<JctPersonAppDao> results = Read(sql.ToString(), connection);
            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception("The jct_person_app table contains a duplicate record.\n" +
                    "person_id = " + personId + ", app_Id = " + appId);
        }

        public List<JctPersonAppDao> Read_WithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_app where person_id = " + personId;

            return Read(sql, connection);
        }
        public List<JctPersonAppDao> Read_WithAppId(int appId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_app where app_id = " + appId;

            return Read(sql, connection);
        }

        private List<JctPersonAppDao> Read(string sql, IConnectable connection)
        {
            List<JctPersonAppDao> jctPersonAppDaos = new List<JctPersonAppDao>();

            SqlDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    JctPersonAppDao foundJctPersonAppDao = new JctPersonAppDao();
                    foundJctPersonAppDao.person_id = reader.GetInt32(0);
                    foundJctPersonAppDao.app_id = reader.GetInt32(1);
                    jctPersonAppDaos.Add(foundJctPersonAppDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return jctPersonAppDaos;
        }

        public JctPersonAppDao Write(int personId, int appId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_person_app (person_id, app_id) VALUES (");
            sql.Append(personId + ", ");
            sql.Append(appId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            JctPersonAppDao jctPersonAppDao = new JctPersonAppDao();
            jctPersonAppDao.person_id = personId;
            jctPersonAppDao.app_id = appId;

            return jctPersonAppDao;
        }
    }
}
