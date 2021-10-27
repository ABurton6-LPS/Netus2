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
    public class JctPersonAppDaoImpl : IJctPersonAppDao
    {
        public void Delete(int personId, int appId, IConnectable connection)
        {
            if (personId <= 0 || appId <= 0)
                throw new Exception("Cannot delete a record from jct_person_app " +
                    "without a database-assigned ID for both personId and appId." +
                    "\npersonId: " + personId +
                    "\nappId: " + appId);

            string sql = "DELETE FROM jct_person_app WHERE 1=1 " +
            "AND person_id = @person_id " +
            "AND app_id = @app_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@app_id", appId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int personId, int appId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_app WHERE 1=1 " +
            "AND person_id = @person_id " +
            "AND app_id = @app_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@app_id", appId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_app WHERE " +
                "person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllWithAppId(int appId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_app WHERE " +
                "app_id = @app_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@app_id", appId));

            return Read(sql, connection, parameters);
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctPersonApp = DataTableFactory.CreateDataTable_Netus2_JctPersonApp();
            dtJctPersonApp = connection.ReadIntoDataTable(sql, dtJctPersonApp, parameters);
            
            List<DataRow> jctPersonAppDaos = new List<DataRow>();
            foreach (DataRow row in dtJctPersonApp.Rows)
                jctPersonAppDaos.Add(row);

            return jctPersonAppDaos;
        }

        public DataRow Write(int personId, int appId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_person_app (" +
                "person_id, app_id) VALUES (" +
                "@person_id, @app_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@app_id", appId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctPersonAppDao = DataTableFactory.CreateDataTable_Netus2_JctPersonApp().NewRow();
            jctPersonAppDao["person_id"] = personId;
            jctPersonAppDao["app_id"] = appId;

            return jctPersonAppDao;
        }
    }
}
