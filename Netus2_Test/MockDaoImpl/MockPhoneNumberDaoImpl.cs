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
        public bool WasCalled_Read = false;
        public bool WasCalled_ReadWithPhoneNumberId = false;
        public bool WasCalled_ReadWithPhoneNumberValue = false;
        public bool WasCalled_Update = false;
        public bool WasCalled_Write = false;
        public bool _shouldReadReturnData = false;

        public void SetTaskId(int taskId)
        {
            //Do Nothing
        }

        public int? GetTaskId()
        {
            return null;
        }

        public void Delete(PhoneNumber phoneNumber, IConnectable connection)
        {
            WasCalled_Delete = true;
        }

        public List<PhoneNumber> Read(PhoneNumber phoneNumber, IConnectable connection)
        {
            WasCalled_Read = true;

            List<PhoneNumber> result = new List<PhoneNumber>();

            if(_shouldReadReturnData)
            {
                if (phoneNumber.PhoneNumberValue == tdBuilder.teacher.PhoneNumbers[0].PhoneNumberValue)
                    result.Add(tdBuilder.teacher.PhoneNumbers[0]);
                if (phoneNumber.PhoneNumberValue == tdBuilder.student.PhoneNumbers[0].PhoneNumberValue)
                    result.Add(tdBuilder.student.PhoneNumbers[0]);
            }

            return result;
        }

        public PhoneNumber Read_WithPhoneNumberId(int phoneNumberId, IConnectable connection)
        {
            WasCalled_ReadWithPhoneNumberId = true;

            if (_shouldReadReturnData)
            {
                if (phoneNumberId == tdBuilder.teacher.PhoneNumbers[0].Id)
                    return tdBuilder.teacher.PhoneNumbers[0];
                else if (phoneNumberId == tdBuilder.student.PhoneNumbers[0].Id)
                    return tdBuilder.student.PhoneNumbers[0];
            }
            
            return null;
        }

        public void Update(PhoneNumber phoneNumber, IConnectable connection)
        {
            WasCalled_Update = true;
        }

        public PhoneNumber Write(PhoneNumber phoneNumber, IConnectable connection)
        {
            WasCalled_Write = true;

            phoneNumber.Id = 45;
            return phoneNumber;
        }

        public PhoneNumber Read_WithPhoneNumberValue(string phoneNumberValue, IConnectable connection)
        {
            WasCalled_ReadWithPhoneNumberValue = true;

            if(_shouldReadReturnData)
            {
                if (phoneNumberValue == tdBuilder.teacher.PhoneNumbers[0].PhoneNumberValue)
                    return tdBuilder.teacher.PhoneNumbers[0];
                else if (phoneNumberValue == tdBuilder.student.PhoneNumbers[0].PhoneNumberValue)
                    return tdBuilder.student.PhoneNumbers[0];
            }

            return null;
        }

        public MockPhoneNumberDaoImpl(TestDataBuilder tdBuilder)
        {
            this.tdBuilder = tdBuilder;
        }
    }
}
