using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCAllProjects.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [Authorize]
        public ActionResult Index()
        {
            //return Content("Login Successfully");
            TempData["SuccessMessage"] = "Login Successfully";
            return RedirectToAction("Index", "UserRegistration");
        }
    }
}