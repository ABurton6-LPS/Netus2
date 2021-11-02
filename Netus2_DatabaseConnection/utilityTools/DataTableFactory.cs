using System;
using System.Data;

namespace Netus2_DatabaseConnection.utilityTools
{
    public static class DataTableFactory
    {
        public static DataTable CreateDataTable_Sis_AcademicSession()
        {
            DataTable dtAcademicSession = new DataTable("AcademicSession");
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "building_code";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "school_year";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "term_code";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "track_code";
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

            return dtAcademicSession;
        }

        public static DataTable CreateDataTable_Sis_Organization()
        {
            DataTable dtOrganization = new DataTable("Organization");
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "enum_organization_id";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "identifier";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "building_code";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "organization_parent_id";
            dtOrganization.Columns.Add(dtColumn);

            return dtOrganization;
        }

        public static DataTable CreateDataTable_Sis_Person()
        {
            DataTable dtPerson = new DataTable("Person");
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "person_type";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "sis_id";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "first_name";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "middle_name";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "last_name";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "birth_date";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "enum_gender_id";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "enum_ethnic_id";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "enum_residence_status_id";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "login_name";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "login_pw";
            dtPerson.Columns.Add(dtColumn);

            return dtPerson;
        }

        public static DataTable CreateDataTable_Sis_Address()
        {
            DataTable dtAddress = new DataTable("Address");
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_1";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_2";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_3";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_4";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "apartment";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "city";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "enum_state_province_id";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "postal_code";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "enum_country_id";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "is_primary_id";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "suniq";
            dtAddress.Columns.Add(dtColumn);

            return dtAddress;
        }

        public static DataTable CreateDataTable_Sis_PhoneNumber()
        {
            //Do Nothing
            return null;
        }

        public static DataTable CreateDataTable_Sis_Course()
        {
            DataTable dtCourse = new DataTable("Course");
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "course_code";
            dtCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "subject";
            dtCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "grade";
            dtCourse.Columns.Add(dtColumn);

            return dtCourse;
        }

        public static DataTable CreateDataTable_Sis_Class()
        {
            //Do Nothing
            return null;
        }

        public static DataTable CreateDataTable_Sis_Enrollment()
        {
            //Do Nothing
            return null;
        }

        public static DataTable CreateDataTable_Sis_LineItem()
        {
            //Do Nothing
            return null;
        }

        public static DataTable CreateDataTable_Sis_Mark()
        {
            //Do Nothing
            return null;
        }

        public static DataTable CreateDataTable_Netus2_Config()
        {
            DataTable dtConfig = new DataTable("Config");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "config_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtConfig.Columns.Add(dtColumn);
            dtConfig.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_config_id";
            dtConfig.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "config_value";
            dtConfig.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "is_for_student_id";
            dtConfig.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "is_for_staff_id";
            dtConfig.Columns.Add(dtColumn);

            return dtConfig;
        }

        public static DataTable CreateDataTable_Netus2_Enumeration()
        {
            DataTable dtPerson = new DataTable("Enumeration");
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "netus2_code";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "sis_code";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "hr_code";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "descript";
            dtPerson.Columns.Add(dtColumn);

            return dtPerson;
        }

        public static DataTable CreateDataTable_Netus2_AcademicSession()
        {
            DataTable dtAcademicSession = new DataTable("AcademicSession");
            //DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "academic_session_id";
            //PrimaryKeycolumns[0] = dtColumn;
            dtAcademicSession.Columns.Add(dtColumn);
            //dtAcademicSession.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "term_code";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "track_code";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "school_year";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
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
            dtColumn.ColumnName = "enum_session_id";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "parent_session_id";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "organization_id";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtAcademicSession.Columns.Add(dtColumn);

            return dtAcademicSession;
        }

        public static DataTable CreateDataTable_Netus2_Address()
        {
            DataTable dtAddress = new DataTable("Address");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "address_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtAddress.Columns.Add(dtColumn);
            dtAddress.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_1";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_2";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_3";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_4";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "apartment";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "city";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_state_province_id";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "postal_code";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_country_id";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_address_id";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtAddress.Columns.Add(dtColumn);

            return dtAddress;
        }

        public static DataTable CreateDataTable_Netus2_Application()
        {
            DataTable dtApplication = new DataTable("Application");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "app_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtApplication.Columns.Add(dtColumn);
            dtApplication.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "provider_id";
            dtApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtApplication.Columns.Add(dtColumn);

            return dtApplication;
        }

