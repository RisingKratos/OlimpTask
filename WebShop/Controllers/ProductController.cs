using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using PagedList;
using System.Web;
using System.Web.Mvc;
using WebShop.DAL;
using WebShop.Models;
using WebShop.Security;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        private MarketContext db = new MarketContext();

        // GET: Product
        public ActionResult Index(string searchString, int? page, string currentFilter)
        {
            var products = db.Products.Include(product => product.Seller).Include(product => product.Category).Include(product => product.Favourites);
            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                string[] words = searchString.Split(' ');
                foreach (string word in words)
                {
                    products = products.Where(product => product.Title.Contains(word)
                                          || product.Category.Name.Contains(word)
                                          || product.Description.Contains(word)
                                          || product.Seller.ContactInfo.Contains(word));
                }
            }
            products = products.OrderBy(product => product.Title);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        [CustomAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult Create([Bind(Include = "ProductID,Title,Description,Price,Delivery,Category")] Product product, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var dir_path = Server.MapPath("/Images/" + "\\");
                var site_path = "/Images/";
                var file_name = file.FileName;
                var path = dir_path + file_name;
                if (System.IO.File.Exists(path))
                {

                }
                file.SaveAs(path);
                foreach (User user in db.Users.ToList())
                {
                    if (SessionContainer.UserID.ToString().Equals(user.ID.ToString()))
                        product.Seller = user;
                }
                if (ModelState.IsValid)
                {
                    product.Image = site_path + file_name;
                    product.Status = false;
                    product.CreatedDate = DateTime.Now;
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return View(product);
        }

        // GET: Product/Edit/5
        [CustomAuthorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Title,Description,Price,CreatedDate,Status,Delivery")] Product product, HttpPostedFileBase file)
        {
            var productToUpdate = db.Products.Find(product.ProductID);
            if (productToUpdate.Seller.ID.ToString().Equals(SessionContainer.UserID) || SessionContainer.Role.ToString().Equals("1"))
            {
                if (file != null)
                {
                    var dir_path = Server.MapPath("/Images/" + "\\");
                    var site_path = "/Images/";
                    var file_name = file.FileName;
                    var path = dir_path + file_name;
                    file.SaveAs(path);

                    if (ModelState.IsValid)
                    {
                        productToUpdate.Image = site_path + file_name;
                    }
                }
                productToUpdate.Category = product.Category;
                productToUpdate.Title = product.Title;
                productToUpdate.Description = product.Description;
                productToUpdate.Price = product.Price;
                productToUpdate.CreatedDate = product.CreatedDate;
                productToUpdate.Delivery = product.Delivery;
                productToUpdate.Status = product.Status;
                db.Entry(productToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(product);
        }

        // GET: Product/Delete/5
        [CustomAuthorize]
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if (SessionContainer.Role.Equals("1"))
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [CustomAuthorize]
        public ActionResult AddToFavourites(int? id)
        {
            foreach (var notSeller in db.Users.ToList())
            {
                if (notSeller.ID.ToString().Equals(SessionContainer.UserID))
                {
                    User user = notSeller;
                    Product product = db.Products.Find(id);
                    Favourite favourite = new Favourite() { User = user, Product = product };
                    db.Favourites.Add(favourite);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
