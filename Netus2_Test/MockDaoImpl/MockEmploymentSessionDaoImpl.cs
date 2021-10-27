using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockEmploymentSessionDaoImpl : IEmploymentSessionDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadAllWithOrganizationId = false;
        public bool WasCalled_ReadAllWithPersonId = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_Write = false;
        public bool WasCalled_ReadUsingPersonId = false;
        public bool WasCalled_ReadUsingOrganizationId = false;
        public bool _shouldReadReturnData = false;

        public void SetTaskId(int taskId)
        {
            //Do Nothing
        }

        public int? GetTaskId()
        {
            return null;
        }

        public MockEmploymentSessionDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(EmploymentSession employmentSession, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<EmploymentSession> Read_AllWithOrganizationId(int organizationId, IConnectable connection)
        {
            WasCalled_ReadAllWithOrganizationId = true;

            List<EmploymentSession> returnData = new List<EmploymentSession>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.employmentSession);

            return returnData;
        }

        public List<EmploymentSession> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadAllWithPersonId = true;

            List<EmploymentSession> returnData = new List<EmploymentSession>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.employmentSession);

            return returnData;
        }

        public List<EmploymentSession> Read_UsingPersonId(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            WasCalled_ReadUsingPersonId = true;

            List<EmploymentSession> returnData = new List<EmploymentSession>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.employmentSession);

            return returnData;
        }

        public List<EmploymentSession> Read_UsingOrganizationId(EmploymentSession employmentSession, int organizationId, IConnectable connection)
        {
            WasCalled_ReadUsingOrganizationId = true;

            List<EmploymentSession> returnData = new List<EmploymentSession>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.employmentSession);

            return returnData;
        }

        public void Update(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public EmploymentSession Write(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            WasCalled_Write = true;

            return tdBuilder.employmentSession;
        }
    }
}
