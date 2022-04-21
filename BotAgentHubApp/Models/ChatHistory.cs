namespace BotAgentHubApp.Models
{
    public class ChatHistory : BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsDoneOnBot { get; set; }
        public bool IsDoneOnLiveChat { get; set; }
        public bool IsDoneOnEmail { get; set; }
        public string ChatHistoryFileName { get; set; }
    }
}