using System;

namespace Netus2_DatabaseConnection.daoObjects
{
    public class CourseDao
    {
        public int? course_id { get; set; }
        public string name { get; set; }
        public string course_code { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
