using Netus2.daoInterfaces;
using Netus2.daoObjects;
using Netus2.dbAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Netus2.daoImplementations
{
    public class ApplicationDaoImpl : IApplicationDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(Application appliction, IConnectable connection)
        {
            DeleteJctPersonApp(appliction, connection);

            ApplicationDao appDao = daoObjectMapper.MapApp(appliction);

            StringBuilder sql = new StringBuilder("DELETE FROM app WHERE 1=1 ");
            sql.Append("AND app_id " + (appDao.app_id != null ? "= " + appDao.app_id + " " : "IS NULL "));
            sql.Append("AND name " + (appDao.name != null ? "LIKE '" + appDao.name + "' " : "IS NULL "));
            sql.Append("AND provider_id " + (appDao.provider_id != null ? "= " + appDao.provider_id + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void DeleteJctPersonApp(Application application, IConnectable connection)
        {
            IJctPersonAppDao jctPersonAppDaoImpl = new JctPersonAppDaoImpl();
            List<JctPersonAppDao> foundJctPersonAppDaos = jctPersonAppDaoImpl.Read_WithAppId(application.Id, connection);

            foreach (JctPersonAppDao foundJctPersonAppDao in foundJctPersonAppDaos)
            {
                jctPersonAppDaoImpl.Delete((int)foundJctPersonAppDao.person_id, (int)foundJctPersonAppDao.app_id, connection);
            }
        }

        public Application Read_UsingAppId(int appId, IConnectable connection)
        {
            string sql = "SELECT * FROM app WHERE app_id = " + appId;

            List<Application> results = Read(sql, connection);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public List<Application> Read_UsingProviderId(int providerId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM app WHERE provider_id = " + providerId);

            return Read(sql.ToString(), connection);
        }

        public List<Application> Read(Application application, IConnectable connection)
        {
            ApplicationDao appDao = daoObjectMapper.MapApp(application);

            StringBuilder sql = new StringBuilder("SELECT * FROM app WHERE 1=1 ");
            if (appDao.app_id != null)
                sql.Append("AND app_id = " + appDao.app_id + " ");
            else
            {
                if (appDao.name != null)
                    sql.Append("AND name LIKE '" + appDao.name + "' ");
                if (appDao.provider_id != null)
                    sql.Append("AND provider_id = " + appDao.provider_id + " ");
            }

            return Read(sql.ToString(), connection);
        }

        private List<Application> Read(string sql, IConnectable connection)
        {
            List<ApplicationDao> foundAppDaos = new List<ApplicationDao>();
            SqlDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    ApplicationDao foundAppDao = new ApplicationDao();

                    foundAppDao.app_id = reader.GetInt32(0);
                    foundAppDao.name = reader.GetString(1);
                    foundAppDao.provider_id = reader.GetInt32(2);

                    foundAppDaos.Add(foundAppDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<Application> results = new List<Application>();
            foreach (ApplicationDao foundAppDao in foundAppDaos)
            {
                Provider foundProvider = Read_Provider((int)foundAppDao.provider_id, connection);
                results.Add(daoObjectMapper.MapApp(foundAppDao, foundProvider));
            }

            return results;
        }

        private Provider Read_Provider(int providerId, IConnectable connection)
        {
            IProviderDao providerDaoImpl = new ProviderDaoImpl();
            return providerDaoImpl.Read_WithProviderId(providerId, connection);
        }

        public void Update(Application application, IConnectable connection)
        {
            List<Application> foundApplications = Read(application, connection);
            if (foundApplications.Count == 0)
                Write(application, connection);
            else if (foundApplications.Count == 1)
            {
                application.Id = foundApplications[0].Id;
                UpdateInternals(application, connection);
            }
            else if (foundApplications.Count > 1)
                throw new Exception("Multiple Applications found matching the description of:\n" +
                    application.ToString());
        }

        private void UpdateInternals(Application application, IConnectable connection)
        {
            ApplicationDao appDao = daoObjectMapper.MapApp(application);

            StringBuilder sql = new StringBuilder("UPDATE app SET ");
            sql.Append("name = " + (appDao.name != null ? "'" + appDao.name + "', " : "NULL, "));
            sql.Append("provider_id = " + (appDao.provider_id != null ? appDao.provider_id + ", " : "NULL, "));
            sql.Append("changed = GETDATE(), ");
            sql.Append("changed_by = 'Netus2' ");
            sql.Append("WHERE app_id = " + appDao.app_id);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public Application Write(Application application, IConnectable connection)
        {
            ApplicationDao appDao = daoObjectMapper.MapApp(application);

            StringBuilder sql = new StringBuilder("INSERT INTO app (name, provider_id, created, created_by) VALUES (");
            sql.Append(appDao.name != null ? "'" + appDao.name + "', " : "NULL, ");
            sql.Append(appDao.provider_id != null ? appDao.provider_id + ", " : "NULL, ");
            sql.Append("GETDATE(), ");
            sql.Append("'Netus2')");

            appDao.app_id = connection.InsertNewRecord(sql.ToString(), "app");

            Provider foundProvider = Read_Provider((int)appDao.provider_id, connection);
            return daoObjectMapper.MapApp(appDao, foundProvider);
        }
    }
}
