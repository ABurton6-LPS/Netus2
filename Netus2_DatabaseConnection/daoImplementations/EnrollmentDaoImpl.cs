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

            Delete_JctEnrollmentClassEnrolled(enrollment, connection);

            string sql = "DELETE FROM enrollment WHERE " +
                "enrollment_id = @enrollment_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@enrollment_id", enrollment.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void Delete_JctEnrollmentClassEnrolled(Enrollment enrollment, IConnectable connection)
        {
            IJctEnrollmentClassEnrolledDao jctEnrollmentClassEnrolledDaoImpl = DaoImplFactory.GetJctEnrollmentClassEnrolledDaoImpl();
            List<DataRow> jctEnrollmentClassEnrolledDaos =
                jctEnrollmentClassEnrolledDaoImpl.Read_AllWithEnrollmentId(enrollment.Id, connection);

            foreach (DataRow jctEnrollmentClassEnrolledDao in jctEnrollmentClassEnrolledDaos)
            {
                jctEnrollmentClassEnrolledDaoImpl.Delete(
                    (int)jctEnrollmentClassEnrolledDao["enrollment_id"],
                    (int)jctEnrollmentClassEnrolledDao["class_enrolled_id"],
                    connection);
            }
        }

        public List<Enrollment> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM enrollment WHERE person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            return Read(sql, connection, parameters);
        }

        public List<Enrollment> Read_AllWithAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            string sql = "SELECT * FROM enrollment WHERE academic_session_id = @academic_session_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@academic_session_id", academicSessionId));

            return Read(sql, connection, parameters);
        }

        public Enrollment Read_UsingAcademicSessionIdAndPersonId(int academicSessionId, int personId, IConnectable connection)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            string sql = "SELECT * FROM enrollment WHERE academic_session_id = @academic_session_id AND person_id = @person_id";

            parameters.Add(new SqlParameter("@academic_session_id", academicSessionId));
            parameters.Add(new SqlParameter("@person_id", personId));

            List<Enrollment> results = Read(sql, connection, parameters);

            if (results.Count > 0)
                return results[0];
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found with academicSessionId: " + academicSessionId + " and personId: " + personId);
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

                if (row["academic_session_id"] != DBNull.Value)
                {
                    sql.Append("AND academic_session_id = @academic_session_id ");
                    parameters.Add(new SqlParameter("@academic_session_id", row["academic_session_id"]));
                }

                if (row["enum_grade_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_grade_id = @enum_grade_id ");
                    parameters.Add(new SqlParameter("@enum_grade_id", row["enum_grade_id"]));
                }

                if(row["start_date"] != DBNull.Value)
                {
                    sql.Append("AND start_date = @start_date ");
                    parameters.Add(new SqlParameter("@start_date", row["start_date"]));
                }

                if(row["end_date"] != DBNull.Value)
                {
                    sql.Append("AND end_date = @end_date");
                    parameters.Add(new SqlParameter("@end_date", row["end_date"]));
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
                List<ClassEnrolled> foundClassesEnrolled = Read_ClassesEnrolled((int)foundEnrollmentDao["enrollment_id"], connection);

                AcademicSession foundAcademicSession = null;
                if(foundEnrollmentDao["academic_session_id"] != DBNull.Value)
                    foundAcademicSession = Read_AcademicSession((int)foundEnrollmentDao["academic_session_id"], connection);
                results.Add(daoObjectMapper.MapEnrollment(foundEnrollmentDao, foundClassesEnrolled, foundAcademicSession));
            }

            return results;
        }

        private List<ClassEnrolled> Read_ClassesEnrolled(int enrollmentId, IConnectable connection)
        {
            List<ClassEnrolled> classesEnrolled = new List<ClassEnrolled>();

            IJctEnrollmentClassEnrolledDao jctEnrollmentClassEnrolledDaoImpl = DaoImplFactory.GetJctEnrollmentClassEnrolledDaoImpl();
            List<DataRow> foundJctEnrolledClassEnrolledDaos =
                jctEnrollmentClassEnrolledDaoImpl.Read_AllWithEnrollmentId(enrollmentId, connection);

            IClassEnrolledDao classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();

            foreach(DataRow foundJctEnrollmentClassEnrolledDao in foundJctEnrolledClassEnrolledDaos)
            {
                classesEnrolled.Add(
                    classEnrolledDaoImpl.Read_UsingClassId((int)foundJctEnrollmentClassEnrolledDao["class_enrolled_id"], connection));
            }

            return classesEnrolled;
        }

        private AcademicSession Read_AcademicSession(int academicSessionId, IConnectable connection)
        {
            IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
            return academicSessionDaoImpl.Read_UsingAcademicSessionId(academicSessionId, connection);
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

                if (row["academic_session_id"] != DBNull.Value)
                {
                    sql.Append("academic_session_id = @academic_session_id, ");
                    parameters.Add(new SqlParameter("@academic_session_id", row["academic_session_id"]));
                }
                else
                    sql.Append("academic_session_id = NULL, ");

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

                Update_JctEnrollmentClassEnrolled(enrollment.ClassesEnrolled, (int)row["enrollment_id"], connection);
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

            if (row["academic_session_id"] != DBNull.Value)
            {
                sqlValues.Append("@academic_session_id, ");
                parameters.Add(new SqlParameter("@academic_session_id", row["academic_session_id"]));
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
                "(person_id, academic_session_id, enum_grade_id, start_date, end_date, is_primary_id, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["enrollment_id"] = connection.InsertNewRecord(sql, parameters);

            List<ClassEnrolled> foundClassesEnrolled = Update_JctEnrollmentClassEnrolled(enrollment.ClassesEnrolled, (int)row["enrollment_id"], connection);
            AcademicSession foundAcademicSession = row["academic_session_id"] != DBNull.Value ? Read_AcademicSession((int)row["academic_session_id"], connection) : null;
            Enrollment result = daoObjectMapper.MapEnrollment(row, foundClassesEnrolled, foundAcademicSession);

            return result;
        }

        private List<ClassEnrolled> Update_JctEnrollmentClassEnrolled(List<ClassEnrolled> classesEnrolled, int enrollmentId, IConnectable connection)
        {
            List<ClassEnrolled> classesEnrolledToBeReturned = new List<ClassEnrolled>();

            IClassEnrolledDao classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
            IJctEnrollmentClassEnrolledDao jctEnrollmentClassEnrolledDaoImpl = DaoImplFactory.GetJctEnrollmentClassEnrolledDaoImpl();

            List<int> passedInClassEnrolledIds = new List<int>();
            foreach(ClassEnrolled passedInClassEnrolled in classesEnrolled)
                if(passedInClassEnrolled.Id <= 0)
                    throw new Exception("Class Enrolled must be in the database before it can be linked with a person. Class Enrolled: " + passedInClassEnrolled.ToString());
                else
                {
                    passedInClassEnrolledIds.Add(passedInClassEnrolled.Id);

                    DataRow foundJctEnrollmentClassEnrolled = jctEnrollmentClassEnrolledDaoImpl.Read(enrollmentId, passedInClassEnrolled.Id, connection);

                    if (foundJctEnrollmentClassEnrolled == null)
                        foundJctEnrollmentClassEnrolled = jctEnrollmentClassEnrolledDaoImpl.Write(enrollmentId, passedInClassEnrolled.Id, 
                            passedInClassEnrolled.EnrollmentStartDate, passedInClassEnrolled.EnrollmentEndDate, connection);

                    ClassEnrolled foundClassEnrolled = classEnrolledDaoImpl.Read_UsingClassId(passedInClassEnrolled.Id, connection);

                    if(foundClassEnrolled != null)
                    {
                        if (foundJctEnrollmentClassEnrolled["enrollment_start_date"] != DBNull.Value)
                            foundClassEnrolled.EnrollmentStartDate = (DateTime)foundJctEnrollmentClassEnrolled["enrollment_start_date"];
                        else
                            foundClassEnrolled.EnrollmentStartDate = null;
                        
                        if (foundJctEnrollmentClassEnrolled["enrollment_end_date"] != DBNull.Value)
                            foundClassEnrolled.EnrollmentEndDate = (DateTime)foundJctEnrollmentClassEnrolled["enrollment_end_date"];
                        else
                            foundClassEnrolled.EnrollmentEndDate = null;

                        classesEnrolledToBeReturned.Add(foundClassEnrolled);
                    }
                }

            List<DataRow> foundJctEnrollmentClassesEnrolled = jctEnrollmentClassEnrolledDaoImpl.Read_AllWithEnrollmentId(enrollmentId, connection);
            foreach(DataRow foundJctEnrollmentClassEnrolled in foundJctEnrollmentClassesEnrolled)
            {
                if (passedInClassEnrolledIds.Contains((int)foundJctEnrollmentClassEnrolled["class_enrolled_id"]) == false)
                    jctEnrollmentClassEnrolledDaoImpl.Delete(enrollmentId, (int)foundJctEnrollmentClassEnrolled["class_enrolled_id"], connection);
            }

            return classesEnrolledToBeReturned;
        }
    }
}