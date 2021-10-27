using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IUniqueIdentifierDao
    {
        void SetTaskId(int taskId);

        int? GetTaskId();

        public void Delete(UniqueIdentifier uniqueId, IConnectable connection);

        public List<UniqueIdentifier> Read_AllWithPersonId(int personId, IConnectable connection);

        public List<UniqueIdentifier> Read(UniqueIdentifier uniqueId, int personId, IConnectable connection);

        public void Update(UniqueIdentifier uniqueIdentifier, int personId, IConnectable connection);

        public UniqueIdentifier Write(UniqueIdentifier uniqueId, int personId, IConnectable connection);
    }
}