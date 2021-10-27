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
    public class JctPersonPersonDaoImpl : IJctPersonPersonDao
    {
        public void Delete(int personOneId, int personTwoId, IConnectable connection)
        {
            if (personOneId <= 0 || personTwoId <= 0)
                throw new Exception("Cannot delete a record from jct_person_person " +
                    "without a database-assigned ID for both personOneId and personTwoId." +
                    "\npersonOneId: " + personOneId +
                    "\npersonTwoId: " + personTwoId);

            string sql = "DELETE FROM jct_person_person WHERE 1=1 " +
            "AND person_one_id = @person_one_id " +
            "AND person_two_id = @person_two_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_one_id", personOneId));
            parameters.Add(new SqlParameter("@person_two_id", personTwoId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int personOneId, int personTwoId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_person WHERE 1=1 " +
            "AND person_one_id = @person_one_id " +
            "AND person_two_id = @person_two_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_one_id", personOneId));
            parameters.Add(new SqlParameter("@person_two_id", personTwoId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public List<DataRow> Read_AllWithPersonOneId(int personOneId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_person WHERE 1=1 " +
                "AND person_one_id = @person_one_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_one_id", personOneId));

            return Read(sql, connection, parameters);
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctPersonPerson = DataTableFactory.CreateDataTable_Netus2_JctPersonPerson();
            dtJctPersonPerson = connection.ReadIntoDataTable(sql, dtJctPersonPerson, parameters);

            List<DataRow> jctPersonPersonDaos = new List<DataRow>();
            foreach (DataRow row in dtJctPersonPerson.Rows)
                jctPersonPersonDaos.Add(row);

            return jctPersonPersonDaos;
        }

        public DataRow Write(int personOneId, int personTwoId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_person_person (" +
                "person_one_id, person_two_id) VALUES (" +
                "@person_one_id, @person_two_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_one_id", personOneId));
            parameters.Add(new SqlParameter("@person_two_id", personTwoId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctPersonPersonDao = DataTableFactory.CreateDataTable_Netus2_JctPersonPerson().NewRow();
            jctPersonPersonDao["person_one_id"] = personOneId;
            jctPersonPersonDao["person_two_id"] = personTwoId;

            return jctPersonPersonDao;
        }
    }
}
