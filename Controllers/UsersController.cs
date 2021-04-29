using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly BlogDbContext db = new BlogDbContext();

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = db.Users
                .Include(u => u.LikedPosts)
                .Include(u => u.Comments)
                .Where(u => u.ID == id)
                .FirstOrDefault();

            if (user == null)
                return HttpNotFound();

            return View(user);
        }
    }
}
