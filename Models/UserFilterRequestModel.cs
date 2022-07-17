using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class UserFilterRequestModel
    {
        [JsonPropertyName("onlyStudent")]
        public bool OnlyStudent { get; set; } = false;
    }
}
