using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctClassResourceDaoImpl : IJctClassResourceDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadWithClassIdAndResourceId = false;
        public bool WasCalled_ReadWithClassId = false;
        public bool WasCalled_ReadWithResourceId = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockJctClassResourceDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(int classId, int resourceId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public DataRow Read(int classId, int resourceId, IConnectable connection)
        {
            WasCalled_ReadWithClassIdAndResourceId = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctClassResource().NewRow();
            row["class_id"] = classId;
            row["resource_id"] = resourceId;
            return row;
        }

        public List<DataRow> Read_WithClassId(int classId, IConnectable connection)
        {
            WasCalled_ReadWithClassId = true;

            List<DataRow> returnData = new List<DataRow>();
            if(_shouldReadReturnData)
                for(int i = 0; i < tdBuilder.classEnrolled.Resources.Count; i++)
                {
                    DataRow row = DataTableFactory.CreateDataTable_Netus2_JctClassResource().NewRow();
                    row["class_id"] = classId ;
                    row["resource_id"] = tdBuilder.classEnrolled.Resources[i].Id;
                    returnData.Add(row);
                }

            return returnData;
        }

        public List<DataRow> Read_WithResourceId(int resourceId, IConnectable connection)
        {
            WasCalled_ReadWithResourceId = true;

            List<DataRow> returnData = new List<DataRow>();
            if (_shouldReadReturnData)
            {
                DataRow row = DataTableFactory.CreateDataTable_Netus2_JctClassResource().NewRow();
                row["class_id"] = tdBuilder.classEnrolled.Id;
                row["resource_id"] = resourceId;
                returnData.Add(row);
            }

            return returnData;
        }

        public DataRow Write(int classId, int resourceId, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctClassResource().NewRow();
            row["class_id"] = classId;
            row["resource_id"] = resourceId;

            return row;
        }
    }
}
