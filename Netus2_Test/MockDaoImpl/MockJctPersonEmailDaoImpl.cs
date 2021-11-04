using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctPersonEmailDaoImpl : IJctPersonEmailDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadAllWithEmailId = false;
        public bool WasCalled_ReadAllWithPersonId = false;
        public bool WasCalled_Write = false;
        public bool WasCalled_ReadEmailIsNotImTempTable = false;
        public bool WasCalled_WriteTempTable = false;
        public bool _shouldReadReturnData = false;

        public MockJctPersonEmailDaoImpl (TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(int personId, int emailId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public DataRow Read(int personId, int emailId, IConnectable connection)
        {
            WasCalled_Read = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonEmail().NewRow();
            row["person_id"] = tdBuilder.student.Id;
            row["email_id"] = tdBuilder.student.Emails[0].Id;
            row["is_primary_id"] = tdBuilder.student.Emails[0].IsPrimary.Id;

            if (_shouldReadReturnData)
                return row;
            else 
                return null;
        }

        public List<DataRow> Read_AllEmailIsNotInTempTable(IConnectable connection)
        {
            WasCalled_ReadEmailIsNotImTempTable = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonEmail().NewRow();
            row["person_id"] = tdBuilder.student.Id;
            row["email_id"] = tdBuilder.student.Emails[0].Id;
            row["is_primary_id"] = tdBuilder.student.Emails[0].IsPrimary.Id;

            List<DataRow> results = new List<DataRow>();
            results.Add(row);

            if (_shouldReadReturnData)
                return results;
            else
                return null;
        }

        public List<DataRow> Read_WithAllEmailId(int emailId, IConnectable connection)
        {
            WasCalled_ReadAllWithEmailId = true;

            List<DataRow> list = new List<DataRow>();
            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonEmail().NewRow();
            row["person_id"] = tdBuilder.student.Id;
            row["email_id"] = emailId;
            row["is_primary_id"] = tdBuilder.student.Emails[0].IsPrimary.Id;

            if (_shouldReadReturnData)
                list.Add(row);

            return list;
        }

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadAllWithPersonId = true;

            List<DataRow> list = new List<DataRow>();
            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonEmail().NewRow();
            row["person_id"] = personId;
            row["email_id"] = tdBuilder.student.Emails[0].Id;
            row["is_primary_id"] = tdBuilder.student.Emails[0].IsPrimary.Id;

            if (_shouldReadReturnData)
                list.Add(row);

            return list;
        }

        public DataRow Write(int personId, int emailId, int isPrimary, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonEmail().NewRow();
            row["person_id"] = personId;
            row["email_id"] = emailId;
            row["is_primary_id"] = isPrimary;

            return row;
        }

        public void Write_ToTempTable(int personId, int emailId, IConnectable connection)
        {
            WasCalled_WriteTempTable = true;
        }
    }
}
