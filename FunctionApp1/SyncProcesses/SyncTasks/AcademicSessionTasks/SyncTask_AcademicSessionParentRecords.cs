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
    public class SyncTask_AcademicSessionParentRecords : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_AcademicSessionParentRecords(string name, DateTime timestamp, SyncJob job)
            : base(name, timestamp, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this, _netus2Connection);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisSessionCode = row["session_code"].ToString() == "" ? null : row["session_code"].ToString();
                int numberOfDashesInSessionCode = 2;
                int skipThisCharacterAndStartOnNextOne = 1;
                string schoolCode = sisSessionCode.Substring(0, (sisSessionCode.IndexOf('-')));
                int schoolYear = Int32.Parse(sisSessionCode.Substring(sisSessionCode.LastIndexOf('-') + skipThisCharacterAndStartOnNextOne));
                string termCode = sisSessionCode.Substring(sisSessionCode.IndexOf('-') + skipThisCharacterAndStartOnNextOne,
                    (sisSessionCode.Length - schoolCode.Length) - schoolYear.ToString().Length - numberOfDashesInSessionCode);
                string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
                Enumeration sisEnumSession = Enum_Session.values[row["enum_session_id"].ToString().ToLower()];
                DateTime sisStartDate = DateTime.Parse(row["start_date"].ToString());
                DateTime sisEndDate = DateTime.Parse(row["end_date"].ToString());
                string sisOrganizationId = row["organization_id"].ToString() == "" ? null : row["organization_id"].ToString();

                IOrganizationDao orgDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
                Organization org = orgDaoImpl.Read_WithBuildingCode(sisOrganizationId, _netus2Connection);

                AcademicSession academicSession = new AcademicSession(sisName, sisEnumSession, org, termCode);
                academicSession.SchoolYear = schoolYear;
                academicSession.StartDate = sisStartDate;
                academicSession.EndDate = sisEndDate;

                IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
                List<AcademicSession> foundAcademicSessions = academicSessionDaoImpl.Read(academicSession, _netus2Connection);

                if (foundAcademicSessions.Count == 1)
                {
                    academicSession.Id = foundAcademicSessions[0].Id;
                }
                else
                {
                    throw new Exception(foundAcademicSessions.Count + " record(s) found matching Academic Session:\n" + academicSession.ToString());
                }

                AcademicSession parentAcademicSession = null;
                string sisParentSessionCode = row["parent_session_code"].ToString() == "" ? null : row["parent_session_code"].ToString();

                if (sisParentSessionCode != null && sisParentSessionCode != "")
                {
                    string sisParentSchoolCode = sisParentSessionCode.Substring(0, (sisParentSessionCode.IndexOf('-')));
                    int sisParentSchoolYear = Int32.Parse(sisParentSessionCode.Substring(sisParentSessionCode.LastIndexOf('-') + skipThisCharacterAndStartOnNextOne));
                    string sisParentTermCode = sisParentSessionCode.Substring(sisParentSessionCode.IndexOf('-') + skipThisCharacterAndStartOnNextOne,
                        (sisParentSessionCode.Length - sisParentSchoolCode.Length) - sisParentSchoolYear.ToString().Length - numberOfDashesInSessionCode);

                    parentAcademicSession = academicSessionDaoImpl.Read_UsingSchoolCode_TermCode_Schoolyear(sisParentSchoolCode, sisParentTermCode, sisParentSchoolYear, _netus2Connection);
                    if (parentAcademicSession != null)
                    {
                        List<int> childIds = new List<int>();
                        foreach (AcademicSession child in parentAcademicSession.Children)
                        {
                            childIds.Add(child.Id);
                        }

                        if (childIds.Contains(academicSession.Id) == false)
                        {
                            academicSessionDaoImpl.Update(academicSession, parentAcademicSession.Id, _netus2Connection);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                SyncLogger.LogError(e, this, _netus2Connection);
            }
            finally
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"], _netus2Connection);
                _netus2Connection.CloseConnection();
                latch.Signal();
            }
        }
    }
}
