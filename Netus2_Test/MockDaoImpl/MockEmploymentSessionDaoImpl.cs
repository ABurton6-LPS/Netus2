using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockEmploymentSessionDaoImpl : IEmploymentSessionDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_DeleteWithOrganizationId = false;
        public bool WasCalled_DeleteWithPersonId = false;
        public bool WasCalled_ReadWithOrganizationId = false;
        public bool WasCalled_ReadWithPersonId = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockEmploymentSessionDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete_WithOrganizationId(EmploymentSession employmentSession, int organizationId, IConnectable connection)
        {
            WasCalled_DeleteWithOrganizationId = true;
        }

        public void Delete_WithPersonId(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            WasCalled_DeleteWithPersonId = true;
        }

        public List<EmploymentSession> Read_WithOrganizationId(EmploymentSession employmentSession, int organizationId, IConnectable connection)
        {
            WasCalled_ReadWithOrganizationId = true;

            List<EmploymentSession> returnData = new List<EmploymentSession>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.employmentSession);

            return returnData;
        }

        public List<EmploymentSession> Read_WithPersonId(EmploymentSession employmentSession, int personId, IConnectable connection)
        {
            WasCalled_ReadWithPersonId = true;

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
