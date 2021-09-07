using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctClassPeriodDaoImpl : IJctClassPeriodDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadWithClassIdAndPeriodId = false;
        public bool WasCalled_ReadWithClassId = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockJctClassPeriodDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(int classId, int periodId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<DataRow> Read(int classId, IConnectable connection)
        {
            WasCalled_ReadWithClassId = true;

            List<DataRow> returnData = new List<DataRow>();

            if(_shouldReadReturnData)
                for(int i = 0; i < tdBuilder.classEnrolled.Periods.Count; i++)
                {
                    DataRow row = new DataTableFactory().Dt_Netus2_JctClassPeriod.NewRow();
                    row["class_id"] = classId;
                    row["enum_period_id"] = tdBuilder.classEnrolled.Periods[i].Id;
                    returnData.Add(row);
                }

            return returnData;
        }

        public DataRow Read(int classId, int periodId, IConnectable connection)
        {
            WasCalled_ReadWithClassIdAndPeriodId = true;

            if (_shouldReadReturnData)
            {
                DataRow row = new DataTableFactory().Dt_Netus2_JctClassPeriod.NewRow();
                row["class_id"] = classId;
                row["enum_period_id"] = tdBuilder.classEnrolled.Periods[0].Id;
                return row;
            }
            else
                return null;
        }

        public DataRow Write(int classId, int periodId, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow row = new DataTableFactory().Dt_Netus2_JctClassPeriod.NewRow();
            row["class_id"] = classId;
            row["enum_period_id"] = tdBuilder.classEnrolled.Periods[0].Id;
            return row;
        }
    }
}
