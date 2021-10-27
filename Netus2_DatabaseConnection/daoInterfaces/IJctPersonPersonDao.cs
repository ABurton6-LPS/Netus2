using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonPersonDao
    {
        public void Delete(int personOneId, int personTwoId, IConnectable connection);

        public DataRow Read(int personOneId, int personTwoId, IConnectable connection);

        public List<DataRow> Read_AllWithPersonOneId(int personOneId, IConnectable connection);

        public DataRow Write(int personOneId, int personTwoId, IConnectable connection);


    }
}