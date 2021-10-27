using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IOrganizationDao
    {
        void SetTaskId(int taskId);

        int? GetTaskId();

        public void Delete(Organization organization, IConnectable connection);

        public Organization Read_UsingSisBuildingCode(string sisBuildingCode, IConnectable connection);

        public Organization Read_UsingOrganizationId(int orgId, IConnectable connection);

        public Organization Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection);

        public Organization Read_Parent(Organization organization, IConnectable connection);

        public List<Organization> Read_AllChildrenWithParentId(int parentId, IConnectable connection);

        public List<Organization> Read(Organization organization, IConnectable connection);

        public List<Organization> Read(Organization organization, int parentId, IConnectable connection);

        public void Update(Organization organization, IConnectable connection);

        public void Update(Organization organization, int parentOrganizationId, IConnectable connection);

        public Organization Write(Organization organization, IConnectable connection);

        public Organization Write(Organization organization, int parentOrganizationId, IConnectable connection);
    }
}
