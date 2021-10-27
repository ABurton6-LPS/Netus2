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
        public bool WasCalled_ReadUsingPersonId = false;
        public bool WasCalled_ReadUsingUniqueIdentifier = false;
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

        public MockPersonDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(Person person, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public Person Read_UsingPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadUsingPersonId = true;

            if (_shouldReadReturnData)
                if (tdBuilder.teacher.Id == personId)
                    return tdBuilder.teacher;
                else if (tdBuilder.student.Id == personId)
                    return tdBuilder.student;

            return null;
        }

        public Person Read_UsingUniqueIdentifier(string identifier, IConnectable connection)
        {
            WasCalled_ReadUsingUniqueIdentifier = true;

            if (_shouldReadReturnData)
                if (tdBuilder.teacher.UniqueIdentifiers[0].Identifier == identifier)
                    return tdBuilder.teacher;
                else if (tdBuilder.student.UniqueIdentifiers[0].Identifier == identifier)
                    return tdBuilder.student;

            return null;
        }

        public List<Person> Read(Person person, IConnectable connection)
        {
            WasCalled_Read = true;

            List<Person> results = new List<Person>();

            if (_shouldReadReturnData)
                if (tdBuilder.teacher.FirstName == person.FirstName)
                    results.Add(tdBuilder.teacher);
                else if (tdBuilder.student.FirstName == person.FirstName)
                    results.Add(tdBuilder.student);

            return results;
        }

        public void Update(Person person, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public Person Write(Person person, IConnectable connection)
        {
            WasCalled_Write = true;

            if (_shouldReadReturnData)
                if (tdBuilder.teacher.FirstName == person.FirstName)
                    return tdBuilder.teacher;
                else if (tdBuilder.student.FirstName == person.FirstName)
                    return tdBuilder.student;

            return null;
        }
    }
}
