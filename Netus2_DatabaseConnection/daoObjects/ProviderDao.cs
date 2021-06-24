using System;

namespace Netus2.daoObjects
{
    public class ProviderDao
    {
        public int? provider_id { get; set; }
        public string name { get; set; }
        public string url_standard_access { get; set; }
        public string url_admin_access { get; set; }
        public string populated_by { get; set; }
        public int? parent_provider_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
