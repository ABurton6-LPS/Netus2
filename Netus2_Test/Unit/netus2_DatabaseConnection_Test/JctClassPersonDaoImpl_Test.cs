using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class JctClassPersonDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        JctClassPersonDaoImpl jctClassPersonDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            jctClassPersonDaoImpl = new JctClassPersonDaoImpl();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM jct_class_person " +
                "WHERE class_id = " + tdBuilder.classEnrolled.Id + " " +
                "AND person_id = " + tdBuilder.student.Id;

            jctClassPersonDaoImpl.Delete(tdBuilder.classEnrolled.Id, tdBuilder.student.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_class_person " +
                "WHERE class_id = " + tdBuilder.classEnrolled.Id + " " +
                "AND person_id = " + tdBuilder.student.Id;

            jctClassPersonDaoImpl.Read(tdBuilder.classEnrolled.Id, tdBuilder.student.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithClassId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_class_person " +
                "WHERE class_id = " + tdBuilder.classEnrolled.Id;

            jctClassPersonDaoImpl.Read_WithClassId(tdBuilder.classEnrolled.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithPersonId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_class_person " +
                "WHERE person_id = " + tdBuilder.student.Id;

            jctClassPersonDaoImpl.Read_WithPersonId(tdBuilder.student.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "INSERT INTO jct_class_person (class_id, person_id, enum_role_id) VALUES (" +
                tdBuilder.classEnrolled.Id + ", " +
                tdBuilder.student.Id + ", " +
                tdBuilder.student.Roles[0].Id + ")";

            jctClassPersonDaoImpl.Write(tdBuilder.classEnrolled.Id, tdBuilder.student.Id, tdBuilder.student.Roles[0].Id, _netus2DbConnection);
        }

        [TearDown]
        public void TearDown()
        {
            _netus2DbConnection.expectedNewRecordSql = null;
            _netus2DbConnection.expectedNonQuerySql = null;
            _netus2DbConnection.expectedReaderSql = null;
        }
    }
}
