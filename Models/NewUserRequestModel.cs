using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class NewUserRequestModel
    {
        [JsonPropertyName("user_id")]
        public string UserId;

        [JsonPropertyName("name")]
        public string Name;

        [JsonPropertyName("phone")]
        public string Phone;
    }
}
