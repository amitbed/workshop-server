using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ForumApplication.Models;

namespace ForumApplication.Controllers
{
    public class ApiSubForumController : ApiController
    {
        ForumSystem fs;

        public string Get(string forumName)
        {
            fs = ForumSystem.initForumSystem();
            Forum forum = fs.searchForum(forumName);
            return forum.displaySubforums();
        }
    }
}
