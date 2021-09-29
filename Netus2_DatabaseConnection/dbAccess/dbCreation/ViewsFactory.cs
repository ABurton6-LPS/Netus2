namespace Netus2_DatabaseConnection.dbAccess.dbCreation
{
    public class ViewsFactory
    {
        public static void BuildOneRosterViews(IConnectable connection)
        {
            Build_AcademicSession_OneRosterView(connection);
            Build_Class_OneRosterView(connection);
            Build_Course_OneRosterView(connection);
            Build_Demographic_OneRosterView(connection);
            Build_Enrollment_OneRosterView(connection);
            Build_LineItem_OneRosterView(connection);
            Build_Category_OneRosterView(connection);
            Build_Org_OneRosterView(connection);
            Build_Resource_OneRosterView(connection);
            Build_Result_OneRosterView(connection);
            Build_User_OneRosterView(connection);
        }

        private static void Build_AcademicSession_OneRosterView(IConnectable connection)
        {
            string sql =
                "CREATE VIEW AcademicSession_OneRoster AS " +
                "SELECT DISTINCT " +
                "ass.academic_session_id sourcedId " +
                ",'active' [status] " +
                ",ass.changed dateLastModified " +
                ",NULL metadata " +
                ",ass.[name] title " +
                ",ass.start_date startDate " +
                ",ass.end_date endDate " +
                ",eses.netus2_code [type] " +
                ",ass.parent_session_id parent " +
                ",STUFF((SELECT CONCAT(', ', academic_session_id) " +
                "FROM academic_session aca " +
                "WHERE parent_session_id = ass.academic_session_id " +
                "FOR XML PATH('')), 1, 2, '') children " +
                ",YEAR(end_date) schoolYear " +
                "FROM academic_session ass " +
                "JOIN enum_session eses ON eses.enum_session_id = ass.enum_session_id " +
                "WHERE 1=1";

            connection.ExecuteNonQuery(sql);
        }

        private static void Build_Class_OneRosterView(IConnectable connection)
        {
            string sql =
                "CREATE VIEW Class_OneRoster AS " +
                "SELECT DISTINCT " +
                "c.class_id sourcedId, " +
                "'active' [status], " +
                "c.changed dateLastModified, " +
                "NULL metadata, " +
                "c.[name] title, " +
                "c.class_code classCode, " +
                "ec.netus2_code classType, " +
                "c.room [location], " +
                "STUFF((SELECT CONCAT(', ', netus2_code) " +
                "FROM enum_grade eg " +
                "JOIN jct_course_grade jcg ON jcg.enum_grade_id = eg.enum_grade_id " +
                "WHERE jcg.course_id = c.course_id " +
                "FOR XML PATH('')), 1, 2,'') grades, " +
                "STUFF((SELECT CONCAT(', ', descript) " +
                "FROM enum_subject es " +
                "JOIN jct_course_subject jcs ON jcs.enum_subject_id = es.enum_subject_id " +
                "WHERE jcs.course_id = c.course_id " +
                "FOR XML PATH('')), 1, 2, '') subjects, " +
                "c.course_id course, " +
                "ass.organization_id school, " +
                "c.academic_session_id terms, " +
                "STUFF((SELECT CONCAT(', ', netus2_code) " +
                "FROM enum_subject es " +
                "JOIN jct_course_subject jcs ON jcs.enum_subject_id = es.enum_subject_id " +
                "WHERE jcs.course_id = c.course_id " +
                "FOR XML PATH('')), 1, 2, '') subjectCodes, " +
                "STUFF((SELECT CONCAT(', ', netus2_code) " +
                "FROM enum_period ep " +
                "JOIN jct_class_period jcp ON jcp.enum_period_id = ep.enum_period_id " +
                "WHERE jcp.class_id = c.class_id " +
                "FOR XML PATH('')), 1, 2, '') [periods], " +
                "STUFF ((SELECT CONCAT(', ', r.resource_id) " +
                "FROM [resource] r " +
                "JOIN jct_class_resource jcr ON jcr.resource_id = r.resource_id " +
                "WHERE jcr.class_id = c.class_id " +
                "FOR XML PATH('')), 1, 2, '') resources " +
                "FROM class c " +
                "JOIN enum_class ec ON ec.enum_class_id = c.enum_class_id " +
                "JOIN academic_session ass ON ass.academic_session_id = ass.academic_session_id " +
                "WHERE 1=1";

            connection.ExecuteNonQuery(sql);
        }

        private static void Build_Course_OneRosterView(IConnectable connection)
        {
            string sql =
                "CREATE VIEW Course_OneRoster AS " +
                "SELECT DISTINCT " +
                "c.course_id sourcedId, " +
                "'active' status, " +
                "c.changed dateLastModified, " +
                "NULL metadata, " +
                "c.[name] title, " +
                "ass.academic_session_id schoolYear, " +
                "c.course_code courseCode, " +
                "STUFF((SELECT CONCAT(', ', netus2_code) " +
                "FROM enum_grade eg " +
                "JOIN jct_course_grade jcg ON jcg.enum_grade_id = eg.enum_grade_id " +
                "WHERE jcg.course_id = c.course_id " +
                "FOR XML PATH('')), 1, 2, '') grades, " +
                "STUFF((SELECT CONCAT(', ', descript) " +
                "FROM enum_subject es " +
                "JOIN jct_course_subject jcs ON jcs.enum_subject_id = es.enum_subject_id " +
                "WHERE jcs.course_id = c.course_id " +
                "FOR XML PATH('')), 1, 2, '') subjects, " +
                "ass.organization_id org, " +
                "STUFF((SELECT CONCAT(', ', netus2_code) " +
                "FROM enum_subject es " +
                "JOIN jct_course_subject jcs ON jcs.enum_subject_id = es.enum_subject_id " +
                "WHERE jcs.course_id = c.course_id " +
                "FOR XML PATH('')), 1, 2, '') subjectCodes, " +
                "STUFF ((SELECT CONCAT(', ', r.resource_id) " +
                "FROM [resource] r " +
                "JOIN jct_class_resource jcr ON jcr.resource_id = r.resource_id " +
                "JOIN class cl ON cl.class_id = jcr.class_id " +
                "WHERE cl.course_id = c.course_id " +
                "FOR XML PATH('')), 1, 2, '') resources " +
                "FROM course c " +
                "JOIN class cl ON cl.course_id = c.course_id " +
                "Join academic_session ass ON ass.academic_session_id = cl.academic_session_id " +
                "WHERE 1=1";

            connection.ExecuteNonQuery(sql);
        }

        private static void Build_Demographic_OneRosterView(IConnectable conneciton)
        {
            string sql =
                "CREATE VIEW Demographics_OneRoster as " +
                "SELECT DISTINCT " +
                "p.person_id sourcedId " +
                ",'active' status " +
                ",p.changed dateLastModified " +
                ",NULL metadata " +
                ",p.birth_date birthDate " +
                ",eg.netus2_code sex " +
                ",(CASE  " +
                "WHEN p.enum_ethnic_id =  " +
                "(SELECT enum_ethnic_id  " +
                "FROM enum_ethnic  " +
                "WHERE netus2_code = 'NAT')  " +
                "THEN 'true'  " +
                "ELSE 'false'  " +
                "END) americanIndianOrAlaskaNative " +
                ",(CASE  " +
                "WHEN p.enum_ethnic_id =  " +
                "(SELECT enum_ethnic_id  " +
                "FROM enum_ethnic  " +
                "WHERE netus2_code = 'ASI')  " +
                "THEN 'true'  " +
                "ELSE 'false'  " +
                "END) asian " +
                ",(CASE  " +
                "WHEN p.enum_ethnic_id =  " +
                "(SELECT enum_ethnic_id  " +
                "FROM enum_ethnic  " +
                "WHERE netus2_code = 'BAA')  " +
                "THEN 'true'  " +
                "ELSE 'false'  " +
                "END) blackOrAfricanAmerican " +
                ",(CASE  " +
                "WHEN p.enum_ethnic_id =  " +
                "(SELECT enum_ethnic_id  " +
                "FROM enum_ethnic  " +
                "WHERE netus2_code = 'NHI')  " +
                "THEN 'true'  " +
                "ELSE 'false'  " +
                "END) nativeHawaiianOrOtherPacificIslander " +
                ",(CASE  " +
                "WHEN p.enum_ethnic_id =  " +
                "(SELECT enum_ethnic_id  " +
                "FROM enum_ethnic  " +
                "WHERE netus2_code = 'CAU')  " +
                "THEN 'true'  " +
                "ELSE 'false'  " +
                "END) white " +
                ",(CASE  " +
                "WHEN p.enum_ethnic_id =  " +
                "(SELECT enum_ethnic_id  " +
                "FROM enum_ethnic  " +
                "WHERE netus2_code = 'MUL')  " +
                "THEN 'true'  " +
                "ELSE 'false'  " +
                "END) demographicRaceTwoOrMoreRaces " +
                ",(CASE  " +
                "WHEN p.enum_ethnic_id =  " +
                "(SELECT enum_ethnic_id  " +
                "FROM enum_ethnic  " +
                "WHERE netus2_code = 'HIS')  " +
                "THEN 'true'  " +
                "ELSE 'false'  " +
                "END) hispanicOrLatinoEthnicity " +
                ",(CASE WHEN ea.netus2_code = 'birth'	THEN ec.netus2_code ELSE NULL END) countryOfBirthCode " +
                ",(CASE WHEN ea.netus2_code = 'birth'	THEN esp.netus2_code ELSE NULL END) stateOfBirthAbbreviation " +
                ",(CASE WHEN ea.netus2_code = 'birth' 	THEN a.city ELSE NULL END) cityOfBirth " +
                ",ers.netus2_code publicSchoolResidenceStatus " +
                "FROM person p " +
                "JOIN enum_gender eg ON eg.enum_gender_id = p.enum_gender_id " +
                "JOIN enum_ethnic ee ON ee.enum_ethnic_id = p.enum_ethnic_id " +
                "LEFT OUTER JOIN jct_person_address jpa ON jpa.person_id = p.person_id " +
                "LEFT OUTER JOIN [address] a ON a.address_id = jpa.address_id	AND (a.enum_address_id = (SELECT enum_address_id FROM enum_address WHERE netus2_code = 'birth')) " +
                "LEFT OUTER JOIN enum_address ea ON ea.enum_address_id = a.enum_address_id " +
                "LEFT OUTER JOIN enum_country ec ON ec.enum_country_id = a.enum_country_id " +
                "LEFT OUTER JOIN enum_state_province esp ON esp.enum_state_province_id = a.enum_state_province_id " +
                "LEFT OUTER JOIN enum_residence_status ers ON ers.enum_residence_status_id = p.enum_residence_status_id " +
                "WHERE 1=1";

            conneciton.ExecuteNonQuery(sql);
        }

        private static void Build_Enrollment_OneRosterView(IConnectable connection)
        {
            string sql =
                "CREATE VIEW Enrollment_OneRoster AS " +
                "SELECT DISTINCT " +
                "e.enrollment_id sourcedId, " +
                "'active' status, " +
                "e.changed dateLastModified, " +
                "NULL metadata, " +
                "e.person_id [user], " +
                "e.class_id class, " +
                "ass.organization_id school, " +
                "er.netus2_code [role], " +
                "etf.netus2_code [primary], " +
                "e.[start_date] beginDate, " +
                "e.end_date endDate " +
                "FROM enrollment e " +
                "JOIN jct_enrollment_academic_session jeass ON jeass.enrollment_id = e.enrollment_id " +
                "JOIN academic_session ass ON ass.academic_session_id = jeass.academic_session_id " +
                "JOIN enum_true_false etf ON etf.enum_true_false_id = e.is_primary_id " +
                "JOIN jct_person_role jpr ON jpr.person_id = e.person_id " +
                "JOIN enum_role er ON er.enum_role_id = jpr.enum_role_id " +
                "WHERE 1=1";

            connection.ExecuteNonQuery(sql);
        }

        private static void Build_LineItem_OneRosterView(IConnectable connection)
        {
            string sql =
                "CREATE VIEW LineItem_OneRoster AS " +
                "SELECT DISTINCT " +
                "li.lineitem_id sourcedId, " +
                "'active' status, " +
                "li.changed dateLastModified, " +
                "NULL metadata, " +
                "li.[name] title, " +
                "li.descript [description], " +
                "(left(convert(varchar(30),li.assign_date,126)+ 'T00:00:00.000',23)+'Z') assignDate, " +
                "(left(convert(varchar(30),li.due_date,126)+ 'T00:00:00.000',23)+'Z') dueDate, " +
                "li.class_id class, " +
                "li.enum_category_id category, " +
                "c.academic_session_id gradingPeriod, " +
                "li.markValueMin resultValueMin, " +
                "li.markValueMax resultValueMax " +
                "FROM lineitem li " +
                "JOIN class c on c.class_id = li.class_id " +
                "WHERE 1=1";

            connection.ExecuteNonQuery(sql);
        }

        private static void Build_Category_OneRosterView(IConnectable connection)
        {
            string sql =
                "CREATE VIEW Category_OneRoster AS " +
                "SELECT DISTINCT " +
                "ec.enum_category_id sourcedId, " +
                "'active' status, " +
                "(SELECT left(convert(varchar(30),MAX(log_date),126)+ '.000',23)+'Z' " +
                "FROM log_enum_category WHERE enum_category_id = ec.enum_category_id) dateLastModified, " +
                "NULL metadata, " +
                "ec.netus2_code title " +
                "FROM enum_category ec " +
                "WHERE 1=1";

            connection.ExecuteNonQuery(sql);
        }

        private static void Build_Org_OneRosterView(IConnectable connection)
        {
            string sql =
                "CREATE VIEW Org_OneRoster AS " +
                "SELECT DISTINCT " +
                "o.organization_id sourcedId, " +
                "'active' status, " +
                "o.changed dateLastModified, " +
                "NULL metadata, " +
                "o.[name] [name], " +
                "eo.netus2_code [type], " +
                "o.identifier identifier, " +
                "o.organization_parent_id parent, " +
                "(SELECT organization_id " +
                "FROM organization " +
                "WHERE organization_parent_id = o.organization_id) children " +
                "FROM organization o " +
                "JOIN enum_organization eo ON eo.enum_organization_id = o.enum_organization_id " +
                "WHERE 1=1";

            connection.ExecuteNonQuery(sql);
        }

        private static void Build_Resource_OneRosterView(IConnectable connection)
        {
            string sql =
                "CREATE VIEW Resource_OneRoster AS " +
                "SELECT DISTINCT " +
                "r.resource_id sourcedId, " +
                "'active' [status], " +
                "r.changed dateLastModified, " +
                "NULL metadata, " +
                "r.[name] title, " +
                "'teacher' roles, " +
                "ei.netus2_code importance, " +
                "r.vendor_resource_identification vendorResourceId, " +
                "r.vendor_identification vendorId, " +
                "r.application_identification applicationId " +
                "FROM [resource] r " +
                "LEFT JOIN enum_importance ei ON ei.enum_importance_id = r.enum_importance_id " +
                "WHERE 1=1";

            connection.ExecuteNonQuery(sql);
        }

        private static void Build_Result_OneRosterView(IConnectable connection)
        {
            string sql =
                "CREATE VIEW Result_OneRoster AS " +
                "SELECT DISTINCT " +
                "m.mark_id sourcedId, " +
                "'active' [status], " +
                "m.changed dateLastModified, " +
                "NULL metadata, " +
                "m.lineitem_id lineitem, " +
                "m.person_id student, " +
                "ess.netus2_code scoreStatus, " +
                "m.score score, " +
                "m.score_date scoreDate, " +
                "m.comment comment " +
                "FROM mark m " +
                "JOIN enum_score_status ess ON ess.enum_score_status_id = m.enum_score_status_id " +
                "WHERE 1=1";

            connection.ExecuteNonQuery(sql);
        }

        private static void Build_User_OneRosterView(IConnectable connection)
        {
            string sql =
                "CREATE VIEW User_OneRoster AS " +
                "SELECT DISTINCT " +
                "NULL sourcedId, " +
                "'active' [status], " +
                "(SELECT left(convert(varchar(30),MAX(log_date),126)+ '.000',23)+'Z' " +
                "FROM log_person WHERE person_id = p.person_id) dateLastModified, " +
                "NULL metadata, " +
                "p.login_name username, " +
                "NULL userIds, " +
                "'true' enabledUser, " +
                "p.first_name givenName, " +
                "p.last_name familyName, " +
                "p.middle_name middleName, " +
                "er.netus2_code [role], " +
                "NULL identifier, " +
                "CASE WHEN p.login_name IS NOT NULL " +
                "THEN CONCAT(p.login_name, (CASE  " +
                "WHEN jpr.enum_role_id = (SELECT enum_role_id  " +
                "FROM enum_role  " +
                "WHERE netus2_code = 'student')  " +
                "THEN '@student.livoniapublicschools.org'  " +
                "ELSE '@livoniapublicschools.org' " +
                "END)) " +
                "ELSE NULL " +
                "END email, " +
                "STUFF((SELECT CONCAT(', ', pn.phone_number) " +
                "FROM phone_number pn " +
                "JOIN enum_phone ep ON ep.enum_phone_id = pn.enum_phone_id " +
                "WHERE pn.person_id = p.person_id " +
                "AND ep.netus2_code = 'cell' " +
                "FOR XML PATH('')), 1, 2, '') sms, " +
                "STUFF((SELECT CONCAT(', ', pn.phone_number) " +
                "FROM phone_number pn " +
                "JOIN enum_phone ep ON ep.enum_phone_id = pn.enum_phone_id " +
                "WHERE pn.person_id = p.person_id " +
                "AND pn.is_primary_id = (SELECT enum_true_false_id FROM enum_true_false WHERE netus2_code = 'true') " +
                "FOR XML PATH('')), 1, 2, '') phone, " +
                "STUFF((SELECT CONCAT(', ', jpp.person_two_id) " +
                "FROM jct_person_person jpp " +
                "WHERE jpp.person_one_id = p.person_id " +
                "FOR XML PATH ('')), 1, 2, '') agents, " +
                "STUFF((SELECT CONCAT(', ', ass.organization_id) " +
                "FROM academic_session ass " +
                "JOIN class c ON c.academic_session_id = ass.academic_session_id " +
                "JOIN enrollment e ON e.class_id = c.class_id " +
                "WHERE e.person_id = p.person_id " +
                "FOR XML PATH ('')), 1, 2, '') orgs, " +
                "STUFF((SELECT CONCAT(', ', eg.netus2_code) " +
                "FROM enum_grade eg " +
                "JOIN enrollment e ON e.enum_grade_id = eg.enum_grade_id " +
                "WHERE e.person_id = p.person_id " +
                "FOR XML PATH ('')), 1, 2, '') grades, " +
                "p.login_pw [password] " +
                "FROM person p " +
                "JOIN jct_person_role jpr ON jpr.person_id = p.person_id " +
                "JOIN enum_role er ON er.enum_role_id = jpr.enum_role_id " +
                "WHERE 1=1";

            connection.ExecuteNonQuery(sql);
        }
    }
}