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

        public void Delete(EmploymentSession employmentSession, IConnectable connection)
        {
            if(employmentSession.Id <= 0)
                throw new Exception("Cannot delete an employment session which doesn't have a database-assigned ID.\n" + employmentSession.ToString());

            string sql = "DELETE FROM employment_session WHERE " +
                "employment_session_id = @employment_session_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@employment_session_id", employmentSession.Id));
            
            connection.ExecuteNonQuery(sql, parameters);
        }

        public List<EmploymentSession> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM employment_session WHERE person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            return Read(sql, connection, parameters);
        }

        public List<EmploymentSession> Read(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEmploymentSession_WithPersonId(employmentSession, personId);
            return Read(row, connection);
        }

        public List<EmploymentSession> Read_AllWithOrganizationId(int organizationId, IConnectable connection)
        {
            string sql = "SELECT * FROM employment_session WHERE organization_id = @organization_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@organization_id", organizationId));

            return Read(sql, connection, parameters);
        }

        public List<EmploymentSession> Read_UsingOrganizationId(EmploymentSession employmentSession, int organizationId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEmploymentSession_WithOrganizationId(employmentSession, organizationId);
            return Read(row, connection);
        }

        private List<EmploymentSession> Read(DataRow row, IConnectable connection)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("");

            sql.Append("SELECT * FROM employment_session WHERE 1=1 ");
            if (row["employment_session_id"] != DBNull.Value)
            {
                sql.Append("AND employment_session_id = @employment_session_id");
                parameters.Add(new SqlParameter("@employment_session_id", row["employment_session_id"]));
            }
            else
            {
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("AND name = @name ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }

                if (row["person_id"] != DBNull.Value)
                {
                    sql.Append("AND person_id = @person_id ");
                    parameters.Add(new SqlParameter("@person_id", row["person_id"]));
                }

                if (row["person_id"] != DBNull.Value)
                {
                    sql.Append("AND person_id = @person_id ");
                    parameters.Add(new SqlParameter("@person_id", row["person_id"]));
                }

                if (row["start_date"] != DBNull.Value)
                {
                    sql.Append("AND datediff(day, start_date, @start_date) = 0 ");
                    parameters.Add(new SqlParameter("@start_date", row["start_date"]));
                }

                if (row["end_date"] != DBNull.Value)
                {
                    sql.Append("AND datediff(day, end_date, @end_date) = 0 ");
                    parameters.Add(new SqlParameter("@end_date", row["end_date"]));
                }

                if (row["is_primary"] != DBNull.Value)
                {
                    sql.Append("AND is_primary = @is_primary ");
                    parameters.Add(new SqlParameter("@is_primary", row["is_primary"]));
                }

                if (row["is_primary"] != DBNull.Value)
                {
                    sql.Append("AND is_primary = @is_primary ");
                    parameters.Add(new SqlParameter("@is_primary", row["is_primary"]));
                }

                if (row["enum_session_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_session_id = @enum_session_id ");
                    parameters.Add(new SqlParameter("@enum_session_id", row["enum_session_id"]));
                }

                if (row["organization_id"] != DBNull.Value)
                {
                    sql.Append("AND organization_id = @organization_id");
                    parameters.Add(new SqlParameter("@organization_id", row["organization_id"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<EmploymentSession> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtEmploymentSession = DataTableFactory.CreateDataTable_Netus2_EmploymentSession();
            dtEmploymentSession = connection.ReadIntoDataTable(sql, dtEmploymentSession, parameters);

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
            return organizationDaoImpl.Read_UsingOrganizationId(orgId, connection);
        }

        public void Update(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            List<EmploymentSession> foundEmploymentSessions = Read(employmentSession, personId, connection);
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
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE employment_session SET ");
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("name = @name, ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }
                else
                    sql.Append("name = NULL, ");

                if (row["person_id"] != DBNull.Value)
                {
                    sql.Append("person_id = @person_id, ");
                    parameters.Add(new SqlParameter("@person_id", row["person_id"]));
                }
                else
                    sql.Append("person_id = NULL, ");

                if (row["start_date"] != DBNull.Value)
                {
                    sql.Append("start_date = @start_date, ");
                    parameters.Add(new SqlParameter("@start_date", row["start_date"]));
                }
                else
                    sql.Append("start_date = NULL, ");

                if (row["end_date"] != DBNull.Value)
                {
                    sql.Append("end_date = @end_date, ");
                    parameters.Add(new SqlParameter("@end_date", row["end_date"]));
                }
                else
                    sql.Append("end_date = NULL, ");

                if (row["is_primary"] != DBNull.Value)
                {
                    sql.Append("is_primary = @is_primary, ");
                    parameters.Add(new SqlParameter("@is_primary", row["is_primary"]));
                }
                else
                    sql.Append("is_primary = NULL, ");

                if (row["enum_session_id"] != DBNull.Value)
                {
                    sql.Append("enum_session_id = @enum_session_id, ");
                    parameters.Add(new SqlParameter("@enum_session_id", row["enum_session_id"]));
                }
                else
                    sql.Append("enum_session_id = NULL, ");

                if (row["organization_id"] != DBNull.Value)
                {
                    sql.Append("organization_id = @organization_id, ");
                    parameters.Add(new SqlParameter("@organization_id", row["organization_id"]));
                }
                else
                    sql.Append("organization_id = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE employment_session_id = @employment_session_id");
                parameters.Add(new SqlParameter("@employment_session_id", row["employment_session_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);
            }
            else
                throw new Exception("The following Employment Session needs to be inserted into the database, before it can be updated.\n" + employmentSession.ToString());
        }

        public EmploymentSession Write(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEmploymentSession_WithPersonId(employmentSession, personId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["name"] != DBNull.Value)
            {
                sqlValues.Append("@name, ");
                parameters.Add(new SqlParameter("@name", row["name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["person_id"] != DBNull.Value)
            {
                sqlValues.Append("@person_id, ");
                parameters.Add(new SqlParameter("@person_id", row["person_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["start_date"] != DBNull.Value)
            {
                sqlValues.Append("@start_date, ");
                parameters.Add(new SqlParameter("@start_date", row["start_date"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["end_date"] != DBNull.Value)
            {
                sqlValues.Append("@end_date, ");
                parameters.Add(new SqlParameter("@end_date", row["end_date"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["is_primary"] != DBNull.Value)
            {
                sqlValues.Append("@is_primary, ");
                parameters.Add(new SqlParameter("@is_primary", row["is_primary"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_session_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_session_id, ");
                parameters.Add(new SqlParameter("@enum_session_id", row["enum_session_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["organization_id"] != DBNull.Value)
            {
                sqlValues.Append("@organization_id, ");
                parameters.Add(new SqlParameter("@organization_id", row["organization_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql = "INSERT INTO employment_session " +
                "(name, person_id, start_date, end_date, is_primary, enum_session_id, organization_id, " +
                "created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["employment_session_id"] = connection.InsertNewRecord(sql, parameters);

            Organization org = Read_Organization((int)row["organization_id"], connection);
            EmploymentSession result = daoObjectMapper.MapEmploymentSession(row, org);

            return result;
        }
    }
}
