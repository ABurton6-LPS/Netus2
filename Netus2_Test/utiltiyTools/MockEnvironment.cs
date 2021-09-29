using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;

namespace Netus2_Test.utiltiyTools
{
    public class MockEnvironment : MyEnvironment
    {
        private Dictionary<string, string> _mockEnvironment;

        public MockEnvironment()
        {
            _mockEnvironment = new Dictionary<string, string>();
        }

        public override string GetVariable(string variableName)
        {
            if (_mockEnvironment.ContainsKey(variableName))
                return _mockEnvironment[variableName];
            else
                return base.GetVariable(variableName);
        }

        public override void SetVariable(string variableName, string value)
        {
            if (_mockEnvironment.ContainsKey(variableName) == false)
                _mockEnvironment.Add(variableName, value);
            else
                _mockEnvironment[variableName] = value;
        }
    }
}