        public static DataTable CreateDataTable_Netus2_ClassEnrolled()
        {
            DataTable dtClassEnrolled = new DataTable("ClassEnrolled");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtClassEnrolled.Columns.Add(dtColumn);
            dtClassEnrolled.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "class_code";
            dtClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_class_id";
            dtClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "room";
            dtClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "course_id";
            dtClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "academic_session_id";
            dtClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtClassEnrolled.Columns.Add(dtColumn);

            return dtClassEnrolled;
        }

        public static DataTable CreateDataTable_Netus2_Course()
        {
            DataTable dtCourse = new DataTable("Course");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "course_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtCourse.Columns.Add(dtColumn);
            dtCourse.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "course_code";
            dtCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtCourse.Columns.Add(dtColumn);

            return dtCourse;
        }

        public static DataTable CreateDataTable_Netus2_EmploymentSession()
        {
            DataTable dtEmploymentSession = new DataTable("EmploymentSession");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "employment_session_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtEmploymentSession.Columns.Add(dtColumn);
            dtEmploymentSession.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "start_date";
            dtEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "end_date";
            dtEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "is_primary_id";
            dtEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_session_id";
            dtEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "organization_id";
            dtEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtEmploymentSession.Columns.Add(dtColumn);

            return dtEmploymentSession;
        }

        public static DataTable CreateDataTable_Netus2_Enrollment()
        {
            DataTable dtEnrollment = new DataTable("Enrollment");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enrollment_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtEnrollment.Columns.Add(dtColumn);
            dtEnrollment.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            dtEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_grade_id";
            dtEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "start_date";
            dtEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "end_date";
            dtEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "is_primary_id";
            dtEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtEnrollment.Columns.Add(dtColumn);

            return dtEnrollment;
        }

        public static DataTable CreateDataTable_Netus2_LineItem()
        {
            DataTable dtLineItem = new DataTable("LineItem");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "lineitem_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLineItem.Columns.Add(dtColumn);
            dtLineItem.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "descript";
            dtLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "assign_date";
            dtLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "due_date";
            dtLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            dtLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_category_id";
            dtLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "markValueMin";
            dtLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "markValueMax";
            dtLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLineItem.Columns.Add(dtColumn);

            return dtLineItem;
        }

        public static DataTable CreateDataTable_Netus2_Mark()
        {
            DataTable dtMark = new DataTable("Mark");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "mark_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtMark.Columns.Add(dtColumn);
            dtMark.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "lineitem_id";
            dtMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_score_status_id";
            dtMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "score";
            dtMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "score_date";
            dtMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "comment";
            dtMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtMark.Columns.Add(dtColumn);

            return dtMark;
        }

        public static DataTable CreateDataTable_Netus2_Organization()
        {
            DataTable dtOrganization = new DataTable("Organization");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "organization_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtOrganization.Columns.Add(dtColumn);
            dtOrganization.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_organization_id";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "identifier";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "sis_building_code";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "hr_building_code";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "organization_parent_id";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtOrganization.Columns.Add(dtColumn);

            return dtOrganization;
        }

        public static DataTable CreateDataTable_Netus2_Person()
        {
            DataTable dtPerson = new DataTable("Person");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtPerson.Columns.Add(dtColumn);
            dtPerson.PrimaryKey = PrimaryKeycolumns;
            
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "first_name";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "middle_name";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "last_name";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "birth_date";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_gender_id";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_ethnic_id";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_residence_status_id";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "login_name";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "login_pw";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtPerson.Columns.Add(dtColumn);

            return dtPerson;
        }

        public static DataTable CreateDataTable_Netus2_PhoneNumber()
        {
            DataTable dtPhoneNumber = new DataTable("PhoneNumber");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "phone_number_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtPhoneNumber.Columns.Add(dtColumn);
            dtPhoneNumber.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "phone_number";
            dtPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_phone_id";
            dtPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtPhoneNumber.Columns.Add(dtColumn);

            return dtPhoneNumber;
        }

        public static DataTable CreateDataTable_Netus2_Provider()
        {
            DataTable dtProvider = new DataTable("Provider");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "provider_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtProvider.Columns.Add(dtColumn);
            dtProvider.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "url_standard_access";
            dtProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "url_admin_access";
            dtProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "populated_by";
            dtProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "parent_provider_id";
            dtProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtProvider.Columns.Add(dtColumn);

            return dtProvider;
        }

