using Netus2_DatabaseConnection.enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Enumeration Gender { get; set; }
        public Enumeration Ethnic { get; set; }
        public List<Enumeration> Roles { get; set; }
        public Enumeration ResidenceStatus { get; set; }
        public string LoginName { get; set; }
        public string LoginPw { get; set; }
        public Enumeration StatusType { get; set; }
        public List<int> Relations { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
        public List<Application> Applications { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Email> Emails { get; set; }
        public List<UniqueIdentifier> UniqueIdentifiers { get; set; }
        public List<Mark> Marks { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public List<EmploymentSession> EmploymentSessions { get; set; }

        public Person(string firstName, string lastName, DateTime birthDate, Enumeration gender, Enumeration ethnic)
        {
            Id = -1;

            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
            Ethnic = ethnic;

            Roles = new List<Enumeration>();
            Relations = new List<int>();
            PhoneNumbers = new List<PhoneNumber>();
            Applications = new List<Application>();
            Addresses = new List<Address>();
            Emails = new List<Email>();
            UniqueIdentifiers = new List<UniqueIdentifier>();
            Marks = new List<Mark>();
            Enrollments = new List<Enrollment>();
            EmploymentSessions = new List<EmploymentSession>();
        }

        public override string ToString()
        {
            StringBuilder strPerson = new StringBuilder();
            strPerson.Append("First Name: " + FirstName + "\n");
            strPerson.Append("Middle Name: " + MiddleName + "\n");
            strPerson.Append("Last Name: " + LastName + "\n");
            strPerson.Append("Birth Date: " + BirthDate + "\n");
            strPerson.Append("Gender: " + Gender.Netus2Code + "\n");
            strPerson.Append("Ethnic: " + Ethnic.Netus2Code + "\n");
            strPerson.Append("Number of Roles: " + Roles.Count + "\n");
            strPerson.Append("Residence Status : " + ResidenceStatus.Netus2Code + "\n");
            strPerson.Append("Login Name: " + LoginName + "\n");
            strPerson.Append("Login Password: " + LoginPw + "\n");
            strPerson.Append("Status Type: " + StatusType.Netus2Code + "\n");
            strPerson.Append("Number of Relations: " + Relations.Count + "\n");
            strPerson.Append("Number of Phone Numbers: " + PhoneNumbers.Count + "\n");
            strPerson.Append("Number of Applications: " + Applications.Count + "\n");
            strPerson.Append("Number of Addresses: " + Addresses.Count + "\n");
            strPerson.Append("Number of Emails: " + Emails.Count + "\n");
            strPerson.Append("Number of Unique Identifiers: " + UniqueIdentifiers.Count + "\n");
            strPerson.Append("Number of Marks: " + Marks.Count + "\n");
            strPerson.Append("Number of Enrollments: " + Enrollments.Count + "\n");
            strPerson.Append("Number of Employment Sessions: " + EmploymentSessions.Count + "\n");

            return strPerson.ToString();
        }
    }
}
