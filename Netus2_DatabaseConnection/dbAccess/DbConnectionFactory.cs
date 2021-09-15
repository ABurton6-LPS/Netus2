namespace Netus2_DatabaseConnection.dbAccess
{
    public static class DbConnectionFactory
    {
        public static bool TestMode = false;
        public static IConnectable MockDatabaseConnection = null;

        private static string LocalDatabaseConnectionString = @"Data Source=ITDSL0995104653;Initial Catalog=Netus2;Integrated Security=SSPI";
        private static string SisDatabaseConnectionString = @"Data Source=tcp:lvboe-zangle.resa.net,1433;Initial Catalog=lvboe;Uid=lvnetus;Pwd=V67A#O9miN#TzQ5x2gzS";
        private static string MockDatabaseConnectionString = @"MockDatabaseConnectionString";
        //private static string Netus2DatabaseConnectionString = @"Data Source=tcp:janusdb.database.windows.net,1433;Initial Catalog=Netus2;Uid=janus;Pwd=AqIiA59@$J0K";
        private static string Netus2DatabaseConnectionString = "off";

        public static IConnectable GetLocalConnection()
        {
            if (TestMode)
            {
                if(MockDatabaseConnection == null)
                    MockDatabaseConnection = new MockDatabaseConnection(MockDatabaseConnectionString);
                return MockDatabaseConnection;
            }
            else
            {
                return new GenericDatabaseConnection(LocalDatabaseConnectionString);
            }
        }

        public static IConnectable GetSisConnection()
        {
            if (TestMode)
            {
                if (MockDatabaseConnection == null)
                    MockDatabaseConnection = new MockDatabaseConnection(MockDatabaseConnectionString);
                return MockDatabaseConnection;
            }
            else
            {
                return new GenericDatabaseConnection(SisDatabaseConnectionString);
            }
        }

        public static IConnectable GetNetus2Connection()
        {
            if (TestMode)
            {
                if (MockDatabaseConnection == null)
                    MockDatabaseConnection = new MockDatabaseConnection(MockDatabaseConnectionString);
                return MockDatabaseConnection;
            }
            else
            {
                if(Netus2DatabaseConnectionString != "off")
                {
                    return new GenericDatabaseConnection(Netus2DatabaseConnectionString);
                }
                else
                {
                    return new GenericDatabaseConnection(LocalDatabaseConnectionString);
                }
            }
        }
    }
}
