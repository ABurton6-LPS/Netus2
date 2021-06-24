using Netus2.dbAccess;
using Netus2.enumerations;
using System;

namespace Netus2.logObjects
{
    public class LogLineItem
    {
        public int log_lineitem_id { get; set; }
        public int? lineitem_id { get; set; }
        public string name { get; set; }
        public string descript { get; set; }
        public DateTime? assign_date { get; set; }
        public DateTime? due_date { get; set; }
        public int? class_id { get; set; }
        public double? markValueMin { get; set; }
        public double? markValueMax { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
        public DateTime? log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration Category;
        public Enumeration LogAction;

        public void set_Category(int enumCategoryId)
        {
            Category = Enum_Category.GetEnumFromId(enumCategoryId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}