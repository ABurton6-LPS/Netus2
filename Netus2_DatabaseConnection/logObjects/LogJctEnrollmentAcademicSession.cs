using Netus2.dbAccess;
using Netus2.enumerations;
using System;

namespace Netus2.logObjects
{
    public class LogJctEnrollmentAcademicSession
    {
        public int log_jct_enrollment_academic_session_id { get; set; }
        public int? enrollment_id { get; set; }
        public int? academic_session_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration LogAction;

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}