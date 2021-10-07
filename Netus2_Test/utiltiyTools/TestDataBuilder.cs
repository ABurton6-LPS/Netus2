﻿using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using System;

namespace Netus2_Test
{
    public class TestDataBuilder
    {
        IOrganizationDao organizationDaoImpl = DaoImplFactory.GetOrganizationDaoImpl();
        IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
        IPersonDao personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
        IEmploymentSessionDao employmentSessionDaoImpl = DaoImplFactory.GetEmploymentSessionDaoImpl();
        ICourseDao courseDaoImpl = DaoImplFactory.GetCourseDaoImpl();
        IClassEnrolledDao classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
        IResourceDao resourceDaoImpl = DaoImplFactory.GetResourceDaoImpl();
        ILineItemDao lineItemDaoImpl = DaoImplFactory.GetLineItemDaoImpl();
        IEnrollmentDao enrollmentDaoImpl = DaoImplFactory.GetEnrollmentDaoImpl();
        IMarkDao markDaoImpl = DaoImplFactory.GetMarkDaoImpl();
        IUniqueIdentifierDao uniqueIdentifierDaoImpl = DaoImplFactory.GetUniqueIdentifierDaoImpl();
        IAddressDao addressDaoImpl = DaoImplFactory.GetAddressDaoImpl();
        IPhoneNumberDao phoneNumberDaoImpl = DaoImplFactory.GetPhoneNumberDaoImpl();
        IProviderDao providerDaoImpl = DaoImplFactory.GetProviderDaoImpl();
        IApplicationDao applicationDaoImpl = DaoImplFactory.GetApplicationDaoImpl();

        public Organization district;
        public Organization school;
        public AcademicSession schoolYear;
        public AcademicSession semester1;
        public AcademicSession semester2;
        public Person teacher;
        public EmploymentSession employmentSession;
        public Course spanishCourse;
        public Resource resource;
        public ClassEnrolled classEnrolled;
        public LineItem lineItem;
        public Person student;
        public Enrollment enrollment;
        public Mark mark;
        public UniqueIdentifier uniqueId_Student;
        public UniqueIdentifier uniqueId_Teacher;
        public PhoneNumber phoneNumber_Student;
        public PhoneNumber phoneNumber_Teacher;
        public Address address_Teacher;
        public Address address_Student;
        public Provider provider;
        public Provider provider_parent;
        public Application application;

        public TestDataBuilder(IConnectable connection)
        {
            BuildTestData(connection);
        }

        public TestDataBuilder()
        {
            BuildTestData(null);
        }