        public static DataTable CreateDataTable_Netus2_Resource()
        {
            DataTable dtResource = new DataTable("Resource");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "resource_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtResource.Columns.Add(dtColumn);
            dtResource.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_importance_id";
            dtResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "vendor_resource_identification";
            dtResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "vendor_identification";
            dtResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "application_identification";
            dtResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtResource.Columns.Add(dtColumn);

            return dtResource;
        }

        public static DataTable CreateDataTable_Netus2_UniqueIdentifier()
        {
            DataTable dtUniqueIdentifier = new DataTable("UniqueIdentifier");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "unique_identifier_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtUniqueIdentifier.Columns.Add(dtColumn);
            dtUniqueIdentifier.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "unique_identifier";
            dtUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_identifier_id";
            dtUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtUniqueIdentifier.Columns.Add(dtColumn);

            return dtUniqueIdentifier;
        }

        public static DataTable CreateDataTable_Netus2_JctClassPeriod()
        {
            DataTable dtJctClassPeriod = new DataTable("JctClassPeriod");
            DataColumn[] PrimaryKeycolumns = new DataColumn[2];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtJctClassPeriod.Columns.Add(dtColumn);
            dtJctClassPeriod.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_period_id";
            PrimaryKeycolumns[1] = dtColumn;
            dtJctClassPeriod.Columns.Add(dtColumn);
            dtJctClassPeriod.PrimaryKey = PrimaryKeycolumns;

            return dtJctClassPeriod;
        }

        public static DataTable CreateDataTable_Netus2_JctClassPerson()
        {
            DataTable dtJctClassPerson = new DataTable("JctClassPerson");
            DataColumn[] PrimaryKeycolumns = new DataColumn[2];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtJctClassPerson.Columns.Add(dtColumn);
            dtJctClassPerson.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            PrimaryKeycolumns[1] = dtColumn;
            dtJctClassPerson.Columns.Add(dtColumn);
            dtJctClassPerson.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_role_id";
            dtJctClassPerson.Columns.Add(dtColumn);

            return dtJctClassPerson;
        }

        public static DataTable CreateDataTable_Netus2_JctClassResource()
        {
            DataTable dtJctClassResource = new DataTable("JctClassResource");
            DataColumn[] PrimaryKeycolumns = new DataColumn[2];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtJctClassResource.Columns.Add(dtColumn);
            dtJctClassResource.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "resource_id";
            PrimaryKeycolumns[1] = dtColumn;
            dtJctClassResource.Columns.Add(dtColumn);
            dtJctClassResource.PrimaryKey = PrimaryKeycolumns;

            return dtJctClassResource;
        }

        public static DataTable CreateDataTable_Netus2_JctCourseGrade()
        {
            DataTable dtJctCourseGrade = new DataTable("JctCourseGrade");
            DataColumn[] PrimaryKeycolumns = new DataColumn[2];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "course_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtJctCourseGrade.Columns.Add(dtColumn);
            dtJctCourseGrade.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_grade_id";
            PrimaryKeycolumns[1] = dtColumn;
            dtJctCourseGrade.Columns.Add(dtColumn);
            dtJctCourseGrade.PrimaryKey = PrimaryKeycolumns;

            return dtJctCourseGrade;
        }

        public static DataTable CreateDataTable_Netus2_JctCourseSubject()
        {
            DataTable dtJctCourseSubject = new DataTable("JctCourseSubject");
            DataColumn[] PrimaryKeycolumns = new DataColumn[2];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "course_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtJctCourseSubject.Columns.Add(dtColumn);
            dtJctCourseSubject.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_subject_id";
            PrimaryKeycolumns[1] = dtColumn;
            dtJctCourseSubject.Columns.Add(dtColumn);
            dtJctCourseSubject.PrimaryKey = PrimaryKeycolumns;

            return dtJctCourseSubject;
        }

        public static DataTable CreateDataTable_Netus2_JctEnrollmentAcademicSession()
        {
            DataTable dtJctEnrollmentAcademicSession = new DataTable("JctEnrollmentAcademicSession");
            DataColumn[] PrimaryKeycolumns = new DataColumn[2];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enrollment_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtJctEnrollmentAcademicSession.Columns.Add(dtColumn);
            dtJctEnrollmentAcademicSession.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "academic_session_id";
            PrimaryKeycolumns[1] = dtColumn;
            dtJctEnrollmentAcademicSession.Columns.Add(dtColumn);
            dtJctEnrollmentAcademicSession.PrimaryKey = PrimaryKeycolumns;

            return dtJctEnrollmentAcademicSession;
        }

