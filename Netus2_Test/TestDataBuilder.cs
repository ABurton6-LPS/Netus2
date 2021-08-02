using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using System;
using System.Collections.Generic;

namespace Netus2_Test
{
    class TestDataBuilder
    {
        IOrganizationDao organizationDaoImpl = new OrganizationDaoImpl();
        IAcademicSessionDao academicSessionDaoImpl = new AcademicSessionDaoImpl();
        IPersonDao personDaoImpl = new PersonDaoImpl();
        IEmploymentSessionDao employmentSessionDaoImpl = new EmploymentSessionDaoImpl();
        ICourseDao courseDaoImpl = new CourseDaoImpl();
        IClassEnrolledDao classEnrolledDaoImpl = new ClassEnrolledDaoImpl();
        IResourceDao resourceDaoImpl = new ResourceDaoImpl();
        ILineItemDao lineItemDaoImpl = new LineItemDaoImpl();
        IEnrollmentDao enrollmentDaoImpl = new EnrollmentDaoImpl();
        IMarkDao markDaoImpl = new MarkDaoImpl();
        IUniqueIdentifierDao uniqueIdentifierDaoImpl = new UniqueIdentifierDaoImpl();
        IAddressDao addressDaoImpl = new AddressDaoImpl();
        IPhoneNumberDao phoneNumberDaoImpl = new PhoneNumberDaoImpl();
        IProviderDao providerDaoImpl = new ProviderDaoImpl();
        IApplicationDao applicationDaoImpl = new ApplicationDaoImpl();

        public Organization district;
        public Organization school;
        public AcademicSession schoolYear;
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
        public Application app;

        public TestDataBuilder(IConnectable connection)
        {
            district = new Organization("livonia public schools", Enum_Organization.values["district"], "lps");
            district = organizationDaoImpl.Write(district, connection);

            school = new Organization("livonia high school", Enum_Organization.values["school"], "lhs");
            school.BuildingCode = DateTime.Now.ToString();
            school = organizationDaoImpl.Write(school, connection);
            district.Children.Add(school);
            organizationDaoImpl.Update(school, district.Id, connection);
            school = organizationDaoImpl.Read(school, connection)[0];

            schoolYear = new AcademicSession("2020 - 2021", Enum_Session.values["school year"], school, "T1");
            schoolYear = academicSessionDaoImpl.Write(schoolYear, connection);

            teacher = new Person("John", "Smith", new DateTime(1985, 9, 6), Enum_Gender.values["male"], Enum_Ethnic.values["cau"]);
            teacher.Roles.Add(Enum_Role.values["primary teacher"]);
            teacher = personDaoImpl.Write(teacher, connection);

            phoneNumber_Teacher = new PhoneNumber("8009876549", Enum_True_False.values["true"], Enum_Phone.values["cell"]);
            phoneNumber_Teacher = phoneNumberDaoImpl.Write(phoneNumber_Teacher, teacher.Id, connection);
            teacher = personDaoImpl.Read(teacher, connection)[0];

            address_Teacher = new Address("teacher addr", "somewhere", Enum_State_Province.values["mi"], Enum_Country.values["us"], Enum_True_False.values["true"], Enum_Address.values["home"]);
            address_Teacher = addressDaoImpl.Write(address_Teacher, connection);
            teacher.Addresses.Add(address_Teacher);
            personDaoImpl.Update(teacher, connection);
            teacher = personDaoImpl.Read(teacher, connection)[0];

            uniqueId_Teacher = new UniqueIdentifier(DateTime.Now.ToString(), Enum_Identifier.values["state id"], Enum_True_False.values["true"]);
            uniqueId_Teacher = uniqueIdentifierDaoImpl.Write(uniqueId_Teacher, teacher.Id, connection);

            employmentSession = new EmploymentSession("John Smith Employment", Enum_True_False.values["true"], school);
            employmentSession = employmentSessionDaoImpl.Write(employmentSession, teacher.Id, connection);

            spanishCourse = new Course("spanish", "spn");
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

            address_Student = new Address("student addr", "somewhere", Enum_State_Province.values["mi"], Enum_Country.values["us"], Enum_True_False.values["true"], Enum_Address.values["home"]);
            address_Student = addressDaoImpl.Write(address_Student, connection);
            student.Addresses.Add(address_Student);
            personDaoImpl.Update(student, connection);
            student = personDaoImpl.Read(student, connection)[0];

            enrollment = new Enrollment(Enum_Grade.values["6"], new DateTime(2020, 9, 6), new DateTime(2021, 6, 1), Enum_True_False.values["true"], classEnrolled, null);
            enrollment = enrollmentDaoImpl.Write(enrollment, student.Id, connection);

            mark = new Mark(lineItem, Enum_Score_Status.values["submitted"], 100.00, new DateTime(2020, 9, 1));
            mark = markDaoImpl.Write(mark, student.Id, connection);

            uniqueId_Student = new UniqueIdentifier(DateTime.Now.ToString(), Enum_Identifier.values["student id"], Enum_True_False.values["true"]);
            uniqueId_Student = uniqueIdentifierDaoImpl.Write(uniqueId_Student, student.Id, connection);

            phoneNumber_Student = new PhoneNumber("8001231234", Enum_True_False.values["true"], Enum_Phone.values["cell"]);
            phoneNumber_Student = phoneNumberDaoImpl.Write(phoneNumber_Student, student.Id, connection);
            student = personDaoImpl.Read(student, connection)[0];

            provider = new Provider("new provider");
            provider = providerDaoImpl.Write(provider, connection);

            app = new Application("new application", provider);
            app = applicationDaoImpl.Write(app, connection);
            teacher.Applications.Add(app);
            student.Applications.Add(app);
            personDaoImpl.Update(teacher, connection);
            personDaoImpl.Update(student, connection);
            teacher = personDaoImpl.Read(teacher, connection)[0];
            student = personDaoImpl.Read(student, connection)[0];
        }

