using Netus2_DatabaseConnection.enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Enrollment
    {
        public int Id { get; set; }
        public List<ClassEnrolled> ClassesEnrolled { get; set; }
        public AcademicSession AcademicSession { get; set; }
        public Enumeration GradeLevel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPrimary { get; set; }

        public Enrollment(AcademicSession academicSession)
        {
            Id = -1;
            AcademicSession = academicSession;
            ClassesEnrolled = new List<ClassEnrolled>();
        }

        public override string ToString()
        {
            StringBuilder strEnrollment = new StringBuilder();
            strEnrollment.Append("Number of Classes Enrolled: " + ClassesEnrolled.Count + "\n");
            if(AcademicSession != null)
                strEnrollment.Append("Academic Session: " + AcademicSession.Name + "\n");
            if (GradeLevel != null)
                strEnrollment.Append("Grade Level: " + GradeLevel + "\n");
            strEnrollment.Append("Start Date: " + StartDate + "\n");
            strEnrollment.Append("End Date: " + EndDate + "\n");
            if(IsPrimary != null)
                strEnrollment.Append("Is Primary: " + IsPrimary + "\n");

            return strEnrollment.ToString();
        }
    }
}