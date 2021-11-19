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

namespace Netus2SisSync.SyncProcesses.SyncTasks.AddressTasks
{
    public class SyncTask_JctPersonAddress : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_JctPersonAddress(string name, SyncJob job)
            : base(name, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisAddressLine1 = row["address_line_1"].ToString() == "" ? null : row["address_line_1"].ToString();
                string sisAddressLine2 = row["address_line_2"].ToString() == "" ? null : row["address_line_2"].ToString();
                string sisCity = row["city"].ToString() == "" ? null : row["city"].ToString();
                string sisPostalCode = row["postal_code"].ToString() == "" ? null : row["postal_code"].ToString();
                string sisPersonId = row["sis_person_id"].ToString() == "" ? null : row["sis_person_id"].ToString();
                Enumeration sisAddressType = row["enum_address_id"].ToString() == "" ? null : Enum_Address.GetEnumFromSisCode(row["enum_address_id"].ToString().ToLower());
                Enumeration sisIsPrimary = null;
                if (sisAddressType == Enum_Address.values["home"])
                    sisIsPrimary = Enum_True_False.values["true"];
                else
                    sisIsPrimary = Enum_True_False.values["false"];

                IPersonDao personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
                personDaoImpl.SetTaskId(this.Id);
                Person person = personDaoImpl.Read_UsingUniqueIdentifier(sisPersonId, _netus2Connection);

                if (person == null || person.Id <= 0)
                    throw new Exception("Person with unique id: " + sisPersonId + " doesn't exist within the Netus2 Database");

                Address address = new Address(sisAddressLine1, sisAddressLine2, sisCity, 
                    Enum_State_Province.values["mi"], sisPostalCode);
                IAddressDao addressDaoImpl = DaoImplFactory.GetAddressDaoImpl();
                addressDaoImpl.SetTaskId(this.Id);
                List<Address> foundAddresses = addressDaoImpl.Read(address, _netus2Connection);

                IJctPersonAddressDao jctPersonAddressDaoImpl = DaoImplFactory.GetJctPersonAddressDaoImpl();

                if (foundAddresses.Count == 1)
                {
                    address.Id = foundAddresses[0].Id;

                    DataRow foundJctPersonAddressDao = jctPersonAddressDaoImpl.Read(person.Id, address.Id, _netus2Connection);

                    if (foundJctPersonAddressDao == null)
                        jctPersonAddressDaoImpl.Write(person.Id, address.Id, sisIsPrimary.Id, sisAddressType.Id, _netus2Connection);
                    else
                        if (((int)foundJctPersonAddressDao["is_primary_id"] != sisIsPrimary.Id) ||
                            ((int)foundJctPersonAddressDao["enum_address_id"] != sisAddressType.Id))
                            jctPersonAddressDaoImpl.Update(person.Id, address.Id, sisIsPrimary.Id, sisAddressType.Id, _netus2Connection);

                    jctPersonAddressDaoImpl.Write_ToTempTable(person.Id, address.Id, _netus2Connection);
                }
                else
                    throw new Exception(foundAddresses.Count + " record(s) found matching Address:\n" + address.ToString());

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