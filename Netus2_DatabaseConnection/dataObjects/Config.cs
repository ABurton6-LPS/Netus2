using Netus2_DatabaseConnection.enumerations;
using System.Collections.Generic;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Config
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsForStudents { get; set; }
        public bool IsForStaff { get; set; }
        public string Descript { get; set; }

        public override string ToString()
        {
            StringBuilder strConfig = new StringBuilder();
            strConfig.Append("Type: " + Type + "\n");
            strConfig.Append("Name: " + Name + "\n");
            strConfig.Append("Value: " + Value + "\n");
            strConfig.Append("Is For Students: " + IsForStudents + "\n");
            strConfig.Append("Is For Staff: " + IsForStaff + "\n");
            strConfig.Append("Descript: " + Descript + "\n");

            return strConfig.ToString();
        }
    }
}