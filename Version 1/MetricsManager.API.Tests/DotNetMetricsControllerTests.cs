using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.API.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsManager.Services;
using MetricsManager.Services.Dto;

namespace MetricsManager.API.Tests
{
    public class DotNetMetricsControllerTests
    {
        private readonly DotNetMetricsController _controller;
        private readonly Mock<IRepositoryService<DtoDotNetMetric>> _repository;
        public DotNetMetricsControllerTests()
        {
            Mock<ILogger<DotNetMetricsController>> logger = new Mock<ILogger<DotNetMetricsController>>();
            _repository = new Mock<IRepositoryService<DtoDotNetMetric>>();
            _controller = new DotNetMetricsController(logger.Object, _repository.Object);
        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            int agentId = 1;
            TimeSpan fromTIme = new TimeSpan(10);
            TimeSpan toTime = new TimeSpan(100);
            //Act
            var result = _controller.GetMetricsFromAgent(agentId, fromTIme, toTime);
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            //Arrange
            TimeSpan fromTIme = new TimeSpan(10);
            TimeSpan toTime = new TimeSpan(100);
            //Act
            var result = _controller.GetMetricsFromAllCluster(fromTIme, toTime);
            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
