using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ForumApplication.Models;

namespace ForumApplication.Controllers
{
    public class ApiLogoutController : ApiController
    {
        //
        // GET: /ApiLogout/
        public bool Post(List<string> username)
        {
            ForumSystem fs;
            fs = ForumSystem.initForumSystem();
            if (fs.Members.ContainsKey(username.ElementAt(0)))
            {
                Member mem = fs.Members[username.ElementAt(0)];
                mem.logout();
            }
            return true;
        }

    }
}
