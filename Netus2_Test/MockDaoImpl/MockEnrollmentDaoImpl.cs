using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockEnrollmentDaoImpl : IEnrollmentDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_DeleteWithoutPersonId = false;
        public bool WasCalled_DelteWithPersonId = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadWithClassId = false;
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

        public MockEnrollmentDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(Enrollment enrollment, IConnectable connection)
        {
            WasCalled_DeleteWithoutPersonId = true;
        }

        public void Delete(Enrollment enrollment, int personId, IConnectable connection)
        {
            WasCalled_DelteWithPersonId = true;
        }

        public List<Enrollment> Read(Enrollment enrollment, int personId, IConnectable connection)
        {
            WasCalled_Read = true;

            List<Enrollment> returnData = new List<Enrollment>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.enrollment);

            return returnData;
        }

        public List<Enrollment> Read_WithClassId(int classId, IConnectable connection)
        {
            WasCalled_ReadWithClassId = true;

            List<Enrollment> returnData = new List<Enrollment>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.enrollment);

            return returnData;
        }

        public void Update(Enrollment enrollment, int personId, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public Enrollment Write(Enrollment enrollment, int personId, IConnectable connection)
        {
            WasCalled_Write = true;

            return tdBuilder.enrollment;
        }
    }
}