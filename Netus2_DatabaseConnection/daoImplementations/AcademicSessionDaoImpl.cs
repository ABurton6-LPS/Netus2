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
    public class AcademicSessionDaoImpl : IAcademicSessionDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(AcademicSession academicSession, IConnectable connection)
        {
            UnlinkChildren(academicSession, connection);
            UnlinkEnrollment(academicSession, connection);
            Delete_ClassesEnrolled(academicSession.Id, connection);

            DataRow row = daoObjectMapper.MapAcademicSession(academicSession, -1);

            StringBuilder sql = new StringBuilder("DELETE FROM academic_session WHERE 1=1 ");
            sql.Append("AND academic_session_id = " + row["academic_session_id"] + " ");
            sql.Append("AND term_code " + (row["term_code"] != DBNull.Value ? "LIKE '" + row["term_code"] + "' " : "IS NULL "));
            sql.Append("AND school_year " + (row["school_year"] != DBNull.Value ? "= " + row["school_year"] + " " : "IS NULL "));
            sql.Append("AND name " + (row["name"] != DBNull.Value ? "LIKE '" + row["name"] + "' " : "IS NULL "));
            sql.Append("AND start_date " + (row["start_date"] != DBNull.Value ? "= '" + row["start_date"] + "' " : "IS NULL "));
            sql.Append("AND end_date " + (row["end_date"] != DBNull.Value ? "= '" + row["end_date"] + "' " : "IS NULL "));
            sql.Append("AND enum_session_id " + (row["enum_session_id"] != DBNull.Value ? "= " + row["enum_session_id"] + " " : "IS NULL "));
            sql.Append("AND organization_id " + (row["organization_id"] != DBNull.Value ? "= " + row["organization_id"]: "IS NULL"));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_ClassesEnrolled(int academicSessionId, IConnectable connection)
        {
            IClassEnrolledDao classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
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

        private void UnlinkEnrollment(AcademicSession academicSession, IConnectable connection)
        {
            IJctEnrollmentAcademicSessionDao jctEnrollmentAcademicSessionDaoImpl = DaoImplFactory.GetJctEnrollmentAcademicSessionDaoImpl();
            List<DataRow> linksToBeRemoved = jctEnrollmentAcademicSessionDaoImpl.Read_WithAcademicSessionId(academicSession.Id, connection);
            foreach(DataRow linkToBeRemoved in linksToBeRemoved)
            {
                jctEnrollmentAcademicSessionDaoImpl.Delete((int)linkToBeRemoved["enrollment_id"], (int)linkToBeRemoved["academic_session_id"], connection);
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

        public AcademicSession Read_UsingSisBuildingCode_TermCode_Schoolyear(string sisBuildingCode, string termCode, int schoolYear, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE 1=1 " + 
                "AND term_code = '" + termCode + "' " + 
                "AND school_year = " + schoolYear + " " +
                "AND organization_id in (" +
                "SELECT organization_id FROM organization WHERE sis_building_code LIKE '" + sisBuildingCode + "')";

            List<AcademicSession> resutls = Read(sql, connection);
            if (resutls.Count == 1)
                return resutls[0];
            else if (resutls.Count > 1)
                throw new Exception("Multiple academic_session records found linked to " +
                    "sisBuildingCode: " + sisBuildingCode + 
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

            DataRow row = daoObjectMapper.MapAcademicSession(academicSession, parentId);

            sql.Append("SELECT * FROM academic_session WHERE 1=1 ");
            if (row["academic_session_id"] != DBNull.Value)
                sql.Append("AND academic_session_id = " + row["academic_session_id"] + " ");
            else
            {
                if (row["name"] != DBNull.Value)
                    sql.Append("AND name = '" + row["name"] + "' ");
                if (row["term_code"] != DBNull.Value)
                    sql.Append("AND term_code = '" + row["term_code"] + "' ");
                if (row["school_year"] != DBNull.Value)
                    sql.Append("AND school_year = " + row["school_year"] + " ");
                if (row["start_date"] != DBNull.Value)
                    sql.Append("AND start_date = '" + row["start_date"] + "' ");
                if (row["end_date"] != DBNull.Value)
                    sql.Append("AND end_date = '" + row["end_date"] + "' ");
                if (row["enum_session_id"] != DBNull.Value)
                    sql.Append("AND enum_session_id = " + row["enum_session_id"] + " ");
                if (row["parent_session_id"] != DBNull.Value)
                    sql.Append("AND parent_session_id = " + row["parent_session_id"] + " ");
            }

            return Read(sql.ToString(), connection);
        }

        private List<AcademicSession> Read(string sql, IConnectable connection)
        {
            DataTable dtAcademicSession = new DataTableFactory().Dt_Netus2_AcademicSession;
            dtAcademicSession = connection.ReadIntoDataTable(sql, dtAcademicSession);

            List<AcademicSession> results = new List<AcademicSession>();
            foreach (DataRow row in dtAcademicSession.Rows)
            {                
                Organization foundOrg = Read_Organization((int)row["organization_id"], connection);
                AcademicSession result = daoObjectMapper.MapAcademicSession(row, foundOrg);
                result.Children.AddRange(Read_Children(result, connection));
                results.Add(result);
            }
            return results;
        }

        public List<AcademicSession> Read_Children(AcademicSession parent, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE parent_session_id = " + parent.Id;
            return Read(sql, connection);
        }

        private Organization Read_Organization(int organizationId, IConnectable connection)
        {
            IOrganizationDao organizationDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
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
                if (parentId == -1)
                    Write(academicSession, connection);
                else
                    Write(academicSession, parentId, connection);
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
            DataRow row = daoObjectMapper.MapAcademicSession(academicSession, parentId);

            if (row["academic_session_id"] != DBNull.Value)
            {
                StringBuilder sql = new StringBuilder("UPDATE academic_session SET ");
                sql.Append("term_code = " + (row["term_code"] != DBNull.Value ? "'" + row["term_code"] + "', " : "NULL, "));
                sql.Append("school_year = " + (row["school_year"] != DBNull.Value ? row["school_year"] + ", " : "NULL, "));
                sql.Append("name = " + (row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, "));
                sql.Append("start_date = " + (row["start_date"] != DBNull.Value ? "'" + row["start_date"] + "', " : "NULL, "));
                sql.Append("end_date = " + (row["end_date"] != DBNull.Value ? "'" + row["end_date"] + "', " : "NULL, "));
                sql.Append("enum_session_id = " + (row["enum_session_id"] != DBNull.Value ? row["enum_session_id"] + ", " : "NULL, "));
                sql.Append("parent_session_id = " + (row["parent_session_id"] != DBNull.Value ? row["parent_session_id"] + ", " : "NULL, "));
                sql.Append("organization_id = " + (row["organization_id"] != DBNull.Value ? row["organization_id"] + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE academic_session_id = " + row["academic_session_id"]);

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
            DataRow row = daoObjectMapper.MapAcademicSession(academicSession, parentId);

            StringBuilder sql = new StringBuilder("INSERT INTO academic_session (");
            sql.Append("term_code, school_year, name, start_date, end_date, enum_session_id, parent_session_id, organization_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(row["term_code"] != DBNull.Value ? "'" + row["term_code"] + "', " : "NULL, ");
            sql.Append(row["school_year"] != DBNull.Value ? row["school_year"] + ", " : "NULL, ");
            sql.Append(row["name"] != DBNull.Value ? "'" + row["name"] + "', " : "NULL, ");
            sql.Append(row["start_date"] != DBNull.Value ? "'" + row["start_date"] + "', " : "NULL, ");
            sql.Append(row["end_date"] != DBNull.Value ? "'" + row["end_date"] + "', " : "NULL, ");
            sql.Append(row["enum_session_id"] != DBNull.Value ? row["enum_session_id"] + ", " : "NULL, ");
            sql.Append(row["parent_session_id"] != DBNull.Value ? row["parent_session_id"] + ", " : "NULL, ");
            sql.Append(row["organization_id"] != DBNull.Value ? row["organization_id"] + ", " : "NULL, ");
            sql.Append("GETDATE(), ");
            sql.Append("'Netus2')");

            row["academic_session_id"] = connection.InsertNewRecord(sql.ToString());

            Organization foundOrg = Read_Organization((int)row["organization_id"], connection);
            return daoObjectMapper.MapAcademicSession(row, foundOrg);
        }
    }
}
