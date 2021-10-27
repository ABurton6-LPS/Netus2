using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
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
            if(phoneNumber.Id <= 0)
                throw new Exception("Cannot delete a phone number which doesn't have a database-assigned ID.\n" + phoneNumber.ToString());

            string sql = "DELETE FROM phone_number WHERE " +
                "phone_number_id = @phone_number_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@phone_number_id", phoneNumber.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public List<PhoneNumber> Read(PhoneNumber phoneNumber, IConnectable connection)
        {
            return Read(phoneNumber, -1, connection);
        }

        public List<PhoneNumber> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM phone_number WHERE person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            List<PhoneNumber> result = Read(sql, connection, parameters);

            return result;
        }

        public List<PhoneNumber> Read(PhoneNumber phoneNumber, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPhoneNumber(phoneNumber, personId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM phone_number WHERE 1=1 ");
            if (row["phone_number_id"] != DBNull.Value)
            {
                sql.Append("AND phone_number_id = @phone_number_id");
                parameters.Add(new SqlParameter("@phone_number_id", phoneNumber.Id));
            }
            else
            {
                if (row["person_id"] != DBNull.Value)
                {
                    sql.Append("AND person_id = @person_id ");
                    parameters.Add(new SqlParameter("@person_id", row["person_id"]));
                }

                if (row["phone_number"] != DBNull.Value)
                {
                    sql.Append("AND phone_number = @phone_number ");
                    parameters.Add(new SqlParameter("@phone_number", row["phone_number"]));
                }

                if (row["is_primary_id"] != DBNull.Value)
                {
                    sql.Append("AND is_primary_id = @is_primary_id ");
                    parameters.Add(new SqlParameter("@is_primary_id", row["is_primary_id"]));
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
            Update(phoneNumber, -1, connection);
        }

        public void Update(PhoneNumber phoneNumber, int personId, IConnectable connection)
        {
            List<PhoneNumber> foundPhoneNumbers = Read(phoneNumber, personId, connection);
            if (foundPhoneNumbers.Count == 0)
                Write(phoneNumber, personId, connection);
            else if (foundPhoneNumbers.Count == 1)
            {
                phoneNumber.Id = foundPhoneNumbers[0].Id;
                UpdateInternals(phoneNumber, personId, connection);
            }
            else if (foundPhoneNumbers.Count > 1)
                throw new Exception(foundPhoneNumbers.Count + " Phone Numbers found matching the description of:\n" +
                    phoneNumber.ToString());
        }

        private void UpdateInternals(PhoneNumber phoneNumber, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPhoneNumber(phoneNumber, personId);

            if (row["phone_number_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE phone_number SET ");
                if (row["person_id"] != DBNull.Value)
                {
                    sql.Append("person_id = @person_id, ");
                    parameters.Add(new SqlParameter("@person_id", row["person_id"]));
                }
                else
                    sql.Append("person_id = NULL, ");

                if (row["phone_number"] != DBNull.Value)
                {
                    sql.Append("phone_number = @phone_number, ");
                    parameters.Add(new SqlParameter("@phone_number", row["phone_number"]));
                }
                else
                    sql.Append("phone_number = NULL, ");

                if (row["is_primary_id"] != DBNull.Value)
                {
                    sql.Append("is_primary_id = @is_primary_id, ");
                    parameters.Add(new SqlParameter("@is_primary_id", row["is_primary_id"]));
                }
                else
                    sql.Append("is_primary_id = NULL, ");

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
            {
                throw new Exception("The following Phone Number needs to be inserted into the database, before it can be updated.\n" + phoneNumber.ToString());
            }
        }

        public PhoneNumber Write(PhoneNumber phoneNumber, IConnectable connection)
        {
            return Write(phoneNumber, -1, connection);
        }

        public PhoneNumber Write(PhoneNumber phoneNumber, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPhoneNumber(phoneNumber, personId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["person_id"] != DBNull.Value)
            {
                sqlValues.Append("@person_id, ");
                parameters.Add(new SqlParameter("@person_id", row["person_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["phone_number"] != DBNull.Value)
            {
                sqlValues.Append("@phone_number, ");
                parameters.Add(new SqlParameter("@phone_number", row["phone_number"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["is_primary_id"] != DBNull.Value)
            {
                sqlValues.Append("@is_primary_id, ");
                parameters.Add(new SqlParameter("@is_primary_id", row["is_primary_id"]));
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
                "(person_id, phone_number, is_primary_id, enum_phone_id, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["phone_number_id"] = connection.InsertNewRecord(sql, parameters);

            PhoneNumber result = daoObjectMapper.MapPhoneNumber(row);

            return result;
        }
    }
}
