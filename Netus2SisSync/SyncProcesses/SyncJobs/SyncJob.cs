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

        public SyncJob(string name)
        {
            Name = name;
            Tasks = new List<SyncTask>();

            Config config = 
                Netus2_DatabaseConnection.utilityTools.UtilityTools.ReadConfig(
                Enum_Config.values["max_threads"], 
                Enum_True_False.values["true"], 
                Enum_True_False.values["true"]);

            _maxThreadsPerBatch = int.Parse(config.ConfigValue);
        }
    }
}
