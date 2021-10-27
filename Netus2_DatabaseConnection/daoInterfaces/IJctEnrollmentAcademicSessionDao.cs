using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctEnrollmentAcademicSessionDao
    {
        public void Delete(int enrollmentId, int academicSessionId, IConnectable connection);

        public DataRow Read(int enrollmentId, int academicSessionId, IConnectable connection);

        public List<DataRow> Read_AllWithEnrollmentId(int enrollmentId, IConnectable connection);

        public List<DataRow> Read_AllWithAcademicSessionId(int academicSessionId, IConnectable connection);

        public DataRow Write(int enrollmentId, int academicSessionId, IConnectable connection);
    }
}
