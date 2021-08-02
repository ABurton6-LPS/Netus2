using System;

namespace Netus2_DatabaseConnection.daoObjects
{
    public class OrganizationDao
    {
        public int? organization_id { get; set; }
        public string name { get; set; }
        public int? enum_organization_id { get; set; }
        public string identifier { get; set; }
        public string building_code { get; set; }
        public int? organization_parent_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
