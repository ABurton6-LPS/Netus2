using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockEnrollmentDaoImpl : IEnrollmentDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_DelteWithPersonId = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadAllWithAcademicSessionId = false;
        public bool WasCalled_ReadAllWithPersonId = false;
        public bool WasCalled_ReadUsingAcademicSessionIdAndPersonId = false;
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
            WasCalled_Delete = true;
        }

        public List<Enrollment> Read(Enrollment enrollment, int personId, IConnectable connection)
        {
            WasCalled_Read = true;

            List<Enrollment> returnData = new List<Enrollment>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.enrollment);

            return returnData;
        }

        public List<Enrollment> Read_AllWithAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            WasCalled_ReadAllWithAcademicSessionId = true;

            List<Enrollment> returnData = new List<Enrollment>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.enrollment);

            return returnData;
        }

        public List<Enrollment> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadAllWithPersonId = true;

            List<Enrollment> returnData = new List<Enrollment>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.enrollment);

            return returnData;
        }

        public Enrollment Read_UsingAcademicSessionIdAndPersonId(int academicSessionId, int personId, IConnectable connection)
        {
            WasCalled_ReadUsingAcademicSessionIdAndPersonId = true;

            if (_shouldReadReturnData)
                return tdBuilder.enrollment;

            return null;
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