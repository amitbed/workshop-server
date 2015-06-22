using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumApplication.Models;
using System.Collections.Generic;

namespace ForumTests
{
    [TestClass]
    public class TestForum : ProjectTest
    {
        private Forum Dating, Food, Sport;
        private Member SagiTest, AmitTest, DeanTest;


        public override void SetUp()
        {
            base.SetUp();
            setUpForum();
        }

        private void setUpForum()
        {
            Dating = searchForum("Dating");
            Food = searchForum("Food");
            Sport = searchForum("Sport");
            SagiTest = getMember("sagiav");
            AmitTest = getMember("amitbed");
            DeanTest = getMember("abadie");

        }

        //UC1 - init Forum
        [TestMethod]
        public void initForumTest()
        {
            SetUp();
            Assert.IsNotNull(system);
            Assert.IsNotNull(Dating);
            Assert.IsNotNull(Food);
            Assert.IsNotNull(Sport);
            Assert.IsNotNull(Sagi);
            Assert.IsNotNull(Amit);
            Assert.IsNotNull(Dean);
            Assert.IsTrue(isGuestRegistered("sagiav"));
            Assert.IsTrue(isGuestRegistered("amitbed"));
            Assert.IsTrue(isGuestRegistered("abadie"));
        }

        //UC2 - Create Forum
        [TestMethod]
        public void isSystemDuplicatedTest()
        {
            Assert.AreEqual<int>(4, system.Forums.Count);
        }

        [TestMethod]
        public void addForumTest()
        {
            int prevNumOfForums = system.Forums.Count;
            List<string> adminSport = new List<string>();
            adminSport.Add("abadie");
            Forum Sport1 = createForum("Sport1", adminSport);
            int newNumOfForums = system.Forums.Count;
            Assert.IsNotNull(Sport1);
            Assert.AreEqual<int>(newNumOfForums, prevNumOfForums + 1);
        }
        //UC3 - setProperties
        //UC4 - enter forum
        //UC5 - register
        //UC6 - login / logout
        [TestMethod]
        public void registerAndLoginTest()
        {
            Guest Nofar = new Guest();
            Register(Nofar, "benshnof", "matanShoham", "benshnof@post.bgu.ac.il");
            Assert.IsTrue(isGuestRegistered("benshnof"));
            //Assert.IsFalse(isGuestRegistered("nofar"));
            //make a new is false test: write method IsNotRegisterd
            string forumList = login(Nofar, "benshnof", "matanShoham");
            Assert.IsNotNull(forumList);
            Assert.IsTrue(isMemberExist("benshnof"));
        }

        [TestMethod]
        public void loginFalseTest()
        {

            Guest Ifat = new Guest();
            login(Ifat, "ifateli", "bla");
            Assert.IsFalse(isMemberExist("ifateli"));
        }

        //UC7 - Create SubForum
        [TestMethod]
        public void AddAndDisplayNewSubForumTest()
        {
            List<string> moderators = new List<string>();
            moderators.Add("sagiav");
            List<SubForum> FoodSubs = new List<SubForum>();
            SubForum PassoverRecepies = createSubForum("PassoverRecepies", moderators, "Food", 3);
            SubForum ChosherRecepies = createSubForum("ChosherRecepies", moderators, "Food", 3);
            FoodSubs.Add(PassoverRecepies);
            FoodSubs.Add(PassoverRecepies);
            Assert.IsTrue(subForumInForum(FoodSubs, system.searchForum("Food")));
        }

        [TestMethod]
        public void AddNewSubForumWithWrongForumNameTest()
        {
            List<string> moderators = new List<string>();
            moderators.Add("sagiav");
            List<SubForum> FoodSubs = new List<SubForum>();
            SubForum ItalianRecepies = createSubForum("ItalianRecepies", moderators, "Food", 3);
            FoodSubs.Add(ItalianRecepies);
            Forum f = system.searchForum("Sport");
            Assert.IsFalse(subForumInForum(FoodSubs, f));
        }

