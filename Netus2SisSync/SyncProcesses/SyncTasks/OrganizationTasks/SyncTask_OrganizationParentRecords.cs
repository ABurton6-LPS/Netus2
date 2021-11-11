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

namespace Netus2SisSync.SyncProcesses.SyncTasks.OrganizationTasks
{
    public class SyncTask_OrganizationParentRecords : SyncTask
    {
        public SyncTask_OrganizationParentRecords(string name, SyncJob job)
            : base(name, job)
        {
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            IConnectable netus2Connection = DbConnectionFactory.GetNetus2Connection();
            try
            {
                string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
                Enumeration sisEnumOrganization = row["enum_organization_id"].ToString() == "" ? null : Enum_Organization.GetEnumFromSisCode(row["enum_organization_id"].ToString().ToLower());
                string sisIdentifier = row["identifier"].ToString() == "" ? null : row["identifier"].ToString();
                string sisBuildingCode = row["building_code"].ToString() == "" ? null : row["building_code"].ToString();

                Organization org = new Organization(sisName, sisEnumOrganization, sisIdentifier, sisBuildingCode);

                IOrganizationDao orgDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
                orgDaoImpl.SetTaskId(this.Id);
                List<Organization> foundOrgs = orgDaoImpl.Read(org, netus2Connection);

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
                    parentOrg = orgDaoImpl.Read_UsingSisBuildingCode(sisParentBuildingcode, netus2Connection);
                    if (parentOrg != null)
                    {
                        List<int> childIds = new List<int>();
                        foreach (Organization child in parentOrg.Children)
                        {
                            childIds.Add(child.Id);
                        }

                        if (childIds.Contains(org.Id) == false)
                        {
                            orgDaoImpl.Update(org, parentOrg.Id, netus2Connection);
                        }
                    }
                }
                else
                {
                    parentOrg = orgDaoImpl.Read_Parent(org, netus2Connection);

                    if (parentOrg != null)
                        orgDaoImpl.Update(org, netus2Connection);
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
                netus2Connection.CloseConnection();
                latch.Signal();
            }
        }
    }
}
