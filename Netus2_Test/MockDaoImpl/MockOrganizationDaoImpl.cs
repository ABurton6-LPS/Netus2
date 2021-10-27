using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockOrganizationDaoImpl : IOrganizationDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadWithoutParentId = false;
        public bool WasCalled_ReadWithParentId = false;
        public bool WasCalled_ReadWithAcademicSessionId = false;
        public bool WasCalled_ReadWithSisBuildingCode = false;
        public bool WasCalled_ReadWithOrganizationId = false;
        public bool WasCalled_ReadParent = false;
        public bool WasCalled_ReadAllChildrenWithParentId;
        public bool WasCalled_UpdateWithoutParentId = false;
        public bool WasCalled_UpdateWithParentId = false;
        public bool WasCalled_WriteWithoutParentId = false;
        public bool WasCalled_WriteWithParentId = false;
        public bool _shouldReadReturnData = false;

        public void SetTaskId(int taskId)
        {
            //Do Nothing
        }

        public int? GetTaskId()
        {
            return null;
        }

        public MockOrganizationDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(Organization organization, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<Organization> Read(Organization organization, IConnectable connection)
        {
            WasCalled_ReadWithoutParentId = true;

            List<Organization> orgs = new List<Organization>();

            if(_shouldReadReturnData)
                orgs.Add(tdBuilder.school);

            return orgs;
        }

        public List<Organization> Read(Organization organization, int parentId, IConnectable connection)
        {
            WasCalled_ReadWithParentId = true;

            List<Organization> orgs = new List<Organization>();

            if (_shouldReadReturnData)
                orgs.Add(tdBuilder.school);

            return orgs;
        }

        public Organization Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            WasCalled_ReadWithAcademicSessionId = true;

            if (_shouldReadReturnData)
                return tdBuilder.school;
            else
                return null;
        }

        public Organization Read_UsingSisBuildingCode(string sisBuildingCode, IConnectable connection)
        {
            WasCalled_ReadWithSisBuildingCode = true;

            if (_shouldReadReturnData && sisBuildingCode != "")
                if (sisBuildingCode == tdBuilder.district.SisBuildingCode)
                    return tdBuilder.district;
                else
                    return tdBuilder.school;
            else
                return null;
        }

        public Organization Read_UsingOrganizationId(int organizationId, IConnectable connection)
        {
            WasCalled_ReadWithOrganizationId = true;

            if (_shouldReadReturnData)
                return tdBuilder.school;
            else
                return null;
        }

        public Organization Read_Parent(Organization org, IConnectable connection)
        {
            WasCalled_ReadParent = true;

            if (_shouldReadReturnData)
                return tdBuilder.district;
            else
                return null;
        }

        public List<Organization> Read_AllChildrenWithParentId(int parentId, IConnectable connection)
        {
            WasCalled_ReadAllChildrenWithParentId = true;

            List<Organization> orgs = new List<Organization>();

            if (_shouldReadReturnData)
                orgs.Add(tdBuilder.school);

            return orgs;
        }

        public void Update(Organization organization, IConnectable connection)
        {
            WasCalled_UpdateWithoutParentId = true;
        }

        public void Update(Organization organization, int parentOrganizationId, IConnectable connection)
        {
            WasCalled_UpdateWithParentId = true;
        }

        public Organization Write(Organization organization, IConnectable connection)
        {
            WasCalled_WriteWithoutParentId = true;
            return organization;
        }

        public Organization Write(Organization organization, int parentId, IConnectable connection)
        {
            WasCalled_WriteWithParentId = true;
            return organization;
        }
    }
}
