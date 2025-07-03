using System.Text.Json.Serialization;

namespace CaseItau.Application.DTOs.Requests
{
    public class UpdateNetWorthRequestDto
    {
        [JsonIgnore]
        public string Code { get; set; }

        public decimal Amount { get; set; }
    }
}
