using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctPersonPersonDaoImpl : IJctPersonPersonDao
    {
        public void Delete(int personOneId, int personTwoId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM jct_person_person WHERE 1=1 ");
            sql.Append("AND person_one_id = " + personOneId + " ");
            sql.Append("AND person_two_id = " + personTwoId);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public List<DataRow> Read(int personOneId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_person WHERE 1=1 ");
            sql.Append("AND person_one_id = " + personOneId);

            return Read(sql.ToString(), connection);
        }

        public DataRow Read(int personOneId, int personTwoId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_person WHERE 1=1 ");
            sql.Append("AND person_one_id = " + personOneId + " ");
            sql.Append("AND person_two_id = " + personTwoId);

            List<DataRow> results = Read(sql.ToString(), connection);
            if (results.Count == 0)
                return null;
            if (results.Count == 1)
                return results[0];
            else
                throw new Exception("The jct_person_person table contains a duplicate record.\n" +
                    "person_one_id = " + personOneId + ", person_two_id = " + personTwoId);
        }

        private List<DataRow> Read(string sql, IConnectable connection)
        {
            DataTable dtJctPersonPerson = new DataTableFactory().Dt_Netus2_JctPersonPerson;
            dtJctPersonPerson = connection.ReadIntoDataTable(sql, dtJctPersonPerson);

            List<DataRow> jctPersonPersonDaos = new List<DataRow>();
            foreach (DataRow row in dtJctPersonPerson.Rows)
            {
                jctPersonPersonDaos.Add(row);
            }

            return jctPersonPersonDaos;
        }

        public DataRow Write(int personOneId, int personTwoId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_person_person (person_one_id, person_two_id) VALUES (");
            sql.Append(personOneId + ", ");
            sql.Append(personTwoId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            DataRow jctPersonPersonDao = new DataTableFactory().Dt_Netus2_JctPersonPerson.NewRow();
            jctPersonPersonDao["person_one_id"] = personOneId;
            jctPersonPersonDao["person_two_id"] = personTwoId;

            return jctPersonPersonDao;
        }
    }
}
