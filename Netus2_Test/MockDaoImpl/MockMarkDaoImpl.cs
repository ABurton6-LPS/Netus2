using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockMarkDaoImpl : IMarkDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_DeleteWithPersonId = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadWithLineItemId = false;
        public bool WasCalled_ReadWithPersonId = false;
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

        public MockMarkDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(Mark mark, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<Mark> Read(Mark mark, int personId, IConnectable connection)
        {
            WasCalled_Read = true;

            List<Mark> returnData = new List<Mark>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.mark);

            return returnData;
        }

        public List<Mark> Read_AllWithLineItemId(int lineItemId, IConnectable connection)
        {
            WasCalled_ReadWithLineItemId = true;

            List<Mark> returnData = new List<Mark>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.mark);

            return returnData;
        }

        public List<Mark> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadWithPersonId = true;

            List<Mark> returnData = new List<Mark>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.mark);

            return returnData;
        }

        public void Update(Mark mark, int personId, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public Mark Write(Mark mark, int personId, IConnectable connection)
        {
            WasCalled_Write = true;

            return tdBuilder.mark;
        }
    }
}
