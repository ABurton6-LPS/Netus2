using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.enumerations
{
    public class Enum_Sync_Status
    {
        public static Dictionary<string, Enumeration> values = UtilityTools.PopulateEnumValues("enum_sync_status");

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
