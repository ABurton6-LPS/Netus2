using System;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Organization Organization { get; set; }

        public Session(Organization organization)
        {
            Id = -1;
            StartDate = new DateTime();
            EndDate = new DateTime();
            Organization = organization;
        }

        public override string ToString()
        {
            StringBuilder strSes = new StringBuilder();
            strSes.Append("Name: " + Name + "\n");
            strSes.Append("Start Date: " + StartDate + "\n");
            strSes.Append("End Date: " + EndDate + "\n");
            strSes.Append("Organization: " + Organization.Name);

            return strSes.ToString();
        }
    }
}