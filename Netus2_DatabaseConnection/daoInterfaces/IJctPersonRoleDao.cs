using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonRoleDao
    {
        public void Delete(int personId, int roleId, IConnectable connection);

        public DataRow Read(int personId, int roleId, IConnectable connection);

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection);

        public List<DataRow> Read_AllWithRoleId(int roleId, IConnectable connection);

        public DataRow Write(int personId, int roleId, IConnectable connection);
    }
}
