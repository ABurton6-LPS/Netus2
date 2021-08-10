using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockPersonDaoImpl : IPersonDao
    {
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadUsingPersonId = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_Write = false;
        public List<Person> ReadReturnData = new List<Person>();

        public void Delete(Person person, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<Person> Read(Person person, IConnectable connection)
        {
            WasCalled_Read = true;
            return ReadReturnData;
        }

        public Person Read_UsingPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadUsingPersonId = true;
            if (ReadReturnData.Count > 0)
                return ReadReturnData[0];
            else
                return null;
        }

        public void Update(Person person, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public Person Write(Person person, IConnectable connection)
        {
            WasCalled_Write = true;
            return person;
        }
    }
}
