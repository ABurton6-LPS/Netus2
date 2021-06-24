using Netus2;
using Netus2.dbAccess;
using NUnit.Framework;
using System.Data.SqlClient;

namespace Netus2_Test.dbAccess_Tests
{
    public class DatabaseCreation_Test
    {
        IConnectable connection;

        [Test]
        public void Test_0_OpenConnection()
        {
            connection = new Netus2DatabaseConnection();
            connection.OpenConnection();
            connection.BeginTransaction();
        }

        [Test]
        public void Test_1_BuildSchema_AbleToRunTheCodeWithoutExceptions()
        {
            try
            {
                SchemaFactory.BuildSchema(connection);
            }
            catch (SqlException sqlE)
            {
                if (sqlE.Message.Contains("There is already an object named"))
                    Assert.Pass();
                else
                    throw;
            }
        }

        [Test]
        public void Test_2_BuildLogs_AbleToRunTheCodeWithoutExceptions()
        {
            try
            {
                LogFactory.BuildLogs(connection);
            }
            catch (SqlException sqlE)
            {
                if (sqlE.Message.Contains("There is already an object named"))
                    Assert.Pass();
                else
                    throw;
            }
        }

        [Test]
        public void Test_3_BuildTriggers_AbleToRunTheCodeWithoutExceptions()
        {
            try
            {
                TriggerFactory.BuildTriggers(connection);
            }
            catch (SqlException sqlE)
            {
                if (sqlE.Message.Contains("There is already an object named"))
                    Assert.Pass();
                else
                    throw;
            }
        }

        [Test]
        public void Test_4_BuildOneRosterViews_AbleToRunTheCodeWithoutExceptions()
        {
            try
            {
                ViewsFactory.BuildOneRosterViews(connection);
            }
            catch (SqlException sqlE)
            {
                if (sqlE.Message.Contains("There is already an object named"))
                    Assert.Pass();
                else
                    throw;
            }
        }

        [Test]
        public void Test_5_CloseConnection()
        {
            connection.RollbackTransaction();
            connection.CloseConnection();
        }
    }
}