using Newtonsoft.Json;

namespace BotAgentHubApp.Models
{
    [JsonObject]
    public class DirectLineResponse
    {
        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}