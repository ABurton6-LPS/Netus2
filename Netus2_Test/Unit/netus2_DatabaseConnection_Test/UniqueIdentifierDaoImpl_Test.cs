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
    public class UniqueIdentifierDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        UniqueIdentifierDaoImpl uniqueIdentifierDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            uniqueIdentifierDaoImpl = new UniqueIdentifierDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM unique_identifier " +
                "WHERE 1=1 " +
                "AND unique_identifier_id = " + tdBuilder.uniqueId_Teacher.Id + " " +
                "AND person_id = " + tdBuilder.teacher.Id + " " +
                "AND unique_identifier = '" + tdBuilder.uniqueId_Teacher.Identifier + "' " +
                "AND enum_identifier_id = " + tdBuilder.uniqueId_Teacher.IdentifierType.Id + " " +
                "AND is_active_id = " + tdBuilder.uniqueId_Teacher.IsActive.Id + " ";

            uniqueIdentifierDaoImpl.Delete(tdBuilder.uniqueId_Teacher, tdBuilder.teacher.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileUniqueIdentifierIsNull_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM unique_identifier WHERE person_id = " + tdBuilder.student.Id;

            uniqueIdentifierDaoImpl.Read(null, tdBuilder.student.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileUniqueIdentifierIdIsUsed_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM unique_identifier WHERE 1=1 AND unique_identifier_id = " + tdBuilder.uniqueId_Student.Id + " ";

            uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Student, tdBuilder.student.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileUniqueIdentifierIdIsNotUsed_ShouldUseExpectedSql()
        {
            tdBuilder.uniqueId_Student.Id = -1;

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM unique_identifier " +
                "WHERE 1=1 " +
                "AND person_id = " + tdBuilder.student.Id + " " +
                "AND unique_identifier = '" + tdBuilder.uniqueId_Student.Identifier + "' " +
                "AND enum_identifier_id = " + tdBuilder.uniqueId_Student.IdentifierType.Id + " " +
                "AND is_active_id = " + tdBuilder.uniqueId_Student.IsActive.Id + " ";

            uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Student, tdBuilder.student.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhileRecordNotFound_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO unique_identifier (" +
                "person_id, " +
                "unique_identifier, " +
                "enum_identifier_id, " +
                "is_active_id, created, " +
                "created_by" +
                ") VALUES (" +
                tdBuilder.student.Id + ", " +
                "'" + tdBuilder.uniqueId_Student.Identifier + "', " +
                tdBuilder.uniqueId_Student.IdentifierType.Id + ", " +
                tdBuilder.uniqueId_Student.IsActive.Id + ", " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            uniqueIdentifierDaoImpl.Update(tdBuilder.uniqueId_Student, tdBuilder.student.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhileRecordFound_ShouldUseExpectedSql()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapUniqueIdentifier(tdBuilder.uniqueId_Student, tdBuilder.student.Id));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE unique_identifier SET " +
                "person_id = " + tdBuilder.student.Id + ", " +
                "unique_identifier = '" + tdBuilder.uniqueId_Student.Identifier + "', " +
                "enum_identifier_id = " + tdBuilder.uniqueId_Student.IdentifierType.Id + ", " +
                "is_active_id = " + tdBuilder.uniqueId_Student.IsActive.Id + ", " +
                "changed = dbo.CURRENT_DATETIME(), " +
                "changed_by = 'Netus2' " +
                "WHERE unique_identifier_id = " + tdBuilder.uniqueId_Student.Id;

            uniqueIdentifierDaoImpl.Update(tdBuilder.uniqueId_Student, tdBuilder.student.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO unique_identifier (" +
                "person_id, " +
                "unique_identifier, " +
                "enum_identifier_id, " +
                "is_active_id, created, " +
                "created_by" +
                ") VALUES (" +
                tdBuilder.student.Id + ", " +
                "'" + tdBuilder.uniqueId_Student.Identifier + "', " +
                tdBuilder.uniqueId_Student.IdentifierType.Id + ", " +
                tdBuilder.uniqueId_Student.IsActive.Id + ", " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            uniqueIdentifierDaoImpl.Write(tdBuilder.uniqueId_Student, tdBuilder.student.Id, _netus2DbConnection);
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
                        values[0] = tstDataSet[count]["unique_identifier_id"];
                        values[1] = tstDataSet[count]["person_id"];
                        values[2] = tstDataSet[count]["unique_identifier"];
                        values[3] = tstDataSet[count]["enum_identifier_id"];
                        values[4] = tstDataSet[count]["is_active_id"];
                        values[5] = tstDataSet[count]["created"];
                        values[6] = tstDataSet[count]["created_by"];
                        values[7] = tstDataSet[count]["changed"];
                        values[8] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "unique_identifier_id");
            reader.Setup(x => x.GetOrdinal("unique_identifier_id"))
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
                .Returns(() => "unique_identifier");
            reader.Setup(x => x.GetOrdinal("unique_identifier"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "enum_identifier_id");
            reader.Setup(x => x.GetOrdinal("enum_identifier_id"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "is_active_id");
            reader.Setup(x => x.GetOrdinal("is_active_id"))
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
