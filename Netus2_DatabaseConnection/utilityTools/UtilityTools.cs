using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Netus2_DatabaseConnection.utilityTools
{
    public class UtilityTools
    {
        static void Main(string[] args)
        {
            // Do Nothing, this needs to be here because without it, Visual Studio refuses to run the test cases for some reason.
        }

        public static Dictionary<string, Enumeration> PopulateEnumValues(string tableName)
        {
            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            string sql = "SELECT * FROM " + tableName;

            DataTable dtEnumeration = DataTableFactory.CreateDataTable_Netus2_Enumeration();
            dtEnumeration = connection.ReadIntoDataTable(sql, dtEnumeration);

            Dictionary<string, Enumeration> enumerations = new Dictionary<string, Enumeration>();
            foreach (DataRow row in dtEnumeration.Rows)
            {
                Enumeration enumeration = new Enumeration();
                foreach(DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "netus2_code":
                            if (row[columnName] != DBNull.Value)
                                enumeration.Netus2Code = ((string)row[columnName]).ToLower();
                            else
                                enumeration.Netus2Code = null;
                            break;
                        case "sis_code":
                            if (row[columnName] != DBNull.Value)
                                enumeration.SisCode = ((string)row[columnName]).ToLower();
                            else
                                enumeration.SisCode = null;
                            break;
                        case "hr_code":
                            if (row[columnName] != DBNull.Value)
                                enumeration.HrCode = ((string)row[columnName]).ToLower();
                            else
                                enumeration.HrCode = null;
                            break;
                        case "descript":
                            if (row[columnName] != DBNull.Value)
                                enumeration.Descript = (string)row[columnName];
                            else
                                enumeration.Descript = null;
                            break;
                        default:
                            if (row[columnName] != DBNull.Value)
                                enumeration.Id = (int)row[columnName];
                            else
                                enumeration.Id = -1;
                            break;
                    }
                }
                enumerations.Add(enumeration.Netus2Code, enumeration);
            }

            connection.CloseConnection();

            return enumerations;
        }

        public static Config ReadConfig(Enumeration enumConfig, Enumeration enumIsForStudents, Enumeration enumIsForStaff)
        {
            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            string sql = "SELECT * FROM config WHERE 1=1 " +
                "AND enum_config_id = @enum_config_id " +
                "AND is_for_student_id = @is_for_student_id " +
                "AND is_for_staff_id = @is_for_staff_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enum_config_id", enumConfig.Id));
            parameters.Add(new SqlParameter("@is_for_student_id", enumIsForStudents.Id));
            parameters.Add(new SqlParameter("@is_for_staff_id", enumIsForStaff.Id));

            DataTable dtConfig = DataTableFactory.CreateDataTable_Netus2_Config();
            dtConfig = connection.ReadIntoDataTable(sql, dtConfig, parameters);

            List<Config> configs = new List<Config>();
            foreach(DataRow row in dtConfig.Rows)
            {
                Config config = new Config();
                foreach(DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "config_id":
                            if (row[columnName] != DBNull.Value)
                                config.Id = (int)row[columnName];
                            else
                                config.Id = -1;
                            break;
                        case "enum_config_id":
                            if (row[columnName] != DBNull.Value)
                                config.ConfigType = Enum_Config.GetEnumFromId((int)row[columnName]);
                            else
                                config.ConfigType = null;
                            break;
                        case "config_value":
                            if (row[columnName] != DBNull.Value)
                                config.ConfigValue = (string)row[columnName];
                            else
                                config.ConfigValue = null;
                            break;
                        case "is_for_student_id":
                            if (row[columnName] != DBNull.Value)
                                config.IsForStudents = Enum_True_False.GetEnumFromId((int)row[columnName]);
                            else
                                config.IsForStudents = null;
                            break;
                        case "is_for_staff_id":
                            if (row[columnName] != DBNull.Value)
                                config.IsForStaff = Enum_True_False.GetEnumFromId((int)row[columnName]);
                            else
                                config.IsForStaff = null;
                            break;
                    }
                }
                configs.Add(config);
            }

            connection.CloseConnection();

            if (configs.Count == 1)
                return configs[0];
            else
                throw new Exception(configs.Count + " Config records matching:\n" +
                    "EnumConfig: " + enumConfig.ToString() + "\n" +
                    "EnumIsForStudents: " + enumIsForStudents.ToString() + "\n" +
                    "EnumIsForStaff: " + enumIsForStaff.ToString());
        }
    }
}
