using System.Text.Json.Serialization;
using Auth.Constants;

namespace Auth.Models
{
    public class GeneralResponseModel
    {
        public bool Success = true;
        public object? Data;
        public string? Code = ResponseCode.OK;
    }
}
