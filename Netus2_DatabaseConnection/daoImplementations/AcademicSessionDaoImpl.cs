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
    public class AcademicSessionDaoImpl : IAcademicSessionDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(AcademicSession academicSession, IConnectable connection)
        {
            UnlinkChildren(academicSession, connection);
            Delete_ClassesEnrolled(academicSession.Id, connection);

            AcademicSessionDao academicSessionDao = daoObjectMapper.MapAcademicSession(academicSession, -1);

            StringBuilder sql = new StringBuilder("DELETE FROM academic_session WHERE 1=1 ");
            sql.Append("AND academic_session_id = " + academicSessionDao.academic_session_id + " ");
            sql.Append("AND term_code " + (academicSessionDao.term_code != null ? "LIKE '" + academicSessionDao.term_code + "' " : "IS NULL "));
            sql.Append("AND school_year " + (academicSessionDao.school_year != null ? "= " + academicSessionDao.school_year + " " : "IS NULL "));
            sql.Append("AND name " + (academicSessionDao.name != null ? "LIKE '" + academicSessionDao.name + "' " : "IS NULL "));
            sql.Append("AND start_date " + (academicSessionDao.start_date != null ? "= '" + academicSessionDao.start_date + "' " : "IS NULL "));
            sql.Append("AND start_date " + (academicSessionDao.end_date != null ? "= '" + academicSessionDao.end_date + "' " : "IS NULL "));
            sql.Append("AND enum_session_id " + (academicSessionDao.enum_session_id != null ? "= " + academicSessionDao.enum_session_id + " " : "IS NULL "));
            sql.Append("AND parent_session_id " + (academicSessionDao.parent_session_id != null ? "= " + academicSessionDao.parent_session_id + " " : "IS NULL "));
            sql.Append("AND organization_id " + (academicSessionDao.organization_id != null ? "= " + academicSessionDao.organization_id + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_ClassesEnrolled(int academicSessionId, IConnectable connection)
        {
            IClassEnrolledDao classEnrolledDaoImpl = new ClassEnrolledDaoImpl();
            List<ClassEnrolled> foundClassEnrolleds = classEnrolledDaoImpl.Read_UsingAcademicSessionId(academicSessionId, connection);
            foreach (ClassEnrolled foundClassEnrolled in foundClassEnrolleds)
            {
                classEnrolledDaoImpl.Delete(foundClassEnrolled, connection);
            }
        }

        private void UnlinkChildren(AcademicSession academicSession, IConnectable connection)
        {
            List<AcademicSession> childrenToRemove = new List<AcademicSession>();
            foreach (AcademicSession child in academicSession.Children)
            {
                Update(child, connection);
                childrenToRemove.Add(child);
            }
            foreach (AcademicSession child in childrenToRemove)
            {
                academicSession.Children.Remove(child);
            }
        }

        public List<AcademicSession> Read_UsingOrganizationId(int organizationId, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE organization_id = " + organizationId;
            return Read(sql, connection);
        }

        public AcademicSession Read_UsingClassEnrolledId(int classEnrolledId, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE academic_session_id IN (" +
                "SELECT academic_session_id FROM class WHERE class_id = " + classEnrolledId + ")";

            List<AcademicSession> results = Read(sql, connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
        }

        public AcademicSession Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE academic_session_id = " + academicSessionId;

            List<AcademicSession> resutls = Read(sql, connection);
            if (resutls.Count > 0)
                return resutls[0];
            else
                return null;
        }

        public AcademicSession Read_UsingSchoolCode_TermCode_Schoolyear(string schoolCode, string termCode, int schoolYear, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE 1=1 " + 
                "AND term_code = '" + termCode + "' " + 
                "AND school_year = " + schoolYear + " " +
                "AND organization_id in (" +
                "SELECT organization_id FROM organization WHERE building_code LIKE '" + schoolCode + "')";

            List<AcademicSession> resutls = Read(sql, connection);
            if (resutls.Count == 1)
                return resutls[0];
            else if (resutls.Count > 1)
                throw new Exception("Multiple academic_session records found linked to " +
                    "schoolCode: " + schoolCode + 
                    ", termCode: " + termCode + 
                    ", schoolYear: " + schoolYear);
            else
                return null;
        }

        public List<AcademicSession> Read(AcademicSession academicSession, IConnectable connection)
        {
            return Read(academicSession, -1, connection);
        }

        public List<AcademicSession> Read(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            AcademicSessionDao academicSessionDao = daoObjectMapper.MapAcademicSession(academicSession, parentId);

            sql.Append("SELECT * FROM academic_session WHERE 1=1 ");
            if (academicSessionDao.academic_session_id != null)
                sql.Append("AND academic_session_id = " + academicSessionDao.academic_session_id + " ");
            else
            {
                if (academicSessionDao.name != null)
                    sql.Append("AND name = '" + academicSessionDao.name + "' ");
                if (academicSessionDao.term_code != null)
                    sql.Append("AND term_code = '" + academicSessionDao.term_code + "' ");
                if (academicSessionDao.school_year != null)
                    sql.Append("AND school_year = " + academicSessionDao.school_year + " ");
                if (academicSessionDao.start_date != null)
                    sql.Append("AND start_date = '" + academicSessionDao.start_date + "' ");
                if (academicSessionDao.end_date != null)
                    sql.Append("AND end_date = '" + academicSessionDao.end_date + "' ");
                if (academicSessionDao.enum_session_id != null)
                    sql.Append("AND enum_session_id = " + academicSessionDao.enum_session_id + " ");
                if (academicSessionDao.parent_session_id != null)
                    sql.Append("AND parent_session_id = " + academicSessionDao.parent_session_id + " ");
            }

            return Read(sql.ToString(), connection);
        }

        private List<AcademicSession> Read(string sql, IConnectable connection)
        {
            List<AcademicSessionDao> foundAsDaos = new List<AcademicSessionDao>();
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql.ToString());
                while (reader.Read())
                {
                    AcademicSessionDao foundAsDao = new AcademicSessionDao();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                if (value != DBNull.Value)
                                    foundAsDao.academic_session_id = (int)value;
                                else
                                    foundAsDao.academic_session_id = null;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    foundAsDao.term_code = (string)value;
                                else
                                    foundAsDao.term_code = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    foundAsDao.school_year = (int)value;
                                else
                                    foundAsDao.school_year = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    foundAsDao.name = (string)value;
                                else
                                    foundAsDao.name = null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    foundAsDao.start_date = (DateTime)value;
                                else
                                    foundAsDao.start_date = null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    foundAsDao.end_date = (DateTime)value;
                                else
                                    foundAsDao.end_date = null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    foundAsDao.enum_session_id = (int)value;
                                else
                                    foundAsDao.enum_session_id = null;
                                break;
                            case 7:
                                if (value != DBNull.Value)
                                    foundAsDao.parent_session_id = (int)value;
                                else
                                    foundAsDao.parent_session_id = null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    foundAsDao.organization_id = (int)value;
                                else
                                    foundAsDao.organization_id = null;
                                break;
                            case 9:
                                if (value != DBNull.Value)
                                    foundAsDao.created = (DateTime)value;
                                else
                                    foundAsDao.created = (DateTime)value;
                                break;
                            case 10:
                                foundAsDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 11:
                                if (value != DBNull.Value)
                                    foundAsDao.changed = (DateTime)value;
                                else
                                    foundAsDao.changed = null;
                                break;
                            case 12:
                                foundAsDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in academic_session table: " + reader.GetName(i));
                        }
                    }
                    foundAsDaos.Add(foundAsDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<AcademicSession> results = new List<AcademicSession>();
            foreach (AcademicSessionDao foundAsDao in foundAsDaos)
            {
                Organization foundOrg = Read_Organization((int)foundAsDao.organization_id, connection);
                AcademicSession result = daoObjectMapper.MapAcademicSession(foundAsDao, foundOrg);
                result.Children.AddRange(Read_Children(result, connection));
                results.Add(result);
            }
            return results;
        }

        private List<AcademicSession> Read_Children(AcademicSession parent, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE parent_session_id = " + parent.Id;
            return Read(sql, connection);
        }

        private Organization Read_Organization(int organizationId, IConnectable connection)
        {
            IOrganizationDao organizationDaoImpl = new OrganizationDaoImpl();
            return organizationDaoImpl.Read_WithOrganizationId(organizationId, connection);
        }

        public void Update(AcademicSession academicSession, IConnectable connection)
        {
            Update(academicSession, -1, connection);
        }

        public void Update(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            List<AcademicSession> foundAcademicSessions = Read(academicSession, connection);
            if (foundAcademicSessions.Count == 0)
                Write(academicSession, connection);
            else if (foundAcademicSessions.Count == 1)
            {
                academicSession.Id = foundAcademicSessions[0].Id;
                UpdateInternals(academicSession, parentId, connection);
            }
            else
                throw new Exception(foundAcademicSessions.Count + " Academic Sessions found matching the description of:\n" +
                    academicSession.ToString());
        }

        private void UpdateInternals(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            AcademicSessionDao asDao = daoObjectMapper.MapAcademicSession(academicSession, parentId);

            if (asDao.academic_session_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE academic_session SET ");
                sql.Append("term_code = " + (asDao.term_code != null ? "'" + asDao.term_code + "', " : "NULL, "));
                sql.Append("school_year = " + (asDao.school_year != null ? asDao.school_year + ", " : "NULL, "));
                sql.Append("name = " + (asDao.name != null ? "'" + asDao.name + "', " : "NULL, "));
                sql.Append("start_date = " + (asDao.start_date != null ? "'" + asDao.start_date + "', " : "NULL, "));
                sql.Append("end_date = " + (asDao.end_date != null ? "'" + asDao.end_date + "', " : "NULL, "));
                sql.Append("enum_session_id = " + (asDao.enum_session_id != null ? asDao.enum_session_id + ", " : "NULL, "));
                sql.Append("parent_session_id = " + (asDao.parent_session_id != null ? asDao.parent_session_id + ", " : "NULL, "));
                sql.Append("organization_id = " + (asDao.organization_id != null ? asDao.organization_id + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE academic_session_id = " + asDao.academic_session_id);

                connection.ExecuteNonQuery(sql.ToString());
            }
            else
            {
                throw new Exception("The following Academic Session needs to be inserted into the database, before it can be updated.\n" + academicSession.ToString());
            }
        }

        public AcademicSession Write(AcademicSession academicSession, IConnectable connection)
        {
            return Write(academicSession, -1, connection);
        }

        public AcademicSession Write(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            AcademicSessionDao asDao = daoObjectMapper.MapAcademicSession(academicSession, parentId);

            StringBuilder sql = new StringBuilder("INSERT INTO academic_session (");
            sql.Append("term_code, school_year, name, start_date, end_date, enum_session_id, parent_session_id, organization_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(asDao.term_code != null ? "'" + asDao.term_code + "', " : "NULL, ");
            sql.Append(asDao.school_year != null ? asDao.school_year + ", " : "NULL, ");
            sql.Append(asDao.name != null ? "'" + asDao.name + "', " : "NULL, ");
            sql.Append(asDao.start_date != null ? "'" + asDao.start_date + "', " : "NULL, ");
            sql.Append(asDao.end_date != null ? "'" + asDao.end_date + "', " : "NULL, ");
            sql.Append(asDao.enum_session_id != null ? asDao.enum_session_id + ", " : "NULL, ");
            sql.Append(asDao.parent_session_id != null ? asDao.parent_session_id + ", " : "NULL, ");
            sql.Append(asDao.organization_id != null ? asDao.organization_id + ", " : "NULL, ");
            sql.Append("GETDATE(), ");
            sql.Append("'Netus2')");

            asDao.academic_session_id = connection.InsertNewRecord(sql.ToString());

            Organization foundOrg = Read_Organization((int)asDao.organization_id, connection);
            return daoObjectMapper.MapAcademicSession(asDao, foundOrg);
        }
    }
}
