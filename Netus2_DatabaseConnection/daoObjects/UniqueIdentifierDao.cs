using System;

namespace Netus2.daoObjects
{
    public class UniqueIdentifierDao
    {
        public int? unique_identifier_id { get; set; }
        public int? person_id { get; set; }
        public string unique_identifier { get; set; }
        public int? enum_identifier_id { get; set; }
        public int? is_active_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
