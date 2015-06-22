using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumApplication.Models;

namespace ForumApplication.Controllers
{
    public class CreateThreadController : Controller
    {
        //
        // GET: /CreateThread/

        public ActionResult CreateThread()
        {
            return View();
        }

        public ActionResult CreateNewThread()
        {
            string title = Request["Threadtitle"].ToString();
            string subforum = Request["SubForum"].ToString();
            string forum = Request["Forum"].ToString();
            string content = Request["Content"].ToString();

            ForumSystem fs = ForumSystem.initForumSystem();
            Thread t = fs.Forums[forum].SubForums[subforum].createThread(title, subforum);
            t.Messages.Add("first message", new Message(title, content, "amit"));
            this.ViewData["id"] = forum + " " + subforum;
            return View();
        }
    }
}