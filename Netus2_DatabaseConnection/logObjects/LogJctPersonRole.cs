using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogJctPersonRole
    {
        public int log_jct_person_role_id { get; set; }
        public int? person_id { get; set; }

        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration Role;
        public Enumeration LogAction;

        public void set_Role(int enumRoleId)
        {
            Role = Enum_Role.GetEnumFromId(enumRoleId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}