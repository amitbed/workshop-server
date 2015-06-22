using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ForumApplication.Models;

namespace ForumApplication.Controllers
{
    public class ApiLoginController : ApiController
    {
        ForumSystem fs;
        //login
        public bool Post(List<string> args)
        {
            string username = args.ElementAt(0);
            string password = args.ElementAt(1);
            Guest guest = new Guest();
            if (guest.login(username, password) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //logout
        public bool Post(string username)
        {
            fs = ForumSystem.initForumSystem();
            if (fs.Members.ContainsKey(username))
            {
                Member mem = fs.Members[username];
                mem.logout();
            }
            return true;
        }

    }
}
