using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockUniqueIdentifierDaoImpl : IUniqueIdentifierDao
    {
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_Write = false;
        public List<UniqueIdentifier> ReadReturnData = new List<UniqueIdentifier>();

        public void Delete(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<UniqueIdentifier> Read(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            WasCalled_Read = true;
            return ReadReturnData;
        }

        public void Update(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public UniqueIdentifier Write(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            WasCalled_Write = true;
            return uniqueId;
        }
    }
}
