using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ReferralBonus.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        // GET: /login/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login_()//
        {
            bool isChecked = false;
            if (Request.Form["remember"] != null)
            {
                isChecked = true;
            }
            if (FormsAuthentication.Authenticate(Request.Form["USer name"].ToString(), Request.Form["password"].ToString()))
            {
                FormsAuthentication.RedirectFromLoginPage(Request.Form["USer name"].ToString(), isChecked);
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                return RedirectToAction("ManageFeed", "Home");
            }

            else
            {
                Response.Write("INvalid user");
                return RedirectToAction("login");
            }
            //foreach (string key in formCollection.Keys)
            //{

            //    Response.Write(key);
            //    Response.Write(formCollection[key]);
            //}

        }
    }
}