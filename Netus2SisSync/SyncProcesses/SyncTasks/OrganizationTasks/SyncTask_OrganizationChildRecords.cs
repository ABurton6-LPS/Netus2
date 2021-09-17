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
        IConnectable _netus2Connection;

        public SyncTask_OrganizationChildRecords(string name, SyncJob job)
            : base(name, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this, _netus2Connection);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
                Enumeration sisEnumOrganization = Enum_Organization.values[row["enum_organization_id"].ToString().ToLower()];
                string sisIdentifier = row["identifier"].ToString() == "" ? null : row["identifier"].ToString();
                string sisBuildingCode = row["building_code"].ToString() == "" ? null : row["building_code"].ToString();

                Organization org = new Organization(sisName, sisEnumOrganization, sisIdentifier, sisBuildingCode);

                IOrganizationDao orgDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
                List<Organization> foundOrgs = orgDaoImpl.Read(org, _netus2Connection);

                if (foundOrgs.Count == 0)
                {
                    org = orgDaoImpl.Write(org, _netus2Connection);
                }
                else if (foundOrgs.Count == 1)
                {
                    org.Id = foundOrgs[0].Id;

                    if ((org.Name != foundOrgs[0].Name) ||
                        (org.OrganizationType != foundOrgs[0].OrganizationType) ||
                        (org.Identifier != foundOrgs[0].Identifier) ||
                        (org.SisBuildingCode != foundOrgs[0].SisBuildingCode))
                    {
                        orgDaoImpl.Update(org, _netus2Connection);
                    }
                }
                else
                {
                    throw new Exception(foundOrgs.Count + " record(s) found matching Organization:\n" + org.ToString());
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
