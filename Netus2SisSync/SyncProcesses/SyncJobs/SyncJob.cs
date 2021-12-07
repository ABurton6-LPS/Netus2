using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.enumerations;
using System.Collections.Generic;

namespace Netus2SisSync.SyncProcesses
{
    public class SyncJob
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SyncTask> Tasks { get; }
        public int _maxThreadsPerBatch { get; }
        public int _totalRecordsToProcess { get; set; }

        public SyncJob(string name)
        {
            Name = name;
            Tasks = new List<SyncTask>();

            Config config = 
                Netus2_DatabaseConnection.utilityTools.UtilityTools.ReadConfig("max_threads", true, true);

            _maxThreadsPerBatch = int.Parse(config.Value);
        }
    }
}
