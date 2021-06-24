using Netus2.enumerations;
using System.Text;

namespace Netus2
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
        public Enumeration IsCurrent { get; set; }
        public Enumeration AddressType { get; set; }

        public Address(string line1, string city, Enumeration stateProvince, Enumeration country, Enumeration isCurrent, Enumeration addressType)
        {
            Id = -1;
            Line1 = line1;
            City = city;
            StateProvince = stateProvince;
            Country = country;
            IsCurrent = isCurrent;
            AddressType = addressType;
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
            strAddr.Append("Is Current: " + IsCurrent + "\n");
            strAddr.Append("Address Type: " + AddressType + "\n");

            return strAddr.ToString();
        }
    }
}