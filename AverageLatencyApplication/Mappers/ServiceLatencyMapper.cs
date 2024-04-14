using AverageLatencyApplication.Dto;
using AverageLatencyApplication.Models.Dtos;

namespace AverageLatencyApplication.Mappers
{
    public static class ServiceLatencyMapper
    {
        public static Dictionary<int, SpecificServiceLatencyDto> MapToSpecificServiceLatencyDictionary(this Dictionary<int, RequestDelaysForDateResponse> input)
        {
            var serviceLatencyDataDictionary = new Dictionary<int, SpecificServiceLatencyDto>();
            foreach (var requestData in input)
            {

                var dictionaryContainsEntry = serviceLatencyDataDictionary.TryGetValue(requestData.Value.ServiceId, out var existingEntry);

                if (dictionaryContainsEntry)
                {
                    existingEntry!.TotalResponseTime += requestData.Value.MilliSecondsDelay;
                    existingEntry.NumberOfRequests++;
                }
                else
                {
                    var newEntry = new SpecificServiceLatencyDto
                    {
                        NumberOfRequests = 1,
                        TotalResponseTime = requestData.Value.MilliSecondsDelay
                    };

                    serviceLatencyDataDictionary.Add(requestData.Value.ServiceId, newEntry);
                }
            }

            return serviceLatencyDataDictionary;
        }
    }
}
