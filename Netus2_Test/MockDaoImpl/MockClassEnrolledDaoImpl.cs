using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockClassEnrolledDaoImpl : IClassEnrolledDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadUsingClassId = false;
        public bool WasCalled_UsingAcademicSessionId = false;
        public bool WasCalled_UsingCourseId = false;
        public bool WasCalled_UsingClassEnrolledCode = false;
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

        public MockClassEnrolledDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(ClassEnrolled classEnrolled, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<ClassEnrolled> Read(ClassEnrolled classEnrolled, IConnectable connection)
        {
            WasCalled_Read = true;

            List<ClassEnrolled> list = new List<ClassEnrolled>();

            if (_shouldReadReturnData)
                list.Add(tdBuilder.classEnrolled);
            
            return list;
        }

        public ClassEnrolled Read_UsingClassId(int classId, IConnectable connection)
        {
            WasCalled_ReadUsingClassId = true;

            if (_shouldReadReturnData)
                return tdBuilder.classEnrolled;
            else
                return null;
        }

        public List<ClassEnrolled> Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            WasCalled_UsingAcademicSessionId = true;

            List<ClassEnrolled> list = new List<ClassEnrolled>();

            if(_shouldReadReturnData)
                list.Add(tdBuilder.classEnrolled);

            return list;
        }

        public List<ClassEnrolled> Read_UsingCourseId(int courseId, IConnectable connection)
        {
            WasCalled_UsingCourseId = true;

            List<ClassEnrolled> list = new List<ClassEnrolled>();

            if(_shouldReadReturnData)
                list.Add(tdBuilder.classEnrolled);

            return list;
        }

        public ClassEnrolled Read_UsingClassEnrolledCode(string classCode, IConnectable connection)
        {
            WasCalled_UsingClassEnrolledCode = true;

            if (_shouldReadReturnData)
                return tdBuilder.classEnrolled;

            return null;
        }

        public void Update(ClassEnrolled classEnrolled, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public ClassEnrolled Write(ClassEnrolled classEnrolled, IConnectable connection)
        {
            WasCalled_Write = true;

            return tdBuilder.classEnrolled;
        }
    }
}