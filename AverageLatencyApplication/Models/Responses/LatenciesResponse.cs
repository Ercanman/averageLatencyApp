using AverageLatencyApplication.Models.Dtos;

namespace AverageLatencyApplication.Models.Responses
{
    public class LatenciesResponse
    {
        public List<string> Period { get; set; }
        public List<AverageLatencyDto> AverageLatencies { get; set; }
    }
}
