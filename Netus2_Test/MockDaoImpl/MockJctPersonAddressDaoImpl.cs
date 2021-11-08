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
        public bool WasCalled_ReadAllWithAddressId = false;
        public bool WasCalled_ReadAllWithPersonId = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_Write = false;
        public bool WasCalled_ReadAddressIsNotImTempTable = false;
        public bool WasCalled_WriteTempTable = false;
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

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonAddress().NewRow();
            if(personId == tdBuilder.teacher.Id)
            {
                row["person_id"] = tdBuilder.teacher.Id;
                row["address_id"] = tdBuilder.teacher.Addresses[0].Id;
                row["is_primary_id"] = tdBuilder.teacher.Addresses[0].IsPrimary.Id;
                row["enum_address_id"] = tdBuilder.teacher.Addresses[0].AddressType.Id;
            }
            else if(personId == tdBuilder.student.Id)
            {
                row["person_id"] = tdBuilder.student.Id;
                row["address_id"] = tdBuilder.student.Addresses[0].Id;
                row["is_primary_id"] = tdBuilder.student.Addresses[0].IsPrimary.Id;
                row["enum_address_id"] = tdBuilder.student.Addresses[0].AddressType.Id;
            }

            if (_shouldReadReturnData)
                return row;
            else 
                return null;
        }

        public List<DataRow> Read_AllAddressIsNotInTempTable(IConnectable connection)
        {
            WasCalled_ReadAddressIsNotImTempTable = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonAddress().NewRow();
            row["person_id"] = tdBuilder.student.Id;
            row["address_id"] = tdBuilder.student.Addresses[0].Id;
            row["is_primary_id"] = tdBuilder.student.Addresses[0].IsPrimary.Id;

            List<DataRow> results = new List<DataRow>();
            results.Add(row);

            if (_shouldReadReturnData)
                return results;
            else
                return null;
        }

        public List<DataRow> Read_WithAllAddressId(int addressId, IConnectable connection)
        {
            WasCalled_ReadAllWithAddressId = true;

            List<DataRow> list = new List<DataRow>();
            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonAddress().NewRow();
            row["person_id"] = tdBuilder.student.Id;
            row["address_id"] = addressId;
            row["is_primary_id"] = tdBuilder.student.Addresses[0].IsPrimary.Id;

            if (_shouldReadReturnData)
                list.Add(row);

            return list;
        }

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadAllWithPersonId = true;

            List<DataRow> list = new List<DataRow>();
            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonAddress().NewRow();
            row["person_id"] = personId;
            row["address_id"] = tdBuilder.student.Addresses[0].Id;
            row["is_primary_id"] = tdBuilder.student.Addresses[0].IsPrimary.Id;

            if (_shouldReadReturnData)
                list.Add(row);

            return list;
        }

        public void Update(int personId, int addressId, int isPrimaryId, int enumAddressId, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public DataRow Write(int personId, int addressId, int isPrimary, int enumAddressId, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonAddress().NewRow();
            row["person_id"] = personId;
            row["address_id"] = addressId;
            row["is_primary_id"] = isPrimary;
            row["enum_address_id"] = enumAddressId;

            return row;
        }

        public void Write_ToTempTable(int personId, int addressId, IConnectable connection)
        {
            WasCalled_WriteTempTable = true;
        }
    }
}
