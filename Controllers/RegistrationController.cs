using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly BlogDbContext db = new BlogDbContext();
        //// GET: Registration
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Create()
        {
            return View(new User());
        }

        // POST: Registration/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,Name,Password,RegistrationDate,BlogsCount")] User user)
        {
            if (ModelState.IsValid)
            {
                user.BlogsCount = 0;
                user.RegistrationDate = DateTime.Today;

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
