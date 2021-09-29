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
        public void Delete_ShouldUseExpectedSql()
        {
            DaoImplFactory.MockMarkDaoImpl = new MockMarkDaoImpl(tdBuilder);

            _netus2DbConnection.expectedNonQuerySql =
                "DELETE FROM lineitem " +
                "WHERE 1=1 " +
                "AND lineitem_id = " + tdBuilder.lineItem.Id + " " +
                "AND name = '" + tdBuilder.lineItem.Name + "' " +
                "AND descript = '" + tdBuilder.lineItem.Descript + "' " +
                "AND assign_date = '" + tdBuilder.lineItem.AssignDate + "' " +
                "AND due_date = '" + tdBuilder.lineItem.DueDate + "' " +
                "AND class_id = " + tdBuilder.lineItem.ClassAssigned.Id + " " +
                "AND enum_category_id = " + tdBuilder.lineItem.Category.Id + " " +
                "AND markValueMin = " + tdBuilder.lineItem.MarkValueMin + " " +
                "AND markValueMax = " + tdBuilder.lineItem.MarkValueMax + " ";

            lineItemDaoImpl.Delete(tdBuilder.lineItem, _netus2DbConnection);
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
        public void Read_WhileUsingLineItemId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM lineItem " +
                "WHERE 1=1 " +
                "AND lineItem_id = " + tdBuilder.lineItem.Id + " ";

            lineItemDaoImpl.Read(tdBuilder.lineItem, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileNotUsingLineItemId_ShouldUseExpectedSql()
        {
            tdBuilder.lineItem.Id = -1;

            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM lineItem " +
                "WHERE 1=1 " +
                "AND name = '" + tdBuilder.lineItem.Name + "' " +
                "AND descript = '" + tdBuilder.lineItem.Descript + "' " +
                "AND assign_date = '" + tdBuilder.lineItem.AssignDate + "' " +
                "AND due_date = '" + tdBuilder.lineItem.DueDate + "' " +
                "AND class_id = " + tdBuilder.lineItem.ClassAssigned.Id + " " +
                "AND enum_category_id = " + tdBuilder.lineItem.Category.Id + " " +
                "AND markValueMin = " + tdBuilder.lineItem.MarkValueMin + " " +
                "AND markValueMax = " + tdBuilder.lineItem.MarkValueMax + " ";

            lineItemDaoImpl.Read(tdBuilder.lineItem, _netus2DbConnection);
        }

        [TestCase]
        public void Read_WhileOnlyUsingLineItemId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM lineItem WHERE lineItem_id = " + tdBuilder.lineItem.Id;

            lineItemDaoImpl.Read(tdBuilder.lineItem.Id, _netus2DbConnection);
        }

        [TestCase]
        public void ReadWithClassEnrolledId_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedReaderSql =
                "SELECT * FROM lineitem WHERE class_id = " + tdBuilder.lineItem.ClassAssigned.Id;

            lineItemDaoImpl.Read_WithClassEnrolledId(tdBuilder.lineItem.ClassAssigned.Id, _netus2DbConnection);
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

            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_ReadWithClassId);
        }

        [TestCase]
        public void Update_WhileRecordIsNotFound_ShouldCallExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO lineItem (" +
                "name, " +
                "descript, " +
                "assign_date, " +
                "due_date, " +
                "class_id, " +
                "enum_category_id, " +
                "markValueMin, " +
                "markValueMax, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "'" + tdBuilder.lineItem.Name + "', " +
                "'" + tdBuilder.lineItem.Descript + "', " +
                "'" + tdBuilder.lineItem.AssignDate + "', " +
                "'" + tdBuilder.lineItem.DueDate + "', " +
                tdBuilder.lineItem.ClassAssigned.Id + ", " +
                tdBuilder.lineItem.Category.Id + ", " +
                tdBuilder.lineItem.MarkValueMin + ", " +
                tdBuilder.lineItem.MarkValueMax + ", " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            lineItemDaoImpl.Update(tdBuilder.lineItem, _netus2DbConnection);
        }

        [TestCase]
        public void Update_WhileRecordIsFound_ShouldUseExpectedSql()
        {
            List<DataRow> tstDataSet = new List<DataRow>();
            tstDataSet.Add(daoObjectMapper.MapLineItem(tdBuilder.lineItem));
            SetMockReaderWithTestData(tstDataSet);

            _netus2DbConnection.expectedNonQuerySql =
                "UPDATE lineItem SET " +
                "name = '" + tdBuilder.lineItem.Name + "', " +
                "descript = '" + tdBuilder.lineItem.Descript + "', " +
                "assign_date = '" + tdBuilder.lineItem.AssignDate + "', " +
                "due_date = '" + tdBuilder.lineItem.DueDate + "', " +
                "class_id = " + tdBuilder.classEnrolled.Id + ", " +
                "enum_category_id = " + tdBuilder.lineItem.Category.Id + ", " +
                "markValueMin = " + tdBuilder.lineItem.MarkValueMin + ", " +
                "markValueMax = " + tdBuilder.lineItem.MarkValueMax + ", " +
                "changed = dbo.CURRENT_DATETIME(), " +
                "changed_by = 'Netus2' " +
                "WHERE lineItem_id = " + tdBuilder.lineItem.Id;

            lineItemDaoImpl.Update(tdBuilder.lineItem, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldUseExpectedSql()
        {
            _netus2DbConnection.expectedNewRecordSql =
                "INSERT INTO lineItem (" +
                "name, " +
                "descript, " +
                "assign_date, " +
                "due_date, " +
                "class_id, " +
                "enum_category_id, " +
                "markValueMin, " +
                "markValueMax, " +
                "created, " +
                "created_by" +
                ") VALUES (" +
                "'" + tdBuilder.lineItem.Name + "', " +
                "'" + tdBuilder.lineItem.Descript + "', " +
                "'" + tdBuilder.lineItem.AssignDate + "', " +
                "'" + tdBuilder.lineItem.DueDate + "', " +
                tdBuilder.lineItem.ClassAssigned.Id + ", " +
                tdBuilder.lineItem.Category.Id + ", " +
                tdBuilder.lineItem.MarkValueMin + ", " +
                tdBuilder.lineItem.MarkValueMax + ", " +
                "dbo.CURRENT_DATETIME(), " +
                "'Netus2')";

            lineItemDaoImpl.Write(tdBuilder.lineItem, _netus2DbConnection);
        }

        [TestCase]
        public void Write_ShouldCallExpectedMethods()
        {
            MockClassEnrolledDaoImpl mockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            DaoImplFactory.MockClassEnrolledDaoImpl = mockClassEnrolledDaoImpl;

            lineItemDaoImpl.Write(tdBuilder.lineItem, _netus2DbConnection);

            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_ReadWithClassId);
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
