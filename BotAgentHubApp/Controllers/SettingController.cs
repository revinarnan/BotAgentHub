using BotAgentHubApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace BotAgentHubApp.Controllers
{
    public class SettingController : Controller
    {
        private readonly ApplicationDbContext _context;
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
                           where r.Name == "StaffAdmin" || r.Name == "SuperAdmin"
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

            if (User.IsInRole("SuperAdmin"))
            {
                return View("SettingsView", viewModel);
            }

            if (User.IsInRole("StaffAdmin"))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            if (User.IsInRole("Kaprodi"))
            {
                return RedirectToAction("Index", "ChatHistory");
            }

            return View("InvalidRole");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
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