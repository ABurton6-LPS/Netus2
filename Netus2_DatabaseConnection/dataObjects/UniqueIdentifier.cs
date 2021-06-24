using Netus2.enumerations;
using System.Text;

namespace Netus2
{
    public class UniqueIdentifier
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public Enumeration IdentifierType { get; set; }
        public Enumeration IsActive { get; set; }

        public UniqueIdentifier(string identifier, Enumeration identifierType, Enumeration isActive)
        {
            Id = -1;
            Identifier = identifier;
            IdentifierType = identifierType;
            IsActive = isActive;
        }

        public override string ToString()
        {
            StringBuilder strUniqueId = new StringBuilder();
            strUniqueId.Append("Identifier: " + Identifier + "\n");
            strUniqueId.Append("Identifier Type: " + IdentifierType.Netus2Code + "\n");
            strUniqueId.Append("Is Active: " + IsActive.Netus2Code + "\n");

            return base.ToString();
        }
    }
}