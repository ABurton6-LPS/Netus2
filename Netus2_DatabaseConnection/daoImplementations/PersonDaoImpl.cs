using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
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

            PersonDao personDao = daoObjectMapper.MapPerson(person);

            StringBuilder sql = new StringBuilder("DELETE FROM person WHERE 1=1 ");
            sql.Append("AND person_id " + (personDao.person_id != null ? "= " + personDao.person_id + " " : "IS NULL "));
            sql.Append("AND first_name " + (personDao.first_name != null ? "LIKE '" + personDao.first_name + "' " : "IS NULL "));
            sql.Append("AND middle_name " + (personDao.middle_name != null ? "LIKE '" + personDao.middle_name + "' " : "IS NULL "));
            sql.Append("AND last_name " + (personDao.last_name != null ? "LIKE '" + personDao.last_name + "' " : "IS NULL "));
            sql.Append("AND birth_date " + (personDao.birth_date != null ? "= '" + personDao.birth_date + "' " : "IS NULL "));
            sql.Append("AND enum_gender_id " + (personDao.enum_gender_id != null ? "= " + personDao.enum_gender_id + " " : "IS NULL "));
            sql.Append("AND enum_ethnic_id " + (personDao.enum_ethnic_id != null ? "= " + personDao.enum_ethnic_id + " " : "IS NULL "));
            sql.Append("AND enum_residence_status_id " + (personDao.enum_residence_status_id != null ? "= " + personDao.enum_residence_status_id + " " : "IS NULL "));
            sql.Append("AND login_name " + (personDao.login_name != null ? "LIKE '" + personDao.login_name + "' " : "IS NULL "));
            sql.Append("AND login_pw " + (personDao.login_pw != null ? "LIKE '" + personDao.login_pw + "' " : "IS NULL"));

            connection.ExecuteNonQuery(sql.ToString());
        }

        private void Delete_JctClassPerson(Person person, IConnectable connection)
        {
            IJctClassPersonDao jctClassPersonDaoImpl = new JctClassPersonDaoImpl();
            List<JctClassPersonDao> foundJctClassPersonDaos =
                jctClassPersonDaoImpl.Read_WithPersonId(person.Id, connection);

            foreach (JctClassPersonDao foundClassPersonDao in foundJctClassPersonDaos)
            {
                jctClassPersonDaoImpl.Delete((int)foundClassPersonDao.class_id, person.Id, connection);
            }
        }

        private void Delete_UniqueIdentifier(Person person, IConnectable connection)
        {
            IUniqueIdentifierDao uniqueIdentifierDaoImpl = new UniqueIdentifierDaoImpl();
            List<UniqueIdentifier> foundUniqueIdentifiers = uniqueIdentifierDaoImpl.Read(null, person.Id, connection);
            foreach (UniqueIdentifier uniqueIdentifier in foundUniqueIdentifiers)
            {
                uniqueIdentifierDaoImpl.Delete(uniqueIdentifier, person.Id, connection);
            }
        }

        private void Delete_EmploymentSession(Person person, IConnectable connection)
        {
            IEmploymentSessionDao employmentSessionDaoImpl = new EmploymentSessionDaoImpl();
            List<EmploymentSession> foundEmploymentSessions = employmentSessionDaoImpl.Read_WithPersonId(null, person.Id, connection);
            foreach (EmploymentSession fondEmploymentSession in foundEmploymentSessions)
            {
                employmentSessionDaoImpl.Delete_WithPersonId(fondEmploymentSession, person.Id, connection);
            }
        }

        private void Delete_Enrollment(Person person, IConnectable connection)
        {
            IEnrollmentDao enrollmentDaoImpl = new EnrollmentDaoImpl();
            List<Enrollment> foundEnrollments = enrollmentDaoImpl.Read(null, person.Id, connection);
            foreach (Enrollment enrollment in foundEnrollments)
            {
                enrollmentDaoImpl.Delete(enrollment, person.Id, connection);
            }
        }

        private void Delete_Mark(Person person, IConnectable connection)
        {
            IMarkDao markDaoImpl = new MarkDaoImpl();
            List<Mark> foundMarks = markDaoImpl.Read(null, person.Id, connection);
            foreach (Mark mark in foundMarks)
            {
                markDaoImpl.Delete(mark, person.Id, connection);
            }
        }

        private void Delete_JctPersonAddress(Person person, IConnectable connection)
        {
            IJctPersonAddressDao jctPersonAddressDaoImpl = new JctPersonAddressDaoImpl();
            foreach (Address address in person.Addresses)
            {
                jctPersonAddressDaoImpl.Delete(person.Id, address.Id, connection);
            }
        }

        private void Delete_PhoneNumber(Person person, IConnectable connection)
        {
            IPhoneNumberDao phoneNumberDaoImpl = new PhoneNumberDaoImpl();
            foreach (PhoneNumber phoneNumber in person.PhoneNumbers)
            {
                phoneNumberDaoImpl.Delete(phoneNumber, person.Id, connection);
            }
        }

        private void Delete_JctPersonApp(Person person, IConnectable connection)
        {
            IJctPersonAppDao jctPersonAppDaoImpl = new JctPersonAppDaoImpl();
            foreach (Application app in person.Applications)
            {
                jctPersonAppDaoImpl.Delete(person.Id, app.Id, connection);
            }
        }

        private void Delete_JctPersonRole(Person person, IConnectable connection)
        {
            IJctPersonRoleDao jctPersonRoleDaoImpl = new JctPersonRoleDaoImpl();
            foreach (Enumeration role in person.Roles)
            {
                jctPersonRoleDaoImpl.Delete(person.Id, role.Id, connection);
            }
        }

        private void Delete_JctPersonPerson(Person person, IConnectable connection)
        {
            IJctPersonPersonDao jctPersonPersonDaoImpl = new JctPersonPersonDaoImpl();
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
            PersonDao personDao = daoObjectMapper.MapPerson(person);

            StringBuilder sql = new StringBuilder("SELECT * FROM person WHERE 1=1 ");
            if (personDao.person_id != null)
                sql.Append("AND person_id = " + personDao.person_id + " ");
            else
            {
                if (personDao.first_name != null)
                    sql.Append("AND first_name LIKE '" + personDao.first_name + "' ");
                if (personDao.middle_name != null)
                    sql.Append("AND middle_name LIKE '" + personDao.middle_name + "' ");
                if (personDao.last_name != null)
                    sql.Append("AND last_name LIKE '" + personDao.last_name + "' ");
                if (personDao.birth_date != null)
                    sql.Append("AND datediff(day, birth_date, '" + personDao.birth_date.ToString() + "') = 0 ");
                if (personDao.enum_gender_id != null)
                    sql.Append("AND enum_gender_id = " + personDao.enum_gender_id + " ");
                if (personDao.enum_ethnic_id != null)
                    sql.Append("AND enum_ethnic_id = " + personDao.enum_ethnic_id + " ");
                if (personDao.enum_residence_status_id != null)
                    sql.Append("AND enum_residence_status_id = " + personDao.enum_residence_status_id + " ");
                if (personDao.login_name != null)
                    sql.Append("AND login_name LIKE '" + personDao.login_name + "' ");
                if (personDao.login_pw != null)
                    sql.Append("AND login_pw LIKE '" + personDao.login_pw + "' ");
            }


            return Read(sql.ToString(), connection);
        }

        private List<Person> Read(string sql, IConnectable connection)
        {
            List<PersonDao> foundPeopleDao = new List<PersonDao>();

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    PersonDao foundPersonDao = new PersonDao();

                    List<string> columnNames = new List<String>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        columnNames.Add(reader.GetName(i));

                    foreach (string columnName in columnNames)
                    {
                        var value = reader.GetValue(reader.GetOrdinal(columnName));
                        switch (columnName)
                        {
                            case "person_id":
                                if (value != DBNull.Value)
                                    foundPersonDao.person_id = (int)value;
                                else
                                    foundPersonDao.person_id = null;
                                break;
                            case "first_name":
                                foundPersonDao.first_name = value != DBNull.Value ? (string)value : null;
                                break;
                            case "middle_name":
                                foundPersonDao.middle_name = value != DBNull.Value ? (string)value : null;
                                break;
                            case "last_name":
                                foundPersonDao.last_name = value != DBNull.Value ? (string)value : null;
                                break;
                            case "birth_date":
                                if (value != DBNull.Value)
                                    foundPersonDao.birth_date = (DateTime)value;
                                else
                                    foundPersonDao.birth_date = null;
                                break;
                            case "enum_gender_id":
                                if (value != DBNull.Value)
                                    foundPersonDao.enum_gender_id = (int)value;
                                else
                                    foundPersonDao.enum_gender_id = null;
                                break;
                            case "enum_ethnic_id":
                                if (value != DBNull.Value)
                                    foundPersonDao.enum_ethnic_id = (int)value;
                                else
                                    foundPersonDao.enum_ethnic_id = null;
                                break;
                            case "enum_residence_status_id":
                                if (value != DBNull.Value)
                                    foundPersonDao.enum_residence_status_id = (int)value;
                                else
                                    foundPersonDao.enum_residence_status_id = null;
                                break;
                            case "login_name":
                                foundPersonDao.login_name = value != DBNull.Value ? (string)value : null;
                                break;
                            case "login_pw":
                                foundPersonDao.login_pw = value != DBNull.Value ? (string)value : null;
                                break;
                            case "created":
                                if (value != DBNull.Value)
                                    foundPersonDao.created = (DateTime)value;
                                else
                                    foundPersonDao.created = null;
                                break;
                            case "created_by":
                                foundPersonDao.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case "changed":
                                if (value != DBNull.Value)
                                    foundPersonDao.changed = (DateTime)value;
                                else
                                    foundPersonDao.changed = null;
                                break;
                            case "changed_by":
                                foundPersonDao.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in person table: " + columnName);
                        }
                    }
                    foundPeopleDao.Add(foundPersonDao);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<Person> results = new List<Person>();
            foreach (PersonDao foundPersonDao in foundPeopleDao)
            {
                results.Add(daoObjectMapper.MapPerson(foundPersonDao));
            }

            foreach (Person result in results)
            {
                result.Applications.AddRange(Read_JctPersonApp(result.Id, connection));
                result.Roles.AddRange(Read_JctPersonRole(result.Id, connection));
                result.Relations.AddRange(Read_JctPersonPerson(result.Id, connection));
                result.Addresses.AddRange(Read_JctPersonAddress(result.Id, connection));
                result.PhoneNumbers.AddRange(new PhoneNumberDaoImpl().Read(null, result.Id, connection));
                result.UniqueIdentifiers.AddRange(new UniqueIdentifierDaoImpl().Read(null, result.Id, connection));
                result.EmploymentSessions.AddRange(new EmploymentSessionDaoImpl().Read_WithPersonId(null, result.Id, connection));
                result.Enrollments.AddRange(new EnrollmentDaoImpl().Read(null, result.Id, connection));
                result.Marks.AddRange(new MarkDaoImpl().Read(null, result.Id, connection));
            }

            return results;
        }

        private List<Application> Read_JctPersonApp(int personId, IConnectable connection)
        {
            IApplicationDao appDaoImpl = new ApplicationDaoImpl();
            IJctPersonAppDao jctPersonAppDaoImpl = new JctPersonAppDaoImpl();

            List<Application> foundApplications = new List<Application>();
            List<JctPersonAppDao> foundJctPersonAppDaos = jctPersonAppDaoImpl.Read_WithPersonId(personId, connection);

            foreach (JctPersonAppDao foundJctPersonAppDao in foundJctPersonAppDaos)
            {
                int appId = (int)foundJctPersonAppDao.app_id;
                foundApplications.Add(appDaoImpl.Read_UsingAppId(appId, connection));
            }

            return foundApplications;
        }

        private List<Address> Read_JctPersonAddress(int personId, IConnectable connection)
        {
            IAddressDao addressDaoImpl = new AddressDaoImpl();
            IJctPersonAddressDao jctPersonAddressDaoImpl = new JctPersonAddressDaoImpl();

            List<Address> foundAddresss = new List<Address>();
            List<JctPersonAddressDao> foundJctPersonAddressDaos = jctPersonAddressDaoImpl.Read_WithPersonId(personId, connection);

            foreach (JctPersonAddressDao foundJctPersonAddressDao in foundJctPersonAddressDaos)
            {
                int addressId = (int)foundJctPersonAddressDao.address_id;
                foundAddresss.Add(addressDaoImpl.Read_UsingAdddressId(addressId, connection));
            }

            return foundAddresss;
        }

        private List<Enumeration> Read_JctPersonRole(int personId, IConnectable connection)
        {
            List<Enumeration> foundRoles = new List<Enumeration>();
            List<int> idsFound = new List<int>();

            IJctPersonRoleDao jctPersonRoleDaoImpl = new JctPersonRoleDaoImpl();
            List<JctPersonRoleDao> foundJctPersonRoleDaos = jctPersonRoleDaoImpl.Read(personId, connection);
            foreach (JctPersonRoleDao foundJctPersonRoleDao in foundJctPersonRoleDaos)
            {
                idsFound.Add((int)foundJctPersonRoleDao.enum_role_id);
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

            IJctPersonPersonDao jctPersonPersonDaoImpl = new JctPersonPersonDaoImpl();
            List<JctPersonPersonDao> foundJctPersonPersonDaos = jctPersonPersonDaoImpl.Read(personId, connection);

            foreach (JctPersonPersonDao foundJctPersonPersonDao in foundJctPersonPersonDaos)
            {
                foundRelations.Add((int)foundJctPersonPersonDao.person_two_id);
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
            PersonDao personDao = daoObjectMapper.MapPerson(person);

            if (personDao.person_id != null)
            {
                StringBuilder sql = new StringBuilder("UPDATE person SET ");
                sql.Append("first_name = " + (personDao.first_name != null ? "'" + personDao.first_name + "', " : "NULL, "));
                sql.Append("middle_name = " + (personDao.middle_name != null ? "'" + personDao.middle_name + "', " : "NULL, "));
                sql.Append("last_name = " + (personDao.last_name != null ? "'" + personDao.last_name + "', " : "NULL, "));
                sql.Append("birth_date = " + (personDao.birth_date != null ? "'" + personDao.birth_date + "', " : "NULL, "));
                sql.Append("enum_gender_id = " + (personDao.enum_gender_id != null ? "'" + personDao.enum_gender_id + "', " : "NULL, "));
                sql.Append("enum_ethnic_id = " + (personDao.enum_ethnic_id != null ? "'" + personDao.enum_ethnic_id + "', " : "NULL, "));
                sql.Append("enum_residence_status_id = " + (personDao.enum_residence_status_id != null ? "'" + personDao.enum_residence_status_id + "', " : "NULL, "));
                sql.Append("login_name = " + (personDao.login_name != null ? "'" + personDao.login_name + "', " : "NULL, "));
                sql.Append("login_pw = " + (personDao.login_pw != null ? "'" + personDao.login_pw + "', " : "NULL, "));
                sql.Append("changed = GETDATE(), ");
                sql.Append("changed_by = 'Netus2' ");
                sql.Append("WHERE person_id = " + personDao.person_id);

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
            PersonDao personDao = daoObjectMapper.MapPerson(person);

            StringBuilder sqlValues = new StringBuilder();
            sqlValues.Append(personDao.first_name != null ? "'" + personDao.first_name + "', " : "NULL, ");
            sqlValues.Append(personDao.middle_name != null ? "'" + personDao.middle_name + "', " : "NULL, ");
            sqlValues.Append(personDao.last_name != null ? "'" + personDao.last_name + "', " : "NULL, ");
            sqlValues.Append(personDao.birth_date != null ? "'" + personDao.birth_date + "', " : "NULL, ");
            sqlValues.Append(personDao.enum_gender_id != null ? personDao.enum_gender_id + ", " : "NULL, ");
            sqlValues.Append(personDao.enum_ethnic_id != null ? personDao.enum_ethnic_id + ", " : "NULL, ");
            sqlValues.Append(personDao.enum_residence_status_id != null ? personDao.enum_residence_status_id + ", " : "NULL, ");
            sqlValues.Append(personDao.login_name != null ? "'" + personDao.login_name + "', " : "NULL, ");
            sqlValues.Append(personDao.login_pw != null ? "'" + personDao.login_pw + "', " : "NULL, ");
            sqlValues.Append("GETDATE(), ");
            sqlValues.Append("'Netus2'");

            string sql =
                "INSERT INTO person " +
                "(first_name, middle_name, last_name, birth_date, enum_gender_id, enum_ethnic_id, " +
                "enum_residence_status_id, login_name, login_pw, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            personDao.person_id = connection.InsertNewRecord(sql);

            Person result = daoObjectMapper.MapPerson(personDao);

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
            IMarkDao markDaoImpl = new MarkDaoImpl();

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
            IEnrollmentDao enrollmentDaoImpl = new EnrollmentDaoImpl();

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
            IEmploymentSessionDao employmentSessionDaoImpl = new EmploymentSessionDaoImpl();

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
            IUniqueIdentifierDao uniqueIdentifierDaoImpl = new UniqueIdentifierDaoImpl();

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
            IPhoneNumberDao phoneNumberDaoImpl = new PhoneNumberDaoImpl();
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
            IAddressDao addressDaoImpl = new AddressDaoImpl();
            foreach (Address address in addresses)
            {
                updatedAddresses.AddRange(addressDaoImpl.Read(address, connection));
            }

            UpdateJctPersonAddress(updatedAddresses, personId, connection);

            return updatedAddresses;
        }

        private void UpdateJctPersonAddress(List<Address> addresses, int personId, IConnectable connection)
        {
            IJctPersonAddressDao jctPersonAddressDaoImpl = new JctPersonAddressDaoImpl();
            List<JctPersonAddressDao> foundJctPersonAddressDaos =
                jctPersonAddressDaoImpl.Read_WithPersonId(personId, connection);
            List<int> addressIds = new List<int>();
            List<int> foundAddressIds = new List<int>();

            foreach (Address address in addresses)
            {
                addressIds.Add(address.Id);
            }

            foreach (JctPersonAddressDao jctPersonAddressDao in foundJctPersonAddressDaos)
            {
                foundAddressIds.Add((int)jctPersonAddressDao.address_id);
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
            IApplicationDao appDaoImpl = new ApplicationDaoImpl();
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
            IJctPersonAppDao jctPersonAppDaoImpl = new JctPersonAppDaoImpl();
            List<JctPersonAppDao> foundJctPersonAppDaos =
                jctPersonAppDaoImpl.Read_WithPersonId(personId, connection);
            List<int> appIds = new List<int>();
            List<int> foundAppIds = new List<int>();

            foreach (Application app in apps)
            {
                appIds.Add(app.Id);
            }

            foreach (JctPersonAppDao jctPersonAppDao in foundJctPersonAppDaos)
            {
                foundAppIds.Add((int)jctPersonAppDao.app_id);
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
            IJctPersonRoleDao jctPersonRoleDaoImpl = new JctPersonRoleDaoImpl();
            List<JctPersonRoleDao> foundJctPersonRoleDaos =
                jctPersonRoleDaoImpl.Read(personId, connection);
            List<int> roleIds = new List<int>();
            List<int> foundRoleIds = new List<int>();

            foreach (Enumeration role in roles)
            {
                roleIds.Add(role.Id);
            }

            foreach (JctPersonRoleDao jctPersonRoleDao in foundJctPersonRoleDaos)
            {
                foundRoleIds.Add((int)jctPersonRoleDao.enum_role_id);
            }

            foreach (int roleId in roleIds)
            {
                if (foundRoleIds.Contains(roleId) == false)
                    jctPersonRoleDaoImpl.Write(personId, roleId, connection);

                int enumRoleId = (int)jctPersonRoleDaoImpl.Read(personId, roleId, connection).enum_role_id;

                updatedRoles.Add(Enum_Role.GetEnumFromId(enumRoleId));
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
            IJctPersonPersonDao jctPersonPersonDaoImpl = new JctPersonPersonDaoImpl();
            List<JctPersonPersonDao> foundJctPersonPersonDaos =
                jctPersonPersonDaoImpl.Read(personId, connection);
            List<int> foundRelations = new List<int>();

            foreach (JctPersonPersonDao jctPersonPersonDao in foundJctPersonPersonDaos)
            {
                foundRelations.Add((int)jctPersonPersonDao.person_two_id);
            }

            foreach (int relation in relations)
            {
                if (foundRelations.Contains(relation) == false)
                    jctPersonPersonDaoImpl.Write(personId, relation, connection);
                updatedRelations.Add((int)jctPersonPersonDaoImpl.Read(personId, relation, connection).person_two_id);
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
