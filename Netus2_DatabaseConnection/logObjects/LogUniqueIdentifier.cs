using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_DatabaseConnection.logObjects
{
    public class LogUniqueIdentifier
    {
        public int log_unique_identifier_id { get; set; }
        public int unique_identifier_id { get; set; }
        public int person_id { get; set; }
        public string unique_identifier { get; set; }
        public DateTime created { get; set; }
        public string created_by { get; set; }
        public DateTime changed { get; set; }
        public string changed_by { get; set; }
        public DateTime log_date { get; set; }
        public string log_user { get; set; }

        public Enumeration IdentifierType;
        public Enumeration IsActive;
        public Enumeration LogAction;

        public void set_IsActive(int enumIsActiveId)
        {
            IsActive = Enum_True_False.GetEnumFromId(enumIsActiveId);
        }

        public void set_Identifier(int enumIdentifierId)
        {
            IdentifierType = Enum_Identifier.GetEnumFromId(enumIdentifierId);
        }

        public void set_LogAction(int enumLogActionId)
        {
            LogAction = Enum_Log_Action.GetEnumFromId(enumLogActionId);
        }
    }
}