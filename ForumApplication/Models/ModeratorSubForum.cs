using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApplication.Models
{
    public class ModeratorSubForum : MemberSubForum, IModeratorManager
    {
        List<string> moderators;
        bool isProd = false;
        ForumSystemRepository repository;

        public ModeratorSubForum(string title, List<string> moderators, string parent, int maxModerators)
            : base(title, moderators, parent, maxModerators)
        {
            this.moderators = moderators;
            this.repository = new ForumSystemRepository();
        }

        public bool removeThread(string threadName)
        {
            try
            {
                Thread currThread = Threads[threadName];
                if (currThread != null)
                {
                    currThread.delete();
                    Threads.Remove(threadName);
                    repository.dbRemoveThread(threadName, isProd);
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
}