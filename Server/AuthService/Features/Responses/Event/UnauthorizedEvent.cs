namespace AuthService.Features.Responses.Event
{
    public class UnauthorizedEvent
    {
        public Guid CorrelationId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public string Event { get; set; }
        public bool IsSuccessful { get; set; }
        public string FailureReason { get; set; }
    }
}