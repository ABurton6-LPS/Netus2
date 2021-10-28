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

namespace Netus2SisSync.SyncProcesses.SyncTasks.AcademicSessionTasks
{
    public class SyncTask_AcademicSessionParentRecords : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_AcademicSessionParentRecords(string name, SyncJob job)
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

                if (foundAcademicSession != null)
                    academicSession.Id = foundAcademicSession.Id;
                else
                    throw new Exception("No records found matching Academic Session:\n" + academicSession.ToString());

                AcademicSession parentAcademicSession = null;
                string sisParentSessionCode = row["parent_session_code"].ToString() == "" ? null : row["parent_session_code"].ToString();

                if (sisParentSessionCode != null && sisParentSessionCode != "")
                {
                    string[] sisParentSessionCodeArray = sisParentSessionCode.Split('-');

                    string sisParentBuildingCode = sisParentSessionCodeArray[0];
                    string sisParentTermCode = sisParentSessionCodeArray[1];
                    int sisParentSchoolYear = Int32.Parse(sisParentSessionCodeArray[2]);

                    parentAcademicSession = academicSessionDaoImpl.Read_UsingSisBuildingCode_TermCode_TrackCode_Schoolyear(sisParentBuildingCode, sisParentTermCode, sisTrackCode, sisParentSchoolYear, _netus2Connection);
                    if (parentAcademicSession != null)
                    {
                        List<int> childIds = new List<int>();
                        foreach (AcademicSession child in parentAcademicSession.Children)
                            childIds.Add(child.Id);

                        if (childIds.Contains(academicSession.Id) == false)
                            academicSessionDaoImpl.Update(academicSession, parentAcademicSession.Id, _netus2Connection);
                    }
                    else
                        throw new Exception("No records were found matching parent Academic Session: " +
                            "BuildingCode: " + sisParentBuildingCode + 
                            " TermCode: " + sisParentTermCode + 
                            " TrackCode: " + sisTrackCode + 
                            " SchoolYear: " + sisParentSchoolYear);
                }
                else
                {
                    parentAcademicSession = academicSessionDaoImpl.Read_Parent(academicSession, _netus2Connection);

                    if (parentAcademicSession != null)
                        academicSessionDaoImpl.Update(academicSession, _netus2Connection);
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
