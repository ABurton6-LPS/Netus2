using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Netus2_DatabaseConnection.utilityTools
{
    public class UtilityTools
    {
        static void Main(string[] args)
        {
            // Do Nothing
        }

        public static Dictionary<string, Enumeration> PopulateEnumValues(string tableName)
        {
            Dictionary<string, Enumeration> enumerations = new Dictionary<string, Enumeration>();

            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader("SELECT * FROM " + tableName);
                while (reader.Read())
                {
                    Enumeration enumeration = new Enumeration();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                enumeration.Id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                enumeration.Netus2Code = value != DBNull.Value ? ((string)value).ToLower() : null;
                                break;
                            case 2:
                                enumeration.SisCode = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                enumeration.HrCode = value != DBNull.Value ? (string)value : null;
                                break;
                            case 4:
                                enumeration.PipCode = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                enumeration.Descript = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in " + tableName + " table: " + reader.GetName(i));
                        }
                    }
                    enumerations.Add(enumeration.Netus2Code, enumeration);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            connection.CloseConnection();

            return enumerations;
        }
    }
}
