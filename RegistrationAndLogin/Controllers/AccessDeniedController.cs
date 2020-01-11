using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationAndLogin.Controllers
{
   
    public class AccessDeniedController : Controller
    {
        // GET: AccessDenied

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                return View();
            }
            else
            {

                return RedirectToAction("Login", "User");
            }

        }
    }
}