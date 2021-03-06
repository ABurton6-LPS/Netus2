using Netus2_DatabaseConnection.enumerations;
using System.Collections.Generic;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class AcademicSession : Session
    {
        public string TermCode { get; set; }
        public string TrackCode { get; set; }
        public int SchoolYear { get; set; }
        public string Name { get; set; }
        public Enumeration SessionType { get; set; }
        public List<AcademicSession> Children { get; set; }
        public AcademicSession(Enumeration sessionType, Organization organization, string termCode) : base(organization)
        {
            SessionType = sessionType;
            Children = new List<AcademicSession>();
            TermCode = termCode;
        }

        public override string ToString()
        {
            StringBuilder strAcademicSession = new StringBuilder();
            strAcademicSession.Append("Name: " + Name + "\n");
            strAcademicSession.Append("Term Code: " + TermCode + "\n");
            strAcademicSession.Append("Track Code: " + TrackCode + "\n");
            strAcademicSession.Append("School Year: " + SchoolYear + "\n");
            strAcademicSession.Append("Session Type: " + SessionType + "\n");
            strAcademicSession.Append("Number of Children: " + Children.Count + "\n");
            strAcademicSession.Append(base.ToString());

            return strAcademicSession.ToString();
        }
    }
}