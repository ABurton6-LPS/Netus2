using Netus2_DatabaseConnection.enumerations;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class EmploymentSession : Session
    {
        public Enumeration IsPrimary { get; set; }

        public Enumeration SessionType;

        public EmploymentSession(string name, Enumeration isPrimary, Organization organization) : base(name, organization)
        {
            IsPrimary = isPrimary;
            SessionType = Enum_Session.values["employment"];
        }

        public override string ToString()
        {
            StringBuilder strEmpSes = new StringBuilder();
            strEmpSes.Append("Is Primary: " + IsPrimary.Netus2Code + "\n");
            strEmpSes.Append("Session Type: " + SessionType.Netus2Code + "\n");
            strEmpSes.Append(base.ToString());

            return strEmpSes.ToString();
        }
    }
}