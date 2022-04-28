using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class GeneralResponseModel
    {
        public bool Success { get; set; } = true;
        public object? Data { get; set; }
        public string? Message { get; set; }
    }
}
