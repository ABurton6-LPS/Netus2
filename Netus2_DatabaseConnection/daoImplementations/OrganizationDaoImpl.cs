using Netus2.daoInterfaces;
using Netus2.daoObjects;
using Netus2.dbAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Netus2.daoImplementations
{
    public class OrganizationDaoImpl : IOrganizationDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(Organization organization, IConnectable connection)
        {
            UnlinkChildren(organization, connection);
            Delete_EmploymentSessions(organization, connection);
            Delete_AcademicSession(organization, connection);

            OrganizationDao organizationDao = daoObjectMapper.MapOrganization(organization, -1);

            StringBuilder sql = new StringBuilder("DELETE FROM organization WHERE 1=1 ");
            sql.Append("AND organization_id = " + organizationDao.organization_id + " ");
            sql.Append("AND name " + (organizationDao.name != null ? "LIKE '" + organizationDao.name + "' " : "IS NULL "));
            sql.Append("AND enum_organization_id " + (organizationDao.enum_organization_id != null ? "= " + organizationDao.enum_organization_id + " " : "IS NULL "));
            sql.Append("AND identifier " + (organizationDao.identifier != null ? "LIKE '" + organizationDao.identifier + "' " : "IS NULL "));
            sql.Append("AND building_code " + (organizationDao.building_code != null ? "LIKE '" + organizationDao.building_code + "' " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_AcademicSession(Organization organization, IConnectable connection)
        {
            IAcademicSessionDao academicSessionDaoImpl = new AcademicSessionDaoImpl();
            List<AcademicSession> foundAcademicSessions = academicSessionDaoImpl.Read_UsingOrganizationId(organization.Id, connection);

            foreach (AcademicSession foundAcademicSession in foundAcademicSessions)
                academicSessionDaoImpl.Delete(foundAcademicSession, connection);
        }

        private void Delete_EmploymentSessions(Organization organization, IConnectable connection)
        {
            IEmploymentSessionDao employmentSessionsDaoImpl = new EmploymentSessionDaoImpl();
            List<EmploymentSession> foundEmploymentSessions = employmentSessionsDaoImpl.Read_WithOrganizationId(null, organization.Id, connection);

            foreach (EmploymentSession foundEmploymentSession in foundEmploymentSessions)
            {
                employmentSessionsDaoImpl.Delete_WithOrganizationId(foundEmploymentSession, organization.Id, connection);
            }
        }

        private void UnlinkChildren(Organization organization, IConnectable connection)
        {
            OrganizationDaoImpl orgDaoImpl = new OrganizationDaoImpl();
            List<Organization> childrenToRemove = new List<Organization>();
            foreach (Organization child in organization.Children)
            {
                childrenToRemove.Add(child);

                List<Organization> foundChildren = orgDaoImpl.Read(child, connection);
                if (foundChildren.Count == 1)
                    Update(foundChildren[0], connection);
                else if (foundChildren.Count == 0)
                    return;
                else
                    throw new Exception(foundChildren.Count + " Organization records found matching:\n" + child.ToString());
            }
            foreach (Organization child in childrenToRemove)
            {
                organization.Children.Remove(child);
            }
        }

        public Organization Read_WithBuildingCode(string buildingCode, IConnectable connection)
        {
            string sql = "SELECT * FROM organization WHERE building_code LIKE ('" + buildingCode + "')";

            List<Organization> results = Read(sql, connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
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
                OrganizationDao organizationDao = daoObjectMapper.MapOrganization(organization, parentId);

                sql.Append("SELECT * FROM organization WHERE 1=1 ");
                if (organizationDao.organization_id != null)
                    sql.Append("AND organization_id = " + organizationDao.organization_id + " ");
                else
                {
                    if (organizationDao.name != null)
                        sql.Append("AND name LIKE '" + organizationDao.name + "' ");
                    if (organizationDao.enum_organization_id != null)
                        sql.Append("AND enum_organization_id = " + organizationDao.enum_organization_id + " ");
                    if (organizationDao.identifier != null)
                        sql.Append("AND identifier LIKE '" + organizationDao.identifier + "' ");
                    if (organizationDao.building_code != null)
                        sql.Append("AND building_code LIKE '" + organizationDao.building_code + "' ");
                    if (organizationDao.organization_parent_id != null)
                        sql.Append("AND organization_parent_id = " + organizationDao.organization_parent_id);
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<Organization> Read(string sql, IConnectable connection)
        {
            List<OrganizationDao> foundOrganizationsDaos = new List<OrganizationDao>();

            SqlDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    OrganizationDao foundOrganizationDao = new OrganizationDao();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                if (value != DBNull.Value)
                                    foundOrganizationDao.organization_id = (int)value;
                                else
                                    foundOrganizationDao.organization_id = null;
                                break;
                            case 1:
                                foundOrganizationDao.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    foundOrganizationDao.enum_organization_id = (int)value;
                                else
                                    foundOrganizationDao.enum_organization_id = null;
                                break;
                            case 3:
                                foundOrganizationDao.identifier = value != DBNull.Value ? (string)value : null;
                                break;
                            case 4:
                                foundOrganizationDao.building_code = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                foundOrganizationDao.organization_parent_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    foundOrganizationDao.created = (DateTime)value;
                                else
                                    foundOrganizationDao.created = null;
                                break;
                            case 7:
                                foundOrganizationDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    foundOrganizationDao.changed = (DateTime)value;
                                else
                                    foundOrganizationDao.changed = null;
                                break;
                            case 9:
                                foundOrganizationDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in organization table: " + reader.GetName(i));
                        }
                    }
                    foundOrganizationsDaos.Add(foundOrganizationDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<Organization> results = new List<Organization>();
            foreach (OrganizationDao foundOrganizationDao in foundOrganizationsDaos)
            {
                results.Add(daoObjectMapper.MapOrganization(foundOrganizationDao));
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
            else if (foundOrganizations.Count > 1)
                throw new Exception(foundOrganizations.Count + " Organizations found matching the description of:\n" +
                    organization.ToString());
        }

        private void UpdateInternals(Organization organization, int parentOrganizationId, IConnectable connection)
        {
            OrganizationDao organizationDao = daoObjectMapper.MapOrganization(organization, parentOrganizationId);

            if (organizationDao.organization_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE organization SET ");
                sql.Append("name = " + (organizationDao.name != null ? "'" + organizationDao.name + "', " : "NULL, "));
                sql.Append("enum_organization_id = " + (organizationDao.enum_organization_id != null ? organizationDao.enum_organization_id + ", " : "NULL, "));
                sql.Append("identifier = " + (organizationDao.identifier != null ? "'" + organizationDao.identifier + "', " : "NULL, "));
                sql.Append("building_code = " + (organizationDao.building_code != null ? "'" + organizationDao.building_code + "', " : "NULL, "));
                sql.Append("organization_parent_id = " + (organizationDao.organization_parent_id != null ? organizationDao.organization_parent_id + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE organization_id = " + organizationDao.organization_id);

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
            OrganizationDao organizationDao = daoObjectMapper.MapOrganization(organization, parentOrganizationId);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(organizationDao.name != null ? "'" + organizationDao.name + "', " : "NULL, ");
            sqlValues.Append(organizationDao.enum_organization_id != null ? organizationDao.enum_organization_id + ", " : "NULL, ");
            sqlValues.Append(organizationDao.identifier != null ? "'" + organizationDao.identifier + "', " : "NULL, ");
            sqlValues.Append(organizationDao.building_code != null ? "'" + organizationDao.building_code + "', " : "NULL, ");
            sqlValues.Append(organizationDao.organization_parent_id != null ? organizationDao.organization_parent_id + ", " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            StringBuilder sql = new StringBuilder("INSERT INTO organization (");
            sql.Append("name, enum_organization_id, identifier, building_code, organization_parent_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(sqlValues.ToString());
            sql.Append(")");

            organizationDao.organization_id = connection.InsertNewRecord(sql.ToString());
            Organization resultOrg = daoObjectMapper.MapOrganization(organizationDao);

            return resultOrg;
        }
    }
}
