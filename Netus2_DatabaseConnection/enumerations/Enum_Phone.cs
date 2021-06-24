﻿using System.Collections.Generic;

namespace Netus2.enumerations
{
    public class Enum_Phone
    {
        public static Dictionary<string, Enumeration> values = UtilityTools.PopulateEnumValues("enum_phone");

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
    }
}
