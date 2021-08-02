using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.logObjects;
using System;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection
{
    public class LogReader
    {
        public List<LogPerson> Read_LogPerson(IConnectable connection)
        {
            List<LogPerson> logPeople = new List<LogPerson>();

            string sql = "SELECT * FROM log_person";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogPerson logPerson = new LogPerson();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logPerson.log_person_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logPerson.person_id = (int)value;
                                else
                                    logPerson.person_id = null;
                                break;
                            case 2:
                                logPerson.first_name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                logPerson.middle_name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 4:
                                logPerson.last_name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logPerson.birth_date = (DateTime)value;
                                else
                                    logPerson.birth_date = null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                {
                                    logPerson.set_Gender((int)value);
                                }
                                else
                                    logPerson.Gender = null;
                                break;
                            case 7:
                                if (value != DBNull.Value)
                                    logPerson.set_Ethnic((int)value);
                                else
                                    logPerson.Ethnic = null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logPerson.set_ResidenceStatus((int)value);
                                else
                                    logPerson.ResidenceStatus = null;
                                break;
                            case 9:
                                logPerson.login_name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 10:
                                logPerson.login_pw = value != DBNull.Value ? (string)value : null;
                                break;
                            case 11:
                                if (value != DBNull.Value)
                                    logPerson.created = (DateTime)value;
                                else
                                    logPerson.created = null;
                                break;
                            case 12:
                                logPerson.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 13:
                                if (value != DBNull.Value)
                                    logPerson.changed = (DateTime)value;
                                else
                                    logPerson.changed = null;
                                break;
                            case 14:
                                logPerson.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 15:
                                if (value != DBNull.Value)
                                    logPerson.log_date = (DateTime)value;
                                else
                                    logPerson.log_date = null;
                                break;
                            case 16:
                                logPerson.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 17:
                                if (value != DBNull.Value)
                                    logPerson.set_LogAction((int)value);
                                else
                                    logPerson.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_person table: " + reader.GetName(i));
                        }
                    }
                    logPeople.Add(logPerson);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logPeople;
        }

        public List<LogJctPersonRole> Read_LogJctPersonRole(IConnectable connection)
        {
            List<LogJctPersonRole> logJctPersonRoles = new List<LogJctPersonRole>();

            string sql = "SELECT * FROM log_jct_person_role";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogJctPersonRole logJctPersonRole = new LogJctPersonRole();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logJctPersonRole.log_jct_person_role_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logJctPersonRole.person_id = (int)value;
                                else
                                    logJctPersonRole.person_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logJctPersonRole.set_Role((int)value);
                                else
                                    logJctPersonRole.Role = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logJctPersonRole.log_date = (DateTime)value;
                                else
                                    logJctPersonRole.log_date = null;
                                break;
                            case 4:
                                logJctPersonRole.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logJctPersonRole.set_LogAction((int)value);
                                else
                                    logJctPersonRole.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_jct_person_role table: " + reader.GetName(i));
                        }
                    }
                    logJctPersonRoles.Add(logJctPersonRole);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logJctPersonRoles;
        }

        public List<LogJctPersonPerson> Read_LogJctPersonPerson(IConnectable connection)
        {
            List<LogJctPersonPerson> logJctPersonPersons = new List<LogJctPersonPerson>();

            string sql = "SELECT * FROM log_jct_person_person";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogJctPersonPerson logJctPersonPerson = new LogJctPersonPerson();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logJctPersonPerson.log_jct_person_person_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logJctPersonPerson.person_one_id = (int)value;
                                else
                                    logJctPersonPerson.person_one_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logJctPersonPerson.person_two_id = (int)value;
                                else
                                    logJctPersonPerson.person_two_id = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logJctPersonPerson.log_date = (DateTime)value;
                                else
                                    logJctPersonPerson.log_date = null;
                                break;
                            case 4:
                                logJctPersonPerson.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logJctPersonPerson.set_LogAction((int)value);
                                else
                                    logJctPersonPerson.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_jct_person_person table: " + reader.GetName(i));
                        }
                    }
                    logJctPersonPersons.Add(logJctPersonPerson);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logJctPersonPersons;
        }

        public List<LogUniqueIdentifier> Read_LogUniqueIdentifier(IConnectable connection)
        {
            List<LogUniqueIdentifier> logUniqueIdentifiers = new List<LogUniqueIdentifier>();

            string sql = "SELECT * FROM log_unique_identifier";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogUniqueIdentifier logUniqueIdentifier = new LogUniqueIdentifier();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logUniqueIdentifier.log_unique_identifier_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logUniqueIdentifier.unique_identifier_id = (int)value;
                                else
                                    logUniqueIdentifier.unique_identifier_id = null;
                                    break;
                            case 2:
                                if (value != DBNull.Value)
                                    logUniqueIdentifier.person_id = (int)value;
                                else
                                    logUniqueIdentifier.person_id = null;
                                break;
                            case 3:
                                logUniqueIdentifier.unique_identifier = value != DBNull.Value ? (string)value : null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    logUniqueIdentifier.IdentifierType = Enum_Identifier.GetEnumFromId((int)value);
                                else
                                    logUniqueIdentifier.IdentifierType = null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logUniqueIdentifier.IsActive = Enum_True_False.GetEnumFromId((int)value);
                                else
                                    logUniqueIdentifier.IsActive = null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logUniqueIdentifier.created = (DateTime)value;
                                else
                                    logUniqueIdentifier.created = null;
                                break;
                            case 7:
                                logUniqueIdentifier.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logUniqueIdentifier.changed = (DateTime)value;
                                else
                                    logUniqueIdentifier.changed = null;
                                break;
                            case 9:
                                logUniqueIdentifier.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    logUniqueIdentifier.log_date = (DateTime)value;
                                else
                                    logUniqueIdentifier.log_date = null;
                                break;
                            case 11:
                                logUniqueIdentifier.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 12:
                                if (value != DBNull.Value)
                                    logUniqueIdentifier.set_LogAction((int)value);
                                else
                                    logUniqueIdentifier.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_unique_identifier table: " + reader.GetName(i));
                        }
                    }
                    logUniqueIdentifiers.Add(logUniqueIdentifier);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logUniqueIdentifiers;
        }

        public List<LogProvider> Read_LogProvider(IConnectable connection)
        {
            List<LogProvider> logProviders = new List<LogProvider>();

            string sql = "SELECT * FROM log_provider";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogProvider logProvider = new LogProvider();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logProvider.log_provider_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logProvider.provider_id = (int)value;
                                else
                                    logProvider.provider_id = null;
                                break;
                            case 2:
                                logProvider.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                logProvider.url_standard_access = value != DBNull.Value ? (string)value : null;
                                break;
                            case 4:
                                logProvider.url_admin_access = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logProvider.parent_provider_id = (int)value;
                                else
                                    logProvider.parent_provider_id = null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logProvider.created = (DateTime)value;
                                else
                                    logProvider.created = null;
                                break;
                            case 7:
                                logProvider.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logProvider.changed = (DateTime)value;
                                else
                                    logProvider.changed = null;
                                break;
                            case 9:
                                logProvider.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    logProvider.log_date = (DateTime)value;
                                else
                                    logProvider.log_date = null;
                                break;
                            case 11:
                                logProvider.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 12:
                                if (value != DBNull.Value)
                                    logProvider.set_LogAction((int)value);
                                else
                                    logProvider.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_provider table: " + reader.GetName(i));
                        }
                    }
                    logProviders.Add(logProvider);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logProviders;
        }

        public List<LogApp> Read_LogApp(IConnectable connection)
        {
            List<LogApp> logApps = new List<LogApp>();

            string sql = "SELECT * FROM log_app";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogApp logApp = new LogApp();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logApp.log_app_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logApp.app_id = (int)value;
                                else
                                    logApp.app_id = null;
                                break;
                            case 2:
                                logApp.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logApp.provider_id = (int)value;
                                else
                                    logApp.provider_id = null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    logApp.created = (DateTime)value;
                                else
                                    logApp.created = null;
                                break;
                            case 5:
                                logApp.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logApp.changed = (DateTime)value;
                                else
                                    logApp.changed = null;
                                break;
                            case 7:
                                logApp.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logApp.log_date = (DateTime)value;
                                else
                                    logApp.log_date = null;
                                break;
                            case 9:
                                logApp.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    logApp.set_LogAction((int)value);
                                else
                                    logApp.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_app table: " + reader.GetName(i));
                        }
                    }
                    logApps.Add(logApp);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logApps;
        }

        public List<LogJctPersonApp> Read_LogJctPersonApp(IConnectable connection)
        {
            List<LogJctPersonApp> logJctPersonApps = new List<LogJctPersonApp>();

            string sql = "SELECT * FROM log_jct_person_app";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogJctPersonApp logJctPersonApp = new LogJctPersonApp();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logJctPersonApp.log_jct_person_app_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logJctPersonApp.person_id = (int)value;
                                else
                                    logJctPersonApp.person_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logJctPersonApp.app_id = (int)value;
                                else
                                    logJctPersonApp.app_id = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logJctPersonApp.log_date = (DateTime)value;
                                else
                                    logJctPersonApp.log_date = null;
                                break;
                            case 4:
                                logJctPersonApp.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logJctPersonApp.set_LogAction((int)value);
                                else
                                    logJctPersonApp.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_jct_person_app table: " + reader.GetName(i));
                        }
                    }
                    logJctPersonApps.Add(logJctPersonApp);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logJctPersonApps;
        }

        public List<LogJctClassPerson> Read_LogJctClassPerson(IConnectable connection)
        {
            List<LogJctClassPerson> logJctClassPersonDaos = new List<LogJctClassPerson>();

            string sql = "SELECT * FROM log_jct_class_person";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogJctClassPerson logJctClassPerson = new LogJctClassPerson();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logJctClassPerson.log_jct_class_person_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logJctClassPerson.class_id = (int)value;
                                else
                                    logJctClassPerson.class_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logJctClassPerson.person_id = (int)value;
                                else
                                    logJctClassPerson.person_id = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logJctClassPerson.set_Role((int)value);
                                else
                                    logJctClassPerson.Role = null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    logJctClassPerson.log_date = (DateTime)value;
                                else
                                    logJctClassPerson = null;
                                break;
                            case 5:
                                logJctClassPerson.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logJctClassPerson.set_LogAction((int)value);
                                else
                                    logJctClassPerson.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_jct_class_person table: " + reader.GetName(i));
                        }
                    }
                    logJctClassPersonDaos.Add(logJctClassPerson);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return logJctClassPersonDaos;
        }

        public List<LogPhoneNumber> Read_LogPhoneNumber(IConnectable connection)
        {
            List<LogPhoneNumber> logPhoneNumbers = new List<LogPhoneNumber>();

            string sql = "SELECT * FROM log_phone_number";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogPhoneNumber logPhoneNumber = new LogPhoneNumber();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logPhoneNumber.log_phone_number_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logPhoneNumber.phone_number_id = (int)value;
                                else
                                    logPhoneNumber.phone_number_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logPhoneNumber.person_id = (int)value;
                                else
                                    logPhoneNumber.person_id = null;
                                break;
                            case 3:
                                logPhoneNumber.phone_number = value != DBNull.Value ? (string)value : null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    logPhoneNumber.set_IsPrimary((int)value);
                                else
                                    logPhoneNumber.IsPrimary = null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logPhoneNumber.set_PhoneType((int)value);
                                else
                                    logPhoneNumber.PhoneType = null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logPhoneNumber.created = (DateTime)value;
                                else
                                    logPhoneNumber.created = null;
                                break;
                            case 7:
                                logPhoneNumber.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logPhoneNumber.changed = (DateTime)value;
                                else
                                    logPhoneNumber.changed = null;
                                break;
                            case 9:
                                logPhoneNumber.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    logPhoneNumber.log_date = (DateTime)value;
                                else
                                    logPhoneNumber.log_date = null;
                                break;
                            case 11:
                                logPhoneNumber.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 12:
                                if (value != DBNull.Value)
                                    logPhoneNumber.set_LogAction((int)value);
                                else
                                    logPhoneNumber.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_phone_number table: " + reader.GetName(i));
                        }
                    }
                    logPhoneNumbers.Add(logPhoneNumber);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logPhoneNumbers;
        }

        public List<LogAddress> Read_LogAddress(IConnectable connection)
        {
            List<LogAddress> logAddresses = new List<LogAddress>();

            string sql = "SELECT * FROM log_address";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogAddress logAddress = new LogAddress();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logAddress.log_address_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logAddress.address_id = (int)value;
                                else
                                    logAddress.address_id = null;
                                break;
                            case 2:
                                logAddress.address_line_1 = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                logAddress.address_line_2 = value != DBNull.Value ? (string)value : null;
                                break;
                            case 4:
                                logAddress.address_line_3 = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                logAddress.address_line_4 = value != DBNull.Value ? (string)value : null;
                                break;
                            case 6:
                                logAddress.apartment = value != DBNull.Value ? (string)value : null;
                                break;
                            case 7:
                                logAddress.city = value != DBNull.Value ? (string)value : null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logAddress.set_StateProvince((int)value);
                                else
                                    logAddress.StateProvince = null;
                                break;
                            case 9:
                                logAddress.postal_code = value != DBNull.Value ? (string)value : null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    logAddress.set_Country((int)value);
                                else
                                    logAddress.Country = null;
                                break;
                            case 11:
                                if (value != DBNull.Value)
                                    logAddress.set_IsCurrent((int)value);
                                else
                                    logAddress.IsCurrent = null;
                                break;
                            case 12:
                                if (value != DBNull.Value)
                                    logAddress.set_AddressType((int)value);
                                else
                                    logAddress.AddressType = null;
                                break;
                            case 13:
                                if (value != DBNull.Value)
                                    logAddress.created = (DateTime)value;
                                else
                                    logAddress.created = null;
                                break;
                            case 14:
                                logAddress.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 15:
                                if (value != DBNull.Value)
                                    logAddress.changed = (DateTime)value;
                                else
                                    logAddress.changed = null;
                                break;
                            case 16:
                                logAddress.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 17:
                                if (value != DBNull.Value)
                                    logAddress.log_date = (DateTime)value;
                                else
                                    logAddress.log_date = null;
                                break;
                            case 18:
                                logAddress.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 19:
                                if (value != DBNull.Value)
                                    logAddress.set_LogAction((int)value);
                                else
                                    logAddress.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_address table: " + reader.GetName(i));
                        }
                    }
                    logAddresses.Add(logAddress);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logAddresses;
        }

        public List<LogJctPersonAddress> Read_LogJctPersonAddress(IConnectable connection)
        {
            List<LogJctPersonAddress> logJctPersonAddresss = new List<LogJctPersonAddress>();

            string sql = "SELECT * FROM log_jct_person_address";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogJctPersonAddress logJctPersonAddress = new LogJctPersonAddress();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logJctPersonAddress.log_jct_person_address_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logJctPersonAddress.person_id = (int)value;
                                else
                                    logJctPersonAddress.person_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logJctPersonAddress.address_id = (int)value;
                                else
                                    logJctPersonAddress.address_id = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logJctPersonAddress.log_date = (DateTime)value;
                                else
                                    logJctPersonAddress.log_date = null;
                                break;
                            case 4:
                                logJctPersonAddress.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logJctPersonAddress.set_LogAction((int)value);
                                else
                                    logJctPersonAddress.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_jct_person_address table: " + reader.GetName(i));
                        }
                    }
                    logJctPersonAddresss.Add(logJctPersonAddress);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logJctPersonAddresss;
        }

        public List<LogEmploymentSession> Read_LogEmploymentSession(IConnectable connection)
        {
            List<LogEmploymentSession> logEmploymentSessions = new List<LogEmploymentSession>();

            string sql = "SELECT * FROM log_employment_session";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogEmploymentSession logEmploymentSession = new LogEmploymentSession();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logEmploymentSession.log_employment_session_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logEmploymentSession.employment_session_id = (int)value;
                                else
                                    logEmploymentSession.employment_session_id = null;
                                break;
                            case 2:
                                logEmploymentSession.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logEmploymentSession.person_id = (int)value;
                                else
                                    logEmploymentSession.person_id = null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    logEmploymentSession.start_date = (DateTime)value;
                                else
                                    logEmploymentSession.start_date = null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logEmploymentSession.end_date = (DateTime)value;
                                else
                                    logEmploymentSession.end_date = null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logEmploymentSession.set_IsPrimary((int)value);
                                else
                                    logEmploymentSession.IsPrimary = null;
                                break;
                            case 7:
                                if (value != DBNull.Value)
                                    logEmploymentSession.set_SessionType((int)value);
                                else
                                    logEmploymentSession.SessionType = null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logEmploymentSession.organization_id = (int)value;
                                else
                                    logEmploymentSession.organization_id = null;
                                break;
                            case 9:
                                if (value != DBNull.Value)
                                    logEmploymentSession.created = (DateTime)value;
                                else
                                    logEmploymentSession.created = null;
                                break;
                            case 10:
                                logEmploymentSession.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 11:
                                if (value != DBNull.Value)
                                    logEmploymentSession.changed = (DateTime)value;
                                else
                                    logEmploymentSession.changed = null;
                                break;
                            case 12:
                                logEmploymentSession.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 13:
                                if (value != DBNull.Value)
                                    logEmploymentSession.log_date = (DateTime)value;
                                else
                                    logEmploymentSession.log_date = null;
                                break;
                            case 14:
                                logEmploymentSession.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 15:
                                if (value != DBNull.Value)
                                    logEmploymentSession.set_LogAction((int)value);
                                else
                                    logEmploymentSession.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_employment_session table: " + reader.GetName(i));
                        }
                    }
                    logEmploymentSessions.Add(logEmploymentSession);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logEmploymentSessions;
        }

        public List<LogAcademicSession> Read_LogAcademicSession(IConnectable connection)
        {
            List<LogAcademicSession> logAcademicSessions = new List<LogAcademicSession>();

            string sql = "SELECT * FROM log_academic_session";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogAcademicSession logAcademicSession = new LogAcademicSession();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logAcademicSession.log_academic_session_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logAcademicSession.academic_session_id = (int)value;
                                else
                                    logAcademicSession.academic_session_id = null;
                                break;
                            case 2:
                                logAcademicSession.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logAcademicSession.start_date = (DateTime)value;
                                else
                                    logAcademicSession.start_date = null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    logAcademicSession.end_date = (DateTime)value;
                                else
                                    logAcademicSession.end_date = null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logAcademicSession.set_SessionType((int)value);
                                else
                                    logAcademicSession.SessionType = null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logAcademicSession.parent_session_id = (int)value;
                                else
                                    logAcademicSession.parent_session_id = null;
                                break;
                            case 7:
                                if (value != DBNull.Value)
                                    logAcademicSession.organization_id = (int)value;
                                else
                                    logAcademicSession.organization_id = null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logAcademicSession.created = (DateTime)value;
                                else
                                    logAcademicSession.created = null;
                                break;
                            case 9:
                                logAcademicSession.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    logAcademicSession.changed = (DateTime)value;
                                else
                                    logAcademicSession.changed = null;
                                break;
                            case 11:
                                logAcademicSession.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 12:
                                if (value != DBNull.Value)
                                    logAcademicSession.log_date = (DateTime)value;
                                else
                                    logAcademicSession.log_date = null;
                                break;
                            case 13:
                                logAcademicSession.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 14:
                                if (value != DBNull.Value)
                                    logAcademicSession.set_LogAction((int)value);
                                else
                                    logAcademicSession.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_academic_session table: " + reader.GetName(i));
                        }
                    }
                    logAcademicSessions.Add(logAcademicSession);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logAcademicSessions;
        }

        public List<LogOrganization> Read_LogOrganization(IConnectable connection)
        {
            List<LogOrganization> logOrgs = new List<LogOrganization>();

            string sql = "SELECT * FROM log_organization";
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogOrganization logOrg = new LogOrganization();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logOrg.log_organization_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logOrg.organization_id = (int)value;
                                else
                                    logOrg.organization_id = null;
                                break;
                            case 2:
                                logOrg.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logOrg.set_OrganizationType((int)value);
                                else
                                    logOrg.OrganizationType = null;
                                break;
                            case 4:
                                logOrg.identifier = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                logOrg.building_code = value != DBNull.Value ? (string)value : null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logOrg.organization_parent_id = (int)value;
                                else
                                    logOrg.organization_parent_id = null;
                                break;
                            case 7:
                                if (value != DBNull.Value)
                                    logOrg.created = (DateTime)value;
                                else
                                    logOrg.created = null;
                                break;
                            case 8:
                                logOrg.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 9:
                                if (value != DBNull.Value)
                                    logOrg.changed = (DateTime)value;
                                else
                                    logOrg.changed = null;
                                break;
                            case 10:
                                logOrg.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 11:
                                if (value != DBNull.Value)
                                    logOrg.log_date = (DateTime)value;
                                else
                                    logOrg.log_date = null;
                                break;
                            case 12:
                                logOrg.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 13:
                                if (value != DBNull.Value)
                                    logOrg.set_LogAction((int)value);
                                else
                                    logOrg.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_organization table: " + reader.GetName(i));
                        }
                    }
                    logOrgs.Add(logOrg);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logOrgs;
        }

        public List<LogResource> Read_LogResource(IConnectable connection)
        {
            List<LogResource> logResources = new List<LogResource>();

            string sql = "SELECT * FROM log_resource";
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogResource logResource = new LogResource();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logResource.log_resource_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logResource.resource_id = (int)value;
                                else
                                    logResource.resource_id = null;
                                break;
                            case 2:
                                logResource.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logResource.set_Importance((int)value);
                                else
                                    logResource.Importance = null;
                                break;
                            case 4:
                                logResource.vendor_resource_identification = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                logResource.vendor_identification = value != DBNull.Value ? (string)value : null;
                                break;
                            case 6:
                                logResource.application_identification = value != DBNull.Value ? (string)value : null;
                                break;
                            case 7:
                                if (value != DBNull.Value)
                                    logResource.created = (DateTime)value;
                                else
                                    logResource.created = null;
                                break;
                            case 8:
                                logResource.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 9:
                                if (value != DBNull.Value)
                                    logResource.changed = (DateTime)value;
                                else
                                    logResource.changed = null;
                                break;
                            case 10:
                                logResource.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 11:
                                if (value != DBNull.Value)
                                    logResource.log_date = (DateTime)value;
                                else
                                    logResource.log_date = null;
                                break;
                            case 12:
                                logResource.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 13:
                                if (value != DBNull.Value)
                                    logResource.set_LogAction((int)value);
                                else
                                    logResource.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_resource table: " + reader.GetName(i));
                        }
                    }
                    logResources.Add(logResource);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logResources;
        }

        public List<LogCourse> Read_LogCourse(IConnectable connection)
        {
            List<LogCourse> logCourses = new List<LogCourse>();

            string sql = "SELECT * FROM log_course";
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogCourse logCourse = new LogCourse();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logCourse.log_course_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logCourse.course_id = (int)value;
                                else
                                    logCourse.course_id = null;
                                break;
                            case 2:
                                logCourse.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                logCourse.course_code = value != DBNull.Value ? (string)value : null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    logCourse.created = (DateTime)value;
                                else
                                    logCourse.created = null;
                                break;
                            case 5:
                                logCourse.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logCourse.changed = (DateTime)value;
                                else
                                    logCourse.changed = null;
                                break;
                            case 7:
                                logCourse.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logCourse.log_date = (DateTime)value;
                                else
                                    logCourse.log_date = null;
                                break;
                            case 9:
                                logCourse.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    logCourse.set_LogAction((int)value);
                                else
                                    logCourse.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_resource table: " + reader.GetName(i));
                        }
                    }
                    logCourses.Add(logCourse);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logCourses;
        }

        public List<LogJctCourseSubject> Read_LogJctCourseSubject(IConnectable connection)
        {
            List<LogJctCourseSubject> logJctCourseSubjects = new List<LogJctCourseSubject>();

            string sql = "SELECT * FROM log_jct_course_subject";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogJctCourseSubject logJctCourseSubject = new LogJctCourseSubject();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logJctCourseSubject.log_jct_course_subject_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logJctCourseSubject.course_id = (int)value;
                                else
                                    logJctCourseSubject.course_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logJctCourseSubject.set_Subject((int)value);
                                else
                                    logJctCourseSubject.Subject = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logJctCourseSubject.log_date = (DateTime)value;
                                else
                                    logJctCourseSubject.log_date = null;
                                break;
                            case 4:
                                logJctCourseSubject.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logJctCourseSubject.set_LogAction((int)value);
                                else
                                    logJctCourseSubject.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_jct_course_subject table: " + reader.GetName(i));
                        }
                    }
                    logJctCourseSubjects.Add(logJctCourseSubject);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logJctCourseSubjects;
        }

        public List<LogJctCourseGrade> Read_LogJctCourseGrade(IConnectable connection)
        {
            List<LogJctCourseGrade> logJctCourseGrades = new List<LogJctCourseGrade>();

            string sql = "SELECT * FROM log_jct_course_grade";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogJctCourseGrade logJctCourseGrade = new LogJctCourseGrade();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logJctCourseGrade.log_jct_course_grade_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logJctCourseGrade.course_id = (int)value;
                                else
                                    logJctCourseGrade.course_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logJctCourseGrade.set_Grade((int)value);
                                else
                                    logJctCourseGrade.Grade = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logJctCourseGrade.log_date = (DateTime)value;
                                else
                                    logJctCourseGrade.log_date = null;
                                break;
                            case 4:
                                logJctCourseGrade.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logJctCourseGrade.set_LogAction((int)value);
                                else
                                    logJctCourseGrade.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_jct_course_grade table: " + reader.GetName(i));
                        }
                    }
                    logJctCourseGrades.Add(logJctCourseGrade);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logJctCourseGrades;
        }

        public List<LogClass> Read_LogClass(IConnectable connection)
        {
            List<LogClass> logClasss = new List<LogClass>();

            string sql = "SELECT * FROM log_class";
            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogClass logClass = new LogClass();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logClass.log_class_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logClass.class_id = (int)value;
                                else
                                    logClass.class_id = null;
                                break;
                            case 2:
                                logClass.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                logClass.class_code = value != DBNull.Value ? (string)value : null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    logClass.set_ClassType((int)value);
                                else
                                    logClass.ClassType = null;
                                break;
                            case 5:
                                logClass.room = value != DBNull.Value ? (string)value : null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logClass.course_id = (int)value;
                                else
                                    logClass.course_id = null;
                                break;
                            case 7:
                                if (value != DBNull.Value)
                                    logClass.academic_session_id = (int)value;
                                else
                                    logClass.academic_session_id = null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logClass.created = (DateTime)value;
                                else
                                    logClass.created = null;
                                break;
                            case 9:
                                logClass.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    logClass.changed = (DateTime)value;
                                else
                                    logClass.changed = null;
                                break;
                            case 11:
                                logClass.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 12:
                                if (value != DBNull.Value)
                                    logClass.log_date = (DateTime)value;
                                else
                                    logClass.log_date = null;
                                break;
                            case 13:
                                logClass.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 14:
                                if (value != DBNull.Value)
                                    logClass.set_LogAction((int)value);
                                else
                                    logClass.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_resource table: " + reader.GetName(i));
                        }
                    }
                    logClasss.Add(logClass);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logClasss;
        }

        public List<LogJctClassPeriod> Read_LogJctClassPeriod(IConnectable connection)
        {
            List<LogJctClassPeriod> logJctClassPeriods = new List<LogJctClassPeriod>();

            string sql = "SELECT * FROM log_jct_class_period";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogJctClassPeriod logJctClassPeriod = new LogJctClassPeriod();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logJctClassPeriod.log_jct_class_period_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logJctClassPeriod.class_id = (int)value;
                                else
                                    logJctClassPeriod.class_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logJctClassPeriod.set_Period((int)value);
                                else
                                    logJctClassPeriod.Period = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logJctClassPeriod.log_date = (DateTime)value;
                                else
                                    logJctClassPeriod.log_date = null;
                                break;
                            case 4:
                                logJctClassPeriod.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logJctClassPeriod.set_LogAction((int)value);
                                else
                                    logJctClassPeriod.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_jct_class_period table: " + reader.GetName(i));
                        }
                    }
                    logJctClassPeriods.Add(logJctClassPeriod);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logJctClassPeriods;
        }

        public List<LogJctClassResource> Read_LogJctClassResource(IConnectable connection)
        {
            List<LogJctClassResource> logJctClassResources = new List<LogJctClassResource>();

            string sql = "SELECT * FROM log_jct_class_resource";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogJctClassResource logJctClassResource = new LogJctClassResource();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logJctClassResource.log_jct_class_resource_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logJctClassResource.class_id = (int)value;
                                else
                                    logJctClassResource.class_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logJctClassResource.resource_id = (int)value;
                                else
                                    logJctClassResource.resource_id = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logJctClassResource.log_date = (DateTime)value;
                                else
                                    logJctClassResource.log_date = null;
                                break;
                            case 4:
                                logJctClassResource.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logJctClassResource.set_LogAction((int)value);
                                else
                                    logJctClassResource.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_jct_class_resource table: " + reader.GetName(i));
                        }
                    }
                    logJctClassResources.Add(logJctClassResource);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logJctClassResources;
        }

        public List<LogLineItem> Read_LogLineItem(IConnectable connection)
        {
            List<LogLineItem> logLineItems = new List<LogLineItem>();

            string sql = "SELECT * FROM log_lineitem";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogLineItem logLineItem = new LogLineItem();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logLineItem.log_lineitem_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logLineItem.lineitem_id = (int)value;
                                else
                                    logLineItem.lineitem_id = null;
                                break;
                            case 2:
                                logLineItem.name = value != DBNull.Value ? (string)value : null;
                                break;
                            case 3:
                                logLineItem.descript = value != DBNull.Value ? (string)value : null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    logLineItem.assign_date = (DateTime)value;
                                else
                                    logLineItem.assign_date = null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logLineItem.due_date = (DateTime)value;
                                else
                                    logLineItem = null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logLineItem.class_id = (int)value;
                                else
                                    logLineItem.class_id = null;
                                break;
                            case 7:
                                if (value != DBNull.Value)
                                    logLineItem.set_Category((int)value);
                                else
                                    logLineItem.Category = null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logLineItem.markValueMin = (double)value;
                                else
                                    logLineItem.markValueMin = null;
                                break;
                            case 9:
                                if (value != DBNull.Value)
                                    logLineItem.markValueMax = (double)value;
                                else
                                    logLineItem.markValueMax = null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    logLineItem.created = (DateTime)value;
                                else
                                    logLineItem.created = null;
                                break;
                            case 11:
                                logLineItem.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 12:
                                if (value != DBNull.Value)
                                    logLineItem.changed = (DateTime)value;
                                else
                                    logLineItem.changed = null;
                                break;
                            case 13:
                                logLineItem.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 14:
                                if (value != DBNull.Value)
                                    logLineItem.log_date = (DateTime)value;
                                else
                                    logLineItem.log_date = null;
                                break;
                            case 15:
                                logLineItem.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 16:
                                if (value != DBNull.Value)
                                    logLineItem.set_LogAction((int)value);
                                else
                                    logLineItem.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_lineitem table: " + reader.GetName(i));
                        }
                    }
                    logLineItems.Add(logLineItem);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logLineItems;
        }

        public List<LogEnrollment> Read_LogEnrollment(IConnectable connection)
        {
            List<LogEnrollment> logEnrollments = new List<LogEnrollment>();

            string sql = "SELECT * FROM log_enrollment";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogEnrollment logEnrollment = new LogEnrollment();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logEnrollment.log_enrollment_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logEnrollment.enrollment_id = (int)value;
                                else
                                    logEnrollment.enrollment_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logEnrollment.person_id = (int)value;
                                else
                                    logEnrollment.person_id = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logEnrollment.class_id = (int)value;
                                else
                                    logEnrollment.class_id = null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    logEnrollment.set_GradeLevel((int)value);
                                else
                                    logEnrollment.GradeLevel = null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logEnrollment.start_date = (DateTime)value;
                                else
                                    logEnrollment.start_date = null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logEnrollment.end_date = (DateTime)value;
                                else
                                    logEnrollment.end_date = null;
                                break;
                            case 7:
                                if (value != DBNull.Value)
                                    logEnrollment.set_IsPrimary((int)value);
                                else
                                    logEnrollment.IsPrimary = null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logEnrollment.created = (DateTime)value;
                                else
                                    logEnrollment.created = null;
                                break;
                            case 9:
                                logEnrollment.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    logEnrollment.changed = (DateTime)value;
                                else
                                    logEnrollment.changed = null;
                                break;
                            case 11:
                                logEnrollment.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 12:
                                if (value != DBNull.Value)
                                    logEnrollment.log_date = (DateTime)value;
                                else
                                    logEnrollment.log_date = null;
                                break;
                            case 13:
                                logEnrollment.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 14:
                                if (value != DBNull.Value)
                                    logEnrollment.set_LogAction((int)value);
                                else
                                    logEnrollment.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_enrollment table: " + reader.GetName(i));
                        }
                    }
                    logEnrollments.Add(logEnrollment);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logEnrollments;
        }

        public List<LogMark> Read_LogMark(IConnectable connection)
        {
            List<LogMark> logMarks = new List<LogMark>();

            string sql = "SELECT * FROM log_mark";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogMark logMark = new LogMark();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logMark.log_mark_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logMark.mark_id = (int)value;
                                else
                                    logMark.mark_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logMark.lineitem_id = (int)value;
                                else
                                    logMark.lineitem_id = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logMark.person_id = (int)value;
                                else
                                    logMark.person_id = null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    logMark.set_ScoreStatus((int)value);
                                else
                                    logMark.ScoreStatus = null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logMark.score = (double)value;
                                else
                                    logMark.score = null;
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    logMark.score_date = (DateTime)value;
                                else
                                    logMark.score_date = null;
                                break;
                            case 7:
                                logMark.comment = value != DBNull.Value ? (string)value : null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    logMark.created = (DateTime)value;
                                else
                                    logMark.created = null;
                                break;
                            case 9:
                                logMark.created_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    logMark.changed = (DateTime)value;
                                else
                                    logMark.changed = null;
                                break;
                            case 11:
                                logMark.changed_by = value != DBNull.Value ? (string)value : null;
                                break;
                            case 12:
                                if (value != DBNull.Value)
                                    logMark.log_date = (DateTime)value;
                                else
                                    logMark.log_date = null;
                                break;
                            case 13:
                                logMark.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 14:
                                if (value != DBNull.Value)
                                    logMark.set_LogAction((int)value);
                                else
                                    logMark.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_mark table: " + reader.GetName(i));
                        }
                    }
                    logMarks.Add(logMark);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logMarks;
        }

        public List<LogJctEnrollmentAcademicSession> Read_JctEnrollmentAcademicSession(IConnectable connection)
        {
            List<LogJctEnrollmentAcademicSession> logJctEnrollmentAcademicSessions = new List<LogJctEnrollmentAcademicSession>();

            string sql = "SELECT * FROM log_jct_enrollment_academic_session";

            IDataReader reader = null;
            try
            {
                reader = connection.GetReader(sql);
                while (reader.Read())
                {
                    LogJctEnrollmentAcademicSession logJctEnrollmentAcademicSession = new LogJctEnrollmentAcademicSession();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                logJctEnrollmentAcademicSession.log_jct_enrollment_academic_session_id = value != DBNull.Value ? (int)value : -1;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    logJctEnrollmentAcademicSession.enrollment_id = (int)value;
                                else
                                    logJctEnrollmentAcademicSession.enrollment_id = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    logJctEnrollmentAcademicSession.academic_session_id = (int)value;
                                else
                                    logJctEnrollmentAcademicSession.academic_session_id = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    logJctEnrollmentAcademicSession.log_date = (DateTime)value;
                                else
                                    logJctEnrollmentAcademicSession.log_date = null;
                                break;
                            case 4:
                                logJctEnrollmentAcademicSession.log_user = value != DBNull.Value ? (string)value : null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    logJctEnrollmentAcademicSession.set_LogAction((int)value);
                                else
                                    logJctEnrollmentAcademicSession.LogAction = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in log_jct_enrollment_academic_session table: " + reader.GetName(i));
                        }
                    }
                    logJctEnrollmentAcademicSessions.Add(logJctEnrollmentAcademicSession);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return logJctEnrollmentAcademicSessions;
        }
    }
}