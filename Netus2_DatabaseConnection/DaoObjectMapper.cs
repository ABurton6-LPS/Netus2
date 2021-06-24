using Netus2.daoImplementations;
using Netus2.daoInterfaces;
using Netus2.daoObjects;
using Netus2.dbAccess;
using Netus2.enumerations;
using System;
using System.Collections.Generic;

namespace Netus2
{
    public class DaoObjectMapper
    {
        public PersonDao MapPerson(Person person)
        {
            PersonDao personDao = new PersonDao();

            if (person.Id != -1)
                personDao.person_id = person.Id;
            else
                personDao.person_id = null;

            personDao.first_name = person.FirstName;
            personDao.middle_name = person.MiddleName;
            personDao.last_name = person.LastName;
            personDao.birth_date = person.BirthDate;
            personDao.enum_gender_id = person.Gender?.Id;
            personDao.enum_ethnic_id = person.Ethnic?.Id;
            personDao.enum_residence_status_id = person.ResidenceStatus?.Id;
            personDao.login_name = person.LoginName;
            personDao.login_pw = person.LoginPw;

            return personDao;
        }

        public Person MapPerson(PersonDao personDao)
        {
            Enumeration gender = Enum_Gender.GetEnumFromId((int)personDao.enum_gender_id);
            Enumeration ethnic = Enum_Ethnic.GetEnumFromId((int)personDao.enum_ethnic_id);

            Person person = new Person(personDao.first_name, personDao.last_name, (DateTime)personDao.birth_date, gender, ethnic);

            person.Id = personDao.person_id != null ? (int)personDao.person_id : -1;
            person.MiddleName = personDao.middle_name;
            person.ResidenceStatus = personDao.enum_residence_status_id != null ? Enum_Residence_Status.GetEnumFromId((int)personDao.enum_residence_status_id) : null;
            person.LoginName = personDao.login_name;
            person.LoginPw = personDao.login_pw;

            return person;
        }

        public PhoneNumberDao MapPhoneNumber(PhoneNumber phoneNumber, int personId)
        {
            PhoneNumberDao phoneNumberDao = new PhoneNumberDao();

            if (phoneNumber.Id != -1)
                phoneNumberDao.phone_number_id = phoneNumber.Id;
            else
                phoneNumberDao.phone_number_id = null;
            
            if (personId != -1)
                phoneNumberDao.person_id = personId;
            else
                phoneNumberDao.person_id = null;

            phoneNumberDao.phone_number = phoneNumber.PhoneNumberValue;
            phoneNumberDao.is_primary_id = phoneNumber.IsPrimary?.Id;
            phoneNumberDao.enum_phone_id = phoneNumber.PhoneType?.Id;

            return phoneNumberDao;
        }

        public PhoneNumber MapPhoneNumber(PhoneNumberDao phoneNumberDao)
        {
            Enumeration isPrimary = Enum_True_False.GetEnumFromId((int)phoneNumberDao.is_primary_id);
            Enumeration phoneType = Enum_Phone.GetEnumFromId((int)phoneNumberDao.enum_phone_id);
            PhoneNumber phoneNumber = new PhoneNumber(phoneNumberDao.phone_number, isPrimary, phoneType);

            phoneNumber.Id = phoneNumberDao.phone_number_id != null ? (int)phoneNumberDao.phone_number_id : -1;

            return phoneNumber;
        }

        public ProviderDao MapProvider(Provider provider, int parentId)
        {
            ProviderDao providerDao = new ProviderDao();

            if (provider.Id != -1)
                providerDao.provider_id = provider.Id;
            else
                providerDao.provider_id = null;

            providerDao.name = provider.Name;
            providerDao.url_standard_access = provider.UrlStandardAccess;
            providerDao.url_admin_access = provider.UrlAdminAccess;
            providerDao.populated_by = provider.PopulatedBy;
            
            if (parentId != -1)
                providerDao.parent_provider_id = parentId;
            else
                providerDao.parent_provider_id = null;

            return providerDao;
        }

        public Provider MapProvider(ProviderDao providerDao)
        {
            Provider provider = new Provider(providerDao.name);
            provider.Id = providerDao.provider_id != null ? (int)providerDao.provider_id : -1;
            provider.UrlStandardAccess = providerDao.url_standard_access;
            provider.UrlAdminAccess = providerDao.url_admin_access;
            provider.PopulatedBy = providerDao.populated_by;

            return provider;
        }

