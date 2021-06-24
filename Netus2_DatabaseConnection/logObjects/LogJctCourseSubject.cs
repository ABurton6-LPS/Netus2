using Netus2.dbAccess;
using Netus2.enumerations;
using System;

namespace Netus2.logObjects
{
    public class LogJctCourseSubject
    {
        public int log_jct_course_subject_id { get; set; }
        public int? course_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration Subject;
        public Enumeration LogAction;

        public void set_Subject(int enumSubjectId)
        {
            Subject = Enum_Subject.GetEnumFromId(enumSubjectId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}