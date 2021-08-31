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
    public class AddressDaoImpl : IAddressDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(Address address, IConnectable connection)
        {
            Delete_JctPersonAddress(address.Id, connection);

            DataRow row = daoObjectMapper.MapAddress(address);

            StringBuilder sql = new StringBuilder("DELETE FROM address WHERE 1=1 ");
            sql.Append("AND address_id " + (row["address_id"] != DBNull.Value ? "= " + row["address_id"] + " " : "IS NULL "));
            sql.Append("AND address_line_1 " + (row["address_line_1"] != DBNull.Value ? "LIKE '" + row["address_line_1"] + "' " : "IS NULL "));
            sql.Append("AND address_line_2 " + (row["address_line_2"] != DBNull.Value ? "LIKE '" + row["address_line_2"] + "' " : "IS NULL "));
            sql.Append("AND address_line_3 " + (row["address_line_3"] != DBNull.Value ? "LIKE '" + row["address_line_3"] + "' " : "IS NULL "));
            sql.Append("AND address_line_4 " + (row["address_line_4"] != DBNull.Value ? "LIKE '" + row["address_line_4"] + "' " : "IS NULL "));
            sql.Append("AND apartment " + (row["apartment"] != DBNull.Value ? "LIKE '" + row["apartment"] + "'" : "IS NULL "));
            sql.Append("AND city " + (row["city"] != DBNull.Value ? "LIKE '" + row["city"] + "'" : "IS NULL "));
            sql.Append("AND enum_state_province_id " + (row["enum_state_province_id"] != DBNull.Value ? "= " + row["enum_state_province_id"] + " " : "IS NULL "));
            sql.Append("AND postal_code " + (row["postal_code"] != DBNull.Value ? "LIKE '" + row["postal_code"] + "'" : "IS NULL "));
            sql.Append("AND enum_country_id " + (row["enum_country_id"] != DBNull.Value ? "= " + row["enum_country_id"] + " " : "IS NULL "));
            sql.Append("AND is_current_id " + (row["is_current_id"] != DBNull.Value ? "= " + row["is_current_id"] + " " : "IS NULL "));
            sql.Append("AND enum_address_id " + (row["enum_address_id"] != DBNull.Value ? "= " + row["enum_address_id"] + " " : "IS NULL "));


            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_JctPersonAddress(int addressId, IConnectable connection)
        {
            IJctPersonAddressDao jctPersonAddressDaoImpl = DaoImplFactory.GetJctPersonAddressDaoImpl();
            List<DataRow> foundDataRows =
                jctPersonAddressDaoImpl.Read_WithAddressId(addressId, connection);

            foreach (DataRow foundDataRow in foundDataRows)
            {
                int personId = (int)foundDataRow["person_id"];
                jctPersonAddressDaoImpl.Delete(personId, addressId, connection);
            }
        }

        public List<Address> Read(Address address, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            DataRow row = daoObjectMapper.MapAddress(address);

            sql.Append("SELECT * FROM address WHERE 1=1 ");
            if (row["address_id"] != DBNull.Value)
                sql.Append("AND address_id = " + row["address_id"] + " ");
            else
            {
                if (row["address_line_1"] != DBNull.Value)
                    sql.Append("AND address_line_1 LIKE '" + row["address_line_1"] + "' ");
                if (row["address_line_2"] != DBNull.Value)
                    sql.Append("AND address_line_2 LIKE '" + row["address_line_2"] + "' ");
                if (row["address_line_3"] != DBNull.Value)
                    sql.Append("AND address_line_3 LIKE '" + row["address_line_3"] + "' ");
                if (row["address_line_4"] != DBNull.Value)
                    sql.Append("AND address_line_4 LIKE '" + row["address_line_4"] + "' ");
                if (row["apartment"] != DBNull.Value)
                    sql.Append("AND apartment LIKE '" + row["apartment"] + "'");
                if (row["city"] != DBNull.Value)
                    sql.Append("AND city LIKE '" + row["city"] + "'");
                if (row["enum_state_province_id"] != DBNull.Value)
                    sql.Append("AND enum_state_province_id = " + row["enum_state_province_id"] + " ");
                if (row["postal_code"] != DBNull.Value)
                    sql.Append("AND postal_code LIKE '" + row["postal_code"] + "'");
                if (row["enum_country_id"] != DBNull.Value)
                    sql.Append("AND enum_country_id = " + row["enum_country_id"] + " ");
                if (row["is_current_id"] != DBNull.Value)
                    sql.Append("AND is_current_id = " + row["is_current_id"] + " ");
                if (row["enum_address_id"] != DBNull.Value)
                    sql.Append("AND enum_address_id = " + row["enum_address_id"] + " ");
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
            DataTable dtAddress = new DataTableFactory().Dt_Netus2_Address;
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtAddress.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<Address> results = new List<Address>();
            foreach (DataRow row in dtAddress.Rows)
            {
                results.Add(daoObjectMapper.MapAddress(row));
            }

            return results;
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
            DataRow row = daoObjectMapper.MapAddress(address);

            StringBuilder sql = new StringBuilder("UPDATE address SET ");
            sql.Append("address_line_1 = " + (row["address_line_1"] != DBNull.Value ? "'" + row["address_line_1"] + "', " : "NULL, "));
            sql.Append("address_line_2 = " + (row["address_line_2"] != DBNull.Value ? "'" + row["address_line_2"] + "', " : "NULL, "));
            sql.Append("address_line_3 = " + (row["address_line_3"] != DBNull.Value ? "'" + row["address_line_3"] + "', " : "NULL, "));
            sql.Append("address_line_4 = " + (row["address_line_4"] != DBNull.Value ? "'" + row["address_line_4"] + "', " : "NULL, "));
            sql.Append("apartment = " + (row["apartment"] != DBNull.Value ? "'" + row["apartment"] + "', " : "NULL, "));
            sql.Append("city = " + (row["city"] != DBNull.Value ? "'" + row["city"] + "', " : "NULL, "));
            sql.Append("enum_state_province_id = " + (row["enum_state_province_id"] != DBNull.Value ? row["enum_state_province_id"] + ", " : "NULL, "));
            sql.Append("postal_code = " + (row["postal_code"] != DBNull.Value ? "'" + row["postal_code"] + "', " : "NULL, "));
            sql.Append("enum_country_id = " + (row["enum_country_id"] != DBNull.Value ? row["enum_country_id"] + ", " : "NULL, "));
            sql.Append("is_current_id = " + (row["is_current_id"] != DBNull.Value ? row["is_current_id"] + ", " : "NULL, "));
            sql.Append("enum_address_id = " + (row["enum_address_id"] != DBNull.Value ? row["enum_address_id"] + ", " : "NULL, "));
            sql.Append("changed = GETDATE(), ");
            sql.Append("changed_by = 'Netus2' ");
            sql.Append("WHERE address_id = " + row["address_id"]);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public Address Write(Address address, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapAddress(address);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(row["address_line_1"] != DBNull.Value ? "'" + row["address_line_1"] + "', " : "NULL, ");
            sqlValues.Append(row["address_line_2"] != DBNull.Value ? "'" + row["address_line_2"] + "', " : "NULL, ");
            sqlValues.Append(row["address_line_3"] != DBNull.Value ? "'" + row["address_line_3"] + "', " : "NULL, ");
            sqlValues.Append(row["address_line_4"] != DBNull.Value ? "'" + row["address_line_4"] + "', " : "NULL, ");
            sqlValues.Append(row["apartment"] != DBNull.Value ? "'" + row["apartment"] + "', " : "NULL, ");
            sqlValues.Append(row["city"] != DBNull.Value ? "'" + row["city"] + "', " : "NULL, ");
            sqlValues.Append(row["enum_state_province_id"] != DBNull.Value ? row["enum_state_province_id"] + ", " : "NULL, ");
            sqlValues.Append(row["postal_code"] != DBNull.Value ? "'" + row["postal_code"] + "', " : "NULL, ");
            sqlValues.Append(row["enum_country_id"] != DBNull.Value ? row["enum_country_id"] + ", " : "NULL, ");
            sqlValues.Append(row["is_current_id"] != DBNull.Value ? row["is_current_id"] + ", " : "NULL, ");
            sqlValues.Append(row["enum_address_id"] != DBNull.Value ? row["enum_address_id"] + ", " : "NULL, ");
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
