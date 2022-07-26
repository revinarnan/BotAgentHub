using BotAgentHubApp.Models;
using System.Linq;
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
        private readonly ApplicationDbContext _context;

        public SendEmailController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: SendEmail
        public ActionResult Index(string id)
        {
            var mailTemplate = "Terima kasih telah mencoba menghubungi kami.\n" +
                               "Mohon maaf kami tidak tersedia pada jam tersebut.\n\n" +
                               "Berikut ini merupakan jawaban dari pertanyaan yang anda sampaikan:\n" +
                               "[[SILAKAN ISI JAWABAN SESUAI PERTANYAAN YANG DIAJUKAN]]\n\n" +
                               "Jika masih ada yang ingin ditanyakan, anda dapat membalas melalui email ini.\n" +
                               "Terima kasih,\n" +
                               "EchaBot";

            var questionsModel = from question in _context.ChatBotEmailQuestions
                                 where question.IsAnswered == false
                                 select question;
            //var questionData = questionsModel.Single(q => q.Id == id);
            var emailModel = new EmailModel();

            var viewModel = new DashboardViewModels
            {
                EmailModel = emailModel,
                EmailQuestions = questionsModel
            };
            //viewModel.EmailModel.To = questionData.Email;
            //viewModel.EmailModel.Subject = $"Balas: [{questionData.Question}]";
            viewModel.EmailModel.Body = mailTemplate;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(DashboardViewModels model)
        {
            var emailQuestion = model.EmailQuestions.SingleOrDefault();
            var chatHistoryInDb = _context.ChatHistories.Single(c => c.ChatHistoryFileName == emailQuestion.Id);
            var emailQuestionInDb = _context.ChatBotEmailQuestions.Single(e => e.Id == emailQuestion.Id);

            using (var mm = new MailMessage(Email, model.EmailModel.To))
            {
                mm.Subject = model.EmailModel.Subject;
                mm.Body = model.EmailModel.Body;
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

                    emailQuestionInDb.IsAnswered = true;
                    chatHistoryInDb.IsDoneOnEmail = true;
                    _context.SaveChanges();

                    ViewBag.Message = "Email sent.";
                }
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}