using System.Collections.Generic;

namespace BotAgentHubApp.Models
{
    public class ChatHistoryViewModels
    {
        public IEnumerable<ChatHistory> ChatHistories { get; set; }
        public TranscriptDto Transcript { get; set; }
        public MessageLog Message { get; set; }
    }
}