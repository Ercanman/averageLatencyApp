using AverageLatencyApplication.Dto;
using AverageLatencyApplication.Interfaces;
using AverageLatencyApplication.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [Fact]
        public async Task LatenciesService_ShouldAggregateResults_AndReturnSortedServicesAsync()
        {
            var client = new Mock<ILatenciesClient>();
            var sut = new LatenciesService(client.Object, _loggerMock.Object);

            var firstDay = new List<RequestDelaysForDateResponse>
            {
                new() { RequestId = 1, ServiceId = 1, MilliSecondsDelay = 100 },
                new() { RequestId = 2, ServiceId = 2, MilliSecondsDelay = 200 }
            };

            var secondDay = new List<RequestDelaysForDateResponse>
            {
                new() { RequestId = 1, ServiceId = 1, MilliSecondsDelay = 400 },
                new() { RequestId = 3, ServiceId = 1, MilliSecondsDelay = 300 }
            };

            client.SetupSequence(x => x.FetchLatenciesForSpecificDate(It.IsAny<string>()))
                .ReturnsAsync(firstDay)
                .ReturnsAsync(secondDay);

            var response = await sut.GetAverageLatenciesForPeriod(new DateTime(2023, 01, 01), new DateTime(2023, 01, 02));

            response.AverageLatencies.Should().HaveCount(2);
            response.AverageLatencies.Select(x => x.ServiceId).Should().ContainInOrder(new[] { 1, 2 });

            var firstService = response.AverageLatencies.First(x => x.ServiceId == 1);
            firstService.NumberOfRequests.Should().Be(2);
            firstService.AverageResponseTimeMs.Should().Be(200);

            var secondService = response.AverageLatencies.First(x => x.ServiceId == 2);
            secondService.NumberOfRequests.Should().Be(1);
            secondService.AverageResponseTimeMs.Should().Be(200);
        }
    }
}
