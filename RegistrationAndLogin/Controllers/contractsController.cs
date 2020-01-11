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
    [Authorize(Roles = "Admin,User,Moderator")]
    public class contractsController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        // GET: contracts
        [Authorize(Roles = "Admin,User,Moderator")]
        public ActionResult Index()
        {
            var contracts = db.contracts.Include(c => c.supplier);
            return View(contracts.ToList());
        }

        // GET: contracts/Details/5
        [Authorize(Roles = "Admin,User,Moderator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contract contract = db.contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: contracts/Create
        [Authorize(Roles = "Admin,User,Moderator")]
        public ActionResult Create()
        {
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name");
            return View();
        }

        // POST: contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "contract_id,supplier_id,start_date,end_date,description")] contract contract)
        {
            if (ModelState.IsValid)
            {
                db.contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", contract.supplier_id);
            return View(contract);
        }

        // GET: contracts/Edit/5
        [Authorize(Roles = "Admin,User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contract contract = db.contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", contract.supplier_id);
            return View(contract);
        }

        // POST: contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "contract_id,supplier_id,start_date,end_date,description")] contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", contract.supplier_id);
            return View(contract);
        }

        // GET: contracts/Delete/5
        [Authorize(Roles = "Admin,User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contract contract = db.contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contract contract = db.contracts.Find(id);
            db.contracts.Remove(contract);
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
