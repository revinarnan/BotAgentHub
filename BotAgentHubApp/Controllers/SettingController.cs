using BotAgentHubApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;

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
            if (User.IsInRole("StaffAdmin"))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            if (User.IsInRole("Kaprodi"))
            {
                return RedirectToAction("Index", "ChatHistory");
            }

            var chatbotConfig = _context.ChatbotConfigurations.SingleOrDefault();
            var roleName = from u in _context.Users
                           join ur in _context.UserRoles on u.Id equals ur.UserId
                           join r in _context.Roles on ur.RoleId equals r.Id
                           where r.Name == "StaffAdmin" || r.Name == "SuperAdmin"
                           orderby r.Name descending
                           select new UserRoleDto { UserName = u.UserName, RoleName = r.Name };
            var userList = new List<ApplicationUser>();
            foreach (var user in _context.Users)
            {
                if (user.UserName != User.Identity.Name)
                {
                    userList.Add(user);
                }
            }

            var viewModel = new SettingViewModels
            {
                Users = userList,
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

            return View("InvalidRole");
        }

        // POST /Setting/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Save(SettingViewModels models)
        {
            if (!ModelState.IsValid) RedirectToAction("Index", "Setting");

            var botInDb = _context.ChatbotConfigurations.Single();
            botInDb.UrlClient = models.ChatbotConfiguration.UrlClient;
            botInDb.UrlKb = models.ChatbotConfiguration.UrlKb;

            _context.SaveChanges();

            return RedirectToAction("Index", "Setting");
        }
    }
}