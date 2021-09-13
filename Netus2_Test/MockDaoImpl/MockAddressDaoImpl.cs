using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockAddressDaoImpl : IAddressDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadUsingAddressId = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockAddressDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(Address address, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<Address> Read(Address address, IConnectable connection)
        {
            WasCalled_Read = true;

            List<Address> returnData = new List<Address>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.teacher.Addresses[0]);

            return returnData;
        }

        public Address Read_UsingAdddressId(int addressId, IConnectable connection)
        {
            WasCalled_ReadUsingAddressId = true;

            if (_shouldReadReturnData)
                return tdBuilder.teacher.Addresses[0];
            else
                return null;
        }

        public void Update(Address address, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public Address Write(Address address, IConnectable connection)
        {
            WasCalled_Write = true;

            return tdBuilder.teacher.Addresses[0];
        }
    }
}
