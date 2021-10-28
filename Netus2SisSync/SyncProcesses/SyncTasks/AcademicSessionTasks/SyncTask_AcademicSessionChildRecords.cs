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
                string sisBuildingCode = row["building_code"].ToString() == "" ? null : row["building_code"].ToString();
                string sisTrackCode = row["track_code"].ToString() == "" ? null : row["track_code"].ToString();
                int sisSchoolYear = Int32.Parse(row["school_year"].ToString() == "" ? "-1" : row["school_year"].ToString());
                string sisTermCode = row["term_code"].ToString() == "" ? null : row["term_code"].ToString();

                Enumeration sisEnumSession = Enum_Session.values[row["enum_session_id"].ToString().ToLower()];
                string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();                
                DateTime sisStartDate = DateTime.Parse(row["start_date"].ToString());
                DateTime sisEndDate = DateTime.Parse(row["end_date"].ToString());

                IOrganizationDao orgDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
                orgDaoImpl.SetTaskId(this.Id);
                Organization org = orgDaoImpl.Read_UsingSisBuildingCode(sisBuildingCode, _netus2Connection);

                IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
                academicSessionDaoImpl.SetTaskId(this.Id);
                AcademicSession foundAcademicSession = academicSessionDaoImpl.
                    Read_UsingSisBuildingCode_TermCode_TrackCode_Schoolyear(sisBuildingCode, sisTermCode, sisTrackCode, sisSchoolYear, _netus2Connection);

                AcademicSession academicSession = new AcademicSession(sisEnumSession, org, sisTermCode);
                academicSession.TrackCode = sisTrackCode;
                academicSession.SchoolYear = sisSchoolYear;
                academicSession.Name = sisName;
                academicSession.StartDate = sisStartDate;
                academicSession.EndDate = sisEndDate;

                if (foundAcademicSession == null)
                    academicSession = academicSessionDaoImpl.Write(academicSession, _netus2Connection);
                else if (foundAcademicSession != null)
                {
                    academicSession.Id = foundAcademicSession.Id;

                    if ((academicSession.TermCode != foundAcademicSession.TermCode) ||
                        (academicSession.TrackCode != foundAcademicSession.TrackCode) ||
                        (academicSession.SchoolYear != foundAcademicSession.SchoolYear) ||
                        (academicSession.Name != foundAcademicSession.Name) ||
                        (academicSession.SessionType != foundAcademicSession.SessionType) ||
                        (academicSession.StartDate != foundAcademicSession.StartDate) ||
                        (academicSession.EndDate != foundAcademicSession.EndDate) ||
                        (academicSession.Organization.Id != foundAcademicSession.Organization.Id))
                    {
                        academicSessionDaoImpl.Update(academicSession, _netus2Connection);
                    }
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
