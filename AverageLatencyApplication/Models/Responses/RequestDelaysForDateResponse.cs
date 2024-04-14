namespace AverageLatencyApplication.Dto
{
    public class RequestDelaysForDateResponse
    {
        public int RequestId { get; set; }
        public int ServiceId { get; set; }
        public string Date { get; set; }
        public int MilliSecondsDelay { get; set; }

    }
}
