using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumApplication;
using ForumApplication.Models;
using System.Linq;
using System.Collections.Generic;

namespace unitTestingDevelopers
{
    [TestClass]
    public class ForumSystemUT
    {
        ForumSystem system = ForumSystem.initForumSystem();
        bool isProd = false;

        [TestMethod]
        public void checkAddMemberUnitTest()
        {
            Member member = new Member("username", "password", "email@email.com");
            Member member2 = new Member("username2", "password", "email2@email.com");
            Member member3 = new Member("username3", "password", "email3@email.com");
            system.repository.dbAddMember(member, isProd);
            system.repository.dbAddMember(member2, isProd);
            system.repository.dbAddMember(member3, isProd);
            var dbContext = new ForumDBContext();
            Assert.IsTrue(dbContext.Members.Any(o => o.Username == "username3"));
            Assert.IsTrue(dbContext.Members.Any(o => o.Username == "username"));
            Assert.IsTrue(dbContext.Members.Any(o => o.Username == "username2"));
        }

        [TestMethod]
        public void checkAddForumUnitTest()
        {
            List<string> admins = new List<string>();
            admins.Add("superAdmin");
            Forum ComputersForum = system.createForum("Computers", "superAdmin", admins);
            Forum geekTimeForum = new Forum("GeekTime", admins);
            system.repository.dbAddForum(geekTimeForum, isProd);
            var dbContext = new ForumDBContext();
            Assert.IsTrue(dbContext.Forums.Any(o => o.Title == "GeekTime"));
        }

        [TestMethod]
        public void checkAddMessageUnitTest()
        {
            Message message1 = new Message("Message1", "myContent1", "userName1");
            Message message2 = new Message("Message2", "myContent2", "userName2");
            Message message3 = new Message("title", "myContent3", "myUserName");
            var dbContext = new ForumDBContext();
            Assert.IsTrue(dbContext.Messages.Any(o => o.Title == "Message1"));
            Assert.IsTrue(dbContext.Messages.Any(o => o.Title == "Message2"));
            Assert.IsTrue(dbContext.Messages.Any(o => o.Title == "title"));
            Assert.IsTrue(dbContext.Messages.Any(o => o.UserName == "userName1"));
            Assert.IsTrue(dbContext.Messages.Any(o => o.UserName == "userName2"));
            Assert.IsTrue(dbContext.Messages.Any(o => o.UserName == "myUserName"));
            Assert.IsTrue(dbContext.Messages.Any(o => o.Content == "myContent1"));
            Assert.IsTrue(dbContext.Messages.Any(o => o.Content == "myContent2"));
            Assert.IsTrue(dbContext.Messages.Any(o => o.Content == "myContent3"));

        }
    }
}
