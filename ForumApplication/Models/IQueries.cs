using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ForumApplication.Models
{
    interface IQueries
    {
        //Forums
        //Dictionary<string, Forum> dbGetForums();
        //void dbAddForum(Forum forum);
        //void dbRemoveForum(string forumID);
        //bool searchForum(forumName);

        //Sub forums
        //Dictionary<string, SubForum> dbGetSubForum();
        void dbRemoveSubForum(string subForumTitle, bool isProd);
        void dbAddSubForum(SubForum subForum, bool isProd);
        //bool searchSubForum(subForumName);

        //Threads
        //Dictionary<string, Thread> dbGetThreads();
        void dbRemoveThread(string threadTitle, bool isProd);
        void dbAddThread(Thread thread, bool isProd);
        //bool searchThread(ThreadName);

        //Messages
        //Dictionary<string, Message> dbGetMessages();
        void dbAddMessage(Message message, bool isProd);
        void dbRemoveMessage(string messageID, bool isProd);
        //bool searchMessage(topic,date,username);

        //Members
        //Dictionary<string, Member> dbGetMembers();              //This query retrieves all members from the DB
        void dbAddMember(Member member, bool isProd);                        //This query adds a new member to the DB
        void dbRemoveMember(string MemberID, bool isProd);                   //This query removes a member from the DB
        bool dbIsMemberExists(string username, bool isProd);   //returns true is username exists in DB

    }
}