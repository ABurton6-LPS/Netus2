﻿using Netus2_DatabaseConnection.enumerations;
using System.Collections.Generic;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Config
    {
        public int Id { get; set; }
        public Enumeration ConfigType { get; set; }
        public string ConfigValue { get; set; }
        public Enumeration IsForStudents { get; set; }
        public Enumeration IsForStaff { get; set; }

        public override string ToString()
        {
            StringBuilder strConfig = new StringBuilder();
            strConfig.Append("Config Type: " + ConfigType.ToString() + "\n");
            strConfig.Append("Config Value: " + ConfigValue + "\n");
            strConfig.Append("Is For Students: " + IsForStudents.ToString() + "\n");
            strConfig.Append("Is For Staff: " + IsForStaff.ToString() + "\n");

            return strConfig.ToString();
        }
    }
}