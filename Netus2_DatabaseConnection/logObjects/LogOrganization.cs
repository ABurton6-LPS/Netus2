using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogOrganization
    {
        public int log_organization_id { get; set; }
        public int organization_id { get; set; }
        public string name { get; set; }
        public string identifier { get; set; }
        public string sis_building_code { get; set; }
        public string hr_building_code { get; set; }
        public int organization_parent_id { get; set; }
        public DateTime created { get; set; }
        public string created_by { get; set; }
        public DateTime changed { get; set; }
        public string changed_by { get; set; }
        public DateTime log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration OrganizationType;
        public Enumeration LogAction;

        public void set_OrganizationType(int enumOrgId)
        {
            OrganizationType = Enum_Organization.GetEnumFromId(enumOrgId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}