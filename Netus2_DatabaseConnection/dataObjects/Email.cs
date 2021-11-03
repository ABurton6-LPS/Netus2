using Netus2_DatabaseConnection.enumerations;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Email
    {
        public int Id { get; set; }
        public string EmailValue { get; set; }
        public Enumeration EmailType { get; set; }
        public Enumeration IsPrimary { get; set; }

        public Email(string emailValue, Enumeration emailType)
        {
            Id = -1;
            EmailValue = emailValue;
            EmailType = emailType;
            IsPrimary = Enum_True_False.values["unset"];
        }

        public override string ToString()
        {
            StringBuilder strAddr = new StringBuilder();
            strAddr.Append("Email: " + EmailValue + "\n");
            strAddr.Append("Email Type: " + EmailType.Netus2Code + "\n");
            strAddr.Append("Is Primary: " + IsPrimary.Netus2Code + "\n");

            return strAddr.ToString();
        }
    }
}