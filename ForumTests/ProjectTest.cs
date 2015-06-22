using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumApplication.Models;

namespace ForumTests
{
    [TestClass]
    public class ProjectTest
    {
        private BridgeProject bridge = Driver.getBridge();
        protected ForumSystem system = ForumSystem.initForumSystem();
        List<Member> testMembers = new List<Member>();
        protected Member Sagi;
        protected Member Amit;
        protected Member Dean;
        public virtual void SetUp()
        {
            setUpMembers();
            setUpForum();
        }

        private void setUpMembers()
        {
            Sagi = bridge.createMember("sagiav", "maihayafa", "sagiav@post.bgu.ac.il");
            Amit = bridge.createMember("amitbed", "ronahayafa", "amitbed@post.bgu.ac.il");
            Dean = bridge.createMember("abadie", "liatush", "abadie.post@post.bgu.ac.il");
            testMembers.Add(Sagi);
            testMembers.Add(Amit);
            testMembers.Add(Dean);
        }

        private void setUpForum()
        {
            List<string> adminDating = new List<string>();
            adminDating.Add(Sagi.Username);
            List<string> adminFood = new List<string>();
            adminFood.Add(Amit.Username);
            List<string> adminSport = new List<string>();
            adminFood.Add(Dean.Username);
            Forum Dating = bridge.createForum("Dating", adminDating);
            Forum Food = bridge.createForum("Food", adminFood);
            Forum Sport = bridge.createForum("Sport", adminSport);
        }

        public Forum createForum(string title, List<string> admins)
        {
            return bridge.createForum(title, admins);
        }

        public SubForum createSubForum(string title, List<string> moderators, string parent, int maxMod)
        {
            return bridge.createSubForum(title, moderators, parent, maxMod);
        }

        public Forum searchForum(string forumName)
        {
            return system.searchForum(forumName);
        }
        
        public SubForum setUpSubForum(string title, List<string> moderators, string parent, int maxModerators)
        {
            return bridge.createSubForum(title, moderators, parent, maxModerators);
        }
        
        public void Register(Guest g, string username, string password, string email)
        {
            bridge.register(g, username, password, email);
        }

        public void removeSubForum(string sfName, string forumName)
        {
            bridge.removeSubForum(sfName, forumName);
        }

        public string login(Guest g, string username, string password)
        {
            return bridge.login(g, username, password);
        }

        public bool isMemberExist(string username)
        {
            return system.isUsernameExists(username);
        }
        public Member CreateMember(string username, string password, string email)
        {
            return bridge.createMember(username, password, email);
        }

        public Thread createThread(string title, string subParent, string forum)
        {
            return bridge.createThread(title, subParent, forum);
        }

        public bool isThreadExistsInSubForum(string title, string sf, string f)
        {
            return bridge.isThreadExistsInSubForum(title, sf, f);

        }

        public bool subForumInForum(List<SubForum> subForums, Forum forum)
        {
            bool ans = true;
            Dictionary<string, SubForum> listToCheck = forum.SubForums;
            foreach (SubForum sub in subForums)
            {
                if (!listToCheck.ContainsKey(sub.Title)) //Contains(sub))
                    ans = false;
            }

            return ans;
        }

        public bool IsSubForumExists(string subForumName, string forumName)
        {
            return IsSubForumExists(subForumName, forumName);
        }

        public bool isGuestRegistered(string guestName)
        {
            bool ans = false;
            ans = bridge.queryIsMemberExists(guestName);
            return ans;
        }

        public Member getMember(string memberUsername)
        {
            return bridge.getMember(memberUsername);
        }

        public string displayForum()
        {
            return bridge.displayForums();
        }

        public string displaySubforums(Member member, string forumName)
        {
            return bridge.displaySubforums(member,forumName);
        }

        public SubForum enterSubForum(Member member,string sfName, string fName)
        {
            return bridge.enterSubForum(member,sfName,fName);
        }



        public string displayThreads(Member member, string subForumName, string forumName)
        {
            return bridge.displayThreads(member, subForumName, forumName);
        }

        public void addSubForumToForumByAdmin(SubForum sf, string currForum, ModeratorSubForum currMods)
        {
            bridge.addSubForumToForumByAdmin(sf,currForum,currMods);
        }

        public string displayMessages(Member member, string ThreadName, string sfName, string fName)
        {
            return bridge.displayMessages(member, ThreadName,sfName,fName);
        }

        public void changeSettings(Member member, string username, string password, string email)
        {
            bridge.changeSettings(member,username, password, email);
        }

        public Message createMessage(Member member, string msgTitle, string content, string threadName, string sfName, string fName)
        {
            return bridge.createMessage(member, msgTitle,content, threadName, sfName, fName);
        }

        public Member searchMember(string username)
        {
            return bridge.searchMember(username);
        }

        public void addAdminToForum(string forumName, string memberUsername)
        {
            bridge.addAdminToForum(forumName, memberUsername);
        }
        public bool isAdminInForum(string forumName, string memberUsername)
        {
            return bridge.isAdminInForum(forumName, memberUsername);
        }

        public bool addModeratorToSubForum(string forumName, string subForumName, string moderatorUsername)
        {
            return bridge.addModeratorToSubForum(forumName, subForumName, moderatorUsername);
        }

        public void removeForum(string forumName)
        {
            bridge.removeForum(forumName);
        }

        public bool isSubForumExistInSystem(string subForumName)
        {
            foreach (string f in system.Forums.Keys)
            {
                if (bridge.IsSubForumExists(subForumName, f) == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
