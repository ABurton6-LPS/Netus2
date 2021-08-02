using Netus2_DatabaseConnection.enumerations;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Enumeration Importance { get; set; }
        public string VendorResourceId { get; set; }
        public string VendorId { get; set; }
        public string ApplicationId { get; set; }

        public Resource(string name, string vendorResourceId)
        {
            Id = -1;
            Name = name;
            VendorResourceId = vendorResourceId;
        }

        public override string ToString()
        {
            StringBuilder strResource = new StringBuilder();
            strResource.Append("Name: " + Name + "\n");
            strResource.Append("Importance: " + Importance + "\n");
            strResource.Append("Vendor Resource Id: " + VendorResourceId + "\n");
            strResource.Append("Vendor Id: " + VendorId + "\n");
            strResource.Append("Application Id: " + ApplicationId + "\n");

            return strResource.ToString();
        }
    }
}