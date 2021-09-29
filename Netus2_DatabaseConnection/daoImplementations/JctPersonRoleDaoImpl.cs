using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class JctPersonRoleDaoImpl : IJctPersonRoleDao
    {
        public void Delete(int personId, int roleId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("DELETE FROM jct_person_role WHERE 1=1 ");
            sql.Append("AND person_id = " + personId + " ");
            sql.Append("AND enum_role_id = " + roleId);

            connection.ExecuteNonQuery(sql.ToString());
        }

        public DataRow Read(int personId, int roleId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_role WHERE 1=1 ");
            sql.Append("AND person_id = " + personId + " ");
            sql.Append("AND enum_role_id = " + roleId);

            List<DataRow> results = Read(sql.ToString(), connection);
            if (results.Count == 1)
                return results[0];
            else if (results.Count == 0)
                return null;
            else
                throw new Exception("The jct_person_role table contains a duplicate record.\n" +
                    "person_id = " + personId + ", role_id = " + roleId);
        }

        public List<DataRow> Read(int personId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("SELECT * FROM jct_person_role WHERE 1=1 ");
            sql.Append("AND person_id = " + personId);

            return Read(sql.ToString(), connection);
        }

        private List<DataRow> Read(string sql, IConnectable connection)
        {
            DataTable dtJctPersonRole = DataTableFactory.CreateDataTable_Netus2_JctPersonRole();
            dtJctPersonRole = connection.ReadIntoDataTable(sql, dtJctPersonRole);

            List<DataRow> jctPersonRoleDaos = new List<DataRow>();
            foreach (DataRow row in dtJctPersonRole.Rows)
            {
                jctPersonRoleDaos.Add(row);
            }

            return jctPersonRoleDaos;
        }

        public DataRow Write(int personId, int roleId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO jct_person_role (person_id, enum_role_id) VALUES (");
            sql.Append(personId + ", ");
            sql.Append(roleId + ")");

            connection.ExecuteNonQuery(sql.ToString());

            DataRow jctPersonRoleDao = DataTableFactory.CreateDataTable_Netus2_JctPersonRole().NewRow();
            jctPersonRoleDao["person_id"] = personId;
            jctPersonRoleDao["enum_role_id"] = roleId;

            return jctPersonRoleDao;
        }
    }
}
