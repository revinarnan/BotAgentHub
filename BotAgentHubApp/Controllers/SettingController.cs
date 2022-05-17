using BotAgentHubApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace BotAgentHubApp.Controllers
{
    public class SettingController : Controller
    {
        private ApplicationDbContext _context;
        public SettingController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Setting
        public ActionResult Index()
        {
            var chatbotConfig = _context.ChatbotConfigurations.SingleOrDefault();
            var roleName = from u in _context.Users
                           join ur in _context.UserRoles on u.Id equals ur.UserId
                           join r in _context.Roles on ur.RoleId equals r.Id
                           select new UserRoleDto { UserName = u.UserName, RoleName = r.Name };

            var viewModel = new SettingViewModels
            {
                Users = _context.Users.ToList(),
                Roles = _context.Roles.ToList(),
                UserRole = roleName.ToList(),
                ChatbotConfiguration = chatbotConfig
            };

            ViewBag.Name = new SelectList(viewModel.Roles, "Name", "Name");
            ViewBag.UserName = new SelectList(viewModel.Users, "UserName", "UserName");

            return View("SettingsView", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SettingViewModels models)
        {
            var botInDb = _context.ChatbotConfigurations.Single();
            botInDb.UrlClient = models.ChatbotConfiguration.UrlClient;
            botInDb.UrlKb = models.ChatbotConfiguration.UrlKb;

            _context.SaveChanges();

            return RedirectToAction("Index", "Setting");
        }
    }
}