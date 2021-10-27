using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonAddressDao
    {
        public void Delete(int personId, int addressId, IConnectable connection);

        public DataRow Read(int personId, int addressId, IConnectable connection);

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection);

        public List<DataRow> Read_WithAddressId(int addressId, IConnectable connection);

        public List<DataRow> Read_AllAddressIsNotInTempTable(IConnectable connection);

        public DataRow Write(int personId, int addressId, IConnectable connection);

        public void Write_ToTempTable(int personId, int addressId, IConnectable connection);
    }
}
