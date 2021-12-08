using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.logObjects;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.utilityTools
{
    public class LogReader
    {
        public List<LogPerson> Read_LogPerson(IConnectable connection)
        {
            string sql = "SELECT * FROM log_person";

            DataTable dtLogPerson = DataTableFactory.CreateDataTable_Netus2_Log_Person();
            dtLogPerson = connection.ReadIntoDataTable(sql, dtLogPerson);

            List<LogPerson> logPeople = new List<LogPerson>();
            foreach (DataRow row in dtLogPerson.Rows)
            {
                LogPerson logPerson = new LogPerson();
                foreach(DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_person_id":
                            if (row[columnName] != DBNull.Value)
                                logPerson.log_person_id = (int)row[columnName];
                            else
                                logPerson.log_person_id = -1;
                            break;
                        case "person_id":
                            if (row[columnName] != DBNull.Value)
                                logPerson.person_id = (int)row[columnName];
                            else
                                logPerson.person_id = -1;
                            break;
                        case "first_name":
                            if (row[columnName] != DBNull.Value)
                                logPerson.first_name = (string)row[columnName];
                            else
                                logPerson.first_name = null;
                            break;
                        case "middle_name":
                            if (row[columnName] != DBNull.Value)
                                logPerson.middle_name = (string)row[columnName];
                            else
                                logPerson.middle_name = null;
                            break;
                        case "last_name":
                            if (row[columnName] != DBNull.Value)
                                logPerson.last_name = (string)row[columnName];
                            else
                                logPerson.last_name = null;
                            break;
                        case "birth_date":
                            if (row[columnName] != DBNull.Value)
                                logPerson.birth_date = (DateTime)row[columnName];
                            else
                                logPerson.birth_date = new DateTime();
                            break;
                        case "enum_gender_id":
                            if (row[columnName] != DBNull.Value)
                                logPerson.Gender = Enum_Gender.GetEnumFromId((int)row[columnName]);
                            else
                                logPerson.Gender = null;
                            break;
                        case "enum_ethnic_id":
                            if (row[columnName] != DBNull.Value)
                                logPerson.Ethnic = Enum_Ethnic.GetEnumFromId((int)row[columnName]);
                            else
                                logPerson.Ethnic = null;
                            break;
                        case "enum_residence_status_id":
                            if (row[columnName] != DBNull.Value)
                                logPerson.ResidenceStatus = Enum_Residence_Status.GetEnumFromId((int)row[columnName]);
                            else
                                logPerson.ResidenceStatus = null;
                            break;
                        case "login_name":
                            if (row[columnName] != DBNull.Value)
                                logPerson.login_name = (string)row[columnName];
                            else
                                logPerson.login_name = null;
                            break;
                        case "login_pw":
                            if (row[columnName] != DBNull.Value)
                                logPerson.login_pw = (string)row[columnName];
                            else
                                logPerson.login_pw = null;
                            break;
                        case "enum_status_id":
                            if (row[columnName] != DBNull.Value)
                                logPerson.Status = Enum_Status.GetEnumFromId((int)row[columnName]);
                            else
                                logPerson.Status = null;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logPerson.created = (DateTime)row[columnName];
                            else
                                logPerson.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logPerson.created_by = (string)row[columnName];
                            else
                                logPerson.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logPerson.changed = (DateTime)row[columnName];
                            else
                                logPerson.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logPerson.changed_by = (string)row[columnName];
                            else
                                logPerson.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logPerson.log_date = (DateTime)row[columnName];
                            else
                                logPerson.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logPerson.log_user = (string)row[columnName];
                            else
                                logPerson.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logPerson.LogAction = Enum_Log_Action.GetEnumFromId((int)row[columnName]);
                            else
                                logPerson.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_person table: " + columnName);
                    }
                }
                logPeople.Add(logPerson);
            }
            return logPeople;
        }

        public List<LogJctPersonRole> Read_LogJctPersonRole(IConnectable connection)
        {
            string sql = "SELECT * FROM log_jct_person_role";

            DataTable dtLogJctpersonRole = DataTableFactory.CreateDataTable_Netus2_Log_JctPersonRole();
            dtLogJctpersonRole = connection.ReadIntoDataTable(sql, dtLogJctpersonRole);

            List<LogJctPersonRole> logJctPersonRoles = new List<LogJctPersonRole>();
            foreach (DataRow row in dtLogJctpersonRole.Rows)
            {
                LogJctPersonRole logJctPersonRole = new LogJctPersonRole();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_person_role_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonRole.log_jct_person_role_id = (int)row[columnName];
                            else
                                logJctPersonRole.log_jct_person_role_id = -1;
                            break;
                        case "person_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonRole.person_id = (int)row[columnName];
                            else
                                logJctPersonRole.person_id = -1;
                            break;
                        case "enum_role_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonRole.set_Role((int)row[columnName]);
                            else
                                logJctPersonRole.Role = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonRole.log_date = (DateTime)row[columnName];
                            else
                                logJctPersonRole.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonRole.log_user = (string)row[columnName];
                            else
                                logJctPersonRole.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonRole.set_LogAction((int)row[columnName]);
                            else
                                logJctPersonRole.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_person_role table: " + columnName);
                    }
                }
                logJctPersonRoles.Add(logJctPersonRole);
            }
            return logJctPersonRoles;
        }

        public List<LogJctPersonPerson> Read_LogJctPersonPerson(IConnectable connection)
        {
            string sql = "SELECT * FROM log_jct_person_person";

            DataTable dtLogJctPersonPerson = DataTableFactory.CreateDataTable_Netus2_Log_JctPersonPerson();
            dtLogJctPersonPerson = connection.ReadIntoDataTable(sql, dtLogJctPersonPerson);

            List<LogJctPersonPerson> logJctPersonPersons = new List<LogJctPersonPerson>();
            foreach (DataRow row in dtLogJctPersonPerson.Rows)
            {
                LogJctPersonPerson logJctPersonPerson = new LogJctPersonPerson();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_person_person_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPerson.log_jct_person_person_id = (int)row[columnName];
                            else
                                logJctPersonPerson.log_jct_person_person_id = -1;
                            break;
                        case "person_one_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPerson.person_one_id = (int)row[columnName];
                            else
                                logJctPersonPerson.person_one_id = -1;
                            break;
                        case "person_two_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPerson.person_two_id = (int)row[columnName];
                            else
                                logJctPersonPerson.person_two_id = -1;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPerson.log_date = (DateTime)row[columnName];
                            else
                                logJctPersonPerson.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPerson.log_user = (string)row[columnName];
                            else
                                logJctPersonPerson.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPerson.set_LogAction((int)row[columnName]);
                            else
                                logJctPersonPerson.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_person_person table: " + columnName);
                    }
                }
                logJctPersonPersons.Add(logJctPersonPerson);
            }

            return logJctPersonPersons;
        }

        public List<LogUniqueIdentifier> Read_LogUniqueIdentifier(IConnectable connection)
        {
            string sql = "SELECT * FROM log_unique_identifier";

            DataTable dtLogUniqueIdentifier = DataTableFactory.CreateDataTable_Netus2_Log_UniqueIdentifier();
            dtLogUniqueIdentifier = connection.ReadIntoDataTable(sql, dtLogUniqueIdentifier);

            List<LogUniqueIdentifier> logUniqueIdentifiers = new List<LogUniqueIdentifier>();
            foreach (DataRow row in dtLogUniqueIdentifier.Rows)
            {
                LogUniqueIdentifier logUniqueIdentifier = new LogUniqueIdentifier();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_unique_identifier_id":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.log_unique_identifier_id = (int)row[columnName];
                            else
                                logUniqueIdentifier.log_unique_identifier_id = -1;
                            break;
                        case "unique_identifier_id":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.unique_identifier_id = (int)row[columnName];
                            else
                                logUniqueIdentifier.unique_identifier_id = -1;
                            break;
                        case "person_id":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.person_id = (int)row[columnName];
                            else
                                logUniqueIdentifier.person_id = -1;
                            break;
                        case "unique_identifier_value":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.unique_identifier = (string)row[columnName];
                            else
                                logUniqueIdentifier.unique_identifier = null;
                            break;
                        case "enum_identifier_id":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.IdentifierType = Enum_Identifier.GetEnumFromId((int)row[columnName]);
                            else
                                logUniqueIdentifier.IdentifierType = null;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.created = (DateTime)row[columnName];
                            else
                                logUniqueIdentifier.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.created_by = (string)row[columnName];
                            else
                                logUniqueIdentifier.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.changed = (DateTime)row[columnName];
                            else
                                logUniqueIdentifier.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.changed_by = (string)row[columnName];
                            else
                                logUniqueIdentifier.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.log_date = (DateTime)row[columnName];
                            else
                                logUniqueIdentifier.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.log_user = (string)row[columnName];
                            else
                                logUniqueIdentifier.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.set_LogAction((int)row[columnName]);
                            else
                                logUniqueIdentifier.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_unique_identifier table: " + columnName);
                    }
                }
                logUniqueIdentifiers.Add(logUniqueIdentifier);
            }

            return logUniqueIdentifiers;
        }

        public List<LogProvider> Read_LogProvider(IConnectable connection)
        {
            string sql = "SELECT * FROM log_provider";

            DataTable dtLogProvider = DataTableFactory.CreateDataTable_Netus2_Log_Provider();
            dtLogProvider = connection.ReadIntoDataTable(sql, dtLogProvider);

            List<LogProvider> logProviders = new List<LogProvider>();
            foreach(DataRow row in dtLogProvider.Rows)
            {
                LogProvider logProvider = new LogProvider();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_provider_id":
                            if (row[columnName] != DBNull.Value)
                                logProvider.log_provider_id = (int)row[columnName];
                            else
                                logProvider.log_provider_id = -1;
                            break;
                        case "provider_id":
                            if (row[columnName] != DBNull.Value)
                                logProvider.provider_id = (int)row[columnName];
                            else
                                logProvider.provider_id = -1;
                            break;
                        case "name":
                            if (row[columnName] != DBNull.Value)
                                logProvider.name = (string)row[columnName];
                            else
                                logProvider.name = null;
                            break;
                        case "url_standard_access":
                            if (row[columnName] != DBNull.Value)
                                logProvider.url_standard_access = (string)row[columnName];
                            else
                                logProvider.url_standard_access = null;
                            break;
                        case "url_admin_access":
                            if (row[columnName] != DBNull.Value)
                                logProvider.url_admin_access = (string)row[columnName];
                            else
                                logProvider.url_admin_access = null;
                            break;
                        case "populated_by":
                            if (row[columnName] != DBNull.Value)
                                logProvider.populated_by = (string)row[columnName];
                            else
                                logProvider.populated_by = null;
                            break;
                        case "parent_provider_id":
                            if (row[columnName] != DBNull.Value)
                                logProvider.parent_provider_id = (int)row[columnName];
                            else
                                logProvider.parent_provider_id = -1;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logProvider.created = (DateTime)row[columnName];
                            else
                                logProvider.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logProvider.created_by = (string)row[columnName];
                            else
                                logProvider.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logProvider.changed = (DateTime)row[columnName];
                            else
                                logProvider.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logProvider.changed_by = (string)row[columnName];
                            else
                                logProvider.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logProvider.log_date = (DateTime)row[columnName];
                            else
                                logProvider.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logProvider.log_user = (string)row[columnName];
                            else
                                logProvider.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logProvider.set_LogAction((int)row[columnName]);
                            else
                                logProvider.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_provider table: " + columnName);
                    }
                }
                logProviders.Add(logProvider);
            }

            return logProviders;
        }

        public List<LogApplication> Read_LogApplication(IConnectable connection)
        {
            string sql = "SELECT * FROM log_application";

            DataTable dtLogApp = DataTableFactory.CreateDataTable_Netus2_Log_Application();
            dtLogApp = connection.ReadIntoDataTable(sql, dtLogApp);

            List<LogApplication> logApps = new List<LogApplication>();
            foreach (DataRow row in dtLogApp.Rows)
            {
                LogApplication logApp = new LogApplication();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_application_id":
                            if (row[columnName] != DBNull.Value)
                                logApp.log_application_id = (int)row[columnName];
                            else
                                logApp.log_application_id = -1;
                            break;
                        case "application_id":
                            if (row[columnName] != DBNull.Value)
                                logApp.application_id = (int)row[columnName];
                            else
                                logApp.application_id = -1;
                            break;
                        case "name":
                            if (row[columnName] != DBNull.Value)
                                logApp.name = (string)row[columnName];
                            else
                                logApp.name = null;
                            break;
                        case "provider_id":
                            if (row[columnName] != DBNull.Value)
                                logApp.provider_id = (int)row[columnName];
                            else
                                logApp.provider_id = -1;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logApp.created = (DateTime)row[columnName];
                            else
                                logApp.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logApp.created_by = (string)row[columnName];
                            else
                                logApp.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logApp.changed = (DateTime)row[columnName];
                            else
                                logApp.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logApp.changed_by = (string)row[columnName];
                            else
                                logApp.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logApp.log_date = (DateTime)row[columnName];
                            else
                                logApp.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logApp.log_user = (string)row[columnName];
                            else
                                logApp.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logApp.set_LogAction((int)row[columnName]);
                            else
                                logApp.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_application table: " + columnName);
                    }
                }
                logApps.Add(logApp);
            }

            return logApps;
        }

        public List<LogJctPersonApplication> Read_LogJctPersonApplication(IConnectable connection)
        {
            string sql = "SELECT * FROM log_jct_person_application";

            DataTable dtLogJctPersonApplication = DataTableFactory.CreateDataTable_Netus2_Log_JctPersonApplication();
            dtLogJctPersonApplication = connection.ReadIntoDataTable(sql, dtLogJctPersonApplication);

            List<LogJctPersonApplication> logJctPersonApplications = new List<LogJctPersonApplication>();
            foreach (DataRow row in dtLogJctPersonApplication.Rows)
            {
                LogJctPersonApplication logJctPersonApplication = new LogJctPersonApplication();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_person_application_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApplication.log_jct_person_application_id = (int)row[columnName];
                            else
                                logJctPersonApplication.log_jct_person_application_id = -1;
                            break;
                        case "person_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApplication.person_id = (int)row[columnName];
                            else
                                logJctPersonApplication.person_id = -1;
                            break;
                        case "application_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApplication.application_id = (int)row[columnName];
                            else
                                logJctPersonApplication.application_id = -1;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApplication.log_date = (DateTime)row[columnName];
                            else
                                logJctPersonApplication.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApplication.log_user = (string)row[columnName];
                            else
                                logJctPersonApplication.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApplication.set_LogAction((int)row[columnName]);
                            else
                                logJctPersonApplication.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_person_application table: " + columnName);
                    }
                }
                logJctPersonApplications.Add(logJctPersonApplication);
            }

            return logJctPersonApplications;
        }

        public List<LogJctPersonPhoneNumber> Read_LogJctPersonPhoneNumber(IConnectable connection)
        {
            string sql = "SELECT * FROM log_jct_person_phone_number";

            DataTable dtLogJctPersonPhoneNumber = DataTableFactory.CreateDataTable_Netus2_Log_JctPersonPhoneNumber();
            dtLogJctPersonPhoneNumber = connection.ReadIntoDataTable(sql, dtLogJctPersonPhoneNumber);

            List<LogJctPersonPhoneNumber> logJctPersonPhoneNumbers = new List<LogJctPersonPhoneNumber>();
            foreach (DataRow row in dtLogJctPersonPhoneNumber.Rows)
            {
                LogJctPersonPhoneNumber logJctPersonPhoneNumber = new LogJctPersonPhoneNumber();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_person_phone_number_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPhoneNumber.log_jct_person_phone_number_id = (int)row[columnName];
                            else
                                logJctPersonPhoneNumber.log_jct_person_phone_number_id = -1;
                            break;
                        case "person_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPhoneNumber.person_id = (int)row[columnName];
                            else
                                logJctPersonPhoneNumber.person_id = -1;
                            break;
                        case "phone_number_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPhoneNumber.phone_number_id = (int)row[columnName];
                            else
                                logJctPersonPhoneNumber.phone_number_id = -1;
                            break;
                        case "is_primary":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPhoneNumber.IsPrimary = (bool)row[columnName];
                            else
                                logJctPersonPhoneNumber.IsPrimary = false;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPhoneNumber.log_date = (DateTime)row[columnName];
                            else
                                logJctPersonPhoneNumber.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPhoneNumber.log_user = (string)row[columnName];
                            else
                                logJctPersonPhoneNumber.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonPhoneNumber.set_LogAction((int)row[columnName]);
                            else
                                logJctPersonPhoneNumber.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_person_phone_number table: " + columnName);
                    }
                }
                logJctPersonPhoneNumbers.Add(logJctPersonPhoneNumber);
            }

            return logJctPersonPhoneNumbers;
        }

        public List<LogPhoneNumber> Read_LogPhoneNumber(IConnectable connection)
        {
            string sql = "SELECT * FROM log_phone_number";

            DataTable dtLogPhoneNumber = DataTableFactory.CreateDataTable_Netus2_Log_PhoneNumber();
            dtLogPhoneNumber = connection.ReadIntoDataTable(sql, dtLogPhoneNumber);

            List<LogPhoneNumber> logPhoneNumbers = new List<LogPhoneNumber>();
            foreach (DataRow row in dtLogPhoneNumber.Rows)
            {
                LogPhoneNumber logPhoneNumber = new LogPhoneNumber();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_phone_number_id":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.log_phone_number_id = (int)row[columnName];
                            else
                                logPhoneNumber.log_phone_number_id = -1;
                            break;
                        case "phone_number_id":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.phone_number_id = (int)row[columnName];
                            else
                                logPhoneNumber.phone_number_id = -1;
                            break;
                        case "phone_number_value":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.phone_number = (string)row[columnName];
                            else
                                logPhoneNumber.phone_number = null;
                            break;
                        case "enum_phone_id":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.set_PhoneType((int)row[columnName]);
                            else
                                logPhoneNumber.PhoneType = null;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.created = (DateTime)row[columnName];
                            else
                                logPhoneNumber.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.created_by = (string)row[columnName];
                            else
                                logPhoneNumber.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.changed = (DateTime)row[columnName];
                            else
                                logPhoneNumber.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.changed_by = (string)row[columnName];
                            else
                                logPhoneNumber.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.log_date = (DateTime)row[columnName];
                            else
                                logPhoneNumber.log_date = new DateTime();
                            break;
                        case "log_user":
                            logPhoneNumber.log_user = row[columnName] != DBNull.Value ? (string)row[columnName] : null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.set_LogAction((int)row[columnName]);
                            else
                                logPhoneNumber.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_phone_number table: " + columnName);
                    }
                }
                logPhoneNumbers.Add(logPhoneNumber);
            }

            return logPhoneNumbers;
        }

        public List<LogAddress> Read_LogAddress(IConnectable connection)
        {
            string sql = "SELECT * FROM log_address";

            DataTable dtLogAddress = DataTableFactory.CreateDataTable_Netus2_Log_Address();
            dtLogAddress = connection.ReadIntoDataTable(sql, dtLogAddress);

            List<LogAddress> logAddresses = new List<LogAddress>();
            foreach (DataRow row in dtLogAddress.Rows)
            {
                LogAddress logAddress = new LogAddress();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_address_id":
                            if (row[columnName] != DBNull.Value)
                                logAddress.log_address_id = (int)row[columnName];
                            else
                                logAddress.log_address_id = -1;
                            break;
                        case "address_id":
                            if (row[columnName] != DBNull.Value)
                                logAddress.address_id = (int)row[columnName];
                            else
                                logAddress.address_id = -1;
                            break;
                        case "address_line_1":
                            if (row[columnName] != DBNull.Value)
                                logAddress.address_line_1 = (string)row[columnName];
                            else
                                logAddress.address_line_1 = null;
                            break;
                        case "address_line_2":
                            if (row[columnName] != DBNull.Value)
                                logAddress.address_line_2 = (string)row[columnName];
                            else
                                logAddress.address_line_2 = null;
                            break;
                        case "address_line_3":
                            if (row[columnName] != DBNull.Value)
                                logAddress.address_line_3 = (string)row[columnName];
                            else
                                logAddress.address_line_3 = null;
                            break;
                        case "address_line_4":
                            if (row[columnName] != DBNull.Value)
                                logAddress.address_line_4 = (string)row[columnName];
                            else
                                logAddress.address_line_4 = null;
                            break;
                        case "apartment":
                            if (row[columnName] != DBNull.Value)
                                logAddress.apartment = (string)row[columnName];
                            else
                                logAddress.apartment = null;
                            break;
                        case "city":
                            if (row[columnName] != DBNull.Value)
                                logAddress.city = (string)row[columnName];
                            else
                                logAddress.city = null;
                            break;
                        case "enum_state_province_id":
                            if (row[columnName] != DBNull.Value)
                                logAddress.set_StateProvince((int)row[columnName]);
                            else
                                logAddress.StateProvince = null;
                            break;
                        case "postal_code":
                            if (row[columnName] != DBNull.Value)
                                logAddress.postal_code = (string)row[columnName];
                            else
                                logAddress.postal_code = null;
                            break;
                        case "enum_country_id":
                            if (row[columnName] != DBNull.Value)
                                logAddress.set_Country((int)row[columnName]);
                            else
                                logAddress.Country = null;
                            break;
                        case "enum_address_id":
                            if (row[columnName] != DBNull.Value)
                                logAddress.set_AddressType((int)row[columnName]);
                            else
                                logAddress.AddressType = null;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logAddress.created = (DateTime)row[columnName];
                            else
                                logAddress.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logAddress.created_by = (string)row[columnName];
                            else
                                logAddress.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logAddress.changed = (DateTime)row[columnName];
                            else
                                logAddress.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logAddress.changed_by = (string)row[columnName];
                            else
                                logAddress.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logAddress.log_date = (DateTime)row[columnName];
                            else
                                logAddress.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logAddress.log_user = (string)row[columnName];
                            else
                                logAddress.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logAddress.set_LogAction((int)row[columnName]);
                            else
                                logAddress.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_address table: " + columnName);
                    }
                }
                logAddresses.Add(logAddress);
            }

            return logAddresses;
        }

        public List<LogJctPersonAddress> Read_LogJctPersonAddress(IConnectable connection)
        {
            string sql = "SELECT * FROM log_jct_person_address";

            DataTable dtLogJctPersonAddress = DataTableFactory.CreateDataTable_Netus2_Log_JctPersonAddress();
            dtLogJctPersonAddress = connection.ReadIntoDataTable(sql, dtLogJctPersonAddress);

            List<LogJctPersonAddress> logJctPersonAddresses = new List<LogJctPersonAddress>();
            foreach (DataRow row in dtLogJctPersonAddress.Rows)
            {
                LogJctPersonAddress logJctPersonAddress = new LogJctPersonAddress();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_person_address_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonAddress.log_jct_person_address_id = (int)row[columnName];
                            else
                                logJctPersonAddress.log_jct_person_address_id = -1;
                            break;
                        case "person_id":
                            if(row[columnName] != DBNull.Value)
                                logJctPersonAddress.person_id = (int)row[columnName];
                            else
                                logJctPersonAddress.person_id = -1;
                            break;
                        case "address_id":
                            if(row[columnName] != DBNull.Value)
                                logJctPersonAddress.address_id = (int)row[columnName];
                            else
                                logJctPersonAddress.address_id = -1;
                            break;
                        case "is_primary":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonAddress.IsPrimary = (bool)row[columnName];
                            else
                                logJctPersonAddress.IsPrimary = false;
                            break;
                        case "enum_address_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonAddress.set_AddressType((int)row[columnName]);
                            else
                                logJctPersonAddress.AddressType = null;
                            break;
                        case "log_date":
                            if(row[columnName] != DBNull.Value)
                                logJctPersonAddress.log_date = (DateTime)row[columnName];
                            else
                                logJctPersonAddress.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonAddress.log_user = (string)row[columnName];
                            else
                                logJctPersonAddress.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if(row[columnName] != DBNull.Value)
                                logJctPersonAddress.set_LogAction((int)row[columnName]);
                            else
                                logJctPersonAddress.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_person_address table: " + columnName);
                    }
                }
                logJctPersonAddresses.Add(logJctPersonAddress);
            }

            return logJctPersonAddresses;
        }

        public List<LogEmail> Read_LogEmail(IConnectable connection)
        {
            string sql = "SELECT * FROM log_email";

            DataTable dtLogEmail = DataTableFactory.CreateDataTable_Netus2_Log_Email();
            dtLogEmail = connection.ReadIntoDataTable(sql, dtLogEmail);

            List<LogEmail> logEmails = new List<LogEmail>();
            foreach (DataRow row in dtLogEmail.Rows)
            {
                LogEmail logEmail = new LogEmail();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_email_id":
                            if (row[columnName] != DBNull.Value)
                                logEmail.log_email_id = (int)row[columnName];
                            else
                                logEmail.log_email_id = -1;
                            break;
                        case "email_id":
                            if (row[columnName] != DBNull.Value)
                                logEmail.email_id = (int)row[columnName];
                            else
                                logEmail.email_id = -1;
                            break;
                        case "email":
                            if (row[columnName] != DBNull.Value)
                                logEmail.email = (string)row[columnName];
                            else
                                logEmail.email = null;
                            break;
                        case "enum_email_id":
                            if (row[columnName] != DBNull.Value)
                                logEmail.set_EmailType((int)row[columnName]);
                            else
                                logEmail.EmailType = null;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logEmail.created = (DateTime)row[columnName];
                            else
                                logEmail.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logEmail.created_by = (string)row[columnName];
                            else
                                logEmail.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logEmail.changed = (DateTime)row[columnName];
                            else
                                logEmail.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logEmail.changed_by = (string)row[columnName];
                            else
                                logEmail.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logEmail.log_date = (DateTime)row[columnName];
                            else
                                logEmail.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logEmail.log_user = (string)row[columnName];
                            else
                                logEmail.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logEmail.set_LogAction((int)row[columnName]);
                            else
                                logEmail.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_email table: " + columnName);
                    }
                }
                logEmails.Add(logEmail);
            }

            return logEmails;
        }

        public List<LogJctPersonEmail> Read_LogJctPersonEmail(IConnectable connection)
        {
            string sql = "SELECT * FROM log_jct_person_email";

            DataTable dtLogJctPersonEmail = DataTableFactory.CreateDataTable_Netus2_Log_JctPersonEmail();
            dtLogJctPersonEmail = connection.ReadIntoDataTable(sql, dtLogJctPersonEmail);

            List<LogJctPersonEmail> logJctPersonEmails = new List<LogJctPersonEmail>();
            foreach (DataRow row in dtLogJctPersonEmail.Rows)
            {
                LogJctPersonEmail logJctPersonEmail = new LogJctPersonEmail();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_person_email_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonEmail.log_jct_person_email_id = (int)row[columnName];
                            else
                                logJctPersonEmail.log_jct_person_email_id = -1;
                            break;
                        case "person_id":
                            if(row[columnName] != DBNull.Value)
                                logJctPersonEmail.person_id = (int)row[columnName];
                            else
                                logJctPersonEmail.person_id = -1;
                            break;
                        case "email_id":
                            if(row[columnName] != DBNull.Value)
                                logJctPersonEmail.email_id = (int)row[columnName];
                            else
                                logJctPersonEmail.email_id = -1;
                            break;
                        case "is_primary":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonEmail.IsPrimary = (bool)row[columnName];
                            else
                                logJctPersonEmail.IsPrimary = false;
                            break;
                        case "log_date":
                            if(row[columnName] != DBNull.Value)
                                logJctPersonEmail.log_date = (DateTime)row[columnName];
                            else
                                logJctPersonEmail.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonEmail.log_user = (string)row[columnName];
                            else
                                logJctPersonEmail.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if(row[columnName] != DBNull.Value)
                                logJctPersonEmail.set_LogAction((int)row[columnName]);
                            else
                                logJctPersonEmail.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_person_email table: " + columnName);
                    }
                }
                logJctPersonEmails.Add(logJctPersonEmail);
            }

            return logJctPersonEmails;
        }

        public List<LogEmploymentSession> Read_LogEmploymentSession(IConnectable connection)
        {
            string sql = "SELECT * FROM log_employment_session";

            DataTable dtLogEmploymentSession = DataTableFactory.CreateDataTable_Netus2_Log_EmploymentSession();
            dtLogEmploymentSession = connection.ReadIntoDataTable(sql, dtLogEmploymentSession);

            List<LogEmploymentSession> logEmploymentSessions = new List<LogEmploymentSession>();
            foreach (DataRow row in dtLogEmploymentSession.Rows)
            {
                LogEmploymentSession logEmploymentSession = new LogEmploymentSession();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_employment_session_id":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.log_employment_session_id = (int)row[columnName];
                            else
                                logEmploymentSession.log_employment_session_id = -1;
                            break;
                        case "employment_session_id":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.employment_session_id = (int)row[columnName];
                            else
                                logEmploymentSession.employment_session_id = -1;
                            break;
                        case "name":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.name = (string)row[columnName];
                            else
                                logEmploymentSession.name = null;
                            break;
                        case "person_id":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.person_id = (int)row[columnName];
                            else
                                logEmploymentSession.person_id = -1;
                            break;
                        case "start_date":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.start_date = (DateTime)row[columnName];
                            else
                                logEmploymentSession.start_date = new DateTime();
                            break;
                        case "end_date":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.end_date = (DateTime)row[columnName];
                            else
                                logEmploymentSession.end_date = new DateTime();
                            break;
                        case "is_primary":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.IsPrimary = (bool)row[columnName];
                            else
                                logEmploymentSession.IsPrimary = false;
                            break;
                        case "enum_session_id":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.set_SessionType((int)row[columnName]);
                            else
                                logEmploymentSession.SessionType = null;
                            break;
                        case "organization_id":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.organization_id = (int)row[columnName];
                            else
                                logEmploymentSession.organization_id = -1;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.created = (DateTime)row[columnName];
                            else
                                logEmploymentSession.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.created_by = (string)row[columnName];
                            else
                                logEmploymentSession.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.changed = (DateTime)row[columnName];
                            else
                                logEmploymentSession.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.changed_by = (string)row[columnName];
                            else
                                logEmploymentSession.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.log_date = (DateTime)row[columnName];
                            else
                                logEmploymentSession.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.log_user = (string)row[columnName];
                            else
                                logEmploymentSession.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.set_LogAction((int)row[columnName]);
                            else
                                logEmploymentSession.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_employment_session table: " + columnName);
                    }
                }
                logEmploymentSessions.Add(logEmploymentSession);
            }

            return logEmploymentSessions;
        }

        public List<LogAcademicSession> Read_LogAcademicSession(IConnectable connection)
        {
            string sql = "SELECT * FROM log_academic_session";

            DataTable dtLogAcademicSession = DataTableFactory.CreateDataTable_Netus2_Log_AcademicSession();
            dtLogAcademicSession = connection.ReadIntoDataTable(sql, dtLogAcademicSession);

            List<LogAcademicSession> logAcademicSessions = new List<LogAcademicSession>();
            foreach (DataRow row in dtLogAcademicSession.Rows)
            {
                LogAcademicSession logAcademicSession = new LogAcademicSession();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_academic_session_id":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.log_academic_session_id = (int)row[columnName];
                            else
                                logAcademicSession.log_academic_session_id = -1;
                            break;
                        case "academic_session_id":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.academic_session_id = (int)row[columnName];
                            else
                                logAcademicSession.academic_session_id = -1;
                            break;
                        case "term_code":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.term_code = (string)row[columnName];
                            else
                                logAcademicSession.term_code = null;
                            break;
                        case "track_code":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.track_code = (string)row[columnName];
                            else
                                logAcademicSession.track_code = null;
                            break;
                        case "school_year":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.school_year = (int)row[columnName];
                            else
                                logAcademicSession.school_year = -1;
                            break;
                        case "name":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.name = (string)row[columnName];
                            else
                                logAcademicSession.name = null;
                            break;
                        case "start_date":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.start_date = (DateTime)row[columnName];
                            else
                                logAcademicSession.start_date = new DateTime();
                            break;
                        case "end_date":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.end_date = (DateTime)row[columnName];
                            else
                                logAcademicSession.end_date = new DateTime();
                            break;
                        case "enum_session_id":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.set_SessionType((int)row[columnName]);
                            else
                                logAcademicSession.SessionType = null;
                            break;
                        case "parent_session_id":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.parent_session_id = (int)row[columnName];
                            else
                                logAcademicSession.parent_session_id = -1;
                            break;
                        case "organization_id":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.organization_id = (int)row[columnName];
                            else
                                logAcademicSession.organization_id = -1;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.created = (DateTime)row[columnName];
                            else
                                logAcademicSession.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.created_by = (string)row[columnName];
                            else
                                logAcademicSession.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.changed = (DateTime)row[columnName];
                            else
                                logAcademicSession.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.changed_by = (string)row[columnName];
                            else
                                logAcademicSession.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.log_date = (DateTime)row[columnName];
                            else
                                logAcademicSession.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.log_user = (string)row[columnName];
                            else
                                logAcademicSession.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logAcademicSession.set_LogAction((int)row[columnName]);
                            else
                                logAcademicSession.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_academic_session table: " + columnName);
                    }
                }
                logAcademicSessions.Add(logAcademicSession);
            }

            return logAcademicSessions;
        }

        public List<LogOrganization> Read_LogOrganization(IConnectable connection)
        {
            string sql = "SELECT * FROM log_organization";

            DataTable dtLogOrganization = DataTableFactory.CreateDataTable_Netus2_Log_Organization();
            dtLogOrganization = connection.ReadIntoDataTable(sql, dtLogOrganization);

            List<LogOrganization> logOrgs = new List<LogOrganization>();
            foreach (DataRow row in dtLogOrganization.Rows)
            {
                LogOrganization logOrg = new LogOrganization();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_organization_id":
                            if (row[columnName] != DBNull.Value)
                                logOrg.log_organization_id = (int)row[columnName];
                            else
                                logOrg.log_organization_id = -1;
                            break;
                        case "organization_id":
                            if (row[columnName] != DBNull.Value)
                                logOrg.organization_id = (int)row[columnName];
                            else
                                logOrg.organization_id = -1;
                            break;
                        case "name":
                            if (row[columnName] != DBNull.Value)
                                logOrg.name = (string)row[columnName];
                            else
                                logOrg.name = null;
                            break;
                        case "enum_organization_id":
                            if (row[columnName] != DBNull.Value)
                                logOrg.set_OrganizationType((int)row[columnName]);
                            else
                                logOrg.OrganizationType = null;
                            break;
                        case "identifier":
                            if (row[columnName] != DBNull.Value)
                                logOrg.identifier = (string)row[columnName];
                            else
                                logOrg.identifier = null;
                            break;
                        case "sis_building_code":
                            if (row[columnName] != DBNull.Value)
                                logOrg.sis_building_code = (string)row[columnName];
                            else
                                logOrg.sis_building_code = null;
                            break;
                        case "hr_building_code":
                            if (row[columnName] != DBNull.Value)
                                logOrg.hr_building_code = (string)row[columnName];
                            else
                                logOrg.hr_building_code = null;
                            break;
                        case "organization_parent_id":
                            if (row[columnName] != DBNull.Value)
                                logOrg.organization_parent_id = (int)row[columnName];
                            else
                                logOrg.organization_parent_id = -1;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logOrg.created = (DateTime)row[columnName];
                            else
                                logOrg.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logOrg.created_by = (string)row[columnName];
                            else
                                logOrg.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logOrg.changed = (DateTime)row[columnName];
                            else
                                logOrg.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logOrg.changed_by = (string)row[columnName];
                            else
                                logOrg.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logOrg.log_date = (DateTime)row[columnName];
                            else
                                logOrg.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logOrg.log_user = (string)row[columnName];
                            else
                                logOrg.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logOrg.set_LogAction((int)row[columnName]);
                            else
                                logOrg.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_organization table: " + columnName);
                    }
                }
                logOrgs.Add(logOrg);
            }

            return logOrgs;
        }

        public List<LogResource> Read_LogResource(IConnectable connection)
        {
            string sql = "SELECT * FROM log_resource";

            DataTable dtLogResource = DataTableFactory.CreateDataTable_Netus2_Log_Resource();
            dtLogResource = connection.ReadIntoDataTable(sql, dtLogResource);

            List<LogResource> logResources = new List<LogResource>();
           foreach(DataRow row in dtLogResource.Rows)
            {
                LogResource logResource = new LogResource();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_resource_id":
                            if (row[columnName] != DBNull.Value)
                                logResource.log_resource_id = (int)row[columnName];
                            else
                                logResource.log_resource_id = -1;
                            break;
                        case "resource_id":
                            if (row[columnName] != DBNull.Value)
                                logResource.resource_id = (int)row[columnName];
                            else
                                logResource.resource_id = -1;
                            break;
                        case "name":
                            if (row[columnName] != DBNull.Value)
                                logResource.name = (string)row[columnName];
                            else
                                logResource.name = null;
                            break;
                        case "enum_importance_id":
                            if (row[columnName] != DBNull.Value)
                                logResource.set_Importance((int)row[columnName]);
                            else
                                logResource.Importance = null;
                            break;
                        case "vendor_resource_identification":
                            if (row[columnName] != DBNull.Value)
                                logResource.vendor_resource_identification = (string)row[columnName];
                            else
                                logResource.vendor_resource_identification = null;
                            break;
                        case "vendor_identification":
                            if (row[columnName] != DBNull.Value)
                                logResource.vendor_identification = (string)row[columnName];
                            else
                                logResource.vendor_identification = null;
                            break;
                        case "application_identification":
                            if (row[columnName] != DBNull.Value)
                                logResource.application_identification = (string)row[columnName];
                            else
                                logResource.application_identification = null;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logResource.created = (DateTime)row[columnName];
                            else
                                logResource.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logResource.created_by = (string)row[columnName];
                            else
                                logResource.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logResource.changed = (DateTime)row[columnName];
                            else
                                logResource.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logResource.changed_by = (string)row[columnName];
                            else
                                logResource.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logResource.log_date = (DateTime)row[columnName];
                            else
                                logResource.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logResource.log_user = (string)row[columnName];
                            else
                                logResource.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logResource.set_LogAction((int)row[columnName]);
                            else
                                logResource.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_resource table: " + columnName);
                    }
                }
                logResources.Add(logResource);
            }

            return logResources;
        }

        public List<LogCourse> Read_LogCourse(IConnectable connection)
        {
            string sql = "SELECT * FROM log_course";

            DataTable dtLogCourse = DataTableFactory.CreateDataTable_Netus2_Log_Course();
            dtLogCourse = connection.ReadIntoDataTable(sql, dtLogCourse);

            List<LogCourse> logCourses = new List<LogCourse>();
            foreach(DataRow row in dtLogCourse.Rows)
            {
                LogCourse logCourse = new LogCourse();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_course_id":
                            if (row[columnName] != DBNull.Value)
                                logCourse.log_course_id = (int)row[columnName];
                            else
                                logCourse.log_course_id = -1;
                            break;
                        case "course_id":
                            if (row[columnName] != DBNull.Value)
                                logCourse.course_id = (int)row[columnName];
                            else
                                logCourse.course_id = -1;
                            break;
                        case "name":
                            if (row[columnName] != DBNull.Value)
                                logCourse.name = (string)row[columnName];
                            else
                                logCourse.name = null;
                            break;
                        case "course_code":
                            if (row[columnName] != DBNull.Value)
                                logCourse.course_code = (string)row[columnName];
                            else
                                logCourse.course_code = null;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logCourse.created = (DateTime)row[columnName];
                            else
                                logCourse.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logCourse.created_by = (string)row[columnName];
                            else
                                logCourse.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logCourse.changed = (DateTime)row[columnName];
                            else
                                logCourse.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logCourse.changed_by = (string)row[columnName];
                            else
                                logCourse.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logCourse.log_date = (DateTime)row[columnName];
                            else
                                logCourse.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logCourse.log_user = (string)row[columnName];
                            else
                                logCourse.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logCourse.set_LogAction((int)row[columnName]);
                            else
                                logCourse.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_resource table: " + columnName);
                    }
                }
                logCourses.Add(logCourse);
            }

            return logCourses;
        }

        public List<LogJctCourseSubject> Read_LogJctCourseSubject(IConnectable connection)
        {
            string sql = "SELECT * FROM log_jct_course_subject";

            DataTable dtLogJctCourseSubject = DataTableFactory.CreateDataTable_Netus2_Log_JctCourseSubject();
            dtLogJctCourseSubject = connection.ReadIntoDataTable(sql, dtLogJctCourseSubject);

            List<LogJctCourseSubject> logJctCourseSubjects = new List<LogJctCourseSubject>();
            foreach(DataRow row in dtLogJctCourseSubject.Rows)
            {
                LogJctCourseSubject logJctCourseSubject = new LogJctCourseSubject();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_course_subject_id":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseSubject.log_jct_course_subject_id = (int)row[columnName];
                            else
                                logJctCourseSubject.log_jct_course_subject_id = -1;
                            break;
                        case "course_id":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseSubject.course_id = (int)row[columnName];
                            else
                                logJctCourseSubject.course_id = -1;
                            break;
                        case "enum_subject_id":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseSubject.set_Subject((int)row[columnName]);
                            else
                                logJctCourseSubject.Subject = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseSubject.log_date = (DateTime)row[columnName];
                            else
                                logJctCourseSubject.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseSubject.log_user = (string)row[columnName];
                            else
                                logJctCourseSubject.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseSubject.set_LogAction((int)row[columnName]);
                            else
                                logJctCourseSubject.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_course_subject table: " + columnName);
                    }
                }
                logJctCourseSubjects.Add(logJctCourseSubject);
            }

            return logJctCourseSubjects;
        }

        public List<LogJctCourseGrade> Read_LogJctCourseGrade(IConnectable connection)
        {
            string sql = "SELECT * FROM log_jct_course_grade";

            DataTable dtLogJctCourseGrade = DataTableFactory.CreateDataTable_Netus2_Log_JctCourseGrade();
            dtLogJctCourseGrade = connection.ReadIntoDataTable(sql, dtLogJctCourseGrade);

            List<LogJctCourseGrade> logJctCourseGrades = new List<LogJctCourseGrade>();
            foreach(DataRow row in dtLogJctCourseGrade.Rows)
            {
                LogJctCourseGrade logJctCourseGrade = new LogJctCourseGrade();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_course_grade_id":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseGrade.log_jct_course_grade_id = (int)row[columnName];
                            else
                                logJctCourseGrade.log_jct_course_grade_id = -1;
                            break;
                        case "course_id":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseGrade.course_id = (int)row[columnName];
                            else
                                logJctCourseGrade.course_id = -1;
                            break;
                        case "enum_grade_id":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseGrade.set_Grade((int)row[columnName]);
                            else
                                logJctCourseGrade.Grade = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseGrade.log_date = (DateTime)row[columnName];
                            else
                                logJctCourseGrade.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseGrade.log_user = (string)row[columnName];
                            else
                                logJctCourseGrade.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctCourseGrade.set_LogAction((int)row[columnName]);
                            else
                                logJctCourseGrade.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_course_grade table: " + columnName);
                    }
                }
                logJctCourseGrades.Add(logJctCourseGrade);
            }

            return logJctCourseGrades;
        }

        public List<LogClassEnrolled> Read_LogClassEnrolled(IConnectable connection)
        {
            string sql = "SELECT * FROM log_class_enrolled";

            DataTable dtLogClassEnrolled = DataTableFactory.CreateDataTable_Netus2_Log_ClassEnrolled();
            dtLogClassEnrolled = connection.ReadIntoDataTable(sql, dtLogClassEnrolled);

            List<LogClassEnrolled> logClasssEnrolled = new List<LogClassEnrolled>();
            foreach(DataRow row in dtLogClassEnrolled.Rows)
            {
                LogClassEnrolled logClassEnrolled = new LogClassEnrolled();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_class_enrolled_id":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.log_class_enrolled_id = (int)row[columnName];
                            else
                                logClassEnrolled.log_class_enrolled_id = -1;
                            break;
                        case "class_enrolled_id":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.class_enrolled_id = (int)row[columnName];
                            else
                                logClassEnrolled.class_enrolled_id = -1;
                            break;
                        case "name":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.name = (string)row[columnName];
                            else
                                logClassEnrolled.name = null;
                            break;
                        case "class_enrolled_code":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.class_enrolled_code = (string)row[columnName];
                            else
                                logClassEnrolled.class_enrolled_code = null;
                            break;
                        case "enum_class_enrolled_id":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.set_ClassType((int)row[columnName]);
                            else
                                logClassEnrolled.ClassType = null;
                            break;
                        case "room":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.room = (string)row[columnName];
                            else
                                logClassEnrolled.room = null;
                            break;
                        case "course_id":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.course_id = (int)row[columnName];
                            else
                                logClassEnrolled.course_id = -1;
                            break;
                        case "academic_session_id":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.academic_session_id = (int)row[columnName];
                            else
                                logClassEnrolled.academic_session_id = -1;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.created = (DateTime)row[columnName];
                            else
                                logClassEnrolled.created = new DateTime();
                            break;
                        case "created_by":
                            logClassEnrolled.created_by = row[columnName] != DBNull.Value ? (string)row[columnName] : null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.changed = (DateTime)row[columnName];
                            else
                                logClassEnrolled.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.changed_by = (string)row[columnName];
                            else
                                logClassEnrolled.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.log_date = (DateTime)row[columnName];
                            else
                                logClassEnrolled.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.log_user = (string)row[columnName];
                            else
                                logClassEnrolled.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.set_LogAction((int)row[columnName]);
                            else
                                logClassEnrolled.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_resource table: " + columnName);
                    }
                }
                logClasssEnrolled.Add(logClassEnrolled);
            }

            return logClasssEnrolled;
        }

        public List<LogJctClassPeriod> Read_LogJctClassPeriod(IConnectable connection)
        {
            string sql = "SELECT * FROM log_jct_class_enrolled_period";

            DataTable dtLogJctClassPeriod = DataTableFactory.CreateDataTable_Netus2_Log_JctClassEnrolledPeriod();
            dtLogJctClassPeriod = connection.ReadIntoDataTable(sql, dtLogJctClassPeriod);

            List<LogJctClassPeriod> logJctClassPeriods = new List<LogJctClassPeriod>();
            foreach(DataRow row in dtLogJctClassPeriod.Rows)
            {
                LogJctClassPeriod logJctClassPeriod = new LogJctClassPeriod();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_class_enrolled_period_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPeriod.log_jct_class_period_id = (int)row[columnName];
                            else
                                logJctClassPeriod.log_jct_class_period_id = -1;
                            break;
                        case "class_enrolled_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPeriod.class_id = (int)row[columnName];
                            else
                                logJctClassPeriod.class_id = -1;
                            break;
                        case "enum_period_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPeriod.set_Period((int)row[columnName]);
                            else
                                logJctClassPeriod.Period = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPeriod.log_date = (DateTime)row[columnName];
                            else
                                logJctClassPeriod.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPeriod.log_user = (string)row[columnName];
                            else
                                logJctClassPeriod.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPeriod.set_LogAction((int)row[columnName]);
                            else
                                logJctClassPeriod.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_class_enrolled_period table: " + columnName);
                    }
                }
                logJctClassPeriods.Add(logJctClassPeriod);
            }

            return logJctClassPeriods;
        }

        public List<LogJctClassResource> Read_LogJctClassResource(IConnectable connection)
        {
            string sql = "SELECT * FROM log_jct_class_enrolled_resource";

            DataTable dtLogJctClassResource = DataTableFactory.CreateDataTable_Netus2_Log_JctClassEnrolledResource();
            dtLogJctClassResource = connection.ReadIntoDataTable(sql, dtLogJctClassResource);

            List<LogJctClassResource> logJctClassResources = new List<LogJctClassResource>();
            foreach (DataRow row in dtLogJctClassResource.Rows)
            {
                LogJctClassResource logJctClassResource = new LogJctClassResource();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_class_enrolled_resource_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassResource.log_jct_class_resource_id = (int)row[columnName];
                            else
                                logJctClassResource.log_jct_class_resource_id = -1;
                            break;
                        case "class_enrolled_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassResource.class_id = (int)row[columnName];
                            else
                                logJctClassResource.class_id = -1;
                            break;
                        case "resource_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassResource.resource_id = (int)row[columnName];
                            else
                                logJctClassResource.resource_id = -1;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctClassResource.log_date = (DateTime)row[columnName];
                            else
                                logJctClassResource.log_date = new DateTime();
                            break;
                        case "log_user":
                            logJctClassResource.log_user = row[columnName] != DBNull.Value ? (string)row[columnName] : null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassResource.set_LogAction((int)row[columnName]);
                            else
                                logJctClassResource.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_class_resource table: " + columnName);
                    }
                }
                logJctClassResources.Add(logJctClassResource);
            }

            return logJctClassResources;
        }

        public List<LogLineItem> Read_LogLineItem(IConnectable connection)
        {
            string sql = "SELECT * FROM log_lineitem";

            DataTable dtLogLineItem = DataTableFactory.CreateDataTable_Netus2_Log_LineItem();
            dtLogLineItem = connection.ReadIntoDataTable(sql, dtLogLineItem);

            List<LogLineItem> logLineItems = new List<LogLineItem>();
            foreach(DataRow row in dtLogLineItem.Rows)
            {
                LogLineItem logLineItem = new LogLineItem();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_lineitem_id":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.log_lineitem_id = (int)row[columnName];
                            else
                                logLineItem.log_lineitem_id = -1;
                            break;
                        case "lineitem_id":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.lineitem_id = (int)row[columnName];
                            else
                                logLineItem.lineitem_id = -1;
                            break;
                        case "name":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.name = (string)row[columnName];
                            else
                                logLineItem.name = null;
                            break;
                        case "descript":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.descript = (string)row[columnName];
                            else
                                logLineItem.descript = null;
                            break;
                        case "assign_date":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.assign_date = (DateTime)row[columnName];
                            else
                                logLineItem.assign_date = new DateTime();
                            break;
                        case "due_date":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.due_date = (DateTime)row[columnName];
                            else
                                logLineItem.due_date = new DateTime();
                            break;
                        case "class_enrolled_id":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.class_id = (int)row[columnName];
                            else
                                logLineItem.class_id = -1;
                            break;
                        case "enum_category_id":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.set_Category((int)row[columnName]);
                            else
                                logLineItem.Category = null;
                            break;
                        case "mark_min":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.markValueMin = (double)row[columnName];
                            else
                                logLineItem.markValueMin = null;
                            break;
                        case "mark_max":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.markValueMax = (double)row[columnName];
                            else
                                logLineItem.markValueMax = null;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.created = (DateTime)row[columnName];
                            else
                                logLineItem.created = new DateTime();
                            break;
                        case "created_by":
                            logLineItem.created_by = row[columnName] != DBNull.Value ? (string)row[columnName] : null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.changed = (DateTime)row[columnName];
                            else
                                logLineItem.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.changed_by = (string)row[columnName];
                            else
                                logLineItem.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.log_date = (DateTime)row[columnName];
                            else
                                logLineItem.log_date = new DateTime();
                            break;
                        case "log_user":
                            logLineItem.log_user = row[columnName] != DBNull.Value ? (string)row[columnName] : null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.set_LogAction((int)row[columnName]);
                            else
                                logLineItem.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_lineitem table: " + columnName);
                    }
                }
                logLineItems.Add(logLineItem);
            }

            return logLineItems;
        }

        public List<LogEnrollment> Read_LogEnrollment(IConnectable connection)
        {
            string sql = "SELECT * FROM log_enrollment";

            DataTable dtLogEnrollment = DataTableFactory.CreateDataTable_Netus2_Log_Enrollment();
            dtLogEnrollment = connection.ReadIntoDataTable(sql, dtLogEnrollment);

            List<LogEnrollment> logEnrollments = new List<LogEnrollment>();
            foreach(DataRow row in dtLogEnrollment.Rows)
            {
                LogEnrollment logEnrollment = new LogEnrollment();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_enrollment_id":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.log_enrollment_id = (int)row[columnName];
                            else
                                logEnrollment.log_enrollment_id = -1;
                            break;
                        case "enrollment_id":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.enrollment_id = (int)row[columnName];
                            else
                                logEnrollment.enrollment_id = -1;
                            break;
                        case "person_id":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.person_id = (int)row[columnName];
                            else
                                logEnrollment.person_id = -1;
                            break;
                        case "academic_session_id":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.academic_session_id = (int)row[columnName];
                            else
                                logEnrollment.academic_session_id = -1;
                            break;
                        case "enum_grade_id":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.set_GradeLevel((int)row[columnName]);
                            else
                                logEnrollment.GradeLevel = null;
                            break;
                        case "start_date":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.start_date = (DateTime)row[columnName];
                            else
                                logEnrollment.start_date = new DateTime();
                            break;
                        case "end_date":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.end_date = (DateTime)row[columnName];
                            else
                                logEnrollment.end_date = new DateTime();
                            break;
                        case "is_primary":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.IsPrimary = (bool)row[columnName];
                            else
                                logEnrollment.IsPrimary = false;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.created = (DateTime)row[columnName];
                            else
                                logEnrollment.created = new DateTime();
                            break;
                        case "created_by":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.created_by = (string)row[columnName];
                            else
                                logEnrollment.created_by = null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.changed = (DateTime)row[columnName];
                            else
                                logEnrollment.changed = new DateTime();
                            break;
                        case "changed_by":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.changed_by = (string)row[columnName];
                            else
                                logEnrollment.changed_by = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.log_date = (DateTime)row[columnName];
                            else
                                logEnrollment.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.log_user = (string)row[columnName];
                            else
                                logEnrollment.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.set_LogAction((int)row[columnName]);
                            else
                                logEnrollment.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_enrollment table: " + columnName);
                    }
                }
                logEnrollments.Add(logEnrollment);
            }

            return logEnrollments;
        }

        public List<LogMark> Read_LogMark(IConnectable connection)
        {
            string sql = "SELECT * FROM log_mark";

            DataTable dtLogMark = DataTableFactory.CreateDataTable_Netus2_Log_Mark();
            dtLogMark = connection.ReadIntoDataTable(sql, dtLogMark);

            List<LogMark> logMarks = new List<LogMark>();
            foreach(DataRow row in dtLogMark.Rows)
            {
                LogMark logMark = new LogMark();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_mark_id":
                            if (row[columnName] != DBNull.Value)
                                logMark.log_mark_id = (int)row[columnName];
                            else
                                logMark.log_mark_id = -1;
                            break;
                        case "mark_id":
                            if (row[columnName] != DBNull.Value)
                                logMark.mark_id = (int)row[columnName];
                            else
                                logMark.mark_id = -1;
                            break;
                        case "lineitem_id":
                            if (row[columnName] != DBNull.Value)
                                logMark.lineitem_id = (int)row[columnName];
                            else
                                logMark.lineitem_id = -1;
                            break;
                        case "person_id":
                            if (row[columnName] != DBNull.Value)
                                logMark.person_id = (int)row[columnName];
                            else
                                logMark.person_id = -1;
                            break;
                        case "enum_score_status_id":
                            if (row[columnName] != DBNull.Value)
                                logMark.set_ScoreStatus((int)row[columnName]);
                            else
                                logMark.ScoreStatus = null;
                            break;
                        case "score":
                            if (row[columnName] != DBNull.Value)
                                logMark.score = (double)row[columnName];
                            else
                                logMark.score = null;
                            break;
                        case "score_date":
                            if (row[columnName] != DBNull.Value)
                                logMark.score_date = (DateTime)row[columnName];
                            else
                                logMark.score_date = new DateTime();
                            break;
                        case "comment":
                            if (row[columnName] != DBNull.Value)
                                logMark.comment = (string)row[columnName];
                            else
                                logMark.comment = null;
                            break;
                        case "created":
                            if (row[columnName] != DBNull.Value)
                                logMark.created = (DateTime)row[columnName];
                            else
                                logMark.created = new DateTime();
                            break;
                        case "created_by":
                            logMark.created_by = row[columnName] != DBNull.Value ? (string)row[columnName] : null;
                            break;
                        case "changed":
                            if (row[columnName] != DBNull.Value)
                                logMark.changed = (DateTime)row[columnName];
                            else
                                logMark.changed = new DateTime();
                            break;
                        case "changed_by":
                            logMark.changed_by = row[columnName] != DBNull.Value ? (string)row[columnName] : null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logMark.log_date = (DateTime)row[columnName];
                            else
                                logMark.log_date = new DateTime();
                            break;
                        case "log_user":
                            logMark.log_user = row[columnName] != DBNull.Value ? (string)row[columnName] : null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logMark.set_LogAction((int)row[columnName]);
                            else
                                logMark.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_mark table: " + columnName);
                    }
                }
                logMarks.Add(logMark);
            }

            return logMarks;
        }

        public List<LogJctEnrollmentClassEnrolled> Read_JctEnrollmentClassEnrolled(IConnectable connection)
        {
            string sql = "SELECT * FROM log_jct_enrollment_class_enrolled";

            DataTable dtLogJctEnrollmentClassEnrolled = DataTableFactory.CreateDataTable_Netus2_Log_JctEnrollmentClassEnrolled();
            dtLogJctEnrollmentClassEnrolled = connection.ReadIntoDataTable(sql, dtLogJctEnrollmentClassEnrolled);

            List<LogJctEnrollmentClassEnrolled> logJctEnrollmentClassEnrolleds = new List<LogJctEnrollmentClassEnrolled>();
            foreach(DataRow row in dtLogJctEnrollmentClassEnrolled.Rows)
            {
                LogJctEnrollmentClassEnrolled logJctEnrollmentClassEnrolled = new LogJctEnrollmentClassEnrolled();
                foreach (DataColumn colum in row.Table.Columns)
                {
                    string columnName = colum.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_enrollment_class_enrolled_id":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentClassEnrolled.log_jct_enrollment_class_enrolled_id = (int)row[columnName];
                            else
                                logJctEnrollmentClassEnrolled.log_jct_enrollment_class_enrolled_id = -1;
                            break;
                        case "enrollment_id":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentClassEnrolled.enrollment_id = (int)row[columnName];
                            else
                                logJctEnrollmentClassEnrolled.enrollment_id = -1;
                            break;
                        case "class_enrolled_id":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentClassEnrolled.class_enrolled_id = (int)row[columnName];
                            else
                                logJctEnrollmentClassEnrolled.class_enrolled_id = -1;
                            break;
                        case "enrollment_start_date":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentClassEnrolled.enrollment_start_date = (DateTime)row[columnName];
                            else
                                logJctEnrollmentClassEnrolled.enrollment_start_date = null;
                            break;
                        case "enrollment_end_date":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentClassEnrolled.enrollment_end_date = (DateTime)row[columnName];
                            else
                                logJctEnrollmentClassEnrolled.enrollment_end_date = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentClassEnrolled.log_date = (DateTime)row[columnName];
                            else
                                logJctEnrollmentClassEnrolled.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentClassEnrolled.log_user = (string)row[columnName];
                            else
                                logJctEnrollmentClassEnrolled.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentClassEnrolled.set_LogAction((int)row[columnName]);
                            else
                                logJctEnrollmentClassEnrolled.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_enrollment_class_enrolled table: " + columnName);
                    }
                }
                logJctEnrollmentClassEnrolleds.Add(logJctEnrollmentClassEnrolled);
            }

            return logJctEnrollmentClassEnrolleds;
        }
    }
}