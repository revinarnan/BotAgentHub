namespace BotAgentHubApp.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        //public RoleType RoleType { get; set; }
        public int ChatHistoryId { get; set; }
    }
}