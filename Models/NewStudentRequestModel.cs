using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class NewStudentRequestModel
    {
        [JsonPropertyName("cardinal")]
        public uint Cardinal { get; set; }

        [JsonPropertyName("users")]
        public IEnumerable<StudentSubRequestModel> Users { get; set; }

        [JsonPropertyName("dep")]
        public uint DepId { get; set; }
    }

    public class StudentSubRequestModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
    }
}
