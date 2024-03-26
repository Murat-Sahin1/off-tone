using System.Text.Json.Serialization;

namespace AuthService.Features.Responses.Identity
{
    public class CreateUserResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string DisplayName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> ErrorList { get; set; }
    }
}
