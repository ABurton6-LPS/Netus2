using System;

namespace Netus2_DatabaseConnection.daoObjects
{
    public class MarkDao
    {
        public int? mark_id { get; set; }
        public int? lineitem_id { get; set; }
        public int? person_id { get; set; }
        public int? enum_score_status_id { get; set; }
        public double? score { get; set; }
        public DateTime? score_date { get; set; }
        public string comment { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
