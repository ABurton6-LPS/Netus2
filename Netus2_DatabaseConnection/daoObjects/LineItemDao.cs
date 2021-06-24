using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netus2.daoObjects
{
    public class LineItemDao
    {
        public int? lineitem_id { get; set; }
        public string name { get; set; }
        public string descript { get; set; }
        public DateTime? assign_date { get; set; }
        public DateTime? due_date { get; set; }
        public int? class_id { get; set; }
        public int? enum_category_id { get; set; }
        public double? markValueMin { get; set; }
        public double? markValueMax { get; set; }
        public DateTime? created { get; set; }
        public string created_by { get; set; }
        public DateTime? changed { get; set; }
        public string changed_by { get; set; }
    }
}
