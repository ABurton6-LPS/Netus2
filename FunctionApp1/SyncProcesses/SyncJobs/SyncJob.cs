using System;
using System.Collections.Generic;
using System.Text;

namespace Netus2SisSync.SyncProcesses
{
    public class SyncJob
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SyncTask> Tasks { get; }

        public SyncJob(string name)
        {
            Name = name;
            Tasks = new List<SyncTask>();
        }
    }
}
