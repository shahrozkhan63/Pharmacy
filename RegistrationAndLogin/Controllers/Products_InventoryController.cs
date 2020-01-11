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
    public class Products_InventoryController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        // GET: Products_Inventory
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "ProductName")
            {
                return View(db.Products_Inventory.Where(x => x.ProductName.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Products_Inventory.Where(x => x.formula.StartsWith(search) || search == null).ToList());
            }
        }

        // GET: Products_Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_Inventory products_Inventory = db.Products_Inventory.Find(id);
            if (products_Inventory == null)
            {
                return HttpNotFound();
            }
            return View(products_Inventory);
        }

        // GET: Products_Inventory/Create
        public ActionResult Create()
        {
            ViewBag.ProductName = new SelectList(db.Products, "ProductName", "ProductName");

            ViewBag.manufacturer_id = new SelectList(db.manufacturers, "manufacturer_id", "manufacturer_name");
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name");
            return View();
        }

        // POST: Products_Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductIDD,ProductName,Quantity,Rate,Rate_buy,mfg_date,exp_date,supplier_id,manufacturer_id,formula,description")] Products_Inventory products_Inventory)
        {
            if (ModelState.IsValid)
            {
                db.Products_Inventory.Add(products_Inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.manufacturer_id = new SelectList(db.manufacturers, "manufacturer_id", "manufacturer_name", products_Inventory.manufacturer_id);
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", products_Inventory.supplier_id);
            return View(products_Inventory);
        }

        // GET: Products_Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_Inventory products_Inventory = db.Products_Inventory.Find(id);
            if (products_Inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.manufacturer_id = new SelectList(db.manufacturers, "manufacturer_id", "manufacturer_name", products_Inventory.manufacturer_id);
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", products_Inventory.supplier_id);
            return View(products_Inventory);
        }

        // POST: Products_Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductIDD,ProductName,Quantity,Rate,Rate_buy,mfg_date,exp_date,supplier_id,manufacturer_id,formula,description")] Products_Inventory products_Inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products_Inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.manufacturer_id = new SelectList(db.manufacturers, "manufacturer_id", "manufacturer_name", products_Inventory.manufacturer_id);
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name", products_Inventory.supplier_id);
            return View(products_Inventory);
        }

        // GET: Products_Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products_Inventory products_Inventory = db.Products_Inventory.Find(id);
            if (products_Inventory == null)
            {
                return HttpNotFound();
            }
            return View(products_Inventory);
        }

        // POST: Products_Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products_Inventory products_Inventory = db.Products_Inventory.Find(id);
            db.Products_Inventory.Remove(products_Inventory);
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
