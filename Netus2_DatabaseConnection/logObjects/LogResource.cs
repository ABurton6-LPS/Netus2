using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogResource
    {
        public int log_resource_id { get; set; }
        public int? resource_id { get; set; }
        public string name { get; set; }
        public string vendor_resource_identification { get; set; }
        public string vendor_identification { get; set; }
        public string application_identification { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration Importance;
        public Enumeration LogAction;

        public void set_Importance(int enumImportanceId)
        {
            Importance = Enum_Importance.GetEnumFromId(enumImportanceId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}