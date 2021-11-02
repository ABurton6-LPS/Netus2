using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_Test.MockDaoImpl
{
    public class MockPhoneNumberDaoImpl : IPhoneNumberDao
    {
        public TestDataBuilder tdBuilder;
        public bool WasCalled_Delete = false;
        public bool WasCalled_ReadWithPersonId = false;
        public bool WasCalled_ReadWithoutPersonId = false;
        public bool WasCalled_ReadAllWithPersonId = false;
        public bool WasCalled_ReadAllWithPhoneNumberId = false;
        public bool WasCalled_UpdateWithPersonId = false;
        public bool WasCalled_UpdateWithoutPersonId = false;
        public bool WasCalled_WriteWithPersonId = false;
        public bool WasCalled_WriteWithoutPersonId = false;
        public bool _shouldReadReturnData = false;

        public void SetTaskId(int taskId)
        {
            //Do Nothing
        }

        public int? GetTaskId()
        {
            return null;
        }

        public MockPhoneNumberDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }

        public void Delete(PhoneNumber phoneNumber, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<PhoneNumber> Read_AllWithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadAllWithPersonId = true;

            List<PhoneNumber> returnData = new List<PhoneNumber>();

            if (_shouldReadReturnData)
            {
                if (personId == tdBuilder.teacher.Id)
                    returnData.Add(tdBuilder.phoneNumber_Teacher);
                else
                    returnData.Add(tdBuilder.phoneNumber_Student);
            }

            return returnData;
        }

        public PhoneNumber Read_WithPhoneNumberId(int phoneNumberId, IConnectable connection)
        {
            WasCalled_ReadAllWithPhoneNumberId = true;

            if (_shouldReadReturnData)
            {
                if (phoneNumberId == tdBuilder.phoneNumber_Teacher.Id)
                    return tdBuilder.phoneNumber_Teacher;
                else
                    return tdBuilder.phoneNumber_Student;
            }

            return null;
        }

        public List<PhoneNumber> Read(PhoneNumber phoneNumber, int personId, IConnectable connection)
        {
            WasCalled_ReadWithPersonId = true;

            List<PhoneNumber> returnData = new List<PhoneNumber>();

            if (_shouldReadReturnData)
            {
                if (personId == tdBuilder.teacher.Id)
                    returnData.Add(tdBuilder.phoneNumber_Teacher);
                else
                    returnData.Add(tdBuilder.phoneNumber_Student);
            }
            
            return returnData;
        }

        public List<PhoneNumber> Read(PhoneNumber phoneNumber, IConnectable connection)
        {
            WasCalled_ReadWithoutPersonId = true;

            List<PhoneNumber> returnData = new List<PhoneNumber>();

            if(_shouldReadReturnData)
            {
                returnData.Add(tdBuilder.phoneNumber_Student);
                returnData.Add(tdBuilder.phoneNumber_Teacher);
            }

            return returnData;
        }

        public void Update(PhoneNumber phoneNumber, int personId, IConnectable connection)
        {
            WasCalled_UpdateWithPersonId = true;
        }

        public void Update(PhoneNumber phoneNumber, IConnectable connection)
        {
            WasCalled_UpdateWithoutPersonId = true;
        }

        public PhoneNumber Write(PhoneNumber phoneNumber, int personId, IConnectable connection)
        {
            WasCalled_WriteWithPersonId = true;

            if (personId == tdBuilder.teacher.Id)
                return tdBuilder.phoneNumber_Teacher;
            else
                return tdBuilder.phoneNumber_Student;
        }

        public PhoneNumber Write(PhoneNumber phoneNumber, IConnectable connection)
        {
            throw new System.NotImplementedException();
        }
    }
}
