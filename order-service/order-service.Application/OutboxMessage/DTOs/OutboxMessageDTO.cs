namespace order_service.Application.OutboxMessage.DTOs
{
    public class OutboxMessageDTO
    {
        public string EventType { get; set; }
        public string Payload { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ProcessedAt { get; set; }
        public string Status { get; set; }
        public int RetryCount { get; set; }
        public string ErrorMessage { get; set; }
    }
}
