using Netus2_DatabaseConnection.enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Enrollment
    {
        public int Id { get; set; }
        public ClassEnrolled ClassEnrolled { get; set; }
        public List<AcademicSession> AcademicSessions { get; set; }
        public Enumeration GradeLevel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Enumeration IsPrimary { get; set; }

        public Enrollment(Enumeration gradeLevel, DateTime startDate, DateTime endDate, Enumeration isPrimary, ClassEnrolled classEnrolled, List<AcademicSession> academicSessions)
        {
            Id = -1;
            GradeLevel = gradeLevel;
            StartDate = startDate;
            EndDate = endDate;
            IsPrimary = isPrimary;
            ClassEnrolled = classEnrolled;

            if (academicSessions != null)
                AcademicSessions = academicSessions;
            else
                AcademicSessions = new List<AcademicSession>();
        }

        public override string ToString()
        {
            StringBuilder strEnrollment = new StringBuilder();
            strEnrollment.Append("Class Enrolled: " + ClassEnrolled + "\n");
            strEnrollment.Append("Number of Academic Sessions: " + AcademicSessions.Count + "\n");
            strEnrollment.Append("Grade Level: " + GradeLevel.Netus2Code + "\n");
            strEnrollment.Append("Start Date: " + StartDate + "\n");
            strEnrollment.Append("End Date: " + EndDate + "\n");
            strEnrollment.Append("Is Primary: " + IsPrimary.Netus2Code + "\n");

            return strEnrollment.ToString();
        }
    }
}