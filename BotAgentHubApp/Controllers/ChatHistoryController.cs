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
            var model = _context.ChatHistories.OrderBy(c => c.Date);

            return View("ChatHistoryView", model);
        }
    }
}