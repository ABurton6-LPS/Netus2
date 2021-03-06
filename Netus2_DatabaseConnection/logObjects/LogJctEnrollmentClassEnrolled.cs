using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogJctEnrollmentClassEnrolled
    {
        public int log_jct_enrollment_class_enrolled_id { get; set; }
        public int enrollment_id { get; set; }
        public int class_enrolled_id { get; set; }
        public DateTime? enrollment_start_date { get; set; }
        public DateTime? enrollment_end_date { get; set; }
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