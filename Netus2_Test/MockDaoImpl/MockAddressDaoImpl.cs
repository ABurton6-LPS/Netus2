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
        public bool WasCalled_ReadUsingAddressId = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public void SetTaskId(int taskId)
        {
            //Do Nothing
        }

        public int? GetTaskId()
        {
            return null;
        }

        public void Delete(Address address, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public Address Read_UsingAddressId(int addressId, IConnectable connection)
        {
            WasCalled_ReadUsingAddressId = true;

            if (_shouldReadReturnData)
            {
                if (tdBuilder.address_Student.Id == addressId)
                    return tdBuilder.address_Student;
                if (tdBuilder.address_Teacher.Id == addressId)
                    return tdBuilder.address_Teacher;
            }

            return null;
        }

        public List<Address> Read(Address address, IConnectable connection)
        {
            WasCalled_Read = true;

            List<Address> results = new List<Address>();

            if (_shouldReadReturnData)
            {
                address.Id = 100;
                results.Add(address);
            }

            return results;
        }

        public void Update(Address address, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public Address Write(Address address, IConnectable connection)
        {
            WasCalled_Write = true;

            address.Id = 100;

            return address;
        }

        public MockAddressDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }
    }
}
