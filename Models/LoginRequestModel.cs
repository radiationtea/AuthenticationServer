using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Auth.Models
{
    public class LoginRequestModel
    {
        [JsonPropertyName("user_id")]
        public string UserId;

        [JsonPropertyName("password")]
        public string Password;
    }
}
