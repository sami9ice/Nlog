using LogApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace LogApplication.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            var Username = "sagwogie@gmail.com";
            var Password = "admin";
            if (!this.ModelState.IsValid)
                this.ModelState.AddModelError("", "Invalid Username or Password");
            if (ModelState.IsValid && model.EmailAddress == Username && model.Password == Password)

            {

                ClaimsIdentity claims = new ClaimsIdentity("ApplicationCookie");
                claims.AddClaim(new Claim(ClaimTypes.Email, model.EmailAddress));
                var ctxt = this.Request.GetOwinContext();
                ctxt.Authentication.SignIn(claims);
                return Redirect(GetRedirectUrl(returnUrl));

            }

            return View();

        }
        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {

                return Url.Action("index", "Home");

            }
            return returnUrl;







        }

    }
}