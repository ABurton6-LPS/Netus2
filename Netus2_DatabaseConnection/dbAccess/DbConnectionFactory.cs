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
                case "local":
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
            return UtilityTools.ReadConfig(
                            Enum_Config.values["sis_db_string"],
                            Enum_True_False.values["true"],
                            Enum_True_False.values["false"])
                        .ConfigValue;
        }

        private static string GetDatabaseConnectionString_Netus2_Tst()
        {
            string connectionString = environment.GetVariable("Netus2DbConnectionString_Tst");

            if (connectionString == null)
                throw new System.Exception("The Netus2DbConnectionString_Test Environment Variable is not set.");

            return connectionString;
        }

        private static string GetDatabaseConnectionString_Netus2_Local()
        {
            string connectionString = environment.GetVariable("Netus2DbConnectionString_Local");

            if (connectionString == null)
                throw new System.Exception("The Netus2DbConnectionString_Local Environment Variable is not set.");

            return connectionString;
        }
    }
}
