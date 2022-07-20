using Newtonsoft.Json;

namespace BotAgentHubApp.Models
{
    public class CosmosDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "realId")]
        public string RealId { get; set; }

        [JsonProperty(PropertyName = "document")]
        public object Document { get; set; }
    }
}
