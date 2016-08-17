using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_commerce.Models;
using System.IO;
using System.Drawing;
using System.Web.Helpers;
/**
 * Authors: Rutvik Patel, Ritesh Patel, Himanshu Patel and  Parvati Patel
 * Name: ProductsController.cs
 * Description: This file mainly to add, update, delete products type by admin users.
 */
namespace E_commerce.Controllers
{
    [Authorize(Users = "admin@admin.com")]
    public class ProductsController : Controller
    {
        private EcommerceModel db = new EcommerceModel();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
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

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PDId,Name,Description")] Product product)
        {
            bool flag = db.Products.Any(i => i.Name == product.Name);
            if (flag)
            {
                ViewBag.alert = "Product already added!";
                ViewBag.MenuId = new SelectList(db.Products, "MenuId", "Name");
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        HttpPostedFileBase image = Request.Files["file"];

                        if (image != null)
                        {

                            string fileExtention = Path.GetExtension(image.FileName);
                            if (fileExtention == ".png" || fileExtention == ".jpeg" || fileExtention == ".gif" || fileExtention == ".jpg")
                            {

                                //Thumbnails file path
                                string filePathThumbnail = "~/Assests/Uploads/Products/Thumbnails";
                                bool flag2 = System.IO.Directory.Exists(Server.MapPath(filePathThumbnail));
                                //check if directory exists or not
                                if (!flag2)
                                    System.IO.Directory.CreateDirectory(Server.MapPath(filePathThumbnail));
                                //Save image to file

                                string filename = image.FileName;
                              
                                filename = Guid.NewGuid() + filename;

                                Image img = Image.FromStream(image.InputStream,true,true);
                                int imgHeight = 250;
                                int imgWidth = 200;

                                Image thumb = img.GetThumbnailImage(imgWidth, imgHeight, () => false, IntPtr.Zero);

                                string savedFileName = Path.Combine(Server.MapPath(filePathThumbnail), filename);
                                //save image into folder
                                thumb.Save(savedFileName);
                                product.ThumbUrl = filename;


                                db.Products.Add(product);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                return View(product);
                            }



                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }


                }

                return View(product);
            }
        }

        // GET: Products/Edit/5
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

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PDId,Name,Description,ThumbUrl")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);*/
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
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
