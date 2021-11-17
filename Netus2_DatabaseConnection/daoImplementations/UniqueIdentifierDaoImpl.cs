using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class UniqueIdentifierDaoImpl : IUniqueIdentifierDao
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

        public void Delete(UniqueIdentifier uniqueId, IConnectable connection)
        {
            if(uniqueId.Id <= 0)
                throw new Exception("Cannot delete a unique identifier which doesn't have a database-assigned ID.\n" + uniqueId.ToString());

            string sql = "DELETE FROM unique_identifier WHERE unique_identifier_id = @unique_identifier_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@unique_identifier_id", uniqueId.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        public List<UniqueIdentifier> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM unique_identifier WHERE person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            return Read(sql, connection, parameters);
        }

        public List<UniqueIdentifier> Read(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapUniqueIdentifier(uniqueId, personId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM unique_identifier WHERE 1=1 ");
            if (row["unique_identifier_id"] != DBNull.Value)
            {
                sql.Append("AND unique_identifier_id = @unique_identifier_id");
                parameters.Add(new SqlParameter("@unique_identifier_id", row["unique_identifier_id"]));
            }
            else
            {
                if (row["person_id"] != DBNull.Value)
                {
                    sql.Append("AND person_id = @person_id ");
                    parameters.Add(new SqlParameter("@person_id", row["person_id"]));
                }

                if (row["unique_identifier_value"] != DBNull.Value)
                {
                    sql.Append("AND unique_identifier_value = @unique_identifier_value ");
                    parameters.Add(new SqlParameter("@unique_identifier_value", row["unique_identifier_value"]));
                }

                if (row["enum_identifier_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_identifier_id = @enum_identifier_id ");
                    parameters.Add(new SqlParameter("@enum_identifier_id", row["enum_identifier_id"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<UniqueIdentifier> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtUniqueIdentifier = DataTableFactory.CreateDataTable_Netus2_UniqueIdentifier();
            dtUniqueIdentifier = connection.ReadIntoDataTable(sql, dtUniqueIdentifier, parameters);

            List<UniqueIdentifier> results = new List<UniqueIdentifier>();
            foreach (DataRow row in dtUniqueIdentifier.Rows)
                results.Add(daoObjectMapper.MapUniqueIdentifier(row));

            return results;
        }

        public void Update(UniqueIdentifier uniqueIdentifier, int personId, IConnectable connection)
        {
            List<UniqueIdentifier> foundUniqueIdentifiers = Read(uniqueIdentifier, personId, connection);
            if (foundUniqueIdentifiers.Count == 0)
                Write(uniqueIdentifier, personId, connection);
            else if (foundUniqueIdentifiers.Count == 1)
            {
                uniqueIdentifier.Id = foundUniqueIdentifiers[0].Id;
                UpdateInternals(uniqueIdentifier, personId, connection);
            }
            else if (foundUniqueIdentifiers.Count > 1)
                throw new Exception(foundUniqueIdentifiers.Count + " Unique Identifiers found matching the description of:\n" +
                    uniqueIdentifier.ToString());
        }

        private void UpdateInternals(UniqueIdentifier uniqueIdentifier, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapUniqueIdentifier(uniqueIdentifier, personId);

            if (row["unique_identifier_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE unique_identifier SET ");
                if (row["person_id"] != DBNull.Value)
                {
                    sql.Append("person_id = @person_id, ");
                    parameters.Add(new SqlParameter("@person_id", row["person_id"]));
                }
                else
                    sql.Append("person_id = NULL, ");

                if (row["unique_identifier_value"] != DBNull.Value)
                {
                    sql.Append("unique_identifier_value = @unique_identifier_value, ");
                    parameters.Add(new SqlParameter("@unique_identifier_value", row["unique_identifier_value"]));
                }
                else
                    sql.Append("unique_identifier_value = NULL, ");

                if (row["enum_identifier_id"] != DBNull.Value)
                {
                    sql.Append("enum_identifier_id = @enum_identifier_id, ");
                    parameters.Add(new SqlParameter("@enum_identifier_id", row["enum_identifier_id"]));
                }
                else
                    sql.Append("enum_identifier_id = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE unique_identifier_id = @unique_identifier_id");
                parameters.Add(new SqlParameter("@unique_identifier_id", row["unique_identifier_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);
            }
            else
                throw new Exception("The following Unique Identifier needs to be inserted into the database, before it can be updated.\n" + uniqueIdentifier.ToString());
        }

        public UniqueIdentifier Write(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapUniqueIdentifier(uniqueId, personId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder("");
            if (row["person_id"] != DBNull.Value)
            {
                sqlValues.Append("@person_id, ");
                parameters.Add(new SqlParameter("@person_id", row["person_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["unique_identifier_value"] != DBNull.Value)
            {
                sqlValues.Append("@unique_identifier_value, ");
                parameters.Add(new SqlParameter("@unique_identifier_value", row["unique_identifier_value"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_identifier_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_identifier_id, ");
                parameters.Add(new SqlParameter("@enum_identifier_id", row["enum_identifier_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql = "INSERT INTO unique_identifier " +
                "(person_id, unique_identifier_value, enum_identifier_id, " +
                "created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["unique_identifier_id"] = connection.InsertNewRecord(sql, parameters);

            UniqueIdentifier result = daoObjectMapper.MapUniqueIdentifier(row);

            return result;
        }
    }
}
