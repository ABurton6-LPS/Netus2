using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2SisSync.SyncProcesses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace Netus2SisSync.UtilityTools
{
    public static class SyncLogger
    {
        public static void LogNewJob(SyncJob job)
        {
            IConnectable connection = DbConnectionFactory.GetNetus2Connection();
            try
            {
                string sql = "INSERT INTO sync_job([name], [timestamp]) VALUES (@name, dbo.CURRENT_DATETIME())";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@name", job.Name));

                job.Id = connection.InsertNewRecord(sql, parameters);

                LogStatus(job, Enum_Sync_Status.values["start"]);
            }
            catch(Exception e)
            {
                LogError(e, job);
            }
            finally
            {
                connection.CloseConnection();
            }            
        }

        public static void LogNewTask(SyncTask task)
        {
            IConnectable connection = DbConnectionFactory.GetNetus2Connection();
            try
            {
                string sql = "INSERT INTO sync_task(" +
                "sync_job_id, [name], [timestamp]" +
                ") VALUES (" +
                "@sync_job_id, @name, dbo.CURRENT_DATETIME())";

                List<SqlParameter> paramaters = new List<SqlParameter>();
                paramaters.Add(new SqlParameter("@sync_job_id", task.Job.Id));
                paramaters.Add(new SqlParameter("@name", task.Name));

                task.Id = connection.InsertNewRecord(sql, paramaters);

                LogStatus(task, Enum_Sync_Status.values["start"]);
            }
            catch (Exception e)
            {
                LogError(e, task, null);
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public static void LogStatus(SyncJob job, Enumeration enumStatus)
        {
            IConnectable connection = DbConnectionFactory.GetNetus2Connection();
            try
            {
                string sql = "INSERT INTO sync_job_status (" +
                "sync_job_id, enum_sync_status_id, [timestamp]" +
                ") VALUES (@sync_job_id, @enum_sync_status_id, " +
                "dbo.CURRENT_DATETIME())";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@sync_job_id", job.Id));
                parameters.Add(new SqlParameter("@enum_sync_status_id", enumStatus.Id));

                connection.InsertNewRecord(sql, parameters);
            }
            catch (Exception e)
            {
                LogError(e, job);
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public static void LogStatus(SyncTask task, Enumeration enumStatus)
        {
            IConnectable connection = DbConnectionFactory.GetNetus2Connection();
            try
            {
                string sql = "INSERT INTO sync_task_status (" +
                "sync_task_id, enum_sync_status_id, [timestamp]" +
                ") VALUES (" +
                "@sync_task_id, @enum_sync_status_id, " +
                "dbo.CURRENT_DATETIME())";

                List<SqlParameter> paramaters = new List<SqlParameter>();
                paramaters.Add(new SqlParameter("@sync_task_id", task.Id));
                paramaters.Add(new SqlParameter("@enum_sync_status_id", enumStatus.Id));

                connection.InsertNewRecord(sql, paramaters);
            }
            catch(Exception e)
            {
                LogError(e, task, null);
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public static void LogError(Exception e, SyncJob job)
        {
            LogStatus(job, Enum_Sync_Status.values["error"]);

            IConnectable connection = DbConnectionFactory.GetNetus2Connection();
            try
            {
                string errorMessage = e.Message;
                errorMessage = e.Message.Replace("'", "''");

                string errorStackTrace = e.StackTrace;
                errorStackTrace = e.StackTrace.Replace("'", "''");

                string sql = "INSERT INTO sync_error(" +
                "sync_job_id, [message], stack_trace, [timestamp]" +
                ") VALUES (" +
                "@sync_job_id, @message, @stack_trace, dbo.CURRENT_DATETIME())";

                List<SqlParameter> paramaters = new List<SqlParameter>();

                paramaters.Add(new SqlParameter("@sync_job_id", job.Id));
                paramaters.Add(new SqlParameter("@message", errorMessage));
                paramaters.Add(new SqlParameter("@stack_trace", errorStackTrace));

                connection.InsertNewRecord(sql, paramaters);
            }
            catch(Exception cantEvenLogError)
            {
                Debug.WriteLine(cantEvenLogError.Message + "\n" + cantEvenLogError.StackTrace);
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public static void LogError(Exception e, SyncTask task, DataRow row)
        {
            LogStatus(task, Enum_Sync_Status.values["error"]);

            IConnectable connection = DbConnectionFactory.GetNetus2Connection();
            try
            {
                string errorMessage = e.Message;
                errorMessage = e.Message.Replace("\'", "\'\'");

                string errorStackTrace = e.StackTrace;
                while (e.StackTrace.IndexOf('\'') > 0)
                {
                    errorStackTrace = e.StackTrace.Insert(e.StackTrace.IndexOf('\''), "\'");
                }

                if (row != null)
                {
                    StringBuilder dataThatCausedError = new StringBuilder();
                    foreach (DataColumn col in row.Table.Columns)
                    {
                        dataThatCausedError.AppendFormat("{0},", row[col]);
                    }
                    dataThatCausedError = dataThatCausedError.Remove(dataThatCausedError.Length - 1, 1);
                    errorStackTrace += "\nData For Failed Task:\n" + dataThatCausedError.ToString() + ";";
                }

                string sql = "INSERT INTO sync_error(" +
                "sync_task_id, [message], stack_trace, [timestamp]" +
                ") VALUES (@sync_task_id, @message, @stack_trace, dbo.CURRENT_DATETIME())";

                List<SqlParameter> paramaters = new List<SqlParameter>();
                paramaters.Add(new SqlParameter("@sync_task_id", task.Id));
                paramaters.Add(new SqlParameter("@message", errorMessage));
                paramaters.Add(new SqlParameter("@stack_trace", errorStackTrace));

                connection.InsertNewRecord(sql, paramaters);
            }
            catch (Exception cantEvenLogError)
            {
                Debug.WriteLine(cantEvenLogError.Message + "\n" + cantEvenLogError.StackTrace);
            }
            finally
            {
                connection.CloseConnection();
            }
        }
    }
}
