using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumApplication;
using ForumApplication.Models;
namespace unitTestingDevelopers
{
    [TestClass]
    public class ForumUT
    {
        ForumSystem system = ForumSystem.initForumSystem();
        bool isProd = false;

        [TestMethod]
        public void checkMemberExistanceInDBUnitTest()
        {
            Member CheckingMember = system.addMember("ifatelias", "gilAdSahy", "ifatelias@bgu.ac.il");
            Assert.IsTrue(system.repository.dbIsMemberExists("ifateli", isProd));
        }
    }
}