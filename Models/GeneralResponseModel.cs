using System.Text.Json.Serialization;
using Auth.Constants;

namespace Auth.Models
{
    public class GeneralResponseModel
    {
        public bool Success { get; set; } = true;
        public object? Data { get; set; }
        public string? Code { get; set; } = ResponseCode.OK;
    }
}
