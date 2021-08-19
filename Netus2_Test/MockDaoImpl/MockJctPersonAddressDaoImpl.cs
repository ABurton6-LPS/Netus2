using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

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

        public JctPersonAddressDao Read(int personId, int addressId, IConnectable connection)
        {
            WasCalled_Read = true;

            JctPersonAddressDao jctPersonAddressDao = new JctPersonAddressDao();
            jctPersonAddressDao.person_id = tdBuilder.student.Id;
            jctPersonAddressDao.address_id = tdBuilder.student.Addresses[0].Id;

            if (_shouldReadReturnData)
                return jctPersonAddressDao;
            else 
                return null;
        }

        public List<JctPersonAddressDao> Read_WithAddressId(int addressId, IConnectable connection)
        {
            WasCalled_ReadWithAddressId = true;

            List<JctPersonAddressDao> list = new List<JctPersonAddressDao>();
            JctPersonAddressDao jctPersonAddressDao = new JctPersonAddressDao();
            jctPersonAddressDao.person_id = tdBuilder.student.Id;
            jctPersonAddressDao.address_id = addressId;

            if (_shouldReadReturnData)
                list.Add(jctPersonAddressDao);

            return list;
        }

        public List<JctPersonAddressDao> Read_WithPersonId(int personId, IConnectable connection)
        {
            WasCalled_ReadWithPersonId = true;

            List<JctPersonAddressDao> list = new List<JctPersonAddressDao>();
            JctPersonAddressDao jctPersonAddressDao = new JctPersonAddressDao();
            jctPersonAddressDao.person_id = personId;
            jctPersonAddressDao.address_id = tdBuilder.student.Addresses[0].Id; ;

            if (_shouldReadReturnData)
                list.Add(jctPersonAddressDao);

            return list;
        }

        public JctPersonAddressDao Write(int personId, int addressId, IConnectable connection)
        {
            WasCalled_Write = true;

            JctPersonAddressDao jctPersonAddressDao = new JctPersonAddressDao();
            jctPersonAddressDao.person_id = personId;
            jctPersonAddressDao.address_id = addressId;

            return jctPersonAddressDao;
        }
    }
}