        public static DataTable CreateDataTable_Netus2_JctPersonAddress()
        {
            DataTable dtJctPersonAddress = new DataTable("JctPersonAddress");
            DataColumn[] PrimaryKeycolumns = new DataColumn[2];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtJctPersonAddress.Columns.Add(dtColumn);
            dtJctPersonAddress.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "address_id";
            PrimaryKeycolumns[1] = dtColumn;
            dtJctPersonAddress.Columns.Add(dtColumn);
            dtJctPersonAddress.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "is_primary_id";
            dtJctPersonAddress.Columns.Add(dtColumn);

            return dtJctPersonAddress;
        }

        public static DataTable CreateDataTable_Netus2_JctPersonApp()
        {
            DataTable dtJctPersonApp = new DataTable("JctPersonApp");
            DataColumn[] PrimaryKeycolumns = new DataColumn[2];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtJctPersonApp.Columns.Add(dtColumn);
            dtJctPersonApp.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "app_id";
            PrimaryKeycolumns[1] = dtColumn;
            dtJctPersonApp.Columns.Add(dtColumn);
            dtJctPersonApp.PrimaryKey = PrimaryKeycolumns;

            return dtJctPersonApp;
        }

        public static DataTable CreateDataTable_Netus2_JctPersonPhoneNumber()
        {
            DataTable dtJctPersonPhoneNumber = new DataTable("JctPersonPhoneNumber");
            DataColumn[] PrimaryKeycolumns = new DataColumn[2];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtJctPersonPhoneNumber.Columns.Add(dtColumn);
            dtJctPersonPhoneNumber.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "phone_number_id";
            PrimaryKeycolumns[1] = dtColumn;
            dtJctPersonPhoneNumber.Columns.Add(dtColumn);
            dtJctPersonPhoneNumber.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "is_primary_id";
            dtJctPersonPhoneNumber.Columns.Add(dtColumn);

            return dtJctPersonPhoneNumber;
        }

        public static DataTable CreateDataTable_Netus2_JctPersonPerson()
        {
            DataTable dtJctPersonPerson = new DataTable("JctPersonPerson");
            DataColumn[] PrimaryKeycolumns = new DataColumn[2];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_one_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtJctPersonPerson.Columns.Add(dtColumn);
            dtJctPersonPerson.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_two_id";
            PrimaryKeycolumns[1] = dtColumn;
            dtJctPersonPerson.Columns.Add(dtColumn);
            dtJctPersonPerson.PrimaryKey = PrimaryKeycolumns;

            return dtJctPersonPerson;
        }

        public static DataTable CreateDataTable_Netus2_JctPersonRole()
        {
            DataTable dtJctPersonRole = new DataTable("JctPersonRole");
            DataColumn[] PrimaryKeycolumns = new DataColumn[2];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtJctPersonRole.Columns.Add(dtColumn);
            dtJctPersonRole.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_role_id";
            PrimaryKeycolumns[1] = dtColumn;
            dtJctPersonRole.Columns.Add(dtColumn);
            dtJctPersonRole.PrimaryKey = PrimaryKeycolumns;

            return dtJctPersonRole;
        }

        public static DataTable CreateDataTable_Netus2_Log_AcademicSession()
        {
            DataTable dtLogAcademicSession = new DataTable("Log_AcademicSession");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_academic_session_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogAcademicSession.Columns.Add(dtColumn);
            dtLogAcademicSession.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "academic_session_id";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "term_code";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "track_code";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "school_year";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "start_date";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "end_date";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_session_id";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "parent_session_id";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "organization_id";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogAcademicSession.Columns.Add(dtColumn);

            return dtLogAcademicSession;
        }

        public static DataTable CreateDataTable_Netus2_Log_Address()
        {
            DataTable dtLogAddress = new DataTable("Log_Address");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_address_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogAddress.Columns.Add(dtColumn);
            dtLogAddress.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "address_id";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_1";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_2";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_3";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "address_line_4";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "apartment";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "city";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_state_province_id";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "postal_code";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_country_id";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_address_id";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogAddress.Columns.Add(dtColumn);

            return dtLogAddress;
        }

