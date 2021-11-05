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

namespace Netus2_Test.Unit.SyncProcess
{
    public class SyncCourse_Test
    {
        TestDataBuilder tdBuilder;
        MockDatabaseConnection _sisConnection;
        MockCourseDaoImpl mockCourseDaoImpl;

        [SetUp]
        public void SetUp()
        {
            DbConnectionFactory.ShouldUseMockDb = true;
            _sisConnection = (MockDatabaseConnection)DbConnectionFactory.GetSisConnection();

            tdBuilder = new TestDataBuilder();
            DaoImplFactory.MockAll = true;
            mockCourseDaoImpl = new MockCourseDaoImpl(tdBuilder);
            DaoImplFactory.MockCourseDaoImpl = mockCourseDaoImpl;
        }

        [TestCase]
        public void SisRead_Course_NullData()
        {
            SisCourseTestData tstData = new SisCourseTestData();
            tstData.Name = null;
            tstData.CourseCode = null;
            tstData.Subject = null;
            tstData.Grade = null;

            List<SisCourseTestData> tstDataSet = new List<SisCourseTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Course syncJob_Course = new SyncJob_Course();
            syncJob_Course.ReadFromSis();
            DataTable results = syncJob_Course._dtCourse;

            string emptyString = "";
            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(emptyString, results.Rows[0]["name"].ToString());
            Assert.AreEqual(emptyString, results.Rows[0]["course_code"].ToString());
        }

        [TestCase]
        public void SisRead_Course_TestData()
        {
            SisCourseTestData tstData = new SisCourseTestData();
            tstData.Name = tdBuilder.spanishCourse.Name;
            tstData.CourseCode = tdBuilder.spanishCourse.CourseCode;
            tstData.Subject = tdBuilder.spanishCourse.Subjects[0].Netus2Code;
            tstData.Grade = tdBuilder.spanishCourse.Grades[0].Netus2Code;

            List<SisCourseTestData> tstDataSet = new List<SisCourseTestData>();
            tstDataSet.Add(tstData);

            SetMockReaderWithTestData(tstDataSet);

            SyncJob_Course syncJob_Course = new SyncJob_Course();
            syncJob_Course.ReadFromSis();
            DataTable results = syncJob_Course._dtCourse;

            Assert.NotNull(results);
            Assert.AreEqual(tstDataSet.Count, results.Rows.Count);
            Assert.AreEqual(tstDataSet[0].Name, results.Rows[0]["name"].ToString());
            Assert.AreEqual(tstDataSet[0].CourseCode, results.Rows[0]["course_code"].ToString());
        }

        [TestCase]
        public void SyncChild_Course_ShouldWriteNewRecord()
        {
            SisCourseTestData tstData = new SisCourseTestData();
            tstData.Name = tdBuilder.spanishCourse.Name;
            tstData.CourseCode = tdBuilder.spanishCourse.CourseCode;

            List<SisCourseTestData> tstDataSet = new List<SisCourseTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Course("TestTask", 
                new SyncJob_Course())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Write);
        }

