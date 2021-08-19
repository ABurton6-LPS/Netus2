using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
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
            PhoneNumberDao phoneNumberDao = daoObjectMapper.MapPhoneNumber(phoneNumber, personId);

            StringBuilder sql = new StringBuilder("DELETE FROM phone_number WHERE 1=1 ");
            sql.Append("AND phone_number_id " + (phoneNumberDao.phone_number_id != null ? "= " + phoneNumberDao.phone_number_id + " " : "IS NULL "));
            sql.Append("AND person_id " + (phoneNumberDao.person_id != null ? "= " + phoneNumberDao.person_id + " " : "IS NULL "));
            sql.Append("AND phone_number " + (phoneNumberDao.phone_number != null ? "LIKE '" + phoneNumberDao.phone_number + "' " : "IS NULL "));
            sql.Append("AND is_primary_id " + (phoneNumberDao.is_primary_id != null ? "= " + phoneNumberDao.is_primary_id + " " : "IS NULL "));
            sql.Append("AND enum_phone_id " + (phoneNumberDao.enum_phone_id != null ? "= " + phoneNumberDao.enum_phone_id + " " : "IS NULL "));

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
                PhoneNumberDao phoneNumberDao = daoObjectMapper.MapPhoneNumber(phoneNumber, personId);

                sql.Append("SELECT * FROM phone_number WHERE 1=1 ");
                if (phoneNumberDao.phone_number_id != null)
                    sql.Append("AND phone_number_id = " + phoneNumberDao.phone_number_id + " ");
                else
                {
                    if (phoneNumberDao.person_id != null)
                        sql.Append("AND person_id = " + phoneNumberDao.person_id + " ");
                    if (phoneNumberDao.phone_number != null)
                        sql.Append("AND phone_number LIKE '" + phoneNumberDao.phone_number + "' ");
                    if (phoneNumberDao.is_primary_id != null)
                        sql.Append("AND is_primary_id = " + phoneNumberDao.is_primary_id + " ");
                    if (phoneNumberDao.enum_phone_id != null)
                        sql.Append("AND enum_phone_id = " + phoneNumberDao.enum_phone_id + " ");
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<PhoneNumber> Read(string sql, IConnectable connection)
        {
            List<PhoneNumberDao> foundPhoneNumbersDao = new List<PhoneNumberDao>();

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    PhoneNumberDao foundPhoneNumberDao = new PhoneNumberDao();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "phone_number_id":
                                if (value != DBNull.Value && value != null)
                                    foundPhoneNumberDao.phone_number_id = (int)value;
                                else
                                    foundPhoneNumberDao.phone_number_id = null;
                                break;
                            case "person_id":
                                if (value != DBNull.Value && value != null)
                                    foundPhoneNumberDao.person_id = (int)value;
                                else
                                    foundPhoneNumberDao.person_id = null;
                                break;
                            case "phone_number":
                                foundPhoneNumberDao.phone_number = value != DBNull.Value ? (string)value : null;
                                break;
                            case "is_primary_id":
                                if (value != DBNull.Value && value != null)
                                    foundPhoneNumberDao.is_primary_id = (int)value;
                                else
                                    foundPhoneNumberDao.is_primary_id = null;
                                break;
                            case "enum_phone_id":
                                if (value != DBNull.Value && value != null)
                                    foundPhoneNumberDao.enum_phone_id = (int)value;
                                else
                                    foundPhoneNumberDao.enum_phone_id = null;
                                break;
                            case "created":
                                if (value != DBNull.Value && value != null)
                                    foundPhoneNumberDao.created = (DateTime)value;
                                else
                                    foundPhoneNumberDao.created = null;
                                break;
                            case "created_by":
                                foundPhoneNumberDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case "changed":
                                if (value != DBNull.Value && value != null)
                                    foundPhoneNumberDao.changed = (DateTime)value;
                                else
                                    foundPhoneNumberDao.changed = null;
                                break;
                            case "changed_by":
                                foundPhoneNumberDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in phone_number table: " + columnName);
                        }
                    }
                    foundPhoneNumbersDao.Add(foundPhoneNumberDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<PhoneNumber> results = new List<PhoneNumber>();
            foreach (PhoneNumberDao foundPhoneNumberDao in foundPhoneNumbersDao)
            {
                results.Add(daoObjectMapper.MapPhoneNumber(foundPhoneNumberDao));
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
            PhoneNumberDao phoneNumberDao = daoObjectMapper.MapPhoneNumber(phoneNumber, personId);

            if (phoneNumberDao.phone_number_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE phone_number SET ");
                sql.Append("person_id = " + (phoneNumberDao.person_id != null ? phoneNumberDao.person_id + ", " : "NULL, "));
                sql.Append("phone_number = " + (phoneNumberDao.phone_number != null ? "'" + phoneNumberDao.phone_number + "', " : "NULL, "));
                sql.Append("is_primary_id = " + (phoneNumberDao.is_primary_id != null ? phoneNumberDao.is_primary_id + ", " : "NULL, "));
                sql.Append("enum_phone_id = " + (phoneNumberDao.enum_phone_id != null ? phoneNumberDao.enum_phone_id + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE phone_number_id = " + phoneNumberDao.phone_number_id);

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
            PhoneNumberDao phoneNumberDao = daoObjectMapper.MapPhoneNumber(phoneNumber, personId);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(phoneNumberDao.person_id != null ? phoneNumberDao.person_id + ", " : "NULL, ");
            sqlValues.Append(phoneNumberDao.phone_number != null ? "'" + phoneNumberDao.phone_number + "', " : "NULL, ");
            sqlValues.Append(phoneNumberDao.is_primary_id != null ? phoneNumberDao.is_primary_id + ", " : "NULL, ");
            sqlValues.Append(phoneNumberDao.enum_phone_id != null ? phoneNumberDao.enum_phone_id + ", " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            string sql = "INSERT INTO phone_number " +
                "(person_id, phone_number, is_primary_id, enum_phone_id, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            phoneNumberDao.phone_number_id = connection.InsertNewRecord(sql);

            return daoObjectMapper.MapPhoneNumber(phoneNumberDao);
        }
    }
}
