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
    public class JctClassPersonDaoImpl : IJctClassPersonDao
    {
        public void Delete(int classId, int personId, IConnectable connection)
        {
            if (classId <= 0 || personId <= 0)
                throw new Exception("Cannot delete a record from jct_class_person " +
                    "without a database-assigned ID for classId and personId." +
                    "\nclassId: " + classId +
                    "\npersonId: " + personId);

            string sql = "DELETE FROM jct_class_person WHERE 1=1 " +
                "AND class_id = @class_id " +
                "AND person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classId));
            parameters.Add(new SqlParameter("@person_id", personId));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public DataRow Read(int classId, int personId, int roleId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_person WHERE 1=1 " +
                "AND class_id = @class_id " +
                "AND person_id = @person_id " +
                "AND enum_role_id = @enum_role_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classId));
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@enum_role_id", roleId));

            List<DataRow> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching classId: " + classId + " and personId: " + personId);
        }

        public List<DataRow> Read_AllWithClassId(int classId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_person WHERE " +
                "class_id = @class_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_person WHERE " +
                "person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            return Read(sql, connection, parameters);
        }

        public List<DataRow> Read_AllWithRoleId(int roleId, IConnectable connection)
        {
            string sql = "SELECT * FROM jct_class_person WHERE " +
                "enum_role_id = @enum_role_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enum_role_id", roleId));

            return Read(sql, connection, parameters);
        }

        private List<DataRow> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtJctClassPerson = DataTableFactory.CreateDataTable_Netus2_JctClassPerson();
            dtJctClassPerson = connection.ReadIntoDataTable(sql, dtJctClassPerson, parameters);

            List<DataRow> jctClassPersonDaos = new List<DataRow>();
            foreach (DataRow row in dtJctClassPerson.Rows)
                jctClassPersonDaos.Add(row);

            return jctClassPersonDaos;
        }

        public DataRow Write(int classId, int personId, int roleId, IConnectable connection)
        {
            string sql = "INSERT INTO jct_class_person (" +
                "class_id, person_id, enum_role_id) VALUES (" +
                "@class_id, @person_id, @enum_role_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classId));
            parameters.Add(new SqlParameter("@person_id", personId));
            parameters.Add(new SqlParameter("@enum_role_id", roleId));

            connection.ExecuteNonQuery(sql, parameters);

            DataRow jctClassPersonDao = DataTableFactory.CreateDataTable_Netus2_JctClassPerson().NewRow();
            jctClassPersonDao["class_id"] = classId;
            jctClassPersonDao["person_id"] = personId;
            jctClassPersonDao["enum_role_id"] = roleId;

            return jctClassPersonDao;
        }
    }
}
