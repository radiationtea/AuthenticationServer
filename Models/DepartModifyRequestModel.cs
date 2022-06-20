using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class DepartModifyRequestModel
    {
        [JsonPropertyName("dep_id")]
        public int DepId { get; set; }
        
        [JsonPropertyName("desc")]
        public string desc { get; set; }
    }
}
