using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApplication.Models
{
    public class MemberSubForum : SubForum, IMemberSubForumManager
    {
        private object threadHandler;
        private object messageHandler;
        bool isProd = false;

        public MemberSubForum() { }
        public MemberSubForum(string title, List<string> moderators, string parent, int maxModerators)
            : base(title, moderators, parent, maxModerators)
        {
            this.threadHandler = new object();
            this.messageHandler = new object();
        }

        //This method creates a new thread with openning message
        public bool createThread(Message msg, string threadToAddName)
        {
            lock (threadHandler)
            {
                try
                {
                    if (threadToAddName != string.Empty && threadToAddName != null)
                    {
                        Thread threadToAdd = Threads[threadToAddName];
                        if (threadToAdd != null)
                        {
                            threadToAdd.Messages.Add(msg.Title, msg);
                            repository.dbAddMessage(msg, isProd);
                            Threads.Add(threadToAdd.Title, threadToAdd);
                            repository.dbAddThread(threadToAdd, isProd);
                            Logger.logDebug(string.Format("The new thread: {0} has been created successfully with id {1}", threadToAdd.Title, threadToAdd.ID));
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        Logger.logError("Failed to add thread. Reason: thread is null");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Logger.logError(e.StackTrace);
                    return false;
                }
            }
        }

        public bool addMessage(Message msg, string relatedThreadName)
        {
            lock (messageHandler)
            {
                try
                {
                    ForumSystem forumSystem = ForumSystem.initForumSystem();
                    string username = msg.UserName;
                    if (checkThreadAndMsg(relatedThreadName, msg.Title))
                    {
                        Thread currThread = Threads[relatedThreadName];
                        Member currMember = forumSystem.Members[username];
                        Logger.logDebug(string.Format("Member: {0} increase number of published messages", username));
                        currMember.NumOfPublishedMessages++;
                        currThread.Messages.Add(msg.Title, msg);
                        repository.dbAddMessage(msg, isProd);
                        Logger.logDebug(string.Format("Message: {0} has added to thread", msg.Title));
                        return true;
                    }
                    return false;
                }
                catch (Exception e)
                {
                    Logger.logError(e.StackTrace);
                    return false;
                }
            }
        }

        public Message createMessage(string title, string content, string username, string threadName)
        {
            Message msg = new Message(title, content, username);
            bool create = addMessage(msg, threadName);
            if (create == true)
                return msg;
            return null;
        }

        public bool addReply(Message msgReply, string ParentMsgTopic, string threadName)
        {
            lock (messageHandler)
            {
                try
                {
                    ForumSystem forumSystem = ForumSystem.initForumSystem();
                    string username = msgReply.UserName;
                    Member currMember = forumSystem.Members[username];
                    if (currMember != null)
                    {
                        Logger.logDebug(string.Format("Member: {0} increase number of published messages", username));
                        currMember.NumOfPublishedMessages++;
                        Thread currThread = Threads[threadName];
                        if (currThread != null)
                        {
                            currThread.Messages[ParentMsgTopic].Replies.Add(msgReply);
                            repository.dbAddMessage(msgReply, isProd);
                            Logger.logDebug(string.Format("Message: {0} has added as reply to msg {1}", msgReply.Title, ParentMsgTopic));
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception e)
                {
                    Logger.logError(e.StackTrace);
                    return false;
                }
            }

        }
        public bool fileComplaint(string moderatorUsername, string memberUsername)
        {
            //TODO: add implementation
            return true;
        }

        public bool removeMessage(string memberUsername, string threadName, string messageTopic)
        {
            lock (messageHandler)
            {
                try
                {
                    if (checkThreadAndMsg(threadName, messageTopic))
                    {
                        Thread currThread = Threads[threadName];
                        Message currMessage = currThread.Messages[messageTopic];

                        if (memberUsername.Equals(currMessage.UserName))
                        {
                            currMessage.delete();
                            currThread.Messages.Remove(messageTopic);
                            repository.dbRemoveMessage(messageTopic, isProd);
                            Logger.logDebug(string.Format("Message: {0} was succesfully removed", messageTopic));
                            return true;
                        }
                        else
                        {
                            Logger.logError(string.Format("Msg: {0} does not exist", messageTopic));
                            return false;
                        }
                    }
                    else
                    {
                        Logger.logError(string.Format("Thread: {0} does not exist", threadName));
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Logger.logError(e.StackTrace);
                    return false;
                }
            }
        }

        public bool editMessage(string memberUsername, string msgTopic, string msgContent, string threadName)
        {
            lock (messageHandler)
            {
                try
                {
                    if (checkThreadAndMsg(threadName, msgTopic))
                    {
                        Thread currThread = Threads[threadName];
                        Message currMessage = currThread.Messages[msgTopic];
                        if (memberUsername.Equals(currMessage.UserName))
                        {
                            currMessage.Content = msgContent;
                            // here we need to add query for edit msg
                            Logger.logDebug(string.Format("Message: {0} was succesfully edit", msgTopic));
                            return true;
                        }
                        else
                        {
                            Logger.logError(string.Format("Msg: {0} does not exist", msgTopic));
                            return false;
                        }
                    }
                    else
                    {
                        Logger.logError(string.Format("Thread: {0} does not exist", threadName));
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Logger.logError(e.StackTrace);
                    return false;
                }
            }
        }

        private bool checkThreadAndMsg(string threadName, string msgTopic)
        {
            return (Threads.ContainsKey(threadName) && Threads[threadName].Messages.ContainsKey(msgTopic));
        }
    }
}