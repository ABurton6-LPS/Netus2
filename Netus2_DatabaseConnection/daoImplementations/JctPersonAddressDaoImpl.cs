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
    public class JctPersonAddressDaoImpl : IJctPersonAddressDao
    {
        public void Delete(int personId, int addressId, IConnectable connection)
        {
            if (personId <= 0 || addressId <= 0)
                throw new Exception("Cannot delete a record from jct_person_address " +
                    "without a database-assigned ID for both personId and addressId." +
                    "\npersonId: " + personId +
                    "\naddressId: " + addressId);

            string sql = "DELETE FROM jct_person_address WHERE 1=1 " +
            "AND person_id = @person_id " +
            "AND address_id = @address_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@address_id", addressId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int personId, int addressId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_address WHERE 1=1 " +
            "AND person_id = @person_id " +
            "AND address_id = @address_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@address_id", addressId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching personId: " + personId + " and addressId: " + addressId);
        }

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_address WHERE " +
                "person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_WithAllAddressId(int addressId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_address WHERE " +
                "address_id = @address_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@address_id", addressId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllAddressIsNotInTempTable(IConnectable connection)
        {
            string sql =
                "SELECT jpa.person_id, jpa.address_id " +
                "FROM jct_person_address jpa " +
                "WHERE jpa.address_id NOT IN ( " +
                "SELECT tjpa.address_id " +
                "FROM temp_jct_person_address tjpa " +
                "WHERE tjpa.person_id = jpa.person_id )";

            return Read(sql, connection, new List<SqlParameter>());
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctPersonAddress = DataTableFactory.CreateDataTable_Netus2_JctPersonAddress();
            dtJctPersonAddress = connection.ReadIntoDataTable(sql, dtJctPersonAddress, parameters);

            List<DataRow> jctPersonAddressDaos = new List<DataRow>();
            foreach (DataRow row in dtJctPersonAddress.Rows)
                jctPersonAddressDaos.Add(row);

            return jctPersonAddressDaos;
        }

        public void Update(int personId, int addressId, bool isPrimary, int enumAddressId, IConnectable connection)
        {
            DataRow foundJctPersonAddressDao = Read(personId, addressId, connection);

            if (foundJctPersonAddressDao == null)
                Write(personId, addressId, isPrimary, enumAddressId, connection);
            else
                UpdateInternals(personId, addressId, isPrimary, enumAddressId, connection);
        }

        private void UpdateInternals(int personId, int addressId, bool isPrimary, int enumAddressId, IConnectable connection)
        {
            string sql = "UPDATE jct_person_address SET " + 
                "is_primary = @is_pimary, " +
                "enum_address_id = @enum_address_id " + 
                "WHERE person_id = @person_id " + 
                "AND address_id = @address_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@address_id", addressId));
            parameters.Add(new SqlParameter("@is_primary", isPrimary));
            parameters.Add(new SqlParameter("@enum_address_id", enumAddressId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Write(int personId, int addressId, bool isPrimary, int enumAddressId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_person_address (" +
                "person_id, address_id, is_primary, enum_address_id) VALUES (" +
                "@person_id, @address_id, @is_primary, @enum_address_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@address_id", addressId));
            parameters.Add(new SqlParameter("@is_primary", isPrimary));
            parameters.Add(new SqlParameter("@enum_address_id", enumAddressId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctPersonAddressDao = DataTableFactory.CreateDataTable_Netus2_JctPersonAddress().NewRow();
            jctPersonAddressDao["person_id"] = personId;
            jctPersonAddressDao["address_id"] = addressId;
            jctPersonAddressDao["is_primary"] = isPrimary;
            jctPersonAddressDao["enum_address_id"] = enumAddressId;

            return jctPersonAddressDao;
        }

        public void Write_ToTempTable(int personId, int addressId, IConnectable connection)
        {
            string sql = "INSERT INTO temp_jct_person_address (" +
                "person_id, address_id) VALUES (" +
                "@person_id, @address_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@address_id", addressId));

            connection.ExecuteNonQuery(sql, parameters);
        }
    }
}
