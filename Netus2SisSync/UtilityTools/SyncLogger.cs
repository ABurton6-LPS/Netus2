using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2SisSync.SyncProcesses;
using System;
using System.Data;
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

                StringBuilder sql = new StringBuilder("INSERT INTO sync_job(");
                sql.Append("[name], [timestamp]");
                sql.Append(") VALUES (");
                sql.Append("'" + job.Name + "', ");
                sql.Append("dbo.CURRENT_DATETIME())");

                job.Id = connection.InsertNewRecord(sql.ToString());

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
                StringBuilder sql = new StringBuilder("INSERT INTO sync_task(");
                sql.Append("sync_job_id, [name], [timestamp]");
                sql.Append(") VALUES (");
                sql.Append(task.Job.Id + ", ");
                sql.Append("'" + task.Name + "', ");
                sql.Append("dbo.CURRENT_DATETIME())");

                task.Id = connection.InsertNewRecord(sql.ToString());

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
                StringBuilder sql = new StringBuilder("INSERT INTO sync_job_status (");
                sql.Append("sync_job_id, enum_sync_status_id, [timestamp]");
                sql.Append(") VALUES (");
                sql.Append(job.Id + ", ");
                sql.Append(enumStatus.Id + ", ");
                sql.Append("dbo.CURRENT_DATETIME())");

                connection.InsertNewRecord(sql.ToString());
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
                StringBuilder sql = new StringBuilder("INSERT INTO sync_task_status (");
                sql.Append("sync_task_id, enum_sync_status_id, [timestamp]");
                sql.Append(") VALUES (");
                sql.Append(task.Id + ", ");
                sql.Append(enumStatus.Id + ", ");
                sql.Append("dbo.CURRENT_DATETIME())");

                connection.InsertNewRecord(sql.ToString());
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

                StringBuilder sql = new StringBuilder("INSERT INTO sync_error(");
                sql.Append("sync_job_id, [message], stack_trace, [timestamp]");
                sql.Append(") VALUES (");
                sql.Append(job.Id + ", ");
                sql.Append("'" + errorMessage + "', ");
                sql.Append("'" + errorStackTrace + "', ");
                sql.Append("dbo.CURRENT_DATETIME())");

                connection.InsertNewRecord(sql.ToString());
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

                StringBuilder sql = new StringBuilder("INSERT INTO sync_error(");
                sql.Append("sync_task_id, [message], stack_trace, [timestamp]");
                sql.Append(") VALUES (");
                sql.Append(task.Id + ", ");
                sql.Append("'" + errorMessage + "', ");
                sql.Append("'" + errorStackTrace + "', ");
                sql.Append("dbo.CURRENT_DATETIME())");

                connection.InsertNewRecord(sql.ToString());
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
