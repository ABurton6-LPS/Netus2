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
    public class AddressDaoImpl : IAddressDao
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

        public void Delete(Address address, IConnectable connection)
        {
            if(address.Id <= 0)
                throw new Exception("Cannot delete an address which doesn't have a database-assigned ID.\n" + address.ToString());

            Delete_JctPersonAddress(address.Id, connection);

            string sql = "DELETE FROM address WHERE " +
                "address_id = @address_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@address_id", address.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void Delete_JctPersonAddress(int addressId, IConnectable connection)
        {
            IJctPersonAddressDao jctPersonAddressDaoImpl = DaoImplFactory.GetJctPersonAddressDaoImpl();
            List<DataRow> foundDataRows =
                jctPersonAddressDaoImpl.Read_WithAllAddressId(addressId, connection);

            foreach (DataRow foundDataRow in foundDataRows)
            {
                int personId = (int)foundDataRow["person_id"];
                jctPersonAddressDaoImpl.Delete(personId, addressId, connection);
            }
        }

        public Address Read_UsingAddressId(int addressId, IConnectable connection)
        {
            string sql = "SELECT * FROM address WHERE address_id = @address_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@address_id", addressId));

            List<Address> results = Read(sql, connection, parameters);
            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching addressId: " + addressId);
        }

        public List<Address> Read(Address address, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapAddress(address);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM address WHERE 1=1 ");
            if (row["address_id"] != DBNull.Value)
            {
                sql.Append("AND address_id = @address_id");
                parameters.Add(new SqlParameter("@address_id", row["address_id"]));
            }
            else
            {
                if (row["address_line_1"] != DBNull.Value)
                {
                    sql.Append("AND address_line_1 = @address_line_1 ");
                    parameters.Add(new SqlParameter("@address_line_1", row["address_line_1"]));
                }

                if (row["address_line_2"] != DBNull.Value)
                {
                    sql.Append("AND address_line_2 = @address_line_2 ");
                    parameters.Add(new SqlParameter("@address_line_2", row["address_line_2"]));
                }

                if (row["city"] != DBNull.Value)
                {
                    sql.Append("AND city = @city ");
                    parameters.Add(new SqlParameter("@city", row["city"]));
                }

                if (row["enum_state_province_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_state_province_id = @enum_state_province_id ");
                    parameters.Add(new SqlParameter("@enum_state_province_id", row["enum_state_province_id"]));
                }

                if (row["postal_code"] != DBNull.Value)
                {
                    sql.Append("AND postal_code = @postal_code ");
                    parameters.Add(new SqlParameter("@postal_code", row["postal_code"]));
                }

                if (row["enum_country_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_country_id = @enum_country_id ");
                    parameters.Add(new SqlParameter("@enum_country_id", row["enum_country_id"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }



        public List<Address> Read_Exact(Address address, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapAddress(address);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM address WHERE 1=1 ");
            if (row["address_id"] != DBNull.Value)
            {
                sql.Append("AND address_id = @address_id");
                parameters.Add(new SqlParameter("@address_id", row["address_id"]));
            }
            else
            {
                if (row["address_line_1"] != DBNull.Value)
                {
                    sql.Append("AND address_line_1 = @address_line_1 ");
                    parameters.Add(new SqlParameter("@address_line_1", row["address_line_1"]));
                }
                else
                {
                    sql.Append("AND address_line_1 IS NULL ");
                }

                if (row["address_line_2"] != DBNull.Value)
                {
                    sql.Append("AND address_line_2 = @address_line_2 ");
                    parameters.Add(new SqlParameter("@address_line_2", row["address_line_2"]));
                }
                else
                {
                    sql.Append("AND address_line_2 IS NULL ");
                }

                if (row["city"] != DBNull.Value)
                {
                    sql.Append("AND city = @city ");
                    parameters.Add(new SqlParameter("@city", row["city"]));
                }
                else
                {
                    sql.Append("AND city IS NULL ");
                }

                if (row["enum_state_province_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_state_province_id = @enum_state_province_id ");
                    parameters.Add(new SqlParameter("@enum_state_province_id", row["enum_state_province_id"]));
                }
                else
                {
                    sql.Append("AND enum_state_province_id IS NULL ");
                }

                if (row["postal_code"] != DBNull.Value)
                {
                    sql.Append("AND postal_code = @postal_code ");
                    parameters.Add(new SqlParameter("@postal_code", row["postal_code"]));
                }
                else
                {
                    sql.Append("AND postal_code IS NULL ");
                }

                if (row["enum_country_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_country_id = @enum_country_id ");
                    parameters.Add(new SqlParameter("@enum_country_id", row["enum_country_id"]));
                }
                else
                {
                    sql.Append("AND enum_country_id IS NULL ");
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<Address> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtAddress = DataTableFactory.CreateDataTable_Netus2_Address();
            dtAddress = connection.ReadIntoDataTable(sql, dtAddress, parameters);

            List<Address> results = new List<Address>();
            foreach (DataRow row in dtAddress.Rows)
                results.Add(daoObjectMapper.MapAddress(row));

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

            if(row["address_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE address SET ");
                if (row["address_line_1"] != DBNull.Value)
                {
                    sql.Append("address_line_1 = @address_line_1, ");
                    parameters.Add(new SqlParameter("@address_line_1", row["address_line_1"]));
                }
                else
                    sql.Append("address_line_1 = NULL, ");

                if (row["address_line_2"] != DBNull.Value)
                {
                    sql.Append("address_line_2 = @address_line_2, ");
                    parameters.Add(new SqlParameter("@address_line_2", row["address_line_2"]));
                }
                else
                    sql.Append("address_line_2 = NULL, ");

                if (row["city"] != DBNull.Value)
                {
                    sql.Append("city = @city, ");
                    parameters.Add(new SqlParameter("@city", row["city"]));
                }
                else
                    sql.Append("city = NULL, ");

                if (row["enum_state_province_id"] != DBNull.Value)
                {
                    sql.Append("enum_state_province_id = @enum_state_province_id, ");
                    parameters.Add(new SqlParameter("@enum_state_province_id", row["enum_state_province_id"]));
                }
                else
                    sql.Append("enum_state_province_id = NULL, ");

                if (row["postal_code"] != DBNull.Value)
                {
                    sql.Append("postal_code = @postal_code, ");
                    parameters.Add(new SqlParameter("@postal_code", row["postal_code"]));
                }
                else
                    sql.Append("postal_code = NULL, ");

                if (row["enum_country_id"] != DBNull.Value)
                {
                    sql.Append("enum_country_id = @enum_country_id, ");
                    parameters.Add(new SqlParameter("@enum_country_id", row["enum_country_id"]));
                }
                else
                    sql.Append("enum_country_id = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE address_id = @address_id");
                parameters.Add(new SqlParameter("@address_id", row["address_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);
            }
            else
                throw new Exception("The following Address needs to be inserted into the database, before it can be updated.\n" + address.ToString());
        }

        public Address Write(Address address, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapAddress(address);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["address_line_1"] != DBNull.Value)
            {
                sqlValues.Append("@address_line_1, ");
                parameters.Add(new SqlParameter("@address_line_1", row["address_line_1"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["address_line_2"] != DBNull.Value)
            {
                sqlValues.Append("@address_line_2, ");
                parameters.Add(new SqlParameter("@address_line_2", row["address_line_2"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["city"] != DBNull.Value)
            {
                sqlValues.Append("@city, ");
                parameters.Add(new SqlParameter("@city", row["city"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_state_province_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_state_province_id, ");
                parameters.Add(new SqlParameter("@enum_state_province_id", row["enum_state_province_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["postal_code"] != DBNull.Value)
            {
                sqlValues.Append("@postal_code, ");
                parameters.Add(new SqlParameter("@postal_code", row["postal_code"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_country_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_country_id, ");
                parameters.Add(new SqlParameter("@enum_country_id", row["enum_country_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql = "INSERT INTO address " +
                "(address_line_1, address_line_2, " +
                "city, enum_state_province_id, postal_code, enum_country_id, " +
                "created, created_by) VALUES (" + sqlValues.ToString() + ")";

            row["address_id"] = connection.InsertNewRecord(sql, parameters);

            Address result = daoObjectMapper.MapAddress(row);

            return result;
        }
    }
}
