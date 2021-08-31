using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
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

        public DataRow Read(int classId, int periodId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_class_period WHERE 1=1 ");
            sql.Append("AND class_id = " + classId + " ");
            sql.Append("AND enum_period_id = " + periodId);

            List<DataRow> results = Read(sql.ToString(), connection);
            if (results.Count == 1)
                return results[0];
            else if (results.Count == 0)
                return null;
            else
                throw new Exception("The jct_class_period table contains a duplicate record.\n" +
                    "class_id = " + classId + ", period_id = " + periodId);
        }

        public List<DataRow> Read(int classId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_class_period WHERE 1=1 ");
            sql.Append("AND class_id = " + classId);

            return Read(sql.ToString(), connection);
        }

        private List<DataRow> Read(string sql, IConnectable connection)
        {
            DataTable dtJctClassPeriod = new DataTableFactory().Dt_Netus2_JctClassPeriod;
            List<DataRow> foundDataRows = new List<DataRow>();
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql.ToString());
                dtJctClassPeriod.Load(reader);

                foreach(DataRow row in dtJctClassPeriod.Rows)
                {
                    foundDataRows.Add(row);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return foundDataRows;
        }

        public DataRow Write(int classId, int periodId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_class_period (class_id, enum_period_id) VALUES (");
            sql.Append(classId + ", ");
            sql.Append(periodId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            DataRow jctClassPeriodDao = new DataTableFactory().Dt_Netus2_JctClassPeriod.NewRow();
            jctClassPeriodDao["class_id"] = classId;
            jctClassPeriodDao["enum_period_id"] = periodId;

            return jctClassPeriodDao;
        }
    }
}
