using Moq;
using Netus2_DatabaseConnection.daoInterfaces;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public static class DaoImplFactory
    {
        public static bool MockAll = false;
        public static IAcademicSessionDao MockAcademicSessionDaoImpl = null;
        public static IAddressDao MockAddressDaoImpl = null;
        public static IEmailDao MockEmailDaoImpl = null;
        public static IApplicationDao MockApplicationDaoImpl = null;
        public static IClassEnrolledDao MockClassEnrolledDaoImpl = null;
        public static ICourseDao MockCourseDaoImpl = null;
        public static IEmploymentSessionDao MockEmploymentSessionDaoImpl = null;
        public static IEnrollmentDao MockEnrollmentDaoImpl = null;
        public static IJctClassPeriodDao MockJctClassPeriodDaoImpl = null;
        public static IJctClassPersonDao MockJctClassPersonDaoImpl = null;
        public static IJctClassResourceDao MockJctClassResourceDaoImpl = null;
        public static IJctCourseGradeDao MockJctCourseGradeDaoImpl = null;
        public static IJctCourseSubjectDao MockJctCourseSubjectDaoImpl = null;
        public static IJctEnrollmentAcademicSessionDao MockJctEnrollmentAcademicSessionDaoImpl = null;
        public static IJctPersonAddressDao MockJctPersonAddressDaoImpl = null;
        public static IJctPersonEmailDao MockJctPersonEmailDaoImpl = null;
        public static IJctPersonAppDao MockJctPersonAppDaoImpl = null;
        public static IJctPersonPhoneNumberDao MockJctPersonPhoneNumberDaoImpl = null;
        public static IJctPersonPersonDao MockJctPersonPersonDaoImpl = null;
        public static IJctPersonRoleDao MockJctPersonRoleDaoImpl = null;
        public static ILineItemDao MockLineItemDaoImpl = null;
        public static IMarkDao MockMarkDaoImpl = null;
        public static IOrganizationDao MockOrganizationDaoImpl = null;
        public static IPersonDao MockPersonDaoImpl = null;
        public static IPhoneNumberDao MockPhoneNumberDaoImpl = null;
        public static IProviderDao MockProviderDaoImpl = null;
        public static IResourceDao MockResourceDaoImpl = null;
        public static IUniqueIdentifierDao MockUniqueIdentifierDaoImpl = null;

        private static IAcademicSessionDao AcademicSessionDaoImpl = null;
        private static IAddressDao AddressDaoImpl = null;
        private static IEmailDao EmailDaoImpl = null;
        private static IApplicationDao ApplicationDaoImpl = null;
        private static IClassEnrolledDao ClassEnrolledDaoImpl = null;
        private static ICourseDao CourseDaoImpl = null;
        private static IEmploymentSessionDao EmploymentSessionDaoImpl = null;
        private static IEnrollmentDao EnrollmentDaoImpl = null;
        private static IJctClassPeriodDao JctClassPeriodDaoImpl = null;
        private static IJctClassPersonDao JctClassPersonDaoImpl = null;
        private static IJctClassResourceDao JctClassResourceDaoImpl = null;
        private static IJctCourseGradeDao JctCourseGradeDaoImpl = null;
        private static IJctCourseSubjectDao JctCourseSubjectDaoImpl = null;
        private static IJctEnrollmentAcademicSessionDao JctEnrollmentAcademicSessionDaoImpl = null;
        private static IJctPersonAddressDao JctPersonAddressDaoImpl = null;
        private static IJctPersonEmailDao JctPersonEmailDaoImpl = null;
        private static IJctPersonAppDao JctPersonAppDaoImpl = null;
        private static IJctPersonPhoneNumberDao JctPersonPhoneNumberDaoImpl = null;
        private static IJctPersonPersonDao JctPersonPersonDaoImpl = null;
        private static IJctPersonRoleDao JctPersonRoleDaoImpl = null;
        private static ILineItemDao LineItemDaoImpl = null;
        private static IMarkDao MarkDaoImpl = null;
        private static IOrganizationDao OrganizationDaoImpl = null;
        private static IPersonDao PersonDaoImpl = null;
        private static IPhoneNumberDao PhoneNumberDaoImpl = null;
        private static IProviderDao ProviderDaoImpl = null;
        private static IResourceDao ResourceDaoImpl = null;
        private static IUniqueIdentifierDao UniqueIdentifierDaoImpl = null;

        public static IAcademicSessionDao GetAcademicSessionDaoImpl()
        {
            if (MockAcademicSessionDaoImpl == null && MockAll == false)
                return new AcademicSessionDaoImpl();
            else
                return MockAcademicSessionDaoImpl;
        }

        public static IAddressDao GetAddressDaoImpl()
        {
            if (MockAddressDaoImpl == null && MockAll == false)
                return new AddressDaoImpl();
            else
                return MockAddressDaoImpl;
        }

        public static IEmailDao GetEmailDaoImpl()
        {
            if (MockEmailDaoImpl == null && MockAll == false)
                return new EmailDaoImpl();
            else
                return MockEmailDaoImpl;
        }

        public static IApplicationDao GetApplicationDaoImpl()
        {
            if (MockApplicationDaoImpl == null && MockAll == false)
                return new ApplicationDaoImpl();
            else
                return MockApplicationDaoImpl;
        }

        public static IClassEnrolledDao GetClassEnrolledDaoImpl()
        {
            if (MockClassEnrolledDaoImpl == null && MockAll == false)
                return new ClassEnrolledDaoImpl();
            else
                return MockClassEnrolledDaoImpl;
        }

        public static ICourseDao GetCourseDaoImpl()
        {
            if (MockCourseDaoImpl == null && MockAll == false)
                return new CourseDaoImpl();
            else
                return MockCourseDaoImpl;
        }

        public static IEmploymentSessionDao GetEmploymentSessionDaoImpl()
        {
            if (MockEmploymentSessionDaoImpl == null && MockAll == false)
                return new EmploymentSessionDaoImpl();
            else
                return MockEmploymentSessionDaoImpl;
        }

        public static IEnrollmentDao GetEnrollmentDaoImpl()
        {
            if (MockEnrollmentDaoImpl == null && MockAll == false)
                return new EnrollmentDaoImpl();
            else
                return MockEnrollmentDaoImpl;
        }

        public static IJctClassPeriodDao GetJctClassPeriodDaoImpl()
        {
            if (MockJctClassPeriodDaoImpl == null && MockAll == false)
                return new JctClassPeriodDaoImpl();
            else
                return MockJctClassPeriodDaoImpl;
        }

        public static IJctClassPersonDao GetJctClassPersonDaoImpl()
        {
            if (MockJctClassPersonDaoImpl == null && MockAll == false)
                return new JctClassPersonDaoImpl();
            else
                return MockJctClassPersonDaoImpl;
        }

        public static IJctClassResourceDao GetJctClassResourceDaoImpl()
        {
            if (MockJctClassResourceDaoImpl == null && MockAll == false)
                return new JctClassResourceDaoImpl();
            else
                return MockJctClassResourceDaoImpl;
        }

        public static IJctCourseGradeDao GetJctCourseGradeDaoImpl()
        {
            if (MockJctCourseGradeDaoImpl == null && MockAll == false)
                return new JctCourseGradeDaoImpl();
            else
                return MockJctCourseGradeDaoImpl;
        }

        public static IJctCourseSubjectDao GetJctCourseSubjectDaoImpl()
        {
            if (MockJctCourseSubjectDaoImpl == null && MockAll == false)
                return new JctCourseSubjectDaoImpl();
            else
                return MockJctCourseSubjectDaoImpl;
        }

        public static IJctEnrollmentAcademicSessionDao GetJctEnrollmentAcademicSessionDaoImpl()
        {
            if (MockJctEnrollmentAcademicSessionDaoImpl == null && MockAll == false)
                return new JctEnrollmentAcademicSessionDaoImpl();
            else
                return MockJctEnrollmentAcademicSessionDaoImpl;
        }

        public static IJctPersonAddressDao GetJctPersonAddressDaoImpl()
        {
            if (MockJctPersonAddressDaoImpl == null && MockAll == false)
                return new JctPersonAddressDaoImpl();
            else
                return MockJctPersonAddressDaoImpl;
        }

        public static IJctPersonEmailDao GetJctPersonEmailDaoImpl()
        {
            if (MockJctPersonEmailDaoImpl == null && MockAll == false)
                return new JctPersonEmailDaoImpl();
            else
                return MockJctPersonEmailDaoImpl;
        }

        public static IJctPersonAppDao GetJctPersonAppDaoImpl()
        {
            if (MockJctPersonAppDaoImpl == null && MockAll == false)
                return new JctPersonAppDaoImpl();
            else
                return MockJctPersonAppDaoImpl;
        }

        public static IJctPersonPhoneNumberDao GetJctPersonPhoneNumberDaoImpl()
        {
            if (MockJctPersonPhoneNumberDaoImpl == null && MockAll == false)
                return new JctPersonPhoneNumberDaoImpl();
            else
                return MockJctPersonPhoneNumberDaoImpl;
        }

        public static IJctPersonPersonDao GetJctPersonPersonDaoImpl()
        {
            if (MockJctPersonPersonDaoImpl == null && MockAll == false)
                return new JctPersonPersonDaoImpl();
            else
                return MockJctPersonPersonDaoImpl;
        }

        public static IJctPersonRoleDao GetJctPersonRoleDaoImpl()
        {
            if (MockJctPersonRoleDaoImpl == null && MockAll == false)
                return new JctPersonRoleDaoImpl();
            else
                return MockJctPersonRoleDaoImpl;
        }

        public static ILineItemDao GetLineItemDaoImpl()
        {
            if (MockLineItemDaoImpl == null && MockAll == false)
                return new LineItemDaoImpl();
            else
                return MockLineItemDaoImpl;
        }

        public static IMarkDao GetMarkDaoImpl()
        {
            if (MockMarkDaoImpl == null && MockAll == false)
                return new MarkDaoImpl();
            else
                return MockMarkDaoImpl;
        }

        public static IOrganizationDao GetOrganizationDaoImpl()
        {
            if (MockOrganizationDaoImpl == null && MockAll == false)
                return new OrganizationDaoImpl();
            else
            {
                return MockOrganizationDaoImpl;
            }
        }

        public static IPersonDao GetPersonDaoImpl()
        {
            if (MockPersonDaoImpl == null && MockAll == false)
                return new PersonDaoImpl();
            else
                return MockPersonDaoImpl;
        }

        public static IPhoneNumberDao GetPhoneNumberDaoImpl()
        {
            if (MockPhoneNumberDaoImpl == null && MockAll == false)
                return new PhoneNumberDaoImpl();
            else
                return MockPhoneNumberDaoImpl;
        }

        public static IProviderDao GetProviderDaoImpl()
        {
            if (MockProviderDaoImpl == null && MockAll == false)
                return new ProviderDaoImpl();
            else
                return MockProviderDaoImpl;
        }

        public static IResourceDao GetResourceDaoImpl()
        {
            if (MockResourceDaoImpl == null && MockAll == false)
                return new ResourceDaoImpl();
            else
                return MockResourceDaoImpl;
        }

        public static IUniqueIdentifierDao GetUniqueIdentifierDaoImpl()
        {
            if (MockUniqueIdentifierDaoImpl == null && MockAll == false)
                return new UniqueIdentifierDaoImpl();
            else
                return MockUniqueIdentifierDaoImpl;
        }
    }
}
