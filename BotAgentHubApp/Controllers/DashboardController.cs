using BotAgentHubApp.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

        private readonly ApplicationDbContext _context;

        public DashboardController()
        {
            _context = new ApplicationDbContext();

            if (!string.IsNullOrEmpty(DirectLineSecret))
            {
                _mode = DirectLineMode;
            }
            else
            {
                _mode = UnknownMode;
            }
        }

        // GET
        public ActionResult Index()
        {
            if (User.IsInRole("SuperAdmin") || User.IsInRole("StaffAdmin"))
            {
                var questionsModel = from question in _context.ChatBotEmailQuestions
                                     where question.IsAnswered == false
                                     select question;

                var viewModel = new DashboardViewModels
                {
                    EmailModel = new EmailModel(),
                    EmailQuestions = questionsModel,
                    BotEmailQuestion = new ChatBotEmailQuestion(),
                    Notification = null
                };

                if (!viewModel.EmailQuestions.Any())
                {
                    viewModel.Notification = "Tidak ada permintaan email yang belum dibalas.";
                    return View(viewModel);
                }

                return View(viewModel);
            }

            if (User.IsInRole("Kaprodi"))
            {
                return RedirectToAction("Index", "ChatHistory");
            }

            return View("InvalidRole");
        }

        public async Task<PartialViewResult> DirectLine(string locale = "en-us")
        {
            var isDirectLineMode = string.Equals(DirectLineMode, _mode, StringComparison.OrdinalIgnoreCase);

            ViewData["Locale"] = locale;
            ViewData["Mode"] = _mode;
            ViewData["Title"] = BotName;
            ViewData["UserId"] = User.Identity.GetUserName();
            ViewData["UserName"] = User.Identity.GetUserName();

            if (isDirectLineMode)
            {
                var content = string.Empty;
                var directLineToken = string.Empty;

                var directLineClient = new HttpClient();
                var directLineRequest = new HttpRequestMessage(HttpMethod.Post, GenerateDirectLineTokenUrl);

                directLineRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", DirectLineSecret);
                directLineRequest.Content = new StringContent(content, Encoding.UTF8, "application/json");

                var response = await directLineClient.SendAsync(directLineRequest);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    directLineToken = JsonConvert.DeserializeObject<DirectLineResponse>(responseString)?.Token;
                }

                ViewData["DirectLineToken"] = directLineToken;
            }

            return PartialView("_ChatRoom");
        }
    }
}