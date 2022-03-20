using System;
using Xunit;
using MetricsAgent.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Services.Repository;

namespace MetricsAgent.API.Tests
{
    public class NetworkMetricsControllerTests
    {
        private readonly NetworkMetricsController _controller;
        private readonly IRepositoryService<NetworkMetricDto> _service;
        public NetworkMetricsControllerTests()
        {
            Mock<ILogger<NetworkMetricsController>> logger = new Mock<ILogger<NetworkMetricsController>>();
            Mock<IRepositoryService<NetworkMetricDto>> _service = new Mock<IRepositoryService<NetworkMetricDto>>();
            _controller = new NetworkMetricsController(logger.Object, _service.Object);
        }
        [Fact]
        public void GetLoadFromTime_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            //Act
            var result = _controller.GetMetrics(fromTime, toTime);
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
