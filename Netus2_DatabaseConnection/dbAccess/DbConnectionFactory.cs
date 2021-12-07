using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;

namespace Netus2_DatabaseConnection.dbAccess
{
    public static class DbConnectionFactory
    {
        public static bool ShouldUseMockDb = false;
        public static MockDatabaseConnection _mockDbConnection = null;
        public static MyEnvironment environment = new MyEnvironment();
        
        public static IConnectable GetSisConnection()
        {
            if (ShouldUseMockDb)
                return GetMockConnection();
            else
                return new AsyncDatabaseConnection(GetSisConnectionString());
        }

        public static IConnectable GetNetus2Connection()
        {
            if (ShouldUseMockDb)
                return GetMockConnection();

            string currentEnvironment = environment.GetVariable("CURRENT_ENVIRONMENT");
            switch (currentEnvironment)
            {
                case "loc":
                    return new AsyncDatabaseConnection(GetDatabaseConnectionString_Netus2_Local());
                case "tst":
                    return new AsyncDatabaseConnection(GetDatabaseConnectionString_Netus2_Tst());
                default:
                    return GetMockConnection();
            }
        }

        private static IConnectable GetMockConnection()
        {
            if (_mockDbConnection == null)
                _mockDbConnection =  new MockDatabaseConnection();

            return _mockDbConnection;
        }

        private static string GetSisConnectionString()
        {
            return UtilityTools.ReadConfig("sis_db_string",true,true).Value;
        }

        private static string GetDatabaseConnectionString_Netus2_Tst()
        {
            string nameOfEnvironmentVariable = "Netus2DbConnectionString_Tst";
            string connectionString = environment.GetVariable(nameOfEnvironmentVariable);

            if (connectionString == null)
                throw new System.Exception("The " + nameOfEnvironmentVariable + " Environment Variable is not set.");

            return connectionString;
        }

        private static string GetDatabaseConnectionString_Netus2_Local()
        {
            string nameOfEnvironmentVariable = "Netus2DbConnectionString_Loc";
            string connectionString = environment.GetVariable(nameOfEnvironmentVariable);

            if (connectionString == null)
                throw new System.Exception("The " + nameOfEnvironmentVariable + " Environment Variable is not set.");

            return connectionString;
        }
    }
}
