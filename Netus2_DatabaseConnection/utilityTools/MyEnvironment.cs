using System;

namespace Netus2_DatabaseConnection.utilityTools
{
    public class MyEnvironment
    {
        public virtual string GetVariable(string variableName)
        {
            return Environment.GetEnvironmentVariable(variableName);
        }

        public virtual void SetVariable(string variableName, string value)
        {
            Environment.SetEnvironmentVariable(variableName, value);
        }
    }
}
