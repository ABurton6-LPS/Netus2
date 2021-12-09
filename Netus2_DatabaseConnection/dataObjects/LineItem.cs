using Netus2_DatabaseConnection.enumerations;
using System;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class LineItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descript { get; set; }
        public DateTime AssignDate { get; set; }
        public DateTime DueDate { get; set; }
        public ClassEnrolled ClassAssigned { get; set; }
        public Enumeration Category { get; set; }
        public double MarkValueMin { get; set; }
        public double MarkValueMax { get; set; }

        public LineItem(string name, DateTime assignDate, DateTime dueDate, ClassEnrolled classEnrolled, Enumeration category, double markMin, double markMax)
        {
            Id = -1;
            Name = name;
            AssignDate = assignDate;
            DueDate = dueDate;
            ClassAssigned = classEnrolled;
            Category = category;
            MarkValueMin = markMin;
            MarkValueMax = markMax;
        }

        public override string ToString()
        {
            StringBuilder strLineItem = new StringBuilder();
            strLineItem.Append("Name: " + Name + "\n");
            strLineItem.Append("Description: " + Descript + "\n");
            strLineItem.Append("Date Assigned: " + AssignDate + "\n");
            strLineItem.Append("Date Due: " + DueDate + "\n");
            strLineItem.Append("Class Assigned: " + ClassAssigned + "\n");
            strLineItem.Append("Category: " + Category + "\n");
            strLineItem.Append("Mark Value Min: " + MarkValueMin + "\n");
            strLineItem.Append("Mark Value Max: " + MarkValueMax + "\n");

            return strLineItem.ToString();
        }
    }
}