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
    public class OrderDetailsController : Controller
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        // GET: OrderDetails
        [Authorize(Roles = "Admin,User,Moderator")]
        public ActionResult Index(string SearchBy, string SearchValue)
        {
            try
            {
                var orderDetails = db.OrderDetails.Include(o => o.OrderMaster);

                if (SearchBy == "OrderID")
                {

                    int Id = Convert.ToInt32(SearchValue);
                    return View(db.OrderDetails.Where(x => x.OrderID == Id || SearchValue == null).ToList());
                }

                else if (SearchBy == "ProductID")
                {
                    int Id = Convert.ToInt32(SearchValue);
                    return View(db.OrderDetails.Where(x => x.ProductID == Id || SearchValue == null).ToList());
                }
                else
                {
                    return View(db.OrderDetails.ToList());
                }
            }
            catch
            {
                return View(db.OrderDetails.ToList());
            }
        }

        // GET: OrderDetails/Details/5
        [Authorize(Roles = "Admin,User,Moderator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        [Authorize(Roles = "Admin,User,Moderator")]
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.OrderMasters, "OrderID", "OrderNo");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderDetialsID,OrderID,ProductID,Rate,Quantity")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.OrderMasters, "OrderID", "OrderNo", orderDetail.OrderID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        [Authorize(Roles = "Admin,User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.OrderMasters, "OrderID", "OrderNo", orderDetail.OrderID);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderDetialsID,OrderID,ProductID,Rate,Quantity")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.OrderMasters, "OrderID", "OrderNo", orderDetail.OrderID);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        [Authorize(Roles = "Admin,User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
