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
using System.Linq;

namespace Netus2SisSync.SyncProcesses.SyncTasks.CourseTasks
{
    public class SyncTask_Course : SyncTask
    {
        IConnectable _netus2Connection;

        public SyncTask_Course(string name, SyncJob job)
            : base(name, job)
        {
            _netus2Connection = DbConnectionFactory.GetNetus2Connection();
            SyncLogger.LogNewTask(this);
        }

        public override void Execute(DataRow row, CountDownLatch latch)
        {
            try
            {
                string sisCourseName = row["name"].ToString() == "" ? null : row["name"].ToString();
                string sisCourseCode = row["course_code"].ToString() == "" ? null : row["course_code"].ToString();
                string[] sisSubjectArray = row["subject"].ToString() == "" ? new string[0] : row["subject"].ToString().Split(',');
                string[] sisGradeArray = row["grade"].ToString() == "" ? new string[0] : row["grade"].ToString().Trim().Split(',');

                List<Enumeration> sisSubjectList = new List<Enumeration>();
                foreach(string subject in sisSubjectArray)
                {
                    sisSubjectList.Add(Enum_Subject.values[subject]);
                }

                List<Enumeration> sisGradeList = new List<Enumeration>();
                foreach(string grade in sisGradeArray)
                {
                    sisGradeList.Add(Enum_Grade.values[grade]);
                }

                Course course = new Course(sisCourseName, sisCourseCode);

                ICourseDao courseDaoImpl = DaoImplFactory.GetCourseDaoImpl();
                courseDaoImpl.SetTaskId(this.Id);
                List<Course> foundCourses = courseDaoImpl.Read(course, _netus2Connection);

                if(foundCourses.Count == 0)
                {
                    course = courseDaoImpl.Write(course, _netus2Connection);
                }
                else if(foundCourses.Count == 1)
                {
                    course.Id = foundCourses[0].Id;

                    if((course.Name != foundCourses[0].Name) ||
                       (course.CourseCode != foundCourses[0].CourseCode) ||
                       (sisSubjectList.Except(foundCourses[0].Subjects).ToList().Count > 0) ||
                       (foundCourses[0].Subjects.Except(sisSubjectList).ToList().Count > 0))
                        courseDaoImpl.Update(course, _netus2Connection);                        
                }
                else
                {
                    throw new Exception(foundCourses.Count + " record(s) found matching Course:\n" + course.ToString());
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