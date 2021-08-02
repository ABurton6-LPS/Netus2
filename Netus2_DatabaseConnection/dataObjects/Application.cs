using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Provider Provider { get; set; }

        public Application(string name, Provider provider)
        {
            Id = -1;
            Name = name;
            Provider = provider;
        }

        public override string ToString()
        {
            StringBuilder strApplication = new StringBuilder();
            strApplication.Append("Name: " + Name + "\n");
            strApplication.Append("Provider: " + Provider.Name + "\n");

            return strApplication.ToString();
        }
    }
}