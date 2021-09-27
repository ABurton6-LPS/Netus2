using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctClassPersonDaoImpl : IJctClassPersonDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadWithClassIdAndPersonId = false;
        public bool WasCalled_ReadWithClassId = false;
        public bool WasCalled_ReadWithPersonId = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockJctClassPersonDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(int classId, int personId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public DataRow Read(int classId, int personId, IConnectable connection)
        {
            WasCalled_ReadWithClassIdAndPersonId = true;

            DataRow row = DataTableFactory.Dt_Netus2_JctClassPerson.NewRow();
            row["class_id"] = classId;
            row["person_id"] = personId;
            row["enum_role_id"] = tdBuilder.student.Roles[0].Id;
            return row;
        }

        public List<DataRow> Read_WithClassId(int classId, IConnectable connection)
        {
            WasCalled_ReadWithClassId = true;

            List<DataRow> returnData = new List<DataRow>();
            if(_shouldReadReturnData)
            {
                for(int i = 0; i < tdBuilder.classEnrolled.GetStaff().Count; i++)
                {
                    for(int x = 0; x < tdBuilder.classEnrolled.GetStaff()[i].Roles.Count; x++)
                    {
                        DataRow row = DataTableFactory.Dt_Netus2_JctClassPerson.NewRow();
                        row["class_id"] = classId;
                        row["person_id"] = tdBuilder.classEnrolled.GetStaff()[i].Id;
                        row["enum_role_id"] = tdBuilder.classEnrolled.GetStaff()[i].Roles[x].Id;
                        returnData.Add(row);
                    }
                }
            }

            return returnData;
        }

        public List<DataRow> Read_WithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadWithPersonId = true;

            List<DataRow> returnData = new List<DataRow>();
            if (_shouldReadReturnData)
            {
                for (int i = 0; i < tdBuilder.classEnrolled.GetStaff().Count; i++)
                {
                    for (int x = 0; x < tdBuilder.classEnrolled.GetStaff()[i].Roles.Count; x++)
                    {
                        if(tdBuilder.classEnrolled.GetStaff()[i].Id == personId)
                        {
                            DataRow row = DataTableFactory.Dt_Netus2_JctClassPerson.NewRow();
                            row["class_id"] = tdBuilder.classEnrolled.Id;
                            row["person_id"] = tdBuilder.classEnrolled.GetStaff()[i].Id;
                            row["enum_role_id"] = tdBuilder.classEnrolled.GetStaff()[i].Roles[x].Id;
                            returnData.Add(row);
                        }
                    }
                }
            }

            return returnData;
        }

        public DataRow Write(int classId, int personId, int roleId, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow row = DataTableFactory.Dt_Netus2_JctClassPerson.NewRow();
            row["class_id"] = classId;
            row["person_id"] = personId;
            row["enum_role_id"] = roleId;

            return row;
        }
    }
}