        public ApplicationDao MapApp(Application application)
        {
            ApplicationDao appDao = new ApplicationDao();
            if (application.Id != -1)
                appDao.app_id = application.Id;
            else
                appDao.app_id = null;

            appDao.name = application.Name;
            
            if (application.Provider != null && application.Provider.Id != -1)
                appDao.provider_id = application.Provider.Id;
            else
                appDao.provider_id = null;

            return appDao;
        }

        public Application MapApp(ApplicationDao appDao, Provider provider)
        {
            Application application = new Application(appDao.name, provider);
            application.Id = appDao.app_id != null ? (int)appDao.app_id : -1;

            return application;
        }

        public AddressDao MapAddress(Address address)
        {
            AddressDao addressDao = new AddressDao();
            if (address.Id != -1)
                addressDao.address_id = address.Id;
            else
                addressDao.address_id = null;

            addressDao.address_line_1 = address.Line1;
            addressDao.address_line_2 = address.Line2;
            addressDao.address_line_3 = address.Line3;
            addressDao.address_line_4 = address.Line4;
            addressDao.apartment = address.Apartment;
            addressDao.city = address.City;
            addressDao.enum_state_province_id = address.StateProvince?.Id;
            addressDao.postal_code = address.PostalCode;
            addressDao.enum_country_id = address.Country?.Id;
            addressDao.is_current_id = address.IsCurrent?.Id;
            addressDao.enum_address_id = address.AddressType?.Id;

            return addressDao;
        }

        public Address MapAddress(AddressDao addressDao)
        {
            Enumeration stateProvince = Enum_State_Province.GetEnumFromId((int)addressDao.enum_state_province_id);
            Enumeration country = Enum_Country.GetEnumFromId((int)addressDao.enum_country_id);
            Enumeration isCurrent = Enum_True_False.GetEnumFromId((int)addressDao.is_current_id);
            Enumeration addressType = Enum_Address.GetEnumFromId((int)addressDao.enum_address_id);

            Address address = new Address(addressDao.address_line_1, addressDao.city, stateProvince, country, isCurrent, addressType);

            address.Id = addressDao.address_id != null ? (int)addressDao.address_id : -1;
            address.Line2 = addressDao.address_line_2;
            address.Line3 = addressDao.address_line_3;
            address.Line4 = addressDao.address_line_4;
            address.Apartment = addressDao.apartment;
            address.PostalCode = addressDao.postal_code;

            return address;
        }

        public UniqueIdentifierDao MapUniqueIdentifier(UniqueIdentifier uniqueId, int personId)
        {
            UniqueIdentifierDao uniqueIdDao = new UniqueIdentifierDao();

            if (uniqueId.Id != -1)
                uniqueIdDao.unique_identifier_id = uniqueId.Id;
            else
                uniqueIdDao.unique_identifier_id = null;
            
            if (personId != -1)
                uniqueIdDao.person_id = personId;
            else
                uniqueIdDao.person_id = null;
            
            uniqueIdDao.unique_identifier = uniqueId.Identifier;
            uniqueIdDao.enum_identifier_id = uniqueId.IdentifierType?.Id;
            uniqueIdDao.is_active_id = uniqueId.IsActive?.Id;

            return uniqueIdDao;
        }

        public UniqueIdentifier MapUniqueIdentifier(UniqueIdentifierDao uniqueIdDao)
        {
            Enumeration identifierType = Enum_Identifier.GetEnumFromId((int)uniqueIdDao.enum_identifier_id);
            Enumeration isActive = Enum_True_False.GetEnumFromId((int)uniqueIdDao.is_active_id);
            UniqueIdentifier uniqueId = new UniqueIdentifier(uniqueIdDao.unique_identifier, identifierType, isActive);

            uniqueId.Id = uniqueIdDao.unique_identifier_id != null ? (int)uniqueIdDao.unique_identifier_id : -1;

            return uniqueId;
        }

        public OrganizationDao MapOrganization(Organization organization, int parentOrganizationId)
        {
            OrganizationDao orgDao = new OrganizationDao();

            if (organization.Id != -1)
                orgDao.organization_id = organization.Id;
            else
                orgDao.organization_id = null;

            orgDao.name = organization.Name;
            orgDao.enum_organization_id = organization.OrganizationType?.Id;
            orgDao.identifier = organization.Identifier;
            orgDao.building_code = organization.BuildingCode;

            if (parentOrganizationId != -1)
                orgDao.organization_parent_id = parentOrganizationId;
            else
                orgDao.organization_parent_id = null;

            return orgDao;
        }

