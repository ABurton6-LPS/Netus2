using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IPhoneNumberDao
    {
        void SetTaskId(int taskId);

        int? GetTaskId();

        public void Delete(PhoneNumber phoneNumber, IConnectable connection);

        public List<PhoneNumber> Read(PhoneNumber phoneNumber, IConnectable connection);

        public List<PhoneNumber> Read_AllWithPersonId(int personId, IConnectable connection);

        public List<PhoneNumber> Read(PhoneNumber phoneNumber, int personId, IConnectable connection);

        public void Update(PhoneNumber phoneNumber, IConnectable connection);

        public void Update(PhoneNumber phoneNumber, int personId, IConnectable connection);

        public PhoneNumber Write(PhoneNumber phoneNumber, IConnectable connection);

        public PhoneNumber Write(PhoneNumber phoneNumber, int personId, IConnectable connection);
    }
}