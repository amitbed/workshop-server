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

        //return list of all the members
        public List<string> Get()
        {
            fs = ForumSystem.initForumSystem();
            return fs.displayMembers();
        }

        //register new member
        public void Post(List<string> args)
        {
            string username = args.ElementAt(0);
            string password = args.ElementAt(1);
            string email = args.ElementAt(2);
            Guest guest = new Guest();
            guest.register(username, password, email);
        }

        //check if user is admin in the forum
        //public bool Get(List<string> args)
        //{
        //    fs = ForumSystem.initForumSystem();
        //    string username = args.ElementAt(0);
        //    string forumTitle = args.ElementAt(1);
        //    if(fs.Members.ContainsKey(username)){
        //        Member mem = fs.Members[username];
        //        return (mem.MyForums.Contains(forumTitle));
        //    }
        //    else{
        //        return false;
        //    }
        //}
    }
}
