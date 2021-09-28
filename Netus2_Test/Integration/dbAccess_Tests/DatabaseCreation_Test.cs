using Netus2_DatabaseConnection.dbAccess;
using Netus2_DatabaseConnection.dbAccess.dbCreation;
using NUnit.Framework;
using System;
using System.Data.SqlClient;

namespace Netus2_Test.Integration
{
    public class DatabaseCreation_Test
    {
        IConnectable connection;

        [Test]
        public void Test_0_OpenConnection()
        {
            connection = DbConnectionFactory.GetNetus2Connection();
            connection.BeginTransaction();
        }

        [Test]
        public void Test_1_BuildSchema_AbleToRunTheCodeWithoutExceptions()
        {
            try
            {
                SchemaFactory.BuildSchema(connection);
            }
            catch (Exception sqlE)
            {
                if (sqlE.Message.Contains("There is already an object named"))
                    Assert.Pass();
                else
                    Assert.Fail();
            }
        }

        [Test]
        public void Test_2_BuildLogs_AbleToRunTheCodeWithoutExceptions()
        {
            try
            {
                LogFactory.BuildLogs(connection);
            }
            catch (Exception sqlE)
            {
                if (sqlE.Message.Contains("There is already an object named"))
                    Assert.Pass();
                else
                    Assert.Fail();
            }
        }

        [Test]
        public void Test_3_BuildTriggers_AbleToRunTheCodeWithoutExceptions()
        {
            try
            {
                TriggerFactory.BuildTriggers(connection);
            }
            catch (Exception sqlE)
            {
                if (sqlE.Message.Contains("There is already an object named"))
                    Assert.Pass();
                else
                    Assert.Fail();
            }
        }

        [Test]
        public void Test_4_BuildOneRosterViews_AbleToRunTheCodeWithoutExceptions()
        {
            try
            {
                ViewsFactory.BuildOneRosterViews(connection);
            }
            catch (Exception sqlE)
            {
                if (sqlE.Message.Contains("There is already an object named"))
                    Assert.Pass();
                else
                    Assert.Fail();
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