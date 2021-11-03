using Netus2_DatabaseConnection.enumerations;
using System.Collections.Generic;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public List<Enumeration> Grades { get; set; }
        public List<Enumeration> Subjects { get; set; }

        public Course(string name, string courseCode)
        {
            Id = -1;
            Name = name;
            CourseCode = courseCode;

            Grades = new List<Enumeration>();
            Subjects = new List<Enumeration>();
        }

        public override string ToString()
        {
            StringBuilder strCourse = new StringBuilder();
            strCourse.Append("Name: " + Name + "\n");
            strCourse.Append("Course Code: " + CourseCode + "\n");
            strCourse.Append("Number of Grades: " + Grades.Count + "\n");
            strCourse.Append("Number of Subjects: " + Subjects.Count + "\n");

            return strCourse.ToString();
        }
    }
}