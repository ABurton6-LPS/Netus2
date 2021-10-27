using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface ICourseDao
    {
        void SetTaskId(int taskId);

        int? GetTaskId();

        public void Delete(Course course, IConnectable connection);

        public Course Read_UsingCourseId(int courseId, IConnectable connection);

        public List<Course> Read(Course course, IConnectable connection);

        public void Update(Course course, IConnectable connection);

        public Course Write(Course course, IConnectable connection);
    }
}
