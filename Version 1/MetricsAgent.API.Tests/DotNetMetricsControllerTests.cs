using System;
using Xunit;
using MetricsAgent.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsAgent.Services.Repository;

namespace MetricsAgent.API.Tests
{
    public class DotNetMetricsControllerTests
    {
        private readonly DotNetMetricsController _controller;
        private readonly IRepositoryService<DotNetMetricDto> _service;
        public DotNetMetricsControllerTests()
        {
            Mock<ILogger<DotNetMetricsController>> logger = new Mock<ILogger<DotNetMetricsController>>();
            Mock<IRepositoryService<DotNetMetricDto>> _service = new Mock<IRepositoryService<DotNetMetricDto>>();
            _controller = new DotNetMetricsController(logger.Object, _service.Object);
        }
        [Fact]
        public void GetErrorsFromTime_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            //Act
            var result = _controller.GetErrorsFromTime(fromTime, toTime);
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
