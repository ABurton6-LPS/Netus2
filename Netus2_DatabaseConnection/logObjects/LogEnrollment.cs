﻿using Netus2.dbAccess;
using Netus2.enumerations;
using System;

namespace Netus2.logObjects
{
    public class LogEnrollment
    {
        public int log_enrollment_id { get; set; }
        public int? enrollment_id { get; set; }
        public int? person_id { get; set; }
        public int? class_id { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration GradeLevel;
        public Enumeration IsPrimary;
        public Enumeration LogAction;

        public void set_IsPrimary(int enumIsPrimaryId)
        {
            IsPrimary = Enum_True_False.GetEnumFromId(enumIsPrimaryId);
        }

        public void set_GradeLevel(int enumGradeLevelId)
        {
            GradeLevel = Enum_Grade.GetEnumFromId(enumGradeLevelId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}