        public Organization MapOrganization(OrganizationDao organizationDao)
        {
            Enumeration orgType = Enum_Organization.GetEnumFromId((int)organizationDao.enum_organization_id);
            Organization org = new Organization(organizationDao.name, orgType, organizationDao.identifier);
            org.BuildingCode = organizationDao.building_code;
            org.Id = organizationDao.organization_id != null ? (int)organizationDao.organization_id : -1;

            return org;
        }

        public EmploymentSessionDao MapEmploymentSession_WithPersonId(EmploymentSession employmentSession, int personId)
        {
            EmploymentSessionDao esDao = new EmploymentSessionDao();

            if (employmentSession.Id != -1)
                esDao.employment_session_id = employmentSession.Id;
            else
                esDao.employment_session_id = null;

            esDao.name = employmentSession.Name;
            
            if (personId != -1)
                esDao.person_id = personId;
            else
                esDao.person_id = null;

            esDao.start_date = employmentSession.StartDate;
            esDao.end_date = employmentSession.EndDate;
            esDao.is_primary_id = employmentSession.IsPrimary?.Id;
            esDao.enum_session_id = employmentSession.GetSessionType()?.Id;

            if (employmentSession.Organization != null && employmentSession.Organization.Id != -1)
                esDao.organization_id = employmentSession.Organization.Id;
            else
                esDao.organization_id = null;

            return esDao;
        }

        public EmploymentSessionDao MapEmploymentSession_WithOrganizationId(EmploymentSession employmentSession, int organizationId)
        {
            EmploymentSessionDao esDao = new EmploymentSessionDao();

            if (employmentSession.Id != -1)
                esDao.employment_session_id = employmentSession.Id;
            else
                esDao.employment_session_id = null;

            esDao.name = employmentSession.Name;
            esDao.person_id = null;
            esDao.start_date = employmentSession.StartDate;
            esDao.end_date = employmentSession.EndDate;
            esDao.is_primary_id = employmentSession.IsPrimary?.Id;
            esDao.enum_session_id = employmentSession.GetSessionType()?.Id;
            
            if (organizationId != -1)
                esDao.organization_id = organizationId;
            else
                esDao.organization_id = null;

            return esDao;
        }

        public EmploymentSession MapEmploymentSession(EmploymentSessionDao employmentSessionDao, Organization org)
        {
            string name = employmentSessionDao.name;
            Enumeration isPrimary = Enum_True_False.values["true"];
            EmploymentSession es = new EmploymentSession(name, isPrimary, org);

            es.Id = employmentSessionDao.employment_session_id != null ? (int)employmentSessionDao.employment_session_id : -1;
            es.StartDate = (DateTime)employmentSessionDao.start_date;
            es.EndDate = (DateTime)employmentSessionDao.end_date;
            es.IsPrimary = Enum_True_False.GetEnumFromId((int)employmentSessionDao.is_primary_id);

            return es;
        }

        public EnrollmentDao MapEnrollment(Enrollment enrollment, int personId)
        {
            EnrollmentDao enrollmentDao = new EnrollmentDao();

            if (enrollment.Id != -1)
                enrollmentDao.enrollment_id = enrollment.Id;
            else
                enrollmentDao.enrollment_id = null;

            if (personId != -1)
                enrollmentDao.person_id = personId;
            else
                enrollmentDao.person_id = null;

            if (enrollment.ClassEnrolled != null && enrollment.ClassEnrolled.Id != -1)
                enrollmentDao.class_id = enrollment.ClassEnrolled.Id;
            else
                enrollmentDao.class_id = null;

            enrollmentDao.enum_grade_id = enrollment.GradeLevel?.Id;
            enrollmentDao.start_date = enrollment.StartDate;
            enrollmentDao.end_date = enrollment.EndDate;
            enrollmentDao.is_primary_id = enrollment.IsPrimary?.Id;

            return enrollmentDao;
        }

