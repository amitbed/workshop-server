using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumApplication;
using ForumApplication.Models;
using System.Linq;
using System.Collections.Generic;

namespace unitTestingDevelopers
{
    [TestClass]
    public class SubForumUT
    {
        ForumSystem system = ForumSystem.initForumSystem();
        bool isProd = false;

        [TestMethod]
        public void CreateThredUnitTest()
        {
            List<string> admins = new List<string>();
            admins.Add("superAdmin");
            Forum forumForCheck = system.createForum("ForumForCheck", "superAdmin", admins);
            Assert.IsNotNull(forumForCheck);
            SubForum subForumForCheck = forumForCheck.createSubForum("subForumForCheck", admins, "ForumForCheck", 3);
            Assert.IsNotNull(subForumForCheck);
            Thread threadForCheck = subForumForCheck.createThread("threadToCheck", "subForumForCheck");
            Assert.IsNotNull(threadForCheck);
            system.repository.dbRemoveSubForum("subForumForCheck", isProd);
            system.repository.dbRemoveForum("ForumForCheck", isProd);
            system.repository.dbRemoveThread("threadToCheck", isProd);
        }

        [TestMethod]
        public void CorrectCreationOfThreadUnitTest1()
        {
            List<string> admins = new List<string>();
            admins.Add("superAdmin");
            Forum forumForCheck = system.createForum("ForumForCheck", "superAdmin", admins);
            Assert.IsNotNull(forumForCheck);
            SubForum subForumForCheck = forumForCheck.createSubForum("subForumForCheck", admins, "ForumForCheck", 3);
            Assert.IsNotNull(subForumForCheck);
            Assert.IsTrue(subForumForCheck.Parent == "ForumForCheck");
            Thread threadForCheck = subForumForCheck.createThread("threadToCheck", "subForumForCheck");
            Assert.IsNotNull(threadForCheck);
            system.repository.dbRemoveSubForum("subForumForCheck", isProd);
            system.repository.dbRemoveForum("ForumForCheck", isProd);
            system.repository.dbRemoveThread("threadToCheck", isProd);
        }

        [TestMethod]
        public void CorrectCreationOfThreadUnitTest2()
        {
            List<string> admins = new List<string>();
            admins.Add("superAdmin");
            Forum forumForCheck = system.createForum("ForumForCheck", "superAdmin", admins);
            Assert.IsNotNull(forumForCheck);
            SubForum subForumForCheck = forumForCheck.createSubForum("subForumForCheck", admins, "ForumForCheck", 3);
            Assert.IsNotNull(subForumForCheck);
            Assert.IsTrue(subForumForCheck.Parent == "ForumForCheck");
            Thread threadForCheck = subForumForCheck.createThread("threadToCheck", "subForumForCheck");
            Assert.IsNotNull(threadForCheck);
            Assert.IsTrue(threadForCheck.Parent == "subForumForCheck");
            system.repository.dbRemoveSubForum("subForumForCheck", isProd);
            system.repository.dbRemoveForum("ForumForCheck", isProd);
            system.repository.dbRemoveThread("threadToCheck", isProd);
        }

        [TestMethod]
        public void CorrectCreationOfThreadUnitTest3()
        {
            List<string> admins = new List<string>();
            admins.Add("superAdmin");
            Forum forumForCheck = system.createForum("ForumForCheck", "superAdmin", admins);
            Assert.IsNotNull(forumForCheck);
            SubForum subForumForCheck = forumForCheck.createSubForum("subForumForCheck", admins, "ForumForCheck", 3);
            Assert.IsNotNull(subForumForCheck);
            Assert.IsTrue(subForumForCheck.Parent == "ForumForCheck");
            Thread threadForCheck = subForumForCheck.createThread("threadToCheck", "subForumForCheck");
            Assert.IsNotNull(threadForCheck);
            Assert.IsTrue(threadForCheck.Parent == "subForumForCheck");
            system.repository.dbRemoveThread("threadToCheck", isProd);
            system.repository.dbRemoveSubForum("subForumForCheck", isProd);
            system.repository.dbRemoveForum("ForumForCheck", isProd);

        }
    }
}