using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class NewUserRequestModel
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }
}
