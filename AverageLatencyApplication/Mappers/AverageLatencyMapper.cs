using AverageLatencyApplication.Models.Dtos;

namespace AverageLatencyApplication.Mappers
{
    public static class AverageLatencyMapper
    {
        public static List<AverageLatencyDto> MapToListOfAverageLatencyDto(this Dictionary<int, SpecificServiceLatencyDto> input)
        {

            var list = new List<AverageLatencyDto>();

            foreach (var serviceData in input)
            {
                var averageResponseTimeInMs = Convert.ToInt32(((double)serviceData.Value.TotalResponseTime / (double)serviceData.Value.NumberOfRequests));

                var averageLatencyForService = new AverageLatencyDto
                {
                    ServiceId = serviceData.Key,
                    NumberOfRequests = serviceData.Value.NumberOfRequests,
                    AverageResponseTimeMs = averageResponseTimeInMs
                };

                list.Add(averageLatencyForService);
            }

            return list.OrderBy(x => x.ServiceId).ToList();
        }
    }
}
