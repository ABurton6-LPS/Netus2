using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Netus2SisSync.SyncProcesses.SyncTasks.PersonTasks
{
    public class SyncTask_Person : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_Person(string name, DateTime timestamp, SyncJob job)
            : base(name, timestamp, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this, _netus2Connection);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
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
                List<UniqueIdentifier> foundUniqueIdentifiers = uniqueIdentifierDaoImpl.Read(uniqueId, -1, _netus2Connection);

                IPersonDao personDaoImpl = new PersonDaoImpl();
                Person person = person = new Person(sisFirstName, sisLastName, sisBirthDate, sisGender, sisEthnic);

                if (foundUniqueIdentifiers.Count == 0)
                {
                    person.Roles.Add(sisPersonType);
                    person.MiddleName = sisMiddleName;
                    person.ResidenceStatus = sisResidenceStatus;
                    person.LoginName = sisLoginName;
                    person.LoginPw = sisLoginPw;
                    person.UniqueIdentifiers.Add(uniqueId);
                    person = personDaoImpl.Write(person, _netus2Connection);
                }
                else if (foundUniqueIdentifiers.Count == 1)
                {
                    person.UniqueIdentifiers.Add(foundUniqueIdentifiers[0]);
                    List<Person> foundPersons = personDaoImpl.Read(person, _netus2Connection);

                    if (foundPersons.Count == 1)
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
                            personDaoImpl.Update(person, _netus2Connection);
                    }
                    else
                    {
                        throw new Exception(foundPersons.Count + " record(s) found matching Person:\n" + person.ToString());
                    }
                }
                else
                {
                    throw new Exception(foundUniqueIdentifiers.Count + " record(s) found matching UniqueIdentifier:\n" + uniqueId.ToString());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                SyncLogger.LogError(e, this, _netus2Connection);
            }
            finally
            {
                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"], _netus2Connection);
                _netus2Connection.CloseConnection();
                latch.Signal();
            }
        }
    }
}
