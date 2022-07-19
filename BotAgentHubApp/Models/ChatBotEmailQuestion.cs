namespace BotAgentHubApp.Models
{
    public class ChatBotEmailQuestion
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Question { get; set; }
        public bool IsAnswered { get; set; }
    }
}