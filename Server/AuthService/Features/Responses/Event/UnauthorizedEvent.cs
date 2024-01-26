namespace AuthService.Features.Responses.Event
{
    public class UnauthorizedEvent
    {
        public string CorrelationId { get; set; }
        public string Timestamp { get; set; }
        public string Email { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public string Event { get; set; }
        public bool IsSuccessful { get; set; }
        public string FailureReason { get; set; }
    }
}