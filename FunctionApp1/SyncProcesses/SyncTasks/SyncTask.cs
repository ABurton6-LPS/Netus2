using Netus2_DatabaseConnection.utilityTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Netus2SisSync.SyncProcesses
{
    public abstract class SyncTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public SyncJob Job { get; set; }

        public SyncTask(string name, DateTime timestamp, SyncJob job)
        {
            Name = name;
            Timestamp = timestamp;
            Job = job;
        }

        public abstract void Execute(DataRow row, CountDownLatch latch);
    }
}
