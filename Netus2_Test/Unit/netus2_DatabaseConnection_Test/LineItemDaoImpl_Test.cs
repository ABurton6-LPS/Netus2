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
    public class LineItemDaoImpl_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _netus2DbConnection;
        LineItemDaoImpl lineItemDaoImpl;
        DaoObjectMapper daoObjectMapper;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;

            tdBuilder = new TestDataBuilder();
            _netus2DbConnection = (MockDatabaseConnection)DbConnectionFactory.GetNetus2Connection();

            lineItemDaoImpl = new LineItemDaoImpl();

            daoObjectMapper = new DaoObjectMapper();
        }

        [TestCase]
        public void Delete_ShouldCallExpectedMethods()
        {
            MockMarkDaoImpl mockMarkDaoImpl = new MockMarkDaoImpl(tdBuilder);
            mockMarkDaoImpl._shouldReadReturnData = true;
            DaoImplFactory.MockMarkDaoImpl = mockMarkDaoImpl;

            lineItemDaoImpl.Delete(tdBuilder.lineItem, _netus2DbConnection);

            Assert.IsTrue(mockMarkDaoImpl.WasCalled_ReadWithLineItemId);
            Assert.IsTrue(mockMarkDaoImpl.WasCalled_Delete);
        }

        [TestCase]
        public void Read_ShouldCallExpectedMethods() 
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapLineItem(tdBuilder.lineItem));
            SetMockReaderWithTestData(tstDataSet);

            MockClassEnrolledDaoImpl mockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            DaoImplFactory.MockClassEnrolledDaoImpl = mockClassEnrolledDaoImpl;

            lineItemDaoImpl.Read(tdBuilder.lineItem, _netus2DbConnection);

            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_ReadUsingClassId);
        }

        [TestCase]
        public void Write_ShouldCallExpectedMethods()
        {
            MockClassEnrolledDaoImpl mockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            DaoImplFactory.MockClassEnrolledDaoImpl = mockClassEnrolledDaoImpl;

            lineItemDaoImpl.Write(tdBuilder.lineItem, _netus2DbConnection);

            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_ReadUsingClassId);
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
                .Returns(() => 13);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count]["lineitem_id"];
                        values[1] = tstDataSet[count]["name"];
                        values[2] = tstDataSet[count]["descript"];
                        values[3] = tstDataSet[count]["assign_date"];
                        values[4] = tstDataSet[count]["due_date"];
                        values[5] = tstDataSet[count]["class_id"];
                        values[6] = tstDataSet[count]["enum_category_id"];
                        values[7] = tstDataSet[count]["markValueMin"];
                        values[8] = tstDataSet[count]["markValueMax"];
                        values[9] = tstDataSet[count]["created"];
                        values[10] = tstDataSet[count]["created_by"];
                        values[11] = tstDataSet[count]["changed"];
                        values[12] = tstDataSet[count]["changed_by"];
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "lineitem_id");
            reader.Setup(x => x.GetOrdinal("lineitem_id"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "descript");
            reader.Setup(x => x.GetOrdinal("descript"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "assign_date");
            reader.Setup(x => x.GetOrdinal("assign_date"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "due_date");
            reader.Setup(x => x.GetOrdinal("due_date"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "class_id");
            reader.Setup(x => x.GetOrdinal("class_id"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(6))
                .Returns(() => "enum_category_id");
            reader.Setup(x => x.GetOrdinal("enum_category_id"))
                .Returns(() => 6);
            reader.Setup(x => x.GetFieldType(6))
                .Returns(() => typeof(int));

            reader.Setup(x => x.GetName(7))
                .Returns(() => "markValueMin");
            reader.Setup(x => x.GetOrdinal("markValueMin"))
                .Returns(() => 7);
            reader.Setup(x => x.GetFieldType(7))
                .Returns(() => typeof(double));

            reader.Setup(x => x.GetName(8))
                .Returns(() => "markValueMax");
            reader.Setup(x => x.GetOrdinal("markValueMax"))
                .Returns(() => 8);
            reader.Setup(x => x.GetFieldType(8))
                .Returns(() => typeof(double));

            reader.Setup(x => x.GetName(9))
                .Returns(() => "created");
            reader.Setup(x => x.GetOrdinal("created"))
                .Returns(() => 9);
            reader.Setup(x => x.GetFieldType(9))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(10))
                .Returns(() => "created_by");
            reader.Setup(x => x.GetOrdinal("created_by"))
                .Returns(() => 10);
            reader.Setup(x => x.GetFieldType(10))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(11))
                .Returns(() => "changed");
            reader.Setup(x => x.GetOrdinal("changed"))
                .Returns(() => 11);
            reader.Setup(x => x.GetFieldType(11))
                .Returns(() => typeof(DateTime));

            reader.Setup(x => x.GetName(12))
                .Returns(() => "changed_by");
            reader.Setup(x => x.GetOrdinal("changed_by"))
                .Returns(() => 12);
            reader.Setup(x => x.GetFieldType(12))
                .Returns(() => typeof(string));

            _netus2DbConnection.mockReader = reader;
        }
    }
}
