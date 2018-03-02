using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ReferralBonus.Controllers
{
    public class SignoutController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "login");


        }
    }
}