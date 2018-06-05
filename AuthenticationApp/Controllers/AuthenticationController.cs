using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AuthenticationApp.Models;
using Business.Authentication;
using Data.Authentication;

namespace AuthenticationApp.Controllers
{
    public class AuthenticationController : Controller
    {
        public Class1 C1=new Class1();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            Register rg=new Register()
            {
                UserName = model.Username,
                Password = model.Password
            };
            var register = C1.CheckLogin(rg);
            if (register != null)
            {
                FormsAuthentication.SetAuthCookie(register.Email, false);

                var authTicket = new FormsAuthenticationTicket(1, register.Email, DateTime.Now, DateTime.Now.AddMinutes(20), false, register.Role);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                if (register.Role == "Admin")
                {
                    return RedirectToAction("Success", "Authentication");
                }
                if(register.Role == "User")
                {
                    return RedirectToAction("UserSuccess", "Authentication");
                }
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterModel rm)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterUser");
            }
            Register rg=new Register()
            {
                UserName = rm.Username,
                Password = rm.Password,
                Email = rm.Email,
                Role = rm.Role
            };
            C1.RegisterUser(rg);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Success()
        {
            return View(C1.GetList());
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            HttpContext.Response.Cookies.Clear();
            return RedirectToAction("Index", "Authentication");
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult UserSuccess()
        {
            return View();
        }
    }
}