using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogClass
    {
        public int log_class_id { get; set; }
        public int? class_id { get; set; }
        public string name { get; set; }
        public string class_code { get; set; }
        public string room { get; set; }
        public int? course_id { get; set; }
        public int? academic_session_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration ClassType;
        public Enumeration LogAction;

        public void set_ClassType(int enumClassTypeId)
        {
            ClassType = Enum_Class.GetEnumFromId(enumClassTypeId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}