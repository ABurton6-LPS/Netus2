using Netus2_DatabaseConnection.enumerations;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string PhoneNumberValue { get; set; }
        public Enumeration IsPrimary { get; set; }
        public Enumeration PhoneType { get; set; }

        public PhoneNumber(string phoneNumberValue, Enumeration isPrimary, Enumeration phoneType)
        {
            Id = -1;
            PhoneNumberValue = phoneNumberValue;
            IsPrimary = isPrimary;
            PhoneType = phoneType;
        }

        public override string ToString()
        {
            StringBuilder strPhoneNumber = new StringBuilder();
            strPhoneNumber.Append("Phone Number: " + PhoneNumberValue + "\n");
            strPhoneNumber.Append("Is Primary: " + IsPrimary.Netus2Code + "\n");
            strPhoneNumber.Append("Phone Type: " + PhoneType.Netus2Code);

            return strPhoneNumber.ToString();
        }
    }
}