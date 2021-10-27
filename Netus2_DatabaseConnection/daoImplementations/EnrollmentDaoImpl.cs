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
    public class EnrollmentDaoImpl : IEnrollmentDao
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

        public void Delete(Enrollment enrollment, IConnectable connection)
        {
            if(enrollment.Id <= 0)
                throw new Exception("Cannot delete an enrollment which doesn't have a database-assigned ID.\n" + enrollment.ToString());

            Delete_JctEnrollmentAcademicSession(enrollment.AcademicSessions, enrollment.Id, connection);

            string sql = "DELETE FROM enrollment WHERE " +
                "enrollment_id = @enrollment_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enrollment_id", enrollment.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void Delete_JctEnrollmentAcademicSession(List<AcademicSession> academicSessions, int enrollmentId, IConnectable connection)
        {
            IJctEnrollmentAcademicSessionDao jctEnrollmentAcademicSessionDaoImpl = DaoImplFactory.GetJctEnrollmentAcademicSessionDaoImpl();
            foreach (AcademicSession academicSession in academicSessions)
            {
                jctEnrollmentAcademicSessionDaoImpl.Delete(enrollmentId, academicSession.Id, connection);
            }
        }

        public List<Enrollment> Read_AllWithClassId(int classId, IConnectable connection)
        {
            string sql = "SELECT * FROM enrollment WHERE class_id = @class_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classId));

            return Read(sql, connection, parameters);
        }

        public List<Enrollment> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM enrollment WHERE person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            return Read(sql, connection, parameters);
        }

        public List<Enrollment> Read(Enrollment enrollment, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEnrollment(enrollment, personId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM enrollment WHERE 1=1 ");
            if (row["enrollment_id"] != DBNull.Value)
            {
                sql.Append("AND enrollment_id = @enrollment_id");
                parameters.Add(new SqlParameter("@enrollment_id", row["enrollment_id"]));
            }
            else
            {
                if (row["person_id"] != DBNull.Value)
                {
                    sql.Append("AND person_id = @person_id ");
                    parameters.Add(new SqlParameter("@person_id", row["person_id"]));
                }

                if (row["class_id"] != DBNull.Value)
                {
                    sql.Append("AND class_id = @class_id ");
                    parameters.Add(new SqlParameter("@class_id", row["class_id"]));
                }

                if (row["enum_grade_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_grade_id = @enum_grade_id ");
                    parameters.Add(new SqlParameter("@enum_grade_id", row["enum_grade_id"]));
                }

                if (row["is_primary_id"] != DBNull.Value)
                {
                    sql.Append("AND is_primary_id = @is_primary_id");
                    parameters.Add(new SqlParameter("@is_primary_id", row["is_primary_id"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<Enrollment> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtEnrollment = DataTableFactory.CreateDataTable_Netus2_Enrollment();
            dtEnrollment = connection.ReadIntoDataTable(sql, dtEnrollment, parameters);

            List<Enrollment> results = new List<Enrollment>();
            foreach (DataRow foundEnrollmentDao in dtEnrollment.Rows)
            {
                ClassEnrolled foundClassEnrolled = null;
                if (foundEnrollmentDao["class_id"] != DBNull.Value)
                    foundClassEnrolled = Read_ClassEnrolled((int)foundEnrollmentDao["class_id"], connection);

                List<AcademicSession> academicSessions = Read_AcademicSessions((int)foundEnrollmentDao["enrollment_id"], connection);
                results.Add(daoObjectMapper.MapEnrollment(foundEnrollmentDao, foundClassEnrolled, academicSessions));
            }

            return results;
        }

        private ClassEnrolled Read_ClassEnrolled(int classEnrolledId, IConnectable connection)
        {
            IClassEnrolledDao classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
            return classEnrolledDaoImpl.Read_UsingClassId(classEnrolledId, connection);
        }

        private List<AcademicSession> Read_AcademicSessions(int enrollmentId, IConnectable connection)
        {
            List<AcademicSession> foundAcademicSessions = new List<AcademicSession>();

            IJctEnrollmentAcademicSessionDao jctEnrollmentAcademicSessionDaoImpl = DaoImplFactory.GetJctEnrollmentAcademicSessionDaoImpl();
            List<DataRow> foundJctEnrollmentAcademicSessionDaos =
                jctEnrollmentAcademicSessionDaoImpl.Read_AllWithEnrollmentId(enrollmentId, connection);

            IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();

            foreach (DataRow foundJctEnrollmentAcademicSessionDao in foundJctEnrollmentAcademicSessionDaos)
            {
                foundAcademicSessions.Add(
                    academicSessionDaoImpl.Read_UsingAcademicSessionId((int)foundJctEnrollmentAcademicSessionDao["academic_session_id"], connection));
            }

            return foundAcademicSessions;
        }

        public void Update(Enrollment enrollment, int personId, IConnectable connection)
        {
            List<Enrollment> foundEnrollments = Read(enrollment, personId, connection);
            if (foundEnrollments.Count == 0)
                Write(enrollment, personId, connection);
            else if (foundEnrollments.Count == 1)
            {
                enrollment.Id = foundEnrollments[0].Id;
                UpdateInternals(enrollment, personId, connection);
            }
            else
                throw new Exception(foundEnrollments.Count + " Enrollments found matching the description of:\n" +
                    enrollment.ToString());
        }

        private void UpdateInternals(Enrollment enrollment, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEnrollment(enrollment, personId);

            if (row["enrollment_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE enrollment SET ");
                if (row["person_id"] != DBNull.Value)
                {
                    sql.Append("person_id = @person_id, ");
                    parameters.Add(new SqlParameter("@person_id", row["person_id"]));
                }
                else
                    sql.Append("person_id = NULL, ");

                if (row["class_id"] != DBNull.Value)
                {
                    sql.Append("class_id = @class_id, ");
                    parameters.Add(new SqlParameter("@class_id", row["class_id"]));
                }
                else
                    sql.Append("class_id = NULL, ");

                if (row["enum_grade_id"] != DBNull.Value)
                {
                    sql.Append("enum_grade_id = @enum_grade_id, ");
                    parameters.Add(new SqlParameter("@enum_grade_id", row["enum_grade_id"]));
                }
                else
                    sql.Append("enum_grade_id = NULL, ");

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

                if (row["is_primary_id"] != DBNull.Value)
                {
                    sql.Append("is_primary_id = @is_primary_id, ");
                    parameters.Add(new SqlParameter("@is_primary_id", row["is_primary_id"]));
                }
                else
                    sql.Append("is_primary_id = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE enrollment_id = @enrollment_id");
                parameters.Add(new SqlParameter("@enrollment_id", row["enrollment_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);

                Update_JctEnrollmentAcademicSession(enrollment.AcademicSessions, (int)row["enrollment_id"], connection);
            }
            else
                throw new Exception("The following Enrollment needs to be inserted into the database, before it can be updated.\n" + enrollment.ToString());
        }

        public Enrollment Write(Enrollment enrollment, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEnrollment(enrollment, personId);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder("");
            if (row["person_id"] != DBNull.Value)
            {
                sqlValues.Append("@person_id, ");
                parameters.Add(new SqlParameter("@person_id", row["person_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["class_id"] != DBNull.Value)
            {
                sqlValues.Append("@class_id, ");
                parameters.Add(new SqlParameter("@class_id", row["class_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_grade_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_grade_id, ");
                parameters.Add(new SqlParameter("@enum_grade_id", row["enum_grade_id"]));
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

            if (row["is_primary_id"] != DBNull.Value)
            {
                sqlValues.Append("@is_primary_id, ");
                parameters.Add(new SqlParameter("@is_primary_id", row["is_primary_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql = "INSERT INTO enrollment " +
                "(person_id, class_id, enum_grade_id, start_date, end_date, is_primary_id, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["enrollment_id"] = connection.InsertNewRecord(sql, parameters);

            List<AcademicSession> foundAcademicSessions = Update_JctEnrollmentAcademicSession(enrollment.AcademicSessions, (int)row["enrollment_id"], connection);
            ClassEnrolled foundClassEnrolled = Read_ClassEnrolled((int)row["class_id"], connection);
            Enrollment result = daoObjectMapper.MapEnrollment(row, foundClassEnrolled, foundAcademicSessions);

            return result;
        }

        private List<AcademicSession> Update_JctEnrollmentAcademicSession(List<AcademicSession> academicSessions, int enrollmentId, IConnectable connection)
        {
            IJctEnrollmentAcademicSessionDao jctEnrollmentAcademicSessionDaoImpl = DaoImplFactory.GetJctEnrollmentAcademicSessionDaoImpl();
            List<DataRow> foundJctEnrollmentAcademicSessionDaos =
                jctEnrollmentAcademicSessionDaoImpl.Read_AllWithEnrollmentId(enrollmentId, connection);
            List<int> academicSessionIds = new List<int>();
            List<int> foundAcademicSessionsIds = new List<int>();

            foreach (AcademicSession academicSession in academicSessions)
            {
                academicSessionIds.Add(academicSession.Id);
            }

            foreach (DataRow foundJctEnrollmentAcademicSessionDao in foundJctEnrollmentAcademicSessionDaos)
            {
                foundAcademicSessionsIds.Add((int)foundJctEnrollmentAcademicSessionDao["academic_session_id"]);
            }

            foreach (int academicSessionId in academicSessionIds)
            {
                if (foundAcademicSessionsIds.Contains(academicSessionId) == false)
                    jctEnrollmentAcademicSessionDaoImpl.Write(enrollmentId, academicSessionId, connection);
            }

            foreach (int foundAcademicSessionid in foundAcademicSessionsIds)
            {
                if (academicSessionIds.Contains(foundAcademicSessionid) == false)
                    jctEnrollmentAcademicSessionDaoImpl.Delete(enrollmentId, foundAcademicSessionid, connection);
            }

            return Read_AcademicSessions(enrollmentId, connection);
        }
    }
}