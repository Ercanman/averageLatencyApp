using AverageLatencyApplication.Dto;

namespace AverageLatencyApplication.Interfaces
{
    public interface ILatenciesClient
    {
        Task<IEnumerable<RequestDelaysForDateResponse>> FetchLatenciesForSpecificDate(string date);
    }
}
