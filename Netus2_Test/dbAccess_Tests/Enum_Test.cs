using Netus2.enumerations;
using NUnit.Framework;

namespace Netus2_Test.dbAccess_Tests
{
    class Enum_Test
    {
        [Test]
        public void Test_EnumAddress()
        {
            Enumeration enumAddress_Home = Enum_Address.values["home"];
            Assert.IsTrue(enumAddress_Home.Id > 0);
            Assert.AreEqual("home", enumAddress_Home.Netus2Code);
            Assert.AreEqual(null, enumAddress_Home.SisCode);
            Assert.AreEqual(null, enumAddress_Home.HrCode);
            Assert.AreEqual(null, enumAddress_Home.PipCode);
            Assert.AreEqual("A home address", enumAddress_Home.Descript);
        }

        [Test]
        public void Test_EnumLogAction()
        {
            Assert.AreEqual(2, Enum_Log_Action.values.Count);

            Enumeration enumLogAction_Update = Enum_Log_Action.values["update"];
            Assert.IsTrue(enumLogAction_Update.Id > 0);
            Assert.AreEqual("update", enumLogAction_Update.Netus2Code);
            Assert.AreEqual(null, enumLogAction_Update.SisCode);
            Assert.AreEqual(null, enumLogAction_Update.HrCode);
            Assert.AreEqual(null, enumLogAction_Update.PipCode);
            Assert.AreEqual("Log record was due to an update", enumLogAction_Update.Descript);

            Enumeration enumLogAction_Delete = Enum_Log_Action.values["delete"];
            Assert.IsTrue(enumLogAction_Delete.Id > 0);
            Assert.AreEqual("delete", enumLogAction_Delete.Netus2Code);
            Assert.AreEqual(null, enumLogAction_Delete.SisCode);
            Assert.AreEqual(null, enumLogAction_Delete.HrCode);
            Assert.AreEqual(null, enumLogAction_Delete.PipCode);
            Assert.AreEqual("Log record was due to a delete", enumLogAction_Delete.Descript);
        }
    }
}