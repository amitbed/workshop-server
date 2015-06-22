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
    }
}
