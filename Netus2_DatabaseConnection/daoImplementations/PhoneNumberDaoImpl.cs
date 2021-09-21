using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class PhoneNumberDaoImpl : IPhoneNumberDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(PhoneNumber phoneNumber, IConnectable connection)
        {
            Delete(phoneNumber, -1, connection);
        }

        public void Delete(PhoneNumber phoneNumber, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPhoneNumber(phoneNumber, personId);

            StringBuilder sql = new StringBuilder("DELETE FROM phone_number WHERE 1=1 ");
            sql.Append("AND phone_number_id " + (row["phone_number_id"] != DBNull.Value ? "= " + row["phone_number_id"] + " " : "IS NULL "));
            sql.Append("AND person_id " + (row["person_id"] != DBNull.Value ? "= " + row["person_id"] + " " : "IS NULL "));
            sql.Append("AND phone_number " + (row["phone_number"] != DBNull.Value ? "LIKE '" + row["phone_number"] + "' " : "IS NULL "));
            sql.Append("AND is_primary_id " + (row["is_primary_id"] != DBNull.Value ? "= " + row["is_primary_id"] + " " : "IS NULL "));
            sql.Append("AND enum_phone_id " + (row["enum_phone_id"] != DBNull.Value ? "= " + row["enum_phone_id"] + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        public List<PhoneNumber> Read(PhoneNumber phoneNumber, IConnectable connection)
        {
            return Read(phoneNumber, -1, connection);
        }

        public List<PhoneNumber> Read(PhoneNumber phoneNumber, int personId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            if (phoneNumber == null)
                sql.Append("SELECT * FROM phone_number WHERE person_id = " + personId);
            else
            {
                DataRow row = daoObjectMapper.MapPhoneNumber(phoneNumber, personId);

                sql.Append("SELECT * FROM phone_number WHERE 1=1 ");
                if (row["phone_number_id"] != DBNull.Value)
                    sql.Append("AND phone_number_id = " + row["phone_number_id"] + " ");
                else
                {
                    if (row["person_id"] != DBNull.Value)
                        sql.Append("AND person_id = " + row["person_id"] + " ");
                    if (row["phone_number"] != DBNull.Value)
                        sql.Append("AND phone_number LIKE '" + row["phone_number"] + "' ");
                    if (row["is_primary_id"] != DBNull.Value)
                        sql.Append("AND is_primary_id = " + row["is_primary_id"] + " ");
                    if (row["enum_phone_id"] != DBNull.Value)
                        sql.Append("AND enum_phone_id = " + row["enum_phone_id"] + " ");
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<PhoneNumber> Read(string sql, IConnectable connection)
        {
            DataTable dtPhoneNumber = new DataTableFactory().Dt_Netus2_PhoneNumber;
            dtPhoneNumber = connection.ReadIntoDataTable(sql, dtPhoneNumber);

            List<PhoneNumber> results = new List<PhoneNumber>();
            foreach (DataRow row in dtPhoneNumber.Rows)
            {
                results.Add(daoObjectMapper.MapPhoneNumber(row));
            }

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
                StringBuilder sql = new StringBuilder("UPDATE phone_number SET ");
                sql.Append("person_id = " + (row["person_id"] != DBNull.Value ? row["person_id"] + ", " : "NULL, "));
                sql.Append("phone_number = " + (row["phone_number"] != DBNull.Value ? "'" + row["phone_number"] + "', " : "NULL, "));
                sql.Append("is_primary_id = " + (row["is_primary_id"] != DBNull.Value ? row["is_primary_id"] + ", " : "NULL, "));
                sql.Append("enum_phone_id = " + (row["enum_phone_id"] != DBNull.Value ? row["enum_phone_id"] + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE phone_number_id = " + row["phone_number_id"]);

                connection.ExecuteNonQuery(sql.ToString());
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

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(row["person_id"] != DBNull.Value ? row["person_id"] + ", " : "NULL, ");
            sqlValues.Append(row["phone_number"] != DBNull.Value ? "'" + row["phone_number"] + "', " : "NULL, ");
            sqlValues.Append(row["is_primary_id"] != DBNull.Value ? row["is_primary_id"] + ", " : "NULL, ");
            sqlValues.Append(row["enum_phone_id"] != DBNull.Value ? row["enum_phone_id"] + ", " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            string sql = "INSERT INTO phone_number " +
                "(person_id, phone_number, is_primary_id, enum_phone_id, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["phone_number_id"] = connection.InsertNewRecord(sql);

            return daoObjectMapper.MapPhoneNumber(row);
        }
    }
}
