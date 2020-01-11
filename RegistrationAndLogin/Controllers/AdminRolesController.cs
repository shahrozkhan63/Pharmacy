using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RegistrationAndLogin.Models;

namespace RegistrationAndLogin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminRolesController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        // GET: AdminRoles
        public ActionResult Index()
        {
            return View(db.AdminRoles.ToList());
        }

        // GET: AdminRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminRole adminRole = db.AdminRoles.Find(id);
            if (adminRole == null)
            {
                return HttpNotFound();
            }
            return View(adminRole);
        }

        // GET: AdminRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Role_id,Roletype")] AdminRole adminRole)
        {
            if (ModelState.IsValid)
            {
                db.AdminRoles.Add(adminRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminRole);
        }

        // GET: AdminRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminRole adminRole = db.AdminRoles.Find(id);
            if (adminRole == null)
            {
                return HttpNotFound();
            }
            return View(adminRole);
        }

        // POST: AdminRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Role_id,Roletype")] AdminRole adminRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminRole);
        }

        // GET: AdminRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminRole adminRole = db.AdminRoles.Find(id);
            if (adminRole == null)
            {
                return HttpNotFound();
            }
            return View(adminRole);
        }

        // POST: AdminRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminRole adminRole = db.AdminRoles.Find(id);
            db.AdminRoles.Remove(adminRole);
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
