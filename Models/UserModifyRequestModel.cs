using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class UserModifyRequestModel
    {
        [JsonPropertyName("user_id")]
        public string UserId;

        [JsonPropertyName("reset_password")]
        public bool ResetPassword = false;

        [JsonPropertyName("phone")]
        public string? Phone = null;

        [JsonPropertyName("role_to_remove")]
        public int[] RolesToRemove = Array.Empty<int>();

        [JsonPropertyName("role_to_add")]
        public int[] RolesToAdd = Array.Empty<int>();
    }
}
