using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_Test.utiltiyTools;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Netus2_Test.Integration
{
    class Overall_ReadWrite_Test
    {
        IConnectable connection;
        IOrganizationDao organizationDaoImpl;
        IAcademicSessionDao academicSessionDaoImpl;
        IPersonDao personDaoImpl;
        IEmploymentSessionDao employmentSessionDaoImpl;
        ICourseDao courseDaoImpl;
        IClassEnrolledDao classEnrolledDaoImpl;
        IResourceDao resourceDaoImpl;
        ILineItemDao lineItemDaoImpl;
        IEnrollmentDao enrollmentDaoImpl;
        IMarkDao markDaoImpl;
        IUniqueIdentifierDao uniqueIdentifierDaoImpl;
        IAddressDao addressDaoImpl;
        IPhoneNumberDao phoneNumberDaoImpl;
        IProviderDao providerDaoImpl;
        IApplicationDao applicationDaoImpl;
        IJctPersonPhoneNumberDao jctPersonPhoneNumberDaoImpl;

        [SetUp]
        public void Setup()
        {
            DbConnectionFactory.environment = new MockEnvironment();

            connection = DbConnectionFactory.GetNetus2Connection();
            connection.BeginTransaction();
            organizationDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
            academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
            personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
            employmentSessionDaoImpl = DaoImplFactory.GetEmploymentSessionDaoImpl();
            courseDaoImpl = DaoImplFactory.GetCourseDaoImpl();
            classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
            resourceDaoImpl = DaoImplFactory.GetResourceDaoImpl();
            lineItemDaoImpl = DaoImplFactory.GetLineItemDaoImpl();
            enrollmentDaoImpl = DaoImplFactory.GetEnrollmentDaoImpl();
            markDaoImpl = DaoImplFactory.GetMarkDaoImpl();
            uniqueIdentifierDaoImpl = DaoImplFactory.GetUniqueIdentifierDaoImpl();
            addressDaoImpl = DaoImplFactory.GetAddressDaoImpl();
            phoneNumberDaoImpl = DaoImplFactory.GetPhoneNumberDaoImpl();
            providerDaoImpl = DaoImplFactory.GetProviderDaoImpl();
            applicationDaoImpl = DaoImplFactory.GetApplicationDaoImpl();
            jctPersonPhoneNumberDaoImpl = DaoImplFactory.GetJctPersonPhoneNumberDaoImpl();
        }

        [Test]
        public void Overall_ReadWrite()
        {
            //Create District
            Organization district = new Organization("Livonia Public Schools", Enum_Organization.values["district"], "LPS", "DistrictBuildingCode");
            district = organizationDaoImpl.Write(district, connection);
            district = organizationDaoImpl.Read(district, connection)[0];
            Assert.IsTrue(district.Id > 0);
            Assert.IsEmpty(district.Children);

            //Create School and link it with the District
            //Either write the School with the District Id, or update the School once it has been added to the District
            Organization school = new Organization("Livonia High School", Enum_Organization.values["school"], "LHS", "SchoolBuildingCode");
            school = organizationDaoImpl.Write(school, connection);
            district.Children.Add(school);
            organizationDaoImpl.Update(school, district.Id, connection);
            district = organizationDaoImpl.Read(district, connection)[0];
            Assert.IsTrue(school.Id > 0);
            Assert.IsNotEmpty(district.Children);
            Assert.IsEmpty(school.Children);

            //Add Academic Session to School
            //You must create the AcademicSession with the Organization (School), so no need to update the School
            AcademicSession schoolYear = new AcademicSession(Enum_Session.values["school year"], school, "T1");
            schoolYear.Name = "2020 - 2021";
            schoolYear = academicSessionDaoImpl.Write(schoolYear, connection);
            schoolYear = academicSessionDaoImpl.Read(schoolYear, connection)[0];
            Assert.IsTrue(schoolYear.Id > 0);
            Assert.AreEqual(schoolYear.Organization.Id, school.Id);

            //Add GradingPeriod AcademicSession to SchoolYear
            //You can write the AcademicSession with the parentId, so no need to update the SchoolYear
            AcademicSession gradingPeriod = new AcademicSession(Enum_Session.values["grading period"], school, "T1");
            gradingPeriod.Name = "markingPeriod";
            gradingPeriod = academicSessionDaoImpl.Write(gradingPeriod, schoolYear.Id, connection);
            schoolYear = academicSessionDaoImpl.Read(schoolYear, connection)[0];
            Assert.IsTrue(gradingPeriod.Id > 0);
            Assert.AreEqual(schoolYear.Children[0].Id, gradingPeriod.Id);

            //Create Teacher
            Person teacher = new Person("John", "Smith", new DateTime(1985, 9, 6), Enum_Gender.values["male"], Enum_Ethnic.values["cau"]);
            teacher = personDaoImpl.Write(teacher, connection);
            Assert.IsTrue(teacher.Id > 0);

            //Create AddressTeacher
            //Add the AddressTeacher to the Teacher and update the Teacher
            Address addressTeacher = new Address("123 Main", null, "City", Enum_State_Province.values["mi"], "12345");
            addressTeacher.IsPrimary = Enum_True_False.values["true"];
            addressTeacher = addressDaoImpl.Write(addressTeacher, connection);
            teacher.Addresses.Add(addressTeacher);
            personDaoImpl.Update(teacher, connection);
            teacher = personDaoImpl.Read(teacher, connection)[0];
            Assert.IsTrue(addressTeacher.Id > 0);
            Assert.AreEqual(addressTeacher.Id, teacher.Addresses[0].Id);

            //Add Employment Session to School
            //You must create the Employment Session with the Organization (School), so no need to update the School
            //You must write the Employment Session with the Person Id, so no need to update the Person
            EmploymentSession employmentSession = new EmploymentSession(Enum_True_False.values["true"], school);
            employmentSession = employmentSessionDaoImpl.Write(employmentSession, teacher.Id, connection);
            employmentSession = employmentSessionDaoImpl.Read_AllWithOrganizationId(school.Id, connection)[0];
            Assert.AreEqual(school.Id, employmentSession.Organization.Id);
            Assert.AreEqual(employmentSession.Id, personDaoImpl.Read(teacher, connection)[0].EmploymentSessions[0].Id);
            employmentSession = employmentSessionDaoImpl.Read_AllWithPersonId(teacher.Id, connection)[0];
            Assert.AreEqual(school.Id, employmentSession.Organization.Id);
            Assert.AreEqual(employmentSession.Id, personDaoImpl.Read(teacher, connection)[0].EmploymentSessions[0].Id);
            Assert.AreEqual(employmentSessionDaoImpl.Read_AllWithOrganizationId(school.Id, connection)[0].Id,
                employmentSessionDaoImpl.Read_AllWithPersonId(teacher.Id, connection)[0].Id);

            //Create Course
            Course spanishCourse = new Course("Spanish", "spn");
            spanishCourse = courseDaoImpl.Write(spanishCourse, connection);
            spanishCourse.Grades.Add(Enum_Grade.values["1"]);
            spanishCourse.Subjects.Add(Enum_Subject.values["fl"]);
            courseDaoImpl.Update(spanishCourse, connection);
            spanishCourse = courseDaoImpl.Read(spanishCourse, connection)[0];
            Assert.IsTrue(spanishCourse.Id > 0);
            Assert.IsNotEmpty(spanishCourse.Grades);
            Assert.IsNotEmpty(spanishCourse.Subjects);

            //Create Resource
            Resource resource = new Resource("I do not know what a resource is", "Something a vendor would know");
            resource = resourceDaoImpl.Write(resource, connection);
            Assert.IsTrue(resource.Id > 0);

            //Add Class to School Year
            //You must create the Class with the Course and Academic Session (School Year), so no need to update them
            ClassEnrolled classEnrolled = new ClassEnrolled("Mrs. Beckner 1st hour Spanish Class", "B_1_SPN", Enum_Class.values["scheduled"], "237", spanishCourse, schoolYear);
            classEnrolled = classEnrolledDaoImpl.Write(classEnrolled, connection);
            classEnrolled.Resources.Add(resource);
            classEnrolled.AddStaff(teacher, Enum_Role.values["primary teacher"]);
            classEnrolled.Periods.Add(Enum_Period.values["1"]);
            classEnrolledDaoImpl.Update(classEnrolled, connection);
            classEnrolled = classEnrolledDaoImpl.Read(classEnrolled, connection)[0];
            Assert.IsTrue(classEnrolled.Id > 0);
            Assert.AreEqual(classEnrolled.AcademicSession.Id, schoolYear.Id);
            Assert.AreEqual(classEnrolled.Course.Id, spanishCourse.Id);
            Assert.AreEqual(resource.Id, classEnrolled.Resources[0].Id);
            Assert.AreEqual(teacher.Id, classEnrolled.GetStaff()[0].Id);
            Assert.IsNotEmpty(classEnrolled.Periods);

            //Add LineItem to Class
            //You must create the LineItem with the ClassEnrolled, so no need to update the ClassEnrolled
            LineItem lineItem = new LineItem("Pop Quiz", new DateTime(2020, 9, 1), new DateTime(2020, 9, 1), classEnrolled, Enum_Category.values["test"], 0.0, 100.00);
            lineItem = lineItemDaoImpl.Write(lineItem, connection);
            lineItem = lineItemDaoImpl.Read(lineItem, connection)[0];
            Assert.IsTrue(lineItem.Id > 0);
            Assert.AreEqual(classEnrolled.Id, lineItem.ClassAssigned.Id);

            //Create Student
            Person student = new Person("Tony", "McFarlian", new DateTime(2008, 9, 6), Enum_Gender.values["male"], Enum_Ethnic.values["cau"]);
            student = personDaoImpl.Write(student, connection);
            student.Roles.Add(Enum_Role.values["student"]);
            student.Relations.Add(teacher.Id);
            personDaoImpl.Update(student, connection);
            student = personDaoImpl.Read(student, connection)[0];
            teacher = personDaoImpl.Read(teacher, connection)[0];
            Assert.IsTrue(student.Id > 0);
            Assert.AreEqual(student.Roles[0], Enum_Role.values["student"]);
            Assert.AreEqual(student.Id, teacher.Relations[0]);
            Assert.AreEqual(teacher.Id, student.Relations[0]);

            //Create AddressStudent
            //Add the AddressStudent to the Student and update the Student
            Address addressStudent = new Address("456 Main", null, "City", Enum_State_Province.values["mi"], "12345");
            addressStudent.IsPrimary = Enum_True_False.values["true"];
            addressStudent = addressDaoImpl.Write(addressStudent, connection);
            student.Addresses.Add(addressStudent);
            personDaoImpl.Update(student, connection);
            student = personDaoImpl.Read(student, connection)[0];
            Assert.IsTrue(addressStudent.Id > 0);
            Assert.AreEqual(addressStudent.Id, student.Addresses[0].Id);

            //Create Enrollment
            //You must create the Enrollment with the ClassEnrolled, so no need to update ClassEnrolled
            //You must write the Enrollment with the Student Id, so no need to update Student
            Enrollment enrollment = new Enrollment(Enum_Grade.values["6"], new DateTime(2020, 9, 6), new DateTime(2021, 6, 1), Enum_True_False.values["true"], classEnrolled, new List<AcademicSession>());
            enrollment = enrollmentDaoImpl.Write(enrollment, student.Id, connection);
            Assert.IsTrue(enrollment.Id > 0);
            Assert.AreEqual(classEnrolled.Id, enrollment.ClassEnrolled.Id);
            student = personDaoImpl.Read(student, connection)[0];
            Assert.AreEqual(enrollment.Id, student.Enrollments[0].Id);

            //Link the SchoolYear to the Enrollment, update the Enrollment
            enrollment.AcademicSessions.Add(schoolYear);
            enrollmentDaoImpl.Update(enrollment, student.Id, connection);
            enrollment = enrollmentDaoImpl.Read(enrollment, student.Id, connection)[0];
            Assert.AreEqual(schoolYear.Id, enrollment.AcademicSessions[0].Id);

            //Create Mark
            //You must create the Mark with the LineItem, so no need to update LineItem
            //You must write the Mark with the Student Id, so no need to update the Student
            Mark mark = new Mark(lineItem, Enum_Score_Status.values["submitted"], 100.00, new DateTime(2020, 9, 1));
            mark = markDaoImpl.Write(mark, student.Id, connection);
            student = personDaoImpl.Read(student, connection)[0];
            Assert.IsTrue(mark.Id > 0);
            Assert.AreEqual(mark.Id, student.Marks[0].Id);
            Assert.AreEqual(lineItem.Id, mark.LineItem.Id);

            //Create Unique Identifier
            //You must write the Unique Identifier with the Student Id, so no need to update the Student
            UniqueIdentifier uniqueId = new UniqueIdentifier("Overall_ReadWrite_Test_Student", Enum_Identifier.values["student id"]);
            uniqueId = uniqueIdentifierDaoImpl.Write(uniqueId, student.Id, connection);
            student = personDaoImpl.Read(student, connection)[0];
            Assert.IsTrue(uniqueId.Id > 0);
            Assert.AreEqual(uniqueId.Id, student.UniqueIdentifiers[0].Id);

            //Create Phone Number
            //You must write the Phone Number with the Student Id, so no need to update the Student
            PhoneNumber phoneNumber = new PhoneNumber("8001231234", Enum_Phone.values["cell"]);
            phoneNumber.IsPrimary = Enum_True_False.values["true"];
            phoneNumber = phoneNumberDaoImpl.Write(phoneNumber, connection);
            jctPersonPhoneNumberDaoImpl.Write(student.Id, phoneNumber.Id, phoneNumber.IsPrimary.Id, connection);
            student = personDaoImpl.Read(student, connection)[0];
            Assert.IsTrue(phoneNumber.Id > 0);
            Assert.AreEqual(phoneNumber.Id, student.PhoneNumbers[0].Id);

            //Create Provider
            Provider provider = new Provider("new provider");
            provider = providerDaoImpl.Write(provider, connection);
            Assert.IsTrue(provider.Id > 0);

            //Create Application
            //You must create the Applicaiton with the Provider, so no need to update the Provider
            Application app = new Application("new application", provider);
            app = applicationDaoImpl.Write(app, connection);
            student.Applications.Add(app);
            personDaoImpl.Update(student, connection);
            student = personDaoImpl.Read(student, connection)[0];
            Assert.IsTrue(app.Id > 0);
            Assert.AreEqual(provider.Id, app.Provider.Id);
            Assert.AreEqual(app.Id, student.Applications[0].Id);
        }

        [TearDown]
        public void TearDown()
        {
            connection.RollbackTransaction();
            connection.CloseConnection();
        }
    }
}