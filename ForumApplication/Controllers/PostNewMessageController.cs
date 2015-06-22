using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumApplication.Models;

namespace ForumApplication.Controllers
{
    public class PostNewMessageController : Controller
    {
        //
        // GET: /PostNewMessage/

        public ActionResult PostNewMessage()
        {
            string parameters=(string)this.RouteData.Values["id"];
            string[] paramsAsArray = parameters.Split(' ');
            string forum = paramsAsArray[0];
            string subForum = paramsAsArray[1];
            string thread = paramsAsArray[2];
            ForumSystem fs = ForumSystem.initForumSystem();
            Thread t = fs.Forums[forum].SubForums[subForum].Threads[thread];
            string title = Request["MessageTitle"].ToString();
            string content = Request["content"].ToString();

            t.Messages.Add(title, new Message(title, content, "amit"));
            return View();
        }

    }
}
