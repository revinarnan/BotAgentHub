using BotAgentHub.Data.Services;
using System.Linq;
using System.Web.Mvc;

namespace BotAgentHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BotAgentHubDbContext _context;

        public HomeController()
        {
            _context = new BotAgentHubDbContext();
        }

        public ActionResult Index()
        {
            var model = _context.BotConfigurations.OrderBy(b => b.Name);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}