using LogApplication.App_Start;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(LogApplication.Startup))]
namespace LogApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                ReturnUrlParameter = "returnUrl",
                AuthenticationType = "ApplicationCookie",
                CookieName = "LogApplication",
                LoginPath = new PathString("/Auth/Login")
            });

            FilterConfig.Configure(GlobalFilters.Filters);

        }
    }
}