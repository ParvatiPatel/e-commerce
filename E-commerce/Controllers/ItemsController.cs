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
using System.Web.Helpers;
using System.Drawing;
/**
 * Authors: Rutvik Patel, Ritesh Patel, Himanshu Patel and  Parvati Patel
 * Name: ItemsController.cs
 * Description: This file is for admin user who can add, edit and udpdate item on our site.
 */
namespace E_commerce.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private EcommerceModel db = new EcommerceModel();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Product);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.PDID = new SelectList(db.Products, "PDId", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IID,PDID,Name,Description,Price,ThumbUrl,OriginalUrl,Tag")] Item item)
        {
            bool flag = db.Items.Any(i => i.Name == item.Name);
            if (flag)
            {
                ViewBag.alert = "Item already added!";
                ViewBag.PDID = new SelectList(db.Products, "PDId", "Name");
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
                                //Originals file path
                                string filePathOriginal = "~/Assests/Uploads/Items/Originals";
                                bool flag1 = System.IO.Directory.Exists(Server.MapPath(filePathOriginal));
                                //check if directory exists or not
                                if (!flag1)
                                    System.IO.Directory.CreateDirectory(Server.MapPath(filePathOriginal));
                                //Thumbnails file path
                                string filePathThumbnail = "~/Assests/Uploads/Items/Thumbnails";
                                bool flag2 = System.IO.Directory.Exists(Server.MapPath(filePathThumbnail));
                                //check if directory exists or not
                                if (!flag2)
                                    System.IO.Directory.CreateDirectory(Server.MapPath(filePathThumbnail));
                                //Save image to file

                                string filename = image.FileName;
                                filename = Guid.NewGuid() + filename;

                           

                                string savedFileName = Path.Combine(Server.MapPath(filePathOriginal), filename);
                                //save image into folder
                                image.SaveAs(savedFileName);
                                item.OriginalUrl = filename;


                                //Read image back from file and create thumbnail from it
                                Image imageFile = Image.FromFile(savedFileName);
                                int imgHeight = 250;
                                int imgWidth = 200;

                                Image thumb = imageFile.GetThumbnailImage(imgWidth, imgHeight, () => false, IntPtr.Zero);
                                filePathThumbnail = Path.Combine(Server.MapPath(filePathThumbnail), filename);
                                item.ThumbUrl = filename;
                                //save thumb image into folder
                                thumb.Save(filePathThumbnail);

                            }
                            else
                            {
                                ViewBag.PDID = new SelectList(db.Products, "PDId", "Name", item.PDID);
                                return View(item);
                            }



                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }




                    db.Items.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.PDID = new SelectList(db.Products, "PDId", "Name", item.PDID);
                return View(item);
            }
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.PDID = new SelectList(db.Products, "PDId", "Name", item.PDID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IID,PDID,Name,Description,Price,ThumbUrl,OriginalUrl,Tag")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PDID = new SelectList(db.Products, "PDId", "Name", item.PDID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
            /* if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             Item item = db.Items.Find(id);
             if (item == null)
             {
                 return HttpNotFound();
             }
             return View(item);*/
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
