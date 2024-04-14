namespace AverageLatencyApplication.Models.Dtos
{
    public class AverageLatencyDto
    {
        public int ServiceId { get; set; }
        public int NumberOfRequests { get; set; }
        public int AverageResponseTimeMs { get; set; }
    }
}
