using System;
using System.Collections.Generic;
using System.Text;

namespace Netus2SisSync.SyncProcesses
{
    public class SyncTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }

        public SyncTask(string name, DateTime timestamp)
        {
            Name = name;
            Timestamp = timestamp;
        }
    }
}
