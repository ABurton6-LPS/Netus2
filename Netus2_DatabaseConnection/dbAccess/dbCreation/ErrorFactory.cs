using Netus2.dbAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Netus2_DatabaseConnection.dbAccess.dbCreation
{
    public class ErrorFactory
    {
        public static void BuildErrorTables(IConnectable connection)
        {
            BuildSyncJob(connection);
            BuildSyncTask(connection);
            BuildSyncJobStatus(connection);
            BuildSyncTaskStatus(connection);
            BuildSyncOrganizationError(connection);
        }

        private static void BuildSyncJob(IConnectable connection)
        {
            string sql =
                "CREATE TABLE sync_job("
                + "sync_job_id int IDENTITY(1,1) PRIMARY KEY,"
                + "[name] varchar(20) NOT NULL,"
                + "[timestamp] datetime)";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildSyncTask(IConnectable connection)
        {
            string sql =
                "CREATE TABLE sync_task("
                + "sync_task_id int IDENTITY(1,1) PRIMARY KEY,"
                + "sync_job_id int,"
                + "[name] varchar(20) NOT NULL,"
                + "[timestamp] datetime,"
                + "FOREIGN KEY(sync_job_id) REFERENCES sync_job(sync_job_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildSyncJobStatus(IConnectable connection)
        {
            string sql =
                "CREATE TABLE sync_job_status("
                + "sync_job_status_id int IDENTITY(1,1) PRIMARY KEY,"
                + "sync_job_id int,"
                + "enum_sync_status_id int,"
                + "[timestamp] datetime,"
                + "FOREIGN KEY(sync_job_id) REFERENCES sync_job(sync_job_id),"
                + "FOREIGN KEY(enum_sync_status_id) REFERENCES enum_sync_status(enum_sync_status_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildSyncTaskStatus(IConnectable connection)
        {
            string sql =
                "CREATE TABLE sync_task_status("
                + "sync_task_status_id int IDENTITY(1,1) PRIMARY KEY,"
                + "sync_task_id int,"
                + "enum_sync_status_id int,"
                + "[timestamp] datetime,"
                + "FOREIGN KEY(sync_task_id) REFERENCES sync_task(sync_task_id),"
                + "FOREIGN KEY(enum_sync_status_id) REFERENCES enum_sync_status(enum_sync_status_id))";

            connection.ExecuteNonQuery(sql);
        }

        private static void BuildSyncOrganizationError(IConnectable connection)
        {
            string sql =
                "CREATE TABLE sync_organization_error("
                + "sync_organization_error_id int IDENTITY(1,1) PRIMARY KEY,"
                + "sync_task_id int,"
                + "[message] varchar(100),"
                + "stack_trace text NOT NULL,"
                + "[timestamp] datetime)";

            connection.ExecuteNonQuery(sql);
        }
    }
}