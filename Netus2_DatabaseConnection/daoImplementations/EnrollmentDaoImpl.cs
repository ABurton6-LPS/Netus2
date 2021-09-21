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
    public class EnrollmentDaoImpl : IEnrollmentDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(Enrollment enrollment, IConnectable connection)
        {
            Delete(enrollment, -1, connection);
        }

        public void Delete(Enrollment enrollment, int personId, IConnectable connection)
        {
            Delete_JctEnrollmentAcademicSession(enrollment.AcademicSessions, enrollment.Id, connection);

            DataRow row = daoObjectMapper.MapEnrollment(enrollment, personId);

            StringBuilder sql = new StringBuilder("DELETE FROM enrollment WHERE 1=1 ");
            sql.Append("AND enrollment_id = " + row["enrollment_id"] + " ");
            if (row["person_id"] != DBNull.Value)
                sql.Append("AND person_id = " + row["person_id"] + " ");
            sql.Append("AND class_id " + (row["class_id"] != DBNull.Value ? "= " + row["class_id"] + " " : "IS NULL "));
            sql.Append("AND enum_grade_id " + (row["enum_grade_id"] != DBNull.Value ? "= " + row["enum_grade_id"] + " " : "IS NULL "));
            sql.Append("AND start_date " + (row["start_date"] != DBNull.Value ? "= '" + row["start_date"] + "' " : "IS NULL "));
            sql.Append("AND end_date " + (row["end_date"] != DBNull.Value ? "= '" + row["end_date"] + "' " : "IS NULL "));
            sql.Append("AND is_primary_id " + (row["is_primary_id"] != DBNull.Value ? " = " + row["is_primary_id"] + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_JctEnrollmentAcademicSession(List<AcademicSession> academicSessions, int enrollmentId, IConnectable connection)
        {
            IJctEnrollmentAcademicSessionDao jctEnrollmentAcademicSessionDaoImpl = DaoImplFactory.GetJctEnrollmentAcademicSessionDaoImpl();
            foreach (AcademicSession academicSession in academicSessions)
            {
                jctEnrollmentAcademicSessionDaoImpl.Delete(enrollmentId, academicSession.Id, connection);
            }
        }

        public List<Enrollment> Read_WithClassId(int classId, IConnectable connection)
        {
            string sql = "SELECT * FROM enrollment WHERE class_id = " + classId;

            return Read(sql, connection);
        }

        public List<Enrollment> Read(Enrollment enrollment, int personId, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            if (enrollment == null)
                sql.Append("SELECT * FROM enrollment WHERE person_id = " + personId);
            else
            {
                DataRow row = daoObjectMapper.MapEnrollment(enrollment, personId);
                sql.Append("SELECT * FROM enrollment WHERE 1=1 ");
                if (row["enrollment_id"] != DBNull.Value)
                    sql.Append("AND enrollment_id = " + row["enrollment_id"] + " ");
                else
                {
                    if (row["person_id"] != DBNull.Value)
                        sql.Append("AND person_id = " + row["person_id"] + " ");
                    if (row["class_id"] != DBNull.Value)
                        sql.Append("AND class_id = " + row["class_id"] + " ");
                    if (row["enum_grade_id"] != DBNull.Value)
                        sql.Append("AND enum_grade_id = " + row["enum_grade_id"] + " ");
                    if (row["is_primary_id"] != DBNull.Value)
                        sql.Append("AND is_primary_id = " + row["is_primary_id"] + " ");
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<Enrollment> Read(string sql, IConnectable connection)
        {
            DataTable dtEnrollment = new DataTableFactory().Dt_Netus2_Enrollment;
            dtEnrollment = connection.ReadIntoDataTable(sql, dtEnrollment);

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
            return classEnrolledDaoImpl.Read(classEnrolledId, connection);
        }

        private List<AcademicSession> Read_AcademicSessions(int enrollmentId, IConnectable connection)
        {
            List<AcademicSession> foundAcademicSessions = new List<AcademicSession>();

            IJctEnrollmentAcademicSessionDao jctEnrollmentAcademicSessionDaoImpl = DaoImplFactory.GetJctEnrollmentAcademicSessionDaoImpl();
            List<DataRow> foundJctEnrollmentAcademicSessionDaos =
                jctEnrollmentAcademicSessionDaoImpl.Read_WithEnrollmentId(enrollmentId, connection);

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
                StringBuilder sql = new StringBuilder("UPDATE enrollment SET ");
                sql.Append("person_id = " + (row["person_id"] != DBNull.Value ? row["person_id"] + ", " : "NULL, "));
                sql.Append("class_id = " + (row["class_id"] != DBNull.Value ? row["class_id"] + ", " : "NULL, "));
                sql.Append("enum_grade_id = " + (row["enum_grade_id"] != DBNull.Value ? row["enum_grade_id"] + ", " : "NULL, "));
                sql.Append("start_date = " + (row["start_date"] != DBNull.Value ? "'" + row["start_date"] + "', " : "NULL, "));
                sql.Append("end_date = " + (row["end_date"] != DBNull.Value ? "'" + row["end_date"] + "', " : "NULL, "));
                sql.Append("is_primary_id = " + (row["is_primary_id"] != DBNull.Value ? row["is_primary_id"] + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE enrollment_id = " + row["enrollment_id"]);

                connection.ExecuteNonQuery(sql.ToString());

                Update_JctEnrollmentAcademicSession(enrollment.AcademicSessions, (int)row["enrollment_id"], connection);
            }
            else
            {
                throw new Exception("The following Enrollment needs to be inserted into the database, before it can be updated.\n" + enrollment.ToString());
            }
        }

        public Enrollment Write(Enrollment enrollment, int personId, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapEnrollment(enrollment, personId);

            StringBuilder sql = new StringBuilder("INSERT INTO enrollment (");
            sql.Append("person_id, class_id, enum_grade_id, start_date, end_date, is_primary_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(row["person_id"] != DBNull.Value ? row["person_id"] + ", " : "NULL, ");
            sql.Append(row["class_id"] != DBNull.Value ? row["class_id"] + ", " : "NULL, ");
            sql.Append(row["enum_grade_id"] != DBNull.Value ? row["enum_grade_id"] + ", " : "NULL, ");
            sql.Append(row["start_date"] != DBNull.Value ? "'" + row["start_date"] + "', " : "NULL, ");
            sql.Append(row["end_date"] != DBNull.Value ? "'" + row["end_date"] + "', " : "NULL, ");
            sql.Append(row["is_primary_id"] != DBNull.Value ? row["is_primary_id"] + ", " : "NULL, ");
            sql.Append("GETDATE(), ");
            sql.Append("'Netus2')");

            row["enrollment_id"] = connection.InsertNewRecord(sql.ToString());

            ClassEnrolled foundClassEnrolled = null;

            if (row["class_id"] != DBNull.Value)
                foundClassEnrolled = Read_ClassEnrolled((int)row["class_id"], connection);

            List<AcademicSession> foundAcademicSessions = Update_JctEnrollmentAcademicSession(enrollment.AcademicSessions, (int)row["enrollment_id"], connection);
            return daoObjectMapper.MapEnrollment(row, foundClassEnrolled, foundAcademicSessions);
        }

        private List<AcademicSession> Update_JctEnrollmentAcademicSession(List<AcademicSession> academicSessions, int enrollmentId, IConnectable connection)
        {
            IJctEnrollmentAcademicSessionDao jctEnrollmentAcademicSessionDaoImpl = DaoImplFactory.GetJctEnrollmentAcademicSessionDaoImpl();
            List<DataRow> foundJctEnrollmentAcademicSessionDaos =
                jctEnrollmentAcademicSessionDaoImpl.Read_WithEnrollmentId(enrollmentId, connection);
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