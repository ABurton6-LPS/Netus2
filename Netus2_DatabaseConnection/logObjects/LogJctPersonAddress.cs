﻿using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogJctPersonAddress
    {
        public int log_jct_person_address_id { get; set; }
        public int person_id { get; set; }
        public int address_id { get; set; }
        public DateTime created { get; set; }
        public string created_by { get; set; }
        public DateTime changed { get; set; }
        public string changed_by { get; set; }
        public DateTime log_date { get; set; }
        public string log_user { get; set; }
        public bool IsPrimary { get; set; }

        public Enumeration LogAction;
        public Enumeration AddressType;

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }

        public void set_AddressType(int enumAddressId)
        {
            AddressType = Enum_Address.GetEnumFromId(enumAddressId);
        }
    }
}