        public static DataTable CreateDataTable_Netus2_Log_Application()
        {
            DataTable dtLogApplication = new DataTable("Log_Application");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_app_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogApplication.Columns.Add(dtColumn);
            dtLogApplication.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "app_id";
            dtLogApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtLogApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "provider_id";
            dtLogApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogApplication.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogApplication.Columns.Add(dtColumn);

            return dtLogApplication;
        }

        public static DataTable CreateDataTable_Netus2_Log_ClassEnrolled()
        {
            DataTable dtLogClassEnrolled = new DataTable("Log_ClassEnrolled");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_class_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogClassEnrolled.Columns.Add(dtColumn);
            dtLogClassEnrolled.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "class_code";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_class_id";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "room";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "course_id";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "academic_session_id";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogClassEnrolled.Columns.Add(dtColumn);

            return dtLogClassEnrolled;
        }

        public static DataTable CreateDataTable_Netus2_Log_Course()
        {
            DataTable dtLogCourse = new DataTable("Log_Course");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_course_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogCourse.Columns.Add(dtColumn);
            dtLogCourse.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "course_id";
            dtLogCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtLogCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "course_code";
            dtLogCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogCourse.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogCourse.Columns.Add(dtColumn);

            return dtLogCourse;
        }

        public static DataTable CreateDataTable_Netus2_Log_EmploymentSession()
        {
            DataTable dtLogEmploymentSession = new DataTable("Log_EmploymentSession");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_employment_session_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogEmploymentSession.Columns.Add(dtColumn);
            dtLogEmploymentSession.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "employment_session_id";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "start_date";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "end_date";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "is_primary_id";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_session_id";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "organization_id";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogEmploymentSession.Columns.Add(dtColumn);

            return dtLogEmploymentSession;
        }

        public static DataTable CreateDataTable_Netus2_Log_Enrollment()
        {
            DataTable dtLogEnrollment = new DataTable("Log_Enrollment");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_enrollment_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogEnrollment.Columns.Add(dtColumn);
            dtLogEnrollment.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enrollment_id";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_grade_id";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "start_date";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "end_date";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "is_primary_id";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogEnrollment.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogEnrollment.Columns.Add(dtColumn);

            return dtLogEnrollment;
        }

        public static DataTable CreateDataTable_Netus2_Log_LineItem()
        {
            DataTable dtLogLineItem = new DataTable("Log_LineItem");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_lineitem_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogLineItem.Columns.Add(dtColumn);
            dtLogLineItem.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "lineitem_id";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "descript";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "assign_date";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "due_date";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_category_id";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "markValueMin";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "markValueMax";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogLineItem.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogLineItem.Columns.Add(dtColumn);

            return dtLogLineItem;
        }

        public static DataTable CreateDataTable_Netus2_Log_Mark()
        {
            DataTable dtLogMark = new DataTable("Log_Mark");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_mark_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogMark.Columns.Add(dtColumn);
            dtLogMark.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "mark_id";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "lineitem_id";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_score_status_id";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(double);
            dtColumn.ColumnName = "score";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "score_date";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "comment";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogMark.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogMark.Columns.Add(dtColumn);

            return dtLogMark;
        }

        public static DataTable CreateDataTable_Netus2_Log_Organization()
        {
            DataTable dtLogOrganization = new DataTable("Log_Organization");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_organization_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogOrganization.Columns.Add(dtColumn);
            dtLogOrganization.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "organization_id";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_organization_id";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "identifier";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "sis_building_code";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "hr_building_code";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "organization_parent_id";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogOrganization.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogOrganization.Columns.Add(dtColumn);

            return dtLogOrganization;
        }

        public static DataTable CreateDataTable_Netus2_Log_Person()
        {
            DataTable dtLogPerson = new DataTable("Log_Person");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_person_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogPerson.Columns.Add(dtColumn);
            dtLogPerson.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "first_name";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "middle_name";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "last_name";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "birth_date";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_gender_id";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_ethnic_id";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_residence_status_id";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "login_name";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "login_pw";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogPerson.Columns.Add(dtColumn);

            return dtLogPerson;
        }

