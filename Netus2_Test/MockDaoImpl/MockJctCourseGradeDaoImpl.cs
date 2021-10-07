using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctCourseGradeDaoImpl : IJctCourseGradeDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadWithCourseId = false;
        public bool WaasCalled_Update = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockJctCourseGradeDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(int courseId, int gradeId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<DataRow> Read(int courseId, IConnectable connection)
        {
            WasCalled_ReadWithCourseId = true;

            List<DataRow> returnData = new List<DataRow>();

            if (_shouldReadReturnData)
            {
                DataTable dataTable = DataTableFactory.CreateDataTable_Netus2_JctCourseGrade();
                for (int i = 0; i < tdBuilder.spanishCourse.Grades.Count; i++)
                {
                    DataRow row = dataTable.NewRow();
                    row["course_id"] = courseId;
                    row["enum_grade_id"] = tdBuilder.spanishCourse.Grades[i].Id;
                    returnData.Add(row);
                }
            }

            return returnData;
        }

        public DataRow Read(int courseId, int gradeId, IConnectable connection)
        {
            WasCalled_Read = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctCourseGrade().NewRow();
            row["course_id"] = courseId;
            row["enum_grade_id"] = gradeId;

            if (_shouldReadReturnData)
                return row;
            else
                return null;
        }

        public DataRow Write(int courseId, int gradeId, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctCourseGrade().NewRow();
            row["course_id"] = courseId;
            row["enum_grade_id"] = gradeId;

            return row;
        }
    }
}
