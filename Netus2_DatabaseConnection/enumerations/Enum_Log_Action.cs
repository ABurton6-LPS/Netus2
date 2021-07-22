﻿using System.Collections.Generic;

namespace Netus2.enumerations
{
    public class Enum_Log_Action
    {
        public static Dictionary<string, Enumeration> values = UtilityTools.PopulateEnumValues("enum_log_action");

        public static Enumeration GetEnumFromId(int id)
        {
            foreach (var value in values)
            {
                if (value.Value.Id == id)
                {
                    return value.Value;
                }
            }

            return null;
        }

        public static Enumeration GetEnumFromSisCode(string sisCode)
        {
            foreach (var value in values)
            {
                if (value.Value.SisCode == sisCode)
                {
                    return value.Value;
                }
            }

            return null;
        }
    }
}
