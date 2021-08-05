using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogAcademicSession
    {
        public int log_academic_session_id { get; set; }
        public int? academic_session_id { get; set; }
        public string name { get; set; }
        public string term_code { get; set; }
        public int? school_year { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public int? parent_session_id { get; set; }
        public int? organization_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration SessionType;
        public Enumeration LogAction;

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