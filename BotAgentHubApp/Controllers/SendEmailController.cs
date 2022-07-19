using BotAgentHubApp.Models;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BotAgentHubApp.Controllers
{
    public class SendEmailController : Controller
    {
        private static readonly string Email = WebConfigurationManager.AppSettings["GoogleEmail"];
        private static readonly string Password = WebConfigurationManager.AppSettings["GooglePassword"];

        // GET: SendEmail
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmailModel model)
        {
            using (var mm = new MailMessage(Email, model.To))
            {
                mm.Subject = model.Subject;
                mm.Body = model.Body;
                mm.IsBodyHtml = false;
                using (var smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    var networkCred = new NetworkCredential(Email, Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    ViewBag.Message = "Email sent.";
                }
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}