using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctCourseGradeDao
    {
        public void Delete(int courseId, int gradeId, IConnectable connection);

        public DataRow Read(int courseId, int gradeId, IConnectable connection);

        public List<DataRow> Read_AllWithCourseId(int courseId, IConnectable connection);

        public List<DataRow> Read_AllWithGradeId(int gradeId, IConnectable connection);

        public DataRow Write(int courseId, int gradeId, IConnectable connection);
    }
}
