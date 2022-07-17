using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class NewDepartRequestModel
    {
        [JsonPropertyName("desc")]
        public string Desc;
    }
}
