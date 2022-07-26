using BotAgentHubApp.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BotAgentHubApp.Controllers
{
    public class DashboardController : Controller
    {
        private const string DirectLineMode = "DIRECTLINE";
        private const string GenerateDirectLineTokenUrl = "https://directline.botframework.com/v3/directline/tokens/generate";
        private const string UnknownMode = "UNKNOWN";

        private static readonly bool EnableDirectLineEnhancedAuthentication =
            Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableDirectLineEnhancedAuthentication"]);
        private static readonly string DirectLineSecret = WebConfigurationManager.AppSettings["DirectLineSecret"];
        private static readonly string BotName = WebConfigurationManager.AppSettings["BotName"];

        private readonly string _mode;
        private readonly string _userId;

        private readonly ApplicationDbContext _context;

        public DashboardController()
        {
            _context = new ApplicationDbContext();

            if (EnableDirectLineEnhancedAuthentication)
            {
                this._userId = $"dl_{Guid.NewGuid()}";
            }

            // Determine which mode to operate under:
            // - Direct Line Speech should be used if a speech service region identifier and key are provided
            // - Direct Line should be used if a DL secret is provided
            // - Default to an unknown state (i.e. invalid app configuration provided)
            if (!string.IsNullOrEmpty(DirectLineSecret))
            {
                this._mode = DirectLineMode;
            }
            else
            {
                this._mode = UnknownMode;
            }
        }

        public ActionResult Index()
        {
            if (User.IsInRole("SuperAdmin") || User.IsInRole("StaffAdmin"))
            {
                var questionsModel = from question in _context.ChatBotEmailQuestions
                                     where question.IsAnswered == false
                                     select question;
                var emailModel = new EmailModel();

                var viewModel = new DashboardViewModels
                {
                    EmailModel = emailModel,
                    EmailQuestions = questionsModel
                };

                return View(viewModel);
            }

            if (User.IsInRole("Kaprodi"))
            {
                return RedirectToAction("Index", "ChatHistory");
            }

            return View("InvalidRole");
        }

        //[Authorize(Roles = "SuperAdmin")]
        //[Authorize(Roles = "StaffAdmin")]
        public ActionResult DirectLine(string locale = "en-us")
        {
            bool isDirectLineMode = string.Equals(DirectLineMode, this._mode, StringComparison.OrdinalIgnoreCase);

            ViewData["Locale"] = locale;
            ViewData["Mode"] = this._mode;
            ViewData["Title"] = BotName;
            ViewData["UserId"] = User.Identity.GetUserName();
            ViewData["UserName"] = User.Identity.GetUserName();

            if (isDirectLineMode)
            {
                string directLineToken = string.Empty;

                var directLineClient = new HttpClient();
                directLineClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    DirectLineSecret);

                string content = EnableDirectLineEnhancedAuthentication
                    ? JsonConvert.SerializeObject(
                        new
                        {
                            User = new
                            {
                                Id = this._userId
                            }
                        })
                    : string.Empty;

                HttpResponseMessage response = directLineClient.PostAsync(
                    GenerateDirectLineTokenUrl,
                    new StringContent(content, Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    var directLineResponse = JsonConvert.DeserializeObject<DirectLineResponse>(responseString);
                    directLineToken = directLineResponse.Token;
                }

                ViewData["DirectLineToken"] = directLineToken;
            }

            return PartialView("_ChatRoom");
        }
    }
}