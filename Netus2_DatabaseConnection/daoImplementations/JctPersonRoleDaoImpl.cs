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
    public class JctPersonRoleDaoImpl : IJctPersonRoleDao
    {
        public void Delete(int personId, int roleId, IConnectable connection)
        {
            if (personId <= 0 || roleId < 0)
                throw new Exception("Cannot delete a record from jct_person_role " +
                    "without a database-assigned ID for both personId and roleId." +
                    "\npersonId: " + personId +
                    "\nroleId: " + roleId);

            string sql = "DELETE FROM jct_person_role WHERE 1=1 " +
            "AND person_id = @person_id " +
            "AND enum_role_id = @enum_role_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@enum_role_id", roleId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int personId, int roleId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_role WHERE 1=1 " +
            "AND person_id = @person_id " +
            "AND enum_role_id = @enum_role_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@enum_role_id", roleId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching personId: " + personId + " and roleId: " + roleId);
        }

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_role WHERE " +
            "person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllWithRoleId(int roleId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_person_role WHERE " +
                "enum_role_id = @enum_role_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enum_role_id", roleId));

            return Read(sql, connection, parameters);
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctPersonRole = DataTableFactory.CreateDataTable_Netus2_JctPersonRole();
            dtJctPersonRole = connection.ReadIntoDataTable(sql, dtJctPersonRole, parameters);

            List<DataRow> jctPersonRoleDaos = new List<DataRow>();
            foreach (DataRow row in dtJctPersonRole.Rows)
                jctPersonRoleDaos.Add(row);

            return jctPersonRoleDaos;
        }

        public DataRow Write(int personId, int roleId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_person_role (person_id, enum_role_id) VALUES (" +
                "@person_id, @enum_role_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@enum_role_id", roleId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctPersonRoleDao = DataTableFactory.CreateDataTable_Netus2_JctPersonRole().NewRow();
            jctPersonRoleDao["person_id"] = personId;
            jctPersonRoleDao["enum_role_id"] = roleId;

            return jctPersonRoleDao;
        }
    }
}
