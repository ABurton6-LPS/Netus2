using Moq;
using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class PhoneNumberDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        PhoneNumberDaoImpl phoneNumberDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            phoneNumberDaoImpl = new PhoneNumberDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void Delete_WhileNotUsingPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM phone_number " +
                "WHERE 1=1 " +
                "AND phone_number_id = " + tdBuilder.phoneNumber_Teacher.Id + " " +
                "AND person_id IS NULL " +
                "AND phone_number = '" + tdBuilder.phoneNumber_Teacher.PhoneNumberValue + "' " +
                "AND is_primary_id = " + tdBuilder.phoneNumber_Teacher.IsPrimary.Id + " " +
                "AND enum_phone_id = " + tdBuilder.phoneNumber_Teacher.PhoneType.Id + " ";

            phoneNumberDaoImpl.Delete(tdBuilder.phoneNumber_Teacher, _netus2DbConnection);
        }

        [TestCase]
        public void Delete_WhileUsingPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM phone_number " +
                "WHERE 1=1 " +
                "AND phone_number_id = " + tdBuilder.phoneNumber_Teacher.Id + " " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND phone_number = '" + tdBuilder.phoneNumber_Teacher.PhoneNumberValue + "' " +
                "AND is_primary_id = " + tdBuilder.phoneNumber_Teacher.IsPrimary.Id + " " +
                "AND enum_phone_id = " + tdBuilder.phoneNumber_Teacher.PhoneType.Id + " ";

            phoneNumberDaoImpl.Delete(tdBuilder.phoneNumber_Teacher, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileNotUsingPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM phone_number " +
                "WHERE 1=1 " +
                "AND phone_number_id = " + tdBuilder.phoneNumber_Teacher.Id + " ";

            phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhilePhoneNumberIsNull_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM phone_number " +
                "WHERE person_id = " + tdBuilder.teacher.Id;

            phoneNumberDaoImpl.Read(null, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileUsingPhoneNumberId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM phone_number " +
                "WHERE 1=1 " +
                "AND phone_number_id = " + tdBuilder.phoneNumber_Teacher.Id + " ";

            phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileNotUsingPhoneNumberId_ShouldUseExpectedSql()
        {
            tdBuilder.phoneNumber_Teacher.Id = -1;

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM phone_number " +
                "WHERE 1=1 " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND phone_number = '" + tdBuilder.phoneNumber_Teacher.PhoneNumberValue + "' " +
                "AND is_primary_id = " + tdBuilder.phoneNumber_Teacher.IsPrimary.Id + " " +
                "AND enum_phone_id = " + tdBuilder.phoneNumber_Teacher.PhoneType.Id + " ";

            phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhileNotUsingPersonIdAndNotFindingRecord_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO phone_number (" +
                "person_id, " +
                "phone_number, " +
                "is_primary_id, " +
                "enum_phone_id, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "NULL, " +
                "'" + tdBuilder.phoneNumber_Teacher.PhoneNumberValue + "', " +
                tdBuilder.phoneNumber_Teacher.IsPrimary.Id + ", " +
                tdBuilder.phoneNumber_Teacher.PhoneType.Id + ", " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            phoneNumberDaoImpl.Update(tdBuilder.phoneNumber_Teacher, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhileNotUsingPersonIdAndFindingRecord_ShouldUseExpectedSql()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapPhoneNumber(tdBuilder.phoneNumber_Teacher, -1));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE phone_number SET " +
                "person_id = NULL, " +
                "phone_number = '" + tdBuilder.phoneNumber_Teacher.PhoneNumberValue + "', " +
                "is_primary_id = " + tdBuilder.phoneNumber_Teacher.IsPrimary.Id + ", " +
                "enum_phone_id = " + tdBuilder.phoneNumber_Teacher.PhoneType.Id + ", " +
                "changed = dbo.CURRENT_DATETIME(), " +
                "changed_by = 'Netus2' " +
                "WHERE phone_number_id = " + tdBuilder.phoneNumber_Teacher.Id;

            phoneNumberDaoImpl.Update(tdBuilder.phoneNumber_Teacher, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhileUsingPersonIdAndNotFindingRecord_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO phone_number (" +
                "person_id, " +
                "phone_number, " +
                "is_primary_id, " +
                "enum_phone_id, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                tdBuilder.teacher.Id + ", " +
                "'" + tdBuilder.phoneNumber_Teacher.PhoneNumberValue + "', " +
                tdBuilder.phoneNumber_Teacher.IsPrimary.Id + ", " +
                tdBuilder.phoneNumber_Teacher.PhoneType.Id + ", " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            phoneNumberDaoImpl.Update(tdBuilder.phoneNumber_Teacher, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhileUsingPersonIdAndFindingRecord_ShouldUseExpectedSql()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapPhoneNumber(tdBuilder.phoneNumber_Teacher, tdBuilder.teacher.Id));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE phone_number SET " +
                "person_id = " + tdBuilder.teacher.Id + ", " +
                "phone_number = '" + tdBuilder.phoneNumber_Teacher.PhoneNumberValue + "', " +
                "is_primary_id = " + tdBuilder.phoneNumber_Teacher.IsPrimary.Id + ", " +
                "enum_phone_id = " + tdBuilder.phoneNumber_Teacher.PhoneType.Id + ", " +
                "changed = dbo.CURRENT_DATETIME(), " +
                "changed_by = 'Netus2' " +
                "WHERE phone_number_id = " + tdBuilder.phoneNumber_Teacher.Id;

            phoneNumberDaoImpl.Update(tdBuilder.phoneNumber_Teacher, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_WhileNotUsingPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO phone_number (" +
                "person_id, " +
                "phone_number, " +
                "is_primary_id, " +
                "enum_phone_id, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "NULL, " +
                "'" + tdBuilder.phoneNumber_Teacher.PhoneNumberValue + "', " +
                tdBuilder.phoneNumber_Teacher.IsPrimary.Id + ", " +
                tdBuilder.phoneNumber_Teacher.PhoneType.Id + ", " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            phoneNumberDaoImpl.Write(tdBuilder.phoneNumber_Teacher, _netus2DbConnection);
        }

        [TestCase]
        public void Write_WhileUsingPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO phone_number (" +
                "person_id, " +
                "phone_number, " +
                "is_primary_id, " +
                "enum_phone_id, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                tdBuilder.teacher.Id + ", " +
                "'" + tdBuilder.phoneNumber_Teacher.PhoneNumberValue + "', " +
                tdBuilder.phoneNumber_Teacher.IsPrimary.Id + ", " +
                tdBuilder.phoneNumber_Teacher.PhoneType.Id + ", " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            phoneNumberDaoImpl.Write(tdBuilder.phoneNumber_Teacher, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TearDown]
        public void TearDown()
        {
            _netus2DbConnection.mockReader = new Mock<IDataReader>();
        }

        private void SetMockReaderWithTestData(List<DataRow> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 9);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count]["phone_number_id"];
                        values[1] = tstDataSet[count]["person_id"];
                        values[2] = tstDataSet[count]["phone_number"];
                        values[3] = tstDataSet[count]["is_primary_id"];
                        values[4] = tstDataSet[count]["enum_phone_id"];
                        values[5] = tstDataSet[count]["created"];
                        values[6] = tstDataSet[count]["created_by"];
                        values[7] = tstDataSet[count]["changed"];
                        values[8] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "phone_number_id");
            reader.Setup(x => x.GetOrdinal("phone_number_id"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "person_id");
            reader.Setup(x => x.GetOrdinal("person_id"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "phone_number");
            reader.Setup(x => x.GetOrdinal("phone_number"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "is_primary_id");
            reader.Setup(x => x.GetOrdinal("is_primary_id"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "enum_phone_id");
            reader.Setup(x => x.GetOrdinal("enum_phone_id"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "created");
            reader.Setup(x => x.GetOrdinal("created"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "created_by");
            reader.Setup(x => x.GetOrdinal("created_by"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(7))
                .Returns(() => "changed");
            reader.Setup(x => x.GetOrdinal("changed"))
                .Returns(() => 7);
            reader.Setup(x => x.GetFieldType(7))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(8))
                .Returns(() => "changed_by");
            reader.Setup(x => x.GetOrdinal("changed_by"))
                .Returns(() => 8);
            reader.Setup(x => x.GetFieldType(8))
                .Returns(() => typeof(string));

            _netus2DbConnection.mockReader = reader;
        }
    }
}
