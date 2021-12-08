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

namespace Netus2SisSync.SyncProcesses.SyncTasks.EnrollmentTasks
{
    public class SyncTask_Enrollment : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_Enrollment(string name, SyncJob job)
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
                Enumeration sisGrade = row["enum_grade_id"].ToString() == "" ? null : Enum_Grade.GetEnumFromSisCode(row["enum_grade_id"].ToString());
                bool sisIsPrimary = row["is_primary"].ToString() == "" ? false : (bool)row["is_primary"];

                DateTime sisStartDate = new DateTime();
                if (row["start_date"] != DBNull.Value)
                    sisStartDate = DateTime.Parse(row["start_date"].ToString());

                DateTime sisEndDate = new DateTime();
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
                if(academicSession == null)
                    throw new Exception("No records were found matching Academic Session: " +
                            "BuildingCode: " + sisAcademicSessionIdArray[0] +
                            " TrackCode: " + sisAcademicSessionIdArray[1] +
                            " TermCode: " + sisAcademicSessionIdArray[2] +
                            " SchoolYear: " + sisAcademicSessionIdArray[3]);

                Enrollment enrollmentSearch = new Enrollment(academicSession);
                enrollmentSearch.StartDate = sisStartDate;
                enrollmentSearch.EndDate = sisEndDate;
                IEnrollmentDao enrollmentDaoImpl = DaoImplFactory.GetEnrollmentDaoImpl();
                List<Enrollment> foundEnrollments = enrollmentDaoImpl.Read(enrollmentSearch, person.Id, _netus2Connection);

                if (foundEnrollments.Count == 0)
                {
                    enrollmentSearch.IsPrimary = sisIsPrimary;
                    enrollmentSearch.GradeLevel = sisGrade;
                    enrollmentDaoImpl.Write(enrollmentSearch, person.Id, _netus2Connection);
                }
                else if (foundEnrollments.Count == 1)
                {
                    enrollmentSearch.Id = foundEnrollments[0].Id;

                    if ((foundEnrollments[0].IsPrimary != sisIsPrimary) ||
                        (foundEnrollments[0].GradeLevel != sisGrade))
                    {
                        enrollmentSearch.IsPrimary = sisIsPrimary;
                        enrollmentSearch.GradeLevel = sisGrade;
                        enrollmentDaoImpl.Update(enrollmentSearch, person.Id, _netus2Connection);
                    }
                }
                else
                    throw new Exception(foundEnrollments.Count + " record(s) found matching Enrollment:\n" + enrollmentSearch.ToString() +
                        " \n With personId: " + person.Id);

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