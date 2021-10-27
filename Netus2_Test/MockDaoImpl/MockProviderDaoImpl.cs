using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockProviderDaoImpl : IProviderDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadWithParentId = false;
        public bool WasCalled_ReadWithoutParentId = false;
        public bool WasCalled_ReadWithProviderId = false;
        public bool WasCalled_ReadWithAppId = false;
        public bool WasCalled_ReadAllChildrenWithParentId = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_WriteWithParentId = false;
        public bool WasCalled_WriteWithoutParentId = false;
        public bool _shouldReadReturnData = false;

        public void SetTaskId(int taskId)
        {
            //Do Nothing
        }

        public int? GetTaskId()
        {
            return null;
        }

        public MockProviderDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(Provider provider, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<Provider> Read(Provider provider, IConnectable connection)
        {
            WasCalled_ReadWithoutParentId = true;

            List<Provider> returnData = new List<Provider>();

            if (_shouldReadReturnData)
            {
                returnData.Add(tdBuilder.provider);
                return returnData;
            }
            else
                return returnData;
        }

        public List<Provider> Read(Provider provider, int parentId, IConnectable connection)
        {
            WasCalled_ReadWithParentId = true;

            List<Provider> returnData = new List<Provider>();

            if (_shouldReadReturnData)
            {
                returnData.Add(tdBuilder.provider);
                return returnData;
            }
            else
                return returnData;
        }

        public Provider Read_AllWithAppId(int appId, IConnectable connection)
        {
            WasCalled_ReadWithAppId = true;

            if (_shouldReadReturnData)
                return tdBuilder.provider;
            else
                return null;
        }

        public List<Provider> Read_AllChildrenWithParentId(int parentId, IConnectable connection)
        {
            WasCalled_ReadAllChildrenWithParentId = true;

            List<Provider> returnData = new List<Provider>();

            if (_shouldReadReturnData)
            {
                returnData.Add(tdBuilder.provider);
                return returnData;
            }
            else
                return returnData;
        }

        public Provider Read_UsingProviderId(int providerId, IConnectable connection)
        {
            WasCalled_ReadWithProviderId = true;

            if (_shouldReadReturnData)
                return tdBuilder.provider;
            else
                return null;
        }

        public void Update(Provider provider, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public Provider Write(Provider provider, IConnectable connection)
        {
            WasCalled_WriteWithoutParentId = true;

            return tdBuilder.provider;
        }

        public Provider Write(Provider provider, int parentProviderId, IConnectable connection)
        {
            WasCalled_WriteWithParentId = true;

            return tdBuilder.provider;
        }
    }
}
