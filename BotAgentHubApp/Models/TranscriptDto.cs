using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BotAgentHubApp.Models
{
    public class TranscriptDto
    {
        [JsonPropertyName("ConversationId")]
        public string ConversationId { get; set; }

        [JsonPropertyName("Date")]
        public string Date { get; set; }

        [JsonConverter(typeof(StringConverter))]
        [JsonPropertyName("TextList")]
        public string TextList { get; set; }
    }

    public class MessageLog
    {
        [JsonPropertyName("$values")]
        public List<string> MessageList { get; set; }
    }

    public class StringConverter : JsonConverter<string>
    {
        public override string Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var jsonDoc = JsonDocument.ParseValue(ref reader))
            {
                return jsonDoc.RootElement.GetRawText();
            }
        }

        public override void Write(
            Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
