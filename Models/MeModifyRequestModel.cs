using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class MeModifyRequestModel
    {
        [JsonPropertyName("old_password")]
        public string? OldPassword;
        [JsonPropertyName("new_password")]
        public string? NewPassword;
    }
}
