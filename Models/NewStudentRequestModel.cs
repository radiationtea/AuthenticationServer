using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class NewStudentRequestModel
    {
        [JsonPropertyName("cardinal")]
        public uint Cardinal;

        [JsonPropertyName("users")]
        public IEnumerable<StudentSubRequestModel> Users;

        [JsonPropertyName("dep")]
        public uint DepId;
    }

    public class StudentSubRequestModel
    {
        [JsonPropertyName("name")]
        public string Name;
        [JsonPropertyName("phone")]
        public string Phone;
    }
}
