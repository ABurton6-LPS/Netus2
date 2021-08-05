using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class AddressDaoImpl : IAddressDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(Address address, IConnectable connection)
        {
            Delete_JctPersonAddress(address.Id, connection);

            AddressDao addressDao = daoObjectMapper.MapAddress(address);

            StringBuilder sql = new StringBuilder("DELETE FROM address WHERE 1=1 ");
            sql.Append("AND address_id " + (addressDao.address_id != null ? "= " + addressDao.address_id + " " : "IS NULL "));
            sql.Append("AND address_line_1 " + (addressDao.address_line_1 != null ? "LIKE '" + addressDao.address_line_1 + "' " : "IS NULL "));
            sql.Append("AND address_line_2 " + (addressDao.address_line_2 != null ? "LIKE '" + addressDao.address_line_2 + "' " : "IS NULL "));
            sql.Append("AND address_line_3 " + (addressDao.address_line_3 != null ? "LIKE '" + addressDao.address_line_3 + "' " : "IS NULL "));
            sql.Append("AND address_line_4 " + (addressDao.address_line_4 != null ? "LIKE '" + addressDao.address_line_4 + "' " : "IS NULL "));
            sql.Append("AND apartment " + (addressDao.apartment != null ? "LIKE '" + addressDao.apartment + "'" : "IS NULL "));
            sql.Append("AND city " + (addressDao.city != null ? "LIKE '" + addressDao.city + "'" : "IS NULL "));
            sql.Append("AND enum_state_province_id " + (addressDao.enum_state_province_id != null ? "= " + addressDao.enum_state_province_id + " " : "IS NULL "));
            sql.Append("AND postal_code " + (addressDao.postal_code != null ? "LIKE '" + addressDao.postal_code + "'" : "IS NULL "));
            sql.Append("AND enum_country_id " + (addressDao.enum_country_id != null ? "= " + addressDao.enum_country_id + " " : "IS NULL "));
            sql.Append("AND is_current_id " + (addressDao.is_current_id != null ? "= " + addressDao.is_current_id + " " : "IS NULL "));
            sql.Append("AND enum_address_id " + (addressDao.enum_address_id != null ? "= " + addressDao.enum_address_id + " " : "IS NULL "));


            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_JctPersonAddress(int addressId, IConnectable connection)
        {
            IJctPersonAddressDao jctPersonAddressDaoImpl = new JctPersonAddressDaoImpl();
            List<JctPersonAddressDao> foundJctPersonAddressDaos =
                jctPersonAddressDaoImpl.Read_WithAddressId(addressId, connection);

            foreach (JctPersonAddressDao foundJctPersonAddressDao in foundJctPersonAddressDaos)
            {
                int personId = (int)foundJctPersonAddressDao.person_id;
                jctPersonAddressDaoImpl.Delete(personId, addressId, connection);
            }
        }

        public List<Address> Read(Address address, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            AddressDao addressDao = daoObjectMapper.MapAddress(address);

            sql.Append("SELECT * FROM address WHERE 1=1 ");
            if (addressDao.address_id != null)
                sql.Append("AND address_id = " + addressDao.address_id + " ");
            else
            {
                if (addressDao.address_line_1 != null)
                    sql.Append("AND address_line_1 LIKE '" + addressDao.address_line_1 + "' ");
                if (addressDao.address_line_2 != null)
                    sql.Append("AND address_line_2 LIKE '" + addressDao.address_line_2 + "' ");
                if (addressDao.address_line_3 != null)
                    sql.Append("AND address_line_3 LIKE '" + addressDao.address_line_3 + "' ");
                if (addressDao.address_line_4 != null)
                    sql.Append("AND address_line_4 LIKE '" + addressDao.address_line_4 + "' ");
                if (addressDao.apartment != null)
                    sql.Append("AND apartment LIKE '" + addressDao.apartment + "'");
                if (addressDao.city != null)
                    sql.Append("AND city LIKE '" + addressDao.city + "'");
                if (addressDao.enum_state_province_id != null)
                    sql.Append("AND enum_state_province_id = " + addressDao.enum_state_province_id + " ");
                if (addressDao.postal_code != null)
                    sql.Append("AND postal_code LIKE '" + addressDao.postal_code + "'");
                if (addressDao.enum_country_id != null)
                    sql.Append("AND enum_country_id = " + addressDao.enum_country_id + " ");
                if (addressDao.is_current_id != null)
                    sql.Append("AND is_current_id = " + addressDao.is_current_id + " ");
                if (addressDao.enum_address_id != null)
                    sql.Append("AND enum_address_id = " + addressDao.enum_address_id + " ");
            }

            return Read(sql.ToString(), connection);
        }

        public Address Read_UsingAdddressId(int addressId, IConnectable connection)
        {
            string sql = "SELECT * FROM address WHERE address_id = " + addressId;

            List<Address> results = Read(sql, connection);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        private List<Address> Read(string sql, IConnectable connection)
        {
            List<AddressDao> foundAddressDaos = new List<AddressDao>();
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    AddressDao foundAddressDao = new AddressDao();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "address_id":
                                if (value != DBNull.Value)
                                    foundAddressDao.address_id = (int)value;
                                else
                                    foundAddressDao.address_id = null;
                                break;
                            case "address_line_1":
                                foundAddressDao.address_line_1 = value != DBNull.Value ? (string)value : null;
                                break;
                            case "address_line_2":
                                foundAddressDao.address_line_2 = value != DBNull.Value ? (string)value : null;
                                break;
                            case "address_line_3":
                                foundAddressDao.address_line_3 = value != DBNull.Value ? (string)value : null;
                                break;
                            case "address_line_4":
                                foundAddressDao.address_line_4 = value != DBNull.Value ? (string)value : null;
                                break;
                            case "apartment":
                                foundAddressDao.apartment = value != DBNull.Value ? (string)value : null;
                                break;
                            case "city":
                                foundAddressDao.city = value != DBNull.Value ? (string)value : null;
                                break;
                            case "enum_state_province_id":
                                if (value != DBNull.Value)
                                    foundAddressDao.enum_state_province_id = (int)value;
                                else
                                    foundAddressDao.enum_state_province_id = null;
                                break;
                            case "postal_code":
                                foundAddressDao.postal_code = value != DBNull.Value ? (string)value : null;
                                break;
                            case "enum_country_id":
                                if (value != DBNull.Value)
                                    foundAddressDao.enum_country_id = (int)value;
                                else
                                    foundAddressDao.enum_country_id = null;
                                break;
                            case "is_current_id":
                                if (value != DBNull.Value)
                                    foundAddressDao.is_current_id = (int)value;
                                else
                                    foundAddressDao.is_current_id = null;
                                break;
                            case "enum_address_id":
                                if (value != DBNull.Value)
                                    foundAddressDao.enum_address_id = (int)value;
                                else
                                    foundAddressDao.enum_address_id = null;
                                break;
                            case "created":
                                if (value != DBNull.Value)
                                    foundAddressDao.created = (DateTime)value;
                                else
                                    foundAddressDao.created = null;
                                    break;
                            case "created_by":
                                foundAddressDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case "changed":
                                if (value != DBNull.Value)
                                    foundAddressDao.changed = (DateTime)value;
                                else
                                    foundAddressDao.changed = null;
                                break;
                            case "changed_by":
                                foundAddressDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in address table: " + columnName);
                        }
                    }
                    foundAddressDaos.Add(foundAddressDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<Address> foundAddresses = new List<Address>();
            foreach (AddressDao foundAddressDao in foundAddressDaos)
            {
                foundAddresses.Add(daoObjectMapper.MapAddress(foundAddressDao));
            }

            return foundAddresses;
        }

        public void Update(Address address, IConnectable connection)
        {
            List<Address> foundAddresses = Read(address, connection);
            if (foundAddresses.Count == 0)
                Write(address, connection);
            else if (foundAddresses.Count == 1)
            {
                address.Id = foundAddresses[0].Id;
                UpdateInternals(address, connection);
            }
            else
                throw new Exception(foundAddresses.Count + " Addresses found matching the description of:\n" +
                    address.ToString());
        }

        private void UpdateInternals(Address address, IConnectable connection)
        {
            AddressDao addressDao = daoObjectMapper.MapAddress(address);

            StringBuilder sql = new StringBuilder("UPDATE address SET ");
            sql.Append("address_line_1 = " + (addressDao.address_line_1 != null ? "'" + addressDao.address_line_1 + "', " : "NULL, "));
            sql.Append("address_line_2 = " + (addressDao.address_line_2 != null ? "'" + addressDao.address_line_2 + "', " : "NULL, "));
            sql.Append("address_line_3 = " + (addressDao.address_line_3 != null ? "'" + addressDao.address_line_3 + "', " : "NULL, "));
            sql.Append("address_line_4 = " + (addressDao.address_line_4 != null ? "'" + addressDao.address_line_4 + "', " : "NULL, "));
            sql.Append("apartment = " + (addressDao.apartment != null ? "'" + addressDao.apartment + "', " : "NULL, "));
            sql.Append("city = " + (addressDao.city != null ? "'" + addressDao.city + "', " : "NULL, "));
            sql.Append("enum_state_province_id = " + (addressDao.enum_state_province_id != null ? addressDao.enum_state_province_id + ", " : "NULL, "));
            sql.Append("postal_code = " + (addressDao.postal_code != null ? "'" + addressDao.postal_code + "', " : "NULL, "));
            sql.Append("enum_country_id = " + (addressDao.enum_country_id != null ? addressDao.enum_country_id + ", " : "NULL, "));
            sql.Append("is_current_id = " + (addressDao.is_current_id != null ? addressDao.is_current_id + ", " : "NULL, "));
            sql.Append("enum_address_id = " + (addressDao.enum_address_id != null ? addressDao.enum_address_id + ", " : "NULL, "));
            sql.Append("changed = GETDATE(), ");
            sql.Append("changed_by = 'Netus2' ");
            sql.Append("WHERE address_id = " + addressDao.address_id);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public Address Write(Address address, IConnectable connection)
        {
            AddressDao addressDao = daoObjectMapper.MapAddress(address);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(addressDao.address_line_1 != null ? "'" + addressDao.address_line_1 + "', " : "NULL, ");
            sqlValues.Append(addressDao.address_line_2 != null ? "'" + addressDao.address_line_2 + "', " : "NULL, ");
            sqlValues.Append(addressDao.address_line_3 != null ? "'" + addressDao.address_line_3 + "', " : "NULL, ");
            sqlValues.Append(addressDao.address_line_4 != null ? "'" + addressDao.address_line_4 + "', " : "NULL, ");
            sqlValues.Append(addressDao.apartment != null ? "'" + addressDao.apartment + "', " : "NULL, ");
            sqlValues.Append(addressDao.city != null ? "'" + addressDao.city + "', " : "NULL, ");
            sqlValues.Append(addressDao.enum_state_province_id != null ? addressDao.enum_state_province_id + ", " : "NULL, ");
            sqlValues.Append(addressDao.postal_code != null ? "'" + addressDao.postal_code + "', " : "NULL, ");
            sqlValues.Append(addressDao.enum_country_id != null ? addressDao.enum_country_id + ", " : "NULL, ");
            sqlValues.Append(addressDao.is_current_id != null ? addressDao.is_current_id + ", " : "NULL, ");
            sqlValues.Append(addressDao.enum_address_id != null ? addressDao.enum_address_id + ", " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            StringBuilder sql = new StringBuilder("INSERT INTO address " +
                "(address_line_1, address_line_2, address_line_3, address_line_4, apartment, " +
                "city, enum_state_province_id, postal_code, enum_country_id, is_current_id, enum_address_id, " +
                "created, created_by) VALUES (" + sqlValues.ToString() + ")");

            address.Id = connection.InsertNewRecord(sql.ToString());

            return address;
        }
    }
}
