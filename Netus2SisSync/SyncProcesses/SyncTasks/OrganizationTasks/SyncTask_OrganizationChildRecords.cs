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
    public class SyncTask_OrganizationChildRecords : SyncTask
    {
        public SyncTask_OrganizationChildRecords(string name, SyncJob job)
            : base(name, job)
        {
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            IConnectable netus2Connection = null;
            try
            {
                string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
                Enumeration sisEnumOrganization = Enum_Organization.values[row["enum_organization_id"].ToString().ToLower()];
                string sisIdentifier = row["identifier"].ToString() == "" ? null : row["identifier"].ToString();
                string sisBuildingCode = row["building_code"].ToString() == "" ? null : row["building_code"].ToString();

                Organization org = new Organization(sisName, sisEnumOrganization, sisIdentifier, sisBuildingCode);

                IOrganizationDao orgDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
                orgDaoImpl.SetTaskId(this.Id);

                netus2Connection = DbConnectionFactory.GetNetus2Connection();
                Organization foundOrg = orgDaoImpl.Read_WithSisBuildingCode(sisBuildingCode, netus2Connection);

                if (foundOrg == null)
                {
                    org = orgDaoImpl.Write(org, netus2Connection);
                }
                else
                {
                    org.Id = foundOrg.Id;

                    if ((org.Name != foundOrg.Name) ||
                        (org.OrganizationType != foundOrg.OrganizationType) ||
                        (org.Identifier != foundOrg.Identifier) ||
                        (org.SisBuildingCode != foundOrg.SisBuildingCode))
                    {
                        orgDaoImpl.Update(org, netus2Connection);
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
                netus2Connection.CloseConnection();
                latch.Signal();
            }
        }
    }
}
