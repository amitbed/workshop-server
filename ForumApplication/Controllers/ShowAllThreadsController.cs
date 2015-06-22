using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumApplication.Controllers
{
    public class ShowAllThreadsController : Controller
    {
        //
        // GET: /ShowAllThreads/

        public ActionResult ShowAllThreads()
        {
            return View();
        }

        public ActionResult CreateThread()
        {
            return View();
        }
    }
}
