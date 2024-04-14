using AverageLatencyApplication.Models.Responses;

namespace AverageLatencyApplication.Interfaces
{
    public interface ILatenciesService
    {
        public Task<LatenciesResponse> GetAverageLatenciesForPeriod(DateTime startDate, DateTime endDate);
    }
}
