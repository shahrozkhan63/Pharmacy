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
   [Authorize]
    public class OrderMastersController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        // GET: OrderMasters
        [Authorize(Roles = "Admin,User,Moderator")]
        public ActionResult Index(string SearchBy, string SearchValue)
        {
            try
            {
                var orderDetails = db.OrderDetails.Include(o => o.OrderMaster);

                if (SearchBy == "OrderID")
                {

                    int Id = Convert.ToInt32(SearchValue);
                    return View(db.OrderMasters.Where(x => x.OrderID == Id || SearchValue == null).ToList());
                }

                else if (SearchBy == "Name")
                {

                    return View(db.OrderMasters.Where(x => x.Name.StartsWith(SearchValue) || SearchValue == null).ToList());
                }
                else
                {
                    return View(db.OrderMasters.ToList());
                }
             
            }
            catch
            {
                return View(db.OrderMasters.ToList());
            }
        }

        // GET: OrderMasters/Details/5
        [Authorize(Roles = "Admin,User,Moderator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMaster orderMaster = db.OrderMasters.Find(id);
            if (orderMaster == null)
            {
                return HttpNotFound();
            }
            return View(orderMaster);
        }

        // GET: OrderMasters/Create
        [Authorize(Roles = "Admin,User,Moderator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,OrderNo,OrderDate,Description,TotalBill,Name,Discount")] OrderMaster orderMaster)
        {
            if (ModelState.IsValid)
            {
                db.OrderMasters.Add(orderMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderMaster);
        }

        // GET: OrderMasters/Edit/5
        [Authorize(Roles = "Admin,User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMaster orderMaster = db.OrderMasters.Find(id);
            if (orderMaster == null)
            {
                return HttpNotFound();
            }
            return View(orderMaster);
        }

        // POST: OrderMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Edit([Bind(Include = "OrderID,OrderNo,OrderDate,Description,TotalBill,Name,Discount")] OrderMaster orderMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderMaster);
        }

        // GET: OrderMasters/Delete/5
        [Authorize(Roles = "Admin,User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderMaster orderMaster = db.OrderMasters.Find(id);
            if (orderMaster == null)
            {
                return HttpNotFound();
            }
            return View(orderMaster);
        }

        // POST: OrderMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderMaster orderMaster = db.OrderMasters.Find(id);
            db.OrderMasters.Remove(orderMaster);
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
