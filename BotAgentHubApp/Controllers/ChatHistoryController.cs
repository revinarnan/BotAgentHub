using BotAgentHubApp.Models;
using BotAgentHubApp.Services;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BotAgentHubApp.Controllers
{
    public class ChatHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CosmosDbService _cosmosDbService;

        public ChatHistoryController()
        {
            _cosmosDbService = new CosmosDbService();
            _context = new ApplicationDbContext();
        }

        // GET: ChatHistory
        public ActionResult Index()
        {
            var model = _context.ChatHistories.OrderBy(c => c.Date).ToList();

            if (User.IsInRole("SuperAdmin") || User.IsInRole("StaffAdmin") || User.IsInRole("Kaprodi"))
            {
                return View("ChatHistoryView", model);
            }

            return View("InvalidRole");
        }

        public FileResult DownloadFile(string fileName)
        {
            string path = Server.MapPath("~/Files/ChatHistory/") + fileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", fileName);
        }

        [ActionName("ViewDocument")]
        public async Task<ActionResult> ViewDocumentAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "naughty");
            }

            var item = await _cosmosDbService.GetItemAsync(id);
            if (item == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "naughty");
            }

            return View("ChatHistoryView", item);
        }
    }
}