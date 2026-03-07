using System.Text.Json.Serialization;

namespace Nutrilab.Shared.Models.Exceptions.Models
{
    public class ErrorResponse
    {
        [JsonPropertyName("message")]
        public string Msg { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}
