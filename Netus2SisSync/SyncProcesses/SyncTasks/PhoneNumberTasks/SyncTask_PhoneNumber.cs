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

namespace Netus2SisSync.SyncProcesses.SyncTasks.PhoneNumberTasks
{
    public class SyncTask_PhoneNumber : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_PhoneNumber(string name, SyncJob job)
            : base(name, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisPhoneNumber = row["phone_number"].ToString() == "" ? null : row["phone_number"].ToString();
                Enumeration sisPhoneType = row["phone_type_code"].ToString() == "" ? null : Enum_Phone.GetEnumFromSisCode(row["phone_type_code"].ToString().ToLower());

                PhoneNumber phoneNumber = new PhoneNumber(sisPhoneNumber, sisPhoneType);

                IPhoneNumberDao phoneNumberDaoImpl = DaoImplFactory.GetPhoneNumberDaoImpl();
                phoneNumberDaoImpl.SetTaskId(this.Id);
                List<PhoneNumber> foundPhoneNumbers = phoneNumberDaoImpl.Read(phoneNumber, _netus2Connection);

                if (foundPhoneNumbers.Count == 0)
                    phoneNumberDaoImpl.Write(phoneNumber, _netus2Connection);
                else if (foundPhoneNumbers.Count == 1)
                {
                    phoneNumber.Id = foundPhoneNumbers[0].Id;

                    if ((phoneNumber.PhoneNumberValue != foundPhoneNumbers[0].PhoneNumberValue) ||
                        (phoneNumber.PhoneType != foundPhoneNumbers[0].PhoneType))
                        phoneNumberDaoImpl.Update(phoneNumber, _netus2Connection);
                }
                else
                    throw new Exception(foundPhoneNumbers.Count + " Phone Numbers found matching: " + phoneNumber.ToString());

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