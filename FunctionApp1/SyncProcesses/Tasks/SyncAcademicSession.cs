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
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace Netus2SisSync.SyncProcesses.Tasks
{
    public class SyncAcademicSession
    {
        public static void Start(SyncJob job, IConnectable miStarConnection, IConnectable netus2Connection)
        {
            DataTable dtAcademicSession = SyncAcademicSession.ReadFromSis(job, miStarConnection, netus2Connection);
            CountDownLatch dtAcademicSessionChildLatch = new CountDownLatch(dtAcademicSession.Rows.Count);
            foreach (DataRow row in dtAcademicSession.Rows)
            {
                new Thread(syncthread => SyncAcademicSession.SyncForChildRecords(job, row, dtAcademicSessionChildLatch)).Start();
                Thread.Sleep(100);
            }
            dtAcademicSessionChildLatch.Wait();

            CountDownLatch dtAcademicSessionParentLatch = new CountDownLatch(dtAcademicSession.Rows.Count);
            foreach (DataRow row in dtAcademicSession.Rows)
            {
                new Thread(syncthread => SyncAcademicSession.SyncForParentRecords(job, row, dtAcademicSessionParentLatch)).Start();
                Thread.Sleep(100);
            }
            dtAcademicSessionParentLatch.Wait();
        }

        public static DataTable ReadFromSis(SyncJob job, IConnectable miStarConnection, IConnectable netus2Connection)
        {
            DataTable dtAcademicSession = DataTableFactory.CreateDataTable("AcademicSession");
            IDataReader reader = null;
            try
            {
                reader = miStarConnection.GetReader(SyncScripts.ReadSiS_AcademicSession_SQL);
                while (reader.Read())
                {
                    DataRow myDataRow = dtAcademicSession.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                myDataRow["session_code"] = (string)value;
                                break;
                            case 1:
                                myDataRow["name"] = (string)value;
                                break;
                            case 2:
                                myDataRow["enum_session_id"] = (string)value;
                                break;
                            case 3:
                                myDataRow["start_date"] = (DateTime)value;
                                break;
                            case 4:
                                myDataRow["end_date"] = (DateTime)value;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    myDataRow["parent_session_code"] = (string)value;
                                break;
                            case 6:
                                myDataRow["organization_id"] = (string)value;
                                break;
                            default:
                                throw new Exception("Unexpected column found in SIS lookup for Academic Session: " + reader.GetName(i));
                        }
                    }

                    if ((String)myDataRow["organization_id"] != "LVMS")
                        dtAcademicSession.Rows.Add(myDataRow);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                SyncLogger.LogError(e, job, netus2Connection);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return dtAcademicSession;
        }

        public static void SyncForChildRecords(SyncJob job, DataRow row, CountDownLatch latch)
        {
            SyncTask task = null;

            IConnectable connection = null;
            try
            {
                connection = DbConnectionFactory.GetConnection("Netus2");
                connection.OpenConnection();

                task = SyncLogger.LogNewTask("AcademicSession_SyncForChildRecords", job, connection);

                string sisSessionCode = row["session_code"].ToString() == "" ? null : row["session_code"].ToString();
                int numberOfDashesInSessionCode = 2;
                int skipThisCharacterAndStartOnNextOne = 1;
                string schoolCode = sisSessionCode.Substring(0, (sisSessionCode.IndexOf('-')));
                int schoolYear = Int32.Parse(sisSessionCode.Substring(sisSessionCode.LastIndexOf('-') + skipThisCharacterAndStartOnNextOne));
                string termCode = sisSessionCode.Substring(sisSessionCode.IndexOf('-') + skipThisCharacterAndStartOnNextOne,
                    (sisSessionCode.Length - schoolCode.Length) - schoolYear.ToString().Length - numberOfDashesInSessionCode);
                string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
                Enumeration sisEnumSession = Enum_Session.values[row["enum_session_id"].ToString()];
                DateTime sisStartDate = DateTime.Parse(row["start_date"].ToString());
                DateTime sisEndDate = DateTime.Parse(row["end_date"].ToString());
                string sisOrganizationId = row["organization_id"].ToString() == "" ? null : row["organization_id"].ToString();

                IOrganizationDao orgDaoImpl = new OrganizationDaoImpl();
                Organization org = orgDaoImpl.Read_WithBuildingCode(sisOrganizationId, connection);

                AcademicSession academicSession = new AcademicSession(sisName, sisEnumSession, org, termCode);
                academicSession.SchoolYear = schoolYear;
                academicSession.StartDate = sisStartDate;
                academicSession.EndDate = sisEndDate;

                IAcademicSessionDao academicSessionDaoImpl = new AcademicSessionDaoImpl();
                List<AcademicSession> foundAcademicSessions = academicSessionDaoImpl.Read(academicSession, connection);

                if (foundAcademicSessions.Count == 0)
                {
                    academicSession = academicSessionDaoImpl.Write(academicSession, connection);
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
                        academicSessionDaoImpl.Update(academicSession, connection);
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
                SyncLogger.LogError(e, task, connection);
            }
            finally
            {
                SyncLogger.LogStatus(task, Enum_Sync_Status.values["end"], connection);
                connection.CloseConnection();
                latch.Signal();
            }
        }

        public static void SyncForParentRecords(SyncJob job, DataRow row, CountDownLatch latch)
        {
            SyncTask task = null;

            IConnectable connection = null;
            try
            {
                connection = DbConnectionFactory.GetConnection("Netus2");
                connection.OpenConnection();

                task = SyncLogger.LogNewTask("AcademicSession_SyncForParentRecords", job, connection);

                string sisSessionCode = row["session_code"].ToString() == "" ? null : row["session_code"].ToString();
                int numberOfDashesInSessionCode = 2;
                int skipThisCharacterAndStartOnNextOne = 1;
                string schoolCode = sisSessionCode.Substring(0, (sisSessionCode.IndexOf('-')));
                int schoolYear = Int32.Parse(sisSessionCode.Substring(sisSessionCode.LastIndexOf('-') + skipThisCharacterAndStartOnNextOne));
                string termCode = sisSessionCode.Substring(sisSessionCode.IndexOf('-') + skipThisCharacterAndStartOnNextOne,
                    (sisSessionCode.Length - schoolCode.Length) - schoolYear.ToString().Length - numberOfDashesInSessionCode);
                string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
                Enumeration sisEnumSession = Enum_Session.values[row["enum_session_id"].ToString()];
                DateTime sisStartDate = DateTime.Parse(row["start_date"].ToString());
                DateTime sisEndDate = DateTime.Parse(row["end_date"].ToString());
                string sisOrganizationId = row["organization_id"].ToString() == "" ? null : row["organization_id"].ToString();

                IOrganizationDao orgDaoImpl = new OrganizationDaoImpl();
                Organization org = orgDaoImpl.Read_WithBuildingCode(sisOrganizationId, connection);

                AcademicSession academicSession = new AcademicSession(sisName, sisEnumSession, org, termCode);
                academicSession.SchoolYear = schoolYear;
                academicSession.StartDate = sisStartDate;
                academicSession.EndDate = sisEndDate;

                IAcademicSessionDao academicSessionDaoImpl = new AcademicSessionDaoImpl();
                List<AcademicSession> foundAcademicSessions = academicSessionDaoImpl.Read(academicSession, connection);

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

                    parentAcademicSession = academicSessionDaoImpl.Read_UsingSchoolCode_TermCode_Schoolyear(sisParentSchoolCode, sisParentTermCode, sisParentSchoolYear, connection);
                    if (parentAcademicSession != null)
                    {
                        List<int> childIds = new List<int>();
                        foreach (AcademicSession child in parentAcademicSession.Children)
                        {
                            childIds.Add(child.Id);
                        }

                        if (childIds.Contains(academicSession.Id) == false)
                        {
                            academicSessionDaoImpl.Update(academicSession, parentAcademicSession.Id, connection);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                SyncLogger.LogError(e, task, connection);
            }
            finally
            {
                SyncLogger.LogStatus(task, Enum_Sync_Status.values["end"], connection);
                connection.CloseConnection();
                latch.Signal();
            }
        }
    }
}
