using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctPersonRoleDaoImpl : IJctPersonRoleDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadWithPersonId = false;
        public bool WasCalled_ReadWithRoleId = false;
        public bool WasCalled_Read = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockJctPersonRoleDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(int personId, int roleId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadWithPersonId = true;

            List<DataRow> returnData = new List<DataRow>();

            if (_shouldReadReturnData)
            {
                DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonRole().NewRow();
                row["person_id"] = personId;

                if (personId == tdBuilder.teacher.Id)
                    row["enum_role_id"] = tdBuilder.teacher.Roles[0].Id;
                else
                    row["enum_role_id"] = tdBuilder.student.Roles[0].Id;

                returnData.Add(row);
            }

            return returnData;
        }

        public List<DataRow> Read_AllWithRoleId(int roleIdId, IConnectable connection)
        {
            WasCalled_ReadWithRoleId = true;

            List<DataRow> returnData = new List<DataRow>();

            if (_shouldReadReturnData)
            {
                DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonRole().NewRow();
                row["enum_role_id"] = roleIdId;

                if (roleIdId == tdBuilder.teacher.Roles[0].Id)
                    row["person_id"] = tdBuilder.teacher.Id;
                else
                    row["person_id"] = tdBuilder.student.Id;

                returnData.Add(row);
            }

            return returnData;
        }

        public DataRow Read(int personId, int roleId, IConnectable connection)
        {
            WasCalled_Read = true;

            if (_shouldReadReturnData)
            {
                DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonRole().NewRow();
                row["person_id"] = personId;
                row["enum_role_id"] = roleId;
                return row;
            }
            else
                return null;
        }

        public DataRow Write(int personId, int roleId, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow row = DataTableFactory.CreateDataTable_Netus2_JctPersonRole().NewRow();
            row["person_id"] = personId;
            row["enum_role_id"] = roleId;
            return row;
        }
    }
}
