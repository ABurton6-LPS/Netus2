using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.MockDaoImpl
{
    public class MockJctPersonPersonDaoImpl : IJctPersonPersonDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadWithOnePersonId = false;
        public bool WasCalled_ReadWithTwoPersonIds = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public MockJctPersonPersonDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(int personOneId, int personTwoId, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<DataRow> Read(int personOneId, IConnectable connection)
        {
            WasCalled_ReadWithOnePersonId = true;

            List<DataRow> returnData = new List<DataRow>();

            if (_shouldReadReturnData)
            {
                DataRow row = new DataTableFactory().Dt_Netus2_JctPersonPerson.NewRow();
                row["person_one_id"] = personOneId;

                if (personOneId == tdBuilder.teacher.Id)
                    row["person_two_id"] = tdBuilder.student.Id;
                else
                    row["person_two_id"] = tdBuilder.teacher.Id;

                returnData.Add(row);
            }
            

            return returnData;
        }

        public DataRow Read(int personOneId, int personTwoId, IConnectable connection)
        {
            WasCalled_ReadWithTwoPersonIds = true;

            if (_shouldReadReturnData)
            {
                DataRow returnData = new DataTableFactory().Dt_Netus2_JctPersonPerson.NewRow();
                returnData["person_one_id"] = personOneId;
                returnData["person_two_id"] = personTwoId;
                return returnData;
            }
            else
                return null;
        }

        public DataRow Write(int personOneId, int personTwoId, IConnectable connection)
        {
            WasCalled_Write = true;

            DataRow returnData = new DataTableFactory().Dt_Netus2_JctPersonPerson.NewRow();
            returnData["person_one_id"] = personOneId;
            returnData["person_two_id"] = personTwoId;
            return returnData;
        }
    }
}
