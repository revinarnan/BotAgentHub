using BotAgentHubApp.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BotAgentHubApp.Controllers
{
    public class DashboardController : Controller
    {
        //private static string _directLineSecret = ConfigurationManager.AppSettings["DirectLineSecret"];
        //private static string _botId = ConfigurationManager.AppSettings["BotId"];
        //private static string _fromUser = "agent";

        private const string DirectLineMode = "DIRECTLINE";
        private const string GenerateDirectLineTokenUrl = "https://directline.botframework.com/v3/directline/tokens/generate";
        private const string UnknownMode = "UNKNOWN";

        private static bool _enableDirectLineEnhancedAuthentication =
            Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableDirectLineEnhancedAuthentication"]);
        private static string _directLineSecret = WebConfigurationManager.AppSettings["DirectLineSecret"];
        private static string _botName = WebConfigurationManager.AppSettings["BotName"];

        private readonly string mode;
        private readonly string userId;

        private ApplicationDbContext _context;

        public DashboardController()
        {
            _context = new ApplicationDbContext();

            if (_enableDirectLineEnhancedAuthentication)
            {
                this.userId = $"dl_{Guid.NewGuid()}";
            }

            // Determine which mode to operate under:
            // - Direct Line Speech should be used if a speech service region identifier and key are provided
            // - Direct Line should be used if a DL secret is provided
            // - Default to an unknown state (i.e. invalid app configuration provided)
            if (!string.IsNullOrEmpty(_directLineSecret))
            {
                this.mode = DirectLineMode;
            }
            else
            {
                this.mode = UnknownMode;
            }
        }

        public ActionResult Index()
        {
            if (User.IsInRole("SuperAdmin") || User.IsInRole("StaffAdmin"))
            {
                return View();
            }

            return View("InvalidRole");
        }

        public ActionResult DirectLineTest(string locale = "en-us", string user = "agent")
        {
            bool isDirectLineMode = string.Equals(DirectLineMode, this.mode, StringComparison.OrdinalIgnoreCase);

            ViewData["Locale"] = locale;
            ViewData["Mode"] = this.mode;
            ViewData["Title"] = _botName;
            ViewData["UserId"] = user;
            ViewData["UserName"] = user;

            if (isDirectLineMode)
            {
                string directLineToken = string.Empty;

                var directLineClient = new HttpClient();
                directLineClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    _directLineSecret);

                string content = _enableDirectLineEnhancedAuthentication
                    ? JsonConvert.SerializeObject(
                        new
                        {
                            User = new
                            {
                                Id = this.userId
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

            return View();
        }


    }
}