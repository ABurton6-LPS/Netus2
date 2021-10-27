using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockCourseDaoImpl : ICourseDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadUsingCourseId = false;
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

        public MockCourseDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(Course course, IConnectable connection)
        {
            WasCalled_Delete = false;
        }

        public List<Course> Read(Course course, IConnectable connection)
        {
            WasCalled_Read = true;

            List<Course> returnData = new List<Course>();

            if (_shouldReadReturnData)
                returnData.Add(tdBuilder.spanishCourse);

            return returnData;
        }

        public Course Read_UsingCourseId(int courseId, IConnectable connection)
        {
            WasCalled_ReadUsingCourseId = true;

            if (_shouldReadReturnData)
                return tdBuilder.spanishCourse;
            else
                return null;
        }

        public void Update(Course course, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public Course Write(Course course, IConnectable connection)
        {
            WasCalled_Write = true;

            return tdBuilder.spanishCourse;
        }
    }
}