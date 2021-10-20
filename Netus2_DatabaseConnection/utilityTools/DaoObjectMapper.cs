﻿using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.enumerations;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.utilityTools
{
    public class DaoObjectMapper
    {
        public DataRow MapPerson(Person person)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_Person().NewRow();

            if (person.Id != -1)
                row["person_id"] = person.Id;
            else
                row["person_id"] = DBNull.Value;

            if (person.FirstName != null)
                row["first_name"] = person.FirstName;
            else
                row["first_name"] = DBNull.Value;

            if (person.MiddleName != null)
                row["middle_name"] = person.MiddleName;
            else
                row["middle_name"] = DBNull.Value;

            if (person.LastName != null)
                row["last_name"] = person.LastName;
            else
                row["last_name"] = DBNull.Value;

            if (person.BirthDate != new DateTime())
                row["birth_date"] = person.BirthDate;
            else
                row["birth_date"] = DBNull.Value;

            if (person.Gender != null)
                row["enum_gender_id"] = person.Gender.Id;
            else
                row["enum_gender_id"] = DBNull.Value;

            if (person.Ethnic != null)
                row["enum_ethnic_id"] = person.Ethnic.Id;
            else
                row["enum_ethnic_id"] = DBNull.Value;

            if (person.ResidenceStatus != null)
                row["enum_residence_status_id"] = person.ResidenceStatus.Id;
            else
                row["enum_residence_status_id"] = DBNull.Value;

            if (person.LoginName != null)
                row["login_name"] = person.LoginName;
            else
                row["login_name"] = DBNull.Value;

            if (person.LoginPw != null)
                row["login_pw"] = person.LoginPw;
            else
                row["login_pw"] = DBNull.Value;

            return row;
        }

        public Person MapPerson(DataRow row)
        {
            Enumeration gender = null;
            if(row["enum_gender_id"] != DBNull.Value)
                gender = Enum_Gender.GetEnumFromId((int)row["enum_gender_id"]);

            Enumeration ethnic = null;
            if(row["enum_ethnic_id"] != DBNull.Value)
                ethnic = Enum_Ethnic.GetEnumFromId((int)row["enum_ethnic_id"]);

            string firstName = null;
            if (row["first_name"] != DBNull.Value)
                firstName = (string)row["first_name"];

            string lastName = null;
            if (row["last_name"] != DBNull.Value)
                lastName = (string)row["last_name"];

            DateTime birthDate = new DateTime();
            if (row["birth_date"] != DBNull.Value)
                birthDate = (DateTime)row["birth_date"];

            Person person = new Person(firstName, lastName, birthDate, gender, ethnic);

            if (row["person_id"] != DBNull.Value)
                person.Id = (int)row["person_id"];
            else
                person.Id = -1;

            if (row["middle_name"] != DBNull.Value)
                person.MiddleName = (string)row["middle_name"];
            else
                person.MiddleName = null;

            if (row["enum_residence_status_id"] != DBNull.Value)
                person.ResidenceStatus = Enum_Residence_Status.GetEnumFromId((int)row["enum_residence_status_id"]);
            else
                person.ResidenceStatus = null;

            if (row["login_name"] != DBNull.Value)
                person.LoginName = (string)row["login_name"];
            else
                person.LoginName = null;

            if (row["login_pw"] != DBNull.Value)
                person.LoginPw = (string)row["login_pw"];
            else
                person.LoginPw = null;

            return person;
        }

        public DataRow MapPhoneNumber(PhoneNumber phoneNumber, int personId)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_PhoneNumber().NewRow();

            if (phoneNumber.Id != -1)
                row["phone_number_id"] = phoneNumber.Id;
            else
                row["phone_number_id"] = DBNull.Value;
            
            if (personId != -1)
                row["person_id"] = personId;
            else
                row["person_id"] = DBNull.Value;

            if (phoneNumber.PhoneNumberValue != null)
                row["phone_number"] = phoneNumber.PhoneNumberValue;
            else
                row["phone_number"] = DBNull.Value;

            if (phoneNumber.IsPrimary != null)
                row["is_primary_id"] = phoneNumber.IsPrimary.Id;
            else
                row["is_primary_id"] = DBNull.Value;

            if (phoneNumber.PhoneType != null)
                row["enum_phone_id"] = phoneNumber.PhoneType.Id;
            else
                row["enum_phone_id"] = DBNull.Value;

            return row;
        }

        public PhoneNumber MapPhoneNumber(DataRow row)
        {
            Enumeration isPrimary = null;
            if(row["is_primary_id"] != DBNull.Value)
                isPrimary = Enum_True_False.GetEnumFromId((int)row["is_primary_id"]);

            Enumeration phoneType = null;
            if(row["enum_phone_id"] != DBNull.Value)
                phoneType = Enum_Phone.GetEnumFromId((int)row["enum_phone_id"]);

            string strPhoneNumber = null;
            if (row["phone_number"] != DBNull.Value)
                strPhoneNumber = (string)row["phone_number"];

            PhoneNumber phoneNumber = new PhoneNumber(strPhoneNumber, isPrimary, phoneType);

            if (row["phone_number_id"] != DBNull.Value)
                phoneNumber.Id = (int)row["phone_number_id"];
            else
                phoneNumber.Id = -1;

            return phoneNumber;
        }

        public DataRow MapProvider(Provider provider, int parentId)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_Provider().NewRow();

            if (provider.Id != -1)
                row["provider_id"] = provider.Id;
            else
                row["provider_id"] = DBNull.Value;

            if (provider.Name != null)
                row["name"] = provider.Name;
            else
                row["name"] = DBNull.Value;

            if (provider.UrlStandardAccess != null)
                row["url_standard_access"] = provider.UrlStandardAccess;
            else
                row["url_standard_access"] = DBNull.Value;

            if (provider.UrlAdminAccess != null)
                row["url_admin_access"] = provider.UrlAdminAccess;
            else
                row["url_admin_access"] = DBNull.Value;

            if (provider.PopulatedBy != null)
                row["populated_by"] = provider.PopulatedBy;
            else
                row["populated_by"] = DBNull.Value;
            
            if (parentId != -1)
                row["parent_provider_id"] = parentId;
            else
                row["parent_provider_id"] = DBNull.Value;

            return row;
        }

        public Provider MapProvider(DataRow row)
        {
            string name = null;
            if (row["name"] != DBNull.Value)
                name = (string)row["name"];

            Provider provider = new Provider(name);

            if (row["provider_id"] != DBNull.Value)
                provider.Id = (int)row["provider_id"];
            else
                provider.Id = -1;

            if (row["url_standard_access"] != DBNull.Value)
                provider.UrlStandardAccess = (string)row["url_standard_access"];
            else
                provider.UrlStandardAccess = null;

            if (row["url_admin_access"] != DBNull.Value)
                provider.UrlAdminAccess = (string)row["url_admin_access"];
            else
                provider.UrlAdminAccess = null;

            if (row["populated_by"] != DBNull.Value)
                provider.PopulatedBy = (string)row["populated_by"];
            else
                provider.PopulatedBy = null;

            return provider;
        }

        public DataRow MapApp(Application application)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_Application().NewRow();

            if (application.Id != -1)
                row["app_id"] = application.Id;
            else
                row["app_id"] = DBNull.Value;

            if (application.Name != null)
                row["name"] = application.Name;
            else
                row["name"] = DBNull.Value;
            
            if (application.Provider != null && application.Provider.Id != -1)
                row["provider_id"] = application.Provider.Id;
            else
                row["provider_id"] = DBNull.Value;

            return row;
        }

        public Application MapApp(DataRow row, Provider provider)
        {
            string name = null;
            if (row["name"] != DBNull.Value)
                name = (string)row["name"];

            Application application = new Application(name, provider);

            if (row["app_id"] != DBNull.Value)
                application.Id = (int)row["app_id"];
            else
                application.Id = -1;

            return application;
        }

        public DataRow MapAddress(Address address)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_Address().NewRow();

            if (address.Id != -1)
                row["address_id"] = address.Id;
            else
                row["address_id"] = DBNull.Value;

            if (address.Line1 != null)
                row["address_line_1"] = address.Line1;
            else
                row["address_line_1"] = DBNull.Value;

            if (address.Line2 != null)
                row["address_line_2"] = address.Line2;
            else
                row["address_line_2"] = DBNull.Value;

            if (address.Line3 != null)
                row["address_line_3"] = address.Line3;
            else
                row["address_line_3"] = DBNull.Value;

            if (address.Line4 != null)
                row["address_line_4"] = address.Line4;
            else
                row["address_line_4"] = DBNull.Value;

            if (address.Apartment != null)
                row["apartment"] = address.Apartment;
            else
                row["apartment"] = DBNull.Value;

            if (address.City != null)
                row["city"] = address.City;
            else
                row["city"] = DBNull.Value;

            if (address.StateProvince != null)
                row["enum_state_province_id"] = address.StateProvince.Id;
            else
                row["enum_state_province_id"] = DBNull.Value;

            if (address.PostalCode != null)
                row["postal_code"] = address.PostalCode;
            else
                row["postal_code"] = DBNull.Value;

            if (address.Country != null)
                row["enum_country_id"] = address.Country.Id;
            else
                row["enum_country_id"] = DBNull.Value;

            if (address.IsCurrent != null)
                row["is_current_id"] = address.IsCurrent.Id;
            else
                row["is_current_id"] = DBNull.Value;

            if (address.AddressType != null)
                row["enum_address_id"] = address.AddressType.Id;
            else
                row["enum_address_id"] = DBNull.Value;

            return row;
        }

        public Address MapAddress(DataRow row)
        {
            Enumeration stateProvince = null;
            if(row["enum_state_province_id"] != DBNull.Value)
                stateProvince = Enum_State_Province.GetEnumFromId((int)row["enum_state_province_id"]);

            Enumeration country = null;
            if(row["enum_country_id"] != DBNull.Value)
                country = Enum_Country.GetEnumFromId((int)row["enum_country_id"]);

            Enumeration isCurrent = null;
            if (row["is_current_id"] != DBNull.Value)
                isCurrent = Enum_True_False.GetEnumFromId((int)row["is_current_id"]);

            Enumeration addressType = null;
            if (row["enum_address_id"] != DBNull.Value)
                addressType = Enum_Address.GetEnumFromId((int)row["enum_address_id"]);

            string line1 = null;
            if (row["address_line_1"] != DBNull.Value)
                line1 = (string)row["address_line_1"];

            string city = null;
            if (row["city"] != DBNull.Value)
                city = (string)row["city"];

            Address address = new Address(line1, city, stateProvince, country, isCurrent, addressType);

            if (row["address_id"] != DBNull.Value)
                address.Id = (int)row["address_id"];
            else
                address.Id = -1;

            if (row["address_line_2"] != DBNull.Value)
                address.Line2 = (string)row["address_line_2"];
            else
                address.Line2 = null;

            if (row["address_line_3"] != DBNull.Value)
                address.Line3 = (string)row["address_line_3"];
            else
                address.Line3 = null;

            if (row["address_line_4"] != DBNull.Value)
                address.Line4 = (string)row["address_line_4"];
            else
                address.Line4 = null;

            if (row["apartment"] != DBNull.Value)
                address.Apartment = (string)row["apartment"];
            else
                address.Apartment = null;

            if (row["postal_code"] != DBNull.Value)
                address.PostalCode = (string)row["postal_code"];
            else
                address.PostalCode = null;

            return address;
        }

        public DataRow MapUniqueIdentifier(UniqueIdentifier uniqueId, int personId)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_UniqueIdentifier().NewRow();

            if (uniqueId.Id != -1)
                row["unique_identifier_id"] = uniqueId.Id;
            else
                row["unique_identifier_id"] = DBNull.Value;
            
            if (personId != -1)
                row["person_id"] = personId;
            else
                row["person_id"] = DBNull.Value;

            if (uniqueId.Identifier != null)
                row["unique_identifier"] = uniqueId.Identifier;
            else
                row["unique_identifier"] = DBNull.Value;

            if (uniqueId.IdentifierType != null)
                row["enum_identifier_id"] = uniqueId.IdentifierType.Id;
            else
                row["enum_identifier_id"] = DBNull.Value;

            if (uniqueId.IsActive != null)
                row["is_active_id"] = uniqueId.IsActive.Id;
            else
                row["is_active_id"] = DBNull.Value;

            return row;
        }

        public UniqueIdentifier MapUniqueIdentifier(DataRow row)
        {
            Enumeration identifierType = null;
            if (row["enum_identifier_id"] != DBNull.Value)
                identifierType = Enum_Identifier.GetEnumFromId((int)row["enum_identifier_id"]);

            Enumeration isActive = null;
            if(row["is_active_id"] != DBNull.Value)
                isActive = Enum_True_False.GetEnumFromId((int)row["is_active_id"]);

            string uniqueIdentifier = null;
            if (row["unique_identifier"] != DBNull.Value)
                uniqueIdentifier = (string)row["unique_identifier"];

            UniqueIdentifier uniqueId = new UniqueIdentifier(uniqueIdentifier, identifierType, isActive);

            if (row["unique_identifier_id"] != DBNull.Value)
                uniqueId.Id = (int)row["unique_identifier_id"];
            else
                uniqueId.Id = -1;

            return uniqueId;
        }

        public DataRow MapOrganization(Organization organization, int parentOrganizationId)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_Organization().NewRow();

            if (organization.Id != -1)
                row["organization_id"] = organization.Id;
            else
                row["organization_id"] = DBNull.Value;

            if (organization.Name != null)
                row["name"] = organization.Name;
            else
                row["name"] = DBNull.Value;

            if (organization.OrganizationType != null)
                row["enum_organization_id"] = organization.OrganizationType.Id;
            else
                row["enum_organization_id"] = DBNull.Value;

            if (organization.Identifier != null)
                row["identifier"] = organization.Identifier;
            else
                row["identifier"] = DBNull.Value;

            if (organization.SisBuildingCode != null)
                row["sis_building_code"] = organization.SisBuildingCode;
            else
                row["sis_building_code"] = DBNull.Value;

            if (organization.HrBuildingCode != null)
                row["hr_building_code"] = organization.HrBuildingCode;
            else
                row["hr_building_code"] = DBNull.Value;

            if (parentOrganizationId != -1)
                row["organization_parent_id"] = parentOrganizationId;
            else
                row["organization_parent_id"] = DBNull.Value;

            return row;
        }

        public Organization MapOrganization(DataRow row)
        {
            Enumeration orgType = null;
            if(row["enum_organization_id"] != DBNull.Value)
                orgType = Enum_Organization.GetEnumFromId((int)row["enum_organization_id"]);

            string name = null;
            if (row["name"] != DBNull.Value)
                name = (string)row["name"];

            string identifier = null;
            if (row["identifier"] != DBNull.Value)
                identifier = (string)row["identifier"];

            string sisBuildingCode = null;
            if (row["sis_building_code"] != DBNull.Value)
                sisBuildingCode = (string)row["sis_building_code"];

            Organization org = new Organization(name, orgType, identifier, sisBuildingCode);

            if (row["organization_id"] != DBNull.Value)
                org.Id = (int)row["organization_id"];
            else
                org.Id = -1;

            if (row["hr_building_code"] != DBNull.Value)
                org.HrBuildingCode = (string)row["hr_building_code"];
            else
                org.HrBuildingCode = null;

            return org;
        }

        public DataRow MapEmploymentSession_WithPersonId(EmploymentSession employmentSession, int personId)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_EmploymentSession().NewRow();

            if (employmentSession.Id != -1)
                row["employment_session_id"] = employmentSession.Id;
            else
                row["employment_session_id"] = DBNull.Value;

            if (employmentSession.Name != null)
                row["name"] = employmentSession.Name;
            else
                row["name"] = DBNull.Value;
            
            if (personId != -1)
                row["person_id"] = personId;
            else
                row["person_id"] = DBNull.Value;

            if (employmentSession.StartDate != new DateTime())
                row["start_date"] = employmentSession.StartDate;
            else
                row["start_date"] = new DateTime();

            if (employmentSession.EndDate != new DateTime())
                row["end_date"] = employmentSession.EndDate;
            else
                row["end_date"] = new DateTime();

            if (employmentSession.IsPrimary != null)
                row["is_primary_id"] = employmentSession.IsPrimary.Id;
            else
                row["is_primary_id"] = DBNull.Value;

            if (employmentSession.SessionType != null)
                row["enum_session_id"] = employmentSession.SessionType.Id;
            else
                row["enum_session_id"] = DBNull.Value;

            if (employmentSession.Organization != null && employmentSession.Organization.Id != -1)
                row["organization_id"] = employmentSession.Organization.Id;
            else
                row["organization_id"] = DBNull.Value;

            return row;
        }

        public DataRow MapEmploymentSession_WithOrganizationId(EmploymentSession employmentSession, int organizationId)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_EmploymentSession().NewRow();

            if (employmentSession.Id != -1)
                row["employment_session_id"] = employmentSession.Id;
            else
                row["employment_session_id"] = DBNull.Value;

            if (employmentSession.Name != null)

                if (employmentSession.Name != null)
                    row["name"] = employmentSession.Name;
                else
                    row["name"] = DBNull.Value;

            row["person_id"] = DBNull.Value;

            if (employmentSession.StartDate != new DateTime())
                row["start_date"] = employmentSession.StartDate;
            else
                row["start_date"] = new DateTime();

            if (employmentSession.EndDate != new DateTime())
                row["end_date"] = employmentSession.EndDate;
            else
                row["end_date"] = new DateTime();

            if (employmentSession.IsPrimary != null)
                row["is_primary_id"] = employmentSession.IsPrimary.Id;
            else
                row["is_primary_id"] = DBNull.Value;

            if (employmentSession.SessionType != null)
                row["enum_session_id"] = employmentSession.SessionType.Id;
            else
                row["enum_session_id"] = DBNull.Value;

            if (organizationId != -1)
                row["organization_id"] = organizationId;
            else
                row["organization_id"] = DBNull.Value;

            return row;
        }

        public EmploymentSession MapEmploymentSession(DataRow row, Organization org)
        {
            string name = null;
            if(row["name"] != DBNull.Value)
                name = (string)row["name"];

            Enumeration isPrimary = null;
            if (row["is_primary_id"] != DBNull.Value)
                isPrimary = Enum_True_False.GetEnumFromId((int)row["is_primary_id"]);

            EmploymentSession es = new EmploymentSession(name, isPrimary, org);

            if (row["employment_session_id"] != DBNull.Value)
                es.Id = (int)row["employment_session_id"];
            else
                es.Id = -1;

            if (row["start_date"] != DBNull.Value)
                es.StartDate = (DateTime)row["start_date"];
            else
                es.StartDate = new DateTime();

            if (row["end_date"] != DBNull.Value)
                es.EndDate = (DateTime)row["end_date"];
            else
                es.EndDate = new DateTime();

            if (row["is_primary_id"] != DBNull.Value)
                es.IsPrimary = Enum_True_False.GetEnumFromId((int)row["is_primary_id"]);
            else
                es.IsPrimary = null;

            return es;
        }

        public DataRow MapEnrollment(Enrollment enrollment, int personId)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_Enrollment().NewRow();

            if (enrollment.Id != -1)
                row["enrollment_id"] = enrollment.Id;
            else
                row["enrollment_id"] = DBNull.Value;

            if (personId != -1)
                row["person_id"] = personId;
            else
                row["person_id"] = DBNull.Value;

            if (enrollment.ClassEnrolled != null && enrollment.ClassEnrolled.Id != -1)
                row["class_id"] = enrollment.ClassEnrolled.Id;
            else
                row["class_id"] = DBNull.Value;

            if (enrollment.GradeLevel != null)
                row["enum_grade_id"] = enrollment.GradeLevel.Id;
            else
                row["enum_grade_id"] = DBNull.Value;

            if (enrollment.StartDate != new DateTime())
                row["start_date"] = enrollment.StartDate;
            else
                row["start_date"] = new DateTime();

            if (enrollment.EndDate != new DateTime())
                row["end_date"] = enrollment.EndDate;
            else
                row["end_date"] = new DateTime();

            if (enrollment.IsPrimary != null)
                row["is_primary_id"] = enrollment.IsPrimary.Id;
            else
                row["is_primary_id"] = DBNull.Value;

            return row;
        }

        public Enrollment MapEnrollment(DataRow row, ClassEnrolled classEnrolled, List<AcademicSession> academicSessions)
        {
            Enumeration gradeLevel = null;
            if(row["is_primary_id"] != DBNull.Value)
                gradeLevel = Enum_Grade.GetEnumFromId((int)row["enum_grade_id"]);

            DateTime startDate = new DateTime();
            if(row["start_date"] != DBNull.Value)
                startDate = (DateTime)row["start_date"];

            DateTime endDate = new DateTime();
            if(row["end_date"] != DBNull.Value)
                endDate = (DateTime)row["end_date"];

            Enumeration isPrimary = null;
            if(row["is_primary_id"] != DBNull.Value)
                isPrimary = Enum_True_False.GetEnumFromId((int)row["is_primary_id"]);

            Enrollment enrollment = new Enrollment(gradeLevel, startDate, endDate, isPrimary, classEnrolled, academicSessions);

            if (row["enrollment_id"] != DBNull.Value)
                enrollment.Id = (int)row["enrollment_id"];
            else
                enrollment.Id = -1;

            return enrollment;
        }

        public DataRow MapClassEnrolled(ClassEnrolled classEnrolled)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_ClassEnrolled().NewRow();

            if (classEnrolled.Id != -1)
                row["class_id"] = classEnrolled.Id;
            else
                row["class_id"] = DBNull.Value;

            if (classEnrolled.Name != null)
                row["name"] = classEnrolled.Name;
            else
                row["name"] = DBNull.Value;

            if (classEnrolled.ClassCode != null)
                row["class_code"] = classEnrolled.ClassCode;
            else
                row["class_code"] = DBNull.Value;

            if (classEnrolled.ClassType != null)
                row["enum_class_id"] = classEnrolled.ClassType.Id;
            else
                row["enum_class_id"] = DBNull.Value;

            if (classEnrolled.Room != null)
                row["room"] = classEnrolled.Room;
            else
                row["room"] = DBNull.Value;
            
            if (classEnrolled.Course != null && classEnrolled.Course.Id != -1)
                row["course_id"] = classEnrolled.Course.Id;
            else
                row["course_id"] = DBNull.Value;

            if (classEnrolled.AcademicSession != null && classEnrolled.AcademicSession.Id != -1)
                row["academic_session_id"] = classEnrolled.AcademicSession.Id;
            else
                row["academic_session_id"] = DBNull.Value;

            return row;
        }

        public ClassEnrolled MapClassEnrolled(DataRow row, AcademicSession academicSession, Course course)
        {
            string name = null;
            if(row["name"] != DBNull.Value)
                name = (string)row["name"];

            string classCode = null;
            if(row["class_code"] != DBNull.Value)
                classCode = (string)row["class_code"];

            Enumeration classType = null;
            if(row["enum_class_id"] != DBNull.Value)
                classType = Enum_Class.GetEnumFromId((int)row["enum_class_id"]);

            string room = null;
            if (row["room"] != DBNull.Value)
                room = (string)row["room"];

            ClassEnrolled classEnrolled = new ClassEnrolled(name, classCode, classType, room, course, academicSession);

            if (row["class_id"] != DBNull.Value)
                classEnrolled.Id = (int)row["class_id"];
            else
                classEnrolled.Id = -1;

            return classEnrolled;
        }

        public DataRow MapCourse(Course course)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_Course().NewRow();

            if (course.Id != -1)
                row["course_id"] = course.Id;
            else
                row["course_id"] = DBNull.Value;

            if (course.Name != null)
                row["name"] = course.Name;
            else
                row["name"] = DBNull.Value;

            if (course.CourseCode != null)
                row["course_code"] = course.CourseCode;
            else
                row["course_code"] = DBNull.Value;

            return row;
        }

        public Course MapCourse(DataRow row)
        {
            string name = null;
            if(row["name"] != DBNull.Value)
                name = (string)row["name"];

            string courseCode = null;
            if(row["course_code"] != DBNull.Value)
                courseCode = (string)row["course_code"];

            Course course = new Course(name, courseCode);

            if (row["course_id"] != DBNull.Value)
                course.Id = (int)row["course_id"];
            else
                course.Id = -1;

            return course;
        }

        public DataRow MapAcademicSession(AcademicSession academicSession, int parentId)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_AcademicSession().NewRow();

            if (academicSession.Id != -1)
                row["academic_session_id"] = academicSession.Id;
            else
                row["academic_session_id"] = DBNull.Value;

            if (academicSession.TermCode != null)
                row["term_code"] = academicSession.TermCode;
            else
                row["term_code"] = DBNull.Value;

            if (academicSession.SchoolYear != -1)
                row["school_year"] = academicSession.SchoolYear;
            else
                row["school_year"] = DBNull.Value;

            if (academicSession.Name != null)
                row["name"] = academicSession.Name;
            else
                row["name"] = DBNull.Value;

            if (academicSession.StartDate != new DateTime())
                row["start_date"] = academicSession.StartDate;
            else
                row["start_date"] = new DateTime();

            if (academicSession.EndDate != new DateTime())
                row["end_date"] = academicSession.EndDate;
            else
                row["end_date"] = new DateTime();

            if (academicSession.SessionType != null)
                row["enum_session_id"] = academicSession.SessionType.Id;
            else
                row["enum_session_id"] = DBNull.Value;

            if (parentId != -1)
                row["parent_session_id"] = parentId;
            else
                row["parent_session_id"] = DBNull.Value;

            if (academicSession.Organization != null && academicSession.Organization.Id != -1)
                row["organization_id"] = academicSession.Organization.Id;
            else
                row["organization_id"] = DBNull.Value;

            return row;
        }

        public AcademicSession MapAcademicSession(DataRow row, Organization org)
        {
            string name = null;
            if(row["name"] != DBNull.Value)
                name = (string)row["name"];

            Enumeration sessionType = null;
            if (row["enum_session_id"] != DBNull.Value)
                sessionType = Enum_Session.GetEnumFromId((int)row["enum_session_id"]);

            string termCode = null;
            if (row["term_code"] != DBNull.Value)
                termCode = (string)row["term_code"];

            AcademicSession academicSession = new AcademicSession(name, sessionType, org, termCode);

            if (row["academic_session_id"] != DBNull.Value)
                academicSession.Id = (int)row["academic_session_id"];
            else
                academicSession.Id = -1;

            if ((DateTime)row["start_date"] != new DateTime())
                academicSession.StartDate = (DateTime)row["start_date"];
            else
                academicSession.StartDate = new DateTime();

            if ((DateTime)row["end_date"] != new DateTime())
                academicSession.EndDate = (DateTime)row["end_date"];
            else
                academicSession.EndDate = new DateTime();

            if (row["school_year"] != DBNull.Value)
                academicSession.SchoolYear = (int)row["school_year"];
            else
                academicSession.SchoolYear = -1;

            return academicSession;
        }

        public DataRow MapResource(Resource resource)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_Resource().NewRow();

            if (resource.Id != -1)
                row["resource_id"] = resource.Id;
            else
                row["resource_id"] = DBNull.Value;

            if (resource.Name != null)
                row["name"] = resource.Name;
            else
                row["name"] = DBNull.Value;

            if (resource.Importance != null)
                row["enum_importance_id"] = resource.Importance.Id;
            else
                row["enum_importance_id"] = DBNull.Value;

            if (resource.VendorResourceId != null)
                row["vendor_resource_identification"] = resource.VendorResourceId;
            else
                row["vendor_resource_identification"] = DBNull.Value;

            if (resource.VendorId != null)
                row["vendor_identification"] = resource.VendorId;
            else
                row["vendor_identification"] = DBNull.Value;

            if (resource.ApplicationId != null)
                row["application_identification"] = resource.ApplicationId;
            else
                row["application_identification"] = DBNull.Value;

            return row;
        }

        public Resource MapResource(DataRow row)
        {
            string name = null;
            if(row["name"] != DBNull.Value)
                name = (string)row["name"];

            string vendorResourceId = null;
            if (row["vendor_resource_identification"] != DBNull.Value)
                vendorResourceId = (string)row["vendor_resource_identification"];

            Resource resource = new Resource(name, vendorResourceId);

            if (row["resource_id"] != DBNull.Value)
                resource.Id = (int)row["resource_id"];
            else
                resource.Id = -1;

            if (row["enum_importance_id"] != DBNull.Value)
                resource.Importance = Enum_Importance.GetEnumFromId((int)row["enum_importance_id"]);
            else
                resource.Importance = null;

            if (row["vendor_identification"] != DBNull.Value)
                resource.VendorId = (string)row["vendor_identification"];
            else
                resource.VendorId = null;

            if (row["application_identification"] != DBNull.Value)
                resource.ApplicationId = (string)row["application_identification"];
            else
                resource.ApplicationId = null;

            return resource;
        }

        public DataRow MapMark(Mark mark, int personId)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_Mark().NewRow();

            if (mark.Id != -1)
                row["mark_id"] = mark.Id;
            else
                row["mark_id"] = DBNull.Value;

            if (mark.LineItem != null && mark.LineItem.Id != -1)
                row["lineitem_id"] = mark.LineItem.Id;
            else
                row["lineitem_id"] = DBNull.Value;

            if (personId != -1)
                row["person_id"] = personId;
            else
                row["person_id"] = DBNull.Value;

            if (mark.ScoreStatus != null)
                row["enum_score_status_id"] = mark.ScoreStatus.Id;
            else
                row["enum_score_status_id"] = DBNull.Value;

            if (mark.Score != -1)
                row["score"] = mark.Score;
            else
                row["score"] = DBNull.Value;

            if (mark.ScoreDate != new DateTime())
                row["score_date"] = mark.ScoreDate;
            else
                row["score_date"] = new DateTime();

            if (mark.Comment != null)
                row["comment"] = mark.Comment;
            else
                row["comment"] = DBNull.Value;

            return row;
        }

        public Mark MapMark(DataRow row, LineItem lineItem)
        {
            Enumeration scoreStatus = null;
            if(row["enum_score_status_id"] != DBNull.Value)
                scoreStatus = Enum_Score_Status.GetEnumFromId((int)row["enum_score_status_id"]);

            double score = -1;
            if(row["score"] != DBNull.Value)
                score = (double)row["score"];

            DateTime scoreDate = new DateTime();
            if ((DateTime)row["score_date"] != new DateTime())
                scoreDate = (DateTime)row["score_date"];

            Mark mark = new Mark(lineItem, scoreStatus, score, scoreDate);

            if (row["mark_id"] != DBNull.Value)
                mark.Id = (int)row["mark_id"];
            else
                mark.Id = -1;

            return mark;
        }

        public DataRow MapLineItem(LineItem lineItem)
        {
            DataRow row = DataTableFactory.CreateDataTable_Netus2_LineItem().NewRow();

            if (lineItem.Id != -1)
                row["lineitem_id"] = lineItem.Id;
            else
                row["lineitem_id"] = DBNull.Value;

            if (lineItem.Name != null)
                row["name"] = lineItem.Name;
            else
                row["name"] = DBNull.Value;

            if (lineItem.Descript != null)
                row["descript"] = lineItem.Descript;
            else
                row["descript"] = DBNull.Value;

            if (lineItem.AssignDate != new DateTime())
                row["assign_date"] = lineItem.AssignDate;
            else
                row["assign_date"] = new DateTime();

            if (lineItem.DueDate != new DateTime())
                row["due_date"] = lineItem.DueDate;
            else
                row["due_date"] = new DateTime();
            
            if (lineItem.ClassAssigned != null && lineItem.ClassAssigned.Id != -1)
                row["class_id"] = lineItem.ClassAssigned.Id;
            else
                row["class_id"] = DBNull.Value;

            if (lineItem.Category != null)
                row["enum_category_id"] = lineItem.Category.Id;
            else
                row["enum_category_id"] = DBNull.Value;

            if (lineItem.MarkValueMin != -1)
                row["markValueMin"] = lineItem.MarkValueMin;
            else
                row["markValeMin"] = DBNull.Value;

            if (lineItem.MarkValueMax != -1)
                row["markValueMax"] = lineItem.MarkValueMax;
            else
                row["markValueMax"] = DBNull.Value;

            return row;
        }

        public LineItem MapLineItem(DataRow row, ClassEnrolled classEnrolled)
        {
            string name = null;
            if(row["name"] != DBNull.Value)
                name = (string)row["name"];

            DateTime assignDate = new DateTime();
            if((DateTime)row["assign_date"] != new DateTime())
                assignDate = (DateTime)row["assign_date"];

            DateTime dueDate = new DateTime();
            if ((DateTime)row["due_date"] != new DateTime())
                dueDate = (DateTime)row["due_date"];

            Enumeration category = null;
            if(row["enum_category_id"] != DBNull.Value)
                category = Enum_Category.GetEnumFromId((int)row["enum_category_id"]);

            double markMin = -1;
            if(row["markValueMin"] != DBNull.Value)
                markMin = (double)row["markValueMin"];

            double markMax = -1;
            if(row["markValueMax"] != DBNull.Value)
                markMax = (double)row["markValueMax"];

            LineItem lineItem = new LineItem(name, assignDate, dueDate, classEnrolled, category, markMin, markMax);

            if (row["lineitem_id"] != DBNull.Value)
                lineItem.Id = (int)row["lineitem_id"];
            else
                lineItem.Id = -1;

            if (row["descript"] != DBNull.Value)
                lineItem.Descript = (string)row["descript"];
            else
                lineItem.Descript = null;

            return lineItem;
        }
    }
}