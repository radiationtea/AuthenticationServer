using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class NewUserRequestModel
    {
        [JsonPropertyName("cardinal")]
        public int Cardinal { get; set; }

        [JsonPropertyName("users")]
        public IEnumerable<UserSubRequestModel> Users { get; set; }

        [JsonPropertyName("dep")]
        public int DepId { get; set; }
    }

    public class UserSubRequestModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }
}
