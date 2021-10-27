using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class ClassEnrolledDaoImpl : IClassEnrolledDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();
        public int? _taskId = null;

        public void SetTaskId(int taskId)
        {
            _taskId = taskId;
        }

        public int? GetTaskId()
        {
            return _taskId;
        }

        public void Delete(ClassEnrolled classEnrolled, IConnectable connection)
        {
            if(classEnrolled.Id <= 0)
                throw new Exception("Cannot delete a Class which doesn't have a database-assigned ID.\n" + classEnrolled.ToString());

            Delete_LineItem(classEnrolled, connection);
            Delete_JctClassPeriod(classEnrolled, connection);
            Delete_JctClassResource(classEnrolled, connection);
            Delete_JctClassPerson(classEnrolled, connection);
            Delete_Enrollment(classEnrolled, connection);

            string sql = "DELETE FROM class WHERE " +
                "class_id = @class_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classEnrolled.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void Delete_Enrollment(ClassEnrolled classEnrolled, IConnectable connection)
        {
            IEnrollmentDao enrollmentDaoImpl = DaoImplFactory.GetEnrollmentDaoImpl();
            List<Enrollment> enrollmentsFound = enrollmentDaoImpl.Read_AllWithClassId(classEnrolled.Id, connection);
            foreach (Enrollment foundEnrollment in enrollmentsFound)
            {
                enrollmentDaoImpl.Delete(foundEnrollment, connection);
            }
        }

        private void Delete_JctClassResource(ClassEnrolled classEnrolled, IConnectable connection)
        {
            IJctClassResourceDao jctClassResourceDaoImpl = DaoImplFactory.GetJctClassResourceDaoImpl();
            foreach (Resource resource in classEnrolled.Resources)
            {
                jctClassResourceDaoImpl.Delete(classEnrolled.Id, resource.Id, connection);
            }
        }

        private void Delete_JctClassPerson(ClassEnrolled classEnrolled, IConnectable connection)
        {
            IJctClassPersonDao jctClassPersonDaoImpl = DaoImplFactory.GetJctClassPersonDaoImpl();
            foreach (Person person in classEnrolled.GetStaff())
            {
                jctClassPersonDaoImpl.Delete(classEnrolled.Id, person.Id, connection);
            }
        }

        private void Delete_JctClassPeriod(ClassEnrolled classEnrolled, IConnectable connection)
        {
            IJctClassPeriodDao jctClassPeriodDaoImpl = DaoImplFactory.GetJctClassPeriodDaoImpl();
            foreach (Enumeration period in classEnrolled.Periods)
            {
                jctClassPeriodDaoImpl.Delete(classEnrolled.Id, period.Id, connection);
            }
        }

        private void Delete_LineItem(ClassEnrolled classEnrolled, IConnectable connection)
        {
            ILineItemDao lineItemDaoImpl = DaoImplFactory.GetLineItemDaoImpl();
            List<LineItem> foundLineItems = lineItemDaoImpl.Read_AllWithClassEnrolledId(classEnrolled.Id, connection);
            foreach (LineItem foundLineItem in foundLineItems)
            {
                lineItemDaoImpl.Delete(foundLineItem, connection);
            }
        }

        public List<ClassEnrolled> Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection)
        {
            string sql = "SELECT * FROM class WHERE academic_session_id = @academic_session_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@academic_session_id", academicSessionId));

            List<ClassEnrolled> results = Read(sql, connection, parameters);

            return results;
        }

        public List<ClassEnrolled> Read_UsingCourseId(int courseId, IConnectable connection)
        {
            string sql = "SELECT * FROM class WHERE course_id = @course_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@course_id", courseId));

            List<ClassEnrolled> results = Read(sql, connection, parameters);

            return results;
        }

        public ClassEnrolled Read_UsingClassId(int classId, IConnectable connection)
        {
            string sql = "SELECT * FROM class WHERE class_id = @class_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@class_id", classId));

            List<ClassEnrolled> results = Read(sql, connection, parameters);

            if (results.Count > 0)
                return results[0];
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching classId: " + classId);
        }

        public List<ClassEnrolled> Read(ClassEnrolled classEnrolled, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapClassEnrolled(classEnrolled);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM class WHERE 1=1 ");
            if (row["class_id"] != DBNull.Value)
            {
                sql.Append("AND class_id = @class_id ");
                parameters.Add(new SqlParameter("@class_id", row["class_id"]));
            }                
            else
            {
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("AND name = @name ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }

                if (row["class_code"] != DBNull.Value)
                {
                    sql.Append("AND class_code = @class_code ");
                    parameters.Add(new SqlParameter("@class_code", row["class_code"]));
                }

                if (row["enum_class_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_class_id = @enum_class_id ");
                    parameters.Add(new SqlParameter("@enum_class_id", row["enum_class_id"]));
                }

                if (row["room"] != DBNull.Value)
                {
                    sql.Append("AND room = @room ");
                    parameters.Add(new SqlParameter("@room", row["room"]));
                }

                if (row["course_id"] != DBNull.Value)
                {
                    sql.Append("AND course_id = @course_id ");
                    parameters.Add(new SqlParameter("@course_id", row["course_id"]));
                }

                if (row["academic_session_id"] != DBNull.Value)
                {
                    sql.Append("AND academic_session_id = @academic_session_id ");
                    parameters.Add(new SqlParameter("@academic_session_id", row["academic_session_id"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<ClassEnrolled> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtClassEnrolled = DataTableFactory.CreateDataTable_Netus2_ClassEnrolled();
            dtClassEnrolled = connection.ReadIntoDataTable(sql, dtClassEnrolled, parameters);

            List<ClassEnrolled> results = new List<ClassEnrolled>();
            foreach (DataRow row in dtClassEnrolled.Rows)
            {
                AcademicSession foundAcademicSession = Read_AcademicSession((int)row["academic_session_id"], connection);
                Course foundCourse = Read_Course((int)row["course_id"], connection);
                results.Add(daoObjectMapper.MapClassEnrolled(row, foundAcademicSession, foundCourse));
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
            IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
            return academicSessionDaoImpl.Read_UsingAcademicSessionId(academicSessionId, connection);
        }

        private Course Read_Course(int courseId, IConnectable connection)
        {
            ICourseDao courseDaoImpl = DaoImplFactory.GetCourseDaoImpl();
            return courseDaoImpl.Read_UsingCourseId(courseId, connection);
        }

        private List<Enumeration> Read_JctClassPeriod(int classEnrolledId, IConnectable connection)
        {
            List<Enumeration> foundPeriods = new List<Enumeration>();
            List<int> idsFound = new List<int>();

            IJctClassPeriodDao jctClassPeriodDaoImpl = DaoImplFactory.GetJctClassPeriodDaoImpl();
            List<DataRow> foundDataRows = jctClassPeriodDaoImpl.Read_AllWithClassId(classEnrolledId, connection);
            foreach (DataRow foundDataRow in foundDataRows)
            {
                idsFound.Add((int)foundDataRow["enum_period_id"]);
            }

            foreach (int idFound in idsFound)
            {
                foundPeriods.Add(Enum_Period.GetEnumFromId(idFound));
            }

            return foundPeriods;
        }

        private List<Person> Read_JctClassPerson_Staff(int classEnrolledId, IConnectable connection)
        {
            IPersonDao personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
            IJctClassPersonDao jctClassPersonDaoImpl = DaoImplFactory.GetJctClassPersonDaoImpl();

            List<Person> foundPersons = new List<Person>();
            List<DataRow> foundDataRows =
                jctClassPersonDaoImpl.Read_AllWithClassId(classEnrolledId, connection);

            foreach (DataRow foundDataRow in foundDataRows)
            {
                int personId = (int)foundDataRow["person_id"];
                foundPersons.Add(personDaoImpl.Read_UsingPersonId(personId, connection));
            }

            return foundPersons;
        }

        private List<Enumeration> Read_JctClassPerson_Roles(int classEnrolledId, IConnectable connection)
        {
            IJctClassPersonDao jctClassPersonDaoImpl = DaoImplFactory.GetJctClassPersonDaoImpl();

            List<Enumeration> foundRoles = new List<Enumeration>();
            List<DataRow> foundDataRows =
                jctClassPersonDaoImpl.Read_AllWithClassId(classEnrolledId, connection);

            foreach (DataRow foundDataRow in foundDataRows)
            {
                foundRoles.Add(Enum_Role.GetEnumFromId((int)foundDataRow["enum_role_id"]));
            }

            return foundRoles;
        }

        private List<Resource> Read_JctClassResource(int classEnrolledId, IConnectable connection)
        {
            IResourceDao resourceDaoImpl = DaoImplFactory.GetResourceDaoImpl();
            IJctClassResourceDao jctClassResourceDaoImpl = DaoImplFactory.GetJctClassResourceDaoImpl();

            List<Resource> foundResources = new List<Resource>();
            List<DataRow> foundDataRows =
                jctClassResourceDaoImpl.Read_AllWithClassId(classEnrolledId, connection);

            foreach (DataRow foundDataRow in foundDataRows)
            {
                int resourceId = (int)foundDataRow["resource_id"];
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
            else
                throw new Exception(foundClassEnrolleds.Count + " ClassEnrolleds found matching the description of:\n" +
                    classEnrolled.ToString());
        }

        private void UpdateInternals(ClassEnrolled classEnrolled, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapClassEnrolled(classEnrolled);

            if (row["class_id"] != DBNull.Value)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE class SET ");
                if (row["name"] != DBNull.Value)
                {
                    sql.Append("name = @name, ");
                    parameters.Add(new SqlParameter("@name", row["name"]));
                }
                else
                    sql.Append("name = NULL, ");

                if (row["class_code"] != DBNull.Value)
                {
                    sql.Append("class_code = @class_code, ");
                    parameters.Add(new SqlParameter("@class_code", row["class_code"]));
                }
                else
                    sql.Append("class_code = NULL, ");

                if (row["enum_class_id"] != DBNull.Value)
                {
                    sql.Append("enum_class_id = @enum_class_id, ");
                    parameters.Add(new SqlParameter("@enum_class_id", row["enum_class_id"]));
                }
                else
                    sql.Append("enum_class_id = NULL, ");

                if (row["room"] != DBNull.Value)
                {
                    sql.Append("room = @room, ");
                    parameters.Add(new SqlParameter("@room", row["room"]));
                }
                else
                    sql.Append("room = NULL, ");

                if (row["course_id"] != DBNull.Value)
                {
                    sql.Append("course_id = @course_id, ");
                    parameters.Add(new SqlParameter("@course_id", row["course_id"]));
                }
                else
                    sql.Append("course_id = NULL, ");

                if (row["academic_session_id"] != DBNull.Value)
                {
                    sql.Append("academic_session_id = @academic_session_id, ");
                    parameters.Add(new SqlParameter("@academic_session_id", row["academic_session_id"]));
                }
                else
                    sql.Append("academic_session_id = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE class_id = @class_id");
                parameters.Add(new SqlParameter("@class_id", row["class_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);

                UpdateJctClassPeriod(classEnrolled.Periods, classEnrolled.Id, connection);
                UpdateResource(classEnrolled.Resources, classEnrolled.Id, connection);
                UpdateStaff(classEnrolled.GetStaff(), classEnrolled.GetStaffRoles(), classEnrolled, connection);
            }
            else
                throw new Exception("The following Class needs to be inserted into the database, before it can be updated.\n" + classEnrolled.ToString());
        }

        public ClassEnrolled Write(ClassEnrolled classEnrolled, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapClassEnrolled(classEnrolled);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["name"] != DBNull.Value)
            {
                sqlValues.Append("@name, ");
                parameters.Add(new SqlParameter("@name", row["name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["class_code"] != DBNull.Value)
            {
                sqlValues.Append("@class_code, ");
                parameters.Add(new SqlParameter("@class_code", row["class_code"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_class_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_class_id, ");
                parameters.Add(new SqlParameter("@enum_class_id", row["enum_class_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["room"] != DBNull.Value)
            {
                sqlValues.Append("@room, ");
                parameters.Add(new SqlParameter("@room", row["room"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["course_id"] != DBNull.Value)
            {
                sqlValues.Append("@course_id, ");
                parameters.Add(new SqlParameter("@course_id", row["course_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["academic_session_id"] != DBNull.Value)
            {
                sqlValues.Append("@academic_session_id, ");
                parameters.Add(new SqlParameter("@academic_session_id", row["academic_session_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql = "INSERT INTO class " +
                "(name, class_code, enum_class_id, room, course_id, academic_session_id, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["class_id"] = connection.InsertNewRecord(sql.ToString(), parameters);

            AcademicSession foundAcademicSession = Read_AcademicSession((int)row["academic_session_id"], connection);
            Course foundCourse = Read_Course((int)row["course_id"], connection);
            ClassEnrolled result = daoObjectMapper.MapClassEnrolled(row, foundAcademicSession, foundCourse);

            result.Resources = UpdateResource(classEnrolled.Resources, result.Id, connection);
            result.Periods = UpdateJctClassPeriod(classEnrolled.Periods, result.Id, connection);
            result = UpdateStaff(classEnrolled.GetStaff(), classEnrolled.GetStaffRoles(), result, connection);

            return result;
        }

        private List<Resource> UpdateResource(List<Resource> resources, int classId, IConnectable connection)
        {
            List<Resource> updatedResources = new List<Resource>();
            IResourceDao resourceDaoImpl = DaoImplFactory.GetResourceDaoImpl();
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
            List<DataRow> updatedJctClassPersonDaos = new List<DataRow>();
            IJctClassPersonDao jctClassPersonDaoImpl = DaoImplFactory.GetJctClassPersonDaoImpl();
            IPersonDao personDaoImpl = DaoImplFactory.GetPersonDaoImpl();
            List<DataRow> foundJctClassPersonDaos =
                jctClassPersonDaoImpl.Read_AllWithClassId(classEnrolled.Id, connection);

            List<int> foundStaffIds = new List<int>();
            List<int> foundRoleIds = new List<int>();
            List<int> staffIds = new List<int>();
            List<int> roleIds = new List<int>();

            foreach (DataRow foundClassPersonDao in foundJctClassPersonDaos)
            {
                foundStaffIds.Add((int)foundClassPersonDao["person_id"]);
                foundRoleIds.Add((int)foundClassPersonDao["enum_role_id"]);
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

            foreach (DataRow updatedJctClassPersonDao in updatedJctClassPersonDaos)
            {
                Person updatedStaff = personDaoImpl.Read_UsingPersonId((int)updatedJctClassPersonDao["person_id"], connection);
                Enumeration updatedRole = Enum_Role.GetEnumFromId((int)updatedJctClassPersonDao["enum_role_id"]);

                classEnrolled.AddStaff(updatedStaff, updatedRole);
            }

            return classEnrolled;
        }

        private void UpdateJctClassResource(List<Resource> resources, int classId, IConnectable connection)
        {
            IJctClassResourceDao jctClassResourceDaoImpl = DaoImplFactory.GetJctClassResourceDaoImpl();
            List<DataRow> foundJctClassResourceDaos =
                jctClassResourceDaoImpl.Read_AllWithClassId(classId, connection);

            List<int> foundResourceIds = new List<int>();
            List<int> resourceIds = new List<int>();

            foreach (DataRow foundClassResourceDao in foundJctClassResourceDaos)
            {
                foundResourceIds.Add((int)foundClassResourceDao["resource_id"]);
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
            IJctClassPeriodDao jctClassPeriodDaoImpl = DaoImplFactory.GetJctClassPeriodDaoImpl();
            List<DataRow> foundJctClassPeriodDaos =
                jctClassPeriodDaoImpl.Read_AllWithClassId(classId, connection);
            List<int> periodIds = new List<int>();
            List<int> foundPeriodIds = new List<int>();

            foreach (Enumeration period in periods)
            {
                periodIds.Add(period.Id);
            }

            foreach (DataRow jctClassPeriodDao in foundJctClassPeriodDaos)
            {
                foundPeriodIds.Add((int)jctClassPeriodDao["enum_period_id"]);
            }

            foreach (int periodId in periodIds)
            {
                if (foundPeriodIds.Contains(periodId) == false)
                    jctClassPeriodDaoImpl.Write(classId, periodId, connection);

                DataRow jctClassPeriodDao = jctClassPeriodDaoImpl.Read(classId, periodId, connection);

                if(jctClassPeriodDao != null)
                {
                    int enumPeriodId = (int)jctClassPeriodDao["enum_period_id"];

                    updatedPeriods.Add(Enum_Period.GetEnumFromId(enumPeriodId));
                }                
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
