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

        public string Get()
        {
            fs = ForumSystem.initForumSystem();
            return fs.displayForums();
        }

        public void Post(List<string> args)
        {
            args.Reverse();
            string title = args.First();
            args.Remove(title);
            string username = args.First();
            args.Remove(username);
            List<string> admins = args;
            fs = ForumSystem.initForumSystem();
            //check if there is no other forum with this name 
            Forum forum = new Forum(title, admins);
            fs.createForum(forum, username);
        }
    }
}
