using System.Text.Json.Serialization;

namespace AuthService.Features.Responses.Identity
{
    public class UpdateUserNameResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string NewUserName { get; set; }
    }
}
