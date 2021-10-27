using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IEmploymentSessionDao
    {
        void SetTaskId(int taskId);

        int? GetTaskId();

        public void Delete(EmploymentSession employmentSession, IConnectable connection);

        public List<EmploymentSession> Read_AllWithPersonId(int personId, IConnectable connection);

        public List<EmploymentSession> Read_UsingPersonId(EmploymentSession employmentSession, int personId, IConnectable connection);

        public List<EmploymentSession> Read_AllWithOrganizationId(int organizationId, IConnectable connection);

        public List<EmploymentSession> Read_UsingOrganizationId(EmploymentSession employmentSession, int organizationId, IConnectable connection);

        public void Update(EmploymentSession employmentSession, int personId, IConnectable connection);

        public EmploymentSession Write(EmploymentSession employmentSession, int personId, IConnectable connection);
    }
}
