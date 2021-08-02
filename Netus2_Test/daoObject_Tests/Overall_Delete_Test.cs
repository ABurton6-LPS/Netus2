using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using NUnit.Framework;
using System;

namespace Netus2_Test.daoObject_Tests
{
    class Overall_Delete_Test
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
        IPhoneNumberDao phoneNumberDaoImpl;
        IProviderDao providerDaoImpl;
        IApplicationDao applicationDaoImpl;
        IAddressDao addressDaoImpl;

        Organization district;
        Organization school;
        AcademicSession schoolYear;
        Person teacher;
        EmploymentSession employmentSession;
        Course spanishCourse;
        Resource resource;
        ClassEnrolled classEnrolled;
        LineItem lineItem;
        Person student;
        Enrollment enrollment;
        Mark mark;
        UniqueIdentifier uniqueId;
        PhoneNumber phoneNumber;
        Provider provider;
        Application app;
        Address addressTeacher;
        Address addressStudent;

        [SetUp]
        public void Setup()
        {
            connection = DbConnectionFactory.GetConnection("Local");
            connection.OpenConnection();
            connection.BeginTransaction();
            organizationDaoImpl = new OrganizationDaoImpl();
            academicSessionDaoImpl = new AcademicSessionDaoImpl();
            personDaoImpl = new PersonDaoImpl();
            employmentSessionDaoImpl = new EmploymentSessionDaoImpl();
            courseDaoImpl = new CourseDaoImpl();
            classEnrolledDaoImpl = new ClassEnrolledDaoImpl();
            resourceDaoImpl = new ResourceDaoImpl();
            lineItemDaoImpl = new LineItemDaoImpl();
            enrollmentDaoImpl = new EnrollmentDaoImpl();
            markDaoImpl = new MarkDaoImpl();
            uniqueIdentifierDaoImpl = new UniqueIdentifierDaoImpl();
            phoneNumberDaoImpl = new PhoneNumberDaoImpl();
            providerDaoImpl = new ProviderDaoImpl();
            applicationDaoImpl = new ApplicationDaoImpl();
            addressDaoImpl = new AddressDaoImpl();

            district = new Organization("Livonia Public Schools", Enum_Organization.values["district"], "LPS");
            district = organizationDaoImpl.Write(district, connection);

            school = new Organization("Livonia High School", Enum_Organization.values["school"], "LHS");
            school = organizationDaoImpl.Write(school, connection);
            district.Children.Add(school);
            organizationDaoImpl.Update(school, district.Id, connection);
            school = organizationDaoImpl.Read(school, connection)[0];

            schoolYear = new AcademicSession("2020 - 2021", Enum_Session.values["school year"], school, "T1");
            schoolYear = academicSessionDaoImpl.Write(schoolYear, connection);

            teacher = new Person("John", "Smith", new DateTime(1985, 9, 6), Enum_Gender.values["male"], Enum_Ethnic.values["cau"]);
            teacher = personDaoImpl.Write(teacher, connection);

            addressTeacher = new Address("123 Main", "City", Enum_State_Province.values["mi"], Enum_Country.values["us"], Enum_True_False.values["true"], Enum_Address.values["home"]);
            addressTeacher = addressDaoImpl.Write(addressTeacher, connection);
            teacher.Addresses.Add(addressTeacher);
            personDaoImpl.Update(teacher, connection);
            teacher = personDaoImpl.Read(teacher, connection)[0];

            employmentSession = new EmploymentSession("John Smith Employment", Enum_True_False.values["true"], school);
            employmentSession = employmentSessionDaoImpl.Write(employmentSession, teacher.Id, connection);

            spanishCourse = new Course("Spanish", "spn");
            spanishCourse = courseDaoImpl.Write(spanishCourse, connection);
            spanishCourse.Grades.Add(Enum_Grade.values["1"]);
            spanishCourse.Subjects.Add(Enum_Subject.values["spn"]);
            courseDaoImpl.Update(spanishCourse, connection);
            spanishCourse = courseDaoImpl.Read(spanishCourse, connection)[0];

            resource = new Resource("I do not know what a resource is", "Something a vendor would know");
            resource = resourceDaoImpl.Write(resource, connection);

            classEnrolled = new ClassEnrolled("Mrs. Beckner 1st hour Spanish Class", "B_1_SPN", Enum_Class.values["scheduled"], "237", spanishCourse, schoolYear);
            classEnrolled = classEnrolledDaoImpl.Write(classEnrolled, connection);
            classEnrolled.Resources.Add(resource);
            classEnrolled.AddStaff(teacher, Enum_Role.values["primary teacher"]);
            classEnrolled.Periods.Add(Enum_Period.values["1"]);
            classEnrolledDaoImpl.Update(classEnrolled, connection);
            classEnrolled = classEnrolledDaoImpl.Read(classEnrolled, connection)[0];

            lineItem = new LineItem("Pop Quiz", new DateTime(2020, 9, 1), new DateTime(2020, 9, 1), classEnrolled, Enum_Category.values["test"], 0.0, 100.00);
            lineItem = lineItemDaoImpl.Write(lineItem, connection);

            student = new Person("Tony", "McFarlian", new DateTime(2008, 9, 6), Enum_Gender.values["male"], Enum_Ethnic.values["cau"]);
            student = personDaoImpl.Write(student, connection);
            student.Roles.Add(Enum_Role.values["student"]);
            student.Relations.Add(teacher.Id);
            personDaoImpl.Update(student, connection);
            student = personDaoImpl.Read(student, connection)[0];
            teacher = personDaoImpl.Read(teacher, connection)[0];

            addressStudent = new Address("456 Main", "City", Enum_State_Province.values["mi"], Enum_Country.values["us"], Enum_True_False.values["true"], Enum_Address.values["home"]);
            addressStudent = addressDaoImpl.Write(addressStudent, connection);
            student.Addresses.Add(addressStudent);
            personDaoImpl.Update(student, connection);
            student = personDaoImpl.Read(student, connection)[0];

            enrollment = new Enrollment(Enum_Grade.values["6"], new DateTime(2020, 9, 6), new DateTime(2021, 6, 1), Enum_True_False.values["true"], classEnrolled, null);
            enrollment = enrollmentDaoImpl.Write(enrollment, student.Id, connection);

            mark = new Mark(lineItem, Enum_Score_Status.values["submitted"], 100.00, new DateTime(2020, 9, 1));
            mark = markDaoImpl.Write(mark, student.Id, connection);

            uniqueId = new UniqueIdentifier("Overall_Delete_Test_Student", Enum_Identifier.values["student id"], Enum_True_False.values["true"]);
            uniqueId = uniqueIdentifierDaoImpl.Write(uniqueId, student.Id, connection);

            phoneNumber = new PhoneNumber("8001231234", Enum_True_False.values["true"], Enum_Phone.values["cell"]);
            phoneNumber = phoneNumberDaoImpl.Write(phoneNumber, student.Id, connection);
            student = personDaoImpl.Read(student, connection)[0];

            provider = new Provider("new provider");
            provider = providerDaoImpl.Write(provider, connection);

            app = new Application("new application", provider);
            app = applicationDaoImpl.Write(app, connection);
            student.Applications.Add(app);
            personDaoImpl.Update(student, connection);
            student = personDaoImpl.Read(student, connection)[0];
        }

