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

   //   [TestInitialize]
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

        [TestMethod]
        public void VerifyThreadDeleteAfterSubForumDelete()
        {
            List<string> moderators = new List<string>();
            moderators.Add("sagiav");
            SubForum Kriket = createSubForum("Kriket", moderators, "Sport", 3);
            Thread KochinKriket = createThread("KochinKriket", "Kriket", "Sport");
            Thread AvasKriket = createThread("AvasKriket", "Kriket", "Sport");
            Assert.IsNotNull(Kriket);
            Assert.IsNotNull(KochinKriket);
            Assert.IsNotNull(AvasKriket);
            removeSubForum("Kriket", "Sport");
            Assert.IsTrue(IsSubForumExists("Kriket","Sport")== false);
            Assert.IsNull(Kriket);
            Assert.IsNull(KochinKriket);
            Assert.IsNull(AvasKriket);
        }

        [TestMethod]
        public void VerifyMessagesDeleteAfterThreadDelete()
        {
            List<string> moderators = new List<string>();
            moderators.Add("sagiav");
            SubForum Frisbee = createSubForum("Frisbee", moderators, "Sport", 3);
            Thread KochinFrisbee = createThread("KochinFrisbee", "Frisbee", "Sport");
            Thread AvasFrisbee = createThread("AvasFrisbee", "Frisbee", "Sport");
            Message KochinTalk = createMessage(Sagi,"KochinTalk","content of this", "KochinFrisbee", "Frisbee", "Sport");
            Assert.IsNotNull(Frisbee);
            Assert.IsNotNull(KochinFrisbee);
            Assert.IsNotNull(AvasFrisbee);
            Assert.IsNotNull(KochinTalk);
            removeSubForum("Frisbee", "Sport");
            Assert.IsTrue(IsSubForumExists("Frisbee", "Sport") == false);
            Assert.IsNull(Frisbee);
            Assert.IsNull(KochinFrisbee);
            Assert.IsNull(AvasFrisbee);
            Assert.IsNull(KochinTalk);
        }

        [TestMethod]
        public void addAndDeleteSubForum()
        {
            List<string> moderators = new List<string>();
            moderators.Add("sagiav");
            List<SubForum> FoodSubs = new List<SubForum>();
            SubForum ItalianRecepies = createSubForum("ItalianRecepies", moderators, "Food", 1);
            FoodSubs.Add(ItalianRecepies);
            Forum foodForum = searchForum("Food");
            Assert.IsTrue(foodForum.SubForums.ContainsKey("ItalianRecepies"));

            removeSubForum("ItalianRecepies", "Food");
            Assert.IsFalse(foodForum.SubForums.ContainsKey("ItalianRecepies"));
        }

        [TestMethod]
        public void verifySuperAdminIsForumAdmin()
        {
            Member superAdmin = searchMember("superAdmin");
            List<string> admins = new List<string>();
            admins.Add("sagiav");
            createForum("Reality Shows",admins);
            Forum currForum = searchForum("Reality Shows");
            Assert.IsTrue(isAdminInForum("Reality Shows", superAdmin.Username));
        }

        [TestMethod]
        public void addForumWithNoAdmins()
        {
            int prevNumOfForums = system.Forums.Count;
            List<string> adminSport = new List<string>();
            Forum Sport1 = createForum("Sport1", adminSport);
            int newNumOfForums = system.Forums.Count;
            Assert.IsNotNull(Sport1);
            Assert.AreEqual<int>(newNumOfForums, prevNumOfForums + 1);
        }

        [TestMethod]
        public void addForumWithBadTitle()
        {
            int prevNumOfForums = system.Forums.Count;
            List<string> adminSport = new List<string>();
            adminSport.Add("abadie");
            Forum forum1 = createForum("DROP TABLE", adminSport);
            Assert.IsNull(forum1);
            Assert.AreEqual<int>(system.Forums.Count, prevNumOfForums);
            Forum forum2 = createForum("INSERT", adminSport);
            Assert.IsNull(forum2);
            Assert.AreEqual<int>(system.Forums.Count, prevNumOfForums);
            Forum forum3 = createForum("dfsfsf--", adminSport);
            Assert.IsNull(forum3);
            Assert.AreEqual<int>(system.Forums.Count, prevNumOfForums);
            Forum forum4 = createForum("UPDATE", adminSport);
            Assert.IsNull(forum4);
            Assert.AreEqual<int>(system.Forums.Count, prevNumOfForums);
        }

        [TestMethod]
        public void superAdminAddsAdmin()
        {
            addAdminToForum("Sport", "sagiav");
            Assert.IsTrue(isAdminInForum("sports","sagiav"));
        }

        [TestMethod]
        public void addMoreThanMaxModeratorsToSubForum()
        {
            createForum("RealityShows", new List<string>());
            createSubForum("TheBigBrother", new List<string>(), "RealityShows", 1);
            addModeratorToSubForum("RealityShows", "TheBigBrother", "sagiav");
            Assert.IsFalse(addModeratorToSubForum("RealityShows", "TheBigBrother", "amitbed"));
        }

        [TestMethod]
        public void checkDeleteNonExistingForum()
        {
            List<string> admins = new List<string>();
            admins.Add("abadie");
            admins.Add("amitbed");
            Forum forum = createForum("forumToDelete", admins);
            string listOfForums = displayForum();
            Assert.IsTrue(listOfForums.Contains("forumToDelete"));
            removeForum("forumToDelete");
            Assert.IsFalse(listOfForums.Contains("forumToDelete"));
        }

        [TestMethod]
        public void checkCorrectAdminsInForum()
        {
            List<string> admins = new List<string>();
            admins.Add("abadie");
            admins.Add("amitbed");
            Forum forum = createForum("forumToCheck", admins);
            string listOfForums = displayForum();
            Assert.IsTrue(listOfForums.Contains("forumToCheck"));
            Assert.IsTrue(isAdminInForum(forum.Title, "abadie"));
            Assert.IsTrue(isAdminInForum(forum.Title, "amitbed"));
        }


    }


}
