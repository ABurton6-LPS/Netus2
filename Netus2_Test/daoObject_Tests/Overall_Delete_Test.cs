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

        TestDataBuilder tdBuilder;

        [SetUp]
        public void Setup()
        {
            connection = DbConnectionFactory.GetNetus2Connection();
            connection.BeginTransaction();

            tdBuilder = new TestDataBuilder(connection);

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
        }

        [Test]
        public void Overall_Delete_District()
        {
            organizationDaoImpl.Delete(tdBuilder.district, connection);
            Assert.AreEqual(0, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_School()
        {
            organizationDaoImpl.Delete(tdBuilder.school, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(0, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(0, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(0, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(0, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(0, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_SchoolYear()
        {
            academicSessionDaoImpl.Delete(tdBuilder.schoolYear, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(0, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(0, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(0, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Teacher()
        {
            personDaoImpl.Delete(tdBuilder.teacher, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(0, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(0, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Student, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Student, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_AddressTeacher()
        {
            addressDaoImpl.Delete(tdBuilder.address_Teacher, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(0, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_EmploymentSession()
        {
            employmentSessionDaoImpl.Delete_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(0, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_SpanishCourse()
        {
            courseDaoImpl.Delete(tdBuilder.spanishCourse, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(0, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(0, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(0, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Resource()
        {
            resourceDaoImpl.Delete(tdBuilder.resource, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(0, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_ClassEnrolled()
        {
            classEnrolledDaoImpl.Delete(tdBuilder.classEnrolled, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(0, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(0, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_LineItem()
        {
            lineItemDaoImpl.Delete(tdBuilder.lineItem, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(0, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Student()
        {
            personDaoImpl.Delete(tdBuilder.student, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(0, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Student, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Student, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_AddressStudent()
        {
            addressDaoImpl.Delete(tdBuilder.address_Student, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(0, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Enrollment()
        {
            enrollmentDaoImpl.Delete(tdBuilder.enrollment, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(0, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Mark()
        {
            markDaoImpl.Delete(tdBuilder.mark, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_UniqueIdentifier()
        {
            uniqueIdentifierDaoImpl.Delete(tdBuilder.uniqueId_Student, tdBuilder.student.Id, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Student, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_PhoneNumber()
        {
            phoneNumberDaoImpl.Delete(tdBuilder.phoneNumber_Student, tdBuilder.student.Id, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Student, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(1, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Provider()
        {
            providerDaoImpl.Delete(tdBuilder.provider, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(0, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(0, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [Test]
        public void Overall_Delete_Applicaiton()
        {
            applicationDaoImpl.Delete(tdBuilder.application, connection);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.district, connection).Count);
            Assert.AreEqual(1, organizationDaoImpl.Read(tdBuilder.school, connection).Count);
            Assert.AreEqual(1, academicSessionDaoImpl.Read(tdBuilder.schoolYear, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.teacher, connection).Count);
            Assert.AreEqual(1, employmentSessionDaoImpl.Read_WithPersonId(tdBuilder.employmentSession, tdBuilder.teacher.Id, connection).Count);
            Assert.AreEqual(1, courseDaoImpl.Read(tdBuilder.spanishCourse, connection).Count);
            Assert.AreEqual(1, resourceDaoImpl.Read(tdBuilder.resource, connection).Count);
            Assert.AreEqual(1, classEnrolledDaoImpl.Read(tdBuilder.classEnrolled, connection).Count);
            Assert.AreEqual(1, lineItemDaoImpl.Read(tdBuilder.lineItem, connection).Count);
            Assert.AreEqual(1, personDaoImpl.Read(tdBuilder.student, connection).Count);
            Assert.AreEqual(1, enrollmentDaoImpl.Read(tdBuilder.enrollment, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, markDaoImpl.Read(tdBuilder.mark, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, uniqueIdentifierDaoImpl.Read(tdBuilder.uniqueId_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, phoneNumberDaoImpl.Read(tdBuilder.phoneNumber_Teacher, tdBuilder.student.Id, connection).Count);
            Assert.AreEqual(1, providerDaoImpl.Read(tdBuilder.provider, connection).Count);
            Assert.AreEqual(0, applicationDaoImpl.Read(tdBuilder.application, connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.teacher.Addresses[0], connection).Count);
            Assert.AreEqual(1, addressDaoImpl.Read(tdBuilder.student.Addresses[0], connection).Count);
        }

        [TearDown]
        public void TearDown()
        {
            connection.RollbackTransaction();
            connection.CloseConnection();
        }
    }
}