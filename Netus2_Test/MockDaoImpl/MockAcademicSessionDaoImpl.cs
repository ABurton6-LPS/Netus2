using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockAcademicSessionDaoImpl : IAcademicSessionDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadAllWithOrganizationId = false;
        public bool WasCalled_ReadUsingClassEnrolledId = false;
        public bool WasCalled_ReadUsingAcademicSessionId = false;
        public bool WasCalled_ReadUsingSisBuildingCodeTermCodeSchoolyear = false;
        public bool WasCalled_ReadParent = false;
        public bool WasCalled_ReadChildren = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadWithParentId = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_UpdateWithParentId = false;
        public bool WasCalled_Write = false;
        public bool WasCalled_WriteWithParentId = false;
        public bool _shouldReadReturnData = false;

        public MockAcademicSessionDaoImpl(TestDataBuilder builder)
        {
            tdBuilder = builder;
        }

        public void SetTaskId(int taskId)
        {
            //Do Nothing
        }

        public int? GetTaskId()
        {
            return null;
        }

        public void Delete(AcademicSession academicSession, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<AcademicSession> Read_AllWithOrganizationId(int organizationId, IConnectable connection)
        {
            WasCalled_ReadAllWithOrganizationId = true;

            List<AcademicSession> results = new List<AcademicSession>();

            if (_shouldReadReturnData)
            {
                if (tdBuilder.schoolYear.Organization.Id == organizationId)
                    results.Add(tdBuilder.schoolYear);
                if (tdBuilder.semester1.Organization.Id == organizationId)
                    results.Add(tdBuilder.semester1);
                if (tdBuilder.semester2.Organization.Id == organizationId)
                    results.Add(tdBuilder.semester2);
            }

            return results;
        }

        public AcademicSession Read_UsingClassEnrolledId(int classEnrolledId, IConnectable connection)
        {
            WasCalled_ReadUsingClassEnrolledId = true;

            if (_shouldReadReturnData)
                if (tdBuilder.classEnrolled.Id == classEnrolledId)
                    return tdBuilder.classEnrolled.AcademicSession;

            return null;
        }

        public AcademicSession Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            WasCalled_ReadUsingAcademicSessionId = true;

            if (_shouldReadReturnData)
            {
                if (tdBuilder.schoolYear.Id == academicSessionId)
                    return tdBuilder.schoolYear;
                if (tdBuilder.semester1.Id == academicSessionId)
                    return tdBuilder.semester1;
                if (tdBuilder.semester2.Id == academicSessionId)
                    return tdBuilder.semester2;
            }

            return null;
        }

        public AcademicSession Read_UsingSisBuildingCode_TermCode_TrackCode_Schoolyear(string sisBuildingCode, string termCode, int schoolYear, IConnectable connection)
        {
            WasCalled_ReadUsingSisBuildingCodeTermCodeSchoolyear = true;

            if (_shouldReadReturnData)
            {
                AcademicSession schoolYearSession = tdBuilder.schoolYear;
                AcademicSession semester1 = tdBuilder.semester1;
                AcademicSession semester2 = tdBuilder.semester2;

                if (schoolYearSession.Organization.SisBuildingCode == sisBuildingCode &&
                    schoolYearSession.TermCode == termCode &&
                    schoolYearSession.SchoolYear == schoolYear)
                    return schoolYearSession;
                if (semester1.Organization.SisBuildingCode == sisBuildingCode &&
                    semester1.TermCode == termCode &&
                    semester1.SchoolYear == schoolYear)
                    return semester1;
                if (semester2.Organization.SisBuildingCode == sisBuildingCode &&
                    semester2.TermCode == termCode &&
                    semester2.SchoolYear == schoolYear)
                    return semester2;
            }

            return null;
        }

        public AcademicSession Read_Parent(AcademicSession child, IConnectable connection)
        {
            WasCalled_ReadParent = true;

            if (_shouldReadReturnData)
            {
                foreach(AcademicSession builderChild in tdBuilder.schoolYear.Children)
                {
                    if (builderChild.TermCode == child.TermCode &&
                        builderChild.SchoolYear == child.SchoolYear)
                        return tdBuilder.schoolYear;
                }

                foreach (AcademicSession builderChild in tdBuilder.semester1.Children)
                {
                    if (builderChild.TermCode == child.TermCode &&
                        builderChild.SchoolYear == child.SchoolYear)
                        return tdBuilder.semester1;
                }

                foreach (AcademicSession builderChild in tdBuilder.semester2.Children)
                {
                    if (builderChild.TermCode == child.TermCode &&
                        builderChild.SchoolYear == child.SchoolYear)
                        return tdBuilder.semester2;
                }
            }

            return null;
        }

        public List<AcademicSession> Read_Children(AcademicSession parent, IConnectable connection)
        {
            WasCalled_ReadChildren = true;

            if (_shouldReadReturnData)
            {
                if (tdBuilder.schoolYear.Id == parent.Id)
                    return tdBuilder.schoolYear.Children;
                if (tdBuilder.semester1.Id == parent.Id)
                    return tdBuilder.semester1.Children;
                if (tdBuilder.semester2.Id == parent.Id)
                    return tdBuilder.semester2.Children;
            }

            return new List<AcademicSession>();
        }

        public List<AcademicSession> Read(AcademicSession academicSession, IConnectable connection)
        {
            WasCalled_Read = true;

            List<AcademicSession> results = new List<AcademicSession>();

            if (_shouldReadReturnData)
            {
                if (tdBuilder.schoolYear.Organization.Id == academicSession.Organization.Id &&
                    tdBuilder.schoolYear.SchoolYear == academicSession.SchoolYear &&
                    tdBuilder.schoolYear.TermCode == academicSession.TermCode)
                    results.Add(tdBuilder.schoolYear);
                if (tdBuilder.semester1.Organization.Id == academicSession.Organization.Id &&
                    tdBuilder.semester1.SchoolYear == academicSession.SchoolYear &&
                    tdBuilder.semester1.TermCode == academicSession.TermCode)
                    results.Add(tdBuilder.semester1);
                if (tdBuilder.semester2.Organization.Id == academicSession.Organization.Id &&
                    tdBuilder.semester2.SchoolYear == academicSession.SchoolYear &&
                    tdBuilder.semester2.TermCode == academicSession.TermCode)
                    results.Add(tdBuilder.semester2);
            }

            return results;
        }

        public List<AcademicSession> Read(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            WasCalled_ReadWithParentId = true;

            List<AcademicSession> results = new List<AcademicSession>();

            if (_shouldReadReturnData)
            {
                if (tdBuilder.schoolYear.Name == academicSession.Name)
                    results.Add(tdBuilder.schoolYear);
                if (tdBuilder.semester1.Name == academicSession.Name)
                    results.Add(tdBuilder.semester1);
                if (tdBuilder.semester2.Name == academicSession.Name)
                    results.Add(tdBuilder.semester2);
            }

            return results;
        }

        public void Update(AcademicSession academicSession, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public void Update(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            WasCalled_UpdateWithParentId = true;
        }

        public AcademicSession Write(AcademicSession academicSession, IConnectable connection)
        {
            WasCalled_Write = true;

            academicSession.Id = 100;

            return academicSession;
        }

        public AcademicSession Write(AcademicSession academicSession, int parentId, IConnectable connection)
        {
            WasCalled_WriteWithParentId = true;

            academicSession.Id = 100;

            return academicSession;
        }
    }
}
