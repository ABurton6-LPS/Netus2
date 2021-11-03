using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogEmail
    {
        public int log_email_id { get; set; }
        public int email_id { get; set; }
        public string email { get; set; }
        public DateTime created { get; set; }
        public string created_by { get; set; }
        public DateTime changed { get; set; }
        public string changed_by { get; set; }
        public DateTime log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration EmailType;
        public Enumeration LogAction;

        public void set_EmailType(int enumEmailTypeId)
        {
            EmailType = Enum_Address.GetEnumFromId(enumEmailTypeId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}