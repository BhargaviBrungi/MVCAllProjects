using MVCAllProjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCAllProjects.Controllers
{
    public class HotelRegistrationController : Controller
    {
        // GET: HotelRegistration
        Hotel obj = new Hotel();
        public ActionResult Index()
        {
            //create view here with Template List and select Model
            var response = obj.GetAllRegistrations();
            return View(response);
        }
        public ActionResult HotelRegister()
        {
            TempData["SelectAmenities"] = obj.SelectAmenities;
            TempData.Keep("SelectAmenities");
            return View();
        }
        [HttpPost]
        public ActionResult HotelRegister(Hotel hotel,FormCollection formCollection)
        {
            var data = string.Empty;
            foreach(var item in hotel.SelectAmenities)
            {
                if (formCollection[item]?.ToString().Contains("true")==true)
                {
                    data = data + item+",";
                }
            }
            hotel.Amenities = data.TrimEnd(',');
            int rent = 0, acost = 0;
            string roomtype = formCollection["RoomType"];
            if(roomtype=="Ordinary")
            {
                rent = 1000;
            }
            else if(roomtype=="Delux")
            {
                rent = 1500;
            }
            hotel.RoomType = roomtype;
            if(hotel.Amenities.Contains("AC"))
            {
                acost += 400;
            }
            if(hotel.Amenities.Contains("Non AC"))
            {
                acost += 200;
            }
            int totalCost = rent + acost;
            hotel.TotalCost = totalCost.ToString();
            if (!ModelState.IsValid)
            {
                hotel.AddRegistration(hotel);
                string formattedAmenities = hotel.Amenities;
                TempData["WelcomeMessage"] = $"Welcome {hotel.Username},you are registered for RoomType {hotel.RoomType} with Amenites {formattedAmenities}";
                TempData["SuccessMessage"] = $"{hotel.Username},you have Registered Successfully!,Rent per day is Rs{hotel.TotalCost}";
                return RedirectToAction("HotelRegister", "HotelRegistration");
            }
            TempData["SelectAmenities"] = obj.SelectAmenities;
            TempData.Keep("SelectAmenities");
            return View(hotel);
        }
        public ActionResult Create()
        {
            //create view here with Template create and select model
            return View();
        }
        [HttpPost]
        public ActionResult Create(Hotel hotel)
        {
            obj.AddRegistration(hotel);
            TempData["Message"] = "Data Created Successfully!!";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            //create view here with Template Details and select model
            var response = obj.GetAllRegistrations().Single(a => a.Id == id);
            return View(response);
        }
        public ActionResult Edit(int id)
        {
            //create view here with Template Edit and select model
            var response = obj.GetAllRegistrations().Single(a => a.Id == id);
            return View(response);
        }
        [HttpPost]
        public ActionResult Edit(Hotel hotel)
        {
            obj.UpdateRegistration(hotel);
            TempData["Message"] = "Data Updated Successfully!!";
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            //create view here with Template Delete and select model
            var response = obj.GetAllRegistrations().Single(a => a.Id == id);
            return View(response);
        }
        [HttpPost]
        public ActionResult Delete(Hotel hotel)
        {
            obj.DeleteRegistration(hotel);
            TempData["Message"] = "Data Deleted Successfully!!";
            return RedirectToAction("Index");
        }
    }
}