using Microsoft.Extensions.Logging;
using Netus2;
using Netus2.daoImplementations;
using Netus2.daoInterfaces;
using Netus2.dbAccess;
using Netus2.enumerations;
using Netus2_DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Netus2SisSync
{
    public class SyncPerson
    {
        public static DataTable ReadFromSis(IConnectable miStarConnection)
        {
            DataTable dtPerson = UtilityTools.CreateDataTable("Person");
            SqlDataReader reader = null;
            try
            {
                reader = miStarConnection.GetReader(SyncScripts.ReadSis_Person_SQL);
                while (reader.Read())
                {
                    DataRow myDataRow = dtPerson.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var value = reader.GetValue(i);
                        switch (i)
                        {
                            case 0:
                                if (value != DBNull.Value)
                                    myDataRow["person_type"] = (string)value;
                                else
                                    myDataRow["person_type"] = null;
                                break;
                            case 1:
                                if (value != DBNull.Value)
                                    myDataRow["SIS_ID"] = (int)value;
                                else
                                    myDataRow["SIS_ID"] = null;
                                break;
                            case 2:
                                if (value != DBNull.Value)
                                    myDataRow["first_name"] = (string)value;
                                else
                                    myDataRow["first_name"] = null;
                                break;
                            case 3:
                                if (value != DBNull.Value)
                                    myDataRow["middle_name"] = (string)value;
                                else
                                    myDataRow["middle_name"] = null;
                                break;
                            case 4:
                                if (value != DBNull.Value)
                                    myDataRow["last_name"] = (string)value;
                                else
                                    myDataRow["last_name"] = null;
                                break;
                            case 5:
                                if (value != DBNull.Value)
                                    myDataRow["birth_date"] = (DateTime)value;
                                else
                                    myDataRow["birth_date"] = new DateTime();
                                break;
                            case 6:
                                if (value != DBNull.Value)
                                    myDataRow["enum_gender_id"] = (string)value;
                                else
                                    myDataRow["enum_gender_id"] = null;
                                break;
                            case 7:
                                if (value != DBNull.Value)
                                    myDataRow["enum_ethnic_id"] = (string)value;
                                else
                                    myDataRow["enum_ethnic_id"] = null;
                                break;
                            case 8:
                                if (value != DBNull.Value)
                                    myDataRow["enum_residence_status_id"] = (string)value;
                                else
                                    myDataRow["enum_residence_status_id"] = null;
                                break;
                            case 9:
                                if (value != DBNull.Value)
                                    myDataRow["login_name"] = (string)value;
                                else
                                    myDataRow["login_name"] = null;
                                break;
                            case 10:
                                if (value != DBNull.Value)
                                    myDataRow["login_pw"] = (string)value;
                                else
                                    myDataRow["login_pw"] = null;
                                break;
                            default:
                                throw new Exception("Unexpected column found in SIS lookup for Organization: " + reader.GetName(i));
                        }
                    }
                    dtPerson.Rows.Add(myDataRow);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return dtPerson;
        }

        public static void SyncForAllRecords(DataRow row, CountDownLatch latch, ILogger log)
        {
            IConnectable connection = null;
            try
            {
                connection = new Netus2DatabaseConnection();
                connection.OpenConnection();

                SyncForAllRecords(row, latch, connection, log);
            }
            catch (Exception e)
            {
                log.LogError(e.Message, e);
                Debug.WriteLine(e.Message);
            }
            finally
            {
                connection.CloseConnection();
                latch.Signal();
            }
        }
        public static void SyncForAllRecords(DataRow row, CountDownLatch latch, IConnectable connection, ILogger log)
        {
            Enumeration sisPersonType = Enum_Role.values[row["person_type"].ToString()];
            if (sisPersonType == Enum_Role.values["student"])
            {
                int x = 0;
            }
            string sisId = row["SIS_ID"].ToString() == "" ? null : row["SIS_ID"].ToString();
            string sisFirstName = row["first_name"].ToString() == "" ? null : row["first_name"].ToString();
            string sisMiddleName = row["middle_name"].ToString() == "" ? null : row["middle_name"].ToString();
            string sisLastName = row["last_name"].ToString() == "" ? null : row["last_name"].ToString();
            DateTime sisBirthDate = DateTime.Parse(row["birth_date"].ToString());
            Enumeration sisGender = Enum_Gender.values[row["enum_gender_id"].ToString()];
            Enumeration sisEthnic = row["enum_ethnic_id"].ToString() == "unset" ? Enum_Ethnic.values["unset"] : Enum_Ethnic.GetEnumFromSisCode(row["enum_ethnic_id"].ToString());
            Enumeration sisResidenceStatus = Enum_Residence_Status.values[row["enum_residence_status_id"].ToString()];
            string sisLoginName = row["login_name"].ToString() == "" ? null : row["login_name"].ToString();
            string sisLoginPw = row["login_pw"].ToString() == "" ? null : row["login_pw"].ToString();

            Enumeration typeOfSisId = null;
            if (sisPersonType == Enum_Role.values["staff"])
            {
                typeOfSisId = Enum_Identifier.values["funiq"];
            }
            else
            {
                typeOfSisId = Enum_Identifier.values["student id"];
            }
            UniqueIdentifier uniqueId = new UniqueIdentifier(sisId, typeOfSisId, Enum_True_False.values["true"]);
            IUniqueIdentifierDao uniqueIdentifierDaoImpl = new UniqueIdentifierDaoImpl();
            List<UniqueIdentifier> foundUniqueIdentifiers = uniqueIdentifierDaoImpl.Read(uniqueId, -1, connection);

            IPersonDao personDaoImpl = new PersonDaoImpl();
            Person person = person = new Person(sisFirstName, sisLastName, sisBirthDate, sisGender, sisEthnic);

            if (foundUniqueIdentifiers.Count > 1)
            {
                throw new Exception("Multiple UniqueIdentifier records found matching:\n" + uniqueId.ToString());
            }
            else if (foundUniqueIdentifiers.Count == 0)
            {
                person.Roles.Add(sisPersonType);
                person.MiddleName = sisMiddleName;
                person.ResidenceStatus = sisResidenceStatus;
                person.LoginName = sisLoginName;
                person.LoginPw = sisLoginPw;
                person.UniqueIdentifiers.Add(uniqueId);
                person = personDaoImpl.Write(person, connection);
            }
            else if (foundUniqueIdentifiers.Count == 1)
            {
                person.UniqueIdentifiers.Add(foundUniqueIdentifiers[0]);
                List<Person> foundPersons = personDaoImpl.Read(person, connection);

                if (foundPersons.Count == 0)
                {
                    throw new Exception("Person record was never written to the Netus2 database.\n" + person.ToString());
                }
                else if (foundPersons.Count > 1)
                {
                    throw new Exception("Multiple records found matching Person record:\n" + person.ToString());
                }
                else if (foundPersons.Count == 1)
                {
                    person = foundPersons[0];

                    bool needsToBeUpdated = false;
                    if (person.Roles.Contains(sisPersonType) == false)
                    {
                        person.Roles.Add(sisPersonType);
                        needsToBeUpdated = true;
                    }
                    if ((person.MiddleName != sisMiddleName) ||
                        (person.ResidenceStatus != sisResidenceStatus) ||
                        (person.LoginName != sisLoginName) ||
                        (person.LoginPw != sisLoginPw))
                    {
                        person.MiddleName = sisMiddleName;
                        person.ResidenceStatus = sisResidenceStatus;
                        person.LoginName = sisLoginName;
                        person.LoginPw = sisLoginPw;
                        needsToBeUpdated = true;
                    }
                    if (needsToBeUpdated)
                        personDaoImpl.Update(person, connection);
                }
            }
        }
    }
}
