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
                Enumeration sisStateProvince = row["enum_state_province_id"].ToString() == "" ? null : Enum_State_Province.GetEnumFromSisCode(row["enum_state_province_id"].ToString().ToLower());
                string sisPostalCode = row["postal_code"].ToString() == "" ? null : row["postal_code"].ToString();
                Enumeration sisCountry = row["enum_country_id"].ToString() == "" ? null : Enum_Country.GetEnumFromSisCode(row["enum_country_id"].ToString().ToLower());

                Address address = new Address(sisAddressLine1, sisAddressLine2, sisCity, sisStateProvince, sisPostalCode);
                address.Line3 = sisAddressLine3;
                address.Line4 = sisAddressLine4;
                address.Apartment = sisApartment;
                address.Country = sisCountry;

                IAddressDao addressDaoImpl = DaoImplFactory.GetAddressDaoImpl();
                addressDaoImpl.SetTaskId(this.Id);
                List<Address> foundAddresses = addressDaoImpl.Read(address, _netus2Connection);

                if (foundAddresses.Count == 0)
                {
                    address = addressDaoImpl.Write(address, _netus2Connection);
                }
                else if(foundAddresses.Count == 1)
                {
                    address.Id = foundAddresses[0].Id;
                    
                    if ((address.Line1 != foundAddresses[0].Line1) ||
                        (address.Line2 != foundAddresses[0].Line2) ||
                        (address.Line3 != foundAddresses[0].Line3) ||
                        (address.Line4 != foundAddresses[0].Line4) ||
                        (address.Apartment != foundAddresses[0].Apartment) ||
                        (address.City != foundAddresses[0].City) ||
                        (address.StateProvince != foundAddresses[0].StateProvince) ||
                        (address.PostalCode != foundAddresses[0].PostalCode) ||
                        (address.Country != foundAddresses[0].Country))
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