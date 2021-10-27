using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IEnrollmentDao
    {
        void SetTaskId(int taskId);

        int? GetTaskId();

        public void Delete(Enrollment enrollment, IConnectable connection);

        public List<Enrollment> Read_AllWithClassId(int classId, IConnectable connection);

        public List<Enrollment> Read_AllWithPersonId(int personId, IConnectable connection);

        public List<Enrollment> Read(Enrollment enrollment, int personId, IConnectable connection);

        public void Update(Enrollment enrollment, int personId, IConnectable connection);

        public Enrollment Write(Enrollment enrollment, int personId, IConnectable connection);
    }
}
