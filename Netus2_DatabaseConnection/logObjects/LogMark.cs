using Netus2.dbAccess;
using Netus2.enumerations;
using System;

namespace Netus2.logObjects
{
    public class LogMark
    {
        public int log_mark_id { get; set; }
        public int? mark_id { get; set; }
        public int? lineitem_id { get; set; }
        public int? person_id { get; set; }
        public double? score { get; set; }
        public DateTime? score_date { get; set; }
        public string comment { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration ScoreStatus;
        public Enumeration LogAction;

        public void set_ScoreStatus(int enumScoreStatusId)
        {
            ScoreStatus = Enum_Score_Status.GetEnumFromId(enumScoreStatusId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}