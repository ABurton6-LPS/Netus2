using Netus2_DatabaseConnection.enumerations;
using Netus2_DatabaseConnection.utilityTools;

namespace Netus2_DatabaseConnection.dbAccess
{
    public static class DbConnectionFactory
    {
        public static bool ShouldUseLocalDb = false;
        public static bool ShouldUseMockDb = false;

        public static MockDatabaseConnection _mockDbConnection = null;
        
        public static IConnectable GetLocalConnection()
        {
            if (ShouldUseMockDb)
            {
                if(_mockDbConnection == null)
                    return new MockDatabaseConnection();
                return _mockDbConnection;
            }
            else
            {
                return new AsyncDatabaseConnection(GetLocalDatabaseConnectionString());
            }
        }

        public static IConnectable GetSisConnection()
        {
            if (ShouldUseMockDb)
            {
                if (_mockDbConnection == null)
                    _mockDbConnection =  new MockDatabaseConnection();
                return _mockDbConnection;
            }
            else
            {
                return new AsyncDatabaseConnection(
                        UtilityTools.ReadConfig(
                            Enum_Config.values["sis_Db_String"], 
                            Enum_True_False.values["true"], 
                            Enum_True_False.values["false"])
                        .ConfigValue);
            }
        }

        public static IConnectable GetNetus2Connection()
        {
            if (ShouldUseLocalDb || ShouldUseMockDb)
            {
                return GetLocalConnection();
            }
            else
            {
                return new AsyncDatabaseConnection(GetNetus2ConnectionString());
            }
        }

        private static string GetNetus2ConnectionString()
        {
            string connectionString = System.Environment.GetEnvironmentVariable("Netus2DbConnectionString_Cloud", System.EnvironmentVariableTarget.User);

            if (connectionString == null)
                throw new System.Exception("The Netus2DbConnectionString_Cloud Environment Variable is not set at the User level.");

            return connectionString;
        }

        private static string GetLocalDatabaseConnectionString()
        {
            string connectionString = System.Environment.GetEnvironmentVariable("Netus2DbConnectionString_Local", System.EnvironmentVariableTarget.User);

            if (connectionString == null)
                throw new System.Exception("The Netus2DbConnectionString_Local Environment Variable is not set at the User level.");

            return connectionString;
        }
    }
}
