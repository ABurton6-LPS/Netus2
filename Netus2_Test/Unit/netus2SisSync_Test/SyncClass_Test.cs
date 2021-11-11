using NUnit.Framework;
using Netus2_DatabaseConnection.dbAccess;
using System.Data;
using Moq;
using System.Collections.Generic;
using Netus2SisSync.SyncProcesses.SyncJobs;
using Netus2SisSync.SyncProcesses.SyncTasks.CourseTasks;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_Test.MockDaoImpl;
using Netus2_DatabaseConnection.utilityTools;
using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.enumerations;
using Netus2SisSync.SyncProcesses.SyncTasks.ClassTasks;

namespace Netus2_Test.Unit.SyncProcess
{
    public class SyncClass_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _sisConnection;
        MockCourseDaoImpl mockCourseDaoImpl;
        MockAcademicSessionDaoImpl mockAcademicSessionDaoImpl;
        MockClassEnrolledDaoImpl mockClassEnrolledDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;
            _sisConnection = (MockDatabaseConnection)DbConnectionFactory.GetSisConnection();

            tdBuilder = new TestDataBuilder();
            DaoImplFactory.MockAll = true;
            mockCourseDaoImpl = new MockCourseDaoImpl(tdBuilder);
            DaoImplFactory.MockCourseDaoImpl = mockCourseDaoImpl;
            mockAcademicSessionDaoImpl = new MockAcademicSessionDaoImpl(tdBuilder);
            DaoImplFactory.MockAcademicSessionDaoImpl = mockAcademicSessionDaoImpl;
            mockClassEnrolledDaoImpl = new MockClassEnrolledDaoImpl(tdBuilder);
            DaoImplFactory.MockClassEnrolledDaoImpl = mockClassEnrolledDaoImpl;
        }

        [TestCase]
        public void SisRead_Class_NullData()
        {
            SisClassTestData tstData = new SisClassTestData();
            tstData.Name = null;
            tstData.ClassCode = null;
            tstData.ClassType = null;
            tstData.Room = null;
            tstData.CourseId = null;
            tstData.AcademicSessionId = null;

            List<SisClassTestData> tstDataSet = new List<SisClassTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Class syncJob_Class = new SyncJob_Class();
            syncJob_Class.ReadFromSis();
            DataTable results = syncJob_Class._dtClass;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(emptyString, results.Rows[0]["name"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["class_code"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["enum_class_id"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["room"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["course_id"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["academic_session_id"].ToString());
        }

        [TestCase]
        public void SisRead_Class_TestData()
        {
            SisClassTestData tstData = new SisClassTestData();
            tstData.Name = tdBuilder.classEnrolled.Name;
            tstData.ClassCode = tdBuilder.classEnrolled.ClassCode;
            tstData.ClassType = tdBuilder.classEnrolled.ClassType.Netus2Code;
            tstData.Room = tdBuilder.classEnrolled.Room;
            tstData.CourseId = tdBuilder.spanishCourse.CourseCode;
            tstData.AcademicSessionId =
                tdBuilder.school.SisBuildingCode + "-" +
                tdBuilder.semester1.TrackCode + "-" +
                tdBuilder.semester1.TermCode + "-" +
                tdBuilder.semester1.SchoolYear;

            List<SisClassTestData> tstDataSet = new List<SisClassTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Class syncJob_Class = new SyncJob_Class();
            syncJob_Class.ReadFromSis();
            DataTable results = syncJob_Class._dtClass;

            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstDataSet[0].Name, results.Rows[0]["name"].ToString());
            Assert.AreEqual(tstDataSet[0].ClassCode, results.Rows[0]["class_code"].ToString());
            Assert.AreEqual(tstDataSet[0].ClassType, results.Rows[0]["enum_class_id"].ToString());
            Assert.AreEqual(tstDataSet[0].Room, results.Rows[0]["room"].ToString());
            Assert.AreEqual(tstDataSet[0].CourseId, results.Rows[0]["course_id"].ToString());
            Assert.AreEqual(tstDataSet[0].AcademicSessionId, results.Rows[0]["academic_session_id"].ToString());
        }

        [TestCase]
        public void SyncClass_ShouldWriteNewRecord()
        {
            mockCourseDaoImpl._shouldReadReturnData = true;
            mockAcademicSessionDaoImpl._shouldReadReturnData = true;

            SisClassTestData tstData = new SisClassTestData();
            tstData.Name = tdBuilder.classEnrolled.Name;
            tstData.ClassCode = tdBuilder.classEnrolled.ClassCode;
            tstData.ClassType = tdBuilder.classEnrolled.ClassType.Netus2Code;
            tstData.Room = tdBuilder.classEnrolled.Room;
            tstData.CourseId = tdBuilder.spanishCourse.CourseCode;
            tstData.AcademicSessionId =
                tdBuilder.school.SisBuildingCode + "-" +
                tdBuilder.semester1.TrackCode + "-" +
                tdBuilder.semester1.TermCode + "-" +
                tdBuilder.semester1.SchoolYear;

            List<SisClassTestData> tstDataSet = new List<SisClassTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Class("TestTask",
                new SyncJob_Class())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_ReadUsingCourseCode);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadUsingSisBuildingCodeTermCodeSchoolyear);
            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_Write);
            Assert.IsFalse(mockClassEnrolledDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncClass_ShouldNeitherUpdateNorWriteRecord()
        {
            mockCourseDaoImpl._shouldReadReturnData = true;
            mockAcademicSessionDaoImpl._shouldReadReturnData = true;
            mockClassEnrolledDaoImpl._shouldReadReturnData = true;

            SisClassTestData tstData = new SisClassTestData();
            tstData.Name = tdBuilder.classEnrolled.Name;
            tstData.ClassCode = tdBuilder.classEnrolled.ClassCode;
            tstData.ClassType = tdBuilder.classEnrolled.ClassType.Netus2Code;
            tstData.Room = tdBuilder.classEnrolled.Room;
            tstData.CourseId = tdBuilder.spanishCourse.CourseCode;
            tstData.AcademicSessionId =
                tdBuilder.school.SisBuildingCode + "-" +
                tdBuilder.semester1.TrackCode + "-" +
                tdBuilder.semester1.TermCode + "-" +
                tdBuilder.semester1.SchoolYear;

            List<SisClassTestData> tstDataSet = new List<SisClassTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Class("TestTask",
                new SyncJob_Class())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_ReadUsingCourseCode);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadUsingSisBuildingCodeTermCodeSchoolyear);
            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockClassEnrolledDaoImpl.WasCalled_Write);
            Assert.IsFalse(mockClassEnrolledDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncClass_ShouldUpdateRecord_DifferentName()
        {
            mockCourseDaoImpl._shouldReadReturnData = true;
            mockAcademicSessionDaoImpl._shouldReadReturnData = true;
            mockClassEnrolledDaoImpl._shouldReadReturnData = true;

            SisClassTestData tstData = new SisClassTestData();
            tstData.Name = "NewName";
            tstData.ClassCode = tdBuilder.classEnrolled.ClassCode;
            tstData.ClassType = tdBuilder.classEnrolled.ClassType.Netus2Code;
            tstData.Room = tdBuilder.classEnrolled.Room;
            tstData.CourseId = tdBuilder.spanishCourse.CourseCode;
            tstData.AcademicSessionId =
                tdBuilder.school.SisBuildingCode + "-" +
                tdBuilder.semester1.TrackCode + "-" +
                tdBuilder.semester1.TermCode + "-" +
                tdBuilder.semester1.SchoolYear;

            List<SisClassTestData> tstDataSet = new List<SisClassTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Class("TestTask",
                new SyncJob_Class())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_ReadUsingCourseCode);
            Assert.IsTrue(mockAcademicSessionDaoImpl.WasCalled_ReadUsingSisBuildingCodeTermCodeSchoolyear);
            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockClassEnrolledDaoImpl.WasCalled_Write);
            Assert.IsTrue(mockClassEnrolledDaoImpl.WasCalled_Update);
        }

        [TearDown]
        public void TearDown()
        {
            DaoImplFactory.MockCourseDaoImpl = null;
            DaoImplFactory.MockAcademicSessionDaoImpl = null;
            DaoImplFactory.MockClassEnrolledDaoImpl = null;
        }

        private DataTable BuildTestDataTable(List<SisClassTestData> tstDataSet)
        {
            DataTable dtClass = DataTableFactory.CreateDataTable_Sis_Class();
            foreach (SisClassTestData tstData in tstDataSet)
            {
                DataRow row = dtClass.NewRow();
                row["name"] = tstData.Name;
                row["class_code"] = tstData.ClassCode;
                row["enum_class_id"] = tstData.ClassType;
                row["room"] = tstData.Room;
                row["course_id"] = tstData.CourseId;
                row["academic_session_id"] = tstData.AcademicSessionId;
                dtClass.Rows.Add(row);
            }

            return dtClass;
        }

        private void SetMockReaderWithTestData(List<SisClassTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 6);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count].Name;
                        values[1] = tstDataSet[count].ClassCode;
                        values[2] = tstDataSet[count].ClassType;
                        values[3] = tstDataSet[count].Room;
                        values[4] = tstDataSet[count].CourseId;
                        values[5] = tstDataSet[count].AcademicSessionId;
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "class_code");
            reader.Setup(x => x.GetOrdinal("class_code"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "enum_class_id");
            reader.Setup(x => x.GetOrdinal("enum_class_id"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "room");
            reader.Setup(x => x.GetOrdinal("room"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(4))
                .Returns(() => "course_id");
            reader.Setup(x => x.GetOrdinal("course_id"))
                .Returns(() => 4);
            reader.Setup(x => x.GetFieldType(4))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(5))
                .Returns(() => "academic_session_id");
            reader.Setup(x => x.GetOrdinal("academic_session_id"))
                .Returns(() => 5);
            reader.Setup(x => x.GetFieldType(5))
                .Returns(() => typeof(string));

            _sisConnection.mockReader = reader;
        }

        class SisClassTestData
        {
            public string Name { get; set; }
            public string ClassCode { get; set; }
            public string ClassType { get; set; }
            public string Room { get; set; }
            public string CourseId { get; set; }
            public string AcademicSessionId { get; set; }
        }
    }
}
