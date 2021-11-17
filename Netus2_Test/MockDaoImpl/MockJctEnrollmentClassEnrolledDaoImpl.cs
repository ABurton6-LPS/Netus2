using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctEnrollmentClassEnrolledDaoImpl : IJctEnrollmentClassEnrolledDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadAllWithClassEnrolledId = false;
        public bool WasCalled_ReadAllWithEnrollmentId = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockJctEnrollmentClassEnrolledDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(int enrollmentId, int classEnrolledId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public DataRow Read(int enrollmentId, int classEnrolledId, IConnectable connection)
        {
            WasCalled_Read = true;

            if(_shouldReadReturnData)
            {
                DataRow row = DataTableFactory.CreateDataTable_Netus2_JctEnrollmentClassEnrolled().NewRow();
                row["enrollment_id"] = enrollmentId;
                row["class_enrolled_id"] = classEnrolledId;
                if (tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentStartDate != null)
                    row["enrollment_start_date"] = tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentStartDate;
                else
                    row["enrollment_start_date"] = DBNull.Value;

                if (tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentEndDate != null)
                    row["enrollment_end_date"] = tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentEndDate;
                else
                    row["enrollment_start_date"] = DBNull.Value;
                return row;
            }

            return null;
        }

        public List<DataRow> Read_AllWithClassEnrolledId(int classEnrolledId, IConnectable connection)
        {
            WasCalled_ReadAllWithClassEnrolledId = true;

            List<DataRow> result = new List<DataRow>();

            if (_shouldReadReturnData == true)
            {
                if (classEnrolledId == tdBuilder.classEnrolled.Id)
                {
                    DataRow row = DataTableFactory.CreateDataTable_Netus2_JctEnrollmentClassEnrolled().NewRow();
                    row["enrollment_id"] = tdBuilder.enrollment.Id;
                    row["class_enrolled_id"] = tdBuilder.enrollment.ClassesEnrolled[0].Id;
                    if (tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentStartDate != null)
                        row["enrollment_start_date"] = tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentStartDate;
                    else
                        row["enrollment_start_date"] = DBNull.Value;

                    if (tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentEndDate != null)
                        row["enrollment_end_date"] = tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentEndDate;
                    else
                        row["enrollment_start_date"] = DBNull.Value;
                    result.Add(row);
                }
            }

            return result;
        }

        public List<DataRow> Read_AllWithEnrollmentId(int enrollmentId, IConnectable connection)
        {
            WasCalled_ReadAllWithEnrollmentId = true;

            List<DataRow> result = new List<DataRow>();

            if (_shouldReadReturnData)
            {
                if (enrollmentId == tdBuilder.enrollment.Id)
                {
                    DataRow row = DataTableFactory.CreateDataTable_Netus2_JctEnrollmentClassEnrolled().NewRow();
                    row["enrollment_id"] = tdBuilder.enrollment.Id;
                    row["class_enrolled_id"] = tdBuilder.enrollment.ClassesEnrolled[0].Id;

                    if (tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentStartDate != null)
                        row["enrollment_start_date"] = tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentStartDate;
                    else
                        row["enrollment_start_date"] = DBNull.Value;

                    if (tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentEndDate != null)
                        row["enrollment_end_date"] = tdBuilder.enrollment.ClassesEnrolled[0].EnrollmentEndDate;
                    else
                        row["enrollment_start_date"] = DBNull.Value;

                    result.Add(row);
                }

            }

            return result;
        }

        public void Update(int enrollmentId, int classEnrolledId, DateTime? startDate, DateTime? endDate, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public DataRow Write(int enrollmentId, int classEnrolledId, DateTime? startDate, DateTime? endDate, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctEnrollmentClassEnrolled().NewRow();
            row["enrollment_id"] = tdBuilder.enrollment.Id;
            row["class_enrolled_id"] = tdBuilder.enrollment.ClassesEnrolled[0].Id;

            if (startDate != null)
                row["enrollment_start_date"] = startDate;
            else
                row["enrollment_start_date"] = DBNull.Value;

            if (endDate != null)
                row["enrollment_end_date"] = endDate;
            else
                row["enrollment_start_date"] = DBNull.Value;

            return row;
        }
    }
}
