using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAssignment.Models;
using System.IO;

namespace WebAssignment.Controllers
{
    public class UserController : Controller
    {

//Controller is used to delegate methods from class UserService
        [HttpGet]
        public ActionResult LogInn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogInn(string username, string password)
        {
            User user = UserService.getUser(username, password);
            if (user != null)
            {
                Session["userId"] = user.Id;
                Session["role"] = user.Admin==true?"admin":"user";
                ViewBag.loginMessage = "You are logged in!";
                return View();
            }
            else {
                ViewBag.loginMessage = "Username or password was wrong...";
                return View();
            } 
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            if (Session["role"] != null)
            {
                Session.RemoveAll();
                return View();
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult registerUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registerUser(User user)
        {
            var test = UserService.createUser(user);

            if (test != null) { 
                ViewBag.UserRegistration = "User registered! Now log in...";
                return View();
            }
            else
            {
                ViewBag.UserRegistration = "Username already exists, try another one...";
                return View(); 
            }
        }
    }
}