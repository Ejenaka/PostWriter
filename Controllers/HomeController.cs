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
        public ActionResult Register(User user)
        {
            if (db.Users.Where(u => u.Name == user.Name).Any())
            {
                ModelState.AddModelError("Name", "This user already exists");
                return View(user);
            }

            if (user.Password.Length < 5)
                ModelState.AddModelError("Password", "Password is too small");

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
        public ActionResult Login(User user)
        {
            var foundUser = db.Users.Where(u => u.Name == user.Name).FirstOrDefault();
            if (foundUser == null)
            {
                ModelState.AddModelError("Name", "This user doesn't exist");
                return View(user);
            }

            if (foundUser.Password != Crypto.SHA256(user.Password))
                ModelState.AddModelError("Password", "Wrong password");

            if (ModelState.IsValid)
            {
                Session["User"] = foundUser;
                Session["UserID"] = foundUser.ID;
                Session["Username"] = foundUser.Name;
                return RedirectToAction("Details", "Users", new {id = foundUser.ID});
            }

            return View(user);
        }
    }
}