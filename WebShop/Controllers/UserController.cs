using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShop.DAL;
using WebShop.Models;
using WebShop.Security;

namespace WebShop.Controllers
{
    public class UserController : Controller
    {
        private MarketContext db = new MarketContext();
        private UserManager mn = new UserManager();

        [CustomAuthorize]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Login,Password,RePassword,FirstName,LastName,Email,ContactInfo")] User user)
        {
            if (ModelState.IsValid)
            {
                if (mn.Register(user)) return RedirectToAction("Index");
                ViewBag.ErrorMessage = "That Login is already exist";
            }

            return RedirectToAction("Profile");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Login,Password")] LoginUser user)
        {
            if (ModelState.IsValid)
            {
                if (mn.Login(user.Login, user.Password))
                {
                    //foreach (var justUser in db.Users.ToList())
                    //{
                    //    if (justUser.Login == user.Login && justUser.Password == user.Password)
                    //    {
                    //        return RedirectToAction("Profile", "User", justUser);
                    //    }
                    //}
                    return RedirectToAction("Profile", "User");
                }
                ViewBag.ErrorMessage = "Login or Password is not valid";
            }
            return View(user);
        }

        public ActionResult LogOff()
        {
            mn.LogOff();
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [CustomAuthorize]
        public ActionResult Profile()
        {
            if (string.IsNullOrEmpty(SessionContainer.UserID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id = int.Parse(SessionContainer.UserID);
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        public ActionResult DeleteFavourite(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favourite favourite = db.Favourites.Find(id);
            if (favourite != null)
            {
                db.Favourites.Remove(favourite);
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return RedirectToAction("Profile");
        }

        public ActionResult EditProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "ID, FirstName, LastName, Email, ContactInfo, Password, RePassword, Login")] User user) 
        {
            if (!SessionContainer.UserID.ToString().Equals(user.ID.ToString()))
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View(user);
        }

        public ActionResult Logout(User user)
        {
            mn.LogOff();
            return RedirectToAction("Login");
        }
    }
}
