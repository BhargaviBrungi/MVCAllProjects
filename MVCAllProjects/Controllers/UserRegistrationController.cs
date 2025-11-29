using MVCAllProjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCAllProjects.Controllers
{
    public class UserRegistrationController : Controller
    {
        // GET: UserRegistration
        UserRegistration obj = new UserRegistration();
        public ActionResult Index()
        {
                //create view here with template List and select the model
                var response = obj.GetAllUsers();
                return View(response);
        }
        public ActionResult UserRegister()
        {
            var countrylist = new List<SelectListItem>();
            foreach(var response in obj.Countries)
            {
                countrylist.Add(new SelectListItem()
                {
                    Text = response
                });
            }
            TempData["SelectCountry"] = countrylist;
            TempData.Keep();
            return View();
        }
        [HttpPost]
        public ActionResult UserRegister(UserRegistration user,FormCollection formCollection)
        {
            //create view here
            var data = string.Empty;
            foreach(var item in user.Countries)
            {
                if (formCollection[item]?.ToString().Contains("true")==true)
                {
                    data = data + item;
                }
            }
            user.AddUser(user);
            TempData["SuccessMessage"]= "Registered Successfully";
            return RedirectToAction("Index", "UserRegistration");
            //return View();
        }
        
        public ActionResult Create()
        {
            //create a view here with create template and select model
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserRegistration user)
        {
            obj.AddUser(user);
            TempData["SuccessMessage"] = "Data Created Successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            //create a view here with edit template and select model
            var response = obj.GetAllUsers().Single(a => a.Id == id);
            return View(response);
        }
        [HttpPost]
        public ActionResult Edit(UserRegistration user)
        {
            obj.UpdateUser(user);
            TempData["SuccessMessage"] = "Data Updated Successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            //create a view here with Delete template and select model
            var response = obj.GetAllUsers().Single(a => a.Id == id);
            return View(response);
        }
        [HttpPost]
        public ActionResult Delete(UserRegistration user)
        {
            obj.DeleteUser(user);
            TempData["SuccesMessage"] = "Data Deleted Successfully";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            //create a view here with Details template and select model
            var response = obj.GetAllUsers().Single(a => a.Id == id);
            return View(response);
        }
    }
}