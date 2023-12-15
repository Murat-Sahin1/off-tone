namespace AuthService.Features.Responses.Event{
    public class SuccessfulLoginEvent{
        public Guid CorrelationId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public string Event { get; set; }
        public bool IsSucceessful { get; set; }
    }
}