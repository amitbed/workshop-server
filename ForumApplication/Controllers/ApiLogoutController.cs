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
        public void Post(List<string> args)
        {
            string username = args.ElementAt(0);
            string password = args.ElementAt(1);
            Guest guest = new Guest();
            guest.login(username, password);
        }
    }
}
