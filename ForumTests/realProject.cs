﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumApplication;
using ForumApplication.Models;
using System.Data.Entity;

namespace ForumTests
{
    public class realProject : BridgeProject
    {
        ForumSystem system = ForumSystem.initForumSystem();
        bool isProd = false;

        public Forum createForum(string title, List<string> admins)
        {
            Forum f = system.createForum(title, "superAdmin", admins);
            return f;
        }

        public Member createMember(string username, string password, string email)
        {
            Guest g = new Guest();
            string memberID = g.register(username, password, email);
            return system.addMember(username, password, email);
        }

        public void removeSubForum(string sfName, string forumName)
        {
            Forum f = system.searchForum(forumName);
            SubForum sf = f.SearchSubForum(sfName);

            //TODO:
        }

        public string login(Guest g, string username, string password)
        {
            return g.login(username, password);
        }


        public void register(Guest g, string username, string password, string email)
        {
            g.register(username, password, email);
        }


        public bool IsSubForumExists(string subForumName, string forumName)
        {
            Forum forum = system.Forums[forumName];
            return (forum.SubForums[subForumName] != null);
        }

        public Member getMember(string userName)
        {
            if (system.Members.ContainsKey(userName))
            {
                return system.Members[userName];
            }
            return null;
        }


        public string displayForums()
        {
            return system.displayForums();
        }


        public SubForum createSubForum(string title, List<string> moderators, string parent, int maxMod)
        {
            return new SubForum(title, moderators, parent, maxMod);
        }

        public string displaySubforums(Member member, string forumName)
        {
            Forum f = system.enterForum(member, forumName);
            return f.displaySubforums();
        }


        public SubForum enterSubForum(Member member, string sfName, string fName)
        {
            Forum f = system.enterForum(member, fName);
            return f.enterSubForum(sfName, member);
        }

        public string displayThreads(Member member, string subForumName, string currForum)
        {
            Forum f = system.searchForum(currForum);
            SubForum sf = f.enterSubForum(subForumName, member);
            return sf.displayThreads();
        }

        public void addSubForumToForumByAdmin(SubForum sf, string currForum, ModeratorSubForum arrayOfModerators)
        {
            AdminForum f = (AdminForum)system.searchForum(currForum);
            f.addSubForum(sf, null, null);
        }

        public string displayMessages(Member member, string threadName, string sfName, string fName)
        {
            Forum f = system.searchForum(fName);
            SubForum sf = f.enterSubForum(sfName, member);
            Thread thread = sf.SearchThread(threadName);
            return thread.displayMessages();
        }


        public void changeSettings(Member member, string username, string password, string email)
        {
            throw new NotImplementedException();
            //system.Members[member.Username].changeSettings(username, password, email);
        }

        public Thread createThread(string title, string subForumParent, string forumParent)
        {
            Forum f = system.searchForum(forumParent);
            SubForum sf = f.SearchSubForum(subForumParent);
            return sf.createThread(title, subForumParent);
        }


        public bool isThreadExistsInSubForum(string title, string sf, string f)
        {
            Forum forum = system.searchForum(f);
            SubForum sub = forum.SearchSubForum(sf);
            return sub.isThreadExistsInSubForum(title);
        }


        public bool queryIsMemberExists(string guestName)
        {
            bool ans = system.repository.dbIsMemberExists(guestName, isProd);
            return ans;
        }


        public Message createMessage(Member member, string msgTitle, string content, string threadName, string sfName, string fName)
        {
            Forum f = system.searchForum(fName);
            SubForum sf = f.SearchSubForum(sfName);
            MemberSubForum msf = (MemberSubForum)enterSubForum(member, sfName, fName);
            Message msg = msf.createMessage(msgTitle, content, member.Username, threadName);
            return msg;

        }

        public bool isAdminInForum(string forumName, string memberUsername)
        {
            Forum currForum = system.searchForum(forumName);
            return (currForum.Admins.Contains(memberUsername));
        }

        public void removeForum(string forumToDelete)
        {
            system.removeForum(forumToDelete, isProd);
        }

        public Member searchMember(string username)
        {
            if (system.Members.ContainsKey(username))
            {
                return system.Members[username];
            }
            else return null;
        }

        public void addAdminToForum(string forumName, string memberUsername)
        {
            Forum forum = system.searchForum(forumName);
            forum.Admins.Add(memberUsername);
        }

        public bool addModeratorToSubForum(string forumName, string subForumName, string moderatorUsername)
        {
            Forum forum = system.searchForum(forumName);
            SubForum sf = forum.SearchSubForum(subForumName);
            if (!sf.Moderators.Contains(moderatorUsername))
            {
                return sf.addModerator(moderatorUsername);
            }
            else
            {
                return false;
            }
        }

        //public void addSubForumToForumByAdmin(string forumName, string memberUsername)
        //{
        //    throw new NotImplementedException();
        //}
    }
}