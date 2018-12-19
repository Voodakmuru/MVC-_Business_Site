using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradingCommerce.DAL;
using TradingCommerce.Models;
using System.IO;

namespace TradingCommerce.Controllers
{
    public class BusinessesController : Controller
    {
        private businessContext db = new businessContext();

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Security.checkLevel("Client");
        }

        // GET: Businesses
        public ActionResult Index()
        {
            try
            {
                var securityLevel = (string)System.Web.HttpContext.Current.Session["securityLevel"];
                int userID = (int)System.Web.HttpContext.Current.Session["userID"];
                if (securityLevel == "admin")
                {
                    return View(db.Businesses.ToList());
                }
                else
                {
                    return View(db.Businesses.Where(b => b.userID == userID).ToList());
                }
            }
            catch
            {
                Response.Redirect("/Login/Login");
            }
            return View(db.Businesses.ToList());      
        }

        // GET: Businesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // GET: Businesses/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.Users, "userID", "userID");
            Business business = new Business();
            business.filePath = "New";
            return View(business);
        }

        // POST: Businesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "businessID,businessName,filePath,userID, File")] Business business, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {           
                    business.filePath = "~/Photos/" + file.FileName;
                    var path = Path.Combine(Server.MapPath("~/Photos"), file.FileName);
                    file.SaveAs(path);
                }
                db.Businesses.Add(business);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.Users, "userID", "securityLevel", business.userID);
            return View(business);
        }

        // GET: Businesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.Users, "userID", "securityLevel", business.userID);
            return View(business);
        }

        // POST: Businesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "businessID,businessName,filePath,userID, File")] Business business, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        business.filePath = "~/Photos/" + file.FileName;
                        var path = Path.Combine(Server.MapPath("~/Photos"), file.FileName);
                        file.SaveAs(path);
                    }
                }
                db.Entry(business).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.Users, "userID", "securityLevel", business.userID);
            return View(business);
        }

        // GET: Businesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Business business = db.Businesses.Find(id);
            db.Businesses.Remove(business);
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
