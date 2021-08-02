using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2SisSync.SyncProcesses;
using System;
using System.Text;

namespace Netus2SisSync.UtilityTools
{
    public static class SyncLogger
    {
        public static SyncJob LogNewJob(string jobName, IConnectable connection)
        {
            SyncJob job = new SyncJob(jobName, DateTime.Now);

            StringBuilder sql = new StringBuilder("INSERT INTO sync_job(");
            sql.Append("[name], [timestamp]");
            sql.Append(") VALUES (");
            sql.Append("'" + job.Name + "', ");
            sql.Append("'" + job.Timestamp + "')");

            job.Id = connection.InsertNewRecord(sql.ToString());

            LogStatus(job, Enum_Sync_Status.values["start"], connection);

            return job;
        }

        public static SyncTask LogNewTask(string taskName, SyncJob job, IConnectable connection)
        {
            SyncTask task = new SyncTask(taskName, DateTime.Now);

            StringBuilder sql = new StringBuilder("INSERT INTO sync_task(");
            sql.Append("sync_job_id, [name], [timestamp]");
            sql.Append(") VALUES (");
            sql.Append(job.Id + ", ");
            sql.Append("'" + task.Name + "', ");
            sql.Append("'" + task.Timestamp + "')");

            task.Id = connection.InsertNewRecord(sql.ToString());

            LogStatus(task, Enum_Sync_Status.values["start"], connection);

            return task;
        }

        public static void LogStatus(SyncJob job, Enumeration enumStatus, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO sync_job_status (");
            sql.Append("sync_job_id, enum_sync_status_id, [timestamp]");
            sql.Append(") VALUES (");
            sql.Append(job.Id + ", ");
            sql.Append(enumStatus.Id + ", ");
            sql.Append("'" + job.Timestamp + "')");

            connection.InsertNewRecord(sql.ToString());
        }
        public static void LogStatus(SyncTask task, Enumeration enumStatus, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO sync_task_status (");
            sql.Append("sync_task_id, enum_sync_status_id, [timestamp]");
            sql.Append(") VALUES (");
            sql.Append(task.Id + ", ");
            sql.Append(enumStatus.Id + ", ");
            sql.Append("'" + task.Timestamp + "')");

            connection.InsertNewRecord(sql.ToString());
        }

        public static void LogError(Exception e, SyncJob job, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO sync_error(");
            sql.Append("sync_job_id, [message], stack_trace, [timestamp]");
            sql.Append(") VALUES (");
            sql.Append(job.Id + ", ");
            sql.Append("'" + e.Message + "', ");
            sql.Append("'" + e.StackTrace + "', ");
            sql.Append("'" + DateTime.Now + "')");

            connection.InsertNewRecord(sql.ToString());

            LogStatus(job, Enum_Sync_Status.values["error"], connection);
        }

        public static void LogError(Exception e, SyncTask task, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO sync_error(");
            sql.Append("sync_task_id, [message], stack_trace, [timestamp]");
            sql.Append(") VALUES (");
            sql.Append(task.Id + ", ");
            sql.Append("'" + e.Message + "', ");
            sql.Append("'" + e.StackTrace + "', ");
            sql.Append("'" + DateTime.Now + "')");

            connection.InsertNewRecord(sql.ToString());

            LogStatus(task, Enum_Sync_Status.values["error"], connection);
        }
    }
}
