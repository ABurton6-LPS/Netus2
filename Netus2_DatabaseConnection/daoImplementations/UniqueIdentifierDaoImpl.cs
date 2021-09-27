using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
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

        public void Delete(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapUniqueIdentifier(uniqueId, personId);

            StringBuilder sql = new StringBuilder("DELETE FROM unique_identifier WHERE 1=1 ");
            sql.Append("AND unique_identifier_id " + (row["unique_identifier_id"] != DBNull.Value ? "= " + row["unique_identifier_id"] + " " : "IS NULL "));
            sql.Append("AND person_id " + (row["person_id"] != DBNull.Value ? "= " + row["person_id"] + " " : "IS NULL "));
            sql.Append("AND unique_identifier " + (row["unique_identifier"] != DBNull.Value ? "= '" + row["unique_identifier"] + "' " : "IS NULL "));
            sql.Append("AND enum_identifier_id " + (row["enum_identifier_id"] != DBNull.Value ? "= " + row["enum_identifier_id"] + " " : "IS NULL "));
            sql.Append("AND is_active_id " + (row["is_active_id"] != DBNull.Value ? "= " + row["is_active_id"] + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        public List<UniqueIdentifier> Read(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            if (uniqueId == null)
                sql.Append("SELECT * FROM unique_identifier WHERE person_id = " + personId);
            else
            {
                DataRow row = daoObjectMapper.MapUniqueIdentifier(uniqueId, personId);

                sql.Append("SELECT * FROM unique_identifier WHERE 1=1 ");
                if (row["unique_identifier_id"] != DBNull.Value)
                    sql.Append("AND unique_identifier_id = " + row["unique_identifier_id"] + " ");
                else
                {
                    if (row["person_id"] != DBNull.Value)
                        sql.Append("AND person_id = " + row["person_id"] + " ");
                    if (row["unique_identifier"] != DBNull.Value)
                        sql.Append("AND unique_identifier = '" + row["unique_identifier"] + "' ");
                    if (row["enum_identifier_id"] != DBNull.Value)
                        sql.Append("AND enum_identifier_id = " + row["enum_identifier_id"] + " ");
                    if (row["is_active_id"] != DBNull.Value)
                        sql.Append("AND is_active_id = " + row["is_active_id"] + " ");
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<UniqueIdentifier> Read(string sql, IConnectable connection)
        {
            DataTable dtUniqueIdentifier = DataTableFactory.Dt_Netus2_UniqueIdentifier;
            dtUniqueIdentifier = connection.ReadIntoDataTable(sql, dtUniqueIdentifier);

            List<UniqueIdentifier> results = new List<UniqueIdentifier>();
            foreach (DataRow row in dtUniqueIdentifier.Rows)
            {
                results.Add(daoObjectMapper.MapUniqueIdentifier(row));
            }

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
                StringBuilder sql = new StringBuilder("UPDATE unique_identifier SET ");
                sql.Append("person_id = " + (row["person_id"] != DBNull.Value ? row["person_id"] + ", " : "NULL, "));
                sql.Append("unique_identifier = " + (row["unique_identifier"] != DBNull.Value ? "'" + row["unique_identifier"] + "', " : "NULL, "));
                sql.Append("enum_identifier_id = " + (row["enum_identifier_id"] != DBNull.Value ? row["enum_identifier_id"] + ", " : "NULL, "));
                sql.Append("is_active_id = " + (row["is_active_id"] != DBNull.Value ? row["is_active_id"] + ", " : "NULL, "));
                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE unique_identifier_id = " + row["unique_identifier_id"]);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
            {
                throw new Exception("The following Unique Identifier needs to be inserted into the database, before it can be updated.\n" + uniqueIdentifier.ToString());
            }
        }

        public UniqueIdentifier Write(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapUniqueIdentifier(uniqueId, personId);

            StringBuilder sql = new StringBuilder("INSERT INTO unique_identifier (");
            sql.Append("person_id, unique_identifier, enum_identifier_id, is_active_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(row["person_id"] != DBNull.Value ? row["person_id"] + ", " : "NULL, ");
            sql.Append(row["unique_identifier"] != DBNull.Value ? "'" + row["unique_identifier"] + "', " : "NULL, ");
            sql.Append(row["enum_identifier_id"] != DBNull.Value ? row["enum_identifier_id"] + ", " : "NULL, ");
            sql.Append(row["is_active_id"] != DBNull.Value ? row["is_active_id"] + ", " : "NULL, ");
            sql.Append("dbo.CURRENT_DATETIME(), ");
            sql.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");
            sql.Append(")");

            row["unique_identifier_id"] = connection.InsertNewRecord(sql.ToString());

            return daoObjectMapper.MapUniqueIdentifier(row);
        }
    }
}
