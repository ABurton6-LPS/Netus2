using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockLineItemDaoImpl : ILineItemDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadWithLineItemId = false;
        public bool WasCalled_ReadWithClassEnrolledId = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public void SetTaskId(int taskId)
        {
            //Do Nothing
        }

        public int? GetTaskId()
        {
            return null;
        }

        public MockLineItemDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(LineItem lineItem, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<LineItem> Read(LineItem lineItem, IConnectable connection)
        {
            WasCalled_Read = true;

            List<LineItem> returnData = new List<LineItem>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.lineItem);

            return returnData;
        }

        public LineItem Read_UsingLineItemId(int lineItemId, IConnectable connection)
        {
            WasCalled_ReadWithLineItemId = true;

            if (_shouldReadReturnData)
                return tdBuilder.lineItem;
            else
                return null;
        }

        public List<LineItem> Read_AllWithClassEnrolledId(int classEnrolledId, IConnectable connection)
        {

            WasCalled_ReadWithClassEnrolledId = true;

            List<LineItem> returnData = new List<LineItem>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.lineItem);

            return returnData;
        }

        public void Update(LineItem lineItem, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public LineItem Write(LineItem lineItem, IConnectable connection)
        {
            WasCalled_Write = true;

            return tdBuilder.lineItem;
        }
    }
}
