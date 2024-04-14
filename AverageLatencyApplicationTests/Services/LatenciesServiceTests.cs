using AverageLatencyApplication.Dto;
using AverageLatencyApplication.Interfaces;
using AverageLatencyApplication.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AverageLatencyApplicationTests.Services
{
    public class LatenciesServiceTests
    {
        private readonly LatenciesService _sut;
        private readonly Mock<ILatenciesClient> _client;
        private readonly Mock<ILogger<ILatenciesService>> _loggerMock;

        public LatenciesServiceTests()
        {
            _client = new Mock<ILatenciesClient>();
            _loggerMock = new Mock<ILogger<ILatenciesService>>();
            _sut = new LatenciesService(_client.Object, _loggerMock.Object);

            _client.Setup(x => x.FetchLatenciesForSpecificDate(It.IsAny<string>()))
                .ReturnsAsync(new List<RequestDelaysForDateResponse>());
        }

        [Fact]
        public async Task LatenciesService_ShouldReturn_CorrectDateFormatAsync()
        {
            var response = await _sut.GetAverageLatenciesForPeriod(new DateTime(2023, 01, 01), new DateTime(2023, 01, 02));

            response.Period[0].Should().Be("2023-01-01");
            response.Period[1].Should().Be("2023-01-02");
        }
    }
}