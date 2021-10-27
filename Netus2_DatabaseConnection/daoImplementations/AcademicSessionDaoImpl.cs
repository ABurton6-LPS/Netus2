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
    public class AcademicSessionDaoImpl : IAcademicSessionDao
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

        public void Delete(AcademicSession academicSession, IConnectable connection)
        {
            if (academicSession.Id <= 0)
                throw new Exception("Cannot delete an academic session which doesn't have a database-assigned ID.\n" + academicSession.ToString());

            UnlinkChildren(academicSession, connection);
            UnlinkEnrollment(academicSession, connection);
            Delete_ClassesEnrolled(academicSession.Id, connection);

            string sql = "DELETE FROM academic_session WHERE " +
                "academic_session_id = @academic_session_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@academic_session_id", academicSession.Id));

            connection.ExecuteNonQuery(sql, parameters);
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
            List<DataRow> linksToBeRemoved = jctEnrollmentAcademicSessionDaoImpl.Read_AllWithAcademicSessionId(academicSession.Id, connection);
            foreach(DataRow linkToBeRemoved in linksToBeRemoved)
            {
                jctEnrollmentAcademicSessionDaoImpl.Delete((int)linkToBeRemoved["enrollment_id"], (int)linkToBeRemoved["academic_session_id"], connection);
            }
        }

        public List<AcademicSession> Read_AllWithOrganizationId(int organizationId, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE organization_id = @organization_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@organization_id", organizationId));

            return Read(sql, connection, parameters);
        }

        public AcademicSession Read_UsingClassEnrolledId(int classEnrolledId, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE academic_session_id IN (" +
                "SELECT academic_session_id FROM class WHERE class_id = @class_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classEnrolledId));

            List<AcademicSession> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching classEnrolledId: " + classEnrolledId);
        }

        public AcademicSession Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE academic_session_id = @academic_session_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@academic_session_id", academicSessionId));

            List<AcademicSession> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching academicSessionId: " + academicSessionId);
        }

        public AcademicSession Read_UsingSisBuildingCode_TermCode_Schoolyear(string sisBuildingCode, string termCode, int schoolYear, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE 1=1 " + 
                "AND term_code = @term_code " + 
                "AND school_year = @school_year " +
                "AND organization_id in (" +
                "SELECT organization_id FROM organization WHERE sis_building_code = @sis_building_code)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@term_code", termCode));
            parameters.Add(new SqlParameter("@schoolYear", schoolYear));
            parameters.Add(new SqlParameter("@sis_building_code", sisBuildingCode));

            List<AcademicSession> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching sisBuildingCode: " + sisBuildingCode + " and termCode: " + termCode + " and schoolYear: " + schoolYear);
        }

        public AcademicSession Read_Parent(AcademicSession child, IConnectable connection)
        {
            if (child.Id <= 0)
                throw new Exception("Cannot query the database for the parent of a record that " +
                    "has not yet been written to the database.\n" + child.ToString());

            string sql = "SELECT * FROM academic_session WHERE academic_session_id in (" +
                "SELECT parent_session_id FROM academic_session WEHRE academic_session_id = @academic_session_id)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@academic_session_id", child.Id));

            List<AcademicSession> results = Read(sql, connection, parameters);
            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " parent records found matching child: " + child.ToString());
        }

        public List<AcademicSession> Read_Children(AcademicSession parent, IConnectable connection)
        {
            string sql = "SELECT * FROM academic_session WHERE parent_session_id = @parent_session_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@parent_session_id", parent.Id));

            return Read(sql, connection, parameters);
        }

        public List<AcademicSession> Read(AcademicSession academicSession, IConnectable connection)
        {
            return Read(academicSession, -1, connection);
        }

        public List<AcademicSession> Read(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            DataRow row = daoObjectMapper.MapAcademicSession(academicSession, parentId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            sql.Append("SELECT * FROM academic_session WHERE 1=1 ");
            if (row["academic_session_id"] != DBNull.Value)
            {
                sql.Append("AND academic_session_id = @academic_session_id");
                parameters.Add(new SqlParameter("@academic_session_id", row["academic_session_id"]));
            }
            else
            {
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("AND name = @name ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }
                    
                if (row["term_code"] != DBNull.Value)
                {
                    sql.Append("AND term_code = @term_code ");
                    parameters.Add(new SqlParameter("@term_code", row["term_code"]));
                }
                    
                if (row["school_year"] != DBNull.Value)
                {
                    sql.Append("AND school_year = @school_year ");
                    parameters.Add(new SqlParameter("@school_year", row["school_year"]));
                }
                    
                if (row["start_date"] != DBNull.Value)
                {
                    sql.Append("AND start_date = @start_date ");
                    parameters.Add(new SqlParameter("@start_date", row["start_date"]));
                }
                    
                if (row["end_date"] != DBNull.Value)
                {
                    sql.Append("AND end_date = @end_date ");
                    parameters.Add(new SqlParameter("@end_date", row["end_date"]));
                }
                    
                if (row["enum_session_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_session_id = @enum_session_id ");
                    parameters.Add(new SqlParameter("@enum_session_id", row["enum_session_id"]));
                }
                    
                if (row["parent_session_id"] != DBNull.Value)
                {
                    sql.Append("AND parent_session_id = @parent_session_id");
                    parameters.Add(new SqlParameter("@parent_session_id", row["parent_session_id"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<AcademicSession> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtAcademicSession = DataTableFactory.CreateDataTable_Netus2_AcademicSession();
            dtAcademicSession = connection.ReadIntoDataTable(sql, dtAcademicSession, parameters);

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

        private Organization Read_Organization(int organizationId, IConnectable connection)
        {
            IOrganizationDao organizationDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
            return organizationDaoImpl.Read_UsingOrganizationId(organizationId, connection);
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
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE academic_session SET ");
                if(row["term_code"] != DBNull.Value)
                {
                    sql.Append("term_code = @term_code, ");
                    parameters.Add(new SqlParameter("@term_code", row["term_code"]));
                }
                else
                    sql.Append("term_code = NULL, ");

                if (row["school_year"] != DBNull.Value)
                {
                    sql.Append("school_year = @school_year, ");
                    parameters.Add(new SqlParameter("@school_year", row["school_year"]));
                }
                else
                    sql.Append("school_year = NULL, ");

                if (row["name"] != DBNull.Value)
                {
                    sql.Append("name = @name, ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }
                else
                    sql.Append("name = NULL, ");

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

                if (row["enum_session_id"] != DBNull.Value)
                {
                    sql.Append("enum_session_id = @enum_session_id, ");
                    parameters.Add(new SqlParameter("@enum_session_id", row["enum_session_id"]));
                }
                else
                    sql.Append("enum_session_id = NULL, ");

                if (row["parent_session_id"] != DBNull.Value)
                {
                    sql.Append("parent_session_id = @parent_session_id, ");
                    parameters.Add(new SqlParameter("@parent_session_id", row["parent_session_id"]));
                }
                else
                    sql.Append("parent_session_id = NULL, ");

                if (row["organization_id"] != DBNull.Value)
                {
                    sql.Append("organization_id = @organization_id, ");
                    parameters.Add(new SqlParameter("@organization_id", row["organization_id"]));
                }
                else
                    sql.Append("organization_id = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE academic_session_id = @academic_session_id");
                parameters.Add(new SqlParameter("@academic_session_id", row["academic_session_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);
            }
            else
                throw new Exception("The following Academic Session needs to be inserted into the database, before it can be updated.\n" + academicSession.ToString());
        }

        public AcademicSession Write(AcademicSession academicSession, IConnectable connection)
        {
            return Write(academicSession, -1, connection);
        }

        public AcademicSession Write(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapAcademicSession(academicSession, parentId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["term_code"] != DBNull.Value)
            {
                sqlValues.Append("@term_code, ");
                parameters.Add(new SqlParameter("@term_code", row["term_code"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["school_year"] != DBNull.Value)
            {
                sqlValues.Append("@school_year, ");
                parameters.Add(new SqlParameter("@school_year", row["school_year"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["name"] != DBNull.Value)
            {
                sqlValues.Append("@name, ");
                parameters.Add(new SqlParameter("@name", row["name"]));
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

            if (row["enum_session_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_session_id, ");
                parameters.Add(new SqlParameter("@enum_session_id", row["enum_session_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["parent_session_id"] != DBNull.Value)
            {
                sqlValues.Append("@parent_session_id, ");
                parameters.Add(new SqlParameter("@parent_session_id", row["parent_session_id"]));
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

            string sql =
                "INSERT INTO academic_session " +
                "(term_code, school_year, name, start_date, end_date, enum_session_id, " +
                "parent_session_id, organization_id, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["academic_session_id"] = connection.InsertNewRecord(sql, parameters);

            Organization foundOrg = Read_Organization((int)row["organization_id"], connection);
            AcademicSession result =  daoObjectMapper.MapAcademicSession(row, foundOrg);

            return result;
        }
    }
}
