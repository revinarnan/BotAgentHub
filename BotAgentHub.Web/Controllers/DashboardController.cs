using BotAgentHub.Data.Models;
using BotAgentHub.Data.Services;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotAgentHub.Web.Controllers
{
    public class DashboardController : Controller
    {
        private BotAgentHubDbContext _context;

        public DashboardController()
        {
            _context = new BotAgentHubDbContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _context.BotConfigurations.OrderBy(b => b.Name);

            return !model.Any() ? View("Empty") : View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = _context.BotConfigurations.FirstOrDefault(b => b.Id == id);

            return model == null ? View("NotFound") : View(model);
        }

        [HttpGet]
        public ActionResult NewBot()
        {
            return View("BotConfigForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BotConfiguration botConfiguration, HttpPostedFileBase botImageFile)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                if (botImageFile != null)
                {
                    var extension = Path.GetExtension(botImageFile.FileName).ToLower();

                    if (allowedExtensions.Contains(extension))
                    {
                        SavePhoto(botConfiguration, botImageFile);
                    }
                }

                _context.BotConfigurations.Add(botConfiguration);
                _context.SaveChanges();

                return RedirectToAction("Index", "Dashboard");
            }

            //var errors = ModelState
            //    .Where(x => x.Value.Errors.Count > 0)
            //    .Select(x => new { x.Key, x.Value.Errors })
            //    .ToArray();

            return View("BotConfigForm", botConfiguration);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _context.BotConfigurations.SingleOrDefault(b => b.Id == id);

            if (model == null)
                return View("NotFound");

            return View("BotConfigForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BotConfiguration botConfiguration, HttpPostedFileBase botImageFile)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                if (botImageFile != null)
                {
                    var extension = Path.GetExtension(botImageFile.FileName).ToLower();

                    if (allowedExtensions.Contains(extension))
                    {
                        SavePhoto(botConfiguration, botImageFile);
                    }
                }

                var botInDb = _context.BotConfigurations.Single(b => b.Id == botConfiguration.Id);
                botInDb.Name = botConfiguration.Name;
                botInDb.BotImageName = botConfiguration.BotImageName;
                botInDb.UrlClient = botConfiguration.UrlClient;
                botInDb.UrlKb = botConfiguration.UrlKb;
                botInDb.UserId = botConfiguration.UserId;

                _context.SaveChanges();

                return RedirectToAction("Index", "Dashboard");
            }

            //var errors = ModelState
            //    .Where(x => x.Value.Errors.Count > 0)
            //    .Select(x => new { x.Key, x.Value.Errors })
            //    .ToArray();

            return View("Details", botConfiguration);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection form)
        {
            var model = _context.BotConfigurations.SingleOrDefault(b => b.Id == id);

            if (model == null)
                return View("NotFound");

            _context.BotConfigurations.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private void SavePhoto(BotConfiguration botConfiguration, HttpPostedFileBase botImageFile)
        {
            const string imagesPath = "~/Content/Images/BotConfig/";

            //Delete existing photo
            if (botConfiguration.BotImageName != null)
            {
                System.IO.File.Delete(Path.Combine(Server.MapPath(imagesPath), botConfiguration.BotImageName));
            }
            //Save new photo
            var fileName = DateTime.Now.ToString("hhmmss_ddMMyyyy") + "_"
                                                               + Path.GetFileName(botImageFile.FileName);

            var filePath = Path.Combine(Server.MapPath(imagesPath), fileName);

            botImageFile.SaveAs(filePath);
            botConfiguration.BotImageName = fileName;
        }
    }

}