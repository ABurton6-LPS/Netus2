using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctPersonAddressDaoImpl : IJctPersonAddressDao
    {
        public void Delete(int personId, int addressId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM jct_person_address WHERE 1=1 ");
            sql.Append("AND person_id = " + personId + " ");
            sql.Append("AND address_id = " + addressId);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public DataRow Read(int personId, int addressId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_address WHERE 1=1 ");
            sql.Append("AND person_id = " + personId + " ");
            sql.Append("AND address_id = " + addressId);

            List<DataRow> results = Read(sql.ToString(), connection);
            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception("The jct_person_address table contains a duplicate record.\n" +
                    "person_id = " + personId + ", address_Id = " + addressId);
        }

        public List<DataRow> Read_WithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_address WHERE person_id = " + personId;

            return Read(sql, connection);
        }
        public List<DataRow> Read_WithAddressId(int addressId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_address WHERE address_id = " + addressId;

            return Read(sql, connection);
        }

        private List<DataRow> Read(string sql, IConnectable connection)
        {
            DataTable dtJctPersonAddress = DataTableFactory.CreateDataTable_Netus2_JctPersonAddress();
            dtJctPersonAddress = connection.ReadIntoDataTable(sql, dtJctPersonAddress);

            List<DataRow> jctPersonAddressDaos = new List<DataRow>();
            foreach (DataRow row in dtJctPersonAddress.Rows)
            {
                jctPersonAddressDaos.Add(row);
            }

            return jctPersonAddressDaos;
        }

        public DataRow Write(int personId, int addressId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_person_address (person_id, address_id) VALUES (");
            sql.Append(personId + ", ");
            sql.Append(addressId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            DataRow jctPersonAddressDao = DataTableFactory.CreateDataTable_Netus2_JctPersonAddress().NewRow();
            jctPersonAddressDao["person_id"] = personId;
            jctPersonAddressDao["address_id"] = addressId;

            return jctPersonAddressDao;
        }
    }
}
