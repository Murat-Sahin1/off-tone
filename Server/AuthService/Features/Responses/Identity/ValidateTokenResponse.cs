namespace AuthService.Features.Responses.Identity
{
    public class ValidateTokenResponse
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }
}
