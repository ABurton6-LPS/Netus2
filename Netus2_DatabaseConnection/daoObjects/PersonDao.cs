using System;
using System.Collections.Generic;

namespace Netus2.daoObjects
{
    public class PersonDao
    {
        public int? person_id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public DateTime? birth_date { get; set; }
        public int? enum_gender_id { get; set; }
        public int? enum_ethnic_id { get; set; }
        public int? enum_residence_status_id { get; set; }
        public string login_name { get; set; }
        public string login_pw { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
