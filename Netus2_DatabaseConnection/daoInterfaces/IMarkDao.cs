using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IMarkDao
    {
        public void SetTaskId(int taskId);

        public int? GetTaskId();

        public void Delete(Mark mark, IConnectable connection);

        public List<Mark> Read_AllWithLineItemId(int lineItemId, IConnectable connection);

        public List<Mark> Read_AllWithPersonId(int personId, IConnectable connection);

        public List<Mark> Read(Mark mark, int personId, IConnectable connection);

        public void Update(Mark mark, int personId, IConnectable connection);

        public Mark Write(Mark mark, int personId, IConnectable connection);
    }
}
