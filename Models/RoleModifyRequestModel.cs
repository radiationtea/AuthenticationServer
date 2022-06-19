using System.Text.Json.Serialization;
using Auth.Database.Models;

namespace Auth.Models
{
    public class RoleModifyRequestModel
    {
        [JsonPropertyName("role_id")]
        public int RoleId { get; set; }

        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("perm_to_add")]
        public string[] PermissionsToAdd { get; set; } = Array.Empty<string>();

        [JsonPropertyName("perm_to_remove")]
        public uint[] PermissionsToRemove { get; set; } = Array.Empty<uint>();
    }
}
