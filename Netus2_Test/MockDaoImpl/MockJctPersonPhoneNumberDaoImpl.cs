using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctPersonPhoneNumberDaoImpl : IJctPersonPhoneNumberDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadWithPhoneNumberId = false;
        public bool WasCalled_ReadAllWithPersonId = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockJctPersonPhoneNumberDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(int personId, int phoneNumberId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public DataRow Read(int personId, int phoneNumberId, IConnectable connection)
        {
            WasCalled_Read = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonPhoneNumber().NewRow();
            row["person_id"] = tdBuilder.student.Id;
            row["phone_number_id"] = tdBuilder.phoneNumber_Student.Id;

            if (_shouldReadReturnData)
                return row;
            else 
                return null;
        }

        public List<DataRow> Read_AllWithPhoneNumberId(int phoneNumberId, IConnectable connection)
        {
            WasCalled_ReadWithPhoneNumberId = true;

            List<DataRow> list = new List<DataRow>();
            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonPhoneNumber().NewRow();
            row["person_id"] = tdBuilder.student.Id;
            row["phone_number_id"] = phoneNumberId;

            if (_shouldReadReturnData)
                list.Add(row);

            return list;
        }

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadAllWithPersonId = true;

            List<DataRow> list = new List<DataRow>();
            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonPhoneNumber().NewRow();
            row["person_id"] = personId;
            row["phone_number_id"] = tdBuilder.phoneNumber_Student.Id;

            if (_shouldReadReturnData)
                list.Add(row);

            return list;
        }

        public DataRow Write(int personId, int phoneNumberId, int isPrimaryId, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonPhoneNumber().NewRow();
            row["person_id"] = personId;
            row["phone_number_id"] = phoneNumberId;
            row["is_primary_id"] = tdBuilder.phoneNumber_Student.IsPrimary.Id;

            return row;
        }
    }
}
