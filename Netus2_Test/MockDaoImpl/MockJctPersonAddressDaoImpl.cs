using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctPersonAddressDaoImpl : IJctPersonAddressDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadWithAddressId = false;
        public bool WasCalled_ReadWithPersonId = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockJctPersonAddressDaoImpl (TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(int personId, int addressId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public DataRow Read(int personId, int addressId, IConnectable connection)
        {
            WasCalled_Read = true;

            DataRow row = DataTableFactory.Dt_Netus2_JctPersonAddress.NewRow();
            row["person_id"] = tdBuilder.student.Id;
            row["address_id"] = tdBuilder.student.Addresses[0].Id;

            if (_shouldReadReturnData)
                return row;
            else 
                return null;
        }

        public List<DataRow> Read_WithAddressId(int addressId, IConnectable connection)
        {
            WasCalled_ReadWithAddressId = true;

            List<DataRow> list = new List<DataRow>();
            DataRow row = DataTableFactory.Dt_Netus2_JctPersonAddress.NewRow();
            row["person_id"] = tdBuilder.student.Id;
            row["address_id"] = addressId;

            if (_shouldReadReturnData)
                list.Add(row);

            return list;
        }

        public List<DataRow> Read_WithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadWithPersonId = true;

            List<DataRow> list = new List<DataRow>();
            DataRow row = DataTableFactory.Dt_Netus2_JctPersonAddress.NewRow();
            row["person_id"] = personId;
            row["address_id"] = tdBuilder.student.Addresses[0].Id;

            if (_shouldReadReturnData)
                list.Add(row);

            return list;
        }

        public DataRow Write(int personId, int addressId, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow row = DataTableFactory.Dt_Netus2_JctPersonAddress.NewRow();
            row["person_id"] = personId;
            row["address_id"] = addressId;

            return row;
        }
    }
}
