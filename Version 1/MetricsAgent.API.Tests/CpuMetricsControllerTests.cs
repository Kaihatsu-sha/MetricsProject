using System;
using Xunit;
using MetricsAgent.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Services.Repository;

namespace MetricsAgent.API.Tests
{
    public class CpuMetricsControllerTests
    {
        private readonly CpuMetricsController _controller;
        private readonly IRepositoryService<CpuMetricDto> _service;
        public CpuMetricsControllerTests()
        {
            Mock<ILogger<CpuMetricsController>> logger = new Mock<ILogger<CpuMetricsController>>();
            Mock<IRepositoryService<CpuMetricDto>> _service = new Mock<IRepositoryService<CpuMetricDto>>();
            _controller = new CpuMetricsController(logger.Object,  _service.Object);
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
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
