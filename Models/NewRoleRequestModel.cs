using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class NewRoleRequestModel
    {
        [JsonPropertyName("label")]
        public string Label;
    }
}
