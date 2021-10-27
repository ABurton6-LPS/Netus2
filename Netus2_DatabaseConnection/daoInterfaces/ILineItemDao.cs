using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface ILineItemDao
    {
        public void SetTaskId(int taskId);

        public int? GetTaskId();

        public void Delete(LineItem lineItem, IConnectable connection);

        public LineItem Read_UsingLineItemId(int lineItemId, IConnectable connection);

        public List<LineItem> Read_AllWithClassEnrolledId(int classEnrolledId, IConnectable connection);

        public List<LineItem> Read(LineItem lineItem, IConnectable connection);

        public void Update(LineItem lineItem, IConnectable connection);

        public LineItem Write(LineItem lineItem, IConnectable connection);
    }
}
