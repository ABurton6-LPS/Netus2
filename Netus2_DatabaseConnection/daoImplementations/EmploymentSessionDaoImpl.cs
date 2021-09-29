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
    public class EmploymentSessionDaoImpl : IEmploymentSessionDao
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

        public void Delete_WithPersonId(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEmploymentSession_WithPersonId(employmentSession, personId);

            StringBuilder sql = new StringBuilder("DELETE FROM employment_session WHERE 1=1 ");
            sql.Append("AND employment_session_id " + (row["employment_session_id"] != DBNull.Value ? "= " + row["employment_session_id"] + " " : "IS NULL "));
            sql.Append("AND name " + (row["name"] != DBNull.Value ? "= '" + row["name"] + "' " : "IS NULL "));
            sql.Append("AND person_id " + (row["person_id"] != DBNull.Value ? "= " + row["person_id"] + " " : "IS NULL "));
            sql.Append("AND start_date " + (row["start_date"] != DBNull.Value ? "= '" + row["start_date"] + "' " : "IS NULL "));
            sql.Append("AND end_date " + (row["end_date"] != DBNull.Value ? "= '" + row["end_date"] + "' " : "IS NULL "));
            sql.Append("AND is_primary_id " + (row["is_primary_id"] != DBNull.Value ? "= " + row["is_primary_id"] + " " : "IS NULL "));
            sql.Append("AND enum_session_id " + (row["enum_session_id"] != DBNull.Value ? "= " + row["enum_session_id"] + " " : "IS NULL "));
            sql.Append("AND organization_id " + (row["organization_id"] != DBNull.Value ? "= " + row["organization_id"] + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        public void Delete_WithOrganizationId(EmploymentSession employmentSession, int organizationId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEmploymentSession_WithOrganizationId(employmentSession, organizationId);

            StringBuilder sql = new StringBuilder("DELETE FROM employment_session WHERE 1=1 ");
            sql.Append("AND employment_session_id " + (row["employment_session_id"] != DBNull.Value ? "= " + row["employment_session_id"] + " " : "IS NULL "));
            sql.Append("AND name " + (row["name"] != DBNull.Value ? "= '" + row["name"] + "' " : "IS NULL "));
            sql.Append("AND start_date " + (row["start_date"] != DBNull.Value ? "= '" + row["start_date"] + "' " : "IS NULL "));
            sql.Append("AND end_date " + (row["end_date"] != DBNull.Value ? "= '" + row["end_date"] + "' " : "IS NULL "));
            sql.Append("AND is_primary_id " + (row["is_primary_id"] != DBNull.Value ? "= " + row["is_primary_id"] + " " : "IS NULL "));
            sql.Append("AND enum_session_id " + (row["enum_session_id"] != DBNull.Value ? "= " + row["enum_session_id"] + " " : "IS NULL "));
            sql.Append("AND organization_id " + (row["organization_id"] != DBNull.Value ? "= " + row["organization_id"] + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        public List<EmploymentSession> Read_WithPersonId(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            if (employmentSession == null)
                sql.Append("SELECT * FROM employment_session WHERE person_id = " + personId);
            else
            {
                DataRow row = daoObjectMapper.MapEmploymentSession_WithPersonId(employmentSession, personId);

                sql.Append("SELECT * FROM employment_session WHERE 1=1 ");
                if (row["employment_session_id"] != DBNull.Value)
                    sql.Append("AND employment_session_id = " + row["employment_session_id"] + " ");
                else
                {
                    if (row["name"] != DBNull.Value)
                        sql.Append("AND name = '" + row["name"] + "' ");
                    if (row["person_id"] != DBNull.Value)
                        sql.Append("AND person_id = " + row["person_id"] + " ");
                    if (row["start_date"] != DBNull.Value)
                        sql.Append("AND datediff(day, start_date, '" + row["start_date"].ToString() + "') = 0 ");
                    if (row["end_date"] != DBNull.Value)
                        sql.Append("AND datediff(day, end_date, '" + row["end_date"].ToString() + "') = 0 ");
                    if (row["is_primary_id"] != DBNull.Value)
                        sql.Append("AND is_primary_id = " + row["is_primary_id"] + " ");
                    if (row["enum_session_id"] != DBNull.Value)
                        sql.Append("AND enum_session_id = " + row["enum_session_id"] + " ");
                    if (row["organization_id"] != DBNull.Value)
                        sql.Append("AND organization_id = " + row["organization_id"] + " ");
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
                DataRow row = daoObjectMapper.MapEmploymentSession_WithOrganizationId(employmentSession, organizationId);

                sql.Append("SELECT * FROM employment_session WHERE 1=1 ");
                if (row["employment_session_id"] != DBNull.Value)
                    sql.Append("AND employment_session_id = " + row["employment_session_id"] + " ");
                else
                {
                    if (row["name"] != DBNull.Value)
                        sql.Append("AND name = '" + row["name"] + "' ");
                    if (row["person_id"] != DBNull.Value)
                        sql.Append("AND person_id = " + row["person_id"] + " ");
                    if (row["start_date"] != DBNull.Value)
                        sql.Append("AND datediff(day, start_date, '" + row["start_date"].ToString() + "') = 0 ");
                    if (row["end_date"] != DBNull.Value)
                        sql.Append("AND datediff(day, end_date, '" + row["end_date"].ToString() + "') = 0 ");
                    if (row["is_primary_id"] != DBNull.Value)
                        sql.Append("AND is_primary_id = " + row["is_primary_id"] + " ");
                    if (row["enum_session_id"] != DBNull.Value)
                        sql.Append("AND enum_session_id = " + row["enum_session_id"] + " ");
                    if (row["organization_id"] != DBNull.Value)
                        sql.Append("AND organization_id = " + row["organization_id"] + " ");
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<EmploymentSession> Read(string sql, IConnectable connection)
        {
            DataTable dtEmploymentSession = DataTableFactory.CreateDataTable_Netus2_EmploymentSession();
            dtEmploymentSession = connection.ReadIntoDataTable(sql, dtEmploymentSession);

            List<EmploymentSession> results = new List<EmploymentSession>();
            foreach (DataRow foundEsDao in dtEmploymentSession.Rows)
            {
                Organization foundOrg = Read_Organization((int)foundEsDao["organization_id"], connection);
                results.Add(daoObjectMapper.MapEmploymentSession(foundEsDao, foundOrg));
            }

            return results;
        }

        private Organization Read_Organization(int orgId, IConnectable connection)
        {
            IOrganizationDao organizationDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
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
            DataRow row = daoObjectMapper.MapEmploymentSession_WithPersonId(employmentSession, personId);

            if (row["employment_session_id"] != DBNull.Value)
            {
                StringBuilder sql = new StringBuilder("UPDATE employment_session SET ");
                sql.Append("name = " + (row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, "));
                sql.Append("person_id = " + (row["person_id"] != DBNull.Value ? row["person_id"] + ", " : "NULL, "));
                sql.Append("start_date = " + (row["start_date"] != DBNull.Value ? "'" + row["start_date"] + "', " : "NULL, "));
                sql.Append("end_date = " + (row["end_date"] != DBNull.Value ? "'" + row["end_date"] + "', " : "NULL, "));
                sql.Append("is_primary_id = " + (row["is_primary_id"] != DBNull.Value ? row["is_primary_id"] + ", " : "NULL, "));
                sql.Append("enum_session_id = " + (row["enum_session_id"] != DBNull.Value ? row["enum_session_id"] + ", " : "NULL, "));
                sql.Append("organization_id = " + (row["organization_id"] != DBNull.Value ? row["organization_id"] + ", " : "NULL, "));
                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE employment_session_id = " + row["employment_session_id"]);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
            {
                throw new Exception("The following Employment Session needs to be inserted into the database, before it can be updated.\n" + employmentSession.ToString());
            }
        }

        public EmploymentSession Write(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEmploymentSession_WithPersonId(employmentSession, personId);

            StringBuilder sql = new StringBuilder("INSERT INTO employment_session (");
            sql.Append("name, person_id, start_date, end_date, is_primary_id, enum_session_id, organization_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, ");
            sql.Append(row["person_id"] != DBNull.Value ? row["person_id"] + ", " : "NULL, ");
            sql.Append(row["start_date"] != DBNull.Value ? "'" + row["start_date"] + "', " : "NULL, ");
            sql.Append(row["end_date"] != DBNull.Value ? "'" + row["end_date"] + "', " : "NULL, ");
            sql.Append(row["is_primary_id"] != DBNull.Value ? row["is_primary_id"] + ", " : "NULL, ");
            sql.Append(row["enum_session_id"] != DBNull.Value ? row["enum_session_id"] + ", " : "NULL, ");
            sql.Append(row["organization_id"] != DBNull.Value ? row["organization_id"] + ", " : "NULL, ");
            sql.Append("dbo.CURRENT_DATETIME(), ");
            sql.Append((_taskId != null ? _taskId.ToString() : "'Netus2'") + ")");

            row["employment_session_id"] = connection.InsertNewRecord(sql.ToString());

            Organization foundOrg = Read_Organization((int)row["organization_id"], connection);
            return daoObjectMapper.MapEmploymentSession(row, foundOrg);
        }
    }
}
