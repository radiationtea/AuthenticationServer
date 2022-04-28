using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class NewUserRequestModel
    {
        [JsonPropertyName("cardinal")]
        public int Cardinal { get; set; }
        [JsonPropertyName("dep")]
        public int DepId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }
}
