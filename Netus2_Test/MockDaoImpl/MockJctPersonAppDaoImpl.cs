using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctPersonAppDaoImpl : IJctPersonAppDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadWithAppId = false;
        public bool WasCalled_ReadWithPersonId = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockJctPersonAppDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(int personId, int appId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public DataRow Read(int personId, int appId, IConnectable connection)
        {
            WasCalled_Read = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonApp().NewRow();
            row["person_id"] = tdBuilder.student.Id;
            row["app_id"] = tdBuilder.application.Id;

            if (_shouldReadReturnData)
                return row;
            else 
                return null;
        }

        public List<DataRow> Read_AllWithAppId(int appId, IConnectable connection)
        {
            WasCalled_ReadWithAppId = true;

            List<DataRow> list = new List<DataRow>();
            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonApp().NewRow();
            row["person_id"] = tdBuilder.student.Id;
            row["app_id"] = appId;

            if (_shouldReadReturnData)
                list.Add(row);

            return list;
        }

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadWithPersonId = true;

            List<DataRow> list = new List<DataRow>();
            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonApp().NewRow();
            row["person_id"] = personId;
            row["app_id"] = tdBuilder.application.Id;

            if (_shouldReadReturnData)
                list.Add(row);

            return list;
        }

        public DataRow Write(int personId, int appId, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonApp().NewRow();
            row["person_id"] = personId;
            row["app_id"] = appId;

            return row;
        }
    }
}
