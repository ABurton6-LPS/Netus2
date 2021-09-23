﻿using Netus2SisSync.SyncProcesses.SyncJobs;
using NUnit.Framework;

namespace Netus2_Test.Integration
{
    public class Netus2Sync_Test
    {
        [TestCase]
        public void TestRun()
        {
            new SyncJob_Organization().Start();

            new SyncJob_AcademicSession().Start();

            new SyncJob_Person().Start();

            //new SyncJob_Address().Start();
        }
    }
}
