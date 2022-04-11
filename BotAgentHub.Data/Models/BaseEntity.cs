namespace BotAgentHub.Data.Models
{
    public abstract class BaseEntity
    {
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
