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
    public class SyncTask_Address : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_Address(string name, SyncJob job)
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
                string sisAddressLine3 = row["address_line_3"].ToString() == "" ? null : row["address_line_3"].ToString();
                string sisAddressLine4 = row["address_line_4"].ToString() == "" ? null : row["address_line_4"].ToString();
                string sisApartment = row["apartment"].ToString() == "" ? null : row["apartment"].ToString();
                string sisCity = row["city"].ToString() == "" ? null : row["city"].ToString();
                Enumeration sisStateProvince = Enum_State_Province.values[row["enum_state_province_id"].ToString().ToLower()];
                string sisPostalCode = row["postal_code"].ToString() == "" ? null : row["postal_code"].ToString();
                Enumeration sisCountry = Enum_Country.values[row["enum_country_id"].ToString().ToLower()];
                Enumeration sisIsPrimary = Enum_True_False.values[row["is_primary_id"].ToString().ToLower()];
                Enumeration sisAddressType = Enum_Address.values[row["enum_address_id"].ToString().ToLower()];
                string sisSuniq = row["suniq"].ToString();

                IPersonDao personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
                personDaoImpl.SetTaskId(this.Id);
                Person person = personDaoImpl.Read_UsingUniqueIdentifier(sisSuniq, _netus2Connection);

                if (person == null)
                    throw new Exception("Person with unique id of: " + sisSuniq + " does not exist within the Netus2 Database.");

                Address address = new Address(sisAddressLine1, sisCity, sisStateProvince, sisCountry);
                address.Line2 = sisAddressLine2;
                address.Line3 = sisAddressLine3;
                address.Line4 = sisAddressLine4;
                address.Apartment = sisApartment;
                address.PostalCode = sisPostalCode;

                IAddressDao addressDaoImpl = DaoImplFactory.GetAddressDaoImpl();
                addressDaoImpl.SetTaskId(this.Id);
                List<Address> foundAddresses = addressDaoImpl.Read(address, _netus2Connection);

                address.IsPrimary = sisIsPrimary;
                address.AddressType = sisAddressType;

                IJctPersonAddressDao jctPersonAddressDaoImpl = new JctPersonAddressDaoImpl();

                if (foundAddresses.Count == 0)
                {
                    address = addressDaoImpl.Write(address, _netus2Connection);
                    jctPersonAddressDaoImpl.Write(person.Id, address.Id, address.IsPrimary.Id, _netus2Connection);
                    jctPersonAddressDaoImpl.Write_ToTempTable(person.Id, address.Id, _netus2Connection);
                }
                else if(foundAddresses.Count == 1)
                {
                    address.Id = foundAddresses[0].Id;

                    bool personHasAddress = false;
                    foreach(Address addressLinkedToPerson in person.GetAddresses())
                        if (addressLinkedToPerson.Id == address.Id)
                            personHasAddress = true;

                    if (personHasAddress == false)
                    {
                        jctPersonAddressDaoImpl.Write(person.Id, address.Id, address.IsPrimary.Id, _netus2Connection);
                        jctPersonAddressDaoImpl.Write_ToTempTable(person.Id, address.Id, _netus2Connection);
                    }

                    if ((address.Line1 != foundAddresses[0].Line1) ||
                        (address.Line2 != foundAddresses[0].Line2) ||
                        (address.Line3 != foundAddresses[0].Line3) ||
                        (address.Line4 != foundAddresses[0].Line4) ||
                        (address.Apartment != foundAddresses[0].Apartment) ||
                        (address.City != foundAddresses[0].City) ||
                        (address.StateProvince != foundAddresses[0].StateProvince) ||
                        (address.PostalCode != foundAddresses[0].PostalCode) ||
                        (address.Country != foundAddresses[0].Country) ||
                        (address.IsPrimary != foundAddresses[0].IsPrimary) ||
                        (address.AddressType != foundAddresses[0].AddressType))
                    {
                        addressDaoImpl.Update(address, _netus2Connection);
                    }
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