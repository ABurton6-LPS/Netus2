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
    public class SyncOrganization
    {
        public static DataTable ReadFromSis(IConnectable miStarConnection)
        {
            DataTable dtOrganization = UtilityTools.CreateDataTable("Organization");
            SqlDataReader reader = null;
            try
            {
                reader = miStarConnection.GetReader(SyncScripts.ReadSis_Organization_SQL);
                while (reader.Read())
                {
                    DataRow myDataRow = dtOrganization.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                if (value != DBNull.Value)
                                    myDataRow["name"] = (string)value;
                                else
                                    myDataRow["name"] = null;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    myDataRow["enum_organization_id"] = ((string)value).ToLower();
                                else
                                    myDataRow["enum_organization_id"] = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    myDataRow["identifier"] = (string)value;
                                else
                                    myDataRow["identifier"] = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    myDataRow["building_code"] = (string)value;
                                else
                                    myDataRow["building_code"] = null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    myDataRow["organization_parent_id"] = (string)value;
                                else
                                    myDataRow["organization_parent_id"] = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in SIS lookup for Organization: " + reader.GetName(i));
                        }
                    }
                    dtOrganization.Rows.Add(myDataRow);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return dtOrganization;
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
            string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
            Enumeration sisEnumOrganization = Enum_Organization.values[row["enum_organization_id"].ToString()];
            string sisIdentifier = row["identifier"].ToString() == "" ? null : row["identifier"].ToString();
            string sisBuildingCode = row["building_code"].ToString() == "" ? null : row["building_code"].ToString();

            Organization org = new Organization(sisName, sisEnumOrganization, sisIdentifier);
            org.BuildingCode = sisBuildingCode;

            IOrganizationDao orgDaoImpl = new OrganizationDaoImpl();
            List<Organization> foundOrgs = orgDaoImpl.Read(org, connection);

            if (foundOrgs.Count > 1)
            {
                throw new Exception("Multiple Organization records found matching:\n" + org.ToString());
            }
            else if (foundOrgs.Count == 0)
            {
                org = orgDaoImpl.Write(org, connection);
            }
            else if (foundOrgs.Count == 1)
            {
                org.Id = foundOrgs[0].Id;

                if ((org.Name != foundOrgs[0].Name) ||
                    (org.OrganizationType != foundOrgs[0].OrganizationType) ||
                    (org.Identifier != foundOrgs[0].Identifier) ||
                    (org.BuildingCode != foundOrgs[0].BuildingCode))
                {
                    orgDaoImpl.Update(org, connection);
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
            string sisName = row["name"].ToString() == "" ? null : row["name"].ToString();
            Enumeration sisEnumOrganization = Enum_Organization.values[row["enum_organization_id"].ToString()];
            string sisIdentifier = row["identifier"].ToString() == "" ? null : row["identifier"].ToString();
            string sisBuildingCode = row["building_code"].ToString() == "" ? null : row["building_code"].ToString();

            Organization org = new Organization(sisName, sisEnumOrganization, sisIdentifier);
            org.BuildingCode = sisBuildingCode;

            IOrganizationDao orgDaoImpl = new OrganizationDaoImpl();
            List<Organization> foundOrgs = orgDaoImpl.Read(org, connection);

            if (foundOrgs.Count > 1)
            {
                throw new Exception("Multiple Organization records found matching:\n" + org.ToString());
            }
            else if (foundOrgs.Count == 0)
            {
                throw new Exception("Organization " + org.Name + " was never writted to the database.");
            }
            else if (foundOrgs.Count == 1)
            {
                org.Id = foundOrgs[0].Id;
            }

            string sisParentBuildingcode = row["organization_parent_id"].ToString();
            Organization parentOrg = null;
            if (sisParentBuildingcode != null && sisParentBuildingcode != "")
            {
                parentOrg = orgDaoImpl.Read_WithBuildingCode(sisParentBuildingcode, connection);
                if (parentOrg != null)
                {
                    List<int> childIds = new List<int>();
                    foreach (Organization child in parentOrg.Children)
                    {
                        childIds.Add(child.Id);
                    }

                    if (childIds.Contains(org.Id) == false)
                    {
                        orgDaoImpl.Update(org, parentOrg.Id, connection);
                    }
                }
            }
        }
    }
}
