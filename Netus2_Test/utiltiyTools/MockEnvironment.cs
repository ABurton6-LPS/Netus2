﻿using Netus2_DatabaseConnection.utilityTools;
using System.Collections.Generic;

namespace Netus2_Test.utiltiyTools
{
    public class MockEnvironment : MyEnvironment
    {
        private Dictionary<string, string> _mockEnvironment;

        public MockEnvironment()
        {
            _mockEnvironment = new Dictionary<string, string>();

            SetVariable("CURRENT_ENVIRONMENT", "local");
            SetVariable("Netus2DbConnectionString_Local", "Data Source=ITDSL0995104653;Initial Catalog=Netus2;Integrated Security=SSPI;MultipleActiveResultSets=True");
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