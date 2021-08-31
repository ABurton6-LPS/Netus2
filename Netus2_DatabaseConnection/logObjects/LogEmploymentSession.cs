using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogEmploymentSession
    {
        public int log_employment_session_id { get; set; }
        public int employment_session_id { get; set; }
        public string name { get; set; }
        public int person_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int organization_id { get; set; }
        public DateTime created { get; set; }
        public string created_by { get; set; }
        public DateTime changed { get; set; }
        public string changed_by { get; set; }
        public DateTime log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration IsPrimary;
        public Enumeration SessionType;
        public Enumeration LogAction;

        public void set_IsPrimary(int enumIsPrimaryId)
        {
            IsPrimary = Enum_True_False.GetEnumFromId(enumIsPrimaryId);
        }

        public void set_SessionType(int enumSessionId)
        {
            SessionType = Enum_Session.GetEnumFromId(enumSessionId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}