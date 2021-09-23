using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockPersonDaoImpl : IPersonDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadUsingPersonId = false;
        public bool WasCalled_ReadUsingUniqueId = false;
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

        public MockPersonDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(Person person, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<Person> Read(Person person, IConnectable connection)
        {
            WasCalled_Read = true;

            List<Person> returnData = new List<Person>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.student);

            return returnData;
        }

        public Person Read_UsingPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadUsingPersonId = true;

            if (_shouldReadReturnData)
                return tdBuilder.student;
            else
                return null;
        }

        public Person Read_UsingUniqueId(int uniqueId, IConnectable connection)
        {
            WasCalled_ReadUsingUniqueId = true;

            if (_shouldReadReturnData)
                return tdBuilder.student;
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

            return tdBuilder.student;
        }
    }
}
