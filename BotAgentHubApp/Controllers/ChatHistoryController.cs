using BotAgentHubApp.Models;
using BotAgentHubApp.Services;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BotAgentHubApp.Controllers
{
    public class ChatHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AzureBlobService _azureBlobService;

        public ChatHistoryController()
        {
            _azureBlobService = new AzureBlobService();
            _context = new ApplicationDbContext();
        }

        // GET: ChatHistory
        public ActionResult Index()
        {
            if (User.IsInRole("SuperAdmin") || User.IsInRole("StaffAdmin") || User.IsInRole("Kaprodi"))
            {
                var model = from record in _context.ChatHistories
                            orderby record.CreatedAt
                            select record;

                var viewModel = new ChatHistoryViewModels()
                {
                    ChatHistories = model,
                    Transcript = new TranscriptDto()
                };

                return View("ChatHistoryView", viewModel);
            }

            return View("InvalidRole");
        }

        [ActionName("ViewDocument")]
        public async Task<ActionResult> ViewDocumentAsync(string id)
        {
            // Download blob file
            var path = Server.MapPath("~/Files/ChatHistory/") + id;
            await _azureBlobService.DownloadTranscriptAsync(id, path);

            // Deserialize blob file to TranscriptDto model
            var jsonText = System.IO.File.ReadAllText(path);
            var transcript = JsonSerializer.Deserialize<TranscriptDto>(jsonText);

            // Deserialize TextList object to MessageLog model
            var messageObject = transcript.TextList;
            var messageList = JsonSerializer.Deserialize<MessageLog>(messageObject);

            var model = from record in _context.ChatHistories select record;

            var viewModel = new ChatHistoryViewModels()
            {
                ChatHistories = model,
                Transcript = transcript,
                Message = messageList
            };

            return View("DocumentView", viewModel);
        }

        //[ActionName("ViewDocument")]
        //public async Task<ActionResult> ViewDocumentAsync(string id)
        //{
        //    var model = from record in _context.ChatHistories
        //                select record;
        //    var item = await _cosmosDbService.GetItemAsync(id);
        //    var viewModel = new ChatHistoryViewModels()
        //    {
        //        ChatHistories = model,
        //        Cosmos = item
        //    };

        //    ViewBag.Data = item.Document;

        //    return View("DocumentView", viewModel);
        //}
    }
}