        public Person SimpleTeacher()
        {
            string firstName = "Aaron";
            string lastName = "Burton";
            DateTime birthDate = new DateTime(1985, 6, 9);
            Enumeration gender = Enum_Gender.values["male"];
            Enumeration ethnic = Enum_Ethnic.values["cau"];

            return new Person(firstName, lastName, birthDate, gender, ethnic);
        }

        public Person SimpleStudent()
        {
            string firstName = "Dustin";
            string lastName = "Burton";
            DateTime birthDate = new DateTime(2008, 6, 5);
            Enumeration gender = Enum_Gender.values["male"];
            Enumeration ethnic = Enum_Ethnic.values["cau"];

            return new Person(firstName, lastName, birthDate, gender, ethnic);
        }

        public UniqueIdentifier SimpleUniqueIdentifier()
        {
            string identifier = DateTime.Now.ToString();
            Enumeration enumIdentifier = Enum_Identifier.values["badge id"];
            Enumeration isActive = Enum_True_False.values["true"];
            UniqueIdentifier uniqueId = new UniqueIdentifier(identifier, enumIdentifier, isActive);

            return uniqueId;
        }

        public Provider SimpleProvider()
        {
            string name = "Simple Provider";
            Provider provider = new Provider(name);

            return provider;
        }

        public Application SimpleApplication(Provider provider)
        {
            string name = "Simple Application";
            Application application = new Application(name, provider);

            return application;
        }

        public PhoneNumber SimplePhoneNumber()
        {
            string phoneNumberValue = "5175282647";
            Enumeration isPrimary = Enum_True_False.values["true"];
            Enumeration phoneType = Enum_Phone.values["cell"];
            PhoneNumber phoneNumber = new PhoneNumber(phoneNumberValue, isPrimary, phoneType);

            return phoneNumber;
        }

        public Address SimpleAddress()
        {
            string line1 = "Simple Address";
            string city = "NewTown";
            Enumeration stateProvince = Enum_State_Province.values["mi"];
            Enumeration country = Enum_Country.values["us"];
            Enumeration isCurrent = Enum_True_False.values["true"];
            Enumeration addressType = Enum_Address.values["home"];
            Address address = new Address(line1, city, stateProvince, country, isCurrent, addressType);

            return address;
        }

        public Organization SimpleOrg()
        {
            string name = "Simple Org";
            Enumeration orgType = Enum_Organization.values["school"];
            string identifier = "SO";
            Organization org = new Organization(name, orgType, identifier);

            return org;
        }

        public EmploymentSession SimpleEmploymentSession(Organization org)
        {
            string name = "Simple Employment Session";
            Enumeration isPrimary = Enum_True_False.values["true"];
            EmploymentSession employmentSession = new EmploymentSession(name, isPrimary, org);

            return employmentSession;
        }

        public AcademicSession SimpleAcademicSession(Organization org)
        {
            string name = "Simple Academic Session";
            Enumeration sessionType = Enum_Session.values["school year"];
            string termCode = "T1";
            AcademicSession academicSession = new AcademicSession(name, sessionType, org, termCode);

            return academicSession;
        }

        public Course SimpleCourse()
        {
            string name = "Simple Course";
            string courseCode = "SC";
            Course course = new Course(name, courseCode);
            return course;
        }

        public ClassEnrolled SimpleClassEnrolled(Course course, AcademicSession academicSession)
        {
            string name = "Simple Class";
            string classCode = "boo";
            Enumeration classType = Enum_Class.values["scheduled"];
            string room = "here";
            ClassEnrolled classEnrolled = new ClassEnrolled(name, classCode, classType, room, course, academicSession);

            return classEnrolled;
        }

        public Enrollment SimpleEnrollment(ClassEnrolled classEnrolled, AcademicSession academicSession)
        {
            List<AcademicSession> academicSessions = new List<AcademicSession>();
            academicSessions.Add(academicSession);
            Enumeration gradeLevel = Enum_Grade.values["1"];
            DateTime startDate = new DateTime();
            Enumeration isPrimary = Enum_True_False.values["true"];
            Enrollment enrollment = new Enrollment(gradeLevel, startDate, null, isPrimary, classEnrolled, academicSessions);
            return enrollment;
        }

        public LineItem SimpleLineItem(ClassEnrolled classEnrolled)
        {
            string name = "test";
            DateTime assignDate = new DateTime();
            DateTime dueDate = new DateTime();
            Enumeration category = Enum_Category.values["test"];
            double markMin = 0.00;
            double markMax = 100.00;
            LineItem lineItem = new LineItem(name, assignDate, dueDate, classEnrolled, category, markMin, markMax);

            return lineItem;
        }
    }
}
