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
    public class OrganizationDaoImpl : IOrganizationDao
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

        public void Delete(Organization organization, IConnectable connection)
        {
            if (organization.Id <= 0)
                throw new Exception("Cannot delete an organization which doesn't have a database-assigned ID.\n" + organization.ToString());

            UnlinkChildren(organization, connection);
            Delete_EmploymentSessions(organization, connection);
            Delete_AcademicSession(organization, connection);

            DataRow row = daoObjectMapper.MapOrganization(organization, -1);

            string sql = "DELETE FROM organization WHERE " +
            "organization_id = @organization_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@organization_id", organization.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void Delete_AcademicSession(Organization organization, IConnectable connection)
        {
            IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
            List<AcademicSession> foundAcademicSessions = academicSessionDaoImpl.Read_AllWithOrganizationId(organization.Id, connection);

            foreach (AcademicSession foundAcademicSession in foundAcademicSessions)
                academicSessionDaoImpl.Delete(foundAcademicSession, connection);
        }

        private void Delete_EmploymentSessions(Organization organization, IConnectable connection)
        {
            IEmploymentSessionDao employmentSessionsDaoImpl = DaoImplFactory.GetEmploymentSessionDaoImpl();
            List<EmploymentSession> foundEmploymentSessions = employmentSessionsDaoImpl.Read_AllWithOrganizationId(organization.Id, connection);

            foreach (EmploymentSession foundEmploymentSession in foundEmploymentSessions)
            {
                employmentSessionsDaoImpl.Delete(foundEmploymentSession, connection);
            }
        }

        private void UnlinkChildren(Organization organization, IConnectable connection)
        {
            List<Organization> childrenToRemove = new List<Organization>();
            foreach (Organization child in organization.Children)
            {
                Update(child, connection);
                childrenToRemove.Add(child);
            }
            foreach (Organization child in childrenToRemove)
            {
                organization.Children.Remove(child);
            }
        }

        public Organization Read_UsingSisBuildingCode(string sisBuildingCode, IConnectable connection)
        {
            string sql = "SELECT * FROM organization WHERE sis_building_code = @sis_building_code";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@sis_building_code", sisBuildingCode));

            List<Organization> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public Organization Read_UsingOrganizationId(int orgId, IConnectable connection)
        {
            string sql = "SELECT * FROM organization WHERE organization_id = @organization_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@organization_id", orgId));

            List<Organization> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public Organization Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            string sql = "SELECT * FROM organization WHERE organization_id IN (" +
                "SELECT organization_id FROM academic_session WHERE academic_session_id = @academic_session_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@academic_session_id", academicSessionId));

            List<Organization> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public Organization Read_Parent(Organization organization, IConnectable connection)
        {
            string sql = "SELECT * FROM organization WHERE organization_id in ( " +
                "SELECT organization_parent_id FROM organization WHERE organization_id = @organization_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@organization_id", organization.Id));

            List<Organization> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public List<Organization> Read_AllChildrenWithParentId(int parentId, IConnectable connection)
        {
            string sql = "SELECT * FROM organization WHERE organization_parent_id = @organization_parent_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@organization_parent_id", parentId));

            List<Organization> results = Read(sql, connection, parameters);

            return results;
        }

        public List<Organization> Read(Organization organization, IConnectable connection)
        {
            return Read(organization, -1, connection);
        }

        public List<Organization> Read(Organization organization, int parentId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapOrganization(organization, parentId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM organization WHERE 1=1 ");
            if (row["organization_id"] != DBNull.Value)
            {
                sql.Append("AND organization_id = @organization_id ");
                parameters.Add(new SqlParameter("@organization_id", row["organization_id"]));
            }                
            else
            {
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("AND name = @name ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }

                if (row["enum_organization_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_organization_id = @enum_organization_id ");
                    parameters.Add(new SqlParameter("@enum_organization_id", row["enum_organization_id"]));
                }

                if (row["identifier"] != DBNull.Value)
                {
                    sql.Append("AND identifier = @identifier ");
                    parameters.Add(new SqlParameter("@identifier", row["identifier"]));
                }

                if (row["sis_building_code"] != DBNull.Value)
                {
                    sql.Append("AND sis_building_code = @sis_building_code ");
                    parameters.Add(new SqlParameter("@sis_building_code", row["sis_building_code"]));
                }

                if (row["hr_building_code"] != DBNull.Value)
                {
                    sql.Append("AND hr_building_code = @hr_building_code ");
                    parameters.Add(new SqlParameter("@hr_building_code", row["hr_building_code"]));
                }

                if (row["organization_parent_id"] != DBNull.Value)
                {
                    sql.Append("AND organization_parent_id = @organization_parent_id ");
                    parameters.Add(new SqlParameter("@organization_parent_id", row["organization_parent_id"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<Organization> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtOrganization = DataTableFactory.CreateDataTable_Netus2_Organization();
            dtOrganization = connection.ReadIntoDataTable(sql, dtOrganization, parameters);

            List<Organization> results = new List<Organization>();
            foreach (DataRow row in dtOrganization.Rows)
                results.Add(daoObjectMapper.MapOrganization(row));

            foreach (Organization result in results)
                result.Children.AddRange(Read_AllChildrenWithParentId(result.Id, connection));

            return results;
        }

        public void Update(Organization organization, IConnectable connection)
        {
            Update(organization, -1, connection);
        }

        public void Update(Organization organization, int parentOrganizationId, IConnectable connection)
        {
            List<Organization> foundOrganizations = Read(organization, connection);
            if (foundOrganizations.Count == 0)
                Write(organization, parentOrganizationId, connection);
            else if (foundOrganizations.Count == 1)
            {
                organization.Id = foundOrganizations[0].Id;
                UpdateInternals(organization, parentOrganizationId, connection);
            }
            else
                throw new Exception(foundOrganizations.Count + " Organizations found matching the description of:\n" +
                    organization.ToString());
        }

        private void UpdateInternals(Organization organization, int parentOrganizationId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapOrganization(organization, parentOrganizationId);

            if (row["organization_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE organization SET ");
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("name = @name, ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }
                else
                    sql.Append("name = NULL, ");

                if (row["enum_organization_id"] != DBNull.Value)
                {
                    sql.Append("enum_organization_id = @enum_organization_id, ");
                    parameters.Add(new SqlParameter("@enum_organization_id", row["enum_organization_id"]));
                }
                else
                    sql.Append("enum_organization_id = NULL, ");

                if (row["identifier"] != DBNull.Value)
                {
                    sql.Append("identifier = @identifier, ");
                    parameters.Add(new SqlParameter("@identifier", row["identifier"]));
                }
                else
                    sql.Append("identifier = NULL, ");

                if (row["sis_building_code"] != DBNull.Value)
                {
                    sql.Append("sis_building_code = @sis_building_code, ");
                    parameters.Add(new SqlParameter("@sis_building_code", row["sis_building_code"]));
                }
                else
                    sql.Append("sis_building_code = NULL, ");

                if (row["hr_building_code"] != DBNull.Value)
                {
                    sql.Append("hr_building_code = @hr_building_code, ");
                    parameters.Add(new SqlParameter("@hr_building_code", row["hr_building_code"]));
                }
                else
                    sql.Append("hr_building_code = NULL, ");

                if (row["organization_parent_id"] != DBNull.Value)
                {
                    sql.Append("organization_parent_id = @organization_parent_id, ");
                    parameters.Add(new SqlParameter("@organization_parent_id", row["organization_parent_id"]));
                }
                else
                    sql.Append("organization_parent_id = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE organization_id = @organization_id");
                parameters.Add(new SqlParameter("@organization_id", row["organization_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);
            }
            else
                throw new Exception("The following Organization needs to be inserted into the database, before it can be updated.\n" + organization.ToString());
        }

        public Organization Write(Organization organization, IConnectable connection)
        {
            return Write(organization, -1, connection);
        }

        public Organization Write(Organization organization, int parentOrganizationId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapOrganization(organization, parentOrganizationId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["name"] != DBNull.Value)
            {
                sqlValues.Append("@name, ");
                parameters.Add(new SqlParameter("@name", row["name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_organization_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_organization_id, ");
                parameters.Add(new SqlParameter("@enum_organization_id", row["enum_organization_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["identifier"] != DBNull.Value)
            {
                sqlValues.Append("@identifier, ");
                parameters.Add(new SqlParameter("@identifier", row["identifier"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["sis_building_code"] != DBNull.Value)
            {
                sqlValues.Append("@sis_building_code, ");
                parameters.Add(new SqlParameter("@sis_building_code", row["sis_building_code"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["hr_building_code"] != DBNull.Value)
            {
                sqlValues.Append("@hr_building_code, ");
                parameters.Add(new SqlParameter("@hr_building_code", row["hr_building_code"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["organization_parent_id"] != DBNull.Value)
            {
                sqlValues.Append("@organization_parent_id, ");
                parameters.Add(new SqlParameter("@organization_parent_id", row["organization_parent_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql = "INSERT INTO organization " +
                "(name, enum_organization_id, identifier, sis_building_code, hr_building_code, " +
                "organization_parent_id, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["organization_id"] = connection.InsertNewRecord(sql.ToString(), parameters);
            Organization result = daoObjectMapper.MapOrganization(row);

            return result;
        }
    }
}
