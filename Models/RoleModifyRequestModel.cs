using System.Text.Json.Serialization;
using Auth.Database.Models;

namespace Auth.Models
{
    public class RoleModifyRequestModel
    {
        [JsonPropertyName("role_id")]
        public int RoleId;

        [JsonPropertyName("label")]
        public string? Label;

        [JsonPropertyName("perms_to_add")]
        public string[] PermissionsToAdd = Array.Empty<string>();

        [JsonPropertyName("perms_to_remove")]
        public uint[] PermissionsToRemove = Array.Empty<uint>();
    }
}
