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
            UnlinkChildren(organization, connection);
            Delete_EmploymentSessions(organization, connection);
            Delete_AcademicSession(organization, connection);

            DataRow row = daoObjectMapper.MapOrganization(organization, -1);

            StringBuilder sql = new StringBuilder("DELETE FROM organization WHERE 1=1 ");
            sql.Append("AND organization_id = " + row["organization_id"] + " ");
            sql.Append("AND name " + (row["name"] != DBNull.Value ? "LIKE '" + row["name"] + "' " : "IS NULL "));
            sql.Append("AND enum_organization_id " + (row["enum_organization_id"] != DBNull.Value ? "= " + row["enum_organization_id"] + " " : "IS NULL "));
            sql.Append("AND identifier " + (row["identifier"] != DBNull.Value ? "LIKE '" + row["identifier"] + "' " : "IS NULL "));
            sql.Append("AND sis_building_code " + (row["sis_building_code"] != DBNull.Value ? "LIKE '" + row["sis_building_code"] + "' " : "IS NULL "));
            sql.Append("AND hr_building_code " + (row["hr_building_code"] != DBNull.Value ? "LIKE '" + row["hr_building_code"] + "' " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_AcademicSession(Organization organization, IConnectable connection)
        {
            IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
            List<AcademicSession> foundAcademicSessions = academicSessionDaoImpl.Read_UsingOrganizationId(organization.Id, connection);

            foreach (AcademicSession foundAcademicSession in foundAcademicSessions)
                academicSessionDaoImpl.Delete(foundAcademicSession, connection);
        }

        private void Delete_EmploymentSessions(Organization organization, IConnectable connection)
        {
            IEmploymentSessionDao employmentSessionsDaoImpl = DaoImplFactory.GetEmploymentSessionDaoImpl();
            List<EmploymentSession> foundEmploymentSessions = employmentSessionsDaoImpl.Read_WithOrganizationId(null, organization.Id, connection);

            foreach (EmploymentSession foundEmploymentSession in foundEmploymentSessions)
            {
                employmentSessionsDaoImpl.Delete_WithOrganizationId(foundEmploymentSession, organization.Id, connection);
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

        public Organization Read_WithSisBuildingCode(string sisBuildingCode, IConnectable connection)
        {
            string sql = "SELECT * FROM organization WHERE sis_building_code = '" + sisBuildingCode + "'";

            List<Organization> results = Read(sql, connection);
            if (results.Count == 1)
                return results[0];
            else if (results.Count == 0)
                return null;
            else
                throw new Exception(results.Count + "Organization records linked to sisBuildingCode: " + sisBuildingCode);
        }

        public Organization Read_WithOrganizationId(int orgId, IConnectable connection)
        {
            string sql = "SELECT * FROM organization WHERE organization_id = " + orgId;

            List<Organization> results = Read(sql, connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
        }

        public Organization Read_WithAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            string sql = "SELECT * FROM organization WHERE organization_id IN (" +
                "SELECT organization_id FROM academic_session WHERE academic_session_id = " +
                academicSessionId + ")";

            List<Organization> results = Read(sql, connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
        }

        public Organization Read_Parent(Organization organization, IConnectable connection)
        {
            string sql = "SELECT * FROM organization WHERE organization_id in ( " +
                "SELECT organization_parent_id FROM organization WHERE organization_id = " + organization.Id + ")";

            List<Organization> results = Read(sql, connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
        }

        public List<Organization> Read(Organization organization, IConnectable connection)
        {
            return Read(organization, -1, connection);
        }

        public List<Organization> Read(Organization organization, int parentId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            if (organization == null)
            {
                sql.Append("SELECT * FROM organization WHERE organization_parent_id = " + parentId);
            }
            else
            {
                DataRow row = daoObjectMapper.MapOrganization(organization, parentId);

                sql.Append("SELECT * FROM organization WHERE 1=1 ");
                if (row["organization_id"] != DBNull.Value)
                    sql.Append("AND organization_id = " + row["organization_id"] + " ");
                else
                {
                    if (row["name"] != DBNull.Value)
                        sql.Append("AND name = '" + row["name"] + "' ");
                    if (row["enum_organization_id"] != DBNull.Value)
                        sql.Append("AND enum_organization_id = " + row["enum_organization_id"] + " ");
                    if (row["identifier"] != DBNull.Value)
                        sql.Append("AND identifier = '" + row["identifier"] + "' ");
                    if (row["sis_building_code"] != DBNull.Value)
                        sql.Append("AND sis_building_code = '" + row["sis_building_code"] + "' ");
                    if (row["hr_building_code"] != DBNull.Value)
                        sql.Append("AND hr_building_code = '" + row["hr_building_code"] + "' ");
                    if (row["organization_parent_id"] != DBNull.Value)
                        sql.Append("AND organization_parent_id = " + row["organization_parent_id"]);
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<Organization> Read(string sql, IConnectable connection)
        {
            DataTable dtOrganization = DataTableFactory.CreateDataTable_Netus2_Organization();
            dtOrganization = connection.ReadIntoDataTable(sql, dtOrganization);

            List<Organization> results = new List<Organization>();
            foreach (DataRow row in dtOrganization.Rows)
            {
                results.Add(daoObjectMapper.MapOrganization(row));
            }

            foreach (Organization result in results)
            {
                result.Children.AddRange(Read(null, result.Id, connection));
            }

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
                StringBuilder sql = new StringBuilder("UPDATE organization SET ");
                sql.Append("name = " + (row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, "));
                sql.Append("enum_organization_id = " + (row["enum_organization_id"] != DBNull.Value ? row["enum_organization_id"] + ", " : "NULL, "));
                sql.Append("identifier = " + (row["identifier"] != DBNull.Value ? "'" + row["identifier"] + "', " : "NULL, "));
                sql.Append("sis_building_code = " + (row["sis_building_code"] != DBNull.Value ? "'" + row["sis_building_code"] + "', " : "NULL, "));
                sql.Append("hr_building_code = " + (row["hr_building_code"] != DBNull.Value ? "'" + row["hr_building_code"] + "', " : "NULL, "));
                sql.Append("organization_parent_id = " + (row["organization_parent_id"] != DBNull.Value ? row["organization_parent_id"] + ", " : "NULL, "));
                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE organization_id = " + row["organization_id"]);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
            {
                throw new Exception("The following Organization needs to be inserted into the database, before it can be updated.\n" + organization.ToString());
            }
        }

        public Organization Write(Organization organization, IConnectable connection)
        {
            return Write(organization, -1, connection);
        }

        public Organization Write(Organization organization, int parentOrganizationId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapOrganization(organization, parentOrganizationId);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, ");
            sqlValues.Append(row["enum_organization_id"] != DBNull.Value ? row["enum_organization_id"] + ", " : "NULL, ");
            sqlValues.Append(row["identifier"] != DBNull.Value ? "'" + row["identifier"] + "', " : "NULL, ");
            sqlValues.Append(row["sis_building_code"] != DBNull.Value ? "'" + row["sis_building_code"] + "', " : "NULL, ");
            sqlValues.Append(row["hr_building_code"] != DBNull.Value ? "'" + row["hr_building_code"] + "', " : "NULL, ");
            sqlValues.Append(row["organization_parent_id"] != DBNull.Value ? row["organization_parent_id"] + ", " : "NULL, ");
            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            StringBuilder sql = new StringBuilder("INSERT INTO organization (");
            sql.Append("name, enum_organization_id, identifier, sis_building_code, hr_building_code, organization_parent_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(sqlValues.ToString());
            sql.Append(")");

            row["organization_id"] = connection.InsertNewRecord(sql.ToString());
            Organization resultOrg = daoObjectMapper.MapOrganization(row);

            return resultOrg;
        }
    }
}
