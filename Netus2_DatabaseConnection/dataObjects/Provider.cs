using System.Collections.Generic;
using System.Text;

namespace Netus2
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlStandardAccess { get; set; }
        public string UrlAdminAccess { get; set; }
        public string PopulatedBy { get; set; }
        public List<Provider> Children { get; set; }

        public Provider(string name)
        {
            Id = -1;
            Name = name;

            Children = new List<Provider>();
        }

        public override string ToString()
        {
            StringBuilder strProvider = new StringBuilder();
            strProvider.Append("Name: " + Name + "\n");
            strProvider.Append("Standard URL: " + UrlStandardAccess + "\n");
            strProvider.Append("Admin URL: " + UrlAdminAccess + "\n");
            strProvider.Append("Populated By: " + PopulatedBy + "\n");
            strProvider.Append("Number of Child Providers: " + Children.Count + "\n");

            return strProvider.ToString();
        }
    }
}