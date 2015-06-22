using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ForumApplication.Models;

namespace ForumApplication.Controllers
{
    public class ApiMemberController : ApiController
    {
        ForumSystem fs;

        public List<string> Get()
        {
            fs = ForumSystem.initForumSystem();
            return fs.displayMembers();
        }

        public void Post(List<string> args)
        {
            string username = args.ElementAt(0);
            string password = args.ElementAt(1);
            string email = args.ElementAt(2);
            Guest guest = new Guest();
            guest.register(username, password, email);
        }
    }
}
