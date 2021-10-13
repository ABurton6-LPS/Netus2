﻿using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockAcademicSessionDaoImpl : IAcademicSessionDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadWithoutParentId = false;
        public bool WasCalled_ReadWithParentId = false;
        public bool WasCalled_ReadUsingAcademicSessionId = false;
        public bool WasCalled_ReadUsingClassEnrolledId = false;
        public bool WasCalled_ReadUsingOrganizationId = false;
        public bool WasCalled_ReadUsingBuildingCodeTermCodeSchoolYear = false;
        public bool WasCalled_ReadChildren = false;
        public bool WasCalled_ReadParent = false;
        public bool WasCalled_UpdateWithoutParentId = false;
        public bool WasCalled_UpdateWithParentId = false;
        public bool WasCalled_WriteWithoutParentId = false;
        public bool WasCalled_WriteWithParentId = false;
        public bool _shouldReadReturnData = false;

        public void SetTaskId(int taskId)
        {
            //Do Nothing
        }

        public int? GetTaskId()
        {
            return null;
        }

        public MockAcademicSessionDaoImpl (TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(AcademicSession academicSession, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<AcademicSession> Read(AcademicSession academicSession, IConnectable connection)
        {
            WasCalled_ReadWithoutParentId = true;

            List<AcademicSession> academicSessions = new List<AcademicSession>();

            if (_shouldReadReturnData)
                if (academicSession.Name == tdBuilder.semester1.Name)
                    academicSessions.Add(tdBuilder.semester1);
                else if (academicSession.Name == tdBuilder.semester2.Name)
                    academicSessions.Add(tdBuilder.semester2);
                else
                    academicSessions.Add(tdBuilder.schoolYear);

            return academicSessions;
        }

        public List<AcademicSession> Read(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            WasCalled_ReadWithParentId = true;

            List<AcademicSession> academicSessions = new List<AcademicSession>();

            if(_shouldReadReturnData)
                academicSessions.Add(tdBuilder.schoolYear);

            return academicSessions;
        }

        public AcademicSession Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            WasCalled_ReadUsingAcademicSessionId = true;

            if (_shouldReadReturnData)
            {
                switch (academicSessionId)
                {
                    case 1:
                        return tdBuilder.schoolYear;
                    case 2:
                        return tdBuilder.semester1;
                    case 3:
                        return tdBuilder.semester2;
                    default:
                        return tdBuilder.schoolYear;
                }
            }
            else
                return null;
        }

        public AcademicSession Read_UsingClassEnrolledId(int classEnrolledId, IConnectable connection)
        {
            WasCalled_ReadUsingClassEnrolledId = true;

            if (_shouldReadReturnData)
                if (classEnrolledId == 1)
                    return tdBuilder.semester1;
                else
                    return null;
            else
                return null;
        }

        public List<AcademicSession> Read_UsingOrganizationId(int organizationId, IConnectable connection)
        {
            WasCalled_ReadUsingOrganizationId = true;

            List<AcademicSession> academicSessions = new List<AcademicSession>();

            if(_shouldReadReturnData)
                academicSessions.Add(tdBuilder.schoolYear);

            return academicSessions;
        }

        public AcademicSession Read_UsingSisBuildingCode_TermCode_Schoolyear(string schoolCode, string termCode, int schoolYear, IConnectable connection)
        {
            WasCalled_ReadUsingBuildingCodeTermCodeSchoolYear = true;

            if (_shouldReadReturnData)
                if (termCode == tdBuilder.semester1.TermCode)
                    return tdBuilder.semester1;
                else if (termCode == tdBuilder.semester2.TermCode)
                    return tdBuilder.semester2;
                else
                    return tdBuilder.schoolYear;
            else
                return null;
        }

        public List<AcademicSession> Read_Children(AcademicSession parent, IConnectable connection)
        {
            WasCalled_ReadChildren = true;

            if (_shouldReadReturnData && parent.Children.Count > 0)
                return parent.Children;
            else
                return new List<AcademicSession>();
        }

        public AcademicSession Read_Parent(AcademicSession child, IConnectable connection)
        {
            WasCalled_ReadParent = true;

            if (_shouldReadReturnData)
                return tdBuilder.schoolYear;
            else
                return null;
        }

        public void Update(AcademicSession academicSession, IConnectable connection)
        {
            WasCalled_UpdateWithoutParentId = true;
        }

        public void Update(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            WasCalled_UpdateWithParentId = true;
        }

        public AcademicSession Write(AcademicSession academicSession, IConnectable connection)
        {
            WasCalled_WriteWithoutParentId = true;

            if (academicSession.Id == -1)
                academicSession.Id = 1;

            return academicSession;
        }

        public AcademicSession Write(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            WasCalled_WriteWithParentId = true;

            if (academicSession.Id == -1)
                academicSession.Id = 1;

            return academicSession;
        }
    }
}
