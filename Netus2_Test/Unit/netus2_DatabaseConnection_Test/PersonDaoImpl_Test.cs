using Moq;
using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_Test.MockDaoImpl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class PersonDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        PersonDaoImpl personDaoImpl;
        DaoObjectMapper daoObjectMapper;

        MockJctPersonPersonDaoImpl mockJctPersonPersonDaoImpl;
        MockJctPersonRoleDaoImpl mockJctPersonRoleDaoImpl;
        MockJctPersonAppDaoImpl mockJctPersonAppDaoImpl;
        MockPhoneNumberDaoImpl mockPhoneNumberDaoImpl;
        MockJctPersonAddressDaoImpl mockJctPersonAddressDaoImpl;
        MockEmploymentSessionDaoImpl mockEmploymentSessionDaoImpl;
        MockUniqueIdentifierDaoImpl mockUniqueIdentifierDaoImpl;
        MockEnrollmentDaoImpl mockEnrollmentDaoImpl;
        MockMarkDaoImpl mockMarkDaoImpl;
        MockJctClassPersonDaoImpl mockJctClassPersonDaoImpl;
        MockApplicationDaoImpl mockApplicationDaoImpl;
        MockAddressDaoImpl mockAddressDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.TestMode = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            personDaoImpl = new PersonDaoImpl();

            daoObjectMapper = new DaoObjectMapper();

            mockJctPersonPersonDaoImpl = new MockJctPersonPersonDaoImpl(tdBuilder);
            DaoImplFactory.MockJctPersonPersonDaoImpl = mockJctPersonPersonDaoImpl;
            mockJctPersonRoleDaoImpl = new MockJctPersonRoleDaoImpl(tdBuilder);
            DaoImplFactory.MockJctPersonRoleDaoImpl = mockJctPersonRoleDaoImpl;
            mockJctPersonAppDaoImpl = new MockJctPersonAppDaoImpl(tdBuilder);
            DaoImplFactory.MockJctPersonAppDaoImpl = mockJctPersonAppDaoImpl;
            mockPhoneNumberDaoImpl = new MockPhoneNumberDaoImpl(tdBuilder);
            DaoImplFactory.MockPhoneNumberDaoImpl = mockPhoneNumberDaoImpl;
            mockJctPersonAddressDaoImpl = new MockJctPersonAddressDaoImpl(tdBuilder);
            DaoImplFactory.MockJctPersonAddressDaoImpl = mockJctPersonAddressDaoImpl;
            mockEmploymentSessionDaoImpl = new MockEmploymentSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockEmploymentSessionDaoImpl = mockEmploymentSessionDaoImpl;
            mockUniqueIdentifierDaoImpl = new MockUniqueIdentifierDaoImpl(tdBuilder);
            DaoImplFactory.MockUniqueIdentifierDaoImpl = mockUniqueIdentifierDaoImpl;
            mockEnrollmentDaoImpl = new MockEnrollmentDaoImpl(tdBuilder);
            DaoImplFactory.MockEnrollmentDaoImpl = mockEnrollmentDaoImpl;
            mockMarkDaoImpl = new MockMarkDaoImpl(tdBuilder);
            DaoImplFactory.MockMarkDaoImpl = mockMarkDaoImpl;
            mockJctClassPersonDaoImpl = new MockJctClassPersonDaoImpl(tdBuilder);
            DaoImplFactory.MockJctClassPersonDaoImpl = mockJctClassPersonDaoImpl;
            mockApplicationDaoImpl = new MockApplicationDaoImpl(tdBuilder);
            DaoImplFactory.MockApplicationDaoImpl = mockApplicationDaoImpl;
            mockAddressDaoImpl = new MockAddressDaoImpl(tdBuilder);
            DaoImplFactory.MockAddressDaoImpl = mockAddressDaoImpl;

            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapPerson(tdBuilder.teacher));
            SetMockReaderWithTestData(tstDataSet);
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM person " +
                "WHERE 1=1 " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND first_name LIKE '" + tdBuilder.teacher.FirstName + "' " +
                "AND middle_name LIKE '" + tdBuilder.teacher.MiddleName + "' " +
                "AND last_name LIKE '" + tdBuilder.teacher.LastName + "' " +
                "AND birth_date = '" + tdBuilder.teacher.BirthDate + "' " +
                "AND enum_gender_id = " + tdBuilder.teacher.Gender.Id + " " +
                "AND enum_ethnic_id = " + tdBuilder.teacher.Ethnic.Id + " " +
                "AND enum_residence_status_id IS NULL " +
                "AND login_name LIKE '" + tdBuilder.teacher.LoginName + "' " +
                "AND login_pw LIKE '" + tdBuilder.teacher.LoginPw + "' ";

            personDaoImpl.Delete(tdBuilder.teacher, _netus2DbConnection);
        }

        [TestCase]
        public void DeleteJctPersonPerson_ShouldCallExpectedMethod()
        {
            personDaoImpl.Delete(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonPersonDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void DeleteJctPersonRole_ShouldCallExpectedMethod()
        {
            personDaoImpl.Delete(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonRoleDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void DeleteJctPersonApp_ShouldCallExpectedMethod()
        {
            personDaoImpl.Delete(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonAppDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void DeletePhoneNumber_ShouldCallExpectedMethod()
        {
            personDaoImpl.Delete(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockPhoneNumberDaoImpl.WasCalled_DeleteWithPersonId);
        }

        [TestCase]
        public void DeleteJctPersonAddress_ShouldCallExpectedMethod()
        {
            personDaoImpl.Delete(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonAddressDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void DeleteEmploymentSession_ShouldCallExpectedMethods()
        {
            mockEmploymentSessionDaoImpl._shouldReadReturnData = true;

            personDaoImpl.Delete(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockEmploymentSessionDaoImpl.WasCalled_ReadWithPersonId);
            Assert.IsTrue(mockEmploymentSessionDaoImpl.WasCalled_DeleteWithPersonId);
        }

        [TestCase]
        public void DeleteUniqueIdentifier_ShouldCallExpectedMethods()
        {
            mockUniqueIdentifierDaoImpl._shouldReadReturnData = true;

            personDaoImpl.Delete(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockUniqueIdentifierDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockUniqueIdentifierDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void DeleteEnrollment_ShouldCallExpectedMethods()
        {
            mockEnrollmentDaoImpl._shouldReadReturnData = true;

            personDaoImpl.Delete(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockEnrollmentDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockEnrollmentDaoImpl.WasCalled_DelteWithPersonId);
        }

        [TestCase]
        public void DeleteMark_ShouldCallExpectedMethods()
        {
            mockMarkDaoImpl._shouldReadReturnData = true;

            personDaoImpl.Delete(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockMarkDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockMarkDaoImpl.WasCalled_DeleteWithPersonId);
        }

        [TestCase]
        public void DeleteJctClassPerson_ShouldCallExpectedMethods()
        {
            mockJctClassPersonDaoImpl._shouldReadReturnData = true;

            personDaoImpl.Delete(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctClassPersonDaoImpl.WasCalled_ReadWithPersonId);
            Assert.IsTrue(mockJctClassPersonDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void ReadUsingPersonId_ShouldUseExpectedSql()
        {

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM " +
                "person WHERE person_id = " + tdBuilder.teacher.Id;

            personDaoImpl.Read_UsingPersonId(tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileUsingPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM " +
                "person WHERE 1=1 " +
                "AND person_id = " + tdBuilder.teacher.Id + " ";

            personDaoImpl.Read(tdBuilder.teacher, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileNotUsingPersonId_ShouldUseExpectedSql()
        {
            tdBuilder.teacher.Id = -1;

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM person " +
                "WHERE 1=1 " +
                "AND first_name LIKE '" + tdBuilder.teacher.FirstName + "' " +
                "AND middle_name LIKE '" + tdBuilder.teacher.MiddleName + "' " +
                "AND last_name LIKE '" + tdBuilder.teacher.LastName + "' " +
                "AND datediff(day, birth_date, '" + tdBuilder.teacher.BirthDate + "') = 0 " +
                "AND enum_gender_id = " + tdBuilder.teacher.Gender.Id + " " +
                "AND enum_ethnic_id = " + tdBuilder.teacher.Ethnic.Id + " " +
                "AND login_name LIKE '" + tdBuilder.teacher.LoginName + "' " +
                "AND login_pw LIKE '" + tdBuilder.teacher.LoginPw + "' ";

            personDaoImpl.Read(tdBuilder.teacher, _netus2DbConnection);
        }

        [TestCase]
        public void ReadJctPersonApp_ShouldCallExpectedMethods()
        {
            mockJctPersonAppDaoImpl._shouldReadReturnData = true;

            personDaoImpl.Read(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockApplicationDaoImpl.WasCalled_ReadUsingAppId);
            Assert.IsTrue(mockJctPersonAppDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void ReadJctPersonRole_ShouldCallExpectedMethod()
        {
            personDaoImpl.Read(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonRoleDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void ReadJctPeronPerson_ShouldCallExpectedMethod()
        {
            personDaoImpl.Read(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonPersonDaoImpl.WasCalled_ReadWithOnePersonId);
        }

        [TestCase]
        public void ReadJctPersonAddress_ShouldCallExpectedMethods()
        {
            mockJctPersonAddressDaoImpl._shouldReadReturnData = true;

            personDaoImpl.Read(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_ReadUsingAddressId);
            Assert.IsTrue(mockJctPersonAddressDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void ReadPhoneNumber_ShouldCallExpectedMethod()
        {
            personDaoImpl.Read(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockPhoneNumberDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void ReadUniqueIdentifier_ShouldCallExpectedMethod()
        {
            personDaoImpl.Read(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockUniqueIdentifierDaoImpl.WasCalled_Read);
        }

        [TestCase]
        public void ReadEmploymentSession_ShouldCallExpectedMethod()
        {
            personDaoImpl.Read(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockEmploymentSessionDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void ReadEnrollment_ShouldCallExpectedMethod()
        {
            personDaoImpl.Read(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockEnrollmentDaoImpl.WasCalled_Read);
        }

        [TestCase]
        public void ReadMark_ShouldCallExpectedMethod()
        {
            personDaoImpl.Read(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockMarkDaoImpl.WasCalled_Read);
        }

        [TestCase]
        public void Update_WhileRecordIsNotFound_ShouldUseExpectedSql()
        {
            _netus2DbConnection.mockReader = new Mock<IDataReader>();

            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO person (" +
                "first_name, " +
                "middle_name, " +
                "last_name, " +
                "birth_date, " +
                "enum_gender_id, " +
                "enum_ethnic_id, " +
                "enum_residence_status_id, " +
                "login_name, " +
                "login_pw, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "'" + tdBuilder.teacher.FirstName + "', " +
                "'" + tdBuilder.teacher.MiddleName + "', " +
                "'" + tdBuilder.teacher.LastName + "', " +
                "'" + tdBuilder.teacher.BirthDate + "', " +
                tdBuilder.teacher.Gender.Id + ", " +
                tdBuilder.teacher.Ethnic.Id + ", " +
                "NULL, " +
                "'" + tdBuilder.teacher.LoginName + "', " +
                "'" + tdBuilder.teacher.LoginPw + "', " +
                "GETDATE(), " +
                "'Netus2')";

            personDaoImpl.Update(tdBuilder.teacher, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhileRecordIsFound_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE person SET " +
                "first_name = '" + tdBuilder.teacher.FirstName + "', " +
                "middle_name = '" + tdBuilder.teacher.MiddleName + "', " +
                "last_name = '" + tdBuilder.teacher.LastName + "', " +
                "birth_date = '" + tdBuilder.teacher.BirthDate + "', " +
                "enum_gender_id = '" + tdBuilder.teacher.Gender.Id + "', " +
                "enum_ethnic_id = '" + tdBuilder.teacher.Ethnic.Id + "', " +
                "enum_residence_status_id = NULL, " +
                "login_name = '" + tdBuilder.teacher.LoginName + "', " +
                "login_pw = '" + tdBuilder.teacher.LoginPw + "', " +
                "changed = GETDATE(), " +
                "changed_by = 'Netus2' " +
                "WHERE person_id = 1";

            personDaoImpl.Update(tdBuilder.teacher, _netus2DbConnection);
        }

        [TestCase]
        public void UpdateEmploymentSession_ShouldCallExpectedMethod()
        {
            personDaoImpl.Update(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockEmploymentSessionDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void UpdateJctPersonRole_ShouldCallExpectedMethod()
        {
            personDaoImpl.Update(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonRoleDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void UpdatePhoneNumber_ShouldCallExpectedMethod()
        {
            personDaoImpl.Update(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockPhoneNumberDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void UpdateJctPersonPerson_ShouldCallExpectedMethod()
        {
            personDaoImpl.Update(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonPersonDaoImpl.WasCalled_ReadWithOnePersonId);
        }

        [TestCase]
        public void UpdateAddress_ShouldCallExpectedMethod()
        {
            personDaoImpl.Update(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockJctPersonAddressDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void UpdateUniqueIdentifier_ShouldCallExpectedMethod()
        {
            personDaoImpl.Update(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockUniqueIdentifierDaoImpl.WasCalled_Read);
        }

        [TestCase]
        public void UpdateApplication_ShouldCallExpectedMethod()
        {
            personDaoImpl.Update(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockApplicationDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockJctPersonAppDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void UpdateEnrollment_ShouldCallExpectedMethod()
        {
            personDaoImpl.Update(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockEnrollmentDaoImpl.WasCalled_Read);
        }

        [TestCase]
        public void UpdateMark_ShouldCallExpectedMethod()
        {
            personDaoImpl.Update(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockMarkDaoImpl.WasCalled_Read);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            tdBuilder.student.Id = -1;

            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO person (" +
                "first_name, " +
                "middle_name, " +
                "last_name, " +
                "birth_date, " +
                "enum_gender_id, " +
                "enum_ethnic_id, " +
                "enum_residence_status_id, " +
                "login_name, " +
                "login_pw, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "'" + tdBuilder.student.FirstName + "', " +
                "'" + tdBuilder.student.MiddleName + "', " +
                "'" + tdBuilder.student.LastName + "', " +
                "'" + tdBuilder.student.BirthDate + "', " +
                tdBuilder.student.Gender.Id + ", " +
                tdBuilder.student.Ethnic.Id + ", " +
                tdBuilder.student.ResidenceStatus.Id + ", " +
                "'" + tdBuilder.student.LoginName + "', " +
                "'" + tdBuilder.student.LoginPw + "', " +
                "GETDATE(), " +
                "'Netus2')";

            personDaoImpl.Write(tdBuilder.student, _netus2DbConnection);
        }

        [TestCase]
        public void Write_UpdateEmploymentSession_ShouldCallExpectedMethod()
        {
            personDaoImpl.Write(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockEmploymentSessionDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void Write_UpdateJctPersonRole_ShouldCallExpectedMethod()
        {
            personDaoImpl.Write(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonRoleDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void Write_UpdatePhoneNumber_ShouldCallExpectedMethod()
        {
            personDaoImpl.Write(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockPhoneNumberDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void Write_UpdateJctPersonPerson_ShouldCallExpectedMethod()
        {
            personDaoImpl.Write(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockJctPersonPersonDaoImpl.WasCalled_ReadWithOnePersonId);
        }

        [TestCase]
        public void Write_UpdateAddress_ShouldCallExpectedMethod()
        {
            personDaoImpl.Write(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockAddressDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockJctPersonAddressDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void Write_UpdateUniqueIdentifier_ShouldCallExpectedMethod()
        {
            personDaoImpl.Write(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockUniqueIdentifierDaoImpl.WasCalled_Read);
        }

        [TestCase]
        public void Write_UpdateApplication_ShouldCallExpectedMethod()
        {
            personDaoImpl.Write(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockApplicationDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockJctPersonAppDaoImpl.WasCalled_ReadWithPersonId);
        }

        [TestCase]
        public void Write_UpdateEnrollment_ShouldCallExpectedMethod()
        {
            personDaoImpl.Write(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockEnrollmentDaoImpl.WasCalled_Read);
        }

        [TestCase]
        public void Write_UpdateMark_ShouldCallExpectedMethod()
        {
            personDaoImpl.Write(tdBuilder.teacher, _netus2DbConnection);

            Assert.IsTrue(mockMarkDaoImpl.WasCalled_Read);
        }

        [TearDown]
        public void TearDown()
        {
            DbConnectionFactory.MockDatabaseConnection = null;
        }

        private void SetMockReaderWithTestData(List<DataRow> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 14);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count]["person_id"];
                        values[1] = tstDataSet[count]["first_name"];
                        values[2] = tstDataSet[count]["middle_name"];
                        values[3] = tstDataSet[count]["last_name"];
                        values[4] = tstDataSet[count]["birth_date"];
                        values[5] = tstDataSet[count]["enum_gender_id"];
                        values[6] = tstDataSet[count]["enum_ethnic_id"];
                        values[7] = tstDataSet[count]["enum_residence_status_id"];
                        values[8] = tstDataSet[count]["login_name"];
                        values[9] = tstDataSet[count]["login_pw"];
                        values[10] = tstDataSet[count]["created"];
                        values[11] = tstDataSet[count]["created_by"];
                        values[12] = tstDataSet[count]["changed"];
                        values[13] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "person_id");
            reader.Setup(x => x.GetOrdinal("person_id"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "first_name");
            reader.Setup(x => x.GetOrdinal("first_name"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "middle_name");
            reader.Setup(x => x.GetOrdinal("middle_name"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "last_name");
            reader.Setup(x => x.GetOrdinal("last_name"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "birth_date");
            reader.Setup(x => x.GetOrdinal("birth_date"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "enum_gender_id");
            reader.Setup(x => x.GetOrdinal("enum_gender_id"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "enum_ethnic_id");
            reader.Setup(x => x.GetOrdinal("enum_ethnic_id"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(7))
                .Returns(() => "enum_residence_status_id");
            reader.Setup(x => x.GetOrdinal("enum_residence_status_id"))
                .Returns(() => 7);
            reader.Setup(x => x.GetFieldType(7))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(8))
                .Returns(() => "login_name");
            reader.Setup(x => x.GetOrdinal("login_name"))
                .Returns(() => 8);
            reader.Setup(x => x.GetFieldType(8))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(9))
                .Returns(() => "login_pw");
            reader.Setup(x => x.GetOrdinal("login_pw"))
                .Returns(() => 9);
            reader.Setup(x => x.GetFieldType(9))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(10))
                .Returns(() => "created");
            reader.Setup(x => x.GetOrdinal("created"))
                .Returns(() => 10);
            reader.Setup(x => x.GetFieldType(10))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(11))
                .Returns(() => "created_by");
            reader.Setup(x => x.GetOrdinal("created_by"))
                .Returns(() => 11);
            reader.Setup(x => x.GetFieldType(11))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(12))
                .Returns(() => "changed");
            reader.Setup(x => x.GetOrdinal("changed"))
                .Returns(() => 12);
            reader.Setup(x => x.GetFieldType(12))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(13))
                .Returns(() => "changed_by");
            reader.Setup(x => x.GetOrdinal("changed_by"))
                .Returns(() => 13);
            reader.Setup(x => x.GetFieldType(13))
                .Returns(() => typeof(string));

            _netus2DbConnection.mockReader = reader;
        }
    }
}
