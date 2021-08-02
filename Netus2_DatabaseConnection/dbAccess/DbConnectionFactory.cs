using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace Netus2_DatabaseConnection.dbAccess
{
    public class DbConnectionFactory
    {
        readonly static string LocalDatabaseConnectionString = @"Data Source=ITDSL0995104653;Initial Catalog=Netus2;Integrated Security=SSPI";
        readonly static string Netus2DatabaseConnectionString = @"Data Source=tcp:janusdb.database.windows.net,1433;Initial Catalog=Netus2;Uid=janus;Pwd=AqIiA59@$J0K";
        readonly static string MiStarDatabaseConnectionString = @"Data Source=tcp:lvboe-zangle.resa.net,1433;Initial Catalog=lvboe;Uid=lvnetus;Pwd=V67A#O9miN#TzQ5x2gzS";
        readonly static string MockDatabaseConnectionString = @"MockDatabaseConnectionString";

        public static IConnectable GetConnection(string connectionType)
        {
            switch (connectionType)
            {
                case "Local":
                    return new GenericDatabaseConnection(LocalDatabaseConnectionString);
                case "Netus2":
                    return new GenericDatabaseConnection(Netus2DatabaseConnectionString);
                case "MiStar":
                    return new GenericDatabaseConnection(MiStarDatabaseConnectionString);
                case "Mock":
                    return new MockDatabaseConnection(MockDatabaseConnectionString);
                default:
                    return null;
            }
        }
    }
}