        public static DataTable CreateDataTable_Netus2_Log_PhoneNumber()
        {
            DataTable dtLogPhoneNumber = new DataTable("Log_PhoneNumber");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_phone_number_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogPhoneNumber.Columns.Add(dtColumn);
            dtLogPhoneNumber.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "phone_number_id";
            dtLogPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "phone_number";
            dtLogPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_phone_id";
            dtLogPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogPhoneNumber.Columns.Add(dtColumn);

            return dtLogPhoneNumber;
        }

        public static DataTable CreateDataTable_Netus2_Log_Provider()
        {
            DataTable dtLogProvider = new DataTable("Log_Provider");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_provider_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogProvider.Columns.Add(dtColumn);
            dtLogProvider.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "provider_id";
            dtLogProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtLogProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "url_standard_access";
            dtLogProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "url_admin_access";
            dtLogProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "parent_provider_id";
            dtLogProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogProvider.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogProvider.Columns.Add(dtColumn);

            return dtLogProvider;
        }

        public static DataTable CreateDataTable_Netus2_Log_Resource()
        {
            DataTable dtLogResource = new DataTable("Log_Resource");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_resource_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogResource.Columns.Add(dtColumn);
            dtLogResource.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "resource_id";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "name";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_importance_id";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "vendor_resource_identification";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "vendor_identification";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "application_identification";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogResource.Columns.Add(dtColumn);

            return dtLogResource;
        }

        public static DataTable CreateDataTable_Netus2_Log_UniqueIdentifier()
        {
            DataTable dtLogUniqueIdentifier = new DataTable("Log_UniqueIdentifier");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_unique_identifier_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogUniqueIdentifier.Columns.Add(dtColumn);
            dtLogUniqueIdentifier.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "unique_identifier_id";
            dtLogUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtLogUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "unique_identifier";
            dtLogUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_identifier_id";
            dtLogUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "created";
            dtLogUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "created_by";
            dtLogUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "changed";
            dtLogUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "changed_by";
            dtLogUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogUniqueIdentifier.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogUniqueIdentifier.Columns.Add(dtColumn);

            return dtLogUniqueIdentifier;
        }

        public static DataTable CreateDataTable_Netus2_Log_JctClassPeriod()
        {
            DataTable dtLogJctClassPeriod = new DataTable("Log_JctClassPeriod");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_jct_class_period_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogJctClassPeriod.Columns.Add(dtColumn);
            dtLogJctClassPeriod.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            dtLogJctClassPeriod.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_period_id";
            dtLogJctClassPeriod.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogJctClassPeriod.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogJctClassPeriod.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogJctClassPeriod.Columns.Add(dtColumn);

            return dtLogJctClassPeriod;
        }

        public static DataTable CreateDataTable_Netus2_Log_JctClassPerson()
        {
            DataTable dtLogJctClassPerson = new DataTable("Log_JctClassPerson");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_jct_class_person_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogJctClassPerson.Columns.Add(dtColumn);
            dtLogJctClassPerson.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            dtLogJctClassPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtLogJctClassPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_role_id";
            dtLogJctClassPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogJctClassPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogJctClassPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogJctClassPerson.Columns.Add(dtColumn);

            return dtLogJctClassPerson;
        }

        public static DataTable CreateDataTable_Netus2_Log_JctClassResource()
        {
            DataTable dtLogJctClassResource = new DataTable("Log_JctClassResource");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_jct_class_resource_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogJctClassResource.Columns.Add(dtColumn);
            dtLogJctClassResource.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "class_id";
            dtLogJctClassResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "resource_id";
            dtLogJctClassResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogJctClassResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogJctClassResource.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogJctClassResource.Columns.Add(dtColumn);

            return dtLogJctClassResource;
        }

        public static DataTable CreateDataTable_Netus2_Log_JctCourseGrade()
        {
            DataTable dtLogJctCourseGrade = new DataTable("Log_JctCourseGrade");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_jct_course_grade_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogJctCourseGrade.Columns.Add(dtColumn);
            dtLogJctCourseGrade.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "course_id";
            dtLogJctCourseGrade.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_grade_id";
            dtLogJctCourseGrade.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogJctCourseGrade.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogJctCourseGrade.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogJctCourseGrade.Columns.Add(dtColumn);

            return dtLogJctCourseGrade;
        }

