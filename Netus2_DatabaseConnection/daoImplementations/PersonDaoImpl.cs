using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2_DatabaseConnection.daoImplementations
{
    public class PersonDaoImpl : IPersonDao
    {
        DaoObjectMapper daoObjectMapper = new DaoObjectMapper();

        public void Delete(Person person, IConnectable connection)
        {
            Delete_JctPersonPerson(person, connection);
            Delete_JctPersonRole(person, connection);
            Delete_JctPersonApp(person, connection);
            Delete_PhoneNumber(person, connection);
            Delete_JctPersonAddress(person, connection);
            Delete_EmploymentSession(person, connection);
            Delete_UniqueIdentifier(person, connection);
            Delete_Enrollment(person, connection);
            Delete_Mark(person, connection);
            Delete_JctClassPerson(person, connection);

            DataRow row = daoObjectMapper.MapPerson(person);

            StringBuilder sql = new StringBuilder("DELETE FROM person WHERE 1=1 ");
            sql.Append("AND person_id " + (row["person_id"] != DBNull.Value ? "= " + row["person_id"] + " " : "IS NULL "));
            sql.Append("AND first_name " + (row["first_name"] != DBNull.Value ? "LIKE '" + row["first_name"] + "' " : "IS NULL "));
            sql.Append("AND middle_name " + (row["middle_name"] != DBNull.Value ? "LIKE '" + row["middle_name"] + "' " : "IS NULL "));
            sql.Append("AND last_name " + (row["last_name"] != DBNull.Value ? "LIKE '" + row["last_name"] + "' " : "IS NULL "));
            sql.Append("AND birth_date " + (row["birth_date"] != DBNull.Value ? "= '" + row["birth_date"] + "' " : "IS NULL "));
            sql.Append("AND enum_gender_id " + (row["enum_gender_id"] != DBNull.Value ? "= " + row["enum_gender_id"] + " " : "IS NULL "));
            sql.Append("AND enum_ethnic_id " + (row["enum_ethnic_id"] != DBNull.Value ? "= " + row["enum_ethnic_id"] + " " : "IS NULL "));
            sql.Append("AND enum_residence_status_id " + (row["enum_residence_status_id"] != DBNull.Value ? "= " + row["enum_residence_status_id"] + " " : "IS NULL "));
            sql.Append("AND login_name " + (row["login_name"] != DBNull.Value ? "LIKE '" + row["login_name"] + "' " : "IS NULL "));
            sql.Append("AND login_pw " + (row["login_pw"] != DBNull.Value ? "LIKE '" + row["login_pw"] + "' " : "IS NULL"));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_JctClassPerson(Person person, IConnectable connection)
        {
            IJctClassPersonDao jctClassPersonDaoImpl = DaoImplFactory.GetJctClassPersonDaoImpl();
            List<DataRow> foundJctClassPersonDaos =
                jctClassPersonDaoImpl.Read_WithPersonId(person.Id, connection);

            foreach (DataRow foundClassPersonDao in foundJctClassPersonDaos)
            {
                jctClassPersonDaoImpl.Delete((int)foundClassPersonDao["class_id"], person.Id, connection);
            }
        }

        private void Delete_UniqueIdentifier(Person person, IConnectable connection)
        {
            IUniqueIdentifierDao uniqueIdentifierDaoImpl = DaoImplFactory.GetUniqueIdentifierDaoImpl();
            List<UniqueIdentifier> foundUniqueIdentifiers = uniqueIdentifierDaoImpl.Read(null, person.Id, connection);
            foreach (UniqueIdentifier uniqueIdentifier in foundUniqueIdentifiers)
            {
                uniqueIdentifierDaoImpl.Delete(uniqueIdentifier, person.Id, connection);
            }
        }

        private void Delete_EmploymentSession(Person person, IConnectable connection)
        {
            IEmploymentSessionDao employmentSessionDaoImpl = DaoImplFactory.GetEmploymentSessionDaoImpl();
            List<EmploymentSession> foundEmploymentSessions = employmentSessionDaoImpl.Read_WithPersonId(null, person.Id, connection);
            foreach (EmploymentSession fondEmploymentSession in foundEmploymentSessions)
            {
                employmentSessionDaoImpl.Delete_WithPersonId(fondEmploymentSession, person.Id, connection);
            }
        }

        private void Delete_Enrollment(Person person, IConnectable connection)
        {
            IEnrollmentDao enrollmentDaoImpl = DaoImplFactory.GetEnrollmentDaoImpl();
            List<Enrollment> foundEnrollments = enrollmentDaoImpl.Read(null, person.Id, connection);
            foreach (Enrollment enrollment in foundEnrollments)
            {
                enrollmentDaoImpl.Delete(enrollment, person.Id, connection);
            }
        }

        private void Delete_Mark(Person person, IConnectable connection)
        {
            IMarkDao markDaoImpl = DaoImplFactory.GetMarkDaoImpl();
            List<Mark> foundMarks = markDaoImpl.Read(null, person.Id, connection);
            foreach (Mark mark in foundMarks)
            {
                markDaoImpl.Delete(mark, person.Id, connection);
            }
        }

        private void Delete_JctPersonAddress(Person person, IConnectable connection)
        {
            IJctPersonAddressDao jctPersonAddressDaoImpl = DaoImplFactory.GetJctPersonAddressDaoImpl();
            foreach (Address address in person.Addresses)
            {
                jctPersonAddressDaoImpl.Delete(person.Id, address.Id, connection);
            }
        }

        private void Delete_PhoneNumber(Person person, IConnectable connection)
        {
            IPhoneNumberDao phoneNumberDaoImpl = DaoImplFactory.GetPhoneNumberDaoImpl();
            foreach (PhoneNumber phoneNumber in person.PhoneNumbers)
            {
                phoneNumberDaoImpl.Delete(phoneNumber, person.Id, connection);
            }
        }

        private void Delete_JctPersonApp(Person person, IConnectable connection)
        {
            IJctPersonAppDao jctPersonAppDaoImpl = DaoImplFactory.GetJctPersonAppDaoImpl();
            foreach (Application app in person.Applications)
            {
                jctPersonAppDaoImpl.Delete(person.Id, app.Id, connection);
            }
        }

        private void Delete_JctPersonRole(Person person, IConnectable connection)
        {
            IJctPersonRoleDao jctPersonRoleDaoImpl = DaoImplFactory.GetJctPersonRoleDaoImpl();
            foreach (Enumeration role in person.Roles)
            {
                jctPersonRoleDaoImpl.Delete(person.Id, role.Id, connection);
            }
        }

        private void Delete_JctPersonPerson(Person person, IConnectable connection)
        {
            IJctPersonPersonDao jctPersonPersonDaoImpl = DaoImplFactory.GetJctPersonPersonDaoImpl();
            foreach (int relationId in person.Relations)
            {
                jctPersonPersonDaoImpl.Delete(person.Id, relationId, connection);
            }
        }

        public Person Read_UsingPersonId(int personId, IConnectable connection)
        {
            string sql = "SELECT * FROM person WHERE person_id = " + personId;

            List<Person> results = Read(sql, connection);
            if (results.Count == 0)
                return null;
            else
                return results[0];
        }

        public List<Person> Read(Person person, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPerson(person);

            StringBuilder sql = new StringBuilder("SELECT * FROM person WHERE 1=1 ");
            if (row["person_id"] != DBNull.Value)
                sql.Append("AND person_id = " + row["person_id"] + " ");
            else
            {
                if (row["first_name"] != DBNull.Value)
                    sql.Append("AND first_name LIKE '" + row["first_name"] + "' ");
                if (row["middle_name"] != DBNull.Value)
                    sql.Append("AND middle_name LIKE '" + row["middle_name"] + "' ");
                if (row["last_name"] != DBNull.Value)
                    sql.Append("AND last_name LIKE '" + row["last_name"] + "' ");
                if (row["birth_date"] != DBNull.Value)
                    sql.Append("AND datediff(day, birth_date, '" + row["birth_date"].ToString() + "') = 0 ");
                if (row["enum_gender_id"] != DBNull.Value)
                    sql.Append("AND enum_gender_id = " + row["enum_gender_id"] + " ");
                if (row["enum_ethnic_id"] != DBNull.Value)
                    sql.Append("AND enum_ethnic_id = " + row["enum_ethnic_id"] + " ");
                if (row["enum_residence_status_id"] != DBNull.Value)
                    sql.Append("AND enum_residence_status_id = " + row["enum_residence_status_id"] + " ");
                if (row["login_name"] != DBNull.Value)
                    sql.Append("AND login_name LIKE '" + row["login_name"] + "' ");
                if (row["login_pw"] != DBNull.Value)
                    sql.Append("AND login_pw LIKE '" + row["login_pw"] + "' ");
            }


            return Read(sql.ToString(), connection);
        }

        private List<Person> Read(string sql, IConnectable connection)
        {
            DataTable dtPerson = new DataTableFactory().Dt_Netus2_Person;
            dtPerson = connection.ReadIntoDataTable(sql, dtPerson).Result;

            List<Person> results = new List<Person>();
            foreach (DataRow row in dtPerson.Rows)
            {
                results.Add(daoObjectMapper.MapPerson(row));
            }

            foreach (Person result in results)
            {
                result.Applications.AddRange(Read_JctPersonApp(result.Id, connection));
                result.Roles.AddRange(Read_JctPersonRole(result.Id, connection));
                result.Relations.AddRange(Read_JctPersonPerson(result.Id, connection));
                result.Addresses.AddRange(Read_JctPersonAddress(result.Id, connection));
                result.PhoneNumbers.AddRange(DaoImplFactory.GetPhoneNumberDaoImpl().Read(null, result.Id, connection));
                result.UniqueIdentifiers.AddRange(DaoImplFactory.GetUniqueIdentifierDaoImpl().Read(null, result.Id, connection));
                result.EmploymentSessions.AddRange(DaoImplFactory.GetEmploymentSessionDaoImpl().Read_WithPersonId(null, result.Id, connection));
                result.Enrollments.AddRange(DaoImplFactory.GetEnrollmentDaoImpl().Read(null, result.Id, connection));
                result.Marks.AddRange(DaoImplFactory.GetMarkDaoImpl().Read(null, result.Id, connection));
            }

            return results;
        }

        private List<Application> Read_JctPersonApp(int personId, IConnectable connection)
        {
            IApplicationDao appDaoImpl = DaoImplFactory.GetApplicationDaoImpl();
            IJctPersonAppDao jctPersonAppDaoImpl = DaoImplFactory.GetJctPersonAppDaoImpl();

            List<Application> foundApplications = new List<Application>();
            List<DataRow> foundJctPersonAppDaos = jctPersonAppDaoImpl.Read_WithPersonId(personId, connection);

            foreach (DataRow foundJctPersonAppDao in foundJctPersonAppDaos)
            {
                int appId = (int)foundJctPersonAppDao["app_id"];
                foundApplications.Add(appDaoImpl.Read_UsingAppId(appId, connection));
            }

            return foundApplications;
        }

        private List<Address> Read_JctPersonAddress(int personId, IConnectable connection)
        {
            IAddressDao addressDaoImpl = DaoImplFactory.GetAddressDaoImpl();
            IJctPersonAddressDao jctPersonAddressDaoImpl = DaoImplFactory.GetJctPersonAddressDaoImpl();

            List<Address> foundAddresss = new List<Address>();
            List<DataRow> foundJctPersonAddressDaos = jctPersonAddressDaoImpl.Read_WithPersonId(personId, connection);

            foreach (DataRow foundJctPersonAddressDao in foundJctPersonAddressDaos)
            {
                int addressId = (int)foundJctPersonAddressDao["address_id"];
                foundAddresss.Add(addressDaoImpl.Read_UsingAdddressId(addressId, connection));
            }

            return foundAddresss;
        }

        private List<Enumeration> Read_JctPersonRole(int personId, IConnectable connection)
        {
            List<Enumeration> foundRoles = new List<Enumeration>();
            List<int> idsFound = new List<int>();

            IJctPersonRoleDao jctPersonRoleDaoImpl = DaoImplFactory.GetJctPersonRoleDaoImpl();
            List<DataRow> foundJctPersonRoleDaos = jctPersonRoleDaoImpl.Read(personId, connection);
            foreach (DataRow foundJctPersonRoleDao in foundJctPersonRoleDaos)
            {
                idsFound.Add((int)foundJctPersonRoleDao["enum_role_id"]);
            }

            foreach (int idFound in idsFound)
            {
                foundRoles.Add(Enum_Role.GetEnumFromId(idFound));
            }

            return foundRoles;
        }

        private List<int> Read_JctPersonPerson(int personId, IConnectable connection)
        {
            List<int> foundRelations = new List<int>();

            IJctPersonPersonDao jctPersonPersonDaoImpl = DaoImplFactory.GetJctPersonPersonDaoImpl();
            List<DataRow> foundJctPersonPersonDaos = jctPersonPersonDaoImpl.Read(personId, connection);

            foreach (DataRow foundJctPersonPersonDao in foundJctPersonPersonDaos)
            {
                foundRelations.Add((int)foundJctPersonPersonDao["person_two_id"]);
            }

            return foundRelations;
        }

        public void Update(Person person, IConnectable connection)
        {
            List<Person> foundPersons = Read(person, connection);
            if (foundPersons.Count == 0)
                Write(person, connection);
            else if (foundPersons.Count == 1)
            {
                person.Id = foundPersons[0].Id;
                UpdateInternals(person, connection);
            }
            else if (foundPersons.Count > 1)
                throw new Exception(foundPersons.Count + " Persons found matching the description of:\n" +
                    person.ToString());
        }

        private void UpdateInternals(Person person, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPerson(person);

            if (row["person_id"] != DBNull.Value)
            {
                StringBuilder sql = new StringBuilder("UPDATE person SET ");
                sql.Append("first_name = " + (row["first_name"] != DBNull.Value ? "'" + row["first_name"] + "', " : "NULL, "));
                sql.Append("middle_name = " + (row["middle_name"] != DBNull.Value ? "'" + row["middle_name"] + "', " : "NULL, "));
                sql.Append("last_name = " + (row["last_name"] != DBNull.Value ? "'" + row["last_name"] + "', " : "NULL, "));
                sql.Append("birth_date = " + (row["birth_date"] != DBNull.Value ? "'" + row["birth_date"] + "', " : "NULL, "));
                sql.Append("enum_gender_id = " + (row["enum_gender_id"] != DBNull.Value ? "'" + row["enum_gender_id"] + "', " : "NULL, "));
                sql.Append("enum_ethnic_id = " + (row["enum_ethnic_id"] != DBNull.Value ? "'" + row["enum_ethnic_id"] + "', " : "NULL, "));
                sql.Append("enum_residence_status_id = " + (row["enum_residence_status_id"] != DBNull.Value ? "'" + row["enum_residence_status_id"] + "', " : "NULL, "));
                sql.Append("login_name = " + (row["login_name"] != DBNull.Value ? "'" + row["login_name"] + "', " : "NULL, "));
                sql.Append("login_pw = " + (row["login_pw"] != DBNull.Value ? "'" + row["login_pw"] + "', " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE person_id = " + row["person_id"]);

                connection.ExecuteNonQuery(sql.ToString());

                UpdateEmploymentSessions(person.EmploymentSessions, person.Id, connection);
                UpdateJctPersonRole(person.Roles, person.Id, connection);
                UpdatePhoneNumbers(person.PhoneNumbers, person.Id, connection);
                UpdateJctPersonPerson(person.Relations, person.Id, connection);
                UpdateAddresses(person.Addresses, person.Id, connection);
                UpdateUniqueIdentifiers(person.UniqueIdentifiers, person.Id, connection);
                UpdateApplications(person.Applications, person.Id, connection);
                UpdatedEnrollments(person.Enrollments, person.Id, connection);
                UpdateMarks(person.Marks, person.Id, connection);
            }
            else
                throw new Exception("The following Person needs to be inserted into the database, before it can be updated.\n" + person.ToString());
        }

        public Person Write(Person person, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPerson(person);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(row["first_name"] != DBNull.Value ? "'" + row["first_name"] + "', " : "NULL, ");
            sqlValues.Append(row["middle_name"] != DBNull.Value ? "'" + row["middle_name"] + "', " : "NULL, ");
            sqlValues.Append(row["last_name"] != DBNull.Value ? "'" + row["last_name"] + "', " : "NULL, ");
            sqlValues.Append(row["birth_date"] != DBNull.Value ? "'" + row["birth_date"] + "', " : "NULL, ");
            sqlValues.Append(row["enum_gender_id"] != DBNull.Value ? row["enum_gender_id"] + ", " : "NULL, ");
            sqlValues.Append(row["enum_ethnic_id"] != DBNull.Value ? row["enum_ethnic_id"] + ", " : "NULL, ");
            sqlValues.Append(row["enum_residence_status_id"] != DBNull.Value ? row["enum_residence_status_id"] + ", " : "NULL, ");
            sqlValues.Append(row["login_name"] != DBNull.Value ? "'" + row["login_name"] + "', " : "NULL, ");
            sqlValues.Append(row["login_pw"] != DBNull.Value ? "'" + row["login_pw"] + "', " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            string sql =
                "INSERT INTO person " +
                "(first_name, middle_name, last_name, birth_date, enum_gender_id, enum_ethnic_id, " +
                "enum_residence_status_id, login_name, login_pw, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["person_id"] = connection.InsertNewRecord(sql);

            Person result = daoObjectMapper.MapPerson(row);

            result.UniqueIdentifiers = UpdateUniqueIdentifiers(person.UniqueIdentifiers, result.Id, connection);
            result.PhoneNumbers = UpdatePhoneNumbers(person.PhoneNumbers, result.Id, connection);
            result.Addresses = UpdateAddresses(person.Addresses, result.Id, connection);
            result.Applications = UpdateApplications(person.Applications, result.Id, connection);
            result.Roles = UpdateJctPersonRole(person.Roles, result.Id, connection);
            result.Relations = UpdateJctPersonPerson(person.Relations, result.Id, connection);
            result.EmploymentSessions = UpdateEmploymentSessions(person.EmploymentSessions, result.Id, connection);
            result.Enrollments = UpdatedEnrollments(person.Enrollments, result.Id, connection);
            result.Marks = UpdateMarks(person.Marks, result.Id, connection);

            return result;
        }

        private List<Mark> UpdateMarks(List<Mark> marks, int personId, IConnectable connection)
        {
            List<Mark> updatedMarks = new List<Mark>();
            IMarkDao markDaoImpl = DaoImplFactory.GetMarkDaoImpl();

            List<Mark> foundMarks =
                markDaoImpl.Read(null, personId, connection);

            foreach (Mark mark in marks)
            {
                markDaoImpl.Update(mark, personId, connection);
                updatedMarks.AddRange(markDaoImpl.Read(mark, personId, connection));
            }

            foreach (Mark foundMark in foundMarks)
            {
                if (marks.Find(x => (x.Id == foundMark.Id)) == null)
                    markDaoImpl.Delete(foundMark, personId, connection);
            }

            return updatedMarks;
        }

        private List<Enrollment> UpdatedEnrollments(List<Enrollment> enrollments, int personId, IConnectable connection)
        {
            List<Enrollment> updatedEnrollments = new List<Enrollment>();
            IEnrollmentDao enrollmentDaoImpl = DaoImplFactory.GetEnrollmentDaoImpl();

            List<Enrollment> foundEnrollments =
                enrollmentDaoImpl.Read(null, personId, connection);

            foreach (Enrollment enrollment in enrollments)
            {
                enrollmentDaoImpl.Update(enrollment, personId, connection);
                updatedEnrollments.AddRange(enrollmentDaoImpl.Read(enrollment, personId, connection));
            }

            foreach (Enrollment foundEnrollment in foundEnrollments)
            {
                if (enrollments.Find(x => (x.Id == foundEnrollment.Id)) == null)
                    enrollmentDaoImpl.Delete(foundEnrollment, personId, connection);
            }

            return updatedEnrollments;
        }

        private List<EmploymentSession> UpdateEmploymentSessions(List<EmploymentSession> employmentSessions, int personId, IConnectable connection)
        {
            List<EmploymentSession> updatedEmploymentSessions = new List<EmploymentSession>();
            IEmploymentSessionDao employmentSessionDaoImpl = DaoImplFactory.GetEmploymentSessionDaoImpl();

            List<EmploymentSession> foundEmploymentSessions =
                employmentSessionDaoImpl.Read_WithPersonId(null, personId, connection);

            foreach (EmploymentSession employmentSession in employmentSessions)
            {
                employmentSessionDaoImpl.Update(employmentSession, personId, connection);
                updatedEmploymentSessions.AddRange(employmentSessionDaoImpl.Read_WithPersonId(employmentSession, personId, connection));
            }

            foreach (EmploymentSession foundEmploymentSession in foundEmploymentSessions)
            {
                if (employmentSessions.Find(x => (x.Id == foundEmploymentSession.Id)) == null)
                    employmentSessionDaoImpl.Delete_WithPersonId(foundEmploymentSession, personId, connection);
            }

            return updatedEmploymentSessions;
        }

        private List<UniqueIdentifier> UpdateUniqueIdentifiers(List<UniqueIdentifier> uniqueIdentifieres, int personId, IConnectable connection)
        {
            List<UniqueIdentifier> updatedUniqueIdentifieres = new List<UniqueIdentifier>();
            IUniqueIdentifierDao uniqueIdentifierDaoImpl = DaoImplFactory.GetUniqueIdentifierDaoImpl();

            List<UniqueIdentifier> foundUniqueIdentifiers =
                uniqueIdentifierDaoImpl.Read(null, personId, connection);


            foreach (UniqueIdentifier uniqueIdentifier in uniqueIdentifieres)
            {
                uniqueIdentifierDaoImpl.Update(uniqueIdentifier, personId, connection);
                updatedUniqueIdentifieres.AddRange(uniqueIdentifierDaoImpl.Read(uniqueIdentifier, personId, connection));
            }

            foreach (UniqueIdentifier foundUniqueIdentifier in foundUniqueIdentifiers)
            {
                if (uniqueIdentifieres.Find(x => (x.Id == foundUniqueIdentifier.Id)) == null)
                    uniqueIdentifierDaoImpl.Delete(foundUniqueIdentifier, personId, connection);
            }
            return updatedUniqueIdentifieres;
        }

        private List<PhoneNumber> UpdatePhoneNumbers(List<PhoneNumber> phoneNumbers, int personId, IConnectable connection)
        {
            IPhoneNumberDao phoneNumberDaoImpl = DaoImplFactory.GetPhoneNumberDaoImpl();
            List<PhoneNumber> updatedPhoneNumberes = new List<PhoneNumber>();

            List<PhoneNumber> foundPhoneNumbers =
                phoneNumberDaoImpl.Read(null, personId, connection);

            foreach (PhoneNumber phoneNumber in phoneNumbers)
            {
                phoneNumberDaoImpl.Update(phoneNumber, personId, connection);
                updatedPhoneNumberes.AddRange(phoneNumberDaoImpl.Read(phoneNumber, personId, connection));
            }

            foreach (PhoneNumber foundPhoneNumber in foundPhoneNumbers)
            {
                if (phoneNumbers.Find(x => (x.Id == foundPhoneNumber.Id)) == null)
                    phoneNumberDaoImpl.Delete(foundPhoneNumber, personId, connection);
            }

            return updatedPhoneNumberes;
        }

        private List<Address> UpdateAddresses(List<Address> addresses, int personId, IConnectable connection)
        {
            List<Address> updatedAddresses = new List<Address>();
            IAddressDao addressDaoImpl = DaoImplFactory.GetAddressDaoImpl();
            foreach (Address address in addresses)
            {
                updatedAddresses.AddRange(addressDaoImpl.Read(address, connection));
            }

            UpdateJctPersonAddress(updatedAddresses, personId, connection);

            return updatedAddresses;
        }

        private void UpdateJctPersonAddress(List<Address> addresses, int personId, IConnectable connection)
        {
            IJctPersonAddressDao jctPersonAddressDaoImpl = DaoImplFactory.GetJctPersonAddressDaoImpl();
            List<DataRow> foundJctPersonAddressDaos =
                jctPersonAddressDaoImpl.Read_WithPersonId(personId, connection);
            List<int> addressIds = new List<int>();
            List<int> foundAddressIds = new List<int>();

            foreach (Address address in addresses)
            {
                addressIds.Add(address.Id);
            }

            foreach (DataRow jctPersonAddressDao in foundJctPersonAddressDaos)
            {
                foundAddressIds.Add((int)jctPersonAddressDao["address_id"]);
            }

            foreach (int addressId in addressIds)
            {
                if (foundAddressIds.Contains(addressId) == false)
                    jctPersonAddressDaoImpl.Write(personId, addressId, connection);
            }

            foreach (int foundAddressId in foundAddressIds)
            {
                if (addressIds.Contains(foundAddressId) == false)
                    jctPersonAddressDaoImpl.Delete(personId, foundAddressId, connection);
            }
        }

        private List<Application> UpdateApplications(List<Application> apps, int personId, IConnectable connection)
        {
            List<Application> updatedApps = new List<Application>();
            IApplicationDao appDaoImpl = DaoImplFactory.GetApplicationDaoImpl();
            foreach (Application app in apps)
            {
                appDaoImpl.Update(app, connection);
                updatedApps.AddRange(appDaoImpl.Read(app, connection));
            }

            UpdateJctPersonApp(updatedApps, personId, connection);

            return updatedApps;
        }

        private void UpdateJctPersonApp(List<Application> apps, int personId, IConnectable connection)
        {
            IJctPersonAppDao jctPersonAppDaoImpl = DaoImplFactory.GetJctPersonAppDaoImpl();
            List<DataRow> foundJctPersonAppDaos =
                jctPersonAppDaoImpl.Read_WithPersonId(personId, connection);
            List<int> appIds = new List<int>();
            List<int> foundAppIds = new List<int>();

            foreach (Application app in apps)
            {
                appIds.Add(app.Id);
            }

            foreach (DataRow jctPersonAppDao in foundJctPersonAppDaos)
            {
                foundAppIds.Add((int)jctPersonAppDao["app_id"]);
            }

            foreach (int appId in appIds)
            {
                if (foundAppIds.Contains(appId) == false)
                    jctPersonAppDaoImpl.Write(personId, appId, connection);
            }

            foreach (int foundAppId in foundAppIds)
            {
                if (appIds.Contains(foundAppId) == false)
                    jctPersonAppDaoImpl.Delete(personId, foundAppId, connection);
            }
        }

        private List<Enumeration> UpdateJctPersonRole(List<Enumeration> roles, int personId, IConnectable connection)
        {
            List<Enumeration> updatedRoles = new List<Enumeration>();
            IJctPersonRoleDao jctPersonRoleDaoImpl = DaoImplFactory.GetJctPersonRoleDaoImpl();
            List<DataRow> foundJctPersonRoleDaos =
                jctPersonRoleDaoImpl.Read(personId, connection);
            List<int> roleIds = new List<int>();
            List<int> foundRoleIds = new List<int>();

            foreach (Enumeration role in roles)
            {
                roleIds.Add(role.Id);
            }

            foreach (DataRow jctPersonRoleDao in foundJctPersonRoleDaos)
            {
                foundRoleIds.Add((int)jctPersonRoleDao["enum_role_id"]);
            }

            foreach (int roleId in roleIds)
            {
                if (foundRoleIds.Contains(roleId) == false)
                    jctPersonRoleDaoImpl.Write(personId, roleId, connection);

                DataRow jctPersonRoleDao = jctPersonRoleDaoImpl.Read(personId, roleId, connection);
                if(jctPersonRoleDao != null)
                {
                    int enumRoleId = (int)jctPersonRoleDao["enum_role_id"];

                    updatedRoles.Add(Enum_Role.GetEnumFromId(enumRoleId));
                }
            }

            foreach (int foundRoleId in foundRoleIds)
            {
                if (roleIds.Contains(foundRoleId) == false)
                    jctPersonRoleDaoImpl.Delete(personId, foundRoleId, connection);
            }

            return updatedRoles;
        }

        private List<int> UpdateJctPersonPerson(List<int> relations, int personId, IConnectable connection)
        {
            List<int> updatedRelations = new List<int>();
            IJctPersonPersonDao jctPersonPersonDaoImpl = DaoImplFactory.GetJctPersonPersonDaoImpl();
            List<DataRow> foundJctPersonPersonDaos =
                jctPersonPersonDaoImpl.Read(personId, connection);
            List<int> foundRelations = new List<int>();

            foreach (DataRow jctPersonPersonDao in foundJctPersonPersonDaos)
            {
                foundRelations.Add((int)jctPersonPersonDao["person_two_id"]);
            }

            foreach (int relation in relations)
            {
                if (foundRelations.Contains(relation) == false)
                    jctPersonPersonDaoImpl.Write(personId, relation, connection);

                DataRow jctPersonPersonDao = jctPersonPersonDaoImpl.Read(personId, relation, connection);
                if(jctPersonPersonDao != null)
                    updatedRelations.Add((int)jctPersonPersonDao["person_two_id"]);
            }

            foreach (int foundRelation in foundRelations)
            {
                if (relations.Contains(foundRelation) == false)
                    jctPersonPersonDaoImpl.Delete(personId, foundRelation, connection);
            }
            return relations;
        }
    }
}
