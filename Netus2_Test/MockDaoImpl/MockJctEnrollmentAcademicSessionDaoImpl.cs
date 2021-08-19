using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctEnrollmentAcademicSessionDaoImpl : IJctEnrollmentAcademicSessionDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadWithAcademicSessionId = false;
        public bool WasCalled_WithEnrollmentId = false;
        public bool WasCalled_Write = false;

        private JctEnrollmentAcademicSessionDao dao = new JctEnrollmentAcademicSessionDao();

        public MockJctEnrollmentAcademicSessionDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
            dao.academic_session_id = tdBuilder.schoolYear.Id;
            dao.enrollment_id = tdBuilder.enrollment.Id;
        }

        public void Delete(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public JctEnrollmentAcademicSessionDao Read(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            WasCalled_Read = true;            
            return dao;
        }

        public List<JctEnrollmentAcademicSessionDao> Read_WithAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            WasCalled_ReadWithAcademicSessionId = true;
            List<JctEnrollmentAcademicSessionDao> list = new List<JctEnrollmentAcademicSessionDao>();
            list.Add(dao);
            return list;
        }

        public List<JctEnrollmentAcademicSessionDao> Read_WithEnrollmentId(int enrollmentId, IConnectable connection)
        {
            WasCalled_WithEnrollmentId = true;
            List<JctEnrollmentAcademicSessionDao> list = new List<JctEnrollmentAcademicSessionDao>();
            list.Add(dao);
            return list;
        }

        public JctEnrollmentAcademicSessionDao Write(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            WasCalled_Write = true;
            return dao;
        }
    }
}
