using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Netus2SisSync.SyncProcesses.SyncTasks.PhoneNumberTasks
{
    public class SyncTask_JctPersonPhoneNumber : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_JctPersonPhoneNumber(string name, SyncJob job)
            : base(name, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisPhoneNubmer = row["phone_number"].ToString() == "" ? null : row["phone_number"].ToString();
                string sisPersonId = row["unique_id"].ToString() == "" ? null : row["unique_id"].ToString();
                bool sisIsPrimary = row["is_primary"].ToString() == "" ? false : (bool)row["is_primary"];

                IPersonDao personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
                personDaoImpl.SetTaskId(this.Id);
                Person person = personDaoImpl.Read_UsingUniqueIdentifier(sisPersonId, _netus2Connection);

                if (person == null)
                    throw new Exception("Person not found with UniqueId: " + sisPersonId);

                IPhoneNumberDao phoneNumberDaoImpl = DaoImplFactory.GetPhoneNumberDaoImpl();
                phoneNumberDaoImpl.SetTaskId(this.Id);
                PhoneNumber phoneNumber = phoneNumberDaoImpl.Read_WithPhoneNumberValue(sisPhoneNubmer, _netus2Connection);

                if (phoneNumber == null)
                    throw new Exception("Phone Number not found with Value: " + sisPhoneNubmer);

                IJctPersonPhoneNumberDao jctPersonPhoneNumberDaoImpl = DaoImplFactory.GetJctPersonPhoneNumberDaoImpl();
                DataRow foundJctPersonPhoneNumberDao = jctPersonPhoneNumberDaoImpl.Read(person.Id, phoneNumber.Id, _netus2Connection);

                if (foundJctPersonPhoneNumberDao == null)
                    jctPersonPhoneNumberDaoImpl.Write(person.Id, phoneNumber.Id, sisIsPrimary, _netus2Connection);
                else
                    if ((bool)foundJctPersonPhoneNumberDao["is_primary"] != sisIsPrimary)
                        jctPersonPhoneNumberDaoImpl.Update(person.Id, phoneNumber.Id, sisIsPrimary, _netus2Connection);

                jctPersonPhoneNumberDaoImpl.Write_ToTempTable(person.Id, phoneNumber.Id, _netus2Connection);

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