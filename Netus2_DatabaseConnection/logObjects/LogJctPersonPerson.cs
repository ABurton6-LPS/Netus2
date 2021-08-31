using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogJctPersonPerson
    {
        public int log_jct_person_person_id { get; set; }
        public int person_one_id { get; set; }
        public int person_two_id { get; set; }

        public DateTime log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration LogAction;

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}