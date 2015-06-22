using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ForumApplication.Models;

namespace ForumApplication.Controllers
{
    public class ApiForumController : ApiController
    {
        ForumSystem fs;

        //returns string of all the forums
        public string Get()
        {
            fs = ForumSystem.initForumSystem();
            return fs.displayForums();
        }

        //add new forum to the system
        public bool Post(List<string> args)
        {
            try
            {
                args.Reverse();
                string title = args.First();
                args.Remove(title);
                string username = args.First();
                args.Remove(username);
                List<string> admins = args;
                fs = ForumSystem.initForumSystem();
                fs.createForum(title, username, admins);
                return true;
            }
            catch(Exception e)
            {
                Logger.logError(e.StackTrace);
                return false;
            }
        } 
    }
}
