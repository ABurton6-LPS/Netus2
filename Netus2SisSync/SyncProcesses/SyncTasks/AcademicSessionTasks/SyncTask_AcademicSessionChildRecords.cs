using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Netus2SisSync.SyncProcesses.SyncTasks.AcademicSessionTasks
{
    public class SyncTask_AcademicSessionChildRecords : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_AcademicSessionChildRecords(string name, SyncJob job)
            : base(name, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisSchoolCode = row["school_code"].ToString() == "" ? null : row["school_code"].ToString();
                string sisTermCode = row["term_code"].ToString() == "" ? null : row["term_code"].ToString();
                int sisSchoolYear = Int32.Parse(row["school_year"].ToString() == "" ? "-1" : row["school_year"].ToString());
                string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
                Enumeration sisEnumSession = Enum_Session.values[row["enum_session_id"].ToString().ToLower()];
                DateTime sisStartDate = DateTime.Parse(row["start_date"].ToString());
                DateTime sisEndDate = DateTime.Parse(row["end_date"].ToString());

                IOrganizationDao orgDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
                Organization org = orgDaoImpl.Read_WithSisBuildingCode(sisSchoolCode, _netus2Connection);

                AcademicSession academicSession = new AcademicSession(sisName, sisEnumSession, org, sisTermCode);
                academicSession.SchoolYear = sisSchoolYear;
                academicSession.StartDate = sisStartDate;
                academicSession.EndDate = sisEndDate;

                IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
                List<AcademicSession> foundAcademicSessions = academicSessionDaoImpl.Read(academicSession, _netus2Connection);

                if (foundAcademicSessions.Count == 0)
                {
                    academicSession = academicSessionDaoImpl.Write(academicSession, _netus2Connection);
                }
                else if (foundAcademicSessions.Count == 1)
                {
                    academicSession.Id = foundAcademicSessions[0].Id;

                    if ((academicSession.TermCode != foundAcademicSessions[0].TermCode) ||
                        (academicSession.SchoolYear != foundAcademicSessions[0].SchoolYear) ||
                        (academicSession.Name != foundAcademicSessions[0].Name) ||
                        (academicSession.StartDate != foundAcademicSessions[0].StartDate) ||
                        (academicSession.EndDate != foundAcademicSessions[0].EndDate) ||
                        (academicSession.SessionType != foundAcademicSessions[0].SessionType) ||
                        (academicSession.Organization.Id != foundAcademicSessions[0].Organization.Id))
                    {
                        academicSessionDaoImpl.Update(academicSession, _netus2Connection);
                    }
                }
                else
                {
                    throw new Exception(foundAcademicSessions.Count + " record(s) found matching Academic Session:\n" + academicSession.ToString());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                SyncLogger.LogError(e, this, row);
            }
            finally
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"]);
                _netus2Connection.CloseConnection();
                latch.Signal();
            }
        }
    }
}
