using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

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

        public static Config ReadConfig(string configName, bool isForStudents, bool isForStaff)
        {
            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            StringBuilder sql = new StringBuilder("SELECT * FROM config WHERE 1=1 ");

            List<SqlParameter> parameters = new List<SqlParameter>();
            
            sql.Append("AND name = @name ");
            parameters.Add(new SqlParameter("@name", configName));

            sql.Append("AND is_for_student = @is_for_student ");
            parameters.Add(new SqlParameter("@is_for_student", isForStudents));

            sql.Append("AND is_for_staff = @is_for_staff");
            parameters.Add(new SqlParameter("@is_for_staff", isForStaff));

            DataTable dtConfig = DataTableFactory.CreateDataTable_Netus2_Config();
            dtConfig = connection.ReadIntoDataTable(sql.ToString(), dtConfig, parameters);

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
                        case "type":
                            if (row[columnName] != DBNull.Value)
                                config.Type = (string)row[columnName];
                            else
                                config.Type = null;
                            break;
                        case "name":
                            if (row[columnName] != DBNull.Value)
                                config.Name = (string)row[columnName];
                            else
                                config.Name = null;
                            break;
                        case "value":
                            if (row[columnName] != DBNull.Value)
                                config.Value = (string)row[columnName];
                            else
                                config.Value = null;
                            break;
                        case "is_for_student":
                            if (row[columnName] != DBNull.Value)
                                config.IsForStudents = (bool)row[columnName];
                            else
                                config.IsForStudents = false;
                            break;
                        case "is_for_staff":
                            if (row[columnName] != DBNull.Value)
                                config.IsForStaff = (bool)row[columnName];
                            else
                                config.IsForStaff = false;
                            break;
                        case "descript":
                            if (row[columnName] != DBNull.Value)
                                config.Descript = (string)row[columnName];
                            else
                                config.Descript = null;
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
                    "Name: " + configName + "\n" +
                    "IsForStudents: " + isForStudents + "\n" +
                    "IsForStaff: " + isForStaff);
        }
    }
}
