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
        public SyncJob Job { get; set; }

        public SyncTask(string name, SyncJob job)
        {
            Name = name;
            Job = job;
        }

        public abstract void Execute(DataRow row);
    }
}
