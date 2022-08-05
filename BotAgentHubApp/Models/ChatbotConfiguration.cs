using System.ComponentModel.DataAnnotations;

namespace BotAgentHubApp.Models
{
    public class ChatbotConfiguration : BaseEntity
    {
        public int Id { get; set; }

        [Display(Name = "URL Web Client")]
        public string UrlClient { get; set; }

        [Display(Name = "URL Knowledge Base")]
        public string UrlKb { get; set; }
    }
}