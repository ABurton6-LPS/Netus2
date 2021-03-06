using System;
using System.Collections.Generic;
using System.Text;

namespace Netus2_DatabaseConnection.dbAccess
{
    public static class TempTableFactory
    {
        public static void Create_JctPersonAddress()
        {
            try
            {
                Drop_JctPersonAddress();
            }
           catch(Exception e)
            {
                if (e.Message.Contains("does not exist") == false)
                    throw;
            }
            

            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            string sql =
                    "CREATE TABLE temp_jct_person_address ("
                    + "person_id int,"
                    + "address_id int)";

            connection.ExecuteNonQuery(sql);
        }

        public static void Drop_JctPersonAddress()
        {
            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            string drop_sql =
                "DROP TABLE temp_jct_person_address";

            connection.ExecuteNonQuery(drop_sql);
        }

        public static void Create_JctPersonPhoneNumber()
        {
            try
            {
                Drop_JctPersonPhoneNumber();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("does not exist") == false)
                    throw;
            }


            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            string sql =
                    "CREATE TABLE temp_jct_person_phone_number ("
                    + "person_id int,"
                    + "phone_number_id int)";

            connection.ExecuteNonQuery(sql);
        }

        public static void Drop_JctPersonPhoneNumber()
        {
            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            string drop_sql =
                "DROP TABLE temp_jct_person_phone_number";

            connection.ExecuteNonQuery(drop_sql);
        }

        public static void Create_JctEnrollmentClassEnrolled()
        {
            try
            {
                Drop_JctEnrollmentClassEnrolled();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("does not exist") == false)
                    throw;
            }

            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            string sql =
                "CREATE TABLE temp_jct_enrollment_class_enrolled ("
                + "enrollment_id int,"
                + "class_enrolled_id int)";

            connection.ExecuteNonQuery(sql);
        }

        public static void Drop_JctEnrollmentClassEnrolled()
        {
            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            string drop_sql =
                "DROP TABLE temp_jct_enrollment_class_enrolled";

            connection.ExecuteNonQuery(drop_sql);
        }

        public static void Create_JctPersonEmail()
        {
            try
            {
                Drop_JctPersonEmail();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("does not exist") == false)
                    throw;
            }

            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            string sql =
                "CREATE TABLE temp_jct_person_email ("
                + "person_id int,"
                + "email_id int)";

            connection.ExecuteNonQuery(sql);
        }

        public static void Drop_JctPersonEmail()
        {
            IConnectable connection = DbConnectionFactory.GetNetus2Connection();

            string drop_sql =
                "DROP TABLE temp_jct_person_email";

            connection.ExecuteNonQuery(drop_sql);
        }
    }
}