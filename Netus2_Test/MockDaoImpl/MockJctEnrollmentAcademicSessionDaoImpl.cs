using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctEnrollmentAcademicSessionDaoImpl : IJctEnrollmentAcademicSessionDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadWithAcademicSessionId = false;
        public bool WasCalled_ReadWithEnrollmentId = false;
        public bool WasCalled_Write = false;

        private DataRow row = DataTableFactory.CreateDataTable_Netus2_JctEnrollmentAcademicSession().NewRow();

        public MockJctEnrollmentAcademicSessionDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
            row["academic_session_id"] = tdBuilder.schoolYear.Id;
            row["enrollment_id"] = tdBuilder.enrollment.Id;
        }

        public void Delete(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public DataRow Read(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            WasCalled_Read = true;            
            return row;
        }

        public List<DataRow> Read_WithAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            WasCalled_ReadWithAcademicSessionId = true;
            List<DataRow> list = new List<DataRow>();
            list.Add(row);
            return list;
        }

        public List<DataRow> Read_WithEnrollmentId(int enrollmentId, IConnectable connection)
        {
            WasCalled_ReadWithEnrollmentId = true;
            List<DataRow> list = new List<DataRow>();
            list.Add(row);
            return list;
        }

        public DataRow Write(int enrollmentId, int academicSessionId, IConnectable connection)
        {
            WasCalled_Write = true;
            return row;
        }
    }
}
