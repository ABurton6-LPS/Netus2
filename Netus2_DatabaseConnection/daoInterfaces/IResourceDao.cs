using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IResourceDao
    {
        void SetTaskId(int taskId);

        int? GetTaskId();

        public void Delete(Resource resource, IConnectable connection);

        public Resource Read_UsingResourceId(int resourceId, IConnectable connection);

        public List<Resource> Read(Resource resource, IConnectable connection);

        public void Update(Resource resource, IConnectable connection);

        public Resource Write(Resource resource, IConnectable connection);
    }
}
