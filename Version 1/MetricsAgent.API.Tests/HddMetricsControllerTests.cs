using System;
using Xunit;
using MetricsAgent.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Services.Repository;

namespace MetricsAgent.API.Tests
{
    public class HddMetricsControllerTests
    {
        private readonly HddMetricsController _controller;
        private readonly IRepositoryService<HddMetricDto> _service;
        public HddMetricsControllerTests()
        {
            Mock<ILogger<HddMetricsController>> logger = new Mock<ILogger<HddMetricsController>>();
            Mock<IRepositoryService<HddMetricDto>> _service = new Mock<IRepositoryService<HddMetricDto>>();
            _controller = new HddMetricsController(logger.Object, _service.Object);
        }
        [Fact]
        public void GetAvailableSpace_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            //Act
            var result = _controller.GetAvailableSpace(fromTime,toTime);
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
