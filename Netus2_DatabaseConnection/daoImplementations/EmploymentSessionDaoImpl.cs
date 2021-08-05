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
    public class EmploymentSessionDaoImpl : IEmploymentSessionDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete_WithPersonId(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            EmploymentSessionDao esDao = daoObjectMapper.MapEmploymentSession_WithPersonId(employmentSession, personId);

            StringBuilder sql = new StringBuilder("DELETE FROM employment_session WHERE 1=1 ");
            sql.Append("AND employment_session_id " + (esDao.employment_session_id != null ? "= " + esDao.employment_session_id + " " : "IS NULL "));
            sql.Append("AND name " + (esDao.name != null ? "= '" + esDao.name + "' " : "IS NULL "));
            sql.Append("AND person_id " + (esDao.person_id != null ? "= " + esDao.person_id + " " : "IS NULL "));
            sql.Append("AND start_date " + (esDao.start_date != null ? "= '" + esDao.start_date + "' " : "IS NULL "));
            sql.Append("AND end_date " + (esDao.end_date != null ? "= '" + esDao.end_date + "' " : "IS NULL "));
            sql.Append("AND is_primary_id " + (esDao.is_primary_id != null ? "= " + esDao.is_primary_id + " " : "IS NULL "));
            sql.Append("AND enum_session_id " + (esDao.enum_session_id != null ? "= " + esDao.enum_session_id + " " : "IS NULL "));
            sql.Append("AND organization_id " + (esDao.organization_id != null ? "= " + esDao.organization_id + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        public void Delete_WithOrganizationId(EmploymentSession employmentSession, int organizationId, IConnectable connection)
        {
            EmploymentSessionDao esDao = daoObjectMapper.MapEmploymentSession_WithOrganizationId(employmentSession, organizationId);

            StringBuilder sql = new StringBuilder("DELETE FROM employment_session WHERE 1=1 ");
            sql.Append("AND employment_session_id " + (esDao.employment_session_id != null ? "= " + esDao.employment_session_id + " " : "IS NULL "));
            sql.Append("AND name " + (esDao.name != null ? "= '" + esDao.name + "' " : "IS NULL "));
            sql.Append("AND start_date " + (esDao.start_date != null ? "= '" + esDao.start_date + "' " : "IS NULL "));
            sql.Append("AND end_date " + (esDao.end_date != null ? "= '" + esDao.end_date + "' " : "IS NULL "));
            sql.Append("AND is_primary_id " + (esDao.is_primary_id != null ? "= " + esDao.is_primary_id + " " : "IS NULL "));
            sql.Append("AND enum_session_id " + (esDao.enum_session_id != null ? "= " + esDao.enum_session_id + " " : "IS NULL "));
            sql.Append("AND organization_id " + (esDao.organization_id != null ? "= " + esDao.organization_id + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        public List<EmploymentSession> Read_WithPersonId(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            if (employmentSession == null)
                sql.Append("SELECT * FROM employment_session WHERE person_id = " + personId);
            else
            {
                EmploymentSessionDao esDao = daoObjectMapper.MapEmploymentSession_WithPersonId(employmentSession, personId);

                sql.Append("SELECT * FROM employment_session WHERE 1=1 ");
                if (esDao.employment_session_id != null)
                    sql.Append("AND employment_session_id = " + esDao.employment_session_id + " ");
                else
                {
                    if (esDao.name != null)
                        sql.Append("AND name = '" + esDao.name + "' ");
                    if (esDao.person_id != null)
                        sql.Append("AND person_id = " + esDao.person_id + " ");
                    if (esDao.start_date != null)
                        sql.Append("AND datediff(day, start_date, '" + esDao.start_date.ToString() + "') = 0 ");
                    if (esDao.end_date != null)
                        sql.Append("AND datediff(day, end_date, '" + esDao.end_date.ToString() + "') = 0 ");
                    if (esDao.is_primary_id != null)
                        sql.Append("AND is_primary_id = " + esDao.is_primary_id + " ");
                    if (esDao.enum_session_id != null)
                        sql.Append("AND enum_session_id = " + esDao.enum_session_id + " ");
                    if (esDao.organization_id != null)
                        sql.Append("AND organization_id = " + esDao.organization_id + " ");
                }
            }

            return Read(sql.ToString(), connection);
        }

        public List<EmploymentSession> Read_WithOrganizationId(EmploymentSession employmentSession, int organizationId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            if (employmentSession == null)
                sql.Append("SELECT * FROM employment_session WHERE organization_id = " + organizationId);
            else
            {
                EmploymentSessionDao esDao = daoObjectMapper.MapEmploymentSession_WithOrganizationId(employmentSession, organizationId);

                sql.Append("SELECT * FROM employment_session WHERE 1=1 ");
                if (esDao.employment_session_id != null)
                    sql.Append("AND employment_session_id = " + esDao.employment_session_id + " ");
                else
                {
                    if (esDao.name != null)
                        sql.Append("AND name = '" + esDao.name + "' ");
                    if (esDao.person_id != null)
                        sql.Append("AND person_id = " + esDao.person_id + " ");
                    if (esDao.start_date != null)
                        sql.Append("AND datediff(day, start_date, '" + esDao.start_date.ToString() + "') = 0 ");
                    if (esDao.end_date != null)
                        sql.Append("AND datediff(day, end_date, '" + esDao.end_date.ToString() + "') = 0 ");
                    if (esDao.is_primary_id != null)
                        sql.Append("AND is_primary_id = " + esDao.is_primary_id + " ");
                    if (esDao.enum_session_id != null)
                        sql.Append("AND enum_session_id = " + esDao.enum_session_id + " ");
                    if (esDao.organization_id != null)
                        sql.Append("AND organization_id = " + esDao.organization_id + " ");
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<EmploymentSession> Read(string sql, IConnectable connection)
        {
            List<EmploymentSessionDao> foundEsDaos = new List<EmploymentSessionDao>();
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql.ToString());
                while (reader.Read())
                {
                    EmploymentSessionDao foundEsDao = new EmploymentSessionDao();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "employment_session_id":
                                if (value != DBNull.Value)
                                    foundEsDao.employment_session_id = (int)value;
                                else
                                    foundEsDao.employment_session_id = null;
                                break;
                            case "name":
                                foundEsDao.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case "person_id":
                                if (value != DBNull.Value)
                                    foundEsDao.person_id = (int)value;
                                else
                                    foundEsDao.person_id = null;
                                break;
                            case "start_date":
                                if (value != DBNull.Value)
                                    foundEsDao.start_date = (DateTime)value;
                                else
                                    foundEsDao.start_date = null;
                                break;
                            case "end_date":
                                if (value != DBNull.Value)
                                    foundEsDao.end_date = (DateTime)value;
                                else
                                    foundEsDao.end_date = null;
                                break;
                            case "is_primary_id":
                                if (value != DBNull.Value)
                                    foundEsDao.is_primary_id = (int)value;
                                else
                                    foundEsDao.is_primary_id = null;
                                break;
                            case "enum_session_id":
                                if (value != DBNull.Value)
                                    foundEsDao.enum_session_id = (int)value;
                                else
                                    foundEsDao.enum_session_id = null;
                                break;
                            case "organization_id":
                                if (value != DBNull.Value)
                                    foundEsDao.organization_id = (int)value;
                                else
                                    foundEsDao.organization_id = null;
                                break;
                            case "created":
                                if (value != DBNull.Value)
                                    foundEsDao.created = (DateTime)value;
                                else
                                    foundEsDao.created = null;
                                break;
                            case "created_by":
                                foundEsDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case "changed":
                                if (value != DBNull.Value)
                                    foundEsDao.changed = (DateTime)value;
                                else
                                    foundEsDao.changed = null;
                                    break;
                            case "changed_by":
                                foundEsDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in employment_session table: " + columnName);
                        }
                    }
                    foundEsDaos.Add(foundEsDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<EmploymentSession> results = new List<EmploymentSession>();
            foreach (EmploymentSessionDao foundEsDao in foundEsDaos)
            {
                Organization foundOrg = Read_Organization((int)foundEsDao.organization_id, connection);
                results.Add(daoObjectMapper.MapEmploymentSession(foundEsDao, foundOrg));
            }

            return results;
        }

        private Organization Read_Organization(int orgId, IConnectable connection)
        {
            IOrganizationDao organizationDaoImpl = new OrganizationDaoImpl();
            return organizationDaoImpl.Read_WithOrganizationId(orgId, connection);
        }

        public void Update(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            List<EmploymentSession> foundEmploymentSessions = Read_WithPersonId(employmentSession, personId, connection);
            if (foundEmploymentSessions.Count == 0)
                Write(employmentSession, personId, connection);
            else if (foundEmploymentSessions.Count == 1)
            {
                employmentSession.Id = foundEmploymentSessions[0].Id;
                UpdateInternals(employmentSession, personId, connection);
            }
            else
                throw new Exception(foundEmploymentSessions.Count + " Employment Sessions found matching the description of:\n" +
                    employmentSession.ToString());
        }

        private void UpdateInternals(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            EmploymentSessionDao esDao = daoObjectMapper.MapEmploymentSession_WithPersonId(employmentSession, personId);

            if (esDao.employment_session_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE employment_session SET ");
                sql.Append("name = " + (esDao.name != null ? "'" + esDao.name + "', " : "NULL, "));
                sql.Append("person_id = " + (esDao.person_id != null ? esDao.person_id + ", " : "NULL, "));
                sql.Append("start_date = " + (esDao.start_date != null ? "'" + esDao.start_date + "', " : "NULL, "));
                sql.Append("end_date = " + (esDao.end_date != null ? "'" + esDao.end_date + "', " : "NULL, "));
                sql.Append("is_primary_id = " + (esDao.is_primary_id != null ? esDao.is_primary_id + ", " : "NULL, "));
                sql.Append("enum_session_id = " + (esDao.enum_session_id != null ? esDao.enum_session_id + ", " : "NULL, "));
                sql.Append("organization_id = " + (esDao.organization_id != null ? esDao.organization_id + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE employment_session_id = " + esDao.employment_session_id);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
            {
                throw new Exception("The following Employment Session needs to be inserted into the database, before it can be updated.\n" + employmentSession.ToString());
            }
        }

        public EmploymentSession Write(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            EmploymentSessionDao esDao = daoObjectMapper.MapEmploymentSession_WithPersonId(employmentSession, personId);

            StringBuilder sql = new StringBuilder("INSERT INTO employment_session (");
            sql.Append("name, person_id, start_date, end_date, is_primary_id, enum_session_id, organization_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(esDao.name != null ? "'" + esDao.name + "', " : "NULL, ");
            sql.Append(esDao.person_id != null ? esDao.person_id + ", " : "NULL, ");
            sql.Append(esDao.start_date != null ? "'" + esDao.start_date + "', " : "NULL, ");
            sql.Append(esDao.end_date != null ? "'" + esDao.end_date + "', " : "NULL, ");
            sql.Append(esDao.is_primary_id != null ? esDao.is_primary_id + ", " : "NULL, ");
            sql.Append(esDao.enum_session_id != null ? esDao.enum_session_id + ", " : "NULL, ");
            sql.Append(esDao.organization_id != null ? esDao.organization_id + ", " : "NULL, ");
            sql.Append("GETDATE(), ");
            sql.Append("'Netus2')");

            esDao.employment_session_id = connection.InsertNewRecord(sql.ToString());

            Organization foundOrg = Read_Organization((int)esDao.organization_id, connection);
            return daoObjectMapper.MapEmploymentSession(esDao, foundOrg);
        }
    }
}
