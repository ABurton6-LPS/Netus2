using Netus2.daoInterfaces;
using Netus2.daoObjects;
using Netus2.dbAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Netus2.daoImplementations
{
    public class JctClassPeriodDaoImpl : IJctClassPeriodDao
    {
        public void Delete(int classId, int periodId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM jct_class_period WHERE 1=1 ");
            sql.Append("AND class_id = " + classId + " ");
            sql.Append("AND enum_period_id = " + periodId);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public JctClassPeriodDao Read(int classId, int periodId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_class_period WHERE 1=1 ");
            sql.Append("AND class_id = " + classId + " ");
            sql.Append("AND enum_period_id = " + periodId);

            List<JctClassPeriodDao> results = Read(sql.ToString(), connection);
            if (results.Count == 1)
                return results[0];
            else if (results.Count == 0)
                return null;
            else
                throw new Exception("The jct_class_period table contains a duplicate record.\n" +
                    "class_id = " + classId + ", period_id = " + periodId);
        }

        public List<JctClassPeriodDao> Read(int classId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_class_period WHERE 1=1 ");
            sql.Append("AND class_id = " + classId);

            return Read(sql.ToString(), connection);
        }

        private List<JctClassPeriodDao> Read(string sql, IConnectable connection)
        {
            List<JctClassPeriodDao> jctClassPeriodDaos = new List<JctClassPeriodDao>();
            SqlDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql.ToString());
                while (reader.Read())
                {
                    JctClassPeriodDao foundJctClassPeriodDao = new JctClassPeriodDao();
                    foundJctClassPeriodDao.class_id = reader.GetInt32(0);
                    foundJctClassPeriodDao.enum_period_id = reader.GetInt32(1);
                    jctClassPeriodDaos.Add(foundJctClassPeriodDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return jctClassPeriodDaos;
        }

        public JctClassPeriodDao Write(int classId, int periodId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_class_period (class_id, enum_period_id) VALUES (");
            sql.Append(classId + ", ");
            sql.Append(periodId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            JctClassPeriodDao jctClassPeriodDao = new JctClassPeriodDao();
            jctClassPeriodDao.class_id = classId;
            jctClassPeriodDao.enum_period_id = periodId;

            return jctClassPeriodDao;
        }
    }
}
