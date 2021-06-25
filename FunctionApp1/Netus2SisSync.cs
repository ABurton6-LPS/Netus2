using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Netus2;
using Netus2.enumerations;

namespace FunctionApp1
{
    public static class Netus2SisSync
    {
        [FunctionName("Netus2SisSync")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function Netus2SisSync executed at: {DateTime.Now}");

            Person person = new Person("Test", "McTesterston", new DateTime(), Enum_Gender.values["male"], Enum_Ethnic.values["cau"]); ;
            log.LogInformation(person.ToString());
        }
    }
}
