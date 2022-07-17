using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.Models
{
    public class GeneralPaginationRequestModel
    {
        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        [Range(1, 50, ErrorMessage = "Limit must be between 1 to 50")]
        [JsonPropertyName("limit")]
        public int Limit { get; set; } = 10;
    }
}
