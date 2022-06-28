using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class StudentFilterRequestModel
    {
        [JsonPropertyName("cardinal")]
        public int? Cardinal { get; set; }

        [JsonPropertyName("depart")]
        public int? DepartId { get; set; }
    }
}