        public static DataTable CreateDataTable_Netus2_Log_JctCourseSubject()
        {
            DataTable dtLogJctCourseSubject = new DataTable("Log_JctCourseSubject");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_jct_course_subject_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogJctCourseSubject.Columns.Add(dtColumn);
            dtLogJctCourseSubject.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "course_id";
            dtLogJctCourseSubject.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_subject_id";
            dtLogJctCourseSubject.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogJctCourseSubject.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogJctCourseSubject.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogJctCourseSubject.Columns.Add(dtColumn);

            return dtLogJctCourseSubject;
        }

        public static DataTable CreateDataTable_Netus2_Log_JctEnrollmentAcademicSession()
        {
            DataTable dtLogJctEnrollmentAcademicSession = new DataTable("Log_JctEnrollmentAcademicSession");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_jct_enrollment_academic_session_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogJctEnrollmentAcademicSession.Columns.Add(dtColumn);
            dtLogJctEnrollmentAcademicSession.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enrollment_id";
            dtLogJctEnrollmentAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "academic_session_id";
            dtLogJctEnrollmentAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogJctEnrollmentAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogJctEnrollmentAcademicSession.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogJctEnrollmentAcademicSession.Columns.Add(dtColumn);

            return dtLogJctEnrollmentAcademicSession;
        }

        public static DataTable CreateDataTable_Netus2_Log_JctPersonAddress()
        {
            DataTable dtLogJctPersonAddress = new DataTable("Log_JctPersonAddress");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_jct_person_address_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogJctPersonAddress.Columns.Add(dtColumn);
            dtLogJctPersonAddress.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtLogJctPersonAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "address_id";
            dtLogJctPersonAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogJctPersonAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogJctPersonAddress.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogJctPersonAddress.Columns.Add(dtColumn);

            return dtLogJctPersonAddress;
        }

        public static DataTable CreateDataTable_Netus2_Log_JctPersonApp()
        {
            DataTable dtLogJctPersonApp = new DataTable("Log_JctPersonApp");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_jct_person_app_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogJctPersonApp.Columns.Add(dtColumn);
            dtLogJctPersonApp.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtLogJctPersonApp.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "app_id";
            dtLogJctPersonApp.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogJctPersonApp.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogJctPersonApp.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogJctPersonApp.Columns.Add(dtColumn);

            return dtLogJctPersonApp;
        }

        public static DataTable CreateDataTable_Netus2_Log_JctPersonPhoneNumber()
        {
            DataTable dtLogJctPersonPhoneNumber = new DataTable("Log_JctPersonPhoneNumber");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_jct_person_phone_number_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogJctPersonPhoneNumber.Columns.Add(dtColumn);
            dtLogJctPersonPhoneNumber.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtLogJctPersonPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "phone_number_id";
            dtLogJctPersonPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "is_primary_id";
            dtLogJctPersonPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogJctPersonPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogJctPersonPhoneNumber.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogJctPersonPhoneNumber.Columns.Add(dtColumn);

            return dtLogJctPersonPhoneNumber;
        }

        public static DataTable CreateDataTable_Netus2_Log_JctPersonPerson()
        {
            DataTable dtLogJctPersonPerson = new DataTable("Log_JctPersonPerson");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_jct_person_person_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogJctPersonPerson.Columns.Add(dtColumn);
            dtLogJctPersonPerson.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_one_id";
            dtLogJctPersonPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_two_id";
            dtLogJctPersonPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogJctPersonPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogJctPersonPerson.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogJctPersonPerson.Columns.Add(dtColumn);

            return dtLogJctPersonPerson;
        }

        public static DataTable CreateDataTable_Netus2_Log_JctPersonRole()
        {
            DataTable dtLogJctPersonRole = new DataTable("Log_JctPersonRole");
            DataColumn[] PrimaryKeycolumns = new DataColumn[1];
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "log_jct_person_role_id";
            PrimaryKeycolumns[0] = dtColumn;
            dtLogJctPersonRole.Columns.Add(dtColumn);
            dtLogJctPersonRole.PrimaryKey = PrimaryKeycolumns;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "person_id";
            dtLogJctPersonRole.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_role_id";
            dtLogJctPersonRole.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "log_date";
            dtLogJctPersonRole.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "log_user";
            dtLogJctPersonRole.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "enum_log_action_id";
            dtLogJctPersonRole.Columns.Add(dtColumn);

            return dtLogJctPersonRole;
        }
    }
}
