﻿using Microsoft.Extensions.Logging;
using Netus2;
using Netus2.daoImplementations;
using Netus2.daoInterfaces;
using Netus2.dbAccess;
using Netus2.enumerations;
using Netus2_DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Netus2SisSync
{
    public class SyncAcademicSession
    {
        public static DataTable ReadFromSis(IConnectable miStarConnection)
        {
            DataTable dtAcademicSession = UtilityTools.CreateDataTable("AcademicSession");
            SqlDataReader reader = null;
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
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return dtAcademicSession;
        }

        public static void SyncForChildRecords(DataRow row, CountDownLatch latch, ILogger log)
        {
            IConnectable connection = null;
            try
            {
                connection = new Netus2DatabaseConnection();
                connection.OpenConnection();

                SyncForChildRecords(row, latch, connection, log);
            }
            catch (Exception e)
            {
                log.LogError(e.Message, e);
                Debug.WriteLine(e.Message);
            }
            finally
            {
                connection.CloseConnection();
                latch.Signal();
            }
        }
        public static void SyncForChildRecords(DataRow row, CountDownLatch latch, IConnectable connection, ILogger log)
        {
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

            if (foundAcademicSessions.Count > 1)
            {
                throw new Exception("Multiple Academic Session records found matching:\n" + academicSession.ToString());
            }
            else if (foundAcademicSessions.Count == 0)
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
        }

        public static void SyncForParentRecords(DataRow row, CountDownLatch latch, ILogger log)
        {
            IConnectable connection = null;
            try
            {
                connection = new Netus2DatabaseConnection();
                connection.OpenConnection();

                SyncForParentRecords(row, latch, connection, log);
            }
            catch (Exception e)
            {
                log.LogError(e.Message, e);
                Debug.WriteLine(e.Message);
            }
            finally
            {
                connection.CloseConnection();
                latch.Signal();
            }
        }
        public static void SyncForParentRecords(DataRow row, CountDownLatch latch, IConnectable connection, ILogger log)
        {
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
                throw new Exception(foundAcademicSessions.Count + " record(s) found matching Academic Session " + academicSession.ToString());
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
    }
}
