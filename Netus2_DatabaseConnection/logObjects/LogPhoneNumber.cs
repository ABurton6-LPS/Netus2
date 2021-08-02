using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogPhoneNumber
    {
        public int log_phone_number_id { get; set; }
        public int? phone_number_id { get; set; }
        public int? person_id { get; set; }
        public string phone_number { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration IsPrimary;
        public Enumeration PhoneType;
        public Enumeration LogAction;

        public void set_IsPrimary(int enumIsPrimaryId)
        {
            IsPrimary = Enum_True_False.GetEnumFromId(enumIsPrimaryId);
        }

        public void set_PhoneType(int enumPhoneId)
        {
            PhoneType = Enum_Phone.GetEnumFromId(enumPhoneId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}