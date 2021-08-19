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
        public bool WasCalled_ReadWithClassId = false;
        public bool WasCalled_UsingAcademicSessionId = false;
        public bool WasCalled_UsingCourseId = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_Write = false;


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
            list.Add(tdBuilder.classEnrolled);
            return list;
        }

        public ClassEnrolled Read(int classId, IConnectable connection)
        {
            WasCalled_ReadWithClassId = true;
            return tdBuilder.classEnrolled;
        }

        public List<ClassEnrolled> Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            WasCalled_UsingAcademicSessionId = true;
            List<ClassEnrolled> list = new List<ClassEnrolled>();
            list.Add(tdBuilder.classEnrolled);
            return list;
        }

        public List<ClassEnrolled> Read_UsingCourseId(int courseId, IConnectable connection)
        {
            WasCalled_UsingCourseId = true;
            List<ClassEnrolled> list = new List<ClassEnrolled>();
            list.Add(tdBuilder.classEnrolled);
            return list;
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