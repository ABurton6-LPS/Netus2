using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctCourseSubjectDao
    {
        public void Delete(int courseId, int subjectId, IConnectable connection);

        public DataRow Read(int courseId, int subjectId, IConnectable connection);

        public List<DataRow> Read_AllWithCourseId(int courseId, IConnectable connection);

        public List<DataRow> Read_AllWithSubjectId(int subjectId, IConnectable connection);

        public DataRow Write(int courseId, int subjectId, IConnectable connection);
    }
}
