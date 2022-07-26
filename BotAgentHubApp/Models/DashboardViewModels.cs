using System.Collections.Generic;

namespace BotAgentHubApp.Models
{
    public class DashboardViewModels
    {
        public EmailModel EmailModel { get; set; }
        public IEnumerable<ChatBotEmailQuestion> EmailQuestions { get; set; }
        public ChatBotEmailQuestion BotEmailQuestion { get; set; }
        public string Notification { get; set; }
    }
}