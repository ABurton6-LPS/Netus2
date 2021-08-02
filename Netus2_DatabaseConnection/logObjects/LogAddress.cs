using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogAddress
    {
        public int log_address_id { get; set; }
        public int? address_id { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string address_line_3 { get; set; }
        public string address_line_4 { get; set; }
        public string apartment { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration AddressType;
        public Enumeration IsCurrent;
        public Enumeration Country;
        public Enumeration StateProvince;
        public Enumeration LogAction;

        public void set_AddressType(int enumAddressType)
        {
            AddressType = Enum_Address.GetEnumFromId(enumAddressType);
        }

        public void set_IsCurrent(int enumIsCurrentId)
        {
            IsCurrent = Enum_True_False.GetEnumFromId(enumIsCurrentId);
        }

        public void set_Country(int enumCountryId)
        {
            Country = Enum_Country.GetEnumFromId(enumCountryId);
        }

        public void set_StateProvince(int enumStateProvinceId)
        {
            StateProvince = Enum_State_Province.GetEnumFromId(enumStateProvinceId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}