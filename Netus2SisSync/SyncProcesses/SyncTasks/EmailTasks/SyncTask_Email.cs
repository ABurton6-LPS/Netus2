using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Netus2SisSync.SyncProcesses.SyncTasks.EnrollmentTasks
{
    public class SyncTask_Email : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_Email(string name, SyncJob job)
            : base(name, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisEmailValue = row["email_value"].ToString() == "" ? null : row["email_value"].ToString();
                Enumeration sisEmailType = row["enum_email_id"].ToString() == "" ? null : Enum_Email.GetEnumFromSisCode(row["enum_email_id"].ToString());

                IEmailDao emailDaoImpl = DaoImplFactory.GetEmailDaoImpl();
                Email email = new Email(sisEmailValue);
                List<Email> foundEmails = emailDaoImpl.Read(email, _netus2Connection);

                email.EmailType = sisEmailType;

                if (foundEmails.Count == 0)
                {
                    emailDaoImpl.Write(email, _netus2Connection);
                }
                else if (foundEmails.Count == 1)
                {
                    if (foundEmails[0].EmailType != sisEmailType)
                    {
                        emailDaoImpl.Update(email, _netus2Connection);
                    }                        
                }                    
                else
                {
                    throw new Exception(foundEmails.Count + " records found matching Email: " + email.ToString());
                }

                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"]);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                SyncLogger.LogError(e, this, row);
            }
            finally
            {
                _netus2Connection.CloseConnection();
                latch.Signal();
            }
        }
    }
}