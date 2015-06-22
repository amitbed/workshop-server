using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumApplication;
using ForumApplication.Models;
using System.Collections.Generic;

namespace unitTestingDevelopers
{
    [TestClass]
    public class AdminForumUT
    {
        ForumSystem system = ForumSystem.initForumSystem();
        bool isProd = false;

        [TestMethod]
        public void checkAdminForumFunctionality1()
        {
            List<string> admins = new List<string>();
            admins.Add("superAdmin");
            Forum newForumForChecking = system.createForum("newChecking", "superAdmin", admins);
            AdminForum admin = new AdminForum(newForumForChecking);
            Assert.IsNotNull(admin);
            system.repository.dbRemoveForum("newChecking", isProd);
        }

        [TestMethod]
        public void checkAdminForumFunctionality2()
        {
            List<string> admins = new List<string>();
            admins.Add("superAdmin");
            Forum newForumForChecking = system.createForum("newChecking", "superAdmin", admins);
            AdminForum admin = new AdminForum(newForumForChecking);
            Assert.IsTrue(admin.MaxModerators == 0);
            system.repository.dbRemoveForum("newChecking", isProd);
        }

        [TestMethod]
        public void checkAdminForumFunctionality3()
        {
            List<string> admins = new List<string>();
            admins.Add("superAdmin");
            Forum newForumForChecking = system.createForum("newChecking", "superAdmin", admins);
            AdminForum admin = new AdminForum(newForumForChecking);
            Assert.IsTrue(admin.MemberSubForums == null);
            system.repository.dbRemoveForum("newChecking", isProd);
        }
    }
}