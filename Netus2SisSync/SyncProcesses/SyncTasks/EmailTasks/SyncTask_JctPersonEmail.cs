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

namespace Netus2SisSync.SyncProcesses.SyncTasks.MarkTasks
{
    public class SyncTask_JctPersonEmail : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_JctPersonEmail(string name, SyncJob job)
            : base(name, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisUniqueId = row["unique_identifier"].ToString() == "" ? null : row["unique_identifier"].ToString();
                string sisEmailValue = row["email_value"].ToString() == "" ? null : row["email_value"].ToString();
                bool sisIsPrimary = row["is_primary"].ToString() == "" ? false : (bool)row["is_primary"];

                IPersonDao personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
                Person person = personDaoImpl.Read_UsingUniqueIdentifier(sisUniqueId, _netus2Connection);
                if(person == null)
                    throw new Exception("Person not found with UniqueId: " + sisUniqueId);

                IEmailDao emailDaoImpl = DaoImplFactory.GetEmailDaoImpl();
                Email email = new Email(sisEmailValue);
                List<Email> foundEmails = emailDaoImpl.Read(email, _netus2Connection);
                if (foundEmails.Count != 1)
                    throw new Exception(foundEmails.Count + " records found for email: " + sisEmailValue);
                else
                    email = foundEmails[0];

                IJctPersonEmailDao jctPersonEmailDaoImpl = DaoImplFactory.GetJctPersonEmailDaoImpl();
                DataRow jctPersonEmailDao = jctPersonEmailDaoImpl.Read(person.Id, email.Id, _netus2Connection);

                if (jctPersonEmailDao == null)
                {
                    jctPersonEmailDaoImpl.Write(person.Id, email.Id, sisIsPrimary, _netus2Connection);
                }
                else
                {
                    if ((bool)jctPersonEmailDao["is_primary"] != sisIsPrimary)
                    {
                        jctPersonEmailDaoImpl.Update(person.Id, email.Id, sisIsPrimary, _netus2Connection);
                    }
                }

                jctPersonEmailDaoImpl.Write_ToTempTable(person.Id, email.Id, _netus2Connection);

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