        [TestMethod]
        public void SubForumAddedOnlyToNeededForumTest()
        {
            List<string> moderators = new List<string>();
            moderators.Add("sagiav");
            List<SubForum> FoodSubs = new List<SubForum>();
            SubForum IndianRecepies = createSubForum("IndianRecepies", moderators, "Food", 1);
            FoodSubs.Add(IndianRecepies);
            Forum f = system.searchForum("Dating");
            Assert.IsFalse(subForumInForum(FoodSubs, f));
        }

        [TestMethod]
        public void nonAdminAddSubForumTest()
        {
            Guest NofarGuest = new Guest();
            Register(NofarGuest, "benshnof", "matanShoham", "benshnof@post.bgu.ac.il");
            Member Nofar = getMember("benshnof");
            Forum currForum = system.enterForum(Nofar, "Food");
            AdminForum tempAdminType = new AdminForum();
            Assert.IsNotInstanceOfType(Nofar, tempAdminType.GetType());
        }

        //UC8- view sub forums list
        //UC14- delete message
        [TestMethod]
        public void deleteMessageFormTheOwnerOfTheMessage()
        {
            Member a = new Member("sdf", "cvxcv", "dsf@dff.com");
            Message m = new Message("title", "content", "sdf");
            //continue
        }

        [TestMethod]
        public void addAndDisplayForumTest()
        {
            List<string> admins = new List<string>();
            admins.Add("abadie");
            admins.Add("amitbed");
            Forum Sport = createForum("Dancing", admins);
            Assert.IsNotNull(Sport);
            string listOfForums = displayForum();
            Assert.IsTrue(listOfForums.Contains("Dancing"));
        }

        [TestMethod]
        public void addAndDisplayThreadTest()
        {
            List<string> moderators = new List<string>();
            moderators.Add("sagiav");
            SubForum BaskettBall = createSubForum("BaskettBall", moderators, "Sport", 3);
            Assert.IsNotNull(BaskettBall);
            Thread Lakers = createThread("Lakers", "BaskettBall", "Sport");
            Assert.IsNotNull(Lakers);
            //string listOfSubForums = displaySubforums(Sagi,"Sport");
            //Assert.IsTrue(listOfSubForums.Contains("BaskettBall"));
        }

        [TestMethod]
        public void addAndDisplayThreadFalseTest()
        {
            List<string> moderators = new List<string>();
            moderators.Add("sagiav");
            SubForum Soccer = createSubForum("Soccer", moderators, "Sport", 3);
            Assert.IsNotNull(Soccer);
            Thread MaccabiTLV = createThread("MaccabiTLV", "Soccer", "Sport");
            Assert.IsNotNull(MaccabiTLV);
            Assert.IsFalse(isThreadExistsInSubForum("MaccabiTLV", "ChosherRecepies", "Food"));
            //string listOfSubForums = displaySubforums(Sagi,"Sport");
            //Assert.IsTrue(listOfSubForums.Contains("BaskettBall"));
        }

        [TestMethod]
        public void addAndDisplayThreadHalfFalseTest()
        {
            List<string> moderators = new List<string>();
            moderators.Add("sagiav");
            SubForum Tenis = createSubForum("Tenis", moderators, "Sport", 3);
            Assert.IsNotNull(Tenis);
            Thread TenisForFun = createThread("TenisForFun", "Tenis", "Sport");
            Assert.IsNotNull(TenisForFun);
            //string listOfSubForums = displaySubforums(Sagi,"Sport");
            //Assert.IsTrue(listOfSubForums.Contains("BaskettBall"));
        }

        [TestMethod]
        public void addAndDisplayThreadTest1()
        {
            List<string> moderators = new List<string>();
            moderators.Add("sagiav");
            SubForum BaskettBall = createSubForum("BaskettBall", moderators, "Sport", 3);
            Thread Lakers = createThread("Lakers", "BaskettBall", "Sport");
            Assert.IsNotNull(Lakers);
            string listOfForums = displayForum();
            Assert.IsTrue(listOfForums.Contains("Dancing"));
        }

    }


}
