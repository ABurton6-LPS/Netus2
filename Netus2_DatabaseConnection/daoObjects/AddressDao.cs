using System;

namespace Netus2_DatabaseConnection.daoObjects
{
    public class AddressDao
    {
        public int? address_id { get; set; }
        public int? person_id { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string address_line_3 { get; set; }
        public string address_line_4 { get; set; }
        public string apartment { get; set; }
        public string city { get; set; }
        public int? enum_state_province_id { get; set; }
        public string postal_code { get; set; }
        public int? enum_country_id { get; set; }
        public int? is_current_id { get; set; }
        public int? enum_address_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
