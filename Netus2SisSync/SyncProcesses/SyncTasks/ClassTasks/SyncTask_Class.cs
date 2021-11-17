using Netus2_DatabaseConnection;
using Netus2_DatabaseConnection.daoImplementations;
using Netus2_DatabaseConnection.daoInterfaces;
using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.enumerations;
using Netus2SisSync.UtilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Netus2SisSync.SyncProcesses.SyncTasks.ClassTasks
{
    public class SyncTask_Class : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_Class(string name, SyncJob job)
            : base(name, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisClassName = row["name"].ToString() == "" ? null : row["name"].ToString();
                string sisClassCode = row["class_code"].ToString() == "" ? null : row["class_code"].ToString();
                Enumeration sisClassType = row["enum_class_id"].ToString() == "" ? null : Enum_Class_Enrolled.GetEnumFromSisCode(row["enum_class_id"].ToString().ToLower());
                string sisRoom = row["room"].ToString() == "" ? null : row["room"].ToString();
                string sisCourseId = row["course_id"].ToString() == "" ? null : row["course_id"].ToString();
                string sisAcademicSessionId = row["academic_session_id"].ToString() == "" ? null : row["academic_session_id"].ToString();

                ICourseDao courseDaoImpl = DaoImplFactory.GetCourseDaoImpl();
                Course course = courseDaoImpl.Read_UsingCourseCode(sisCourseId, _netus2Connection);

                string[] sisAcademicSessionCodeArray = sisAcademicSessionId.Split('-');
                IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
                AcademicSession academicSession = academicSessionDaoImpl.Read_UsingSisBuildingCode_TrackCode_TermCode_Schoolyear(
                    (sisAcademicSessionCodeArray[0] == "" ? null : sisAcademicSessionCodeArray[0]),
                    (sisAcademicSessionCodeArray[1] == "" ? null : sisAcademicSessionCodeArray[1]),
                    (sisAcademicSessionCodeArray[2] == "" ? null : sisAcademicSessionCodeArray[2]),
                    Int32.Parse((sisAcademicSessionCodeArray[3] == "" ? "-1" : sisAcademicSessionCodeArray[3])),
                    _netus2Connection);

                ClassEnrolled classEnrolled = new ClassEnrolled(sisClassName, sisClassCode, sisClassType, sisRoom, course, academicSession);

                IClassEnrolledDao classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
                classEnrolledDaoImpl.SetTaskId(this.Id);
                List<ClassEnrolled> foundClasses = classEnrolledDaoImpl.Read(classEnrolled, _netus2Connection);

                if (foundClasses.Count == 0)
                {
                    classEnrolledDaoImpl.Write(classEnrolled, _netus2Connection);
                }
                else if(foundClasses.Count == 1)
                {
                    if ((classEnrolled.Name != foundClasses[0].Name) ||
                        (classEnrolled.ClassCode != foundClasses[0].ClassCode) ||
                        (classEnrolled.ClassType != foundClasses[0].ClassType) ||
                        (classEnrolled.Room != foundClasses[0].Room) ||
                        (classEnrolled.Course.Id != foundClasses[0].Course.Id) ||
                        (classEnrolled.AcademicSession.Id != foundClasses[0].AcademicSession.Id))
                        classEnrolledDaoImpl.Update(classEnrolled, _netus2Connection);
                }
                else
                    throw new Exception(foundClasses.Count + " record(s) found matching Class:\n" + classEnrolled.ToString());

                SyncLogger.LogStatus(this, Enum_Sync_Status.values["end"]);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                SyncLogger.LogError(e, this, row);
            }
            finally
            {
                _netus2Connection.CloseConnection();
                latch.Signal();
            }
        }
    }
}