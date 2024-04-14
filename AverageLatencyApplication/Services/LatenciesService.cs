using AverageLatencyApplication.Dto;
using AverageLatencyApplication.Interfaces;
using AverageLatencyApplication.Mappers;
using AverageLatencyApplication.Models.Responses;

namespace AverageLatencyApplication.Services
{
    public class LatenciesService : ILatenciesService
    {
        private readonly ILogger<ILatenciesService> _logger;
        private readonly Dictionary<int, RequestDelaysForDateResponse> _requestDelayDataDictionary;
        private readonly ILatenciesClient _latenciesClient;

        private const string DATE_FORMAT = "yyyy-MM-dd";

        public LatenciesService(ILatenciesClient latenciesClient, ILogger<ILatenciesService> logger)
        {
            _latenciesClient = latenciesClient;
            _logger = logger;
            _requestDelayDataDictionary = new Dictionary<int, RequestDelaysForDateResponse>();
        }

        public async Task<LatenciesResponse> GetAverageLatenciesForPeriod(DateTime startDate, DateTime endDate)
        {
            await RetrievePopulatedDelayDictionaryForAllDatesInDateSpanAsync(startDate, endDate);

            var serviceLatency = _requestDelayDataDictionary.MapToSpecificServiceLatencyDictionary();

            var listOfAverageLatencies = serviceLatency.MapToListOfAverageLatencyDto();

            return new LatenciesResponse
            {
                Period = new List<string> { startDate.ToString(DATE_FORMAT), endDate.ToString(DATE_FORMAT) },
                AverageLatencies = listOfAverageLatencies
            };
        }

        private async Task RetrievePopulatedDelayDictionaryForAllDatesInDateSpanAsync(DateTime startDate, DateTime endDate)
        {
            var currentDate = startDate;

            while (currentDate <= endDate)
            {
                IEnumerable<RequestDelaysForDateResponse> requestDelayDataList;

                try
                {
                    requestDelayDataList = await _latenciesClient.FetchLatenciesForSpecificDate(currentDate.ToString(DATE_FORMAT));
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw;
                }

                PopulateRequestDelayDictionary(requestDelayDataList);

                currentDate = currentDate.AddDays(1);
            }
        }

        private void PopulateRequestDelayDictionary(IEnumerable<RequestDelaysForDateResponse> requestDelayDataList)
        {
            foreach (var requestDelayData in requestDelayDataList)
            {
                //Assuming that the first one found is the legit one, see additional info in readme
                if (!_requestDelayDataDictionary.ContainsKey(requestDelayData.RequestId))
                {
                    _requestDelayDataDictionary.Add(requestDelayData.RequestId, requestDelayData);
                }
            }
        }
    }

    
}
