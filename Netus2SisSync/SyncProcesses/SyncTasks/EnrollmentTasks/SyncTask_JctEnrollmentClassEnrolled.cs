using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Netus2SisSync.SyncProcesses.SyncTasks.MarkTasks
{
    public class SyncTask_JctEnrollmentClassEnrolled : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_JctEnrollmentClassEnrolled(string name, SyncJob job)
            : base(name, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisPersonId = row["person_id"].ToString() == "" ? null : row["person_id"].ToString();
                string sisAcademicSessionId = row["academic_session_id"].ToString() == "" ? null : row["academic_session_id"].ToString();
                string sisClassEnrolledId = row["class_enrolled_id"].ToString() == "" ? null : row["class_enrolled_id"].ToString();

                DateTime? sisStartDate = null;
                if (row["start_date"] != DBNull.Value)
                    sisStartDate = DateTime.Parse(row["start_date"].ToString());

                DateTime? sisEndDate = null;
                if (row["end_date"] != DBNull.Value)
                    sisEndDate = DateTime.Parse(row["end_date"].ToString());

                IPersonDao personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
                Person person = personDaoImpl.Read_UsingUniqueIdentifier(sisPersonId, _netus2Connection);
                if (person == null)
                    throw new Exception("No person found with Unique Identifier: " + sisPersonId);

                string[] sisAcademicSessionIdArray = sisAcademicSessionId.Split('-');
                IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
                AcademicSession academicSession = academicSessionDaoImpl.Read_UsingSisBuildingCode_TrackCode_TermCode_Schoolyear(
                    sisAcademicSessionIdArray[0],
                    sisAcademicSessionIdArray[1],
                    sisAcademicSessionIdArray[2],
                    Int32.Parse(sisAcademicSessionIdArray[3]),
                    _netus2Connection);
                if (academicSession == null)
                    throw new Exception("No records were found matching Academic Session: " +
                            "BuildingCode: " + sisAcademicSessionIdArray[0] +
                            " TrackCode: " + sisAcademicSessionIdArray[1] +
                            " TermCode: " + sisAcademicSessionIdArray[2] +
                            " SchoolYear: " + sisAcademicSessionIdArray[3]);

                IEnrollmentDao enrollmentDaoImpl = DaoImplFactory.GetEnrollmentDaoImpl();
                Enrollment enrollment = enrollmentDaoImpl.Read_UsingAcademicSessionIdAndPersonId(academicSession.Id, person.Id, _netus2Connection);
                if (enrollment == null)
                    throw new Exception("No records found matching Enrollment with academicSessionId: " + academicSession.Id + " and personId: " + person.Id);

                IClassEnrolledDao classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
                ClassEnrolled classEnrolled = classEnrolledDaoImpl.Read_UsingClassEnrolledCode(sisClassEnrolledId, _netus2Connection);
                if (classEnrolled == null)
                    throw new Exception("No records were found matching Class Enrolled Code: " + sisClassEnrolledId);

                IJctEnrollmentClassEnrolledDao jctEnrollmentClassEnrolledDaoImpl = DaoImplFactory.GetJctEnrollmentClassEnrolledDaoImpl();
                DataRow jctEnrollmentClassEnrolledDao = jctEnrollmentClassEnrolledDaoImpl.Read(enrollment.Id, classEnrolled.Id, _netus2Connection);

                if(jctEnrollmentClassEnrolledDao == null)
                {
                    jctEnrollmentClassEnrolledDaoImpl.Write(enrollment.Id, classEnrolled.Id, sisStartDate, sisEndDate, _netus2Connection);
                    jctEnrollmentClassEnrolledDaoImpl.Write_ToTempTable(enrollment.Id, classEnrolled.Id, _netus2Connection);
                }
                else
                {
                    bool needsToBeUpdated = false;

                    if (jctEnrollmentClassEnrolledDao["start_date"] == DBNull.Value && sisStartDate != null)
                        needsToBeUpdated = true;
                    else if (jctEnrollmentClassEnrolledDao["start_date"] != DBNull.Value && sisStartDate == null)
                        needsToBeUpdated = true;
                    else if ((DateTime)jctEnrollmentClassEnrolledDao["start_date"] != sisStartDate)
                        needsToBeUpdated = true;

                    if (jctEnrollmentClassEnrolledDao["end_date"] == DBNull.Value && sisEndDate != null)
                        needsToBeUpdated = true;
                    else if (jctEnrollmentClassEnrolledDao["end_date"] != DBNull.Value && sisEndDate == null)
                        needsToBeUpdated = true;
                    else if ((DateTime)jctEnrollmentClassEnrolledDao["end_date"] != sisEndDate)
                        needsToBeUpdated = true;

                    if (needsToBeUpdated)
                        jctEnrollmentClassEnrolledDaoImpl.Update(enrollment.Id, classEnrolled.Id, sisStartDate, sisEndDate, _netus2Connection);
                }

                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"]);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                SyncLogger.LogError(e, this, row);
            }
            finally
            {
                _netus2Connection.CloseConnection();
                latch.Signal();
            }
        }
    }
}