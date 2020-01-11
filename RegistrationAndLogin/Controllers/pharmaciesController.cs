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
    public class pharmaciesController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        // GET: pharmacies
        public ActionResult Index()
        {
            return View(db.pharmacies.ToList());
        }

        // GET: pharmacies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pharmacy pharmacy = db.pharmacies.Find(id);
            if (pharmacy == null)
            {
                return HttpNotFound();
            }
            return View(pharmacy);
        }

        // GET: pharmacies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: pharmacies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pharmacy_id,pharmacy_name,Address,City,State,Country,Contact,PostalCode")] pharmacy pharmacy)
        {
            if (ModelState.IsValid)
            {
                db.pharmacies.Add(pharmacy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pharmacy);
        }

        // GET: pharmacies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pharmacy pharmacy = db.pharmacies.Find(id);
            if (pharmacy == null)
            {
                return HttpNotFound();
            }
            return View(pharmacy);
        }

        // POST: pharmacies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pharmacy_id,pharmacy_name,Address,City,State,Country,Contact,PostalCode")] pharmacy pharmacy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pharmacy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pharmacy);
        }

        // GET: pharmacies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pharmacy pharmacy = db.pharmacies.Find(id);
            if (pharmacy == null)
            {
                return HttpNotFound();
            }
            return View(pharmacy);
        }

        // POST: pharmacies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pharmacy pharmacy = db.pharmacies.Find(id);
            db.pharmacies.Remove(pharmacy);
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
