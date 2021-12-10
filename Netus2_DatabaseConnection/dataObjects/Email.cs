using Netus2_DatabaseConnection.enumerations;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Email
    {
        public int Id { get; set; }
        public string EmailValue { get; set; }
        public Enumeration EmailType { get; set; }
        public bool IsPrimary { get; set; }

        public Email(string emailValue)
        {
            Id = -1;
            EmailValue = emailValue;
        }

        public override string ToString()
        {
            StringBuilder strAddr = new StringBuilder();
            strAddr.Append("Email: " + EmailValue + "\n");
            strAddr.Append("Email Type: " + EmailType + "\n");
            strAddr.Append("Is Primary: " + IsPrimary + "\n");

            return strAddr.ToString();
        }
    }
}