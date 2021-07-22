using Netus2.enumerations;
using System.Collections.Generic;
using System.Text;

namespace Netus2
{
    public class AcademicSession : Session
    {
        public string TermCode { get; set; }
        public int SchoolYear { get; set; }
        public Enumeration SessionType { get; set; }
        public List<AcademicSession> Children { get; set; }
        public AcademicSession(string name, Enumeration sessionType, Organization organization, string termCode) : base(name, organization)
        {
            SessionType = sessionType;
            Children = new List<AcademicSession>();
            TermCode = termCode;
        }

        public override string ToString()
        {
            StringBuilder strAcademicSession = new StringBuilder();
            strAcademicSession.Append("Term Code: " + TermCode + "\n");
            strAcademicSession.Append("School Year: " + SchoolYear + "\n");
            strAcademicSession.Append("Session Type: " + SessionType + "\n");
            strAcademicSession.Append("Children: " + Children.Count + "\n");

            return strAcademicSession.ToString();
        }
    }
}