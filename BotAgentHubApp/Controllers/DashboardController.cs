using System.Web.Mvc;

namespace BotAgentHubApp.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return !User.IsInRole("SuperAdmin, StaffAdmin") ? View("InvalidRole") : View();
        }
    }
}