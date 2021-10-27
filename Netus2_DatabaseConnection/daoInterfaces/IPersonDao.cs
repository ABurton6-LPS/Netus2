using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IPersonDao
    {
        void SetTaskId(int taskId);

        int? GetTaskId();

        public void Delete(Person person, IConnectable connection);

        public Person Read_UsingPersonId(int personId, IConnectable connection);

        public Person Read_UsingUniqueIdentifier(string identifier, IConnectable connection);

        public List<Person> Read(Person person, IConnectable connection);

        public void Update(Person person, IConnectable connection);

        public Person Write(Person person, IConnectable connection);
    }
}
