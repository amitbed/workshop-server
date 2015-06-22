using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumApplication;
using ForumApplication.Models;
using System.Linq;
using System.Collections.Generic;

namespace unitTestingDevelopers
{
    [TestClass]
    public class ForumSystemRepositoryUT
    {
        ForumSystem system = ForumSystem.initForumSystem();
        [TestMethod]
        public void checkAddMember()
        {
            Member member = new Member("username", "password", "email@email.com");
            Member member2 = new Member("username2", "password", "email2@email.com");
            Member member3 = new Member("username3", "password", "email3@email.com");
            system.repository.dbAddMember(member, false);
            system.repository.dbAddMember(member2, false);
            system.repository.dbAddMember(member3, true);
            var dbContext = new ForumDBContext();
            Assert.IsTrue(dbContext.Members.Any(o => o.Username == "username3"));
            dbContext.ChangeDatabaseTo("TestForumDBContext");
            Assert.IsTrue(dbContext.Members.Any(o => o.Username == "username"));
            Assert.IsTrue(dbContext.Members.Any(o => o.Username == "username2"));
        }

        [TestMethod]
        public void checkAddMessage()
        {
            Message message = new Message("Message1", "test", "username");
            Message message2 = new Message("Message2", "test", "username");
            system.repository.dbAddMessage(message, false);
            system.repository.dbAddMessage(message2, true);
            var dbContext = new ForumDBContext();
            Assert.IsTrue(dbContext.Messages.Any(o => o.Title == "Message2"));
            dbContext.ChangeDatabaseTo("TestForumDBContext");
            Assert.IsTrue(dbContext.Messages.Any(o => o.Title == "Message1"));
        }

        [TestMethod]
        public void checkRemoveForum()
        {
            var dbContext = new ForumDBContext();
            List<string> admins = new List<string>();
            admins.Add("abadie");
            admins.Add("amitbed");
            Forum forum = new Forum("removeForum", admins);
            Forum forum2 = new Forum("removeForum2", admins);
            system.repository.dbAddForum(forum, false);
            system.repository.dbAddForum(forum2, false);
            dbContext.ChangeDatabaseTo("TestForumDBContext");
            Assert.IsTrue(dbContext.Forums.Any(o => o.Title == "removeForum"));
            system.repository.dbRemoveForum("removeForum", false);
            Assert.IsFalse(dbContext.Forums.Any(o => o.Title == "removeForum"));
        }
    }
}