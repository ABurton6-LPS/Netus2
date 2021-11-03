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
        private List<Person> Staff { get; set; }
        private List<Enumeration> StaffRoles { get; set; }


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
            Staff = new List<Person>();
            StaffRoles = new List<Enumeration>();
        }

        public void AddStaff(Person staff, Enumeration role)
        {
            Staff.Add(staff);
            StaffRoles.Add(role);
        }

        public void SetStaff(List<Person> staff, List<Enumeration> roles)
        {
            Staff = staff;
            StaffRoles = roles;
        }

        public List<Person> GetStaff()
        {
            return Staff;
        }

        public List<Enumeration> GetStaffRoles()
        {
            return StaffRoles;
        }

        public void RemoveStaff(Person staff, Enumeration role)
        {
            if (staff.Id > 0)
            {
                for (int i = 0; i < Staff.Count; i++)
                {
                    Person person = Staff[i];
                    if (person.Id == staff.Id)
                    {
                        Staff.RemoveAt(i);
                        StaffRoles.RemoveAt(i);
                    }
                }
            }
            else
            {
                throw new Exception("This Person is not yet saved to the database.\n" + staff.ToString());
            }
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
            strClassEnrolled.Append("Number of Staff: " + Staff.Count + "\n");
            strClassEnrolled.Append("Number of Staff Roles: " + StaffRoles.Count + "\n");

            return strClassEnrolled.ToString();
        }
    }
}