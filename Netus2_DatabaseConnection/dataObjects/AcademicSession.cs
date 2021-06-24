using Netus2.enumerations;
using System.Collections.Generic;
using System.Text;

namespace Netus2
{
    public class AcademicSession : Session
    {
        public Enumeration SessionType { get; set; }
        public List<AcademicSession> Children { get; set; }
        public AcademicSession(string name, Enumeration sessionType, Organization organization) : base(name, organization)
        {
            SessionType = sessionType;
            Children = new List<AcademicSession>();
        }

        public override string ToString()
        {
            StringBuilder strAcademicSession = new StringBuilder();
            strAcademicSession.Append("Session Type: " + SessionType + "\n");
            strAcademicSession.Append("Children: " + Children.Count + "\n");

            return strAcademicSession.ToString();
        }
    }
}