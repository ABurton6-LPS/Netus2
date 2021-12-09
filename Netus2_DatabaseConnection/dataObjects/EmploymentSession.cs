using Netus2_DatabaseConnection.enumerations;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class EmploymentSession : Session
    {
        public bool IsPrimary { get; set; }

        public Enumeration SessionType;

        public EmploymentSession(bool isPrimary, Organization organization) : base(organization)
        {
            IsPrimary = isPrimary;
            SessionType = Enum_Session.values["employment"];
        }

        public override string ToString()
        {
            StringBuilder strEmpSes = new StringBuilder();
            strEmpSes.Append("Is Primary: " + IsPrimary + "\n");
            strEmpSes.Append("Session Type: " + SessionType + "\n");
            strEmpSes.Append(base.ToString());

            return strEmpSes.ToString();
        }
    }
}