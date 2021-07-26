using Netus2.daoInterfaces;
using Netus2.daoObjects;
using Netus2.dbAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Netus2.daoImplementations
{
    public class JctPersonPersonDaoImpl : IJctPersonPersonDao
    {
        public void Delete(int personOneId, int personTwoId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM jct_person_person WHERE 1=1 ");
            sql.Append("AND person_one_id = " + personOneId + " ");
            sql.Append("AND person_two_id = " + personTwoId);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public List<JctPersonPersonDao> Read(int personOneId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_person WHERE 1=1 ");
            sql.Append("AND person_one_id = " + personOneId);

            return Read(sql.ToString(), connection);
        }

        public JctPersonPersonDao Read(int personOneId, int personTwoId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_person WHERE 1=1 ");
            sql.Append("AND person_one_id = " + personOneId + " ");
            sql.Append("AND person_two_id = " + personTwoId);

            List<JctPersonPersonDao> results = Read(sql.ToString(), connection);
            if (results.Count == 0)
                return null;
            if (results.Count == 1)
                return results[0];
            else
                throw new Exception("The jct_person_person table contains a duplicate record.\n" +
                    "person_one_id = " + personOneId + ", person_two_id = " + personTwoId);
        }

        private List<JctPersonPersonDao> Read(string sql, IConnectable connection)
        {
            List<JctPersonPersonDao> jctPersonPersonDaos = new List<JctPersonPersonDao>();
            SqlDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql.ToString());
                while (reader.Read())
                {
                    JctPersonPersonDao foundJctPersonPersonDao = new JctPersonPersonDao();
                    foundJctPersonPersonDao.person_one_id = reader.GetInt32(0);
                    foundJctPersonPersonDao.person_two_id = reader.GetInt32(1);
                    jctPersonPersonDaos.Add(foundJctPersonPersonDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return jctPersonPersonDaos;
        }

        public JctPersonPersonDao Write(int personOneId, int personTwoId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_person_person (person_one_id, person_two_id) VALUES (");
            sql.Append(personOneId + ", ");
            sql.Append(personTwoId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            JctPersonPersonDao jctPersonPersonDao = new JctPersonPersonDao();
            jctPersonPersonDao.person_one_id = personOneId;
            jctPersonPersonDao.person_two_id = personTwoId;

            return jctPersonPersonDao;
        }
    }
}