        [Test]
        public void Overall_Delete_District()
        {
            organizationDaoImpl.Delete(district, connection);
            Assert.AreEqual(0, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_School()
        {
            organizationDaoImpl.Delete(school, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(0, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(0, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(0, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(0, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(0, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_SchoolYear()
        {
            academicSessionDaoImpl.Delete(schoolYear, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(0, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(0, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(0, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Teacher()
        {
            personDaoImpl.Delete(teacher, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(0, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(0, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_AddressTeacher()
        {
            addressDaoImpl.Delete(addressTeacher, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(0, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_EmploymentSession()
        {
            employmentSessionDaoImpl.Delete_WithPersonId(employmentSession, teacher.Id, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(0, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_SpanishCourse()
        {
            courseDaoImpl.Delete(spanishCourse, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(0, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(0, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(0, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Resource()
        {
            resourceDaoImpl.Delete(resource, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(0, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_ClassEnrolled()
        {
            classEnrolledDaoImpl.Delete(classEnrolled, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(0, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(0, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_LineItem()
        {
            lineItemDaoImpl.Delete(lineItem, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(0, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Student()
        {
            personDaoImpl.Delete(student, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(0, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(0, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(0, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_AddressStudent()
        {
            addressDaoImpl.Delete(addressStudent, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(0, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Enrollment()
        {
            enrollmentDaoImpl.Delete(enrollment, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Mark()
        {
            markDaoImpl.Delete(mark, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_UniqueIdentifier()
        {
            uniqueIdentifierDaoImpl.Delete(uniqueId, student.Id, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(0, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_PhoneNumber()
        {
            phoneNumberDaoImpl.Delete(phoneNumber, student.Id, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(0, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Provider()
        {
            providerDaoImpl.Delete(provider, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(0, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(0, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Applicaiton()
        {
            applicationDaoImpl.Delete(app, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(employmentSession, teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(enrollment, student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(mark, student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(uniqueId, student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(phoneNumber, student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(provider, connection).Count);
            Assert.AreEqual(0, applicationDaoImpl.Read(app, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(student.Addresses[0], connection).Count);
        }

        [TearDown]
        public void TearDown()
        {
            connection.RollbackTransaction();
            connection.CloseConnection();
        }
    }
}