using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dbAccess;
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

        public JctPersonAddressDao Read(int personId, int addressId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_address WHERE 1=1 ");
            sql.Append("AND person_id = " + personId + " ");
            sql.Append("AND address_id = " + addressId);

            List<JctPersonAddressDao> results = Read(sql.ToString(), connection);
            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception("The jct_person_address table contains a duplicate record.\n" +
                    "person_id = " + personId + ", address_Id = " + addressId);
        }

        public List<JctPersonAddressDao> Read_WithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_address where person_id = " + personId;

            return Read(sql, connection);
        }
        public List<JctPersonAddressDao> Read_WithAddressId(int addressId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_address where address_id = " + addressId;

            return Read(sql, connection);
        }

        private List<JctPersonAddressDao> Read(string sql, IConnectable connection)
        {
            List<JctPersonAddressDao> jctPersonAddressDaos = new List<JctPersonAddressDao>();

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    JctPersonAddressDao foundJctPersonAddressDao = new JctPersonAddressDao();
                    foundJctPersonAddressDao.person_id = reader.GetInt32(0);
                    foundJctPersonAddressDao.address_id = reader.GetInt32(1);
                    jctPersonAddressDaos.Add(foundJctPersonAddressDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return jctPersonAddressDaos;
        }

        public JctPersonAddressDao Write(int personId, int addressId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_person_address (person_id, address_id) VALUES (");
            sql.Append(personId + ", ");
            sql.Append(addressId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            JctPersonAddressDao jctPersonAddressDao = new JctPersonAddressDao();
            jctPersonAddressDao.person_id = personId;
            jctPersonAddressDao.address_id = addressId;

            return jctPersonAddressDao;
        }
    }
}