        public Enrollment MapEnrollment(EnrollmentDao enrollmentDao, ClassEnrolled classEnrolled, List<AcademicSession> academicSessions)
        {
            Enumeration gradeLevel = Enum_Grade.GetEnumFromId((int)enrollmentDao.enum_grade_id);
            DateTime startDate = (DateTime)enrollmentDao.start_date;
            DateTime? endDate = enrollmentDao.end_date;
            Enumeration isPrimary = Enum_True_False.GetEnumFromId((int)enrollmentDao.is_primary_id);

            Enrollment enrollment = new Enrollment(gradeLevel, startDate, endDate, isPrimary, classEnrolled, academicSessions);
            enrollment.Id = enrollmentDao.enrollment_id != null ? (int)enrollmentDao.enrollment_id : -1;

            return enrollment;
        }

        public ClassEnrolledDao MapClassEnrolled(ClassEnrolled classEnrolled)
        {
            ClassEnrolledDao classEnrolledDao = new ClassEnrolledDao();

            if (classEnrolled.Id != -1)
                classEnrolledDao.class_id = classEnrolled.Id;
            else
                classEnrolledDao.class_id = null;

            classEnrolledDao.name = classEnrolled.Name;
            classEnrolledDao.class_code = classEnrolled.ClassCode;
            classEnrolledDao.enum_class_id = classEnrolled.ClassType?.Id;
            classEnrolledDao.room = classEnrolled.Room;
            
            if (classEnrolled.Course != null && classEnrolled.Course.Id != -1)
                classEnrolledDao.course_id = classEnrolled.Course.Id;
            else
                classEnrolledDao.course_id = null;

            if (classEnrolled.AcademicSession != null && classEnrolled.AcademicSession.Id != -1)
                classEnrolledDao.academic_session_id = classEnrolled.AcademicSession.Id;
            else
                classEnrolledDao.academic_session_id = null;

            return classEnrolledDao;
        }

        public ClassEnrolled MapClassEnrolled(ClassEnrolledDao classEnrolledDao, AcademicSession academicSession, Course course)
        {
            string name = classEnrolledDao.name;
            string classCode = classEnrolledDao.class_code;
            Enumeration classType = Enum_Class.GetEnumFromId((int)classEnrolledDao.enum_class_id);
            string room = classEnrolledDao.room;

            ClassEnrolled classEnrolled = new ClassEnrolled(name, classCode, classType, room, course, academicSession);
            classEnrolled.Id = classEnrolledDao.class_id != null ? (int)classEnrolledDao.class_id : -1;

            return classEnrolled;
        }

        public CourseDao MapCourse(Course course)
        {
            CourseDao courseDao = new CourseDao();

            if (course.Id != -1)
                courseDao.course_id = course.Id;
            else
                courseDao.course_id = null;

            courseDao.name = course.Name;
            courseDao.course_code = course.CourseCode;

            return courseDao;
        }

        public Course MapCourse(CourseDao courseDao)
        {
            string name = courseDao.name;
            string courseCode = courseDao.course_code;

            Course course = new Course(name, courseCode);
            course.Id = courseDao.course_id != null ? (int)courseDao.course_id : -1;

            return course;
        }

        public AcademicSessionDao MapAcademicSession(AcademicSession academicSession, int parentId)
        {
            AcademicSessionDao academicSessionDao = new AcademicSessionDao();

            if (academicSession.Id != -1)
                academicSessionDao.academic_session_id = academicSession.Id;
            else
                academicSessionDao.academic_session_id = null;

            academicSessionDao.name = academicSession.Name;
            academicSessionDao.start_date = academicSession.StartDate;
            academicSessionDao.end_date = academicSession.EndDate;
            academicSessionDao.enum_session_id = academicSession.SessionType?.Id;
            
            if (parentId != -1)
                academicSessionDao.parent_session_id = parentId;
            else
                academicSessionDao.parent_session_id = null;

            if (academicSession.Organization != null && academicSession.Organization.Id != -1)
                academicSessionDao.organization_id = academicSession.Organization.Id;
            else
                academicSessionDao.organization_id = null;

            return academicSessionDao;
        }

        public AcademicSession MapAcademicSession(AcademicSessionDao academicSessionDao, Organization org)
        {
            string name = academicSessionDao.name;
            Enumeration sessionType = Enum_Session.GetEnumFromId((int)academicSessionDao.enum_session_id);
            AcademicSession academicSession = new AcademicSession(name, sessionType, org);

            academicSession.Id = academicSessionDao.academic_session_id != null ? (int)academicSessionDao.academic_session_id : -1;
            academicSession.StartDate = (DateTime)academicSessionDao.start_date;
            academicSession.EndDate = (DateTime)academicSessionDao.end_date;

            return academicSession;
        }

