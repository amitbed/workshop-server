using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApplication.Models
{
    public class SubForum
    {
        #region variables
        public string ID { get; set; }
        public string Title { get; set; }
        public Dictionary<string, Thread> Threads { get; set; }
        public List<string> Moderators { get; set; }
        public int MaxModerators { get; set; }
        private List<Member> members;
        private object threadHandler;

        #endregion

        public SubForum() { }
        //Overload Constructor
        public SubForum(string title, List<string> moderators, string parent, int maxModerators)
        {
            this.threadHandler = new object();
            if ((String.IsNullOrEmpty(title)) || (String.IsNullOrEmpty(parent)) || (moderators == null))
            {
                if (String.IsNullOrEmpty(title))
                {
                    Logger.logError("Failed to create a new sub-forum. Reason: title is empty");
                }

                if (String.IsNullOrEmpty(parent))
                {
                    Logger.logError("Failed to create a new sub-forum. Reason: parent is empty");
                }
                if (moderators == null)
                {
                    Logger.logError("Failed to create a new sub-forum. Reason: moderators is null");
                }
            }
            else
            {
                if (moderators.Count <= maxModerators)
                {
                    //this.ID = IdGen.generateSubForumId();
                    this.Threads = new Dictionary<string, Thread>();
                    this.Title = title;
                    this.MaxModerators = maxModerators + (getParentForum(parent)).Admins.Count;
                    this.Moderators = moderators;
                    Moderators.Concat((getParentForum(parent)).Admins);
                    this.members = new List<Member>();
                    Logger.logDebug(String.Format("A new sub-forum has been created. ID: {0}, title: {1}", this.ID, this.Title));
                }
                else
                {
                    Logger.logError("Failed to create a new sub-forum. Reason: too many moderators");
                }
            }
        }


        //Methods


        public Thread createThread(string title)
        {
            lock (this.threadHandler)
            {
                try
                {
                    Thread t = new Thread(title);
                    Threads.Add(t.Title, t);
                    ForumSystemRepository repository = new ForumSystemRepository();
                    repository.dbAddThread(t, false);
                    return t;
                }
                catch (Exception e)
                {
                    Logger.logError(e.StackTrace);
                    return null;
                }
            }
        }

        public bool isThreadExistsInSubForum(string threadTitle)
        {
            lock (this.threadHandler)
            {
                try
                {
                    foreach (Thread t in Threads.Values)
                    {
                        if (t.Title.Equals(threadTitle))
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

        public Forum getParentForum(string parentName)
        {
            lock (this.threadHandler)
            {
                try
                {
                    ForumSystem forumSystem = ForumSystem.initForumSystem();
                    if (forumSystem.Forums.ContainsKey(parentName))
                    {
                        return forumSystem.Forums[parentName];
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Logger.logError(e.StackTrace);
                    return null;
                }
            }
        }

        //This method displays a sub-forum's threads
        public string displayThreads()
        {
            lock (this.threadHandler)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string threadID in Threads.Keys)
                    {
                        sb.Append(threadID + ". " + Threads[threadID].Title);
                        sb.AppendLine();
                    }
                    string ans = sb.ToString();
                    Logger.logDebug(String.Format("displayThreads ", ans));
                    return ans;
                }
                catch (Exception e)
                {
                    Logger.logError(e.StackTrace);
                    return null;
                }
            }
        }

        public Thread SearchThread(string threadName)
        {
            lock (this.threadHandler)
            {
                try
                {
                    ForumSystem forumSystem = ForumSystem.initForumSystem();
                    Thread threadToFind = Threads[threadName];
                    if (threadToFind == null)
                    {
                        Logger.logError(String.Format("Failed to recieve thread {0}", threadName));
                        return null;
                    }
                    else
                    {
                        Logger.logDebug(String.Format("find thread {1} ", threadName));
                        return threadToFind;
                    }
                }
                catch (Exception e)
                {
                    Logger.logError(e.StackTrace);
                    return null;
                }
            }
        }

        public bool delete()
        {

            try
            {
                this.Threads = new Dictionary<string, Thread>();
                this.Title = null;
                this.Moderators = null;
                this.members = new List<Member>();
                Logger.logDebug(String.Format("subForum: {0} has been deleted", this.ID));
                this.ID = null;
                return true;
            }
            catch (Exception e)
            {
                Logger.logError(e.StackTrace);
                return false;
            }
        }
    }
}
