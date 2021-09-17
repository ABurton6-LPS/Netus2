using Netus2_DatabaseConnection;
using Netus2SisSync.SyncProcesses.SyncTasks.AcademicSessionTasks;
using Netus2SisSync.SyncProcesses.SyncTasks.AddressTasks;
using Netus2SisSync.SyncProcesses.SyncTasks.OrganizationTasks;
using Netus2SisSync.SyncProcesses.SyncTasks.PersonTasks;
using System.Data;

namespace Netus2SisSync.SyncProcesses
{
    public abstract class SyncTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SyncJob Job { get; set; }

        public SyncTask(string name, SyncJob job)
        {
            Name = name;
            Job = job;
        }

        public abstract void Execute(DataRow row, CountDownLatch latch);

        public static void Execute_Organization_ChildRecordSync(SyncJob job, DataRow row, CountDownLatch latch)
        {
            SyncTask syncTask = new SyncTask_OrganizationChildRecords(
                    "SyncTask_OrganizationChildRecords", job);
            syncTask.Execute(row, latch);
        }

        public static void Execute_Organization_ParentRecordSync(SyncJob job, DataRow row, CountDownLatch latch)
        {
            SyncTask syncTask = new SyncTask_OrganizationParentRecords(
                    "SyncTask_OrganizationParentRecords", job);
            syncTask.Execute(row, latch);
        }

        public static void Execute_AcademicSession_ChildRecordSync(SyncJob job, DataRow row, CountDownLatch latch)
        {
            SyncTask syncTask = new SyncTask_AcademicSessionChildRecords(
                "SyncTask_AcademicSessionChildRecords", job);
            syncTask.Execute(row, latch);
        }

        public static void Execute_AcademicSession_ParentRecordSync(SyncJob job, DataRow row, CountDownLatch latch)
        {
            SyncTask syncTask = new SyncTask_AcademicSessionParentRecords(
                "SyncTask_AcademicSessionParentRecords", job);
            syncTask.Execute(row, latch);
        }

        public static void Execute_Person_RecordSync(SyncJob job, DataRow row, CountDownLatch latch)
        {
            SyncTask syncTask = new SyncTask_Person(
                "SyncTask_Person", job);
            syncTask.Execute(row, latch);
        }

        public static void Execute_Address_RecordSync(SyncJob job, DataRow row, CountDownLatch latch)
        {
            SyncTask syncTask = new SyncTask_Address(
                "SyncTask_Address", job);
            syncTask.Execute(row, latch);
        }
    }
}
