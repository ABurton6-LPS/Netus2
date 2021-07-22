using Netus2.daoObjects;
using Netus2.dbAccess;
using Netus2.enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Netus2
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

            IConnectable connection = new Netus2DatabaseConnection();
            connection.OpenConnection();

            SqlDataReader reader = null;
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

        public static DataTable CreateDataTable(string tableToCreate)
        {
            DataTable dtAcademicSession = new DataTable(tableToCreate);
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            switch (tableToCreate)
            {
                case "AcademicSession":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "session_code";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "enum_session_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(DateTime);
                    dtColumn.ColumnName = "start_date";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(DateTime);
                    dtColumn.ColumnName = "end_date";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "parent_session_code";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "organization_id";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "Address":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "address_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "person_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "address_line_1";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "address_line_2";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "address_line_3";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "address_line_4";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "apartment";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "city";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "enum_state_province_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "postal_code";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "enum_country_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "is_current_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "enum_address_id";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "Application":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "app_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "provider_id";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "Classenrolled":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "class_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "class_code";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "enum_class_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "room";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "course_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "academic_session_id";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "Course":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "course_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "course_code";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "EmploymentSession":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "employment_session_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "person_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(DateTime);
                    dtColumn.ColumnName = "start_date";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(DateTime);
                    dtColumn.ColumnName = "end_date";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "is_primary_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "enum_session_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "orgnaization_id";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "Enrollment":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "enrollment_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "person_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "class_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "enum_grade_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(DateTime);
                    dtColumn.ColumnName = "start_date";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(DateTime);
                    dtColumn.ColumnName = "end_date";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "is_primary_id";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "LineItem":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "lineitem_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "descript";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(DateTime);
                    dtColumn.ColumnName = "assign_date";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(DateTime);
                    dtColumn.ColumnName = "due_date";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "class_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "enum_categtory_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(double);
                    dtColumn.ColumnName = "markValueMin";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(double);
                    dtColumn.ColumnName = "markValueMax";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "Mark":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "mark_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "lineitem_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "person_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "enum_score_status_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(double);
                    dtColumn.ColumnName = "score";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(DateTime);
                    dtColumn.ColumnName = "score_date";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "comment";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "Organization":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "enum_organization_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "identifier";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "building_code";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "organization_parent_id";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "Person":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "person_type";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "SIS_ID";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "first_name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "middle_name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "last_name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(DateTime);
                    dtColumn.ColumnName = "birth_date";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "enum_gender_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "enum_ethnic_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "enum_residence_status_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "login_name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "login_pw";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "PhoneNumber":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "phone_number_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "person_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "phone_number";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "is_primary_id";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "Provider":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "provider_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "url_standard_access";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "url_admin_access";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "populated_by";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "parent_provider_id";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "Resource":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "resource_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "name";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "enum_importnace_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "vendor_resource_identification";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "vendor_identification";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;

                case "UniqueIdentifier":
                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "unique_identifier_id";
                    PrimaryKeycolumns[0] = dtColumn;
                    dtAcademicSession.Columns.Add(dtColumn);
                    dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "person_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(string);
                    dtColumn.ColumnName = "unique_identifier";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "enum_identifier_id";
                    dtAcademicSession.Columns.Add(dtColumn);

                    dtColumn = new DataColumn();
                    dtColumn.DataType = typeof(int);
                    dtColumn.ColumnName = "is_active_id";
                    dtAcademicSession.Columns.Add(dtColumn);
                    break;
                default:
                    throw new NotImplementedException(tableToCreate + "has not had the DataTable creation implemented.");
            }

            return dtAcademicSession;
        }
    }
}
