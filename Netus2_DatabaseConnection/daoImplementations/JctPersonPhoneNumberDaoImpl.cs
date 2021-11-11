using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctPersonPhoneNumberDaoImpl : IJctPersonPhoneNumberDao
    {
        public void Delete(int personId, int phoneNumberId, IConnectable connection)
        {
            if (personId <= 0 || phoneNumberId <= 0)
                throw new Exception("Cannot delete a record from jct_person_phone_number " +
                    "without a database-assigned ID for both personId and phoneNumberId." +
                    "\npersonId: " + personId +
                    "\nphoneNumberId: " + phoneNumberId);

            string sql = "DELETE FROM jct_person_phone_number WHERE 1=1 " +
            "AND person_id = @person_id " +
            "AND phone_number_id = @phone_number_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@phone_number_id", phoneNumberId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int personId, int phoneNumberId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_phone_number WHERE 1=1 " +
            "AND person_id = @person_id " +
            "AND phone_number_id = @phone_number_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@phone_number_id", phoneNumberId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching personId: " + personId + " and phoneNumberId: " + phoneNumberId);
        }

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_phone_number WHERE " +
                "person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllWithPhoneNumberId(int phoneNumberId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_phone_number WHERE " +
                "phone_number_id = @phone_number_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@phone_number_id", phoneNumberId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllPhoneNumberIsNotInTempTable(IConnectable connection)
        {
            string sql =
                "SELECT jpp.person_id, jpp.phone_number_id " +
                "FROM jct_person_phone_number jpp " +
                "WHERE jpp.phone_number_id NOT IN ( " +
                "SELECT tjpp.phone_number_id " +
                "FROM temp_jct_person_phone_number tjpp " +
                "WHERE tjpp.person_id = jpp.person_id )";

            return Read(sql, connection, new List<SqlParameter>());
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctPersonphoneNumber = DataTableFactory.CreateDataTable_Netus2_JctPersonPhoneNumber();
            dtJctPersonphoneNumber = connection.ReadIntoDataTable(sql, dtJctPersonphoneNumber, parameters);
            
            List<DataRow> jctPersonphoneNumberDaos = new List<DataRow>();
            foreach (DataRow row in dtJctPersonphoneNumber.Rows)
                jctPersonphoneNumberDaos.Add(row);

            return jctPersonphoneNumberDaos;
        }

        public void Update(int personId, int phoneNumberId, int isPrimaryId, IConnectable connection)
        {
            DataRow foundJctPersonPhoneNumberDao = Read(personId, phoneNumberId, connection);

            if (foundJctPersonPhoneNumberDao == null)
                Write(personId, phoneNumberId, isPrimaryId, connection);
            else
                UpdateInternals(personId, phoneNumberId, isPrimaryId, connection);
        }

        private void UpdateInternals(int personId, int phoneNumberId, int isPrimaryId, IConnectable connection)
        {
            string sql = "UPDATE jct_person_phone_number SET " +
                "is_primary_id = @is_primary_id " +
                "WHERE person_id = @person_id " +
                "AND phone_number_id = @phone_number_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@is_primary_id", isPrimaryId));
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@phone_number_id", phoneNumberId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Write(int personId, int phoneNumberId, int isPrimaryId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_person_phone_number (" +
                "person_id, phone_number_id, is_primary_id) VALUES (" +
                "@person_id, @phone_number_id, @is_primary_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@phone_number_id", phoneNumberId));
            parameters.Add(new SqlParameter("@is_primary_id", isPrimaryId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctPersonPhoneNumberDao = DataTableFactory.CreateDataTable_Netus2_JctPersonPhoneNumber().NewRow();
            jctPersonPhoneNumberDao["person_id"] = personId;
            jctPersonPhoneNumberDao["phone_number_id"] = phoneNumberId;
            jctPersonPhoneNumberDao["is_primary_id"] = isPrimaryId;

            return jctPersonPhoneNumberDao;
        }

        public void Write_ToTempTable(int personId, int phoneNumberId, IConnectable connection)
        {
            string sql = "INSERT INTO temp_jct_person_phone_number (" +
                "person_id, address_id) VALUES (" +
                "@person_id, @phone_number_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@phone_number_id", phoneNumberId));

            connection.ExecuteNonQuery(sql, parameters);
        }
    }
}
