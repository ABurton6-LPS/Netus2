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

namespace Netus2SisSync.SyncProcesses.SyncTasks.OrganizationTasks
{
    public class SyncTask_OrganizationParentRecords : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_OrganizationParentRecords(string name, DateTime timestamp, SyncJob job)
            : base(name, timestamp, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this, _netus2Connection);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
                Enumeration sisEnumOrganization = Enum_Organization.values[row["enum_organization_id"].ToString()];
                string sisIdentifier = row["identifier"].ToString() == "" ? null : row["identifier"].ToString();
                string sisBuildingCode = row["building_code"].ToString() == "" ? null : row["building_code"].ToString();

                Organization org = new Organization(sisName, sisEnumOrganization, sisIdentifier, sisBuildingCode);

                IOrganizationDao orgDaoImpl = new OrganizationDaoImpl();
                List<Organization> foundOrgs = orgDaoImpl.Read(org, _netus2Connection);

                if (foundOrgs.Count == 1)
                {
                    org.Id = foundOrgs[0].Id;
                }
                else
                {
                    throw new Exception(foundOrgs.Count + " record(s) found matching Organization:\n" + org.ToString());
                }

                string sisParentBuildingcode = row["organization_parent_id"].ToString();
                Organization parentOrg = null;
                if (sisParentBuildingcode != null && sisParentBuildingcode != "")
                {
                    parentOrg = orgDaoImpl.Read_WithBuildingCode(sisParentBuildingcode, _netus2Connection);
                    if (parentOrg != null)
                    {
                        List<int> childIds = new List<int>();
                        foreach (Organization child in parentOrg.Children)
                        {
                            childIds.Add(child.Id);
                        }

                        if (childIds.Contains(org.Id) == false)
                        {
                            orgDaoImpl.Update(org, parentOrg.Id, _netus2Connection);
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
