using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
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

        public DataRow Read(int personId, int appId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_app WHERE 1=1 ");
            sql.Append("AND person_id = " + personId + " ");
            sql.Append("AND app_id = " + appId);

            List<DataRow> results = Read(sql.ToString(), connection);
            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception("The jct_person_app table contains a duplicate record.\n" +
                    "person_id = " + personId + ", app_Id = " + appId);
        }

        public List<DataRow> Read_WithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_app WHERE person_id = " + personId;

            return Read(sql, connection);
        }
        public List<DataRow> Read_WithAppId(int appId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_app WHERE app_id = " + appId;

            return Read(sql, connection);
        }

        private List<DataRow> Read(string sql, IConnectable connection)
        {
            DataTable dtJctPersonApp = new DataTableFactory().Dt_Netus2_JctPersonApp;
            dtJctPersonApp = connection.ReadIntoDataTable(sql, dtJctPersonApp).Result;
            
            List<DataRow> jctPersonAppDaos = new List<DataRow>();
            foreach (DataRow row in dtJctPersonApp.Rows)
            {
                jctPersonAppDaos.Add(row);
            }

            return jctPersonAppDaos;
        }

        public DataRow Write(int personId, int appId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_person_app (person_id, app_id) VALUES (");
            sql.Append(personId + ", ");
            sql.Append(appId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            DataRow jctPersonAppDao = new DataTableFactory().Dt_Netus2_JctPersonApp.NewRow();
            jctPersonAppDao["person_id"] = personId;
            jctPersonAppDao["app_id"] = appId;

            return jctPersonAppDao;
        }
    }
}
