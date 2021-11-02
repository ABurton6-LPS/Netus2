using Netus2_DatabaseConnection.enumerations;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Address
    {
        public int Id { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
        public string Apartment { get; set; }
        public string City { get; set; }
        public Enumeration StateProvince { get; set; }
        public string PostalCode { get; set; }
        public Enumeration Country { get; set; }
        public Enumeration IsPrimary { get; set; }
        public Enumeration AddressType { get; set; }

        public Address(string line1, string city, Enumeration stateProvince, Enumeration country)
        {
            Id = -1;
            Line1 = line1;
            City = city;
            StateProvince = stateProvince;
            Country = country;
            IsPrimary = Enum_True_False.values["unset"];
        }

        public override string ToString()
        {
            StringBuilder strAddr = new StringBuilder();
            strAddr.Append("Line 1: " + Line1 + "\n");
            strAddr.Append("Line 2: " + Line2 + "\n");
            strAddr.Append("Line 3: " + Line3 + "\n");
            strAddr.Append("Line 4: " + Line4 + "\n");
            strAddr.Append("Apartment: " + Apartment + "\n");
            strAddr.Append("City: " + City + "\n");
            strAddr.Append("State/Province: " + StateProvince + "\n");
            strAddr.Append("Postal Code: " + PostalCode + "\n");
            strAddr.Append("Country: " + Country + "\n");
            strAddr.Append("Is Current: " + IsPrimary + "\n");
            strAddr.Append("Address Type: " + AddressType + "\n");

            return strAddr.ToString();
        }
    }
}