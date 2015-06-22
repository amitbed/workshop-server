using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumApplication.Models;

namespace ForumApplication.Controllers
{
    public class CreateSubForumController : Controller
    {
        //
        // GET: /CreateSubForum/

        public ActionResult CreateSubForum()
        {
            return View();
        }

        public ActionResult CreateNewSubForum()
        {
            string title = Request["SubForumtitle"].ToString();
            string moderator = Request["moderators"].ToString();
            string parent = Request["parent"].ToString().ToLower();
            int maxModerators = Int32.Parse(Request["maxModerators"].ToString());

            List<string> moderators = new List<string>();
            moderators.Add(moderator);
            ForumSystem fs = ForumSystem.initForumSystem();
            fs.Forums[parent].createSubForum(title, moderators, parent, maxModerators);
            ViewData["subForumParent"] = parent;
            return View();
        }
    }
}