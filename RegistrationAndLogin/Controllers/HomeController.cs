using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistrationAndLogin.Models;
using System.Web.Security;
using System.Net.Mail;
using System.IO;
using static RegistrationAndLogin.Models.Emailing;

namespace RegistrationAndLogin.Controllers
{
    [Authorize(Roles = "Admin,User,Moderator")]
    public class HomeController : Controller
    {
        // GET: Home

        MyDatabaseEntities db = new MyDatabaseEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult UnSuccess()
        {
            return View();
        }
        public JsonResult getProductCategories()
        {
            List<Category> categories = new List<Category>();
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                categories = dc.Categories.OrderBy(a => a.CategortyName).ToList();
            }
            return new JsonResult { Data = categories, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult getProducts(int categoryID)
        {
            List<Product> products = new List<Product>();
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                products = dc.Products.Where(a => a.CategoryID.Equals(categoryID)).OrderBy(a => a.ProductName).ToList();
            }
            return new JsonResult { Data = products, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult getProductsrate(int PID)
        {
            List<Product> rate = new List<Product>();
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                rate = dc.Products.Where(a => a.ProductID.Equals(PID)).OrderBy(a => a.Rate).ToList();
            }
            return new JsonResult { Data = rate, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult getProductsQuantity(int QID)
        {
            List<Product> quantity = new List<Product>();
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                quantity = dc.Products.Where(a => a.ProductID.Equals(QID)).OrderBy(a => a.Quantity).ToList();
            }
            return new JsonResult { Data = quantity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        [HttpPost]
        public JsonResult save(OrderMaster order)
        {
            bool status = false;
            DateTime dateOrg;
            var isValidDate = DateTime.TryParseExact(order.OrderDateString, "MM/dd/yyyy hh:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out dateOrg);
            if (isValidDate)
            {
                order.OrderDate = dateOrg;
            }

            var isValidModel = TryUpdateModel(order);
            if (isValidModel)
            {
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    dc.OrderMasters.Add(order);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        public ActionResult SendEmail()
        {
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name");
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(EmailContent emailContent, List<HttpPostedFileBase> files)
        {
            ViewBag.supplier_id = new SelectList(db.suppliers, "supplier_id", "supplier_name");
            using (MyDatabaseEntities db = new MyDatabaseEntities())
            {
                Emailing em = new Emailing();
                string emails = em.SendEmail(emailContent);
                try
                {
                    const string username = "hospitalalrehman@gmail.com";
                    const string password = "Alrehman123";

                    SmtpClient smtpclient = new SmtpClient();
                    MailMessage mail = new MailMessage();
                    MailAddress fromaddress = new MailAddress("hospitalalrehman@gmail.com", "AL-Rehman-Laboratory");
                    smtpclient.Host = "smtp.gmail.com";
                    smtpclient.Port = 587;

                    mail.From = fromaddress;
                    mail.To.Add(emails);
                    mail.Subject = emailContent.EmailSubject;

                    mail.IsBodyHtml = true;
                    //foreach (HttpPostedFileBase file in files)
                    //{
                    //    if (files != null)
                    //    {
                    //        if (file.ContentLength < 24000000)
                    //        {
                    //            string fileName = Path.GetFileName(file.FileName);
                    //            mail.Attachments.Add(new Attachment(file.InputStream, fileName));
                    //        }
                    //    }
                    //}

                    mail.Body = emailContent.EmailBody;

                    smtpclient.EnableSsl = true;
                    smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpclient.Credentials = new System.Net.NetworkCredential(username, password);
                    smtpclient.Send(mail);

                    ViewBag.Project = new SelectList(db.suppliers, "supplier_id", "name");
                    ViewBag.Message = "Email Sent Successfully!";
                    return RedirectToAction("Success");
                }
                catch
                {
                    ViewBag.Project = new SelectList(db.suppliers, "supplier_id", "name");
                    ViewBag.Message = "Failed to Send Email, Please Try Again";
                    return RedirectToAction("UnSuccess");
                }
            }

        }
    }
}