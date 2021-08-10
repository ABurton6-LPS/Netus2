namespace Netus2SisSync.UtilityTools
{
    class SyncScripts
    {
        public static string ReadSis_Organization_SQL = BuildScript_Sis_Organization();

        public static string ReadSiS_AcademicSession_SQL = BuildScript_Sis_AcademicSession();

        public static string ReadSis_Person_SQL = BuildScript_Sis_Person();

        private static string BuildScript_Sis_Organization()
        {
            return "SELECT " +
                    "LTRIM(RTRIM(schname)) name, " +
                    "CASE isdo " +
                        "WHEN 1 THEN 'District' " +
                        "ELSE 'School' " +
                    "END enum_organization_id, " +
                    "null identifier, " +
                    "s.schoolc building_code, " +
                    "CASE isdo " +
                        "WHEN 1 THEN NULL " +
                        "ELSE '82095' " +
                    "END organization_parent_id " +
                "FROM school s " +
                "WHERE 1 = 1 " +
                    "AND s.schdistc = '82095' " +
                    "AND s.schyear = (SELECT schyear FROM school WHERE isdo = 1) " +
                "ORDER BY isdo DESC, CONVERT(INT, schtypec), schname";
        }

        private static string BuildScript_Sis_AcademicSession()
        {
            return "SELECT DISTINCT " +
                "t.schoolc school_code, " +
                "tt.termc term_code, " +
                "t.schyear school_year, " +
                "CASE CHARINDEX('Y',tt.termc,1) " +
                "WHEN 0 THEN LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(s.schname,'School',''),'High',''),'Middle',''),'Elementary',''))) + ' ' + CONVERT(VARCHAR(4), t.schyear) + ' ' + z.descript  " +
                "ELSE LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(s.schname,'School',''),'High',''),'Middle',''),'Elementary',''))) + ' ' + CONVERT(VARCHAR(4),t.schyear) + ' School Year'  " +
                "END 'name', " +
                "CASE CHARINDEX('Y',tt.termc,1) " +
                "WHEN 0 THEN " +
                "CASE SUBSTRING(tt.termc, 1, 1) " +
                "WHEN 'T' THEN 'grading period' " +
                "WHEN 'M' THEN 'grading period' " +
                "WHEN 'P' THEN 'grading period' " +
                "WHEN 'Q' THEN 'grading period' " +
                "WHEN 'S' THEN 'semester' " +
                "END " +
                "ELSE 'school year' " +
                "END enum_session_id, " +
                "CONVERT(date, tt.termbegindate) start_date,  " +
                "DATEADD(day, 1, CONVERT(date, tt.termenddate)) end_date, " +
                "CASE  " +
                "WHEN tt.termc LIKE 'T%' AND tt.termc NOT LIKE '%Y%' THEN t.schoolc + '-' + pY.termc + '-' + CONVERT(VARCHAR(4),t.schyear) " +
                "WHEN tt.termc LIKE 'P%' THEN t.schoolc + '-' + pQ.termc + '-' + CONVERT(VARCHAR(4),t.schyear) " +
                "WHEN tt.termc LIKE 'Q%' THEN t.schoolc + '-' + pS.termc + '-' + CONVERT(VARCHAR(4),t.schyear) " +
                "WHEN tt.termc LIKE 'S%' THEN t.schoolc + '-' + pY.termc + '-' + CONVERT(VARCHAR(4),t.schyear) " +
                "ELSE NULL " +
                "END parent_session_code " +
                "FROM TrackTerms tt " +
                "JOIN track t ON tt.trkuniq=t.trkuniq " +
                "JOIN zterm z ON z.termc=tt.termc " +
                "JOIN school s ON s.schoolc=t.schoolc " +
                "LEFT JOIN TrackTerms pQ ON pQ.trkuniq=tt.trkuniq AND pQ.termc LIKE 'Q%' AND tt.termbegindate BETWEEN pQ.termbegindate AND pQ.termenddate AND tt.termenddate BETWEEN pQ.termbegindate AND pQ.termenddate " +
                "LEFT JOIN TrackTerms pS ON pS.trkuniq=tt.trkuniq AND pS.termc LIKE 'S%' AND tt.termbegindate BETWEEN pS.termbegindate AND pS.termenddate AND tt.termenddate BETWEEN pS.termbegindate AND pS.termenddate " +
                "LEFT JOIN TrackTerms pY ON pY.trkuniq=tt.trkuniq AND pY.termc LIKE '%Y%' AND tt.termbegindate BETWEEN pY.termbegindate AND pY.termenddate AND tt.termenddate BETWEEN pY.termbegindate AND pY.termenddate " +
                "WHERE 1=1 " +
                "AND NOT tt.termbegindate IS NULL " +
                "AND NOT tt.termenddate IS NULL " +
                "AND t.schyear >= (SELECT schyear FROM school WHERE schoolc='82095') " +
                "ORDER BY school_code, term_code DESC, school_year";
        }

        private static string BuildScript_Sis_Person()
        {
            return "SELECT DISTINCT " +
                "'student' person_type, " +
                "sd.suniq sis_id, " +
                "REPLACE(sd.firstname,'''','''''') first_name, " +
                "CASE " +
                "WHEN sd.middlename = '' THEN NULL " +
                "ELSE REPLACE(sd.middlename,'''','''''') END middle_name, " +
                "REPLACE(sd.lastname,'''','''''') last_name, " +
                "sd.birthdate birth_date, " +
                "CASE " +
                "WHEN sd.genderc = 'M' THEN 'male' " +
                "WHEN sd.genderc = 'F' THEN 'female' " +
                "ELSE 'unset' " +
                "END enum_gender_id, " +
                "sd.ethnicc enum_ethnic_id, " +
                "CASE " +
                "WHEN sd.resdistc='82095' AND sd.resschoolc=t.schoolc THEN '01652' " +
                "WHEN sd.resdistc='82095' AND sd.resschoolc != t.schoolc THEN '01653' " +
                "WHEN sd.homestate = 'MI' AND sd.resdistc != '82095' THEN '01654' " +
                "WHEN sd.homestate != 'MI' THEN '01656' " +
                "ELSE 'unset' " +
                "END enum_residence_status_id, " +
                "uds.net_username login_name, " +
                "CASE " +
                "WHEN uds.net_password = '' THEN NULL " +
                "ELSE uds.net_password " +
                "END login_pw " +
                "FROM studemo sd " +
                "JOIN stustat ss ON ss.suniq=sd.suniq AND ss.xdate IS NULL AND ss.stustatc IN ('A','M') " +
                "JOIN track t ON t.trkuniq=ss.trkuniq AND t.schyear = (SELECT schyear FROM school WHERE isdo=1) " +
                "LEFT JOIN uDefStu uds ON uds.sUniq=sd.suniq " +
                "UNION " +
                "SELECT DISTINCT " +
                "'staff' person_type, " +
                "fd.funiq sis_id, " +
                "REPLACE(fd.firstname,'''','''''') first_name, " +
                "CASE " +
                "WHEN fd.middlename = '' THEN NULL " +
                "ELSE REPLACE(fd.middlename,'''','''''') END middle_name, " +
                "REPLACE(fd.lastname,'''','''''') last_name, " +
                "fd.birthdate birth_date, " +
                "'unset' enum_gender_id, " +
                "'unset' enum_ethnic_id, " +
                "'unset' enum_residence_status_id, " +
                "REPLACE(fd.emailaddr,'@livoniapublicschools.org','') login_name, " +
                "NULL login_pw " +
                "FROM facdemo fd " +
                "JOIN facstat fs ON fs.funiq=fd.funiq AND fs.xdate IS NULL " +
                "JOIN track t ON t.trkuniq=fs.trkuniq AND t.schyear = (SELECT schyear FROM school WHERE isdo=1) " +
                "WHERE 1=1 " +
                "AND fd.funiq != 0 " +
                "AND fd.firstname NOT IN ('', ' ,', ',', '1', '2', '3', '4') " +
                "AND NOT (fd.emailaddr = '' OR fd.emailaddr IS NULL OR REPLACE(fd.emailaddr,'@livoniapublicschools.org','') LIKE '%@%') " +
                "ORDER BY last_name, first_name";
        }
    }
}