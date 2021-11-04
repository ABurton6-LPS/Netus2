using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockEmailDaoImpl : IEmailDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadUsingEmailId = false;
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

        public void Delete(Email email, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public Email Read_UsingEmailId(int emailId, IConnectable connection)
        {
            WasCalled_ReadUsingEmailId = true;

            if (_shouldReadReturnData)
            {
                if (tdBuilder.email_Student.Id == emailId)
                    return tdBuilder.email_Student;
                if (tdBuilder.email_Teacher.Id == emailId)
                    return tdBuilder.email_Teacher;
            }

            return null;
        }

        public List<Email> Read(Email email, IConnectable connection)
        {
            WasCalled_Read = true;

            List<Email> results = new List<Email>();

            if (_shouldReadReturnData)
            {
                email.Id = 100;
                results.Add(email);
            }

            return results;
        }

        public void Update(Email email, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public Email Write(Email email, IConnectable connection)
        {
            WasCalled_Write = true;

            email.Id = 100;

            return email;
        }

        public MockEmailDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }
    }
}
