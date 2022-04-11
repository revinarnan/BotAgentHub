namespace BotAgentHub.Data.Models
{
    public class ChatHistory
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int BotConfigurationId { get; set; }
        public int UserId { get; set; }
    }
}
