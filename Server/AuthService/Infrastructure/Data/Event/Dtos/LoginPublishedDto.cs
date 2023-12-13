namespace AuthService.Infrastructure.Data.Event.Dtos{
    public class LoginPublishedDto{
        public Guid CorrelationId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Event { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserAgent { get; set; }
        public string IPAddress { get; set; }

    }
}