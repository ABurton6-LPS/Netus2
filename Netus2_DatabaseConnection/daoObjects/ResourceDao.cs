using System;

namespace Netus2_DatabaseConnection.daoObjects
{
    public class ResourceDao
    {
        public int? resource_id { get; set; }
        public string name { get; set; }
        public int? enum_importance_id { get; set; }
        public string vendor_resource_identification { get; set; }
        public string vendor_identification { get; set; }
        public string application_identification { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
