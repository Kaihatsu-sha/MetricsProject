using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.API.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsManager.Services.Dto;
using MetricsManager.Services;

namespace MetricsManager.API.Tests
{
    public class RamMetricsControllerTests
    {
        private readonly RamMetricsController _controller;
        private readonly Mock<IRepositoryService<DtoRamMetric>> _repository;
        public RamMetricsControllerTests()
        {
            Mock<ILogger<RamMetricsController>> logger = new Mock<ILogger<RamMetricsController>>();
            _repository = new Mock<IRepositoryService<DtoRamMetric>>();
            _controller = new RamMetricsController(logger.Object, _repository.Object);
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
