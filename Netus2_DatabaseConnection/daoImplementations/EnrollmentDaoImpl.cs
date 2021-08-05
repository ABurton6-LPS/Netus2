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

            EnrollmentDao enrollmentDao = daoObjectMapper.MapEnrollment(enrollment, personId);

            StringBuilder sql = new StringBuilder("DELETE FROM enrollment WHERE 1=1 ");
            sql.Append("AND enrollment_id = " + enrollmentDao.enrollment_id + " ");
            if (enrollmentDao.person_id != null)
                sql.Append("AND person_id = " + enrollmentDao.person_id + " ");
            sql.Append("AND class_id " + (enrollmentDao.class_id != null ? "= " + enrollmentDao.class_id + " " : "IS NULL "));
            sql.Append("AND enum_grade_id " + (enrollmentDao.enum_grade_id != null ? "= " + enrollmentDao.enum_grade_id + " " : "IS NULL "));
            sql.Append("AND start_date " + (enrollmentDao.start_date != null ? "= '" + enrollmentDao.start_date + "' " : "IS NULL "));
            sql.Append("AND end_date " + (enrollmentDao.end_date != null ? "= '" + enrollmentDao.end_date + "' " : "IS NULL "));
            sql.Append("AND is_primary_id " + (enrollmentDao.is_primary_id != null ? " = " + enrollmentDao.is_primary_id + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_JctEnrollmentAcademicSession(List<AcademicSession> academicSessions, int enrollmentId, IConnectable connection)
        {
            IJctEnrollmentAcademicSessionDao jctEnrollmentAcademicSessionDaoImpl = new JctEnrollmentAcademicSessionDaoImpl();
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
                EnrollmentDao enrollmentDao = daoObjectMapper.MapEnrollment(enrollment, personId);
                sql.Append("SELECT * FROM enrollment WHERE 1=1 ");
                if (enrollmentDao.enrollment_id != null)
                    sql.Append("AND enrollment_id = " + enrollmentDao.enrollment_id + " ");
                else
                {
                    if (enrollmentDao.person_id != null)
                        sql.Append("AND person_id = " + enrollmentDao.person_id + " ");
                    if (enrollmentDao.class_id != null)
                        sql.Append("AND class_id = " + enrollmentDao.class_id + " ");
                    if (enrollmentDao.enum_grade_id != null)
                        sql.Append("AND enum_grade_id = " + enrollmentDao.enum_grade_id + " ");
                    if (enrollmentDao.is_primary_id != null)
                        sql.Append("AND is_primary_id = " + enrollmentDao.is_primary_id + " ");
                }
            }

            return Read(sql.ToString(), connection);
        }

        private List<Enrollment> Read(string sql, IConnectable connection)
        {
            List<EnrollmentDao> foundEnrollmentDaos = new List<EnrollmentDao>();
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    EnrollmentDao foundEnrollmentDao = new EnrollmentDao();

                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "enrollment_id":
                                if (value != DBNull.Value)
                                    foundEnrollmentDao.enrollment_id = (int)value;
                                else
                                    foundEnrollmentDao.enrollment_id = null;
                                break;
                            case "person_id":
                                if (value != DBNull.Value)
                                    foundEnrollmentDao.person_id = (int)value;
                                else
                                    foundEnrollmentDao.person_id = null;
                                break;
                            case "class_id":
                                if (value != DBNull.Value)
                                    foundEnrollmentDao.class_id = (int)value;
                                else
                                    foundEnrollmentDao.class_id = null;
                                break;
                            case "enum_grade_id":
                                if (value != DBNull.Value)
                                    foundEnrollmentDao.enum_grade_id = (int)value;
                                else
                                    foundEnrollmentDao.enum_grade_id = null;
                                break;
                            case "start_date":
                                if (value != DBNull.Value)
                                    foundEnrollmentDao.start_date = (DateTime)value;
                                else
                                    foundEnrollmentDao.start_date = null;
                                break;
                            case "end_date":
                                if (value != DBNull.Value)
                                    foundEnrollmentDao.end_date = (DateTime)value;
                                else
                                    foundEnrollmentDao.end_date = null;
                                break;
                            case "is_primary_id":
                                if (value != DBNull.Value)
                                    foundEnrollmentDao.is_primary_id = (int)value;
                                else
                                    foundEnrollmentDao.is_primary_id = null;
                                break;
                            case "created":
                                if (value != DBNull.Value)
                                    foundEnrollmentDao.created = (DateTime)value;
                                else
                                    foundEnrollmentDao.created = null;
                                break;
                            case "created_by":
                                foundEnrollmentDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case "changed":
                                if (value != DBNull.Value)
                                    foundEnrollmentDao.changed = (DateTime)value;
                                else
                                    foundEnrollmentDao.changed = null;
                                break;
                            case "changed_by":
                                foundEnrollmentDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in enrollment table: " + columnName);
                        }
                    }
                    foundEnrollmentDaos.Add(foundEnrollmentDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<Enrollment> results = new List<Enrollment>();
            foreach (EnrollmentDao foundEnrollmentDao in foundEnrollmentDaos)
            {
                ClassEnrolled foundClassEnrolled = null;
                if (foundEnrollmentDao.class_id != null)
                    foundClassEnrolled = Read_ClassEnrolled((int)foundEnrollmentDao.class_id, connection);

                List<AcademicSession> academicSessions = Read_AcademicSessions((int)foundEnrollmentDao.enrollment_id, connection);
                results.Add(daoObjectMapper.MapEnrollment(foundEnrollmentDao, foundClassEnrolled, academicSessions));
            }

            return results;
        }

        private ClassEnrolled Read_ClassEnrolled(int classEnrolledId, IConnectable connection)
        {
            IClassEnrolledDao classEnrolledDaoImpl = new ClassEnrolledDaoImpl();
            return classEnrolledDaoImpl.Read(classEnrolledId, connection);
        }

        private List<AcademicSession> Read_AcademicSessions(int enrollmentId, IConnectable connection)
        {
            List<AcademicSession> foundAcademicSessions = new List<AcademicSession>();

            IJctEnrollmentAcademicSessionDao jctEnrollmentAcademicSessionDaoImpl = new JctEnrollmentAcademicSessionDaoImpl();
            List<JctEnrollmentAcademicSessionDao> foundJctEnrollmentAcademicSessionDaos =
                jctEnrollmentAcademicSessionDaoImpl.Read_WithEnrollmentId(enrollmentId, connection);

            IAcademicSessionDao academicSessionDaoImpl = new AcademicSessionDaoImpl();

            foreach (JctEnrollmentAcademicSessionDao foundJctEnrollmentAcademicSessionDao in foundJctEnrollmentAcademicSessionDaos)
            {
                foundAcademicSessions.Add(
                    academicSessionDaoImpl.Read_UsingAcademicSessionId((int)foundJctEnrollmentAcademicSessionDao.academic_session_id, connection));
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
            EnrollmentDao enrollmentDao = daoObjectMapper.MapEnrollment(enrollment, personId);

            if (enrollmentDao.enrollment_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE enrollment SET ");
                sql.Append("person_id = " + (enrollmentDao.person_id != null ? enrollmentDao.person_id + ", " : "NULL, "));
                sql.Append("class_id = " + (enrollmentDao.class_id != null ? enrollmentDao.class_id + ", " : "NULL, "));
                sql.Append("enum_grade_id = " + (enrollmentDao.enum_grade_id != null ? enrollmentDao.enum_grade_id + ", " : "NULL, "));
                sql.Append("start_date = " + (enrollmentDao.start_date != null ? "'" + enrollmentDao.start_date + "', " : "NULL, "));
                sql.Append("end_date = " + (enrollmentDao.end_date != null ? "'" + enrollmentDao.end_date + "', " : "NULL, "));
                sql.Append("is_primary_id = " + (enrollmentDao.is_primary_id != null ? +enrollmentDao.is_primary_id + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE enrollment_id = " + enrollmentDao.enrollment_id);

                connection.ExecuteNonQuery(sql.ToString());

                Update_JctEnrollmentAcademicSession(enrollment.AcademicSessions, (int)enrollmentDao.enrollment_id, connection);
            }
            else
            {
                throw new Exception("The following Enrollment needs to be inserted into the database, before it can be updated.\n" + enrollment.ToString());
            }
        }

        public Enrollment Write(Enrollment enrollment, int personId, IConnectable connection)
        {
            EnrollmentDao enrollmentDao = daoObjectMapper.MapEnrollment(enrollment, personId);

            StringBuilder sql = new StringBuilder("INSERT INTO enrollment (");
            sql.Append("person_id, class_id, enum_grade_id, start_date, end_date, is_primary_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(enrollmentDao.person_id != null ? enrollmentDao.person_id + ", " : "NULL, ");
            sql.Append(enrollmentDao.class_id != null ? enrollmentDao.class_id + ", " : "NULL, ");
            sql.Append(enrollmentDao.enum_grade_id != null ? enrollmentDao.enum_grade_id + ", " : "NULL, ");
            sql.Append(enrollmentDao.start_date != null ? "'" + enrollmentDao.start_date + "', " : "NULL, ");
            sql.Append(enrollmentDao.end_date != null ? "'" + enrollmentDao.end_date + "', " : "NULL, ");
            sql.Append(enrollmentDao.is_primary_id != null ? enrollmentDao.is_primary_id + ", " : "NULL, ");
            sql.Append("GETDATE(), ");
            sql.Append("'Netus2')");

            enrollmentDao.enrollment_id = connection.InsertNewRecord(sql.ToString());

            ClassEnrolled foundClassEnrolled = null;

            if (enrollmentDao.class_id != null)
                foundClassEnrolled = Read_ClassEnrolled((int)enrollmentDao.class_id, connection);

            List<AcademicSession> foundAcademicSessions = Update_JctEnrollmentAcademicSession(enrollment.AcademicSessions, (int)enrollmentDao.enrollment_id, connection);
            return daoObjectMapper.MapEnrollment(enrollmentDao, foundClassEnrolled, foundAcademicSessions);
        }

        private List<AcademicSession> Update_JctEnrollmentAcademicSession(List<AcademicSession> academicSessions, int enrollmentId, IConnectable connection)
        {
            IJctEnrollmentAcademicSessionDao jctEnrollmentAcademicSessionDaoImpl = new JctEnrollmentAcademicSessionDaoImpl();
            List<JctEnrollmentAcademicSessionDao> foundJctEnrollmentAcademicSessionDaos =
                jctEnrollmentAcademicSessionDaoImpl.Read_WithEnrollmentId(enrollmentId, connection);
            List<int> academicSessionIds = new List<int>();
            List<int> foundAcademicSessionsIds = new List<int>();

            foreach (AcademicSession academicSession in academicSessions)
            {
                academicSessionIds.Add(academicSession.Id);
            }

            foreach (JctEnrollmentAcademicSessionDao foundJctEnrollmentAcademicSessionDao in foundJctEnrollmentAcademicSessionDaos)
            {
                foundAcademicSessionsIds.Add((int)foundJctEnrollmentAcademicSessionDao.academic_session_id);
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