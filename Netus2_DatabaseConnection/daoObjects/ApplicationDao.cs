using System;

namespace Netus2.daoObjects
{
    public class ApplicationDao
    {
        public int? app_id { get; set; }
        public string name { get; set; }
        public int? provider_id { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
