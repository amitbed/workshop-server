using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumApplication;
using ForumApplication.Models;
using System.Linq;
using System.Collections.Generic;

namespace unitTestingDevelopers
{

    [TestClass]
    public class MemberUT
    {
        ForumSystem system = ForumSystem.initForumSystem();
        bool isProd = false;

        [TestMethod]
        public void checkMemberCreationAndRemovingFromDB()
        {
            Member CheckingMember = system.addMember("checking", "checkingPassword", "checking@bgu.ac.il");
            Assert.IsTrue(system.Members.ContainsKey("checking"));
            Assert.IsTrue(system.repository.dbIsMemberExists("checking", isProd));
            system.repository.dbRemoveMember("checking", isProd);
        }

        [TestMethod]
        public void checkMemberCreationInDBFalseUnitTest()
        {
            Member CheckingMember = system.addMember("checking", "checkingPassword", "checking@bgu.ac.il");
            Assert.IsTrue(system.Members.ContainsValue(CheckingMember));
            Assert.IsFalse(system.repository.dbIsMemberExists("notShouldBeInDB", isProd));
            system.repository.dbRemoveMember("checking", isProd);

        }

        [TestMethod]
        public void checkMemberCorrectInsertionOfEmailToDB()
        {
            Member CheckingMember = system.addMember("checking", "checkingPassword", "checking@bgu.ac.il");
            var dbContext = new ForumDBContext();
            Assert.IsTrue(dbContext.Members.Any(o => o.Email == "checking@bgu.ac.il"));
            system.repository.dbRemoveMember("checking", isProd);
        }

        [TestMethod]
        public void checkMemberCorrectInserionOfPasswordToDB()
        {
            Member CheckingMember = system.addMember("checking", "checkingPassword", "checking@bgu.ac.il");
            var dbContext = new ForumDBContext();
            Assert.IsTrue(dbContext.Members.Any(o => o.Password == "checkingPassword"));
            system.repository.dbRemoveMember("checking", isProd);
        }

        [TestMethod]
        public void checkMemberCorrectInsertionOfOtherFieldsToCache()
        {
            Member CheckingMember = system.addMember("checking", "checkingPassword", "checking@bgu.ac.il");
            string passQues = "What is the name of your first dog?";
            string passAns = "Tipex";
            CheckingMember.PasswordQuestion.Add(passQues, passAns);
            Assert.IsNotNull(CheckingMember.PasswordQuestion);
        }


        [TestMethod]
        public void checkMemberTypeRegular()
        {
            Member CheckingMember = system.addMember("ifateli", "gilAd", "ifateli@bgu.ac.il");
            bool ans = CheckingMember.MemberType == (int)Types.Regular;
            Assert.IsTrue(ans);
        }

        [TestMethod]
        public void checkMemberTypeSilver()
        {
            Member CheckingMember = system.addMember("ifateli", "gilAd", "ifateli@bgu.ac.il");
            CheckingMember.upgrade();
            bool ans = CheckingMember.MemberType == (int)Types.Silver;
            Assert.IsTrue(ans);
        }

        [TestMethod]
        public void checkMemberTypeGold()
        {
            Member CheckingMember = system.addMember("ifateli", "gilAd", "ifateli@bgu.ac.il");
            CheckingMember.upgrade();
            CheckingMember.upgrade();
            bool ans = CheckingMember.MemberType == (int)Types.Gold;
            Assert.IsTrue(ans);
        }

        [TestMethod]
        public void checkMemberPassword()
        {
            Member CheckingMember = system.addMember("ifateli", "gilAd", "ifateli@bgu.ac.il");
            bool ans = CheckingMember.changePassword("Neta");
            Assert.IsTrue(ans);
        }

        [TestMethod]
        public void checkMemberBadPassword()
        {
            Member CheckingMember = system.addMember("ifateli", "gilAd", "ifateli@bgu.ac.il");
            bool ans = CheckingMember.changePassword("gilAd");
            Assert.IsFalse(ans);
        }

        [TestMethod]
        public void checkMemberCapsLockPassword()
        {
            Member CheckingMember = system.addMember("ifateli", "gilAd", "ifateli@bgu.ac.il");
            bool ans = CheckingMember.changePassword("IFAT");
            Assert.IsTrue(ans);
        }

        [TestMethod]
        public void checkMemberNonCapsLockPassword()
        {
            Member CheckingMember = system.addMember("ifateli", "gilAd", "ifateli@bgu.ac.il");
            bool ans = CheckingMember.changePassword("gilad");
            Assert.IsTrue(ans);
        }
    }
}