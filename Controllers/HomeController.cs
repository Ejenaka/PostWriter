using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.Entity;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogDbContext db = new BlogDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View(new User());
        }

        public ActionResult LogOut()
        {
            Session.Remove("Username");
            Session.Remove("UserID");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "ID,Name,Password,RegistrationDate,BlogsCount")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Crypto.SHA256(user.Password);
                user.BlogsCount = 0;
                user.RegistrationDate = DateTime.Today;

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Login()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "ID,Name,Password,RegistrationDate,BlogsCount")] User user)
        {
            if (ModelState.IsValid)
            {
                var foundUser = db.Users.Where(u => u.Name == user.Name).First();
                if (foundUser != null)
                {
                    if (foundUser.Password == Crypto.SHA256(user.Password))
                    {
                        Session["User"] = foundUser;
                        Session["UserID"] = foundUser.ID;
                        Session["Username"] = foundUser.Name;
                        return RedirectToAction("Details", "Users", new {id = foundUser.ID});
                    }
                }
            }

            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}