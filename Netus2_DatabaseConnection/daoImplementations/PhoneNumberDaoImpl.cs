using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class PhoneNumberDaoImpl : IPhoneNumberDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();
        public int? _taskId = null;

        public void SetTaskId(int taskId)
        {
            _taskId = taskId;
        }

        public int? GetTaskId()
        {
            return _taskId;
        }

        public void Delete(PhoneNumber phoneNumber, IConnectable connection)
        {
            if (phoneNumber.Id <= 0)
                throw new Exception("Cannot delete a phone number which doesn't have a database-assigned ID.\n" + phoneNumber.ToString());

            Delete_JctPersonPhoneNumber(phoneNumber, connection);

            if (phoneNumber.Id <= 0)
                throw new Exception("Cannot delete a phone number which doesn't have a database-assigned ID.\n" + phoneNumber.ToString());

            string sql = "DELETE FROM phone_number WHERE " +
                "phone_number_id = @phone_number_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@phone_number_id", phoneNumber.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void Delete_JctPersonPhoneNumber(PhoneNumber phoneNumber, IConnectable connection)
        {
            IJctPersonPhoneNumberDao jctPersonPhoneNumberDaoImpl = DaoImplFactory.GetJctPersonPhoneNumberDaoImpl();
            List<DataRow> foundRecords = jctPersonPhoneNumberDaoImpl.Read_AllWithPhoneNumberId(phoneNumber.Id, connection);

            foreach (DataRow row in foundRecords)
            {
                jctPersonPhoneNumberDaoImpl.Delete((int)row["person_id"], phoneNumber.Id, connection);
            }
        }

        public PhoneNumber Read_WithPhoneNumberValue(string phoneNumberValue, IConnectable connection)
        {
            string sql = "SELECT * FROM phone_number WHERE phone_number_value = @phone_number_value";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@phone_number_value", phoneNumberValue));

            List<PhoneNumber> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching phone number value: " + phoneNumberValue);
        }

        public PhoneNumber Read_WithPhoneNumberId(int phoneNumberId, IConnectable connection)
        {
            string sql = "SELECT * FROM phone_number WHERE phone_number_id = @phone_number_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@phone_number_id", phoneNumberId));

            List<PhoneNumber> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching phoneNumberId: " + phoneNumberId);
        }

        public List<PhoneNumber> Read(PhoneNumber phoneNumber, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPhoneNumber(phoneNumber);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM phone_number WHERE 1=1 ");
            if (row["phone_number_id"] != DBNull.Value)
            {
                sql.Append("AND phone_number_id = @phone_number_id");
                parameters.Add(new SqlParameter("@phone_number_id", phoneNumber.Id));
            }
            else
            {
                if (row["phone_number_value"] != DBNull.Value)
                {
                    sql.Append("AND phone_number_value = @phone_number_value ");
                    parameters.Add(new SqlParameter("@phone_number_value", row["phone_number_value"]));
                }

                if (row["enum_phone_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_phone_id = @enum_phone_id ");
                    parameters.Add(new SqlParameter("@enum_phone_id", row["enum_phone_id"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<PhoneNumber> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtPhoneNumber = DataTableFactory.CreateDataTable_Netus2_PhoneNumber();
            dtPhoneNumber = connection.ReadIntoDataTable(sql, dtPhoneNumber, parameters);

            List<PhoneNumber> results = new List<PhoneNumber>();
            foreach (DataRow row in dtPhoneNumber.Rows)
                results.Add(daoObjectMapper.MapPhoneNumber(row));

            return results;
        }
        
        public void Update(PhoneNumber phoneNumber, IConnectable connection)
        {
            List<PhoneNumber> foundPhoneNumbers = Read(phoneNumber, connection);
            if (foundPhoneNumbers.Count == 0)
                Write(phoneNumber, connection);
            else if (foundPhoneNumbers.Count == 1)
            {
                phoneNumber.Id = foundPhoneNumbers[0].Id;
                UpdateInternals(phoneNumber, connection);
            }
            else if (foundPhoneNumbers.Count > 1)
                throw new Exception(foundPhoneNumbers.Count + " Phone Numbers found matching the description of:\n" +
                    phoneNumber.ToString());
        }

        private void UpdateInternals(PhoneNumber phoneNumber, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPhoneNumber(phoneNumber);

            if (row["phone_number_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE phone_number SET ");
                if (row["phone_number_value"] != DBNull.Value)
                {
                    sql.Append("phone_number_value = @phone_number_value, ");
                    parameters.Add(new SqlParameter("@phone_number_value", row["phone_number_value"]));
                }
                else
                    sql.Append("phone_number_value = NULL, ");

                if (row["enum_phone_id"] != DBNull.Value)
                {
                    sql.Append("enum_phone_id = @enum_phone_id, ");
                    parameters.Add(new SqlParameter("@enum_phone_id", row["enum_phone_id"]));
                }
                else
                    sql.Append("enum_phone_id = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE phone_number_id = @phone_number_id");
                parameters.Add(new SqlParameter("@phone_number_id", row["phone_number_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);
            }
            else
                throw new Exception("The following Phone Number needs to be inserted into the database, before it can be updated.\n" + phoneNumber.ToString());
        }

        public PhoneNumber Write(PhoneNumber phoneNumber, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPhoneNumber(phoneNumber);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["phone_number_value"] != DBNull.Value)
            {
                sqlValues.Append("@phone_number_value, ");
                parameters.Add(new SqlParameter("@phone_number_value", row["phone_number_value"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_phone_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_phone_id, ");
                parameters.Add(new SqlParameter("@enum_phone_id", row["enum_phone_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql = "INSERT INTO phone_number " +
                "(phone_number_value, enum_phone_id, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["phone_number_id"] = connection.InsertNewRecord(sql, parameters);

            PhoneNumber result = daoObjectMapper.MapPhoneNumber(row);
            
            return result;
        }
    }
}
