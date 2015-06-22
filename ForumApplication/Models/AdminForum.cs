using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApplication.Models
{
    public class AdminForum : Forum, IAdminManager
    {
        Forum parentforum;
        public int MaxModerators { get; set; }
        object forumHandler;

        public AdminForum(Forum forum)
        {
            parentforum = forum;
            this.forumHandler = new object();

        }

        public AdminForum()
        {
            // TODO: Complete member initialization
        }

        public bool setProperties(int moderatorNumber)
        {
            try
            {
                foreach (SubForum s in parentforum.SubForums.Values)
                {
                    s.MaxModerators = moderatorNumber;
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

        public bool addSubForum(SubForum subForum, MemberSubForum memberSubForum, ModeratorSubForum moderatorSubForum)
        {
            lock (this.forumHandler)
            {
                try
                {
                    if (subForum != null && memberSubForum != null && moderatorSubForum != null)
                    {
                        //MemberSubForum msf = new MemberSubForum(subForum.Title, subForum.Moderators, this.Title, subForum.MaxModerators);
                        //SubForums.Add(subForum.Title, subForum);
                        //MemberSubForums.Add(subForum.Title, msf);
                        //ModeratorSubForums.Add(subForum.Title, new ModeratorSubForum(msf));

                        base.SubForums.Add(subForum.Title, subForum);
                        base.MemberSubForums.Add(memberSubForum.Title, memberSubForum);
                        base.ModeratorSubForums.Add(moderatorSubForum.Title, moderatorSubForum);
                        Logger.logDebug(string.Format("The new sub forum: {0} has been created successfully with id {1}", subForum.Title, subForum.ID));
                        return true;
                    }
                    else
                    {
                        Logger.logError("Failed to add sub forum. Reason: sub forum is null");
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

       
        public bool removeSubForum(string subForumName)
        {
            lock (forumHandler)
            {
                try
                {
                    SubForum currSubForum = SubForums[subForumName];
                    if (currSubForum != null)
                    {
                        currSubForum.delete();
                        SubForums.Remove(subForumName);
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

        public bool upgradeMember(string memberID)
        {
            try
            {
                //TODO: add implementation
                return true;
            }
            catch (Exception e)
            {
                Logger.logError(e.StackTrace);
                return false;
            }
        }

        public bool downgradeMember(string memberID)
        {
            try
            {
                //TODO: add implementation
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
