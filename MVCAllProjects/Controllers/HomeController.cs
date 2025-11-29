using Microsoft.Ajax.Utilities;
using MVCAllProjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCAllProjects.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            //Create View Here
            return View(new Login());
        }
        [HttpPost]
        public ActionResult LoginCheck(Login model)
        {
            if(!ModelState.IsValid)
            {
                return View("Login", model);
            }
            LoginDataAccessLayer obj = new LoginDataAccessLayer();
            string userName = Request.Form["Username"];
            string password = Request.Form["Password"];
            int result = obj.CheckUserDetails(userName, password);
            if (result == 1)
            {
                FormsAuthentication.SetAuthCookie(userName, true);
                return RedirectToAction("Index","Login");
            }
            else
            {
                ModelState.AddModelError("","Invalid Username and Password");
                return View("Login", model);
                //return RedirectToAction("Login");
            }
        }
    }
}