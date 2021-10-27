using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonAppDao
    {
        public void Delete(int personId, int appId, IConnectable connection);

        public DataRow Read(int personId, int appId, IConnectable connection);

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection);

        public List<DataRow> Read_AllWithAppId(int appId, IConnectable connection);

        public DataRow Write(int personId, int appId, IConnectable connection);
    }
}
