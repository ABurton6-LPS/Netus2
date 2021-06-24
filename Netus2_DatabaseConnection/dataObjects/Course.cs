using Netus2.enumerations;
using System.Collections.Generic;
using System.Text;

namespace Netus2
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
            strCourse.Append("Grades: " + Grades + "\n");
            strCourse.Append("Subjects: " + Subjects + "\n");

            return strCourse.ToString();
        }
    }
}