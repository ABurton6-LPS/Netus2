using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2SisSync.SyncProcesses;
using System;
using System.Data;
using System.Text;

namespace Netus2SisSync.UtilityTools
{
    public static class SyncLogger
    {
        public static void LogNewJob(SyncJob job, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO sync_job(");
            sql.Append("[name], [timestamp]");
            sql.Append(") VALUES (");
            sql.Append("'" + job.Name + "', ");
            sql.Append("'" + DateTime.Now + "')");

            job.Id = connection.InsertNewRecord(sql.ToString());

            LogStatus(job, Enum_Sync_Status.values["start"], connection);
        }

        public static SyncTask LogNewTask(SyncTask task, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO sync_task(");
            sql.Append("sync_job_id, [name], [timestamp]");
            sql.Append(") VALUES (");
            sql.Append(task.Job.Id + ", ");
            sql.Append("'" + task.Name + "', ");
            sql.Append("'" + DateTime.Now + "')");

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
            sql.Append("'" + DateTime.Now + "')");

            connection.InsertNewRecord(sql.ToString());
        }
        public static void LogStatus(SyncTask task, Enumeration enumStatus, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO sync_task_status (");
            sql.Append("sync_task_id, enum_sync_status_id, [timestamp]");
            sql.Append(") VALUES (");
            sql.Append(task.Id + ", ");
            sql.Append(enumStatus.Id + ", ");
            sql.Append("'" + DateTime.Now + "')");

            connection.InsertNewRecord(sql.ToString());
        }

        public static void LogError(Exception e, SyncJob job, IConnectable connection)
        {
            string errorMessage = e.Message;
            while(e.Message.IndexOf('\'') > 0)
            {
                errorMessage = e.Message.Insert(e.Message.IndexOf('\''), "\'");
            }

            string errorStackTrace = e.StackTrace;
            while(e.StackTrace.IndexOf('\'') > 0)
            {
                errorStackTrace = e.StackTrace.Insert(e.StackTrace.IndexOf('\''), "\'");
            }

            StringBuilder sql = new StringBuilder("INSERT INTO sync_error(");
            sql.Append("sync_job_id, [message], stack_trace, [timestamp]");
            sql.Append(") VALUES (");
            sql.Append(job.Id + ", ");
            sql.Append("'" + errorMessage + "', ");
            sql.Append("'" + errorStackTrace + "', ");
            sql.Append("'" + DateTime.Now + "')");

            connection.InsertNewRecord(sql.ToString());

            LogStatus(job, Enum_Sync_Status.values["error"], connection);
        }

        public static void LogError(Exception e, SyncTask task, IConnectable connection)
        {
            LogError(e, task, null, connection);
        }

        public static void LogError(Exception e, SyncTask task, DataRow row, IConnectable connection)
        {
            string errorMessage = e.Message;
            errorMessage = e.Message.Replace("\'", "\'\'");

            string errorStackTrace = e.StackTrace;
            while (e.StackTrace.IndexOf('\'') > 0)
            {
                errorStackTrace = e.StackTrace.Insert(e.StackTrace.IndexOf('\''), "\'");
            }
            
            if(row != null)
            {
                StringBuilder dataThatCausedError = new StringBuilder();
                foreach (DataColumn col in row.Table.Columns)
                {
                    dataThatCausedError.AppendFormat("{0},", row[col]);
                }
                dataThatCausedError = dataThatCausedError.Remove(dataThatCausedError.Length - 1, 1);
                errorStackTrace += "\nData That Caused Error:\n" + dataThatCausedError.ToString() + ";";
            }

            StringBuilder sql = new StringBuilder("INSERT INTO sync_error(");
            sql.Append("sync_task_id, [message], stack_trace, [timestamp]");
            sql.Append(") VALUES (");
            sql.Append(task.Id + ", ");
            sql.Append("'" + errorMessage + "', ");
            sql.Append("'" + errorStackTrace + "', ");
            sql.Append("'" + DateTime.Now + "')");

            connection.InsertNewRecord(sql.ToString());

            LogStatus(task, Enum_Sync_Status.values["error"], connection);
        }
    }
}
