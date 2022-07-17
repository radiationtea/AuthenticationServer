using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class PermissionsRequestModel
    {
        [JsonPropertyName("user_id")]
        public string UserId;
    }
}