        [TestCase]
        public void SyncChild_Course_ShouldNeitherUpdateNorWriteRecord()
        {
            mockCourseDaoImpl._shouldReadReturnData = true;

            SisCourseTestData tstData = new SisCourseTestData();
            tstData.Name = tdBuilder.spanishCourse.Name;
            tstData.CourseCode = tdBuilder.spanishCourse.CourseCode;
            foreach(Enumeration enumSubject in tdBuilder.spanishCourse.Subjects)
            {
                tstData.Subject += enumSubject.Netus2Code + ',';
            }
            tstData.Subject = tstData.Subject.Remove(tstData.Subject.Length - 1, 1);

            foreach(Enumeration enumGrade in tdBuilder.spanishCourse.Grades)
            {
                tstData.Grade += enumGrade.Netus2Code + ',';
            }
            tstData.Grade = tstData.Grade.Remove(tstData.Grade.Length - 1, 1);

            List<SisCourseTestData> tstDataSet = new List<SisCourseTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Course("TestTask",
                new SyncJob_Course())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Read);
            Assert.IsFalse(mockCourseDaoImpl.WasCalled_Write);
            Assert.IsFalse(mockCourseDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncChild_Course_ShouldUpdateRecord_DifferentCourseCode()
        {
            mockCourseDaoImpl._shouldReadReturnData = true;

            SisCourseTestData tstData = new SisCourseTestData();
            tstData.Name = tdBuilder.spanishCourse.Name;
            tstData.CourseCode = "NewCourseCode";
            foreach (Enumeration enumSubject in tdBuilder.spanishCourse.Subjects)
            {
                tstData.Subject += enumSubject.Netus2Code + ',';
            }
            tstData.Subject = tstData.Subject.Remove(tstData.Subject.Length - 1, 1);

            foreach (Enumeration enumGrade in tdBuilder.spanishCourse.Grades)
            {
                tstData.Grade += enumGrade.Netus2Code + ',';
            }
            tstData.Grade = tstData.Grade.Remove(tstData.Grade.Length - 1, 1);

            List<SisCourseTestData> tstDataSet = new List<SisCourseTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Course("TestTask",
                new SyncJob_Course())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncChild_Course_ShouldUpdateRecord_DifferentName()
        {
            mockCourseDaoImpl._shouldReadReturnData = true;

            SisCourseTestData tstData = new SisCourseTestData();
            tstData.Name = "NewCourseName";
            tstData.CourseCode = tdBuilder.spanishCourse.CourseCode;
            foreach (Enumeration enumSubject in tdBuilder.spanishCourse.Subjects)
            {
                tstData.Subject += enumSubject.Netus2Code + ',';
            }
            tstData.Subject = tstData.Subject.Remove(tstData.Subject.Length - 1, 1);

            foreach (Enumeration enumGrade in tdBuilder.spanishCourse.Grades)
            {
                tstData.Grade += enumGrade.Netus2Code + ',';
            }
            tstData.Grade = tstData.Grade.Remove(tstData.Grade.Length - 1, 1);

            List<SisCourseTestData> tstDataSet = new List<SisCourseTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Course("TestTask",
                new SyncJob_Course())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncChild_Course_ShouldUpdateRecord_ClearSubjects()
        {
            mockCourseDaoImpl._shouldReadReturnData = true;

            SisCourseTestData tstData = new SisCourseTestData();
            tstData.Name = tdBuilder.spanishCourse.Name;
            tstData.CourseCode = tdBuilder.spanishCourse.CourseCode;
            tstData.Subject = null;

            foreach (Enumeration enumGrade in tdBuilder.spanishCourse.Grades)
            {
                tstData.Grade += enumGrade.Netus2Code + ',';
            }
            tstData.Grade = tstData.Grade.Remove(tstData.Grade.Length - 1, 1);

            List<SisCourseTestData> tstDataSet = new List<SisCourseTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Course("TestTask",
                new SyncJob_Course())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncChild_Course_ShouldUpdateRecord_AddSubject()
        {
            mockCourseDaoImpl._shouldReadReturnData = true;

            SisCourseTestData tstData = new SisCourseTestData();
            tstData.Name = tdBuilder.spanishCourse.Name;
            tstData.CourseCode = tdBuilder.spanishCourse.CourseCode;

            foreach (Enumeration enumSubject in tdBuilder.spanishCourse.Subjects)
            {
                tstData.Subject += enumSubject.Netus2Code + ',';
            }
            tstData.Subject = tstData.Subject.Remove(tstData.Subject.Length - 1, 1);
            tstData.Subject += ("," + Enum_Subject.values["be"].SisCode);

            foreach (Enumeration enumGrade in tdBuilder.spanishCourse.Grades)
            {
                tstData.Grade += enumGrade.Netus2Code + ',';
            }
            tstData.Grade = tstData.Grade.Remove(tstData.Grade.Length - 1, 1);

            List<SisCourseTestData> tstDataSet = new List<SisCourseTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Course("TestTask",
                new SyncJob_Course())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncChild_Course_ShouldUpdateRecord_ClearGrades()
        {
            mockCourseDaoImpl._shouldReadReturnData = true;

            SisCourseTestData tstData = new SisCourseTestData();
            tstData.Name = tdBuilder.spanishCourse.Name;
            tstData.CourseCode = tdBuilder.spanishCourse.CourseCode;
            foreach (Enumeration enumSubject in tdBuilder.spanishCourse.Subjects)
            {
                tstData.Subject += enumSubject.Netus2Code + ',';
            }
            tstData.Subject = tstData.Subject.Remove(tstData.Subject.Length - 1, 1);

            foreach (Enumeration enumGrade in tdBuilder.spanishCourse.Grades)
            {
                tstData.Grade += enumGrade.Netus2Code + ',';
            }
            tstData.Grade = tstData.Grade.Remove(tstData.Grade.Length - 1, 1);
            tstData.Grade += "," + Enum_Grade.values["14"].SisCode;

            List<SisCourseTestData> tstDataSet = new List<SisCourseTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Course("TestTask",
                new SyncJob_Course())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Update);
        }

        [TestCase]
        public void SyncChild_Course_ShouldUpdateRecord_AddGrade()
        {
            mockCourseDaoImpl._shouldReadReturnData = true;

            SisCourseTestData tstData = new SisCourseTestData();
            tstData.Name = tdBuilder.spanishCourse.Name;
            tstData.CourseCode = tdBuilder.spanishCourse.CourseCode;
            foreach (Enumeration enumSubject in tdBuilder.spanishCourse.Subjects)
            {
                tstData.Subject += enumSubject.Netus2Code + ',';
            }
            tstData.Subject = tstData.Subject.Remove(tstData.Subject.Length - 1, 1);

            tstData.Grade = null;

            List<SisCourseTestData> tstDataSet = new List<SisCourseTestData>();
            tstDataSet.Add(tstData);
            DataRow row = BuildTestDataTable(tstDataSet).Rows[0];

            new SyncTask_Course("TestTask",
                new SyncJob_Course())
                .Execute(row, new CountDownLatch(0));

            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Read);
            Assert.IsTrue(mockCourseDaoImpl.WasCalled_Update);
        }

        private DataTable BuildTestDataTable(List<SisCourseTestData> tstDataSet)
        {
            DataTable dtCourse = DataTableFactory.CreateDataTable_Sis_Course();
            foreach(SisCourseTestData tstData in tstDataSet)
            {
                DataRow row = dtCourse.NewRow();
                row["name"] = tstData.Name;
                row["course_code"] = tstData.CourseCode;
                row["subject"] = tstData.Subject;
                row["grade"] = tstData.Grade;
                dtCourse.Rows.Add(row);
            }

            return dtCourse;
        }

        private void SetMockReaderWithTestData(List<SisCourseTestData> tstDataSet)
        {
            int count = -1;
            var reader = new Mock<IDataReader>();

            reader.Setup(x => x.Read())
                .Returns(() => count < tstDataSet.Count - 1)
                .Callback(() => count++);

            reader.Setup(x => x.FieldCount)
                .Returns(() => 4);

            reader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Callback<object[]>(
                    (values) =>
                    {
                        values[0] = tstDataSet[count].Name;
                        values[1] = tstDataSet[count].CourseCode;
                        values[2] = tstDataSet[count].Subject;
                        values[3] = tstDataSet[count].Grade;
                    }
                ).Returns(count);

            reader.Setup(x => x.GetName(0))
                .Returns(() => "name");
            reader.Setup(x => x.GetOrdinal("name"))
                .Returns(() => 0);
            reader.Setup(x => x.GetFieldType(0))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(1))
                .Returns(() => "course_code");
            reader.Setup(x => x.GetOrdinal("course_code"))
                .Returns(() => 1);
            reader.Setup(x => x.GetFieldType(1))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(2))
                .Returns(() => "subject");
            reader.Setup(x => x.GetOrdinal("subject"))
                .Returns(() => 2);
            reader.Setup(x => x.GetFieldType(2))
                .Returns(() => typeof(string));

            reader.Setup(x => x.GetName(3))
                .Returns(() => "grade");
            reader.Setup(x => x.GetOrdinal("grade"))
                .Returns(() => 3);
            reader.Setup(x => x.GetFieldType(3))
                .Returns(() => typeof(string));

            _sisConnection.mockReader = reader;
        }
    }

    class SisCourseTestData
    {
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public string Subject { get; set; }
        public string Grade { get; set; }
    }
}
