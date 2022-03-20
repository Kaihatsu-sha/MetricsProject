using System;
using Xunit;
using MetricsAgent.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Services.Repository;

namespace MetricsAgent.API.Tests
{
    public class RamMetricsControllerTests
    {
        private readonly RamMetricsController _controller;
        private readonly IRepositoryService<RamMetricDto> _service;
        public RamMetricsControllerTests()
        {
            Mock<ILogger<RamMetricsController>> logger = new Mock<ILogger<RamMetricsController>>();
            Mock<IRepositoryService<RamMetricDto>> _service = new Mock<IRepositoryService<RamMetricDto>>();
            _controller = new RamMetricsController(logger.Object, _service.Object);
        }
        [Fact]
        public void GetAvailableMemory_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            //Act
            var result = _controller.GetAvailableMemory(fromTime, toTime);
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
