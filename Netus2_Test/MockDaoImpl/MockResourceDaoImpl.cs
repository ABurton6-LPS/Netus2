using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockResourceDaoImpl : IResourceDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadUsingResourceId = false;
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

        public MockResourceDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(Resource resource, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<Resource> Read(Resource resource, IConnectable connection)
        {
            WasCalled_Read = true;

            List<Resource> returnData = new List<Resource>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.resource);

            return returnData;
        }

        public Resource Read_UsingResourceId(int resourceId, IConnectable connection)
        {
            WasCalled_ReadUsingResourceId = true;

            if (_shouldReadReturnData)
                return tdBuilder.resource;
            else
                return null;
        }

        public void Update(Resource resource, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public Resource Write(Resource resource, IConnectable connection)
        {
            WasCalled_Write = true;

            return tdBuilder.resource;
        }
    }
}
