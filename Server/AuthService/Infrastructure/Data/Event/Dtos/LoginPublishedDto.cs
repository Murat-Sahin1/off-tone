namespace AuthService.Infrastructure.Data.Event.Dtos{
    public class LoginPublishedDto{
        public string Email { get; set; }
        public string Password { get; set; }
        public string Event { get; set; }
    }
}