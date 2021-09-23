using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockApplicationDaoImpl : IApplicationDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadUsingAppId = false;
        public bool WasCalled_ReadUsingProviderId = false;
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

        public MockApplicationDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(Application appliction, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<Application> Read(Application application, IConnectable connection)
        {
            WasCalled_Read = true;

            List<Application> returnData = new List<Application>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.application);

            return returnData;
        }

        public Application Read_UsingAppId(int appId, IConnectable connection)
        {
            WasCalled_ReadUsingAppId = true;

            if (_shouldReadReturnData)
                return tdBuilder.application;
            else
                return null;
        }

        public List<Application> Read_UsingProviderId(int providerId, IConnectable connection)
        {
            WasCalled_ReadUsingProviderId = true;

            List<Application> returnData = new List<Application>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.application);

            return returnData;
        }

        public void Update(Application application, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public Application Write(Application application, IConnectable connection)
        {
            WasCalled_Write = true;

            return tdBuilder.application;
        }
    }
}
