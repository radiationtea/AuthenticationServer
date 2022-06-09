using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class UserModifyRequestModel
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("reset_password")]
        public bool ResetPassword { get; set; } = false;

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("role_to_remove")]
        public int[] RolesToRemove { get; set; } = Array.Empty<int>();

        [JsonPropertyName("role_to_add")]
        public int[] RolesToAdd { get; set; } = Array.Empty<int>();
    }
}
