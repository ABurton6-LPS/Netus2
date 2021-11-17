using Netus2_DatabaseConnection.enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class ClassEnrolled
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassCode { get; set; }
        public Enumeration ClassType { get; set; }
        public string Room { get; set; }
        public Course Course { get; set; }
        public AcademicSession AcademicSession { get; set; }
        public List<Enumeration> Periods { get; set; }
        public List<Resource> Resources { get; set; }
        public DateTime? EnrollmentStartDate { get; set; }
        public DateTime? EnrollmentEndDate { get; set; }


        public ClassEnrolled(string name, string classCode, Enumeration classType, string room, Course course, AcademicSession academicSession)
        {
            Id = -1;
            Name = name;
            ClassCode = classCode;
            ClassType = classType;
            Room = room;
            Course = course;
            AcademicSession = academicSession;

            Periods = new List<Enumeration>();
            Resources = new List<Resource>();
        }

        public override string ToString()
        {
            StringBuilder strClassEnrolled = new StringBuilder();
            strClassEnrolled.Append("Name: " + Name + "\n");
            strClassEnrolled.Append("Class Code: " + ClassCode + "\n");
            strClassEnrolled.Append("Class Type: " + ClassType.Netus2Code + "\n");
            strClassEnrolled.Append("Room: " + Room + "\n");
            strClassEnrolled.Append("Course: " + Course + "\n");
            strClassEnrolled.Append("Academic Session: " + AcademicSession + "\n");
            strClassEnrolled.Append("Number of Periods: " + Periods.Count + "\n");
            strClassEnrolled.Append("Number of Resources: " + Resources.Count + "\n");
            strClassEnrolled.Append("Enrollment Start Date: " + EnrollmentStartDate + "\n");
            strClassEnrolled.Append("Enrollment End Date: " + EnrollmentEndDate + "\n");

            return strClassEnrolled.ToString();
        }
    }
}