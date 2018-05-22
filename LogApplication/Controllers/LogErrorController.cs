using LogApplication.Helpers;
using LogApplication.Messaging;
using LogApplication.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace LogApplication.Controllers
{
    public class LogErrorController : Controller
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: LogError
        [HttpGet]
        public ActionResult Log4net()
        {
            log.Error("This is our error message");
            try
            {
                int i = 5;
                var s = i / 0;
            }
            catch (DivideByZeroException ex)
            {
                log.Fatal("This was a terrible error");
                log.Debug("Here again", ex);
            }

            return View();
        }
        [HttpPost]
        public ActionResult Log4net(EmailViewModel ev)
        {
            if (!ModelState.IsValid) this.ModelState.AddModelError("", "Fields are not valid");
            string key = ConfigurationManager.AppSettings["Sendgrid.Key"];

            SendGridEmailService service = new SendGridEmailService(key);

            EmailMessage msg = new EmailMessage
            {
                Recipient = ev.Email,
                Body = ev.Message,
                Subject = ev.Subject
            };

            var fileName = FileToByteArrayConverter.GetFilePath("Log\\log-file.txt");
            var fileData = FileToByteArrayConverter.Convert(fileName);

            service.SendMail(msg, false, fileName, fileData);

            return RedirectToAction("SuccessPage", "Home");


        }

        [HttpGet]
        public ActionResult NLog()
        {
            try
            {
                int i = 5;
                var s = i / 0;
            }
            catch (DivideByZeroException ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();

                logger.Error(ex, "this too");

            }

            return View();
        }
        [HttpPost]
        public ActionResult NLog(EmailViewModel em)
        {
            if (!ModelState.IsValid) this.ModelState.AddModelError("", "Fields are not valid");
            string key = ConfigurationManager.AppSettings["Sendgrid.Key"];

            SendGridEmailService service = new SendGridEmailService(key);

            EmailMessage msg = new EmailMessage
            {
                Recipient = em.Email,
                Body = em.Message,
                Subject = em.Subject
            };

            var fileName = FileToByteArrayConverter.GetFilePath("Logs\\2018-05-21.log");
            var fileData = FileToByteArrayConverter.Convert(fileName);

            service.SendMail(msg, false, fileName, fileData);

            return RedirectToAction("SuccessPage", "Home");
        }
    }
}
