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
    public class SyncTask_ClassEnrolled : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_ClassEnrolled(string name, SyncJob job)
            : base(name, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisClassEnrolledCode = row["class_enrolled_code"].ToString() == "" ? null : row["class_enrolled_code"].ToString();
                string sisClassEnrolledName = row["name"].ToString() == "" ? null : row["name"].ToString();
                string sisRoom = row["room"].ToString() == "" ? null : row["room"].ToString();
                string sisCourseId = row["course_id"].ToString() == "" ? null : row["course_id"].ToString();
                string sisAcademicSessionId = row["academic_session_id"].ToString() == "" ? null : row["academic_session_id"].ToString();

                ICourseDao courseDaoImpl = DaoImplFactory.GetCourseDaoImpl();
                courseDaoImpl.SetTaskId(this.Id);
                Course course = courseDaoImpl.Read_UsingCourseCode(sisCourseId, _netus2Connection);

                if (course == null)
                    throw new Exception("Course not found with Course Code: " + sisCourseId);

                string[] sisAcademicSessionCodeArray = sisAcademicSessionId.Split('-');
                IAcademicSessionDao academicSessionDaoImpl = DaoImplFactory.GetAcademicSessionDaoImpl();
                academicSessionDaoImpl.SetTaskId(this.Id);
                AcademicSession academicSession = academicSessionDaoImpl.Read_UsingSisBuildingCode_TrackCode_TermCode_Schoolyear(
                    (sisAcademicSessionCodeArray[0] == "" ? null : sisAcademicSessionCodeArray[0]),
                    (sisAcademicSessionCodeArray[1] == "" ? null : sisAcademicSessionCodeArray[1]),
                    (sisAcademicSessionCodeArray[2] == "" ? null : sisAcademicSessionCodeArray[2]),
                    Int32.Parse((sisAcademicSessionCodeArray[3] == "" ? "-1" : sisAcademicSessionCodeArray[3])),
                    _netus2Connection);

                if (academicSession == null)
                    throw new Exception("AcademicSession not ofund with Building Code, Track Code, Term Code, and School Year: " + sisAcademicSessionId);

                IClassEnrolledDao classEnrolledDaoImpl = DaoImplFactory.GetClassEnrolledDaoImpl();
                classEnrolledDaoImpl.SetTaskId(this.Id);
                ClassEnrolled foundClassEnrolled = classEnrolledDaoImpl.Read_UsingClassEnrolledCode(sisClassEnrolledCode, _netus2Connection);

                ClassEnrolled classEnrolled = new ClassEnrolled(sisClassEnrolledName, sisClassEnrolledCode, sisRoom, course, academicSession);
                if (foundClassEnrolled == null)
                {
                    classEnrolledDaoImpl.Write(classEnrolled, _netus2Connection);
                }
                else
                {
                    if ((classEnrolled.Name != foundClassEnrolled.Name) ||
                        (classEnrolled.ClassCode != foundClassEnrolled.ClassCode) ||
                        (classEnrolled.Room != foundClassEnrolled.Room) ||
                        (classEnrolled.Course.Id != foundClassEnrolled.Course.Id) ||
                        (classEnrolled.AcademicSession.Id != foundClassEnrolled.AcademicSession.Id))
                    {
                        classEnrolled.Id = foundClassEnrolled.Id;
                        classEnrolledDaoImpl.Update(classEnrolled, _netus2Connection);
                    }
                }

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