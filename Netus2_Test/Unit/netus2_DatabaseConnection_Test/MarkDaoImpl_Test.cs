using Moq;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.utilityTools;
using Netus2_Test.MockDaoImpl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_Test.Unit.Netus2_DBConnection
{
    public class MarkDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        MarkDaoImpl markDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            markDaoImpl = new MarkDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void Read_ShouldCallExpectedMethods()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapMark(tdBuilder.mark, tdBuilder.student.Id));
            SetMockReaderWithTestData(tstDataSet);

            MockLineItemDaoImpl mockLineItemDaoImpl = new MockLineItemDaoImpl(tdBuilder);
            DaoImplFactory.MockLineItemDaoImpl = mockLineItemDaoImpl;

            markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, _netus2DbConnection);

            Assert.IsTrue(mockLineItemDaoImpl.WasCalled_ReadWithLineItemId);
        }

        [TestCase]
        public void Update_WhileNoRecordFound_ShouldThrowExpectedException()
        {
            string expectedExceptionMessage = 
                "This new Mark must first be saved to the database before " +
                "it can be attached to a student.\n" + tdBuilder.mark.ToString();

            try
            {
                markDaoImpl.Update(tdBuilder.mark, tdBuilder.student.Id, _netus2DbConnection);
                Assert.Fail("No exception was thrown");
            }
            catch(Exception e)
            {
                Assert.IsNotEmpty(e.Message);
                Assert.AreEqual(expectedExceptionMessage, e.Message);
            }
        }

        [TestCase]
        public void Write_ShouldCallExpectedMethods()
        {
            MockLineItemDaoImpl mockLineItemDaoImpl = new MockLineItemDaoImpl(tdBuilder);
            DaoImplFactory.MockLineItemDaoImpl = mockLineItemDaoImpl;

            markDaoImpl.Write(tdBuilder.mark, tdBuilder.student.Id, _netus2DbConnection);

            Assert.IsTrue(mockLineItemDaoImpl.WasCalled_ReadWithLineItemId);
        }

        [TearDown]
        public void TearDown()
        {
            _netus2DbConnection.mockReader = new Mock<IDataReader>();
            _netus2DbConnection.expectedNewRecordSql = null;
            _netus2DbConnection.expectedNonQuerySql = null;
            _netus2DbConnection.expectedReaderSql = null;
        }

        private void SetMockReaderWithTestData(List<DataRow> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 11);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count]["mark_id"];
                        values[1] = tstDataSet[count]["lineitem_id"];
                        values[2] = tstDataSet[count]["person_id"];
                        values[3] = tstDataSet[count]["enum_score_status_id"];
                        values[4] = tstDataSet[count]["score"];
                        values[5] = tstDataSet[count]["score_date"];
                        values[6] = tstDataSet[count]["comment"];
                        values[7] = tstDataSet[count]["created"];
                        values[8] = tstDataSet[count]["created_by"];
                        values[9] = tstDataSet[count]["changed"];
                        values[10] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "mark_id");
            reader.Setup(x => x.GetOrdinal("mark_id"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "lineitem_id");
            reader.Setup(x => x.GetOrdinal("lineitem_id"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "person_id");
            reader.Setup(x => x.GetOrdinal("person_id"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "enum_score_status_id");
            reader.Setup(x => x.GetOrdinal("enum_score_status_id"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "score");
            reader.Setup(x => x.GetOrdinal("score"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(double));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "score_date");
            reader.Setup(x => x.GetOrdinal("score_date"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "comment");
            reader.Setup(x => x.GetOrdinal("comment"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(7))
                .Returns(() => "created");
            reader.Setup(x => x.GetOrdinal("created"))
                .Returns(() => 7);
            reader.Setup(x => x.GetFieldType(7))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(8))
                .Returns(() => "created_by");
            reader.Setup(x => x.GetOrdinal("created_by"))
                .Returns(() => 8);
            reader.Setup(x => x.GetFieldType(8))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(9))
                .Returns(() => "changed");
            reader.Setup(x => x.GetOrdinal("changed"))
                .Returns(() => 9);
            reader.Setup(x => x.GetFieldType(9))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(10))
                .Returns(() => "changed_by");
            reader.Setup(x => x.GetOrdinal("changed_by"))
                .Returns(() => 10);
            reader.Setup(x => x.GetFieldType(10))
                .Returns(() => typeof(string));

            _netus2DbConnection.mockReader = reader;
        }
    }
}
