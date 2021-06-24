using System;

namespace Netus2.daoObjects
{
    public class ClassEnrolledDao
    {
        public int? class_id { get; set; }
        public string name { get; set; }
        public string class_code { get; set; }
        public int? enum_class_id { get; set; }
        public string room { get; set; }
        public int? course_id { get; set; }
        public int? academic_session_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
