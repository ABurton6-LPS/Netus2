using System;

namespace Netus2_DatabaseConnection.daoObjects
{
    public class EnrollmentDao
    {
        public int? enrollment_id { get; set; }
        public int? person_id { get; set; }
        public int? class_id { get; set; }
        public int? enum_grade_id { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public int? is_primary_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
