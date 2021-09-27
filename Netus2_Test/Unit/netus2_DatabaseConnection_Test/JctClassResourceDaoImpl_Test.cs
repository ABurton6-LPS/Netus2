using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using NUnit.Framework;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class JctClassResourceDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        JctClassResourceDaoImpl jctClassResourceDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            jctClassResourceDaoImpl = new JctClassResourceDaoImpl();
        }

        [TestCase]
        public void Delete_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM jct_class_resource " +
                "WHERE class_id = " + tdBuilder.classEnrolled.Id + " " +
                "AND resource_id = " + tdBuilder.resource.Id;

            jctClassResourceDaoImpl.Delete(tdBuilder.classEnrolled.Id, tdBuilder.resource.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Read_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_class_resource " +
                "WHERE class_id = " + tdBuilder.classEnrolled.Id + " " +
                "AND resource_id = " + tdBuilder.resource.Id;

            jctClassResourceDaoImpl.Read(tdBuilder.classEnrolled.Id, tdBuilder.resource.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithClassId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_class_resource " +
                "WHERE class_id = " + tdBuilder.classEnrolled.Id;

            jctClassResourceDaoImpl.Read_WithClassId(tdBuilder.classEnrolled.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithResourceId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM jct_class_resource " +
                "WHERE resource_id = " + tdBuilder.resource.Id;

            jctClassResourceDaoImpl.Read_WithResourceId(tdBuilder.resource.Id, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNonQuerySql =
                "INSERT INTO jct_class_resource (class_id, resource_id) VALUES (" +
                tdBuilder.classEnrolled.Id + ", " +
                tdBuilder.resource.Id + ")";

            jctClassResourceDaoImpl.Write(tdBuilder.classEnrolled.Id, tdBuilder.resource.Id, _netus2DbConnection);
        }
    }
}
