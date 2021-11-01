using Netus2_DatabaseConnection.enumerations;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class UniqueIdentifier
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public Enumeration IdentifierType { get; set; }

        public UniqueIdentifier(string identifier, Enumeration identifierType)
        {
            Id = -1;
            Identifier = identifier;
            IdentifierType = identifierType;
        }

        public override string ToString()
        {
            StringBuilder strUniqueId = new StringBuilder();
            strUniqueId.Append("Identifier: " + Identifier + "\n");
            strUniqueId.Append("Identifier Type: " + IdentifierType.Netus2Code + "\n");

            return base.ToString();
        }
    }
}