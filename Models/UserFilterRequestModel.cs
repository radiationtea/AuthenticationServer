using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class UserFilterRequestModel
    {
        [JsonPropertyName("excludeStudent")]
        public bool ExcludeStudent { get; set; } = false;
    }
}
