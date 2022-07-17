using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class DepartModifyRequestModel
    {
        [JsonPropertyName("dep_id")]
        public int DepId;
        
        [JsonPropertyName("desc")]
        public string Desc;
    }
}
