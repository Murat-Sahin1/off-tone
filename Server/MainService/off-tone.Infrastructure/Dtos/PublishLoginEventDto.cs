namespace off_tone.Infrastructure.Dtos
{
    public class PublishLoginEventDto
    {
        public string CorrelationId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Event { get; set; }
        public string Timestamp { get; set; }
        public string UserAgent { get; set; }
        public string IPAddress { get; set; }
    }
}
