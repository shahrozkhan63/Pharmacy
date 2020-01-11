using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RegistrationAndLogin.Models;

namespace RegistrationAndLogin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        // GET: Account
        public ActionResult Index(string searchBy, string search)
        {
            var users = db.Users.Include(u => u.pharmacy);
            if (searchBy == "EmailID")
            {
                return View(db.Users.Where(x => x.EmailID.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Users.Where(x => x.Role.StartsWith(search) || search == null).ToList());
            }
        }

        // GET: Account/Details/5
        public ActionResult Details(int? id)
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

        // GET: Account/Create
        public ActionResult Create()
        {
            ViewBag.pharmacy_id = new SelectList(db.pharmacies, "pharmacy_id", "pharmacy_name");
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,EmailID,DateOfBirth,Password,IsEmailVerified,ActivationCode,pharmacy_id,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pharmacy_id = new SelectList(db.pharmacies, "pharmacy_id", "pharmacy_name", user.pharmacy_id);
            return View(user);
        }
        private MyDatabaseEntities dc = new MyDatabaseEntities();
        // GET: Account/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.pharmacy_id = new SelectList(db.pharmacies, "pharmacy_id", "pharmacy_name", user.pharmacy_id);
            ViewBag.Role = new SelectList(dc.AdminRoles, "Roletype", "Roletype");
            return View(user);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,pharmacy_id,Role")] User user)
        {
           

            if (ModelState.IsValid)
            {
                user.Password = Crypto.Hash(user.Password);
                
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pharmacy_id = new SelectList(db.pharmacies, "pharmacy_id", "pharmacy_name", user.pharmacy_id);
            ViewBag.Role = new SelectList(dc.AdminRoles, "Roletype", "Roletype");
            return View(user);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
