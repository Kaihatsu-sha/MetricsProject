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
    public class NetworkMetricsControllerTests
    {
        private readonly NetworkMetricsController _controller;
        private readonly Mock<IRepositoryService<DtoNetworkMetric>> _repository;
        public NetworkMetricsControllerTests()
        {
            Mock<ILogger<NetworkMetricsController>> logger = new Mock<ILogger<NetworkMetricsController>>();
            _repository = new Mock<IRepositoryService<DtoNetworkMetric>>();
            _controller = new NetworkMetricsController(logger.Object, _repository.Object);
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