        private void BuildTestData(IConnectable connection)
        {
            Enumeration orgEnum = Enum_Organization.values["district"];
            district = new Organization("livonia public schools", orgEnum, "lps", "dBc");
            if (connection != null)
                district = organizationDaoImpl.Write(district, connection);
            else
                district.Id = 1;

            school = new Organization("livonia high school", Enum_Organization.values["school"], "lhs", DateTime.Now.ToString());
            school.HrBuildingCode = "hr building code";
            if (connection != null)
                school = organizationDaoImpl.Write(school, connection);
            else
                school.Id = 1;
            district.Children.Add(school);
            if (connection != null)
            {
                organizationDaoImpl.Update(school, district.Id, connection);
                district = organizationDaoImpl.Read_WithOrganizationId(district.Id, connection);
            }

            schoolYear = new AcademicSession("2021", Enum_Session.values["school year"], school, "T1");
            schoolYear.SchoolYear = 2021;
            if (connection != null)
                schoolYear = academicSessionDaoImpl.Write(schoolYear, connection);
            else
                schoolYear.Id = 1;

            semester1 = new AcademicSession("Semester1", Enum_Session.values["semester"], school, "S1");
            semester1.SchoolYear = 2021;
            schoolYear.Children.Add(semester1);
            if (connection != null)
                semester1 = academicSessionDaoImpl.Write(semester1, schoolYear.Id, connection);
            else
                semester1.Id = 2;

            semester2 = new AcademicSession("Semester2", Enum_Session.values["semester"], school, "S2");
            semester2.SchoolYear = 2021;
            schoolYear.Children.Add(semester2);
            if (connection != null)
                semester2 = academicSessionDaoImpl.Write(semester2, schoolYear.Id, connection);
            else
                semester2.Id = 3;

            teacher = new Person("John", "Smith", new DateTime(1985, 9, 6), Enum_Gender.values["male"], Enum_Ethnic.values["cau"]);
            teacher.Roles.Add(Enum_Role.values["primary teacher"]);
            teacher.MiddleName = "Andrew";
            teacher.LoginName = "JSmith";
            teacher.LoginPw = "Login123@Home";
            if (connection != null)
                teacher = personDaoImpl.Write(teacher, connection);
            else
                teacher.Id = 1;

            phoneNumber_Teacher = new PhoneNumber("8009876549", Enum_True_False.values["true"], Enum_Phone.values["cell"]);
            if (connection != null)
            {
                phoneNumber_Teacher = phoneNumberDaoImpl.Write(phoneNumber_Teacher, teacher.Id, connection);
                teacher = personDaoImpl.Read(teacher, connection)[0];
            }
            else
            {
                phoneNumber_Teacher.Id = 1;
                teacher.PhoneNumbers.Add(phoneNumber_Teacher);
            }

            address_Teacher = new Address("teacher addr", "somewhere", Enum_State_Province.values["mi"], Enum_Country.values["us"], Enum_True_False.values["true"], Enum_Address.values["home"]);
            if (connection != null)
                address_Teacher = addressDaoImpl.Write(address_Teacher, connection);
            else
                address_Teacher.Id = 1;
            teacher.Addresses.Add(address_Teacher);
            if (connection != null)
            {
                personDaoImpl.Update(teacher, connection);
                teacher = personDaoImpl.Read(teacher, connection)[0];
            }

            uniqueId_Teacher = new UniqueIdentifier(DateTime.Now.ToString() + "_teacher", Enum_Identifier.values["state id"], Enum_True_False.values["true"]);
            if (connection != null)
                uniqueId_Teacher = uniqueIdentifierDaoImpl.Write(uniqueId_Teacher, teacher.Id, connection); 
            else
                uniqueId_Teacher.Id = 1;
            teacher.UniqueIdentifiers.Add(uniqueId_Teacher);

            employmentSession = new EmploymentSession("John Smith Employment", Enum_True_False.values["true"], school);
            if (connection != null)
                employmentSession = employmentSessionDaoImpl.Write(employmentSession, teacher.Id, connection);
            else
                employmentSession.Id = 1;

            spanishCourse = new Course("spanish", "spn");
            if (connection != null)
                spanishCourse = courseDaoImpl.Write(spanishCourse, connection);
            else
                spanishCourse.Id = 1;
            spanishCourse.Grades.Add(Enum_Grade.values["1"]);
            spanishCourse.Subjects.Add(Enum_Subject.values["spn"]);
            if (connection != null)
            {
                courseDaoImpl.Update(spanishCourse, connection);
                spanishCourse = courseDaoImpl.Read(spanishCourse, connection)[0];
            }

            resource = new Resource("I do not know what a resource is", "Something a vendor would know");
            resource.Importance = Enum_Importance.values["primary"];
            resource.VendorId = "123IDK";
            resource.ApplicationId = "Not an actual app id";
            if (connection != null)
                resource = resourceDaoImpl.Write(resource, connection);
            else
                resource.Id = 1;

            classEnrolled = new ClassEnrolled("Mrs. Beckner 1st hour Spanish Class", "B_1_SPN", Enum_Class.values["scheduled"], "237", spanishCourse, schoolYear);
            classEnrolled.Resources.Add(resource);
            classEnrolled.AddStaff(teacher, Enum_Role.values["primary teacher"]);
            classEnrolled.Periods.Add(Enum_Period.values["1"]);
            classEnrolled.AcademicSession = semester1;
            if (connection != null)
                classEnrolled = classEnrolledDaoImpl.Write(classEnrolled, connection);
            else
                classEnrolled.Id = 1;

            lineItem = new LineItem("Pop Quiz", new DateTime(2020, 9, 1), new DateTime(2020, 9, 1), classEnrolled, Enum_Category.values["test"], 0.0, 100.00);
            lineItem.Descript = "Test Line Item";
            if (connection != null)
                lineItem = lineItemDaoImpl.Write(lineItem, connection);
            else
                lineItem.Id = 1;

            student = new Person("Tony", "McFarlian", new DateTime(2008, 9, 6), Enum_Gender.values["male"], Enum_Ethnic.values["cau"]);
            student.ResidenceStatus = Enum_Residence_Status.values["01652"];
            student.MiddleName = "Andrew";
            student.LoginName = "TMcFarlian";
            student.LoginPw = "WouldntYouLikeToKnow";
            if (connection != null)
                student = personDaoImpl.Write(student, connection);
            else
                student.Id = 2;
            student.Roles.Add(Enum_Role.values["student"]);
            student.Relations.Add(teacher.Id);
            if (connection != null)
            {
                personDaoImpl.Update(student, connection);
                student = personDaoImpl.Read(student, connection)[0];
                teacher = personDaoImpl.Read(teacher, connection)[0];
            }
            else
                teacher.Relations.Add(student.Id);

            address_Student = new Address("student addr", "somewhere", Enum_State_Province.values["mi"], Enum_Country.values["us"], Enum_True_False.values["true"], Enum_Address.values["home"]);
            if (connection != null)
                address_Student = addressDaoImpl.Write(address_Student, connection);
            else
                address_Student.Id = 2;
            student.Addresses.Add(address_Student);
            if (connection != null)
            {
                personDaoImpl.Update(student, connection);
                student = personDaoImpl.Read(student, connection)[0];
            }

            enrollment = new Enrollment(Enum_Grade.values["6"], new DateTime(2020, 9, 6), new DateTime(2021, 6, 1), Enum_True_False.values["true"], classEnrolled, null);
            enrollment.AcademicSessions.Add(schoolYear);
            if (connection != null)
                enrollment = enrollmentDaoImpl.Write(enrollment, student.Id, connection);
            else
                enrollment.Id = 1;

            mark = new Mark(lineItem, Enum_Score_Status.values["submitted"], 100.00, new DateTime(2020, 9, 1));
            mark.Comment = "I like this test!";
            if (connection != null)
                mark = markDaoImpl.Write(mark, student.Id, connection);
            else
                mark.Id = 1;

            uniqueId_Student = new UniqueIdentifier(DateTime.Now.ToString() + "_student", Enum_Identifier.values["student id"], Enum_True_False.values["true"]);
            if (connection != null)
                uniqueId_Student = uniqueIdentifierDaoImpl.Write(uniqueId_Student, student.Id, connection);
            else
            {
                uniqueId_Student.Id = 2;
                student.UniqueIdentifiers.Add(uniqueId_Student);
            }

            phoneNumber_Student = new PhoneNumber("8001231234", Enum_True_False.values["true"], Enum_Phone.values["cell"]);
            if (connection != null)
            {
                phoneNumber_Student = phoneNumberDaoImpl.Write(phoneNumber_Student, student.Id, connection);
                student = personDaoImpl.Read(student, connection)[0];
            }
            else
            {
                phoneNumber_Student.Id = 2;
                student.PhoneNumbers.Add(phoneNumber_Student);
            }

            provider = new Provider("new provider");
            provider.UrlStandardAccess = "StandardAccessUrl";
            provider.UrlAdminAccess = "AdminAccessUrl";
            provider.PopulatedBy = "TestDataBuilder";
            if (connection != null)
                provider = providerDaoImpl.Write(provider, connection);
            else
                provider.Id = 1;

            provider_parent = new Provider("new provider_parent");
            provider_parent.UrlStandardAccess = "StandardAccessUrl";
            provider_parent.UrlAdminAccess = "AdminAccessUrl";
            provider_parent.PopulatedBy = "TestDataBuilder";
            provider_parent.Children.Add(provider);
            if (connection != null)
                provider_parent = providerDaoImpl.Write(provider_parent, connection);
            else
                provider_parent.Id = 2;

            application = new Application("new application", provider);
            if (connection != null)
                application = applicationDaoImpl.Write(application, connection);
            else
                application.Id = 1;
            teacher.Applications.Add(application);
            student.Applications.Add(application);
            if (connection != null)
            {
                personDaoImpl.Update(teacher, connection);
                personDaoImpl.Update(student, connection);
                teacher = personDaoImpl.Read(teacher, connection)[0];
                student = personDaoImpl.Read(student, connection)[0];
            }
        }
    }
}