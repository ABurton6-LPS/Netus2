using Netus2.dbAccess;
using Netus2.enumerations;
using System;

namespace Netus2.logObjects
{
    public class LogJctClassPeriod
    {
        public int log_jct_class_period_id { get; set; }
        public int? class_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration Period;
        public Enumeration LogAction;

        public void set_Period(int enumPeriodId)
        {
            Period = Enum_Period.GetEnumFromId(enumPeriodId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}