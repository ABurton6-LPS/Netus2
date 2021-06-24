using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Netus2_SisSync
{
    public static class Netus2_SisSync
    {
        [FunctionName("Netus2_SisSync")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function Netus2_SisSync executed at: {DateTime.Now}");
        }
    }
}
