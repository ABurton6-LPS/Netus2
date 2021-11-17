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
        public Enumeration IsPrimary { get; set; }

        public Enrollment(Enumeration gradeLevel, DateTime startDate, DateTime endDate, Enumeration isPrimary)
        {
            Id = -1;
            GradeLevel = gradeLevel;
            StartDate = startDate;
            EndDate = endDate;
            IsPrimary = isPrimary;
            ClassesEnrolled = new List<ClassEnrolled>();
        }

        public override string ToString()
        {
            StringBuilder strEnrollment = new StringBuilder();
            strEnrollment.Append("Number of Classes Enrolled: " + ClassesEnrolled.Count + "\n");
            strEnrollment.Append("Academic Sessions: " + AcademicSession.Name + "\n");
            strEnrollment.Append("Grade Level: " + GradeLevel.Netus2Code + "\n");
            strEnrollment.Append("Start Date: " + StartDate + "\n");
            strEnrollment.Append("End Date: " + EndDate + "\n");
            strEnrollment.Append("Is Primary: " + IsPrimary.Netus2Code + "\n");

            return strEnrollment.ToString();
        }
    }
}