using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ForumApplication.Models;
namespace ForumApplication.Controllers
{
    public class ApiAdminController : ApiController
    {
        //check if user is admin in the forum
        public bool Get(List<string> args)
        {
            ForumSystem fs = ForumSystem.initForumSystem();
            string username = args.ElementAt(0);
            string forumTitle = args.ElementAt(1);
            if (fs.Members.ContainsKey(username))
            {
                Member mem = fs.Members[username];
                return (mem.MyForums.Contains(forumTitle));
            }
            else
            {
                return false;
            }
        }
    }
}
