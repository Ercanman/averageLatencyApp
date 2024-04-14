using AverageLatencyApplication.Dto;
using AverageLatencyApplication.Interfaces;
using System.Text.Json;

namespace AverageLatencyApplication.Clients
{
    public class LatenciesClient : ILatenciesClient
    {
        private readonly HttpClient _httpClient;
        public LatenciesClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LatencyClient");
        }

        public async Task<IEnumerable<RequestDelaysForDateResponse>> FetchLatenciesForSpecificDate(string date)
        {
            var response = await _httpClient.GetAsync($"/latencies?date={date}");

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve latencies for date with exception code {response.StatusCode}");
            }

            var responseData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var requestDelayDataList = JsonSerializer.Deserialize<IEnumerable<RequestDelaysForDateResponse>>(responseData, options);

            if(requestDelayDataList == null)
            {
                throw new Exception("Failed to deserialize response");
            }

            return requestDelayDataList;
        }
    }
}
