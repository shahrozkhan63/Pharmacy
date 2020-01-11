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
    [Authorize(Roles = "Admin,User")]
    public class product_purchaseController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        // GET: product_purchase
        public ActionResult Index()
        {
            var product_purchase = db.product_purchase.Include(p => p.supplier);
            return View(product_purchase.ToList());
        }

        // GET: product_purchase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_purchase product_purchase = db.product_purchase.Find(id);
            if (product_purchase == null)
            {
                return HttpNotFound();
            }
            return View(product_purchase);
        }

        // GET: product_purchase/Create
        public ActionResult Create()
        {
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name");
            return View();
        }

        // POST: product_purchase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "purchase_id,supplier_id,purchase_date,total_amount")] product_purchase product_purchase)
        {
            if (ModelState.IsValid)
            {
                db.product_purchase.Add(product_purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", product_purchase.supplier_id);
            return View(product_purchase);
        }

        // GET: product_purchase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_purchase product_purchase = db.product_purchase.Find(id);
            if (product_purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", product_purchase.supplier_id);
            return View(product_purchase);
        }

        // POST: product_purchase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "purchase_id,supplier_id,purchase_date,total_amount")] product_purchase product_purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", product_purchase.supplier_id);
            return View(product_purchase);
        }

        // GET: product_purchase/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_purchase product_purchase = db.product_purchase.Find(id);
            if (product_purchase == null)
            {
                return HttpNotFound();
            }
            return View(product_purchase);
        }

        // POST: product_purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_purchase product_purchase = db.product_purchase.Find(id);
            db.product_purchase.Remove(product_purchase);
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
