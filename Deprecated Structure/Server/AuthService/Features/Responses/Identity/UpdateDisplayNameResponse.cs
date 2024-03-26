using System.Text.Json.Serialization;

namespace AuthService.Features.Responses.Identity
{
    public class UpdateDisplayNameResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string NewDisplayName { get; set; }
    }
}
