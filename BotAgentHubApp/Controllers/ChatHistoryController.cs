using BotAgentHubApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace BotAgentHubApp.Controllers
{
    public class ChatHistoryController : Controller
    {
        private ApplicationDbContext _context;

        public ChatHistoryController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: ChatHistory
        public ActionResult Index()
        {
            var model = _context.ChatHistories.OrderBy(c => c.Date).ToList();

            if (!User.IsInRole("SuperAdmin, StaffAdmin, Kaprodi"))
                return View("InvalidRole");

            return View("ChatHistoryView", model);
        }

        public FileResult DownloadFile(string fileName)
        {
            string path = Server.MapPath("~/Files/ChatHistory/") + fileName;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", fileName);
        }
    }
}