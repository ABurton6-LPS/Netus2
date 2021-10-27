using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctClassPersonDao
    {
        public void Delete(int classId, int personId, IConnectable connection);

        public DataRow Read(int classId, int personId, int roleId, IConnectable connection);

        public List<DataRow> Read_AllWithClassId(int classId, IConnectable connection);

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection);

        public List<DataRow> Read_AllWithRoleId(int roleId, IConnectable connection);

        public DataRow Write(int classId, int personId, int roleId, IConnectable connection);
    }
}
