using Netus2_DatabaseConnection.enumerations;
using System.Collections.Generic;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Enumeration OrganizationType { get; set; }
        public string Identifier { get; set; }
        public string BuildingCode { get; set; }
        public List<Organization> Children { get; set; }

        public Organization(string name, Enumeration organizationType, string identifier)
        {
            Id = -1;
            Name = name;
            OrganizationType = organizationType;
            Identifier = identifier;

            Children = new List<Organization>();
        }

        public override string ToString()
        {
            StringBuilder strOrg = new StringBuilder();
            strOrg.Append("Name: " + Name + "\n");
            strOrg.Append("Organization Type: " + OrganizationType + "\n");
            strOrg.Append("Identifier: " + Identifier + "\n");
            strOrg.Append("Building Code: " + BuildingCode + "\n");
            strOrg.Append("Number of Child Organizations: " + Children.Count + "\n");

            return strOrg.ToString();
        }
    }
}