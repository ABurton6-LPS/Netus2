using System;

namespace Netus2_DatabaseConnection.daoObjects
{
    public class PhoneNumberDao
    {
        public int? phone_number_id { get; set; }
        public int? person_id { get; set; }
        public string phone_number { get; set; }
        public int? is_primary_id { get; set; }
        public int? enum_phone_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
