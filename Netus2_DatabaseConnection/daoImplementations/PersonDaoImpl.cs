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
    public class PersonDaoImpl : IPersonDao
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

        public void Delete(Person person, IConnectable connection)
        {
            if(person.Id <= 0)
                throw new Exception("Cannot delete a person who doesn't have a database-assigned ID.\n" + person.ToString());

            Delete_JctPersonPerson(person, connection);
            Delete_JctPersonRole(person, connection);
            Delete_JctPersonApp(person, connection);
            Delete_JctPersonPhoneNumber(person, connection);
            Delete_JctPersonAddress(person, connection);
            Delete_JctPersonEmail(person, connection);
            Delete_EmploymentSession(person, connection);
            Delete_UniqueIdentifier(person, connection);
            Delete_Enrollment(person, connection);
            Delete_Mark(person, connection);
            Delete_JctClassPerson(person, connection);
            
            string sql = "DELETE FROM person WHERE " +
                "person_id = @person_id";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", person.Id));

            connection.ExecuteNonQuery(sql, parameters);
        }

        private void Delete_JctClassPerson(Person person, IConnectable connection)
        {
            IJctClassPersonDao jctClassPersonDaoImpl = DaoImplFactory.GetJctClassPersonDaoImpl();
            List<DataRow> foundJctClassPersonDaos =
                jctClassPersonDaoImpl.Read_AllWithPersonId(person.Id, connection);

            foreach (DataRow foundClassPersonDao in foundJctClassPersonDaos)
            {
                jctClassPersonDaoImpl.Delete(
                    (int)foundClassPersonDao["class_id"],
                    (int)foundClassPersonDao["person_id"], 
                    connection);
            }
        }

        private void Delete_UniqueIdentifier(Person person, IConnectable connection)
        {
            IUniqueIdentifierDao uniqueIdentifierDaoImpl = DaoImplFactory.GetUniqueIdentifierDaoImpl();
            List<UniqueIdentifier> foundUniqueIdentifiers = uniqueIdentifierDaoImpl.Read_AllWithPersonId(person.Id, connection);
            foreach (UniqueIdentifier uniqueIdentifier in foundUniqueIdentifiers)
            {
                uniqueIdentifierDaoImpl.Delete(uniqueIdentifier, connection);
            }
        }

        private void Delete_EmploymentSession(Person person, IConnectable connection)
        {
            IEmploymentSessionDao employmentSessionDaoImpl = DaoImplFactory.GetEmploymentSessionDaoImpl();
            List<EmploymentSession> foundEmploymentSessions = employmentSessionDaoImpl.Read_AllWithPersonId(person.Id, connection);
            foreach (EmploymentSession fondEmploymentSession in foundEmploymentSessions)
            {
                employmentSessionDaoImpl.Delete(fondEmploymentSession, connection);
            }
        }

        private void Delete_Enrollment(Person person, IConnectable connection)
        {
            IEnrollmentDao enrollmentDaoImpl = DaoImplFactory.GetEnrollmentDaoImpl();
            List<Enrollment> foundEnrollments = enrollmentDaoImpl.Read_AllWithPersonId(person.Id, connection);
            foreach (Enrollment enrollment in foundEnrollments)
            {
                enrollmentDaoImpl.Delete(enrollment, connection);
            }
        }

        private void Delete_Mark(Person person, IConnectable connection)
        {
            IMarkDao markDaoImpl = DaoImplFactory.GetMarkDaoImpl();
            List<Mark> foundMarks = markDaoImpl.Read_AllWithPersonId(person.Id, connection);
            foreach (Mark mark in foundMarks)
            {
                markDaoImpl.Delete(mark, connection);
            }
        }

        private void Delete_JctPersonAddress(Person person, IConnectable connection)
        {
            IJctPersonAddressDao jctPersonAddressDaoImpl = DaoImplFactory.GetJctPersonAddressDaoImpl();
            List<DataRow> foundJctPersonAddresses = jctPersonAddressDaoImpl.Read_AllWithPersonId(person.Id, connection);

            foreach (DataRow foundJctPersonAddress in foundJctPersonAddresses)
            {
                jctPersonAddressDaoImpl.Delete(
                    (int)foundJctPersonAddress["person_id"], 
                    (int)foundJctPersonAddress["address_id"], connection);
            }
        }

        private void Delete_JctPersonEmail(Person person, IConnectable connection)
        {
            IJctPersonEmailDao jctPersonEmailDaoImpl = DaoImplFactory.GetJctPersonEmailDaoImpl();
            List<DataRow> foundJctPersonEmails = jctPersonEmailDaoImpl.Read_AllWithPersonId(person.Id, connection);

            foreach (DataRow foundJctPersonEmail in foundJctPersonEmails)
            {
                jctPersonEmailDaoImpl.Delete(
                    (int)foundJctPersonEmail["person_id"],
                    (int)foundJctPersonEmail["email_id"], connection);
            }
        }

        private void Delete_JctPersonPhoneNumber(Person person, IConnectable connection)
        {
            IJctPersonPhoneNumberDao jctPersonPhoneNumberDaoImpl = DaoImplFactory.GetJctPersonPhoneNumberDaoImpl();
            List<DataRow> foundJctPersonPhoneNumbers = jctPersonPhoneNumberDaoImpl.Read_AllWithPersonId(person.Id, connection);

            foreach (DataRow foundJctPersonPhoneNumber in foundJctPersonPhoneNumbers)
            {
                jctPersonPhoneNumberDaoImpl.Delete(
                    (int)foundJctPersonPhoneNumber["person_id"], 
                    (int)foundJctPersonPhoneNumber["phone_number_id"], connection);
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
            string sql = "SELECT * FROM person WHERE person_id = @person_id";
            
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@person_id", personId));

            List<Person> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching personId: " + personId);
        }

        public Person Read_UsingUniqueIdentifier(string identifier, IConnectable connection)
        {
            string sql = "SELECT * FROM person WHERE person_id in (" +
                "SELECT person_id FROM unique_identifier WHERE unique_identifier = @unique_identifier)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@unique_identifier", identifier));

            List<Person> results = Read(sql, connection, parameters);

            if (results.Count == 0)
                return null;
            else if (results.Count == 1)
                return results[0];
            else
                throw new Exception(results.Count + " found matching identifier: " + identifier);
        }

        public List<Person> Read(Person person, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPerson(person);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder("SELECT * FROM person WHERE 1=1 ");
            if (row["person_id"] != DBNull.Value)
            {
                sql.Append("AND person_id = @person_id ");
                parameters.Add(new SqlParameter("@person_id", row["person_id"]));
            }
            else
            {
                if (row["first_name"] != DBNull.Value)
                {
                    sql.Append("AND first_name = @first_name ");
                    parameters.Add(new SqlParameter("@first_name", row["first_name"]));
                }

                if (row["middle_name"] != DBNull.Value)
                {
                    sql.Append("AND middle_name = @middle_name ");
                    parameters.Add(new SqlParameter("@middle_name", row["middle_name"]));
                }

                if (row["last_name"] != DBNull.Value)
                {
                    sql.Append("AND last_name = @last_name ");
                    parameters.Add(new SqlParameter("@last_name", row["last_name"]));
                }

                if (row["birth_date"] != DBNull.Value)
                {
                    sql.Append("AND datediff(day, birth_date, @birth_date) = 0 ");
                    parameters.Add(new SqlParameter("@birth_date", row["birth_date"]));
                }

                if (row["enum_gender_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_gender_id = @enum_gender_id ");
                    parameters.Add(new SqlParameter("@enum_gender_id", row["enum_gender_id"]));
                }

                if (row["enum_ethnic_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_ethnic_id = @enum_ethnic_id ");
                    parameters.Add(new SqlParameter("@enum_ethnic_id", row["enum_ethnic_id"]));
                }

                if (row["enum_residence_status_id"] != DBNull.Value)
                {
                    sql.Append("AND enum_residence_status_id = @enum_residence_status_id ");
                    parameters.Add(new SqlParameter("@enum_residence_status_id", row["enum_residence_status_id"]));
                }

                if (row["login_name"] != DBNull.Value)
                {
                    sql.Append("AND login_name = @login_name ");
                    parameters.Add(new SqlParameter("@login_name", row["login_name"]));
                }

                if (row["login_pw"] != DBNull.Value)
                {
                    sql.Append("AND login_pw = @login_pw");
                    parameters.Add(new SqlParameter("@login_pw", row["login_pw"]));
                }
            }

            return Read(sql.ToString(), connection, parameters);
        }

        private List<Person> Read(string sql, IConnectable connection, List<SqlParameter> parameters)
        {
            DataTable dtPerson = DataTableFactory.CreateDataTable_Netus2_Person();
            dtPerson = connection.ReadIntoDataTable(sql, dtPerson, parameters);

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
                result.Addresses = Read_JctPersonAddress(result.Id, connection);
                result.Emails = Read_JctPersonEmail(result.Id, connection);
                result.PhoneNumbers.AddRange(Read_JctPersonPhoneNumber(result.Id, connection));
                result.UniqueIdentifiers.AddRange(DaoImplFactory.GetUniqueIdentifierDaoImpl().Read_AllWithPersonId(result.Id, connection));
                result.EmploymentSessions.AddRange(DaoImplFactory.GetEmploymentSessionDaoImpl().Read_AllWithPersonId(result.Id, connection));
                result.Enrollments.AddRange(DaoImplFactory.GetEnrollmentDaoImpl().Read_AllWithPersonId(result.Id, connection));
                result.Marks.AddRange(DaoImplFactory.GetMarkDaoImpl().Read_AllWithPersonId(result.Id, connection));
            }

            return results;
        }

        private List<Application> Read_JctPersonApp(int personId, IConnectable connection)
        {
            IApplicationDao appDaoImpl = DaoImplFactory.GetApplicationDaoImpl();
            IJctPersonAppDao jctPersonAppDaoImpl = DaoImplFactory.GetJctPersonAppDaoImpl();

            List<Application> foundApplications = new List<Application>();
            List<DataRow> foundJctPersonAppDaos = jctPersonAppDaoImpl.Read_AllWithPersonId(personId, connection);

            foreach (DataRow foundJctPersonAppDao in foundJctPersonAppDaos)
            {
                int appId = (int)foundJctPersonAppDao["app_id"];
                foundApplications.Add(appDaoImpl.Read_UsingAppId(appId, connection));
            }

            return foundApplications;
        }

        private List<PhoneNumber> Read_JctPersonPhoneNumber(int personId, IConnectable connection)
        {
            IPhoneNumberDao phoneNumberDaoImpl = DaoImplFactory.GetPhoneNumberDaoImpl();
            IJctPersonPhoneNumberDao jctPersonPhoneNumberDaoImpl = DaoImplFactory.GetJctPersonPhoneNumberDaoImpl();

            List<PhoneNumber> foundPhoneNumbers = new List<PhoneNumber>();
            List<DataRow> foundJctPersonPhoneNumberDaos = jctPersonPhoneNumberDaoImpl.Read_AllWithPersonId(personId, connection);

            foreach (DataRow foundJctPersonPhoneNumberDao in foundJctPersonPhoneNumberDaos)
            {
                int phoneNumberId = (int)foundJctPersonPhoneNumberDao["phone_number_id"];
                PhoneNumber foundPhoneNumber = phoneNumberDaoImpl.Read_WithPhoneNumberId(phoneNumberId, connection);

                if(foundPhoneNumber != null)
                {
                    foundPhoneNumber.IsPrimary = Enum_True_False.GetEnumFromId((int)foundJctPersonPhoneNumberDao["is_primary_id"]);
                    foundPhoneNumbers.Add(foundPhoneNumber);
                }
            }

            return foundPhoneNumbers;
        }

        private List<Address> Read_JctPersonAddress(int personId, IConnectable connection)
        {
            IAddressDao addressDaoImpl = DaoImplFactory.GetAddressDaoImpl();
            IJctPersonAddressDao jctPersonAddressDaoImpl = DaoImplFactory.GetJctPersonAddressDaoImpl();

            List<Address> foundAddresss = new List<Address>();
            List<DataRow> foundJctPersonAddressDaos = jctPersonAddressDaoImpl.Read_AllWithPersonId(personId, connection);

            foreach (DataRow foundJctPersonAddressDao in foundJctPersonAddressDaos)
            {
                int addressId = (int)foundJctPersonAddressDao["address_id"];
                Address foundAddress = addressDaoImpl.Read_UsingAddressId(addressId, connection);
                if(foundAddress != null)
                {
                    foundAddress.IsPrimary = Enum_True_False.GetEnumFromId((int)foundJctPersonAddressDao["is_primary_id"]);
                    foundAddresss.Add(foundAddress);
                }
            }

            return foundAddresss;
        }

        private List<Email> Read_JctPersonEmail(int personId, IConnectable connection)
        {
            IEmailDao emailDaoImpl = DaoImplFactory.GetEmailDaoImpl();
            IJctPersonEmailDao jctPersonEmailDaoImpl = DaoImplFactory.GetJctPersonEmailDaoImpl();

            List<Email> foundEmails = new List<Email>();
            List<DataRow> foundJctPersonEmailDaos = jctPersonEmailDaoImpl.Read_AllWithPersonId(personId, connection);

            foreach (DataRow foundJctPersonEmailDao in foundJctPersonEmailDaos)
            {
                int emailId = (int)foundJctPersonEmailDao["email_id"];
                Email foundEmail = emailDaoImpl.Read_UsingEmailId(emailId, connection);
                if (foundEmail != null)
                {
                    foundEmail.IsPrimary = Enum_True_False.GetEnumFromId((int)foundJctPersonEmailDao["is_primary_id"]);
                    foundEmails.Add(foundEmail);
                }
            }

            return foundEmails;
        }

        private List<Enumeration> Read_JctPersonRole(int personId, IConnectable connection)
        {
            List<Enumeration> foundRoles = new List<Enumeration>();
            List<int> idsFound = new List<int>();

            IJctPersonRoleDao jctPersonRoleDaoImpl = DaoImplFactory.GetJctPersonRoleDaoImpl();
            List<DataRow> foundJctPersonRoleDaos = jctPersonRoleDaoImpl.Read_AllWithPersonId(personId, connection);
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
            List<DataRow> foundJctPersonPersonDaos = jctPersonPersonDaoImpl.Read_AllWithPersonOneId(personId, connection);

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
                List<SqlParameter> parameters = new List<SqlParameter>();

                StringBuilder sql = new StringBuilder("UPDATE person SET ");
                if (row["first_name"] != DBNull.Value)
                {
                    sql.Append("first_name = @first_name, ");
                    parameters.Add(new SqlParameter("@first_name", row["first_name"]));
                }
                else
                    sql.Append("first_name = NULL, ");

                if (row["middle_name"] != DBNull.Value)
                {
                    sql.Append("middle_name = @middle_name, ");
                    parameters.Add(new SqlParameter("@middle_name", row["middle_name"]));
                }
                else
                    sql.Append("middle_name = NULL, ");

                if (row["last_name"] != DBNull.Value)
                {
                    sql.Append("last_name = @last_name, ");
                    parameters.Add(new SqlParameter("@last_name", row["last_name"]));
                }
                else
                    sql.Append("last_name = NULL, ");

                if (row["birth_date"] != DBNull.Value)
                {
                    sql.Append("birth_date = @birth_date, ");
                    parameters.Add(new SqlParameter("@birth_date", row["birth_date"]));
                }
                else
                    sql.Append("birth_date = NULL, ");

                if (row["enum_gender_id"] != DBNull.Value)
                {
                    sql.Append("enum_gender_id = @enum_gender_id, ");
                    parameters.Add(new SqlParameter("@enum_gender_id", row["enum_gender_id"]));
                }
                else
                    sql.Append("enum_gender_id = NULL, ");

                if (row["enum_ethnic_id"] != DBNull.Value)
                {
                    sql.Append("enum_ethnic_id = @enum_ethnic_id, ");
                    parameters.Add(new SqlParameter("@enum_ethnic_id", row["enum_ethnic_id"]));
                }
                else
                    sql.Append("enum_ethnic_id = NULL, ");

                if (row["enum_residence_status_id"] != DBNull.Value)
                {
                    sql.Append("enum_residence_status_id = @enum_residence_status_id, ");
                    parameters.Add(new SqlParameter("@enum_residence_status_id", row["enum_residence_status_id"]));
                }
                else
                    sql.Append("enum_residence_status_id = NULL, ");

                if (row["login_name"] != DBNull.Value)
                {
                    sql.Append("login_name = @login_name, ");
                    parameters.Add(new SqlParameter("@login_name", row["login_name"]));
                }
                else
                    sql.Append("login_name = NULL, ");

                if (row["login_pw"] != DBNull.Value)
                {
                    sql.Append("login_pw = @login_pw, ");
                    parameters.Add(new SqlParameter("@login_pw", row["login_pw"]));
                }
                else
                    sql.Append("login_pw = NULL, ");

                sql.Append("changed = dbo.CURRENT_DATETIME(), ");
                sql.Append("changed_by = " + (_taskId != null ? _taskId.ToString() : "'Netus2'") + " ");
                sql.Append("WHERE person_id = @person_id");
                parameters.Add(new SqlParameter("@person_id", row["person_id"]));

                connection.ExecuteNonQuery(sql.ToString(), parameters);

                UpdateJctPersonRole(person.Roles, person.Id, connection);
                UpdatePhoneNumbers(person.PhoneNumbers, person.Id, connection);
                UpdateJctPersonPerson(person.Relations, person.Id, connection);
                UpdateAddresses(person.Addresses, person.Id, connection);
                UpdateEmails(person.Emails, person.Id, connection);
                UpdateJctPersonApp(person.Applications, person.Id, connection);
            }
            else
                throw new Exception("The following Person needs to be inserted into the database, before it can be updated.\n" + person.ToString());
        }

        private List<Application> UpdateJctPersonApp(List<Application> apps, int personId, IConnectable connection)
        {
            IJctPersonAppDao jctPersonAppDaoImpl = DaoImplFactory.GetJctPersonAppDaoImpl();
            List<DataRow> foundJctPersonAppDaos =
                jctPersonAppDaoImpl.Read_AllWithPersonId(personId, connection);
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
                if (appId <= 0)
                    throw new Exception("Applications must be already in the database before they can be linked with a person. PersonId: " + personId);

                if (foundAppIds.Contains(appId) == false)
                    jctPersonAppDaoImpl.Write(personId, appId, connection);
            }

            foreach (int foundAppId in foundAppIds)
            {
                if (appIds.Contains(foundAppId) == false)
                    jctPersonAppDaoImpl.Delete(personId, foundAppId, connection);
            }

            return apps;
        }

        private List<Address> UpdateAddresses(List<Address> addresses, int personId, IConnectable connection)
        {
            List<Address> addressesToBeReturned = new List<Address>();

            IAddressDao addressDaoImpl = DaoImplFactory.GetAddressDaoImpl();
            IJctPersonAddressDao jctPersonAddressDaoImpl = DaoImplFactory.GetJctPersonAddressDaoImpl();

            List<int> passedInAddressIds = new List<int>();
            foreach (Address passedInAddress in addresses)
            {
                if (passedInAddress.Id <= 0)
                    throw new Exception("Address must be in the database before it can be linked with a person. Address: " + passedInAddress.ToString());
                else
                {
                    passedInAddressIds.Add(passedInAddress.Id);

                    DataRow foundJctPersonAddress = jctPersonAddressDaoImpl.Read(personId, passedInAddress.Id, connection);

                    if (foundJctPersonAddress == null)
                        foundJctPersonAddress = jctPersonAddressDaoImpl.Write(personId, passedInAddress.Id, passedInAddress.IsPrimary.Id, connection);

                    Address foundAddress = addressDaoImpl.Read_UsingAddressId(passedInAddress.Id, connection);

                    if(foundAddress != null)
                    {
                        foundAddress.IsPrimary = Enum_True_False.GetEnumFromId((int)foundJctPersonAddress["is_primary_id"]);
                        addressesToBeReturned.Add(foundAddress);
                    }                    
                }
            }

            List<DataRow> foundJctAddresses = jctPersonAddressDaoImpl.Read_AllWithPersonId(personId, connection);
            foreach(DataRow foundJctPersonAddress in foundJctAddresses)
            {
                if (passedInAddressIds.Contains((int)foundJctPersonAddress["address_id"]) == false)
                    jctPersonAddressDaoImpl.Delete(personId, (int)foundJctPersonAddress["address_id"], connection);
            }

            return addressesToBeReturned;
        }

        private List<Email> UpdateEmails(List<Email> emails, int personId, IConnectable connection)
        {
            List<Email> emailsToBeReturned = new List<Email>();

            IEmailDao emailDaoImpl = DaoImplFactory.GetEmailDaoImpl();
            IJctPersonEmailDao jctPersonEmailDaoImpl = DaoImplFactory.GetJctPersonEmailDaoImpl();

            List<int> passedInEmailIds = new List<int>();
            foreach (Email passedInEmail in emails)
            {
                if (passedInEmail.Id <= 0)
                    throw new Exception("Email must be in the database before it can be linked with a person. Email: " + passedInEmail.ToString());
                else
                {
                    passedInEmailIds.Add(passedInEmail.Id);

                    DataRow foundJctPersonEmail = jctPersonEmailDaoImpl.Read(personId, passedInEmail.Id, connection);

                    if (foundJctPersonEmail == null)
                        foundJctPersonEmail = jctPersonEmailDaoImpl.Write(personId, passedInEmail.Id, passedInEmail.IsPrimary.Id, connection);

                    Email foundEmail = emailDaoImpl.Read_UsingEmailId(passedInEmail.Id, connection);

                    if (foundEmail != null)
                    {
                        foundEmail.IsPrimary = Enum_True_False.GetEnumFromId((int)foundJctPersonEmail["is_primary_id"]);
                        emailsToBeReturned.Add(foundEmail);
                    }
                }
            }

            List<DataRow> foundJctEmails = jctPersonEmailDaoImpl.Read_AllWithPersonId(personId, connection);
            foreach (DataRow foundJctPersonEmail in foundJctEmails)
            {
                if (passedInEmailIds.Contains((int)foundJctPersonEmail["email_id"]) == false)
                    jctPersonEmailDaoImpl.Delete(personId, (int)foundJctPersonEmail["email_id"], connection);
            }

            return emailsToBeReturned;
        }

        private List<Enumeration> UpdateJctPersonRole(List<Enumeration> roles, int personId, IConnectable connection)
        {
            List<Enumeration> updatedRoles = new List<Enumeration>();
            IJctPersonRoleDao jctPersonRoleDaoImpl = DaoImplFactory.GetJctPersonRoleDaoImpl();
            List<DataRow> foundJctPersonRoleDaos =
                jctPersonRoleDaoImpl.Read_AllWithPersonId(personId, connection);
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
                if (jctPersonRoleDao != null)
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
                jctPersonPersonDaoImpl.Read_AllWithPersonOneId(personId, connection);
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
                if (jctPersonPersonDao != null)
                    updatedRelations.Add((int)jctPersonPersonDao["person_two_id"]);
            }

            foreach (int foundRelation in foundRelations)
            {
                if (relations.Contains(foundRelation) == false)
                    jctPersonPersonDaoImpl.Delete(personId, foundRelation, connection);
            }
            return relations;
        }

        private List<PhoneNumber> UpdatePhoneNumbers(List<PhoneNumber> phoneNumbers, int personId, IConnectable connection)
        {
            List<PhoneNumber> phoneNumbersToBeReturned = new List<PhoneNumber>();

            IPhoneNumberDao phoneNumberDaoImpl = DaoImplFactory.GetPhoneNumberDaoImpl();
            IJctPersonPhoneNumberDao jctPersonPhoneNumberDaoImpl = DaoImplFactory.GetJctPersonPhoneNumberDaoImpl();

            List<int> passedInPhoneNubmerIds = new List<int>();
            foreach (PhoneNumber passedInPhoneNumber in phoneNumbers)
            {
                if(passedInPhoneNumber.Id <= 0)
                    throw new Exception("Phone Number must be in the database before it can be linked with a person. PhoneNumber: " + passedInPhoneNumber.ToString());
                else
                {
                    passedInPhoneNubmerIds.Add(passedInPhoneNumber.Id);

                    DataRow foundJctPersonPhoneNumber = jctPersonPhoneNumberDaoImpl.Read(personId, passedInPhoneNumber.Id, connection);

                    if (foundJctPersonPhoneNumber == null)
                        foundJctPersonPhoneNumber = jctPersonPhoneNumberDaoImpl.Write(personId, passedInPhoneNumber.Id, passedInPhoneNumber.IsPrimary.Id, connection);

                    PhoneNumber foundPhoneNumber = phoneNumberDaoImpl.Read_WithPhoneNumberId(passedInPhoneNumber.Id, connection);

                    if(foundPhoneNumber != null)
                    {
                        foundPhoneNumber.IsPrimary = Enum_True_False.GetEnumFromId((int)foundJctPersonPhoneNumber["is_primary_id"]);
                        phoneNumbersToBeReturned.Add(foundPhoneNumber);
                    }
                }
            }

            List<DataRow> foundJctPersonPhoneNubmers = jctPersonPhoneNumberDaoImpl.Read_AllWithPersonId(personId, connection);
            foreach(DataRow foundJctPersonPhoneNubmer in foundJctPersonPhoneNubmers)
            {
                if (passedInPhoneNubmerIds.Contains((int)foundJctPersonPhoneNubmer["phone_number_id"]) == false)
                    jctPersonPhoneNumberDaoImpl.Delete(personId, (int)foundJctPersonPhoneNubmer["phone_number_id"], connection);
            }

            return phoneNumbersToBeReturned;
        }

        public Person Write(Person person, IConnectable connection)
        {
            DataRow row = daoObjectMapper.MapPerson(person);

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sqlValues = new StringBuilder();
            if (row["first_name"] != DBNull.Value)
            {
                sqlValues.Append("@first_name, ");
                parameters.Add(new SqlParameter("@first_name", row["first_name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["middle_name"] != DBNull.Value)
            {
                sqlValues.Append("@middle_name, ");
                parameters.Add(new SqlParameter("@middle_name", row["middle_name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if(row["last_name"] != DBNull.Value)
            {
                sqlValues.Append("@last_name, ");
                parameters.Add(new SqlParameter("@last_name", row["last_name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["birth_date"] != DBNull.Value)
            {
                sqlValues.Append("@birth_date, ");
                var birth_date = row["birth_date"];
                parameters.Add(new SqlParameter("@birth_date", birth_date));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_gender_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_gender_id, ");
                parameters.Add(new SqlParameter("@enum_gender_id", row["enum_gender_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_ethnic_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_ethnic_id, ");
                parameters.Add(new SqlParameter("@enum_ethnic_id", row["enum_ethnic_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["enum_residence_status_id"] != DBNull.Value)
            {
                sqlValues.Append("@enum_residence_status_id, ");
                parameters.Add(new SqlParameter("@enum_residence_status_id", row["enum_residence_status_id"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["login_name"] != DBNull.Value)
            {
                sqlValues.Append("@login_name, ");
                parameters.Add(new SqlParameter("@login_name", row["login_name"]));
            }
            else
                sqlValues.Append("NULL, ");

            if (row["login_pw"] != DBNull.Value)
            {
                sqlValues.Append("@login_pw, ");
                parameters.Add(new SqlParameter("@login_pw", row["login_pw"]));
            }
            else
                sqlValues.Append("NULL, ");

            sqlValues.Append("dbo.CURRENT_DATETIME(), ");
            sqlValues.Append(_taskId != null ? _taskId.ToString() : "'Netus2'");

            string sql =
                "INSERT INTO person " +
                "(first_name, middle_name, last_name, birth_date, enum_gender_id, enum_ethnic_id, " +
                "enum_residence_status_id, login_name, login_pw, created, created_by) " +
                "VALUES (" + sqlValues.ToString() + ")";

            row["person_id"] = connection.InsertNewRecord(sql, parameters);

            Person result = daoObjectMapper.MapPerson(row);

            result.PhoneNumbers = UpdatePhoneNumbers(person.PhoneNumbers, result.Id, connection);
            result.Addresses = UpdateAddresses(person.Addresses, result.Id, connection);
            result.Emails = UpdateEmails(person.Emails, result.Id, connection);
            result.Applications = UpdateJctPersonApp(person.Applications, result.Id, connection);
            result.Roles = UpdateJctPersonRole(person.Roles, result.Id, connection);
            result.Relations = UpdateJctPersonPerson(person.Relations, result.Id, connection);

            return result;
        }
    }
}
