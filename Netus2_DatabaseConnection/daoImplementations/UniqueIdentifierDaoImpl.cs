using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class UniqueIdentifierDaoImpl : IUniqueIdentifierDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            UniqueIdentifierDao uniqueIdDao = daoObjectMapper.MapUniqueIdentifier(uniqueId, personId);

            StringBuilder sql = new StringBuilder("DELETE FROM unique_identifier WHERE 1=1 ");
            sql.Append("AND unique_identifier_id " + (uniqueIdDao.unique_identifier_id != null ? "= " + uniqueIdDao.unique_identifier_id + " " : "IS NULL "));
            sql.Append("AND person_id " + (uniqueIdDao.person_id != null ? "= " + uniqueIdDao.person_id + " " : "IS NULL "));
            sql.Append("AND unique_identifier " + (uniqueIdDao.unique_identifier != null ? "= '" + uniqueIdDao.unique_identifier + "' " : "IS NULL "));
            sql.Append("AND enum_identifier_id " + (uniqueIdDao.enum_identifier_id != null ? "= " + uniqueIdDao.enum_identifier_id + " " : "IS NULL "));
            sql.Append("AND is_active_id " + (uniqueIdDao.is_active_id != null ? "= " + uniqueIdDao.is_active_id + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        public List<UniqueIdentifier> Read(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            if (uniqueId == null)
                sql.Append("SELECT * FROM unique_identifier WHERE person_id = " + personId);
            else
            {
                UniqueIdentifierDao uniqueIdDao = daoObjectMapper.MapUniqueIdentifier(uniqueId, personId);

                sql.Append("SELECT * FROM unique_identifier WHERE 1=1");
                if (uniqueIdDao.unique_identifier_id != null)
                    sql.Append("AND unique_identifier_id = " + uniqueIdDao.unique_identifier_id + " ");
                else
                {
                    if (uniqueIdDao.person_id != null)
                        sql.Append("AND person_id = " + uniqueIdDao.person_id + " ");
                    if (uniqueIdDao.unique_identifier != null)
                        sql.Append("AND unique_identifier = '" + uniqueIdDao.unique_identifier + "' ");
                    if (uniqueIdDao.enum_identifier_id != null)
                        sql.Append("AND enum_identifier_id = " + uniqueIdDao.enum_identifier_id + " ");
                    if (uniqueIdDao.is_active_id != null)
                        sql.Append("AND is_active_id = " + uniqueIdDao.is_active_id + " ");
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<UniqueIdentifier> Read(string sql, IConnectable connection)
        {
            List<UniqueIdentifierDao> foundUniqueIdDaos = new List<UniqueIdentifierDao>();

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    UniqueIdentifierDao foundUniqueIdDao = new UniqueIdentifierDao();

                    List<string> columnNames = new List<String>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "unique_identifier_id":
                                if (value != DBNull.Value)
                                    foundUniqueIdDao.unique_identifier_id = (int)value;
                                else
                                    foundUniqueIdDao.unique_identifier_id = null;
                                break;
                            case "person_id":
                                if (value != DBNull.Value)
                                    foundUniqueIdDao.person_id = (int)value;
                                else
                                    foundUniqueIdDao.person_id = null;
                                break;
                            case "unique_identifier":
                                foundUniqueIdDao.unique_identifier = value != DBNull.Value ? (string)value : null;
                                break;
                            case "enum_identifier_id":
                                if (value != DBNull.Value)
                                    foundUniqueIdDao.enum_identifier_id = (int)value;
                                else
                                    foundUniqueIdDao.enum_identifier_id = null;
                                break;
                            case "is_active_id":
                                if (value != DBNull.Value)
                                    foundUniqueIdDao.is_active_id = (int)value;
                                else
                                    foundUniqueIdDao.is_active_id = null;
                                break;
                            case "created":
                                if (value != DBNull.Value)
                                    foundUniqueIdDao.created = (DateTime)value;
                                else
                                    foundUniqueIdDao.created = null;
                                break;
                            case "created_by":
                                foundUniqueIdDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case "changed":
                                if (value != DBNull.Value)
                                    foundUniqueIdDao.changed = (DateTime)value;
                                else
                                    foundUniqueIdDao.changed = null;
                                break;
                            case "changed_by":
                                foundUniqueIdDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in unique_identifier table: " + columnName);
                        }
                    }
                    foundUniqueIdDaos.Add(foundUniqueIdDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<UniqueIdentifier> results = new List<UniqueIdentifier>();
            foreach (UniqueIdentifierDao foundUniqueIdentifierDao in foundUniqueIdDaos)
            {
                results.Add(daoObjectMapper.MapUniqueIdentifier(foundUniqueIdentifierDao));
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
            UniqueIdentifierDao uniqueIdDao = daoObjectMapper.MapUniqueIdentifier(uniqueIdentifier, personId);

            if (uniqueIdDao.unique_identifier_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE unique_identifier SET ");
                sql.Append("person_id = " + (uniqueIdDao.person_id != null ? uniqueIdDao.person_id + ", " : "NULL, "));
                sql.Append("unique_identifier = " + (uniqueIdDao.unique_identifier != null ? "'" + uniqueIdDao.unique_identifier + "', " : "NULL, "));
                sql.Append("enum_identifier_id = " + (uniqueIdDao.enum_identifier_id != null ? uniqueIdDao.enum_identifier_id + ", " : "NULL, "));
                sql.Append("is_active_id = " + (uniqueIdDao.is_active_id != null ? uniqueIdDao.is_active_id + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE unique_identifier_id = " + uniqueIdDao.unique_identifier_id);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
            {
                throw new Exception("The following Unique Identifier needs to be inserted into the database, before it can be updated.\n" + uniqueIdentifier.ToString());
            }
        }

        public UniqueIdentifier Write(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            UniqueIdentifierDao uniqueIdDao = daoObjectMapper.MapUniqueIdentifier(uniqueId, personId);

            StringBuilder sql = new StringBuilder("INSERT INTO unique_identifier (");
            sql.Append("person_id, unique_identifier, enum_identifier_id, is_active_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(uniqueIdDao.person_id != null ? uniqueIdDao.person_id + ", " : "NULL, ");
            sql.Append(uniqueIdDao.unique_identifier != null ? "'" + uniqueIdDao.unique_identifier + "', " : "NULL, ");
            sql.Append(uniqueIdDao.enum_identifier_id != null ? uniqueIdDao.enum_identifier_id + ", " : "NULL, ");
            sql.Append(uniqueIdDao.is_active_id != null ? uniqueIdDao.is_active_id + ", " : "NULL, ");
            sql.Append("GETDATE(), ");
            sql.Append("'Netus2'");
            sql.Append(")");

            uniqueIdDao.unique_identifier_id = connection.InsertNewRecord(sql.ToString());

            return daoObjectMapper.MapUniqueIdentifier(uniqueIdDao);
        }
    }
}
