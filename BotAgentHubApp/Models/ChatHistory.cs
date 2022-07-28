using System.ComponentModel.DataAnnotations;

namespace BotAgentHubApp.Models
{
    public class ChatHistory : BaseEntity
    {
        public string UserId { get; set; }
        public bool IsDoneOnBot { get; set; }
        public bool IsDoneOnLiveChat { get; set; }
        public bool IsDoneOnEmail { get; set; }
        [Key]
        public string ChatHistoryFileName { get; set; } // Conversation ID
    }
}