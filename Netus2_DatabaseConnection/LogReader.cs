using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.logObjects;
using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection
{
    public class LogReader
    {
        public List<LogPerson> Read_LogPerson(IConnectable connection)
        {
            DataTable dtLogPerson = new DataTableFactory().Dt_Netus2_Log_Person;

            string sql = "SELECT * FROM log_person";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogPerson.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
            DataTable dtLogJctpersonRole = new DataTableFactory().Dt_Netus2_Log_JctPersonRole;

            string sql = "SELECT * FROM log_jct_person_role";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogJctpersonRole.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
            DataTable dtLogJctPersonPerson = new DataTableFactory().Dt_Netus2_Log_JctPersonPerson;

            string sql = "SELECT * FROM log_jct_person_person";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogJctPersonPerson.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
            DataTable dtLogUniqueIdentifier = new DataTableFactory().Dt_Netus2_Log_UniqueIdentifier;

            string sql = "SELECT * FROM log_unique_identifier";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogUniqueIdentifier.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
                        case "unique_identifier":
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
                        case "is_active_id":
                            if (row[columnName] != DBNull.Value)
                                logUniqueIdentifier.IsActive = Enum_True_False.GetEnumFromId((int)row[columnName]);
                            else
                                logUniqueIdentifier.IsActive = null;
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
            DataTable dtLogProvider = new DataTableFactory().Dt_Netus2_Log_Provider;

            string sql = "SELECT * FROM log_provider";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogProvider.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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

        public List<LogApp> Read_LogApp(IConnectable connection)
        {
            DataTable dtLogApp = new DataTableFactory().Dt_Netus2_Log_Application;

            string sql = "SELECT * FROM log_app";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogApp.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<LogApp> logApps = new List<LogApp>();
            foreach (DataRow row in dtLogApp.Rows)
            {
                LogApp logApp = new LogApp();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_app_id":
                            if (row[columnName] != DBNull.Value)
                                logApp.log_app_id = (int)row[columnName];
                            else
                                logApp.log_app_id = -1;
                            break;
                        case "app_id":
                            if (row[columnName] != DBNull.Value)
                                logApp.app_id = (int)row[columnName];
                            else
                                logApp.app_id = -1;
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
                            throw new Exception("Unexpected column found in log_app table: " + columnName);
                    }
                }
                logApps.Add(logApp);
            }

            return logApps;
        }

        public List<LogJctPersonApp> Read_LogJctPersonApp(IConnectable connection)
        {
            DataTable dtLogJctPersonApp = new DataTableFactory().Dt_Netus2_Log_JctPersonApp;

            string sql = "SELECT * FROM log_jct_person_app";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogJctPersonApp.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<LogJctPersonApp> logJctPersonApps = new List<LogJctPersonApp>();
            foreach (DataRow row in dtLogJctPersonApp.Rows)
            {
                LogJctPersonApp logJctPersonApp = new LogJctPersonApp();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_person_app_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApp.log_jct_person_app_id = (int)row[columnName];
                            else
                                logJctPersonApp.log_jct_person_app_id = -1;
                            break;
                        case "person_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApp.person_id = (int)row[columnName];
                            else
                                logJctPersonApp.person_id = -1;
                            break;
                        case "app_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApp.app_id = (int)row[columnName];
                            else
                                logJctPersonApp.app_id = -1;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApp.log_date = (DateTime)row[columnName];
                            else
                                logJctPersonApp.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApp.log_user = (string)row[columnName];
                            else
                                logJctPersonApp.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctPersonApp.set_LogAction((int)row[columnName]);
                            else
                                logJctPersonApp.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_person_app table: " + columnName);
                    }
                }
                logJctPersonApps.Add(logJctPersonApp);
            }

            return logJctPersonApps;
        }

        public List<LogJctClassPerson> Read_LogJctClassPerson(IConnectable connection)
        {
            DataTable dtLogJctClassPerson = new DataTableFactory().Dt_Netus2_Log_JctClassPerson;

            string sql = "SELECT * FROM log_jct_class_person";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogJctClassPerson.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<LogJctClassPerson> logJctClassPersonDaos = new List<LogJctClassPerson>();
            foreach (DataRow row in dtLogJctClassPerson.Rows)
            {
                LogJctClassPerson logJctClassPerson = new LogJctClassPerson();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_class_person_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPerson.log_jct_class_person_id = (int)row[columnName];
                            else
                                logJctClassPerson.log_jct_class_person_id = -1;
                            break;
                        case "class_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPerson.class_id = (int)row[columnName];
                            else
                                logJctClassPerson.class_id = -1;
                            break;
                        case "person_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPerson.person_id = (int)row[columnName];
                            else
                                logJctClassPerson.person_id = -1;
                            break;
                        case "enum_role_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPerson.set_Role((int)row[columnName]);
                            else
                                logJctClassPerson.Role = null;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPerson.log_date = (DateTime)row[columnName];
                            else
                                logJctClassPerson = null;
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPerson.log_user = (string)row[columnName];
                            else
                                logJctClassPerson.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPerson.set_LogAction((int)row[columnName]);
                            else
                                logJctClassPerson.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_class_person table: " + columnName);
                    }
                }
                logJctClassPersonDaos.Add(logJctClassPerson);
            }

            return logJctClassPersonDaos;
        }

        public List<LogPhoneNumber> Read_LogPhoneNumber(IConnectable connection)
        {
            DataTable dtLogPhoneNumber = new DataTableFactory().Dt_Netus2_Log_PhoneNumber;

            string sql = "SELECT * FROM log_phone_number";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogPhoneNumber.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
                        case "person_id":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.person_id = (int)row[columnName];
                            else
                                logPhoneNumber.person_id = -1;
                            break;
                        case "phone_number":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.phone_number = (string)row[columnName];
                            else
                                logPhoneNumber.phone_number = null;
                            break;
                        case "is_primary_id":
                            if (row[columnName] != DBNull.Value)
                                logPhoneNumber.set_IsPrimary((int)row[columnName]);
                            else
                                logPhoneNumber.IsPrimary = null;
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
            DataTable dtLogAddress = new DataTableFactory().Dt_Netus2_Log_Address;

            string sql = "SELECT * FROM log_address";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogAddress.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
                        case "is_current_id":
                            if (row[columnName] != DBNull.Value)
                                logAddress.set_IsCurrent((int)row[columnName]);
                            else
                                logAddress.IsCurrent = null;
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
            DataTable dtLogJctPersonAddress = new DataTableFactory().Dt_Netus2_Log_JctPersonAddress;

            string sql = "SELECT * FROM log_jct_person_address";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogJctPersonAddress.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<LogJctPersonAddress> logJctPersonAddresss = new List<LogJctPersonAddress>();
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
                logJctPersonAddresss.Add(logJctPersonAddress);
            }

            return logJctPersonAddresss;
        }

        public List<LogEmploymentSession> Read_LogEmploymentSession(IConnectable connection)
        {
            DataTable dtLogEmploymentSession = new DataTableFactory().Dt_Netus2_Log_EmploymentSession;

            string sql = "SELECT * FROM log_employment_session";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogEmploymentSession.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
                        case "is_primary_id":
                            if (row[columnName] != DBNull.Value)
                                logEmploymentSession.set_IsPrimary((int)row[columnName]);
                            else
                                logEmploymentSession.IsPrimary = null;
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
            DataTable dtLogAcademicSession = new DataTableFactory().Dt_Netus2_Log_AcademicSession;

            string sql = "SELECT * FROM log_academic_session";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogAcademicSession.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
            DataTable dtLogOrganization = new DataTableFactory().Dt_Netus2_Log_Organization;

            string sql = "SELECT * FROM log_organization";
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogOrganization.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
                        case "building_code":
                            if (row[columnName] != DBNull.Value)
                                logOrg.building_code = (string)row[columnName];
                            else
                                logOrg.building_code = null;
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
            DataTable dtLogResource = new DataTableFactory().Dt_Netus2_Log_Resource;

            string sql = "SELECT * FROM log_resource";
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogResource.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
            DataTable dtLogCourse = new DataTableFactory().Dt_Netus2_Log_Course;

            string sql = "SELECT * FROM log_course";
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogCourse.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
            DataTable dtLogJctCourseSubject = new DataTableFactory().Dt_Netus2_Log_JctCourseSubject;

            string sql = "SELECT * FROM log_jct_course_subject";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogJctCourseSubject.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
            DataTable dtLogJctCourseGrade = new DataTableFactory().Dt_Netus2_Log_JctCourseGrade;

            string sql = "SELECT * FROM log_jct_course_grade";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogJctCourseGrade.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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

        public List<LogClass> Read_LogClassEnrolled(IConnectable connection)
        {
            DataTable dtLogClassEnrolled = new DataTableFactory().Dt_Netus2_Log_ClassEnrolled;

            string sql = "SELECT * FROM log_class";
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogClassEnrolled.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<LogClass> logClasssEnrolled = new List<LogClass>();
            foreach(DataRow row in dtLogClassEnrolled.Rows)
            {
                LogClass logClassEnrolled = new LogClass();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_class_id":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.log_class_id = (int)row[columnName];
                            else
                                logClassEnrolled.log_class_id = -1;
                            break;
                        case "class_id":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.class_id = (int)row[columnName];
                            else
                                logClassEnrolled.class_id = -1;
                            break;
                        case "name":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.name = (string)row[columnName];
                            else
                                logClassEnrolled.name = null;
                            break;
                        case "class_code":
                            if (row[columnName] != DBNull.Value)
                                logClassEnrolled.class_code = (string)row[columnName];
                            else
                                logClassEnrolled.class_code = null;
                            break;
                        case "enum_class_id":
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
            DataTable dtLogJctClassPeriod = new DataTableFactory().Dt_Netus2_Log_JctClassPeriod;

            string sql = "SELECT * FROM log_jct_class_period";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogJctClassPeriod.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<LogJctClassPeriod> logJctClassPeriods = new List<LogJctClassPeriod>();
            foreach(DataRow row in dtLogJctClassPeriod.Rows)
            {
                LogJctClassPeriod logJctClassPeriod = new LogJctClassPeriod();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_class_period_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassPeriod.log_jct_class_period_id = (int)row[columnName];
                            else
                                logJctClassPeriod.log_jct_class_period_id = -1;
                            break;
                        case "class_id":
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
                            throw new Exception("Unexpected column found in log_jct_class_period table: " + columnName);
                    }
                }
                logJctClassPeriods.Add(logJctClassPeriod);
            }

            return logJctClassPeriods;
        }

        public List<LogJctClassResource> Read_LogJctClassResource(IConnectable connection)
        {
            DataTable dtLogJctClassResource = new DataTableFactory().Dt_Netus2_Log_JctClassResource;

            string sql = "SELECT * FROM log_jct_class_resource";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogJctClassResource.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<LogJctClassResource> logJctClassResources = new List<LogJctClassResource>();
            foreach (DataRow row in dtLogJctClassResource.Rows)
            {
                LogJctClassResource logJctClassResource = new LogJctClassResource();
                foreach (DataColumn column in row.Table.Columns)
                {
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_class_resource_id":
                            if (row[columnName] != DBNull.Value)
                                logJctClassResource.log_jct_class_resource_id = (int)row[columnName];
                            else
                                logJctClassResource.log_jct_class_resource_id = -1;
                            break;
                        case "class_id":
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
            DataTable dtLogLineItem = new DataTableFactory().Dt_Netus2_Log_LineItem;

            string sql = "SELECT * FROM log_lineitem";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogLineItem.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
                                logLineItem = null;
                            break;
                        case "class_id":
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
                        case "markValueMin":
                            if (row[columnName] != DBNull.Value)
                                logLineItem.markValueMin = (double)row[columnName];
                            else
                                logLineItem.markValueMin = null;
                            break;
                        case "markValueMax":
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
            DataTable dtLogEnrollment = new DataTableFactory().Dt_Netus2_Log_Enrollment;

            string sql = "SELECT * FROM log_enrollment";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogEnrollment.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
                        case "class_id":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.class_id = (int)row[columnName];
                            else
                                logEnrollment.class_id = -1;
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
                        case "is_primary_id":
                            if (row[columnName] != DBNull.Value)
                                logEnrollment.set_IsPrimary((int)row[columnName]);
                            else
                                logEnrollment.IsPrimary = null;
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
            DataTable dtLogMark = new DataTableFactory().Dt_Netus2_Log_Mark;

            string sql = "SELECT * FROM log_mark";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogMark.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

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
                            logMark.comment = row[columnName] != DBNull.Value ? (string)row[columnName] : null;
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

        public List<LogJctEnrollmentAcademicSession> Read_JctEnrollmentAcademicSession(IConnectable connection)
        {
            DataTable dtLogJctEnrollmentAcademicSession = new DataTableFactory().Dt_Netus2_Log_JctEnrollmentAcademicSession;

            string sql = "SELECT * FROM log_jct_enrollment_academic_session";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                dtLogJctEnrollmentAcademicSession.Load(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            List<LogJctEnrollmentAcademicSession> logJctEnrollmentAcademicSessions = new List<LogJctEnrollmentAcademicSession>();
            foreach(DataRow row in dtLogJctEnrollmentAcademicSession.Rows)
            {
                LogJctEnrollmentAcademicSession logJctEnrollmentAcademicSession = new LogJctEnrollmentAcademicSession();
                foreach (DataColumn colum in row.Table.Columns)
                {
                    string columnName = colum.ColumnName;
                    switch (columnName)
                    {
                        case "log_jct_enrollment_academic_session_id":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentAcademicSession.log_jct_enrollment_academic_session_id = (int)row[columnName];
                            else
                                logJctEnrollmentAcademicSession.log_jct_enrollment_academic_session_id = -1;
                            break;
                        case "enrollment_id":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentAcademicSession.enrollment_id = (int)row[columnName];
                            else
                                logJctEnrollmentAcademicSession.enrollment_id = -1;
                            break;
                        case "academic_session_id":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentAcademicSession.academic_session_id = (int)row[columnName];
                            else
                                logJctEnrollmentAcademicSession.academic_session_id = -1;
                            break;
                        case "log_date":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentAcademicSession.log_date = (DateTime)row[columnName];
                            else
                                logJctEnrollmentAcademicSession.log_date = new DateTime();
                            break;
                        case "log_user":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentAcademicSession.log_user = (string)row[columnName];
                            else
                                logJctEnrollmentAcademicSession.log_user = null;
                            break;
                        case "enum_log_action_id":
                            if (row[columnName] != DBNull.Value)
                                logJctEnrollmentAcademicSession.set_LogAction((int)row[columnName]);
                            else
                                logJctEnrollmentAcademicSession.LogAction = null;
                            break;
                        default:
                            throw new Exception("Unexpected column found in log_jct_enrollment_academic_session table: " + columnName);
                    }
                }
                logJctEnrollmentAcademicSessions.Add(logJctEnrollmentAcademicSession);
            }

            return logJctEnrollmentAcademicSessions;
        }
    }
}