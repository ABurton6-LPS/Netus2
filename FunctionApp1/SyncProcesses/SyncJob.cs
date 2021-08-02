using System;
using System.Collections.Generic;
using System.Text;

namespace Netus2SisSync.SyncProcesses
{
    public class SyncJob
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public List<SyncTask> Tasks { get; }

        public SyncJob(string name, DateTime timestamp)
        {
            Name = name;
            Timestamp = timestamp;
            Tasks = new List<SyncTask>();
        }
    }
}
