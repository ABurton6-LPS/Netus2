﻿using Netus2.dbAccess;
using Netus2.enumerations;
using System;

namespace Netus2.logObjects
{
    public class LogPerson
    {
        public int log_person_id { get; set; }
        public int? person_id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public DateTime? birth_date { get; set; }
        public string login_name { get; set; }
        public string login_pw { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration Gender;
        public Enumeration Ethnic;
        public Enumeration ResidenceStatus;
        public Enumeration LogAction;

        public void set_Gender(int enumGenderId)
        {
            Gender = Enum_Gender.GetEnumFromId(enumGenderId);
        }

        public void set_Ethnic(int enumEthnicId)
        {
            Ethnic = Enum_Ethnic.GetEnumFromId(enumEthnicId);
        }

        public void set_ResidenceStatus(int enumResidenceStatusId)
        {
            ResidenceStatus = Enum_Residence_Status.GetEnumFromId(enumResidenceStatusId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}