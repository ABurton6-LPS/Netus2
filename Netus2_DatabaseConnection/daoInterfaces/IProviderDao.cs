using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IProviderDao
    {
        void SetTaskId(int taskId);

        int? GetTaskId();

        public void Delete(Provider provider, IConnectable connection);

        public Provider Read_UsingProviderId(int providerId, IConnectable connection);

        public Provider Read_AllWithAppId(int appId, IConnectable connection);

        public List<Provider> Read_AllChildrenWithParentId(int parentId, IConnectable connection);

        public List<Provider> Read(Provider provider, IConnectable connection);

        public List<Provider> Read(Provider provider, int parentId, IConnectable connection);

        public void Update(Provider provider, IConnectable connection);

        public Provider Write(Provider provider, IConnectable connection);

        public Provider Write(Provider provider, int parentProviderId, IConnectable connection);
    }
}