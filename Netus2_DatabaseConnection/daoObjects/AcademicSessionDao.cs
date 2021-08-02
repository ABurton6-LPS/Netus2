using System;

namespace Netus2_DatabaseConnection.daoObjects
{
    public class AcademicSessionDao
    {
        public int? academic_session_id { get; set; }
        public string term_code { get; set; }
        public int? school_year { get; set; }
        public string name { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public int? enum_session_id { get; set; }
        public int? parent_session_id { get; set; }
        public int? organization_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
