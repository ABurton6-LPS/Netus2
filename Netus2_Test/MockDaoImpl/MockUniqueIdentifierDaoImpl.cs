using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockUniqueIdentifierDaoImpl : IUniqueIdentifierDao
    {
        TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
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

        public MockUniqueIdentifierDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<UniqueIdentifier> Read(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            WasCalled_Read = true;

            List <UniqueIdentifier> returnData =  new List<UniqueIdentifier>();

            if (_shouldReadReturnData)
            {
                if (personId == tdBuilder.student.Id)
                    returnData.Add(tdBuilder.uniqueId_Student);
                else
                    returnData.Add(tdBuilder.uniqueId_Teacher);
            }

            return returnData;
        }

        public void Update(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public UniqueIdentifier Write(UniqueIdentifier uniqueId, int personId, IConnectable connection)
        {
            WasCalled_Write = true;

            if (personId == tdBuilder.student.Id)
                return tdBuilder.uniqueId_Student;
            else
                return tdBuilder.uniqueId_Teacher;
        }
    }
}
