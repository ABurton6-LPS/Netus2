using Netus2_DatabaseConnection.enumerations;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string PhoneNumberValue { get; set; }
        public bool IsPrimary { get; set; }
        public Enumeration PhoneType { get; set; }

        public PhoneNumber(string phoneNumberValue, Enumeration phoneType)
        {
            Id = -1;
            PhoneNumberValue = phoneNumberValue;
            PhoneType = phoneType;
        }

        public override string ToString()
        {
            StringBuilder strPhoneNumber = new StringBuilder();
            strPhoneNumber.Append("Phone Number: " + PhoneNumberValue + "\n");
            strPhoneNumber.Append("Is Primary: " + IsPrimary + "\n");
            strPhoneNumber.Append("Phone Type: " + PhoneType.Netus2Code);

            return strPhoneNumber.ToString();
        }
    }
}