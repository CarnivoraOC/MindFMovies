using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAssignment.Models;

namespace WebAssignment.Controllers
{
    public class CommentController : Controller
    {

//Controller is used to delegate methods from class CommentService
        [HttpGet]
        public ActionResult AAddComment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AAddComment(Comment comment)
        {
            CommentService.createComment(comment);
            return View();
        }
    }
}