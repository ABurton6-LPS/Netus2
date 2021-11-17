using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogProvider
    {
        public int log_provider_id { get; set; }
        public int provider_id { get; set; }
        public string name { get; set; }
        public string url_standard_access { get; set; }
        public string url_admin_access { get; set; }
        public string populated_by { get; set; }
        public int parent_provider_id { get; set; }
        public DateTime created { get; set; }
        public string created_by { get; set; }
        public DateTime changed { get; set; }
        public string changed_by { get; set; }
        public DateTime log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration LogAction;

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}