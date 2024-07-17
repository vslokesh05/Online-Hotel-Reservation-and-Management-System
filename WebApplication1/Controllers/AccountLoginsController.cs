using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountLoginsController : Controller
    {
        private OnlineHotelDBContext db = new OnlineHotelDBContext();

        // GET: AccountLogins
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(AccountLogin model)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == model.UserName && u.Password == model.Password);
            if (user != null)
            {
                return RedirectToAction("RMlist", "RoomInfos");
            }
            else
            {
                ModelState.AddModelError("UserName", "Invalid username or password.");
                return View("UserLogin");
            }
        }

        [HttpPost]
        public ActionResult AdminLogin(AccountLogin model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index", "RoomInfos");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Admin login attempt.");
            }
            return View("AdminLogin");
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            TempData["SuccessMessage"] = "Successfully Logged Out!";
            return RedirectToAction("LogoutSuccess");
        }

        public ActionResult LogoutSuccess()
        {
            string successMessage = TempData["SuccessMessage"]?.ToString();
            ViewBag.SuccessMessage = successMessage;
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        // POST: /Account/Signup
        [HttpPost]
        public ActionResult Signup(AccountLogin model, string accountType)
        {
            if (accountType == "User")
            {
                db.Users.Add(new User { Username = model.UserName, Password = model.Password });


                return RedirectToAction("Create", "Users");
                db.SaveChanges();
            }
            else if (accountType == "Admin")
            {
                db.Admins.Add(new Admin { AUserName = model.UserName, APassword = model.Password });
                return RedirectToAction("Login", "AccountLogins");
                db.SaveChanges();
            }

            return RedirectToAction("Signup");
        }

        // Users controller
        [HttpPost]
        public ActionResult Create(User user)
        {

            return RedirectToAction("Login", "AccountLogins");

        }
    }
}