        public ResourceDao MapResource(Resource resource)
        {
            ResourceDao resourceDao = new ResourceDao();

            if (resource.Id != -1)
                resourceDao.resource_id = resource.Id;
            else
                resourceDao.resource_id = null;

            resourceDao.name = resource.Name;
            resourceDao.enum_importance_id = resource.Importance?.Id;
            resourceDao.vendor_resource_identification = resource.VendorResourceId;
            resourceDao.vendor_identification = resource.VendorId;
            resourceDao.application_identification = resource.ApplicationId;

            return resourceDao;
        }

        public Resource MapResource(ResourceDao resourceDao)
        {
            string name = resourceDao.name;
            string vendorResourceId = resourceDao.vendor_resource_identification;

            Resource resource = new Resource(name, vendorResourceId);

            resource.Id = resourceDao.resource_id != null ? (int)resourceDao.resource_id : -1;
            resource.Importance = resourceDao.enum_importance_id != null ? Enum_Importance.GetEnumFromId((int)resourceDao.enum_importance_id) : null;
            resource.VendorId = resourceDao.vendor_identification;
            resource.ApplicationId = resourceDao.application_identification;

            return resource;
        }

        public MarkDao MapMark(Mark mark, int personId)
        {
            MarkDao markDao = new MarkDao();

            if (mark.Id != -1)
                markDao.mark_id = mark.Id;
            else
                markDao.mark_id = null;

            if (mark.LineItem != null && mark.LineItem.Id != -1)
                markDao.lineitem_id = mark.LineItem.Id;
            else
                markDao.lineitem_id = null;

            if (personId != -1)
                markDao.person_id = personId;
            else
                markDao.person_id = null;

            markDao.enum_score_status_id = mark.ScoreStatus?.Id;
            markDao.score = mark.Score;
            markDao.score_date = mark.ScoreDate;
            markDao.comment = mark.Comment;

            return markDao;
        }

        public Mark MapMark(MarkDao markDao, LineItem lineItem)
        {
            Enumeration scoreStatus = markDao.enum_score_status_id != null ?
                Enum_Score_Status.GetEnumFromId((int)markDao.enum_score_status_id) : null;
            double score = (double)markDao.score;
            DateTime scoreDate = (DateTime)markDao.score_date;

            Mark mark = new Mark(lineItem, scoreStatus, score, scoreDate);
            mark.Id = markDao.mark_id != null ? (int)markDao.mark_id : -1;

            return mark;
        }

        public LineItemDao MapLineItem(LineItem lineItem)
        {
            LineItemDao lineItemDao = new LineItemDao();

            if (lineItem.Id != -1)
                lineItemDao.lineitem_id = lineItem.Id;
            else
                lineItemDao.lineitem_id = null;

            lineItemDao.name = lineItem.Name;
            lineItemDao.descript = lineItem.Descript;
            lineItemDao.assign_date = lineItem.AssignDate;
            lineItemDao.due_date = lineItem.DueDate;
            
            if (lineItem.ClassAssigned != null && lineItem.ClassAssigned.Id != -1)
                lineItemDao.class_id = lineItem.ClassAssigned.Id;
            else
                lineItemDao.class_id = null;
            
            lineItemDao.enum_category_id = lineItem.Category?.Id;
            lineItemDao.markValueMin = lineItem.MarkValueMin;
            lineItemDao.markValueMax = lineItem.MarkValueMax;

            return lineItemDao;
        }

        public LineItem MapLineItem(LineItemDao lineItemDao, ClassEnrolled classEnrolled)
        {
            string name = lineItemDao.name;
            DateTime assignDate = (DateTime)lineItemDao.assign_date;
            DateTime dueDate = (DateTime)lineItemDao.due_date;
            Enumeration category = lineItemDao.enum_category_id != null ? Enum_Category.GetEnumFromId((int)lineItemDao.enum_category_id) : null;
            double markMin = (double)lineItemDao.markValueMin;
            double markMax = (double)lineItemDao.markValueMax;

            LineItem lineItem = new LineItem(name, assignDate, dueDate, classEnrolled, category, markMin, markMax);
            lineItem.Id = lineItemDao.lineitem_id != null ? (int)lineItemDao.lineitem_id : -1;

            return lineItem;
        }
    }
}