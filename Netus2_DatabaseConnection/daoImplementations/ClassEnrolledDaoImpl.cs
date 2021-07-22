using Netus2.daoInterfaces;
using Netus2.daoObjects;
using Netus2.dbAccess;
using Netus2.enumerations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Netus2.daoImplementations
{
    public class ClassEnrolledDaoImpl : IClassEnrolledDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(ClassEnrolled classEnrolled, IConnectable connection)
        {
            Delete_LineItem(classEnrolled, connection);
            Delete_JctClassPeriod(classEnrolled, connection);
            Delete_JctClassResource(classEnrolled, connection);
            Delete_JctClassPerson(classEnrolled, connection);
            Delete_Enrollment(classEnrolled, connection);

            ClassEnrolledDao classEnrolledDao = daoObjectMapper.MapClassEnrolled(classEnrolled);

            StringBuilder sql = new StringBuilder("DELETE FROM class WHERE 1=1 ");
            sql.Append("AND class_id = " + classEnrolledDao.class_id + " ");
            sql.Append("AND name " + (classEnrolledDao.name != null ? "= '" + classEnrolledDao.name + "' " : "IS NULL "));
            sql.Append("AND class_code " + (classEnrolledDao.class_code != null ? "= '" + classEnrolledDao.class_code + "' " : "IS NULL "));
            sql.Append("AND enum_class_id " + (classEnrolledDao.enum_class_id != null ? "= " + classEnrolledDao.enum_class_id + " " : "IS NULL "));
            sql.Append("AND room " + (classEnrolledDao.room != null ? "= '" + classEnrolledDao.room + "' " : "IS NULL "));
            sql.Append("AND course_id " + (classEnrolledDao.course_id != null ? "= " + classEnrolledDao.course_id + " " : "IS NULL "));
            sql.Append("AND academic_session_id " + (classEnrolledDao.academic_session_id != null ? " = " + classEnrolledDao.academic_session_id + " " : "IS NULL "));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_Enrollment(ClassEnrolled classEnrolled, IConnectable connection)
        {
            IEnrollmentDao enrollmentDaoImpl = new EnrollmentDaoImpl();
            List<Enrollment> enrollmentsFound = enrollmentDaoImpl.Read_WithClassId(classEnrolled.Id, connection);
            foreach (Enrollment foundEnrollment in enrollmentsFound)
            {
                enrollmentDaoImpl.Delete(foundEnrollment, connection);
            }
        }

        private void Delete_JctClassResource(ClassEnrolled classEnrolled, IConnectable connection)
        {
            IJctClassResourceDao jctClassResourceDaoImpl = new JctClassResourceDaoImpl();
            foreach (Resource resource in classEnrolled.Resources)
            {
                jctClassResourceDaoImpl.Delete(classEnrolled.Id, resource.Id, connection);
            }
        }

        private void Delete_JctClassPerson(ClassEnrolled classEnrolled, IConnectable connection)
        {
            IJctClassPersonDao jctClassPersonDaoImpl = new JctClassPersonDaoImpl();
            foreach (Person person in classEnrolled.GetStaff())
            {
                jctClassPersonDaoImpl.Delete(classEnrolled.Id, person.Id, connection);
            }
        }

        private void Delete_JctClassPeriod(ClassEnrolled classEnrolled, IConnectable connection)
        {
            IJctClassPeriodDao jctClassPeriodDaoImpl = new JctClassPeriodDaoImpl();
            foreach (Enumeration period in classEnrolled.Periods)
            {
                jctClassPeriodDaoImpl.Delete(classEnrolled.Id, period.Id, connection);
            }
        }

        private void Delete_LineItem(ClassEnrolled classEnrolled, IConnectable connection)
        {
            ILineItemDao lineItemDaoImpl = new LineItemDaoImpl();
            List<LineItem> foundLineItems = lineItemDaoImpl.Read_WithClassEnrolledId(classEnrolled.Id, connection);
            foreach (LineItem foundLineItem in foundLineItems)
            {
                lineItemDaoImpl.Delete(foundLineItem, connection);
            }
        }

        public List<ClassEnrolled> Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            string sql = "SELECT * FROM class WHERE academic_session_id = " + academicSessionId;

            return Read(sql, connection);
        }

        public List<ClassEnrolled> Read_UsingCourseId(int courseId, IConnectable connection)
        {
            string sql = "SELECT * FROM class WHERE course_id = " + courseId;

            return Read(sql, connection);
        }

        public List<ClassEnrolled> Read(ClassEnrolled classEnrolled, IConnectable connection)
        {
            StringBuilder sql = new StringBuilder("");

            ClassEnrolledDao classEnrolledDao = daoObjectMapper.MapClassEnrolled(classEnrolled);

            sql.Append("SELECT * FROM class WHERE 1=1");
            if (classEnrolledDao.class_id != null)
                sql.Append("AND class_id = " + classEnrolledDao.class_id + " ");
            else
            {
                if (classEnrolledDao.name != null)
                    sql.Append("AND name = '" + classEnrolledDao.name + "' ");
                if (classEnrolledDao.class_code != null)
                    sql.Append("AND class_code = '" + classEnrolledDao.class_code + "' ");
                if (classEnrolledDao.enum_class_id != null)
                    sql.Append("AND enum_class_id = " + classEnrolledDao.enum_class_id + " ");
                if (classEnrolledDao.room != null)
                    sql.Append("AND room = '" + classEnrolledDao.room + "' ");
                if (classEnrolledDao.course_id != null)
                    sql.Append("AND course_id = " + classEnrolledDao.course_id + " ");
                if (classEnrolledDao.academic_session_id != null)
                    sql.Append("AND academic_session_id = " + classEnrolledDao.academic_session_id + " ");
            }

            return Read(sql.ToString(), connection);
        }

        public ClassEnrolled Read(int classId, IConnectable connection)
        {
            string sql = "SELECT * FROM class WHERE class_id = " + classId;
            List<ClassEnrolled> results = Read(sql, connection);
            if (results.Count > 0)
                return results[0];
            else
                return null;
        }

        private List<ClassEnrolled> Read(string sql, IConnectable connection)
        {
            List<ClassEnrolledDao> foundClassEnrolledDaos = new List<ClassEnrolledDao>();
            SqlDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql.ToString());
                while (reader.Read())
                {
                    ClassEnrolledDao foundClassEnrolledDao = new ClassEnrolledDao();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                if (value != DBNull.Value)
                                    foundClassEnrolledDao.class_id = (int)value;
                                else
                                    foundClassEnrolledDao.class_id = null;
                                break;
                            case 1:
                                foundClassEnrolledDao.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 2:
                                foundClassEnrolledDao.class_code = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    foundClassEnrolledDao.enum_class_id = (int)value;
                                else
                                    foundClassEnrolledDao.enum_class_id = null;
                                break;
                            case 4:
                                foundClassEnrolledDao.room = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    foundClassEnrolledDao.course_id = (int)value;
                                else
                                    foundClassEnrolledDao.course_id = null;
                                    break;
                            case 6:
                                if (value != DBNull.Value)
                                    foundClassEnrolledDao.academic_session_id = (int)value;
                                else
                                    foundClassEnrolledDao.academic_session_id = null;
                                break;
                            case 7:
                                if (value != DBNull.Value)
                                    foundClassEnrolledDao.created = (DateTime)value;
                                else
                                    foundClassEnrolledDao.created = null;
                                break;
                            case 8:
                                foundClassEnrolledDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 9:
                                if (value != DBNull.Value)
                                    foundClassEnrolledDao.changed = (DateTime)value;
                                else
                                    foundClassEnrolledDao.changed = null;
                                break;
                            case 10:
                                foundClassEnrolledDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in class table: " + reader.GetName(i));
                        }
                    }
                    foundClassEnrolledDaos.Add(foundClassEnrolledDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<ClassEnrolled> results = new List<ClassEnrolled>();
            foreach (ClassEnrolledDao foundClassEnrolledDao in foundClassEnrolledDaos)
            {
                AcademicSession foundAcademicSession = Read_AcademicSession((int)foundClassEnrolledDao.academic_session_id, connection);
                Course foundCourse = Read_Course((int)foundClassEnrolledDao.course_id, connection);
                results.Add(daoObjectMapper.MapClassEnrolled(foundClassEnrolledDao, foundAcademicSession, foundCourse));
            }

            foreach (ClassEnrolled result in results)
            {
                result.Resources.AddRange(Read_JctClassResource(result.Id, connection));
                result.Periods.AddRange(Read_JctClassPeriod(result.Id, connection));
                result.SetStaff(Read_JctClassPerson_Staff(result.Id, connection), Read_JctClassPerson_Roles(result.Id, connection));
            }

            return results;
        }

        private AcademicSession Read_AcademicSession(int academicSessionId, IConnectable connection)
        {
            IAcademicSessionDao academicSessionDaoImpl = new AcademicSessionDaoImpl();
            return academicSessionDaoImpl.Read_UsingAcademicSessionId(academicSessionId, connection);
        }

        private Course Read_Course(int courseId, IConnectable connection)
        {
            ICourseDao courseDaoImpl = new CourseDaoImpl();
            return courseDaoImpl.Read(courseId, connection);
        }

        private List<Enumeration> Read_JctClassPeriod(int classEnrolledId, IConnectable connection)
        {
            List<Enumeration> foundPeriods = new List<Enumeration>();
            List<int> idsFound = new List<int>();

            IJctClassPeriodDao jctClassPeriodDaoImpl = new JctClassPeriodDaoImpl();
            List<JctClassPeriodDao> foundJctClassPeriodDaos = jctClassPeriodDaoImpl.Read(classEnrolledId, connection);
            foreach (JctClassPeriodDao foundJctClassPeriodDao in foundJctClassPeriodDaos)
            {
                idsFound.Add((int)foundJctClassPeriodDao.enum_period_id);
            }

            foreach (int idFound in idsFound)
            {
                foundPeriods.Add(Enum_Period.GetEnumFromId(idFound));
            }

            return foundPeriods;
        }

        private List<Person> Read_JctClassPerson_Staff(int classEnrolledId, IConnectable connection)
        {
            IPersonDao personDaoImpl = new PersonDaoImpl();
            IJctClassPersonDao jctClassPersonDaoImpl = new JctClassPersonDaoImpl();

            List<Person> foundPersons = new List<Person>();
            List<JctClassPersonDao> foundJctClassPersonDaos =
                jctClassPersonDaoImpl.Read_WithClassId(classEnrolledId, connection);

            foreach (JctClassPersonDao foundJctClassPersonDao in foundJctClassPersonDaos)
            {
                int personId = (int)foundJctClassPersonDao.person_id;
                foundPersons.Add(personDaoImpl.Read_UsingPersonId(personId, connection));
            }

            return foundPersons;
        }

        private List<Enumeration> Read_JctClassPerson_Roles(int classEnrolledId, IConnectable connection)
        {
            IJctClassPersonDao jctClassPersonDaoImpl = new JctClassPersonDaoImpl();

            List<Enumeration> foundRoles = new List<Enumeration>();
            List<JctClassPersonDao> foundJctClassPersonDaos =
                jctClassPersonDaoImpl.Read_WithClassId(classEnrolledId, connection);

            foreach (JctClassPersonDao foundJctClassPersonDao in foundJctClassPersonDaos)
            {
                foundRoles.Add(Enum_Role.GetEnumFromId((int)foundJctClassPersonDao.enum_role_id));
            }

            return foundRoles;
        }

        private List<Resource> Read_JctClassResource(int classEnrolledId, IConnectable connection)
        {
            IResourceDao resourceDaoImpl = new ResourceDaoImpl();
            IJctClassResourceDao jctClassResourceDaoImpl = new JctClassResourceDaoImpl();

            List<Resource> foundResources = new List<Resource>();
            List<JctClassResourceDao> foundJctClassResourceDaos =
                jctClassResourceDaoImpl.Read_WithClassId(classEnrolledId, connection);

            foreach (JctClassResourceDao foundJctClassResourceDao in foundJctClassResourceDaos)
            {
                int resourceId = (int)foundJctClassResourceDao.resource_id;
                foundResources.Add(resourceDaoImpl.Read_UsingResourceId(resourceId, connection));
            }

            return foundResources;
        }

        public void Update(ClassEnrolled classEnrolled, IConnectable connection)
        {
            List<ClassEnrolled> foundClassEnrolleds = Read(classEnrolled, connection);
            if (foundClassEnrolleds.Count == 0)
                Write(classEnrolled, connection);
            else if (foundClassEnrolleds.Count == 1)
            {
                classEnrolled.Id = foundClassEnrolleds[0].Id;
                UpdateInternals(classEnrolled, connection);
            }
            else if (foundClassEnrolleds.Count > 1)
                throw new Exception("Multiple ClassEnrolleds found matching the description of:\n" +
                    classEnrolled.ToString());
        }

        private void UpdateInternals(ClassEnrolled classEnrolled, IConnectable connection)
        {
            ClassEnrolledDao classEnrolledDao = daoObjectMapper.MapClassEnrolled(classEnrolled);

            if (classEnrolledDao.class_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE class SET ");
                sql.Append("name = " + (classEnrolledDao.name != null ? "'" + classEnrolledDao.name + "', " : "NULL, "));
                sql.Append("class_code = " + (classEnrolledDao.class_code != null ? "'" + classEnrolledDao.class_code + "', " : "NULL, "));
                sql.Append("enum_class_id = " + (classEnrolledDao.enum_class_id != null ? classEnrolledDao.enum_class_id + ", " : "NULL, "));
                sql.Append("room = " + (classEnrolledDao.room != null ? "'" + classEnrolledDao.room + "', " : "NULL, "));
                sql.Append("course_id = " + (classEnrolledDao.course_id != null ? classEnrolledDao.course_id + ", " : "NULL, "));
                sql.Append("academic_session_id = " + (classEnrolledDao.academic_session_id != null ? classEnrolledDao.academic_session_id + ", " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE class_id = " + classEnrolledDao.class_id);

                connection.ExecuteNonQuery(sql.ToString());

                UpdateJctClassPeriod(classEnrolled.Periods, classEnrolled.Id, connection);
                UpdateResource(classEnrolled.Resources, classEnrolled.Id, connection);
                UpdateStaff(classEnrolled.GetStaff(), classEnrolled.GetStaffRoles(), classEnrolled, connection);
            }
            else
            {
                throw new Exception("The following Class needs to be inserted into the database, before it can be updated.\n" + classEnrolled.ToString());
            }
        }

        public ClassEnrolled Write(ClassEnrolled classEnrolled, IConnectable connection)
        {
            ClassEnrolledDao classEnrolledDao = daoObjectMapper.MapClassEnrolled(classEnrolled);

            StringBuilder sql = new StringBuilder("INSERT INTO class (");
            sql.Append("name, class_code, enum_class_id, room, course_id, academic_session_id, created, created_by");
            sql.Append(") VALUES (");
            sql.Append(classEnrolledDao.name != null ? "'" + classEnrolledDao.name + "', " : "NULL, ");
            sql.Append(classEnrolledDao.class_code != null ? "'" + classEnrolledDao.class_code + "', " : "NULL, ");
            sql.Append(classEnrolledDao.enum_class_id != null ? classEnrolledDao.enum_class_id + ", " : "NULL, ");
            sql.Append(classEnrolledDao.room != null ? "'" + classEnrolledDao.room + "', " : "NULL, ");
            sql.Append(classEnrolledDao.course_id != null ? classEnrolledDao.course_id + ", " : "NULL, ");
            sql.Append(classEnrolledDao.academic_session_id != null ? classEnrolledDao.academic_session_id + ", " : "NULL, ");
            sql.Append("GETDATE(), ");
            sql.Append("'Netus2')");

            classEnrolledDao.class_id = connection.InsertNewRecord(sql.ToString());

            AcademicSession foundAcademicSession = Read_AcademicSession((int)classEnrolledDao.academic_session_id, connection);
            Course foundCourse = Read_Course((int)classEnrolledDao.course_id, connection);
            ClassEnrolled result = daoObjectMapper.MapClassEnrolled(classEnrolledDao, foundAcademicSession, foundCourse);

            result.Resources = UpdateResource(classEnrolled.Resources, result.Id, connection);
            result.Periods = UpdateJctClassPeriod(classEnrolled.Periods, result.Id, connection);
            result = UpdateStaff(classEnrolled.GetStaff(), classEnrolled.GetStaffRoles(), result, connection);

            return result;
        }

        private List<Resource> UpdateResource(List<Resource> resources, int classId, IConnectable connection)
        {
            List<Resource> updatedResources = new List<Resource>();
            IResourceDao resourceDaoImpl = new ResourceDaoImpl();
            foreach (Resource resource in resources)
            {
                if (resource.Id != -1)
                {
                    updatedResources.AddRange(resourceDaoImpl.Read(resource, connection));
                }
                else
                    throw new Exception("This resource must be added to the database before they can be assigned to a class:\n" + resource.ToString());
            }

            UpdateJctClassResource(updatedResources, classId, connection);

            return updatedResources;
        }

        private ClassEnrolled UpdateStaff(List<Person> staffs, List<Enumeration> roles, ClassEnrolled classEnrolled, IConnectable connection)
        {
            List<JctClassPersonDao> updatedJctClassPersonDaos = new List<JctClassPersonDao>();
            IJctClassPersonDao jctClassPersonDaoImpl = new JctClassPersonDaoImpl();
            IPersonDao personDaoImpl = new PersonDaoImpl();
            List<JctClassPersonDao> foundJctClassPersonDaos =
                jctClassPersonDaoImpl.Read_WithClassId(classEnrolled.Id, connection);

            List<int> foundStaffIds = new List<int>();
            List<int> foundRoleIds = new List<int>();
            List<int> staffIds = new List<int>();
            List<int> roleIds = new List<int>();

            foreach (JctClassPersonDao foundClassPersonDao in foundJctClassPersonDaos)
            {
                foundStaffIds.Add((int)foundClassPersonDao.person_id);
                foundRoleIds.Add((int)foundClassPersonDao.enum_role_id);
            }

            foreach (Enumeration role in roles)
            {
                roleIds.Add(role.Id);
            }

            foreach (Person staff in staffs)
            {
                staffIds.Add(staff.Id);
            }

            for (int i = 0; i < staffIds.Count; ++i)
            {
                if (foundStaffIds.Contains(staffIds[i]) == false)
                    updatedJctClassPersonDaos.Add(jctClassPersonDaoImpl.Write(classEnrolled.Id, staffIds[i], roleIds[i], connection));
            }

            for (int i = 0; i < foundStaffIds.Count; ++i)
            {
                if (foundStaffIds.Count > 0 && staffIds.Contains(foundStaffIds[i]) == false)
                    jctClassPersonDaoImpl.Delete(classEnrolled.Id, foundStaffIds[i], connection);
            }

            foreach (JctClassPersonDao updatedJctClassPersonDao in updatedJctClassPersonDaos)
            {
                Person updatedStaff = personDaoImpl.Read_UsingPersonId((int)updatedJctClassPersonDao.person_id, connection);
                Enumeration updatedRole = Enum_Role.GetEnumFromId((int)updatedJctClassPersonDao.enum_role_id);

                classEnrolled.AddStaff(updatedStaff, updatedRole);
            }

            return classEnrolled;
        }

        private void UpdateJctClassResource(List<Resource> resources, int classId, IConnectable connection)
        {
            IJctClassResourceDao jctClassResourceDaoImpl = new JctClassResourceDaoImpl();
            List<JctClassResourceDao> foundJctClassResourceDaos =
                jctClassResourceDaoImpl.Read_WithClassId(classId, connection);

            List<int> foundResourceIds = new List<int>();
            List<int> resourceIds = new List<int>();

            foreach (JctClassResourceDao foundClassResourceDao in foundJctClassResourceDaos)
            {
                foundResourceIds.Add((int)foundClassResourceDao.resource_id);
            }

            foreach (Resource resource in resources)
            {
                resourceIds.Add(resource.Id);
            }

            foreach (int resourceId in resourceIds)
            {
                if (foundResourceIds.Contains(resourceId) == false)
                    jctClassResourceDaoImpl.Write(classId, resourceId, connection);
            }

            foreach (int foundResourceId in foundResourceIds)
            {
                if (resourceIds.Contains(foundResourceId) == false)
                    jctClassResourceDaoImpl.Delete(classId, foundResourceId, connection);
            }
        }

        private List<Enumeration> UpdateJctClassPeriod(List<Enumeration> periods, int classId, IConnectable connection)
        {
            List<Enumeration> updatedPeriods = new List<Enumeration>();
            IJctClassPeriodDao jctClassPeriodDaoImpl = new JctClassPeriodDaoImpl();
            List<JctClassPeriodDao> foundJctClassPeriodDaos =
                jctClassPeriodDaoImpl.Read(classId, connection);
            List<int> periodIds = new List<int>();
            List<int> foundPeriodIds = new List<int>();

            foreach (Enumeration period in periods)
            {
                periodIds.Add(period.Id);
            }

            foreach (JctClassPeriodDao jctClassPeriodDao in foundJctClassPeriodDaos)
            {
                foundPeriodIds.Add((int)jctClassPeriodDao.enum_period_id);
            }

            foreach (int periodId in periodIds)
            {
                if (foundPeriodIds.Contains(periodId) == false)
                    jctClassPeriodDaoImpl.Write(classId, periodId, connection);

                int enumPeriodId = (int)jctClassPeriodDaoImpl.Read(classId, periodId, connection).enum_period_id;

                updatedPeriods.Add(Enum_Period.GetEnumFromId(enumPeriodId));
            }

            foreach (int foundPeriodId in foundPeriodIds)
            {
                if (periodIds.Contains(foundPeriodId) == false)
                    jctClassPeriodDaoImpl.Delete(classId, foundPeriodId, connection);
            }

            return updatedPeriods;
        }
    }
}
